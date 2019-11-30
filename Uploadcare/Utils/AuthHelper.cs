using System.Text;

namespace Uploadcare.Utils
{
    internal static class AuthHeaderHelper
    {
        public static string GetSimple(string publicKey, string privateKey)
        {
            return $"Uploadcare.Simple {publicKey}:{privateKey}";
        }

        public static string GetSigned(string publicKey, string signature)
        {
            return $"Uploadcare {publicKey}:{signature}";
        }

        public static string CombineDataForSignature(string httpMethod, string requestBodyHash, string contentTypeHeader, string dateHeader, string uri)
        {
            var sb = new StringBuilder();

            sb.Append(httpMethod).Append('\n');
            sb.Append(requestBodyHash).Append('\n');
            sb.Append(contentTypeHeader).Append('\n');
            sb.Append(dateHeader).Append('\n');
            sb.Append(uri);

            return sb.ToString();
        }
    }
}
