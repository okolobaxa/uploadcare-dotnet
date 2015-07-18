using System;
using System.IO;
using Uploadcare.API;
using Uploadcare.Upload;
using UploadcareCSharp.Enums;
using Xunit;

namespace Uploadcare.Test
{
	public class ClientTest
    {
        private const string FileId = "b0534d24-1114-425b-87cb-11595b6b9119";

        [Fact]
		public void client_getfile_assert()
        {
            var _client = Client.DemoClient();
            var result = _client.GetFile(new Guid(FileId));

            Assert.NotNull(result.FileId);
        }

        [Fact]
        public void client_project_assert()
        {
            var _client = Client.DemoClient();
            var result = _client.Project;

            Assert.NotNull(result);
            Assert.True(!string.IsNullOrEmpty(result.Name));
        }

        [Fact]
        public void client_savefile_assert()
        {
            var _client = Client.DemoClient();
            var file = new FileInfo("test.png");

            var uploader = new FileUploader(_client, file);
            var uploadedFileInfo = uploader.Upload(EStoreType.Auto);

            _client.SaveFile(uploadedFileInfo.FileId);
            var result = _client.GetFile(uploadedFileInfo.FileId);

            Assert.NotNull(result.FileId);
            Assert.True(result.Stored);
        }

        [Fact]
        public void client_deletefile_assert()
        {
            var _client = Client.DemoClient();
            var file = new FileInfo("test.png");

            var uploader = new FileUploader(_client, file);
            var uploadedFileInfo = uploader.Upload(EStoreType.Auto);

            _client.DeleteFile(uploadedFileInfo.FileId);
            var result = _client.GetFile(uploadedFileInfo.FileId);

            Assert.NotNull(result.FileId);
            Assert.True(result.Removed);
        }
	}

}