using System.IO;
using Uploadcare.API;
using Uploadcare.Upload;
using UploadcareCSharp.Enums;
using Xunit;

namespace Uploadcare.Test
{
	public class FileUploaderTest
    {
        [Fact]
        public void fileuploader_upload_notstore()
        {
            var _client = Client.DemoClient();
            var file = new FileInfo("test.png");

            var uploader = new FileUploader(_client, file);
            var result = uploader.Upload(EStoreType.DoNotStore);

            Assert.NotNull(result.FileId);
            Assert.False(result.Stored);
        }

        [Fact]
        public void fileuploader_upload_store()
        {
            var _client = Client.DemoClient();
            var file = new FileInfo("test.png");

            var uploader = new FileUploader(_client, file);
            var result = uploader.Upload(EStoreType.Store);

            Assert.NotNull(result.FileId);
            Assert.True(result.Stored);
        }

        [Fact]
        public void fileuploader_upload_bytes()
        {
            var _client = Client.DemoClient();
            var file = new FileInfo("test.png");
            var bytes = File.ReadAllBytes(file.FullName);

            var uploader = new FileUploader(_client, bytes, file.Name);
            var result = uploader.Upload(EStoreType.Store);

            Assert.NotNull(result.FileId);
            Assert.True(result.Stored);
        }
	}

}