using System;
using System.Security.Cryptography;
using System.Text;

namespace Uploadcare.Utils
{
    internal static class CryptoHelper
    {
        public static string StringToMD5(string s)
        {
            using (var md5 = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(s);
                var hashBytes = md5.ComputeHash(bytes);

                return HexStringFromBytes(hashBytes);
            }
        }

        public static string BytesToMD5(byte[] bytes)
        {
            using (var md5 = MD5.Create())
            {
                var hashBytes = md5.ComputeHash(bytes);

                return HexStringFromBytes(hashBytes);
            }
        }

        public static string Sign(string stringToBeSigned, string privateKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(privateKey);

            using (var hmac = new HMACSHA1(keyBytes))
            {
                hmac.Initialize();

                var dataBytes = Encoding.UTF8.GetBytes(stringToBeSigned);

                var hashBytes = hmac.ComputeHash(dataBytes);

                return HexStringFromBytes(hashBytes);
            }
        }

        private static string HexStringFromBytes(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
