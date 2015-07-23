using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UploadcareCSharp.Data;
using UploadcareCSharp.Url.UrlParameters;

namespace UploadcareCSharp.API
{
    internal class FilesEnumator<T, U> : IEnumerator<T>, IEnumerable<T>
    {
        private readonly RequestHelper _requestHelper;
        private readonly Uri _url;
        private readonly bool _includeApiHeaders;
        private readonly IPageData<U> _dataClass;
        private int page = 0;
        private bool more;
        private IEnumerator<U> _pageIterator;

        public FilesEnumator(RequestHelper requestHelper, Uri url, List<IUrlParameter> urlParameters,
            bool includeApiHeaders, IPageData<U> dataClass, IDataWrapper<T, U> dataWrapper)
        {
            _requestHelper = requestHelper;
            _url = url;
            _includeApiHeaders = includeApiHeaders;
            _dataClass = dataClass;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            var request = (HttpWebRequest) WebRequest.Create(_url);
            request.Method = "GET";

            IPageData<U> pageData = _requestHelper.ExecuteQuery(request, _includeApiHeaders, _dataClass);
            more = pageData.HasMore();
            _pageIterator = pageData.GetResults().GetEnumerator();

            return _pageIterator.MoveNext();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public T Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
