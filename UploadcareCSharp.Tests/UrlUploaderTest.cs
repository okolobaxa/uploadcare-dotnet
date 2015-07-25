using System.IO;
using UploadcareCSharp.API;
using UploadcareCSharp.Upload;
using Xunit;

namespace UploadcareCSharp.Tests
{
    public class UrlUploaderTest
    {
        private const string URL = "http://oldsaratov.ru/sites/default/files/logo.png";

        [Fact]
        public void urluploader_upload_assert()
        {
            var client = Client.DemoClient();

            var uploader = new UrlUploader(client, URL);
            var result = uploader.Upload();

            Assert.NotNull(result.FileId);
        }
    }
}