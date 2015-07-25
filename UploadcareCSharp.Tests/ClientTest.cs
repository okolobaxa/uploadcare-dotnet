using System.IO;
using UploadcareCSharp.API;
using UploadcareCSharp.Upload;
using Xunit;

namespace UploadcareCSharp.Tests
{
	public class ClientTest
    {
        [Fact]
		public void client_getfile_assert()
        {
            var client = Client.DemoClient();
            var file = new FileInfo("test.png");

            var uploader = new FileUploader(client, file);
            var uploadedFileInfo = uploader.Upload();

            var result = client.GetFile(uploadedFileInfo.FileId);

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
            var uploadedFileInfo = uploader.Upload();

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
            var uploadedFileInfo = uploader.Upload();

            client.DeleteFile(uploadedFileInfo.FileId);
            var result = client.GetFile(uploadedFileInfo.FileId);

            Assert.NotNull(result.FileId);
            Assert.True(result.Removed);
        }

        [Fact]
	    public void client_getfiles_iterable()
	    {
	        var client = Client.DemoClient();
            var count = 0;
            
            foreach (var file in client.GetFiles().Stored(true).AsIterable())
            {
                Assert.NotNull(file);
                count++;
                if (count == 150)
                    break;
            }

            Assert.True(count == 150);
	    }
	}

}