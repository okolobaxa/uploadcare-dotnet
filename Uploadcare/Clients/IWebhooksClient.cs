using System.Collections.Generic;
using System.Threading.Tasks;
using Uploadcare.Models;

namespace Uploadcare.Clients
{
    public interface IWebhooksClient
    {
        /// <summary>
        /// Get a list of active subscriptions.
        /// </summary>
        /// <returns>Webhook resources</returns>
        Task<IEnumerable<UploadcareWebhook>> GetAsync();

        /// <summary>
        /// Create a new subscription.
        /// </summary>
        /// <param name="targetUrl"> A URL that is triggered by an event, for example, a file upload </param>
        /// <param name="eventType"> An event you subscribe to. </param>
        /// <param name="isActive"> Marks a subscription as either active or not, defaults to true. </param>
        /// <returns>Webhook resource</returns>
        Task<UploadcareWebhook> SubscribeAsync(string eventType, string targetUrl, bool isActive = true);

        /// <summary>
        /// Update subscription parameters.
        /// </summary>
        /// <param name="subscriptionId"> Subscription Id </param>
        /// <param name="targetUrl"> A URL that is triggered by an event, for example, a file upload </param>
        /// <param name="eventType"> An event you subscribe to. </param>
        /// <param name="isActive"> Marks a subscription as either active or not, defaults to true. </param>
        Task<UploadcareWebhook> UpdateAsync(long subscriptionId, string eventType = null, string targetUrl = null, bool? isActive = null);

        /// <summary>
        /// Delete a subscription.
        /// </summary>
        /// <param name="targetUrl"> A URL that is triggered by an event, for example, a file upload </param>
        /// <param name="eventType"> An event you unsubscribe from. </param>
        Task UnsubscribeAsync(string eventType, string targetUrl);
    }
}