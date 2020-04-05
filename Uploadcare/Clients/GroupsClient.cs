using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uploadcare.Models;
using Uploadcare.Utils;

namespace Uploadcare.Clients
{
    internal class GroupsClient : IGroupsClient
    {
        private readonly RequestHelper _requestHelper;

        public GroupsClient(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public async Task<UploadcareGroup> GetAsync(string groupId)
        {
            if (string.IsNullOrEmpty(groupId))
            {
                throw new ArgumentNullException(nameof(groupId));
            }

            var url = Urls.ApiGroup(groupId);

            var result = await _requestHelper.Get<UploadcareGroup>(url, default);

            return result;
        }

        public async Task<UploadcareGroup> StoreAsync(string groupId)
        {
            if (string.IsNullOrEmpty(groupId))
            {
                throw new ArgumentNullException(nameof(groupId));
            }

            var url = Urls.ApiGroupsStorage(groupId);

            var result = await _requestHelper.Post<UploadcareGroup>(url);

            return result;
        }

        public async Task<UploadcareGroup> CreateAsync(IReadOnlyCollection<string> fileIds)
        {
            if (fileIds == null)
            {
                throw new ArgumentNullException(nameof(fileIds));
            }

            if (!fileIds.Any())
            {
                throw new ArgumentException(nameof(fileIds));
            }

            var url = Urls.ApiCreateGroup;

            var publicKey = _requestHelper.GetConnection().PublicKey;

            var formData = new Dictionary<string, string>(fileIds.Count + 1)
            {
                {"pub_key", publicKey}
            };

            int i = 0;
            foreach (var fileId in fileIds)
            {
                formData.Add($"files[{i}]", fileId);
                i++;
            }

            var result = await _requestHelper.PostFormData<UploadcareGroup>(url, formData);

            return result;
        }

        public GroupsQueryBuilder GetGroups()
        {
            return new GroupsQueryBuilder(_requestHelper);
        }
    }
}
