using System;
using Uploadcare.Clients;
using Uploadcare.Utils;

namespace Uploadcare
{
    /// <summary>
    /// Uploadcare API client.
    /// 
    /// Provides simple access to UploadcareFile and Project resources.
    /// </summary>
    public sealed class UploadcareClient : IDisposable
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
        public static UploadcareClient DemoClient()
        {
            return new UploadcareClient("demopublickey", "demoprivatekey");
        }

        public static UploadcareClient DemoClientWithSignedAuth()
        {
            return new UploadcareClient("demopublickey", "demoprivatekey", UploadcareAuthType.Signed);
        }

        public IProjectsClient Projects { get; }

        public IFilesClient Files { get; }

        public IGroupsClient Groups { get; }

        public IWebhooksClient Webhooks { get; }

        /// <summary>
        /// Returns the public key.
        /// </summary>
        /// <returns> Public key </returns>
        public string PublicKey => Connection.PublicKey;

        /// <summary>
        /// Returns the private key.
        /// </summary>
        /// <returns> Private key </returns>
        public string PrivateKey => Connection.PrivateKey;

        /// <summary>
        /// Returns true, if simple authentication is used.
        /// </summary>
        /// <returns> true, if simple authentication is used, false otherwise </returns>
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