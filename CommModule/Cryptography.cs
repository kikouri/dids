using System;
using System.Text;

using CommModule.Messages;
using System.Security.Cryptography;
using System.IO;

namespace CommModule
{
    public static class Cryptography
    {
        
        public static string generateAESKey()
        {
            Aes aes = new AesManaged();
            aes.GenerateKey();

            byte[] output = new byte[aes.Key.Length];
            output = aes.Key;

            return System.Convert.ToBase64String(output);
        }
        
        
        /*
         * Will use public keys
         * Divide the message in blocks because of .NET RSA implementation limitations...
         */
        public static string encryptMessageRSA(string text, string key)
        {
            const int blockSize = 64;
            
            UTF8Encoding enc = new UTF8Encoding();
            byte[] messageBytes = enc.GetBytes(text);

            int timesToEncrypt = (int)Math.Floor((double)messageBytes.Length / (double)blockSize);
            int howMuchLeft = messageBytes.Length % blockSize;

            byte[] finalData = new byte[(timesToEncrypt+(howMuchLeft > 0 ? 1 : 0)) * 128];
            byte[] encryptionStep;

            int bytesEncrypted = 0;
            
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSA.FromXmlString(key);

            for(int i = 0; i < timesToEncrypt; ++i)
            {
                byte[] bytesToEncryptNow = new byte[blockSize];
                Array.Copy(messageBytes, i * blockSize, bytesToEncryptNow, 0, blockSize);
                
                encryptionStep = RSA.Encrypt(bytesToEncryptNow, true);
                bytesEncrypted += encryptionStep.Length;

                Array.Copy(encryptionStep, 0, finalData, i*encryptionStep.Length, encryptionStep.Length);
            }

            if (howMuchLeft > 0)
            {
                byte[] bytesToEncryptNow = new byte[howMuchLeft];
                Array.Copy(messageBytes, timesToEncrypt * blockSize, bytesToEncryptNow, 0, howMuchLeft);

                encryptionStep = RSA.Encrypt(bytesToEncryptNow, true);

                Array.Copy(encryptionStep, 0, finalData, bytesEncrypted, encryptionStep.Length);
            } 
            return System.Convert.ToBase64String(finalData);
        }

        /*
         * Will use private keys 
         */
        public static string decryptMessageRSA(string text, string key)
        {
            const int blockSize = 128;
            UTF8Encoding enc = new UTF8Encoding();

            byte[] messageBytes = System.Convert.FromBase64String(text);

            int timesToDecrypt = (int)Math.Floor((double)messageBytes.Length / (double)blockSize);
            int howMuchLeft = messageBytes.Length % blockSize;

            string finalData = "";
            byte[] decryptionStep;

            int bytesDecrypted = 0;

            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSA.FromXmlString(key);

            for (int i = 0; i < timesToDecrypt; ++i)
            {
                byte[] bytesToDecryptNow = new byte[blockSize];
                Array.Copy(messageBytes, i * blockSize, bytesToDecryptNow, 0, blockSize);

                decryptionStep = RSA.Decrypt(bytesToDecryptNow, true);
                bytesDecrypted += decryptionStep.Length;

                finalData += enc.GetString(decryptionStep);
            }

            if (howMuchLeft > 0)
            {
                byte[] bytesToEncryptNow = new byte[howMuchLeft];
                Array.Copy(messageBytes, timesToDecrypt * blockSize, bytesToEncryptNow, 0, howMuchLeft);

                decryptionStep = RSA.Encrypt(bytesToEncryptNow, true);

                finalData += enc.GetString(decryptionStep);
            }
            return finalData;
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
            byte[] encryptedMessageAndIV = null;
            try
            {
                encryptedMessageAndIV = System.Convert.FromBase64String(text);
            }
            catch (Exception e)
            {
                return null;
            }
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

        public static void signMessageAES(GenericMessage message, string key)
        {
            string hash = applySHA256(message.ObjectString);
            message.Signature = encryptMessageAES(hash, key);
        }

        public static bool checkMessageSignatureAES(GenericMessage message, string key)
        {
            string hash = applySHA256(message.ObjectString);
            string signature = decryptMessageAES(message.Signature, key);

            return (hash == signature);
        }

        public static void signMessageRSA(GenericMessage message, string key)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(key);

            UTF8Encoding enc = new UTF8Encoding();
            byte[] signed = rsa.SignData(enc.GetBytes(message.ObjectString), CryptoConfig.MapNameToOID("SHA1"));

            message.Signature = System.Convert.ToBase64String(signed);
        }

        public static bool checkMessageSignatureRSA(GenericMessage message, string key)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(key);

            UTF8Encoding enc = new UTF8Encoding();

            return rsa.VerifyData(enc.GetBytes(message.ObjectString), CryptoConfig.MapNameToOID("SHA1"), 
                System.Convert.FromBase64String(message.Signature));
        }


        public static void signCertificate(Certificate cert, string key)
        {
            string acum = cert.SerialNumber.ToString();
            acum += cert.Issuer;
            acum += cert.Subject;
            acum += cert.Validity.ToString();
            acum += cert.SubjectPublicKey;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(key);

            UTF8Encoding enc = new UTF8Encoding();
            byte[] signed = rsa.SignData(enc.GetBytes(acum), CryptoConfig.MapNameToOID("SHA1"));

            cert.Signature = System.Convert.ToBase64String(signed);
        }

        public static bool checkCertificateSignature(Certificate cert, string key)
        {
            string acum = cert.SerialNumber.ToString();
            acum += cert.Issuer;
            acum += cert.Subject;
            acum += cert.Validity.ToString();
            acum += cert.SubjectPublicKey;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(key);

            UTF8Encoding enc = new UTF8Encoding();

            return rsa.VerifyData(enc.GetBytes(acum), CryptoConfig.MapNameToOID("SHA1"),
                System.Convert.FromBase64String(cert.Signature));
        }
    }
}
