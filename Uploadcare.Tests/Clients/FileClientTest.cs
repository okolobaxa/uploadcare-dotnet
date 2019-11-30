using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Uploadcare.Upload;
using Xunit;

namespace Uploadcare.Tests.Clients
{
    public class FileClientTest
    {
        [Fact]
        public async Task client_getfile_assert()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();
            var file = new FileInfo("Lenna.png");

            var uploader = new FileUploader(client);
            var uploadedFileInfo = await uploader.Upload(file);

            var result = await client.Files.GetAsync(uploadedFileInfo.FileId);

            Assert.NotNull(result.FileId);
        }

        [Fact]
        public async Task client_savefile_assert()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();
            var file = new FileInfo("Lenna.png");

            var uploader = new FileUploader(client);
            var uploadedFileInfo = await uploader.Upload(file);

            var result = await client.Files.StoreAsync(uploadedFileInfo.FileId);

            Assert.NotNull(result.FileId);
            Assert.True(result.Stored);
        }

        [Fact]
        public async Task client_deletefile_assert()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();
            var file = new FileInfo("Lenna.png");

            var uploader = new FileUploader(client);
            var uploadedFileInfo = await uploader.Upload(file);

            await client.Files.DeleteAsync(uploadedFileInfo.FileId);
            var result = await client.Files.GetAsync(uploadedFileInfo.FileId);

            Assert.NotNull(result.FileId);
            Assert.True(result.Removed);
        }

        [Fact]
        public void client_getfiles_iterable()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();
            var count = 0;

            foreach (var file in client.Files.GetFiles().AsEnumerable())
            {
                Assert.NotNull(file);
                count++;
                if (count == 50)
                    break;
            }

            Assert.True(count == 50);
        }

        [Fact]
        public void client_getfiles_iterable_stored()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();
            var count = 0;

            foreach (var file in client.Files.GetFiles().Stored(true).AsEnumerable())
            {
                Assert.NotNull(file);
                count++;
                if (count == 150)
                    break;
            }

            Assert.True(count == 150);
        }

        [Fact]
        public void client_getfiles_iterable_orderby_size()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();
            var count = 0;

            foreach (var file in client.Files.GetFiles().OrderBySize(100000).AsEnumerable())
            {
                Assert.NotNull(file);
                count++;
                if (count == 150)
                    break;
            }

            Assert.True(count == 150);
        }

        [Fact]
        public void client_getfiles_iterable_orderby_date()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();
            var count = 0;

            foreach (var file in client.Files.GetFiles().OrderByUploadDate(DateTime.UtcNow.AddDays(-1)).AsEnumerable())
            {
                Assert.NotNull(file);
                count++;
                if (count == 150)
                    break;
            }

            Assert.True(count == 150);
        }

        [Fact]
        public async Task client_copyfile_assert()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();
            var file = new FileInfo("Lenna.png");

            var uploader = new FileUploader(client);

            var uploadedFileInfo = uploader.Upload(file).GetAwaiter().GetResult();

            //We have to wait end of file processing in Uploadcare backend for copy it
            if (!uploadedFileInfo.IsReady)
            {
                await Task.Delay(1000);

                while (!uploadedFileInfo.IsReady)
                {
                    uploadedFileInfo = await client.Files.GetAsync(uploadedFileInfo.FileId);
                }
            }

            var newFileId = client.Files.CopyAsync(uploadedFileInfo.FileId).GetAwaiter().GetResult();

            Assert.NotNull(newFileId);
        }

        [Fact]
        public async Task client_massstore_assert()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();
            var file = new FileInfo("Lenna.png");

            var uploader = new FileUploader(client);
            var uploadedFileInfo1 = await uploader.Upload(file, false);
            var uploadedFileInfo2 = await uploader.Upload(file, false);
            var badFileId = "4j334o01-8bs3";

            var fileIds = new[] { uploadedFileInfo1.FileId, uploadedFileInfo2.FileId, badFileId };

            var (files, problems) = await client.Files.StoreAsync(fileIds.ToList());

            Assert.Contains(files, x => x.FileId == uploadedFileInfo1.FileId);
            Assert.Contains(files, x => x.FileId == uploadedFileInfo2.FileId);
            Assert.Contains(problems, x => x.Key == badFileId);
        }

        [Fact]
        public async Task client_massdelete_assert()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();
            var file = new FileInfo("Lenna.png");

            var uploader = new FileUploader(client);
            var uploadedFileInfo1 = await uploader.Upload(file, false);
            var uploadedFileInfo2 = await uploader.Upload(file, false);
            var badFileId = "4j334o01-8bs3";

            var fileIds = new[] { uploadedFileInfo1.FileId, uploadedFileInfo2.FileId, badFileId };

            var (files, problems) = await client.Files.DeleteAsync(fileIds.ToList());

            Assert.Contains(files, x => x.FileId == uploadedFileInfo1.FileId);
            Assert.Contains(files, x => x.FileId == uploadedFileInfo2.FileId);
            Assert.Contains(problems, x => x.Key == badFileId);
        }
    }
}