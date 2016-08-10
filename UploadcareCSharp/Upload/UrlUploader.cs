using System.Net;
using System.Threading;
using UploadcareCSharp.API;
using UploadcareCSharp.Data;
using UploadcareCSharp.Exceptions;
using UploadcareCSharp.Url;

namespace UploadcareCSharp.Upload
{
    /// <summary>
    /// Uploadcare uploader for URLs.
    /// </summary>
    internal class UrlUploader : IUploader
    {
        private readonly Client _client;
        private readonly string _sourceUrl;


        /// <summary>
        /// Create a new uploader from a URL.
        /// </summary>
        /// <param name="client"> Uploadcare client </param>
        /// <param name="sourceUrl"> URL to upload from </param>
        public UrlUploader(Client client, string sourceUrl)
        {
            _client = client;
            _sourceUrl = sourceUrl;
        }

        /// <summary>
        /// Synchronously uploads the file to Uploadcare.
        /// The calling thread will be busy until the upload is finished.
        /// Uploadcare is polled every 500 ms for upload progress.
        /// </summary>
        /// <returns> UploadcareFile resource </returns>
        /// <exception cref="UploadFailureException"></exception>
        public UploadcareFile Upload(bool? store = null)
        {
            if (store != null)
                throw new NotImplementedException();
            return Upload(500);
        }

        /// <summary>
        /// Synchronously uploads the file to Uploadcare.
        /// The calling thread will be busy until the upload is finished.
        /// </summary>
        /// <param name="pollingInterval">Pooling interval</param>
        /// <returns> UploadcareFile resource </returns>
        /// <exception cref="UploadFailureException"></exception>
        public UploadcareFile Upload(int pollingInterval)
        {
            var requestHelper = _client.GetRequestHelper();
            var uploadUrl = Urls.UploadFromUrl(_sourceUrl, _client.PublicKey);

            var request = (HttpWebRequest) WebRequest.Create(uploadUrl);
            request.Method = "GET";

            var token = requestHelper.ExecuteQuery(request, false, new UploadFromUrlData()).Token;
            var statusUrl = Urls.UploadFromUrlStatus(token);
            while (true)
            {
                Sleep(pollingInterval);
                
                var statusRequest = (HttpWebRequest) WebRequest.Create(statusUrl);
                request.Method = "GET";

                var data = requestHelper.ExecuteQuery(statusRequest, false, new UploadFromUrlStatusData());
               
                if (data.Status.Equals("success"))
                {
                    return _client.GetFile(data.FileId);
                }
                if (data.Status.Equals("error") || data.Status.Equals("failed"))
                {
                    throw new UploadFailureException();
                }
            }
        }

        private static void Sleep(int millis)
        {
            try
            {
                Thread.Sleep(millis);
            }
            catch (ThreadInterruptedException)
            {
                Thread.CurrentThread.Interrupt();
            }
        }
    }
}
