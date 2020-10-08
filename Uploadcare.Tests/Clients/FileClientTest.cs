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

            var result = await client.Files.GetAsync(uploadedFileInfo.Uuid);

            Assert.NotNull(result.Uuid);
        }

        [Fact]
        public async Task client_savefile_assert()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();
            var file = new FileInfo("Lenna.png");

            var uploader = new FileUploader(client);
            var uploadedFileInfo = await uploader.Upload(file);

            var result = await client.Files.StoreAsync(uploadedFileInfo.Uuid);

            Assert.NotNull(result.Uuid);
            Assert.True(result.Stored);
        }

        [Fact]
        public async Task client_deletefile_assert()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();
            var file = new FileInfo("Lenna.png");

            var uploader = new FileUploader(client);
            var uploadedFileInfo = await uploader.Upload(file);

            await client.Files.DeleteAsync(uploadedFileInfo.Uuid);
            var result = await client.Files.GetAsync(uploadedFileInfo.Uuid);

            Assert.NotNull(result.Uuid);
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
                    uploadedFileInfo = await client.Files.GetAsync(uploadedFileInfo.Uuid);
                }
            }

            var newFileId = client.Files.CopyAsync(uploadedFileInfo.Uuid).GetAwaiter().GetResult();

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

            var fileIds = new[] { uploadedFileInfo1.Uuid, uploadedFileInfo2.Uuid, badFileId };

            var (files, problems) = await client.Files.StoreAsync(fileIds.ToList());

            Assert.Contains(files, x => x.Uuid == uploadedFileInfo1.Uuid);
            Assert.Contains(files, x => x.Uuid == uploadedFileInfo2.Uuid);
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

            var fileIds = new[] { uploadedFileInfo1.Uuid, uploadedFileInfo2.Uuid, badFileId };

            var (files, problems) = await client.Files.DeleteAsync(fileIds.ToList());

            Assert.Contains(files, x => x.Uuid == uploadedFileInfo1.Uuid);
            Assert.Contains(files, x => x.Uuid == uploadedFileInfo2.Uuid);
            Assert.Contains(problems, x => x.Key == badFileId);
        }

        [Fact]
        public async Task client_detect_faces()
        {
            var client = new UploadcareClient("e8e49b932d98e53748a3", "899a21a3aee6a7800859"); 
            var file = new FileInfo("2.jpg");

            var uploader = new FileUploader(client);
            var uploadedFileInfo = await uploader.Upload(file, false);

            var faces = await client.FaceDetection.DetectFaces(uploadedFileInfo.Uuid);

            Assert.NotEmpty(faces);
        }

        [Fact]
        public async Task client_detect_no_faces()
        {
            var client = new UploadcareClient("e8e49b932d98e53748a3", "899a21a3aee6a7800859"); 
            var file = new FileInfo("0.jpg");

            var uploader = new FileUploader(client);
            var uploadedFileInfo = await uploader.Upload(file, false);

            var faces = await client.FaceDetection.DetectFaces(uploadedFileInfo.Uuid);

            Assert.Empty(faces);
        }
    }
}