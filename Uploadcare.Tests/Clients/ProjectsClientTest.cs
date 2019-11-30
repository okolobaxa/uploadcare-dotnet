using System.Threading.Tasks;
using Xunit;

namespace Uploadcare.Tests.Clients
{
	public class ProjectsClientTest
    {
        [Fact]
        public async Task client_project_assert()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();
            var result = await client.Projects.GetAsync();

            Assert.NotNull(result);
            Assert.NotNull(result.Name);
            Assert.NotEmpty(result.Name);
            Assert.NotNull(result.Owner);
            Assert.NotEmpty(result.Collaborators);
        }
	}

}