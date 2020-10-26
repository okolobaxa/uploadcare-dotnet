using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Uploadcare.DTO;
using Uploadcare.Models;
using Uploadcare.Utils;

namespace Uploadcare.Clients
{
    internal class FilesClient : IFilesClient
    {
        private readonly RequestHelper _requestHelper;

        public FilesClient(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public async Task<UploadcareFile> GetAsync(string fileId)
        {
            if (string.IsNullOrEmpty(fileId))
            {
                throw new ArgumentNullException(nameof(fileId));
            }

            var url = Urls.ApiFile(fileId);

            var result = await _requestHelper.Get<UploadcareFile>(url, default);

            return result;
        }

        public async Task<Stream> GetStreamAsync(string fileId)
        {
            var result = await GetAsync(fileId);

            return await _requestHelper.HttpClient.GetStreamAsync(result.OriginalFileUrl);
        }

        public async Task DeleteAsync(string fileId)
        {
            if (string.IsNullOrEmpty(fileId))
            {
                throw new ArgumentNullException(nameof(fileId));
            }

            var url = Urls.ApiFile(fileId);

            await _requestHelper.Delete<string, UploadcareFile>(url, string.Empty);
        }

        public async Task<UploadcareFile> StoreAsync(string fileId)
        {
            if (string.IsNullOrEmpty(fileId))
            {
                throw new ArgumentNullException(nameof(fileId));
            }

            var url = Urls.ApiFileStorage(fileId);

            var result = await _requestHelper.Post<UploadcareFile>(url);

            return result;
        }

        public async Task<string> CopyAsync(string source, bool store = false, bool makePublic = false)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentNullException(nameof(source));
            }

            var url = Urls.ApiFiles;

            var formData = new Dictionary<string, string>
            {
                {"source", source},
                {"store", store.ToString()},
                {"make_public", makePublic.ToString()}
            };

            var result = await _requestHelper.PostFormData<CopyFileData>(url, formData);

            return result.File.Uuid;
        }

        public async Task<string> CopyAsync(string source, string target,
            bool store = false, bool makePublic = false, string pattern = null)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentNullException(nameof(target));
            }

            var url = Urls.ApiFiles;

            var formData = new Dictionary<string, string>
            {
                {"source", source},
                {"target", target},
                {"pattern", pattern},
                {"store", store.ToString()},
                {"make_public", makePublic.ToString()}
            };

            var result = await _requestHelper.PostFormData<CopyUrlData>(url, formData);

            return result.Url;
        }

        public async Task<Tuple<IEnumerable<UploadcareFile>, IReadOnlyDictionary<string, string>>> StoreAsync(IReadOnlyCollection<string> fileIds)
        {
            if (fileIds == null)
            {
                throw new ArgumentNullException(nameof(fileIds));
            }

            if (!fileIds.Any())
            {
                return new Tuple<IEnumerable<UploadcareFile>, IReadOnlyDictionary<string, string>>(Array.Empty<UploadcareFile>(), new Dictionary<string, string>());
            }

            var url = Urls.ApiFilesStorage;

            var result = await _requestHelper.Put<IReadOnlyCollection<string>, MassOperationsData>(url, fileIds);

            return new Tuple<IEnumerable<UploadcareFile>, IReadOnlyDictionary<string, string>>(result.Files, result.Problems);
        }

        public async Task<Tuple<IEnumerable<UploadcareFile>, IReadOnlyDictionary<string, string>>> DeleteAsync(IReadOnlyCollection<string> fileIds)
        {
            if (fileIds == null)
            {
                throw new ArgumentNullException(nameof(fileIds));
            }

            if (!fileIds.Any())
            {
                return new Tuple<IEnumerable<UploadcareFile>, IReadOnlyDictionary<string, string>>(Array.Empty<UploadcareFile>(), new Dictionary<string, string>());
            }

            var url = Urls.ApiFilesStorage;

            var result = await _requestHelper.Delete<IReadOnlyCollection<string>, MassOperationsData>(url, fileIds);

            return new Tuple<IEnumerable<UploadcareFile>, IReadOnlyDictionary<string, string>>(result.Files, result.Problems);
        }

        public FilesQueryBuilder GetFiles()
        {
            return new FilesQueryBuilder(_requestHelper);
        }
    }
}
