using System.IO;
using UploadcareCSharp.API;
using UploadcareCSharp.Enums;
using UploadcareCSharp.Upload;
using Xunit;

namespace UploadcareCSharp.Tests
{
	public class FileUploaderTest
    {
        [Fact]
        public void fileuploader_upload_notstore()
        {
            var client = Client.DemoClient();
            var file = new FileInfo("test.png");

            var uploader = new FileUploader(client, file);
            var result = uploader.Upload(EStoreType.DoNotStore);

            Assert.NotNull(result.FileId);
            Assert.False(result.Stored);
        }

        [Fact]
        public void fileuploader_upload_store()
        {
            var client = Client.DemoClient();
            var file = new FileInfo("test.png");

            var uploader = new FileUploader(client, file);
            var result = uploader.Upload(EStoreType.Store);

            Assert.NotNull(result.FileId);
            Assert.True(result.Stored);
        }

        [Fact]
        public void fileuploader_upload_bytes()
        {
            var client = Client.DemoClient();
            var file = new FileInfo("test.png");
            var bytes = File.ReadAllBytes(file.FullName);

            var uploader = new FileUploader(client, bytes, file.Name);
            var result = uploader.Upload(EStoreType.Store);

            Assert.NotNull(result.FileId);
            Assert.True(result.Stored);
        }
	}

}