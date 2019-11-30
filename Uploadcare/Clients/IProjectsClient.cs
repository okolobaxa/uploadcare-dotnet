using System.Threading.Tasks;
using Uploadcare.Models;

namespace Uploadcare.Clients
{
    public interface IProjectsClient
    {
        /// <summary>
        /// This provides basic information about the project you connect to.
        /// </summary>
        /// <returns>Project resource</returns>
        Task<UploadcareProject> GetAsync();
    }
}