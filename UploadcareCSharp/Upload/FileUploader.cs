using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using UploadcareCSharp.API;
using UploadcareCSharp.Data;
using UploadcareCSharp.Enums;
using UploadcareCSharp.Exceptions;
using UploadcareCSharp.Url;

namespace UploadcareCSharp.Upload
{
	/// <summary>
	/// Uploadcare uploader for files and binary data.
	/// </summary>
	public sealed class FileUploader : IUploader
	{
		private readonly Client _client;
        private readonly FileInfo _file;
        private readonly byte[] _bytes;
        private readonly string _fileName;

		/// <summary>
		/// Creates a new uploader from a file on disk
		/// (not to be confused with a file resource from Uploadcare API).
		/// </summary>
		/// <param name="client"> Uploadcare client </param>
		/// <param name="file"> File on disk </param>
        public FileUploader(Client client, FileInfo file)
		{
			_client = client;
			_file = file;
            _bytes = null;
            _fileName = null;
		}

        /// <summary>
        /// Creates a new uploader from binary data.
        /// </summary>
        /// <param name="client"> Uploadcare client </param>
        /// <param name="bytes"> File contents as binary data </param>
        /// <param name="filename"> Original filename </param>
        public FileUploader(Client client, byte[] bytes, string filename)
        {
            _client = client;
            _file = null;
            _bytes = bytes;
            _fileName = filename;
        }

		/// <summary>
		/// Synchronously uploads the file to Uploadcare.
		/// 
		/// The calling thread will be busy until the upload is finished.
		/// </summary>
		/// <returns> An Uploadcare file </returns>
		/// <exception cref="UploadFailureException"> </exception>
        public UploadcareFile Upload(EStoreType storetype)
        {
            var url = Urls.UploadBase();

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            try
            {
                var boundary = "Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                using (var content = new MultipartFormDataContent(boundary))
                {
                    content.Add(new StringContent(_client.PublicKey), "UPLOADCARE_PUB_KEY");
                    content.Add(new StringContent(GetUploadType(storetype)), "UPLOADCARE_STORE");
                    if (_file != null)
                        content.Add(new StreamContent(File.OpenRead(_file.FullName)), "file", _file.Name);
                    else
                        content.Add(new ByteArrayContent(_bytes), "file", _fileName);
                    var buffer = content.ReadAsByteArrayAsync().Result;
                    using (var reqStream = request.GetRequestStream())
                    {
                        reqStream.Write(buffer, 0, buffer.Length);
                    }

                    request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);


                    var fileId = _client.GetRequestHelper().ExecuteQuery(request, false, new UploadBaseData()).File;

                    return _client.GetFile(fileId);
                }
            }
            catch (Exception ex)
            {
                throw new UploadFailureException();
            }
        }

        private static string GetUploadType(EStoreType type)
        {
            switch(type)
            {
                case EStoreType.DoNotStore: return "0";
                case EStoreType.Store: return "1";
                case EStoreType.Auto: return "auto";
                default: throw new ArgumentException("");
            }
        }
	}

}

