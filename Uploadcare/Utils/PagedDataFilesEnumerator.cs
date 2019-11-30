using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Uploadcare.Utils.UrlParameters;

namespace Uploadcare.Utils
{
    internal class PagedDataFilesEnumerator<T, TU, TK> : IEnumerator<T>, IEnumerable<T>
    {
        private readonly RequestHelper _requestHelper;
        private readonly Uri _url;
        private readonly IReadOnlyCollection<IUrlParameter> _urlParameters;
        private readonly TK _dataClass;
        private readonly IDataWrapper<T, TU> _dataWrapper;
        private int _page;
        private int _total;
        private bool _more;
        private IEnumerator<TU> _pageIterator;

        public PagedDataFilesEnumerator(RequestHelper requestHelper, Uri url, IReadOnlyCollection<IUrlParameter> urlParameters, TK dataClass, IDataWrapper<T, TU> dataWrapper)
        {
            _requestHelper = requestHelper;
            _url = url;
            _urlParameters = urlParameters;
            _dataClass = dataClass;
            _dataWrapper = dataWrapper;

            GetNext();
        }

        private void GetNext()
        {
            var builder = new UriBuilder(_url);
            
            var queryParameters = new NameValueCollection();
            SetQueryParameters(queryParameters, _urlParameters);
            queryParameters.Add("page", (++_page).ToString());

            builder.Query = ToQueryString(queryParameters);

            var rawPageData = _requestHelper.Get(builder.Uri, _dataClass).GetAwaiter().GetResult();
            var pageData = (IPageData<TU>)rawPageData;
            var results = pageData.GetResults();

            _total += results.Count;
            _more = _total + 1 < pageData.Total;

            _pageIterator = results.GetEnumerator();
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

        public void Reset() => throw new NotImplementedException();

        public void Dispose()
        {
        }

        public T Current => _dataWrapper.Wrap(_pageIterator.Current);

        object IEnumerator.Current => Current;

        public IEnumerator<T> GetEnumerator() => this;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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
                         select $"{key}={value}").ToArray();

            return string.Join("&", array);
        }
    }
}
