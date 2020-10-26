using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Uploadcare.Exceptions;
using Uploadcare.Utils.UrlParameters;

namespace Uploadcare.Utils
{
    internal class RequestHelper : IDisposable
    {
        private readonly IUploadcareConnection _connection;
        internal readonly HttpClient HttpClient;

        internal RequestHelper(IUploadcareConnection connection)
        {
            _connection = connection;
            HttpClient = new HttpClient();

            HttpClient.DefaultRequestHeaders.Add("Accept", new[] { "application/vnd.uploadcare-v0.5+json" });

            if (connection.AuthType == UploadcareAuthType.Simple)
            {
                HttpClient.DefaultRequestHeaders.Add("Authorization", AuthHeaderHelper.GetSimple(_connection.PublicKey, _connection.PrivateKey));
            }
        }

        public IUploadcareConnection GetConnection() => _connection;

        public IEnumerable<T> ExecutePaginatedQuery<T, TK>(Uri url, IReadOnlyCollection<IUrlParameter> urlParameters, TK pageData)
        {
            return new PagedDataFilesEnumerator<T, TK>(this, url, urlParameters, pageData);
        }

        public async Task<TOut> PostContent<TOut>(Uri uri, HttpContent requestContent)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = uri,
                Headers =
                    {
                        { HttpRequestHeader.Date.ToString(), DateTime.Now.ToString("R") }
                    },
                Content = requestContent
            };

            var responseMessage = await HttpClient.SendAsync(httpRequestMessage);

            await CheckResponseStatus(responseMessage);

            return await GetResponse<TOut>(responseMessage);
        }

        public async Task<TOut> Get<TOut>(Uri uri, TOut dataClass)
        {
            var responseMessage = await Send(uri, HttpMethod.Get, new StringContent(string.Empty, Encoding.UTF8, "application/json"));

            await CheckResponseStatus(responseMessage);

            return await GetResponse<TOut>(responseMessage);
        }

        public async Task<TOut> Post<TOut>(Uri uri)
        {
            var responseMessage = await Send(uri, HttpMethod.Post, new StringContent(string.Empty, Encoding.UTF8, "application/json"));

            await CheckResponseStatus(responseMessage);

            return await GetResponse<TOut>(responseMessage);
        }

        public async Task<TOut> PostFormData<TOut>(Uri uri, Dictionary<string, string> formData)
        {
            using (var content = new FormUrlEncodedContent(formData))
            {
                var responseMessage = await Send(uri, HttpMethod.Post, content);

                await CheckResponseStatus(responseMessage);

                return await GetResponse<TOut>(responseMessage);
            }
        }

        public async Task PostFormData(Uri uri, Dictionary<string, string> formData)
        {
            using (var content = new FormUrlEncodedContent(formData))
            {
                var responseMessage = await Send(uri, HttpMethod.Post, content);

                await CheckResponseStatus(responseMessage);
            }
        }

        public async Task<TOut> Put<TIn, TOut>(Uri uri, TIn data)
        {
            var stringData = JsonSerializer.Serialize(data);
            var content = new StringContent(stringData, Encoding.UTF8, "application/json");

            var responseMessage = await Send(uri, HttpMethod.Put, content);

            await CheckResponseStatus(responseMessage);

            return await GetResponse<TOut>(responseMessage);
        }

        public async Task<TOut> Delete<TIn, TOut>(Uri uri, TIn data)
        {
            var stringData = JsonSerializer.Serialize(data);
            var content = new StringContent(stringData, Encoding.UTF8, "application/json");

            var responseMessage = await Send(uri, HttpMethod.Delete, content);

            await CheckResponseStatus(responseMessage);

            return await GetResponse<TOut>(responseMessage);
        }

        private async Task<HttpResponseMessage> Send(Uri uri, HttpMethod method, HttpContent content)
        {
            try
            {
                var dateHeader = DateTime.UtcNow.ToString("R");

                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = method,
                    RequestUri = uri,
                    Headers =
                    {
                        {HttpRequestHeader.Date.ToString(), dateHeader}
                    },
                    Content = content
                };

                if (_connection.AuthType == UploadcareAuthType.Signed)
                {
                    var contentTypeHeader = content.Headers.ContentType.ToString();
                    var contentBytes = await content.ReadAsByteArrayAsync();
                    var stringContentHash = CryptoHelper.BytesToMD5(contentBytes);

                    var dataForSign = AuthHeaderHelper.CombineDataForSignature(method.Method, stringContentHash, contentTypeHeader, dateHeader, uri.PathAndQuery);
                    var signature = CryptoHelper.Sign(dataForSign, _connection.PrivateKey);

                    httpRequestMessage.Headers.Add("Authorization", AuthHeaderHelper.GetSigned(_connection.PublicKey, signature));
                }

                var responseMessage = await HttpClient.SendAsync(httpRequestMessage);

                await CheckResponseStatus(responseMessage);

                return responseMessage;
            }
            catch (HttpRequestException e)
            {
                throw new UploadcareNetworkException(e);
            }
        }

        private static async Task CheckResponseStatus(HttpResponseMessage response)
        {
            var statusCode = response.StatusCode;

            if (statusCode >= HttpStatusCode.OK && statusCode < HttpStatusCode.Ambiguous)
            {
                return;
            }

            var body = await response.Content.ReadAsStringAsync();

            JsonDocument jObject;

            try
            {
                jObject = JsonDocument.Parse(body);
            }
            catch (JsonException)
            {
                throw new UploadcareInvalidResponseException(body);
            }

            var errorToken = jObject.RootElement.GetProperty("detail");
            if (errorToken.ValueKind == JsonValueKind.Null)
            {
                throw new UploadcareInvalidResponseException(body);
            }

            switch (statusCode)
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    throw new UploadcareAuthenticationException(errorToken.GetString());
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.NotFound:
                    throw new UploadcareInvalidRequestException(errorToken.GetString());
            }

            throw new UploadcareApiException("Unknown exception during an API call, response:" + body);
        }

        private static async Task<TOut> GetResponse<TOut>(HttpResponseMessage responseMessage)
        {
            using (var responseContent = responseMessage.Content)
            {
                if (responseContent == null) throw new UploadcareInvalidResponseException("Received empty response");

                var responseBody = await responseContent.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<TOut>(responseBody);
            }
        }

        public void Dispose()
        {
            HttpClient?.Dispose();
        }
    }
}