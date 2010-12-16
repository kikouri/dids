using System;
using System.Text;

using CommModule.Messages;
using System.Security.Cryptography;
using System.IO;

namespace CommModule
{
    public static class Cryptography
    {              
        public static string encryptMessageRSA(string text, string key)
        {
            return text;
        }

        public static string decryptMessageRSA(string text, string key)
        {
            return text;
        }

        public static string encryptMessageAES(string text, string key)
        {
            UTF8Encoding enc = new UTF8Encoding();
            byte[] messageBytes = enc.GetBytes(text);
            
            Aes aes = new AesManaged();

            try
            {
                aes.Key = System.Convert.FromBase64String(key);
            }
            catch (FormatException)
            {
                return null;
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

            return System.Convert.ToBase64String(dataToSend);
        }

        public static string decryptMessageAES(string text, string key)
        {
            byte[] encryptedMessageAndIV = System.Convert.FromBase64String(text);
            
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

            byte[] decryptedMessage = new byte[encryptedMessageSize];

            try
            {
                cryptoStream.Read(decryptedMessage, 0, encryptedMessageSize);

                UTF8Encoding enc = new UTF8Encoding();
                return enc.GetString(decryptedMessage).TrimEnd('\0');
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string applySHA256(string text)
        {
            //To avoid some nasty problems...
            text = text.Remove('\r');
            text = text.Remove('\n');
            
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(text));
            return System.Convert.ToBase64String(hashedDataBytes);
        }

        public static void signMessage(GenericMessage message, string key)
        {
            string hash = applySHA256(message.ObjectString);
            message.Signature = encryptMessageAES(hash, key);
        }

        public static bool checkMessageSignature(GenericMessage message, string key)
        {
            string hash = applySHA256(message.ObjectString);
            string signature = decryptMessageAES(message.Signature, key);

            return (hash == signature);
        }

        public static void signCertificate(Certificate cert, string key)
        {
            string acum = cert.SerialNumber.ToString();
            acum += cert.Issuer;
            acum += cert.Subject;
            acum += cert.Validity.ToString();
            acum += cert.SubjectPublicKey;
            
            string hash = applySHA256(acum);
            cert.Signature = encryptMessageAES(hash, key);
        }

        public static bool checkCertificateSignature(Certificate cert, string key)
        {
            string acum = cert.SerialNumber.ToString();
            acum += cert.Issuer;
            acum += cert.Subject;
            acum += cert.Validity.ToString();
            acum += cert.SubjectPublicKey;
            
            string hash = applySHA256(acum);
            string signature = decryptMessageAES(cert.Signature, key);

            return (hash == signature);
        }
    }
}
