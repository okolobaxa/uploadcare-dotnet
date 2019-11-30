using System;
using System.Collections.Generic;
using System.Linq;
using Uploadcare.DTO;
using Uploadcare.Models;
using Uploadcare.Utils;
using Uploadcare.Utils.UrlParameters;

namespace Uploadcare
{
    public class FilesQueryBuilder : IPaginatedQueryBuilder<UploadcareFile>
    {
        private readonly RequestHelper _requestHelper;
        private readonly List<IUrlParameter> _parameters = new List<IUrlParameter>(12);


        /// <summary>
        /// Initializes a new builder.
        /// </summary>
        internal FilesQueryBuilder(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        /// <summary>
        /// Adds a filter for removed files.
        /// </summary>
        /// <param name="removed">true to only include removed files in the response, false to include existing files. Defaults to false.</param>
        public FilesQueryBuilder Removed(bool removed)
        {
            _parameters.Add(new FilesRemovedParameter(removed));

            return this;
        }

        /// <summary>
        /// Adds a filter for removed files.
        /// </summary>
        public FilesQueryBuilder OnlyRemoved()
        {
            _parameters.Add(new FilesRemovedParameter(removed: true));

            return this;
        }

        /// <summary>
        /// Adds a filter for stored files.
        /// </summary>
        /// <param name="stored">true to only include files that were stored, false to include temporary ones. The default is unset: both stored and not stored files are returned.</param>
        public FilesQueryBuilder Stored(bool stored)
        {
            _parameters.Add(new FilesStoredParameter(stored));

            return this;
        }

        /// <summary>
        /// Adds a filter for stored files.
        /// </summary>
        public FilesQueryBuilder OnlyStored()
        {
            _parameters.Add(new FilesStoredParameter(stored: true));

            return this;
        }

        /// <summary>
        /// Set a preferred amount of files in a list for a single response
        /// </summary>
        /// <param name="limit">A preferred amount of files in a list for a single response. Defaults to 100, while the maximum is 1000.</param>
        public FilesQueryBuilder Limit(int limit)
        {
            if (limit > 1000)
            {
                limit = 1000;
            }

            _parameters.Add(new LimitParameter(limit));

            return this;
        }

        public FilesQueryBuilder OrderByUploadDate(DateTime? from = null)
        {
            _parameters.Add(new FileOrderingParameter(EFileOrderBy.DatetimeUploaded));
            
            if (from.HasValue)
            {
                _parameters.Add(new OrderingFromDateParameter(from.Value));
            }

            return this;
        }

        public FilesQueryBuilder OrderByUploadDateDesc(DateTime? from = null)
        {
            _parameters.Add(new FileOrderingParameter(EFileOrderBy.DatetimeUploadedDesc));
            
            if (from.HasValue)
            {
                _parameters.Add(new OrderingFromDateParameter(from.Value));
            }

            return this;
        }

        public FilesQueryBuilder OrderBySize(long? sizeFromBytes = null)
        {
            _parameters.Add(new FileOrderingParameter(EFileOrderBy.Size));

            if (sizeFromBytes.HasValue)
            {
                _parameters.Add(new OrderingFromSizeParameter(sizeFromBytes.Value));
            }

            return this;
        }

        public FilesQueryBuilder OrderBySizeDesc(long? sizeFromBytes = null)
        {
            _parameters.Add(new FileOrderingParameter(EFileOrderBy.SizeDesc));

            if (sizeFromBytes.HasValue)
            {
                _parameters.Add(new OrderingFromSizeParameter(sizeFromBytes.Value));
            }

            return this;
        }

        public IEnumerable<UploadcareFile> AsEnumerable()
        {
            var url = Urls.ApiFiles;

            var result = _requestHelper.ExecutePaginatedQuery(url, _parameters, new FilePageData(), new FileDataWrapper());

            return result;
        }

        public List<UploadcareFile> AsList()
        {
            return AsEnumerable().ToList();
        }
    }
}