using System.IO;
using UploadCareCSharp.API;
using UploadcareCSharp.Upload;
using Xunit;

namespace UploadcareCSharp.Tests
{
	public class FileUploaderTest
    {
        [Fact]
        public void fileuploader_upload_assert()
        {
            var client = Client.DemoClient();
            var file = new FileInfo("test.png");

            var uploader = new FileUploader(client, file);
            var result = uploader.Upload();

            Assert.NotNull(result.FileId);
        }

        [Fact]
        public void fileuploader_upload_bytes()
        {
            var client = Client.DemoClient();
            var file = new FileInfo("test.png");
            var bytes = File.ReadAllBytes(file.FullName);

            var uploader = new FileUploader(client, bytes, file.Name);
            var result = uploader.Upload();

            Assert.NotNull(result.FileId);
        }
	}

}