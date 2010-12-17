using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using CommModule.Messages;
using System.Security.Cryptography;
using System.IO;


namespace CommModule
{
    public class UDPSecureSocket
    {
        private bool _bypass;
        private UDPSocket _socket;
        private KeysManager _keysManager;

        public UDPSecureSocket(int port, KeysManager km)
        {
            _socket = new UDPSocket(port);
            _bypass = false;

            _keysManager = km;
        }

        public void sendMessage(Object message, String address, int portToSend)
        {
            if (_bypass)
            {
                _socket.sendMessage(message, address, portToSend);
            }
            else
            {
                Console.WriteLine("[UDPSecureSocket] Sending");
                sendMessageWithSpecificKey(message, address, portToSend, null, null, "AES");
            }
        }

        /*
         * Signature key is the key used to sign the message.
         * If both of the keys are null, will retrieve them from the keyManager. (Default Behaviour)
         */
        public void sendMessageWithSpecificKey(Object message, String address, int portToSend, string key, string signatureKey, string algorithm)
        {
            if (key == null && signatureKey == null)
            {
                Console.WriteLine("[UDPSecureSocket]Getting session key.");
                key = _keysManager.getSessionKey(new Node(address, portToSend));
                Console.WriteLine("[UDPSecureSocket]Got session key: " + key);
                signatureKey = _keysManager.PrivateAndPublicKeys;
            }
            
            GenericMessage gm = ObjectSerialization.SerializeObjectToGenericMessage(message);

            if (algorithm == "AES")
            {
                if (signatureKey != null)
                    Cryptography.signMessageAES(gm, signatureKey);

                if (key != null)
                    gm.ObjectString = Cryptography.encryptMessageAES(gm.ObjectString, key);
            }
            else
            {
                if (signatureKey != null)
                    Cryptography.signMessageRSA(gm, signatureKey);

                if (key != null)
                    gm.ObjectString = Cryptography.encryptMessageRSA(gm.ObjectString, key);
            }

            byte[] toSend = ObjectSerialization.SerializeGenericMessage(gm);

            _socket.sendMessageBytes(toSend, address, portToSend);
        }

        public Object receiveMessage()
        {
            if (_bypass)
            {
                return _socket.receiveMessage();
            }
            else
            {
                return receiveMessageWithSpecificKey(null, null, "AES");
            }
        }

        /*
         * If both of the keys are null, will retrieve them from the keyManager. (Default Behaviour)
         */
        public Object receiveMessageWithSpecificKey(string key, string signatureKey, string algorithm)
        {
            IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            EndPoint endPoint = (EndPoint)remoteIpEndPoint;

            GenericMessage gm;

            do
            {
                byte[] genericMessageBytes = _socket.receiveMessageBytes(ref endPoint);

                remoteIpEndPoint = (IPEndPoint)endPoint;

                gm = ObjectSerialization.DeserializeObjectToGenericMessage(genericMessageBytes);

            } while (checkIfMessageBelongsToThisLayer(gm, remoteIpEndPoint));

            if (key == null && signatureKey == null)
            {
                key = (string)_keysManager.getSessionKey(new Node(remoteIpEndPoint.Address.ToString(), remoteIpEndPoint.Port));
                signatureKey = _keysManager.getCertificate(new Node(remoteIpEndPoint.Address.ToString(), remoteIpEndPoint.Port)).SubjectPublicKey;
            }


            if (algorithm == "AES")
            {
                if (key != null)
                    gm.ObjectString = Cryptography.decryptMessageAES(gm.ObjectString, key);

                if (signatureKey != null)
                {
                    if (Cryptography.checkMessageSignatureAES(gm, signatureKey) == false)
                    {
                        return null;
                    }
                }
            }
            else
            {
                if (key != null)
                    gm.ObjectString = Cryptography.decryptMessageRSA(gm.ObjectString, key);

                if (signatureKey != null)
                {
                    if (Cryptography.checkMessageSignatureRSA(gm, signatureKey) == false)
                    {
                        return null;
                    }
                }
            }

            return ObjectSerialization.DeserializeGenericMessage(gm);
        }

        public void close()
        {
            _socket.close();
        }


        public bool Bypass
        {
            get { return _bypass; }
            set { _bypass = value; }
        }

        private bool checkIfMessageBelongsToThisLayer(GenericMessage gm, IPEndPoint ep)
        {
            if (gm.ObjectType == "CommModule.Messages.CertificateRequestMessage")
            {            
                CertificateRequestMessage crm = (CertificateRequestMessage) ObjectSerialization.DeserializeGenericMessage(gm);
                
                Console.WriteLine("[CommLayer] Receiving a Certificate request from node: " + crm.AdressToAnswer + ":" + crm.PortToAnswer);

                _keysManager.addCertificate(new Node(crm.AdressToAnswer, crm.PortToAnswer), crm.MyCertificate);
                
                _socket.sendMessage(_keysManager.MyCertificate, crm.AdressToAnswer, crm.PortToAnswer);

                return true;

            }
            else if (gm.ObjectType == "CommModule.Messages.SessionKeyMessage")
            {
                Console.WriteLine("[CommLayer] Receiving a Session key...");
                
                gm.ObjectString = Cryptography.decryptMessageRSA(gm.ObjectString, _keysManager.PrivateAndPublicKeys);

                SessionKeyMessage skm = (SessionKeyMessage)ObjectSerialization.DeserializeGenericMessage(gm);
                
                if (Cryptography.checkMessageSignatureRSA(gm, _keysManager.getCertificate(new Node(skm.AdressToAnswer, skm.PortToAnswer)).SubjectPublicKey) == false)
                {
                    return true;
                }

                Console.WriteLine("[CommLayer] Accepting a Session key from node: " + ep.Address.ToString() + ":" + ep.Port);

                _keysManager.addSessionKey(new Node(skm.AdressToAnswer, skm.PortToAnswer), skm.Key, skm.Validity);

                SessionKeyMessageACK skma = new SessionKeyMessageACK();
                _socket.sendMessage(skma, skm.AdressToAnswer, skm.PortToAnswer);

                return true;
            }
            return false;
        }


        public UDPSocket Socket
        {
            get { return _socket; }
        }
    }
}
