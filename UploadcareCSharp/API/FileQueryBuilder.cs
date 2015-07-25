using System;
using System.Collections.Generic;
using System.Linq;
using UploadcareCSharp.Data;
using UploadcareCSharp.Url;
using UploadcareCSharp.Url.UrlParameters;

namespace UploadcareCSharp.API
{
    public class FilesQueryBuilder : IPaginatedQueryBuilder<UploadcareFile>
    {
        private readonly Client _client;
        private readonly List<IUrlParameter> _parameters = new List<IUrlParameter>();


        /// <summary>
        /// Initializes a new builder for the given client.
        /// </summary>
        public FilesQueryBuilder(Client client)
        {
            _client = client;
        }

        /// <summary>
        /// Adds a filter for removed files.
        /// </summary>
        /// <param name="removed"> If true, accepts removed files, otherwise declines them </param>
        public FilesQueryBuilder Removed(bool removed)
        {
            _parameters.Add(new FilesRemovedParameter(removed));
            return this;
        }

        /// <summary>
        /// Adds a filter for stored files.
        /// </summary>
        /// <param name="stored"> If true, accepts stored files, otherwise declines them </param>
        public FilesQueryBuilder Stored(bool stored)
        {
            _parameters.Add(new FilesStoredParameter(stored));
            return this;
        }

        public IEnumerable<UploadcareFile> AsIterable()
        {
            var url = Urls.ApiFiles();
            var requestHelper = _client.GetRequestHelper();

            var result = requestHelper.ExecutePaginatedQuery(url, _parameters, true, new FilePageData(), new FileDataWrapper(_client));

            return result;
        }

        public List<UploadcareFile> AsList()
        {
            return AsIterable().ToList();
        }
    }
}