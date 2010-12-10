using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

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

                //sendMessageWithSpecificKey(message, address, portToSend, sessionkey);
            }
                    
        }

        public void sendMessageWithSpecificKey(Object message, String address, int portToSend, string key)
        {
            byte[] messageBytes = ObjectSerialization.SerializeObject(message);

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

        public Object receiveMessageWithSpecificKey(string key)
        {
            byte[] encryptedMessageAndIV = _socket.receiveMessageBytes();

            int IVSize = 16;
            int encryptedMessageSize = encryptedMessageAndIV.Length - IVSize;

            Aes aes = new AesManaged();

            try
            {
                aes.Key = System.Convert.FromBase64String(key);
            }
            catch(Exception) 
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

            byte[] decryptedMessage = new byte[encryptedMessageSize];

            try
            {
                cryptoStream.Read(decryptedMessage, 0, encryptedMessageSize);
            }
            catch(Exception) 
            {
                return null;
            }

            Object message = ObjectSerialization.DeserializeObject(decryptedMessage);
            return message;
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
