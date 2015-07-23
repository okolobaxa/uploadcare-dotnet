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

        private Client client;
        private List<IUrlParameter> parameters = new List<IUrlParameter>();

        /**
     * Initializes a new builder for the given client.
     */

        public FilesQueryBuilder(Client client)
        {
            this.client = client;
        }

        /**
     * Adds a filter for removed files.
     *
     * @param removed If {@code true}, accepts removed files, otherwise declines them.
     */

        public FilesQueryBuilder Removed(bool removed)
        {
            parameters.Add(new FilesRemovedParameter(removed));
            return this;
        }

        /**
     * Adds a filter for stored files.
     *
     * @param stored If {@code true}, accepts stored files, otherwise declines them.
     */

        public FilesQueryBuilder Stored(bool stored)
        {
            parameters.Add(new FilesStoredParameter(stored));
            return this;
        }

        public IEnumerable<UploadcareFile> AsIterable()
        {
            var url = Urls.ApiFiles();

            RequestHelper requestHelper = client.GetRequestHelper();

            return requestHelper.ExecutePaginatedQuery(url, parameters, true, new FilePageData());
        }

        public List<UploadcareFile> AsList()
        {

            return AsIterable().ToList();
        }
    }
}