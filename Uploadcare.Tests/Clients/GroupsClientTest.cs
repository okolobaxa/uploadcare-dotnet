using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Uploadcare.Upload;
using Xunit;

namespace Uploadcare.Tests.Clients
{
    public class GroupsClientTest
    {
        [Fact]
        public async Task client_getgroup_assert()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();

            var group = client.Groups.GetGroups().AsEnumerable().FirstOrDefault();

            var result = await client.Groups.GetAsync(group.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task client_storegroup_assert()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();

            var group = client.Groups.GetGroups().AsEnumerable().FirstOrDefault();
            
            var result = await client.Groups.StoreAsync(group.Id);

            Assert.NotNull(result);
            Assert.NotNull(result.StoreDate);
        }


        [Fact]
        public void client_getgroups_iterable()
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
        public void client_getgroups_iterable_orderby_date()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();
            var count = 0;

            foreach (var file in client.Groups.GetGroups().OrderByCreateDate(DateTime.UtcNow.AddDays(-1)).AsEnumerable())
            {
                Assert.NotNull(file);
                count++;
                if (count == 150)
                    break;
            }

            Assert.True(count == 150);
        }

        [Fact]
        public async Task client_creategroup_assert()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();

            var file = new FileInfo("Lenna.png");

            var uploader = new FileUploader(client);
            var result1 = await uploader.Upload(file);
            var result2 = await uploader.Upload(file);

            var group = await client.Groups.CreateAsync(new[] { result1.FileId, result2.FileId });
                       
            Assert.True(group.FilesCount == 2);

            Assert.Contains(group.Files, x => x.FileId == result1.FileId);
            Assert.Contains(group.Files, x => x.FileId == result2.FileId);
        }
    }
}