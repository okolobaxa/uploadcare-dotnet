using System;

namespace Uploadcare.Clients
{
    /// <summary>
    /// Uploadcare API client.
    /// 
    /// Provides simple access to UploadcareFile and Project resources.
    /// </summary>
    public interface IUploadcareClient : IDisposable
    {
        IProjectsClient Projects { get; }

        IFilesClient Files { get; }

        IGroupsClient Groups { get; }

        IWebhooksClient Webhooks { get; }

        IFaceDetectionClient FaceDetection { get; }

        /// <summary>
        /// Returns the public key.
        /// </summary>
        /// <returns> Public key </returns>
        string PublicKey { get; }

        /// <summary>
        /// Returns the private key.
        /// </summary>
        /// <returns> Private key </returns>
        string PrivateKey { get; }

        /// <summary>
        /// Returns true, if simple authentication is used.
        /// </summary>
        /// <returns> true, if simple authentication is used, false otherwise </returns>
        UploadcareAuthType AuthType { get; }

        IUploadcareConnection Connection { get; }
    }
}
