using System;
using System.Collections.Generic;
using System.Linq;
using Uploadcare.DTO;
using Uploadcare.Models;
using Uploadcare.Utils;
using Uploadcare.Utils.UrlParameters;

namespace Uploadcare
{
    public class GroupsQueryBuilder : IPaginatedQueryBuilder<UploadcareGroup>
    {
        private readonly RequestHelper _requestHelper;
        private readonly List<IUrlParameter> _parameters = new List<IUrlParameter>(5);


        /// <summary>
        /// Initializes a new builder.
        /// </summary>
        internal GroupsQueryBuilder(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        /// <summary>
        /// Set a preferred amount of files in a list for a single response
        /// </summary>
        /// <param name="limit">A preferred amount of files in a list for a single response. Defaults to 100, while the maximum is 1000.</param>
        public GroupsQueryBuilder Limit(int limit)
        {
            if (limit > 1000)
            {
                limit = 1000;
            }

            _parameters.Add(new LimitParameter(limit));

            return this;
        }

        public GroupsQueryBuilder OrderByCreateDate(DateTime? from = null)
        {
            _parameters.Add(new GroupOrderingParameter(EGroupOrderBy.DatetimeCreated));

            if (from.HasValue)
            {
                _parameters.Add(new OrderingFromDateParameter(from.Value));
            }

            return this;
        }

        public GroupsQueryBuilder OrderByCreateDateDesc(DateTime? from = null)
        {
            _parameters.Add(new GroupOrderingParameter(EGroupOrderBy.DatetimeCreatedDesc));

            if (from.HasValue)
            {
                _parameters.Add(new OrderingFromDateParameter(from.Value));
            }

            return this;
        }

        public IEnumerable<UploadcareGroup> AsEnumerable()
        {
            var url = Urls.ApiGroups;

            var result = _requestHelper.ExecutePaginatedQuery<UploadcareGroup, GroupPageData>(url, _parameters, new GroupPageData());

            return result;
        }

        public List<UploadcareGroup> AsList()
        {
            return AsEnumerable().ToList();
        }
    }
}