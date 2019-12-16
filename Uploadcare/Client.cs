using Uploadcare.Clients;
using Uploadcare.Utils;

namespace Uploadcare
{
    public sealed class UploadcareClient : IUploadcareClient
    {
        private readonly RequestHelper _requestHelper;

        /// <summary>
        /// Initializes a client with custom access keys.
        /// Can use simple or secure authentication.
        /// </summary>
        /// <param name="publicKey">API public key</param>
        /// <param name="privateKey">API private key</param>
        /// <param name="authType">Type of auth: simple or signed</param>
        public UploadcareClient(string publicKey, string privateKey, UploadcareAuthType authType = UploadcareAuthType.Simple)
        {
            Connection = new UploadcareConnection(publicKey, privateKey, authType);

            _requestHelper = new RequestHelper(Connection);

            Projects = new ProjectsClient(_requestHelper);
            Files = new FilesClient(_requestHelper); 
            Groups = new GroupsClient(_requestHelper);
            Webhooks = new WebhookClient(_requestHelper);
        }

        public UploadcareClient(IUploadcareConnection connection)
        {
            Connection = connection;

            _requestHelper = new RequestHelper(Connection);

            Projects = new ProjectsClient(_requestHelper);
            Files = new FilesClient(_requestHelper);
            Groups = new GroupsClient(_requestHelper);
            Webhooks = new WebhookClient(_requestHelper);
        }

        /// <summary>
        /// Creates a client with demo credentials. Useful for tests and anonymous access.
        /// Warning! Do not use in production.  All demo account files are eventually purged.
        /// </summary>
        /// <returns> A demo client </returns>
        public static IUploadcareClient DemoClient()
        {
            return new UploadcareClient("demopublickey", "demoprivatekey");
        }

        public static IUploadcareClient DemoClientWithSignedAuth()
        {
            return new UploadcareClient("demopublickey", "demoprivatekey", UploadcareAuthType.Signed);
        }

        public IProjectsClient Projects { get; }

        public IFilesClient Files { get; }

        public IGroupsClient Groups { get; }

        public IWebhooksClient Webhooks { get; }

        public string PublicKey => Connection.PublicKey;

        public string PrivateKey => Connection.PrivateKey;

        public UploadcareAuthType AuthType => Connection.AuthType;

        public IUploadcareConnection Connection { get; }

        internal RequestHelper GetRequestHelper()
        {
            return _requestHelper;
        }

        public void Dispose()
        {
            _requestHelper?.Dispose();
        }
    }

}