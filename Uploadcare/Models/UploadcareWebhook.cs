using System;
using Uploadcare.DTO;

namespace Uploadcare.Models
{
    public class UploadcareWebhook
    {
        private readonly WebhookData _webhookData;

        internal UploadcareWebhook(WebhookData webhookData)
        {
            _webhookData = webhookData;
        }

        public int Id => _webhookData.Id;

        public DateTime Created => _webhookData.Created;

        public DateTime? Updated => _webhookData.Updated;

        public string EventType => _webhookData.EventType;

        public string TargetUrl => _webhookData.TargetUrl;

        public int Project => _webhookData.Project;

        public bool IsActive => _webhookData.IsActive;
    }
}