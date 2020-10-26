using System;
using System.Threading;
using System.Threading.Tasks;
using Uploadcare.Clients;
using Uploadcare.DTO;
using Uploadcare.Exceptions;
using Uploadcare.Models;
using Uploadcare.Utils;

namespace Uploadcare.Upload
{
    /// <summary>
    /// Uploadcare uploader for URLs.
    /// </summary>
    public sealed class UrlUploader
    {
        private readonly UploadcareClient _client;
        private readonly int _pollingIntervalMsec;

        private CancellationTokenSource cts;

        /// <summary>
        /// Create a new uploader from a URL.
        /// </summary>
        /// <param name="client"> Uploadcare client </param>
        /// <param name="pollingIntervalMsec"> Time interval for polling upload progress </param>
        public UrlUploader(IUploadcareClient client, int pollingIntervalMsec = 500)
        {
            _client = (UploadcareClient)client;
            _pollingIntervalMsec = pollingIntervalMsec;
        }

        /// <summary>
        /// Uploads the file to Uploadcare from URL.
        /// </summary>
        /// <param name="sourceUrl">Defines your source file URL, which should be a public HTTP or HTTPS link</param>
        /// <param name="store">Sets the file storing behavior. Once stored, files are not deleted after a 24-hour period</param>
        /// <returns> UploadcareFile resource </returns>
        /// <exception cref="UploadFailureException"></exception>
        public async Task<UploadcareFile> Upload(string sourceUrl, bool? store = null)
        {
            if (string.IsNullOrEmpty(sourceUrl))
            {
                throw new ArgumentNullException(nameof(sourceUrl));
            }

            var requestHelper = _client.GetRequestHelper();
            var uploadUrl = Urls.UploadFromUrl(sourceUrl, _client.PublicKey, store);

            var uploadData = await requestHelper.Get<UploadFromUrlData>(uploadUrl, default);

            using (cts = new CancellationTokenSource())
            {
                var task = Task.Run(() => PoolStatus(requestHelper, uploadData.Token, cts.Token), cts.Token);

                var statusData = task.Result;

                return await _client.Files.GetAsync(statusData.FileId);
            }
        }

        private async Task<UploadFromUrlStatusData> PoolStatus(RequestHelper requestHelper, string fileToken, CancellationToken cancellationToken)
        {
            var statusUrl = Urls.UploadFromUrlStatus(fileToken);

            while (true)
            {
                await Task.Delay(_pollingIntervalMsec, cancellationToken);

                var data = await requestHelper.Get<UploadFromUrlStatusData>(statusUrl, default);

                if (data.Status.Equals("success"))
                {
                    return data;
                }

                if (data.Status.Equals("error") || data.Status.Equals("failed"))
                {
                    throw new UploadFailureException(data.Error);
                }

                cancellationToken.ThrowIfCancellationRequested();
            }
        }
    }
}
