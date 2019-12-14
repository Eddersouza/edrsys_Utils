using System;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace edrsys.Utils.extensions
{
    public static class Strings
    {
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

        public static T GetEnumByCode<T>(
            this string value, 
            Enum defaultEnum) where T : Enum
        {
            Type type = typeof(T);

            foreach (T itemEnum in type.GetEnumValues())
            {
                string temtEnum = itemEnum.GetCode();

                if (value == temtEnum)
                    return itemEnum;
            }

            return (T)defaultEnum;
        }

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

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool LengthGreatherOrEqualThan(this string value, int length)
        {
            return (value ?? string.Empty).Length >= length;
        }

        public static bool LengthGreatherThan(this string value, int length)
        {
            return (value ?? string.Empty).Length > length;
        }

        public static bool LengthLessOrEqualThan(this string value, int length)
        {
            return (value ?? string.Empty).Length < length;
        }

        public static bool LengthLessThan(this string value, int length)
        {
            return (value ?? string.Empty).Length < length;
        }

        public static decimal ToDecimal(this string value)
        {
            decimal valueResult = 0;

            decimal.TryParse(value, out valueResult);

            return valueResult;
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static int ToInt(this string value)
        {
            int valueResult = 0;

            int.TryParse(value, out valueResult);

            return valueResult;
        }

        public static long ToLong(this string value)
        {
            long valueResult = 0;

            long.TryParse(value, out valueResult);

            return valueResult;
        }
    }
}