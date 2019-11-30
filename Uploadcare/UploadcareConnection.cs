namespace Uploadcare
{
    public class UploadcareConnection : IUploadcareConnection
    {
        public UploadcareConnection(string publicKey, string privateKey, UploadcareAuthType authType)
        {
            PublicKey = publicKey;
            PrivateKey = privateKey;
            AuthType = authType;
        }

        public UploadcareAuthType AuthType { get; }
        public string PublicKey { get; }
        public string PrivateKey { get; }
    }
}
