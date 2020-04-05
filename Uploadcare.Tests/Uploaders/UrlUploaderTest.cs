using System.Threading.Tasks;
using Uploadcare.Upload;
using Xunit;

namespace Uploadcare.Tests.Uploaders
{
    public class UrlUploaderTest
    {
        private const string URL = "https://upload.wikimedia.org/wikipedia/ru/2/24/Lenna.png";

        [Fact]
        public async Task urluploader_upload_assert()
        {
            var client = UploadcareClient.DemoClient();

            var uploader = new UrlUploader(client);
            var result = await uploader.Upload(URL);

            Assert.NotNull(result.Uuid);
        }
    }
}