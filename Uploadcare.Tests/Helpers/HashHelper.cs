using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Uploadcare.Tests.Helpers
{
    internal static class HashHelper
    {
        public static string GetFileHash(string filename)
        {
            var hash = new SHA1Managed();
            var clearBytes = File.ReadAllBytes(filename);
            var hashedBytes = hash.ComputeHash(clearBytes);
            return ConvertBytesToHex(hashedBytes);
        }

        private static string ConvertBytesToHex(byte[] bytes)
        {
            var sb = new StringBuilder();

            foreach (var t in bytes)
            {
                sb.Append(t.ToString("x"));
            }

            return sb.ToString();
        }
    }
}
