using System.IO;
using System.Threading.Tasks;
using Uploadcare.Upload;
using Xunit;

namespace Uploadcare.Tests.Uploaders
{
	public class FileUploaderTest
    {
        [Fact]
        public async Task fileuploader_upload_assert()
        {
            var client = UploadcareClient.DemoClient();
            var file = new FileInfo("Lenna.png");

            var uploader = new FileUploader(client);
            var result = await uploader.Upload(file);

            Assert.NotNull(result.FileId);
        }

        [Fact]
        public async Task fileuploader_upload_bytes()
        {
            var client = UploadcareClient.DemoClient();
            var file = new FileInfo("Lenna.png");
            var bytes = File.ReadAllBytes(file.FullName);

            var uploader = new FileUploader(client);
            var result = await uploader.Upload(bytes, file.Name);

            Assert.NotNull(result.FileId);
        }
	}

}