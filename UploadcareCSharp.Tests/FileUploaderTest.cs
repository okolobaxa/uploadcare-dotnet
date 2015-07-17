using System.IO;
using Uploadcare.API;
using Uploadcare.Upload;
using Xunit;

namespace Uploadcare.Test
{
	public class FileUploaderTest
    {
        [Fact]
        public void fileuploader_upload_assert()
        {
            var _client = Client.DemoClient();
            var file = new FileInfo("test.png");

            var uploader = new FileUploader(_client, file);
            var result = uploader.Upload();

            Assert.NotNull(result.FileId);
        }

	}

}