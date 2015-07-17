using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uploadcare.API;
using Uploadcare.Upload;

namespace Uploadcare.Test
{
    [TestClass]
	public class FileUploaderTest
    {
        private Client _client;

        [TestInitialize]
        public void SetUp()
        {
            _client = Client.DemoClient();
        }

        [TestMethod]
        public void fileuploader_upload_assert()
        {
            var file = new FileInfo("test.png");

            var uploader = new FileUploader(_client, file);
            var result = uploader.Upload();

            Assert.IsNotNull(result.FileId);
        }

	}

}