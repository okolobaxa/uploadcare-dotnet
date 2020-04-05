using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Uploadcare.Models;
using Uploadcare.Utils;

namespace Uploadcare.Clients
{
    internal class WebhookClient : IWebhooksClient
    {
        private readonly RequestHelper _requestHelper;

        public WebhookClient(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public async Task<IEnumerable<UploadcareWebhook>> GetAsync()
        {
            var url = Urls.ApiWebhooks;

            var result = await _requestHelper.Get<List<UploadcareWebhook>>(url, default);

            return result;
        }

        public async Task<UploadcareWebhook> SubscribeAsync(string eventType, string targetUrl, bool isActive = true)
        {
            if (string.IsNullOrEmpty(eventType))
            {
                throw new ArgumentNullException(nameof(eventType));
            }

            if (string.IsNullOrEmpty(targetUrl))
            {
                throw new ArgumentNullException(nameof(targetUrl));
            }

            var url = Urls.ApiWebhooks;

            var formData = new Dictionary<string, string>(3)
            {
                {"event", eventType},
                {"target_url", targetUrl},
                {"is_active", isActive.ToString()}
            };

            var result = await _requestHelper.PostFormData<UploadcareWebhook>(url, formData);

            return result;
        }

        public async Task UnsubscribeAsync(string eventType, string targetUrl)
        {
            if (string.IsNullOrEmpty(eventType))
            {
                throw new ArgumentNullException(nameof(eventType));
            }

            if (string.IsNullOrEmpty(targetUrl))
            {
                throw new ArgumentNullException(nameof(targetUrl));
            }

            var url = Urls.ApiWebhooksUnsubscribe;

            var formData = new Dictionary<string, string>(2)
            {
                {"event", eventType},
                {"target_url", targetUrl}
            };

            await _requestHelper.PostFormData(url, formData);
        }

        public async Task<UploadcareWebhook> UpdateAsync(long subscriptionId, string eventType = null, string targetUrl = null, bool? isActive = null)
        {
            if (subscriptionId <=0)
            {
                throw new ArgumentException(nameof(subscriptionId));
            }

            var url = Urls.ApiWebhook(subscriptionId);

            var formData = new Dictionary<string, string>(3);

            if (eventType != null)
                formData.Add("event", eventType);

            if (targetUrl != null)
                formData.Add("target_url", targetUrl);

            if (isActive != null)
                formData.Add("is_active", isActive.Value.ToString());

            var result = await _requestHelper.PostFormData<UploadcareWebhook>(url, formData);

            return result;
        }
    }
}
