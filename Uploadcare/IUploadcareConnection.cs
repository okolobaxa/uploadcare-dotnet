namespace Uploadcare
{
    public interface IUploadcareConnection
    {
        UploadcareAuthType AuthType { get; }

        string PublicKey { get; }

        string PrivateKey { get; }
    }
}