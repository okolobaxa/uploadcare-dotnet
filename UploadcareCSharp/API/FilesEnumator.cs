using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using UploadcareCSharp.Data;
using UploadcareCSharp.Url.UrlParameters;

namespace UploadcareCSharp.API
{
    internal class FilesEnumator<TFileData, TFilePageData> : IEnumerator<TFileData>, IEnumerable<TFileData>
    {
        /// <summary>
        /// T - UploadcareFile
        /// U - FilePageData
        /// </summary>
        private readonly RequestHelper _requestHelper;
        private readonly Uri _url;
        private readonly List<IUrlParameter> _urlParameters;
        private readonly bool _includeApiHeaders;
        private readonly TFilePageData _dataClass;
        private int _page;
        private bool _more;
        private IEnumerator<TFileData> _pageIterator;

        public FilesEnumator(RequestHelper requestHelper, Uri url, List<IUrlParameter> urlParameters,
            bool includeApiHeaders, TFilePageData dataClass)
        {
            _requestHelper = requestHelper;
            _url = url;
            _urlParameters = urlParameters;
            _includeApiHeaders = includeApiHeaders;
            _dataClass = dataClass;

            GetNext();
        }

        private void GetNext()
        {
            var builder = new UriBuilder(_url);
            
            var queryParameters = new NameValueCollection();
            SetQueryParameters(queryParameters, _urlParameters);
            queryParameters.Add("page", (++_page).ToString());

            builder.Query = ToQueryString(queryParameters);

            var request = (HttpWebRequest)WebRequest.Create(builder.Uri);
            request.Method = "GET";

            var pageData = _requestHelper.ExecuteQuery(request, _includeApiHeaders, _dataClass);
            _more = ((IPageData<TFileData>)pageData).HasMore();
            _pageIterator = ((IPageData<TFileData>)pageData).GetResults().GetEnumerator();
            _pageIterator.MoveNext();
        }

        public bool MoveNext()
        {
            if (_pageIterator.MoveNext())
            {
                return true;
            }
            if (_more)
            {
                GetNext();
                return true;
            }
            return false;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public TFileData Current
        {
            get
            {
                return _pageIterator.Current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public IEnumerator<TFileData> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static void SetQueryParameters(NameValueCollection queryParameters, IEnumerable<IUrlParameter> parameters)
        {
            foreach (var parameter in parameters)
            {
                queryParameters.Add(parameter.GetParam(), parameter.GetValue());
            }
        }

        private static string ToQueryString(NameValueCollection nvc)
        {
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}", key, value))
                .ToArray();
            return string.Join("&", array);
        }
    }
}
