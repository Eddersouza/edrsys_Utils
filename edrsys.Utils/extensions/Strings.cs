using System;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace edrsys.Utils.extensions
{
    /// <summary>
    /// Extentions to string class
    /// </summary>
    public static class Strings
    {
        /// <summary>
        /// Dencript value.
        /// </summary>
        /// <param name="value">Value to dencription</param>
        /// <param name="EncryptionKey">Key to dencription.</param>
        /// <returns>Value encripted.</returns>
        public static string Decrypt(
            this string value,
            string EncryptionKey)
        {
            byte[] cipherBytes = Convert.FromBase64String(value);

            string cipherText = string.Empty;

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb =
                    new Rfc2898DeriveBytes(
                        EncryptionKey,
                        new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs =
                        new CryptoStream(
                            ms,
                            encryptor.CreateDecryptor(),
                            CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }

                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return cipherText;
        }

        /// <summary>
        /// Encript value.
        /// </summary>
        /// <param name="value">Value to encription</param>
        /// <param name="EncryptionKey">Key to encription.</param>
        /// <returns>Value encripted.</returns>
        public static string Encrypt(
            this string value,
            string EncryptionKey)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(value);
            string clearText = string.Empty;
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb =
                    new Rfc2898DeriveBytes(
                        EncryptionKey,
                        new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(
                        ms,
                        encryptor.CreateEncryptor(),
                        CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        /// <summary>
        /// Verify if is email.
        /// </summary>
        /// <param name="value">Value to verify</param>
        /// <returns>True if is email, false if not</returns>
        public static bool IsEmail(this string value)
        {
            try
            {
                MailAddress m = new MailAddress(value);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}