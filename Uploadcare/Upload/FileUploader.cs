using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Uploadcare.Clients;
using Uploadcare.DTO;
using Uploadcare.Exceptions;
using Uploadcare.Models;
using Uploadcare.Utils;

namespace Uploadcare.Upload
{
    /// <summary>
    /// Uploadcare uploader for files and binary data.
    /// </summary>
    public class FileUploader
    {
        protected readonly UploadcareClient Client;

        private const string PublicKeyContent = "UPLOADCARE_PUB_KEY";
        private const string StoreContent = "UPLOADCARE_STORE";


        /// <summary>
        /// Creates a new uploader from binary data.
        /// </summary>
        /// <param name="client"> Uploadcare client </param>
        public FileUploader(IUploadcareClient client)
        {
            Client = (UploadcareClient)client;
        }

        /// <summary>
        /// Uploads the file to Uploadcare.
        /// </summary>
        /// <param name="bytes">File binary data</param>
        /// <param name="filename">File name</param>
        /// <param name="store">Sets the file storing behavior. In this context, storing a file means making it permanently available</param>
        /// <returns> An Uploadcare file </returns>
        /// <exception cref="UploadFailureException"> </exception>
        public Task<UploadcareFile> Upload(byte[] bytes, string filename, bool? store = null)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException(nameof(filename));
            }

            var content = new ByteArrayContent(bytes);

            return UploadInternal(content, filename, store);
        }

        /// <summary>
        /// Uploads the file to Uploadcare.
        /// </summary>
        /// <param name="bytes">File information</param>
        /// <param name="store">Sets the file storing behavior. In this context, storing a file means making it permanently available</param>
        /// <returns> An Uploadcare file </returns>
        /// <exception cref="UploadFailureException"> </exception>
        public Task<UploadcareFile> Upload(FileInfo fileInfo, bool? store = null)
        {
            if (fileInfo == null)
            {
                throw new ArgumentNullException(nameof(fileInfo));
            }

            var content = new StreamContent(File.OpenRead(fileInfo.FullName));

            return UploadInternal(content, fileInfo.FullName, store);
        }

        private async Task<UploadcareFile> UploadInternal(HttpContent binaryContent, string filename, bool? store = null)
        {
            var url = Urls.UploadBase;

            try
            {
                var boundary = DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);

                using (var content = new MultipartFormDataContent(boundary))
                {
                    content.Add(new StringContent(Client.PublicKey), PublicKeyContent);

                    if (store != null)
                    {
                        content.Add(store.Value ? new StringContent("1") : new StringContent("0"), StoreContent);
                    }
                    else
                    {
                        content.Add(new StringContent("auto"), StoreContent);
                    }

                    AddAdditionalContent(content);

                    content.Add(binaryContent, "file", filename);

                    var file = await Client.GetRequestHelper().PostContent<UploadcareFileBaseData>(url, content);

                    return await Client.Files.GetAsync(file.File);
                }
            }
            catch (Exception e)
            {
                throw new UploadFailureException(e);
            }
        }

        protected virtual void AddAdditionalContent(MultipartFormDataContent content) { }
    }

}