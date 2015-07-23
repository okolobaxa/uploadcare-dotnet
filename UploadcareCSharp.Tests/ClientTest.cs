using System;
using System.IO;
using System.Linq;
using UploadcareCSharp.API;
using UploadcareCSharp.Enums;
using UploadcareCSharp.Upload;
using Xunit;

namespace UploadcareCSharp.Tests
{
	public class ClientTest
    {
        private const string FileId = "b0534d24-1114-425b-87cb-11595b6b9119";

        [Fact]
		public void client_getfile_assert()
        {
            var client = Client.DemoClient();
            var result = client.GetFile(new Guid(FileId));

            Assert.NotNull(result.FileId);
        }

        [Fact]
        public void client_project_assert()
        {
            var client = Client.DemoClient();
            var result = client.GetProject();

            Assert.NotNull(result);
            Assert.True(!string.IsNullOrEmpty(result.Name));
        }

        [Fact]
        public void client_savefile_assert()
        {
            var client = Client.DemoClient();
            var file = new FileInfo("test.png");

            var uploader = new FileUploader(client, file);
            var uploadedFileInfo = uploader.Upload(EStoreType.Auto);

            client.SaveFile(uploadedFileInfo.FileId);
            var result = client.GetFile(uploadedFileInfo.FileId);

            Assert.NotNull(result.FileId);
            Assert.True(result.Stored);
        }

        [Fact]
        public void client_deletefile_assert()
        {
            var client = Client.DemoClient();
            var file = new FileInfo("test.png");

            var uploader = new FileUploader(client, file);
            var uploadedFileInfo = uploader.Upload(EStoreType.Auto);

            client.DeleteFile(uploadedFileInfo.FileId);
            var result = client.GetFile(uploadedFileInfo.FileId);

            Assert.NotNull(result.FileId);
            Assert.True(result.Removed);
        }

        [Fact]
	    public void client_getfiles_assert()
	    {
	        var client = Client.DemoClient();
	        
            var count = client.GetFiles().AsIterable().Count();

            Assert.True(count > 0);
	    }
	}

}