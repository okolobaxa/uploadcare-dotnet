using System;
using System.IO;
using System.Threading.Tasks;
using Uploadcare.Upload;
using Xunit;

namespace Uploadcare.Tests.Uploaders
{
	public class SignedFileUploaderTest
    {
        [Fact]
        public async Task signedfileuploader_upload_assert()
        {
            var client = UploadcareClient.DemoClient();
            var file = new FileInfo("Lenna.png");

            var uploader = new SignedFileUploader(client, new TimeSpan(0, 0, 30));
            var result = await uploader.Upload(file);

            Assert.NotNull(result.Uuid);
        }

        [Fact]
        public async Task signedfileuploader_upload_bytes()
        {
            var client = UploadcareClient.DemoClient();
            var file = new FileInfo("Lenna.png");
            var bytes = File.ReadAllBytes(file.FullName);

            var uploader = new SignedFileUploader(client, new TimeSpan(0, 0, 30));
            var result = await uploader.Upload(bytes, file.Name);

            Assert.NotNull(result.Uuid);
        }
	}
}