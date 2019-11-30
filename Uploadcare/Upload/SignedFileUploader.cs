using System;
using System.Globalization;
using System.Net.Http;
using Uploadcare.Utils;

namespace Uploadcare.Upload
{
    /// <summary>
    /// Uploadcare signed uploader for files and binary data.
    /// </summary>
    public sealed class SignedFileUploader : FileUploader
    {
        private readonly TimeSpan _expireTime;

        /// <summary>
        /// Creates a new uploader from binary data.
        /// </summary>
        /// <param name="client"> Uploadcare client </param>
        /// <param name="expireTime"> Expiration time for signature </param>
        public SignedFileUploader(UploadcareClient client, TimeSpan expireTime) : base(client)
        {
            _expireTime = expireTime;
        }

        protected override void AddAdditionalContent(MultipartFormDataContent content)
        {
            var expire = (DateTimeOffset.UtcNow.ToUnixTimeSeconds() + _expireTime.TotalSeconds).ToString(CultureInfo.InvariantCulture);
            var signature = CryptoHelper.StringToMD5(Client.PrivateKey + expire);

            content.Add(new StringContent(expire), "expire");
            content.Add(new StringContent(signature), "signature");
        }
    }
}
