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

        public UDPSecureSocket(int port)
        {
            _socket = new UDPSocket(port);
            _bypass = true;
        }

        public void sendMessage(Object message, String address, int portToSend)
        {
            if (_bypass)
            {
                _socket.sendMessage(message, address, portToSend);
            }
            else
            {
                //get session key from somewhere

                //sendMessageWithSpecificKey(message, address, portToSend, sessionkey, privateKey);
            }
                    
        }

        /*
         * Signature key is the key used to sign the message, usually is the private key, in one case is the IAK.
         */
        public void sendMessageWithSpecificKey(Object message, String address, int portToSend, string key, string signatureKey)
        {
            GenericMessage gm = ObjectSerialization.SerializeObjectToGenericMessage(message);
            signMessage(gm, signatureKey);
            byte[] messageBytes = ObjectSerialization.SerializeGenericMessage(gm);

            if (key != null)
            {
                Aes aes = new AesManaged();

                try
                {
                    aes.Key = System.Convert.FromBase64String(key);
                }
                catch (FormatException)
                {
                    return;
                }
                aes.GenerateIV();

                ICryptoTransform transform = aes.CreateEncryptor();

                MemoryStream memStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memStream, transform, CryptoStreamMode.Write);

                cryptoStream.Write(messageBytes, 0, messageBytes.Length);
                cryptoStream.FlushFinalBlock();
                memStream.Close();

                byte[] encryptedMessage = memStream.ToArray();

                //Appends the IV to the end of the encrypted message
                byte[] dataToSend = new byte[encryptedMessage.Length + aes.IV.Length];
                Array.Copy(encryptedMessage, dataToSend, encryptedMessage.Length);
                Array.Copy(aes.IV, 0, dataToSend, encryptedMessage.Length, aes.IV.Length);

                _socket.sendMessageBytes(dataToSend, address, portToSend);
            }
            else
            {
                _socket.sendMessageBytes(messageBytes, address, portToSend);
            }
        }

        public Object receiveMessage()
        {
            if (_bypass)
            {
                return _socket.receiveMessage();
            }
            else
            {
                //get session key from somewhere

                //return receiveMessageWithSpecificKey(key);

                return null;
            }
        }

        public Object receiveMessageWithSpecificKey(string key, string signatureKey)
        {
            byte[] encryptedMessageAndIV = _socket.receiveMessageBytes();
            byte[] decryptedMessage = null;

            if (key != null)
            {
                int IVSize = 16;
                int encryptedMessageSize = encryptedMessageAndIV.Length - IVSize;

                Aes aes = new AesManaged();

                try
                {
                    aes.Key = System.Convert.FromBase64String(key);
                }
                catch (Exception)
                {
                    return null;
                }

                //Separates the IV from the encrypted message
                byte[] encryptedMessage = new byte[encryptedMessageSize];
                byte[] IV = new byte[IVSize];
                Array.Copy(encryptedMessageAndIV, 0, encryptedMessage, 0, encryptedMessageSize);
                Array.Copy(encryptedMessageAndIV, encryptedMessageSize, IV, 0, IVSize);

                aes.IV = IV;

                ICryptoTransform transform = aes.CreateDecryptor();

                MemoryStream memStream = new MemoryStream(encryptedMessage);
                CryptoStream cryptoStream = new CryptoStream(memStream, transform, CryptoStreamMode.Read);

                decryptedMessage = new byte[encryptedMessageSize];

                try
                {
                    cryptoStream.Read(decryptedMessage, 0, encryptedMessageSize);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                decryptedMessage = encryptedMessageAndIV;
            }

            GenericMessage gm = ObjectSerialization.DeserializeObjectToGenericMessage(decryptedMessage);

            if (checkSignature(gm, signatureKey) == false)
            {
                return null;
            }
            else 
            {
                return ObjectSerialization.DeserializeGenericMessage(gm);
            }
        }

        public void close()
        {
            _socket.close();
        }

        private void signMessage(GenericMessage message, string key)
        {
            message.Signature = "fsdfsdfs";
        }

        private bool checkSignature(GenericMessage message, string key)
        {
            return true;
        }

        public bool Bypass
        {
            get { return _bypass; }
            set { _bypass = value; }
        }
    }
}
