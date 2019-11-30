using System.Collections.Generic;
using System.Threading.Tasks;
using Uploadcare.Models;

namespace Uploadcare.Clients
{
    public interface IGroupsClient
    {
        /// <summary>
        /// Get a file group by UUID.
        /// </summary>
        /// <param name="groupId"> Group UUID </param>
        /// <returns>Group resource</returns>
        Task<UploadcareGroup> GetAsync(string groupId);

        /// <summary>
        /// Mark all files in a group as stored.
        /// </summary>
        /// <param name="groupId"> Group UUID </param>
        /// <returns>Group resource</returns>
        Task<UploadcareGroup> StoreAsync(string groupId);

        /// <summary>
        /// Create group from a set of files by using their UUIDs.
        /// </summary>
        /// <param name="fileIds"> That parameter defines a set of files you want to join in a group. 
        /// Each parameter can be a file UUID or a CDN URL, with or without applied Image Transformations operations. 
        /// </param>
        /// <returns>Group resource</returns>
        Task<UploadcareGroup> CreateAsync(IReadOnlyCollection<string> fileIds);

        GroupsQueryBuilder GetGroups();
    }
}