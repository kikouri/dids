using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace IDS
{
    class PasswordFile
    {
        public static Boolean ValidatePassword(String password)
        {
            FileStream passwdFile = new FileStream("c:\\IDS\\passwd.bin", FileMode.Open, FileAccess.Read, FileShare.Read, 96);

            byte[] passwordBytes = new byte[64];
            byte[] passwordSalt = new byte[16];

            passwdFile.Read(passwordBytes, 0, 64);
            passwdFile.Read(passwordSalt, 63, 16);

            Rfc2898DeriveBytes receivedPasswordDeriveBytes = new Rfc2898DeriveBytes(password, passwordSalt, 2000);

            byte[] existingPasswordBytes = new byte[64];
            existingPasswordBytes = receivedPasswordDeriveBytes.GetBytes(64);

            for (int i = 0; i < 63; i++)
            {
                if (passwordBytes[i] != existingPasswordBytes[i])
                    return false;
            }

            for (int i = 0; i < 63; i++)
            {
                passwordBytes[i] = 0;
            }

            for (int i = 0; i < 63; i++)
            {
                existingPasswordBytes[i] = 0;
            }

            for (int i = 0; i < 15; i++)
            {
                passwordSalt[i] = 0;
            }

            return true;
        }

        public static void CreatePasswordFile(String password)
        {
            Rfc2898DeriveBytes passwordDeriveBytes = new Rfc2898DeriveBytes(password, 16, 2000);
            Rfc2898DeriveBytes contentPasswordDeriveBytes = new Rfc2898DeriveBytes(password, 16, 2000);

            byte[] passwordBytes = new byte[64];
            byte[] passwordSalt = new byte[16];
            byte[] contentSalt = new byte[16];

            passwordBytes = passwordDeriveBytes.GetBytes(64);
            passwordSalt = passwordDeriveBytes.Salt;
            contentSalt = contentPasswordDeriveBytes.Salt;

            Directory.CreateDirectory("c:\\IDS");

            FileStream passwdFile = new FileStream("c:\\IDS\\passwd.bin", FileMode.Create, FileAccess.Write, FileShare.Read, 96);
            passwdFile.Write(passwordBytes, 0, 64);
            passwdFile.Write(passwordSalt, 0, 16);
            passwdFile.Write(contentSalt, 0, 16);

            passwdFile.Flush();
            passwdFile.Close();
            passwdFile.Dispose();

            for (int i = 0; i < 63; i++)
            {
                passwordBytes[i] = 0;
            }

            for (int i = 0; i < 15; i++)
            {
                passwordSalt[i] = 0;
            }

            for (int i = 0; i < 15; i++)
            {
                contentSalt[i] = 0;
            }
        }

        public static byte[] getContentCipheringKey(String password)
        {
            FileStream passwdFile = new FileStream("c:\\IDS\\passwd.bin", FileMode.Open, FileAccess.Read, FileShare.Read, 96);

            byte[] trashBytes = new byte[80];
            byte[] contentSalt = new byte[16];

            passwdFile.Read(trashBytes, 0, 80);
            passwdFile.Read(contentSalt, 79, 16);

            Rfc2898DeriveBytes contentKey = new Rfc2898DeriveBytes(password, contentSalt, 2000);

            byte[] contentKeyBytes = new byte[64];
            contentKeyBytes = contentKey.GetBytes(64);

            for (int i = 0; i < 79; i++)
            {
                trashBytes[i] = 0;
            }

            for (int i = 0; i < 15; i++)
            {
                contentSalt[i] = 0;
            }

            return contentKeyBytes;
        }
    }
}
