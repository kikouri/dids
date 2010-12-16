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
                sendMessageWithSpecificKey(message, address, portToSend, null, null, "RSA");
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
                key = _keysManager.getSessionKey(new Node(address, portToSend));
                signatureKey = _keysManager.getCertificate(new Node(address, portToSend)).SubjectPublicKey;
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
                return receiveMessageWithSpecificKey(null, null, "RSA");
            }
        }

        /*
         * If both of the keys are null, will retrieve them from the keyManager. (Default Behaviour)
         */
        public Object receiveMessageWithSpecificKey(string key, string signatureKey, string algorithm)
        {
            IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            EndPoint endPoint = (EndPoint)remoteIpEndPoint;
            byte[] genericMessageBytes = _socket.receiveMessageBytes(ref endPoint);

            remoteIpEndPoint = (IPEndPoint)endPoint;

            GenericMessage gm = ObjectSerialization.DeserializeObjectToGenericMessage(genericMessageBytes);

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
    }
}
