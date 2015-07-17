using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uploadcare.API;
using Uploadcare.Upload;

namespace Uploadcare.Test
{
    [TestClass]
	public class ClientTest
    {
        private Client _client;
        private const string FileId = "b0534d24-1114-425b-87cb-11595b6b9119";

        [TestInitialize]
        public void SetUp()
        {
            _client = Client.DemoClient();
        }

        [TestMethod]
		public void client_getfile_assert()
        {
            var result = _client.GetFile(new Guid(FileId));

            Assert.IsNotNull(result.FileId);
        }

        [TestMethod]
        public void client_project_assert()
        {
            var result = _client.Project;

            Assert.IsNotNull(result);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Name));
        }

        [TestMethod]
        public void client_savefile_assert()
        {
            var file = new FileInfo("test.png");

            var uploader = new FileUploader(_client, file);
            var uploadedFileInfo = uploader.Upload();

            _client.SaveFile(uploadedFileInfo.FileId);
            var result = _client.GetFile(uploadedFileInfo.FileId);

            Assert.IsNotNull(result.FileId);
            Assert.IsTrue(result.Stored);
        }

        [TestMethod]
        public void client_deletefile_assert()
        {
            var file = new FileInfo("test.png");

            var uploader = new FileUploader(_client, file);
            var uploadedFileInfo = uploader.Upload();

            _client.DeleteFile(uploadedFileInfo.FileId);
            var result = _client.GetFile(uploadedFileInfo.FileId);

            Assert.IsNotNull(result.FileId);
            Assert.IsTrue(result.Removed);
        }
	}

}