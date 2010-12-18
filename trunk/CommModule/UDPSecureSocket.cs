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
            _bypass = true;

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
                sendMessageWithSpecificKey(message, address, portToSend, null, null, "AES", "RSA");
            }
        }

        /*
         * Signature key is the key used to sign the message.
         * If both of the keys are null, will retrieve them from the keyManager. (Default Behaviour)
         */
        public void sendMessageWithSpecificKey(Object message, String address, int portToSend, string key, string signatureKey, string cipherAlgorithm, string signatureAlgorithm)
        {
            if (key == null && signatureKey == null)
            {
                Console.WriteLine("[UDPSecureSocket] Checking if there is a session key.");
                key = _keysManager.getSessionKey(address, portToSend, portToSend+1);
                signatureKey = _keysManager.PrivateAndPublicKeys;
            }
            
            GenericMessage gm = ObjectSerialization.SerializeObjectToGenericMessage(message);

            if (signatureAlgorithm == "AES")
            {
                if (signatureKey != null)
                    Cryptography.signMessageAES(gm, signatureKey);
            }
            else
            {
                if (signatureKey != null)
                    Cryptography.signMessageRSA(gm, signatureKey);
            }

            if (cipherAlgorithm == "AES")
            {
                if (key != null)
                    gm.ObjectString = Cryptography.encryptMessageAES(gm.ObjectString, key);
            }
            else
            {
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
                return receiveMessageWithSpecificKey(null, null, "AES", "RSA");
            }
        }

        /*
         * If both of the keys are null, will retrieve them from the keyManager. (Default Behaviour)
         */
        public Object receiveMessageWithSpecificKey(string key, string signatureKey, string cipherAlgorithm, string signatureAlgorithm)
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
                key = (string)_keysManager.getSessionKey(remoteIpEndPoint.Address.ToString(), remoteIpEndPoint.Port-1, remoteIpEndPoint.Port);
                signatureKey = _keysManager.getCertificate(remoteIpEndPoint.Address.ToString(), remoteIpEndPoint.Port-1, remoteIpEndPoint.Port).SubjectPublicKey;
            }


            if (cipherAlgorithm == "AES")
            {
                if (key != null)
                    gm.ObjectString = Cryptography.decryptMessageAES(gm.ObjectString, key);
            }
            else
            {
                if (key != null)
                    gm.ObjectString = Cryptography.decryptMessageRSA(gm.ObjectString, key);
            }

            if(signatureAlgorithm == "AES")
            {
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

                _keysManager.addCertificate(crm.AdressToAnswer, crm.PortToAnswer, crm.PortToAnswer+1, crm.MyCertificate);

                _socket.sendMessage(_keysManager.MyCertificate, crm.AdressToAnswer, crm.PortToAnswer);

                Console.WriteLine("[CommLayer] My certificate was sent.");

                return true;

            }
            /*else if (gm.ObjectType == "CommModule.Messages.Certificate")
            {
                //Its from the PKI, so let it pass to the keys manager
                if (!gm.ObjectString.StartsWith("<?xml"))
                    return false;
                
                Certificate c = (Certificate)ObjectSerialization.DeserializeGenericMessage(gm);

                Console.WriteLine("[CommLayer] Receiving a Certificate from node: " + ep.Address.ToString() + ":" + ep.Port);

                _keysManager.addCertificate(c.SubjectAddress, ep.Port-1, ep.Port, c);

                Console.WriteLine("[CommLayer] I got the certificate from subject: " + c.Subject);

                return true;

            }*/
            else if (gm.ObjectType == "CommModule.Messages.SessionKeyMessage")
            {
                Console.WriteLine("[CommLayer] Receiving a Session key...");
                
                gm.ObjectString = Cryptography.decryptMessageRSA(gm.ObjectString, _keysManager.PrivateAndPublicKeys);

                SessionKeyMessage skm = (SessionKeyMessage)ObjectSerialization.DeserializeGenericMessage(gm);
                
                if (Cryptography.checkMessageSignatureRSA(gm, _keysManager.getCertificate(skm.AdressToAnswer, skm.PortToAnswer, skm.PortToAnswer+1).SubjectPublicKey) == false)
                {
                    return true;
                }

                Console.WriteLine("[CommLayer] Accepting a Session key from node: " + skm.AdressToAnswer + ":" + skm.PortToAnswer);

                _keysManager.addSessionKey(skm.AdressToAnswer, skm.PortToAnswer, skm.PortToAnswer+1, skm.Key, skm.Validity);

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
