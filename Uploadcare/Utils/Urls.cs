using System;

namespace Uploadcare.Utils
{
    internal static class Urls
    {
        public const string API_BASE = "https://api.uploadcare.com";
        public const string CDN_BASE = "https://ucarecdn.com";
        public const string UPLOAD_BASE = "https://upload.uploadcare.com";

        public static Uri ApiProject => new Uri($"{API_BASE}/project/");

        public static Uri ApiFile(string fileId) => new Uri($"{API_BASE}/files/{fileId}/");

        public static Uri ApiFiles => new Uri($"{API_BASE}/files/");

        public static Uri ApiFilesStorage => new Uri($"{API_BASE}/files/storage/");

        public static Uri ApiGroup(string groupId) => new Uri($"{API_BASE}/groups/{groupId}/");

        public static Uri ApiGroups => new Uri($"{API_BASE}/groups/");

        public static Uri ApiCreateGroup => new Uri($"{UPLOAD_BASE}/group/");

        public static Uri ApiFileStorage(string fileId) => new Uri($"{API_BASE}/files/{fileId}/storage/");

        public static Uri ApiGroupsStorage(string groupId) => new Uri($"{API_BASE}/groups/{groupId}/storage/");

        public static Uri ApiWebhook(long subscriptionId) => new Uri($"{API_BASE}/webhooks/{subscriptionId}/");

        public static Uri ApiWebhooks => new Uri($"{API_BASE}/webhooks/");

        public static Uri ApiWebhooksUnsubscribe => new Uri($"{API_BASE}/webhooks/unsubscribe/");

        public static Uri UploadBase => new Uri($"{UPLOAD_BASE}/base/");

        public static Uri UploadFromUrl(string sourceUrl, string pubKey, bool? store = null)
        {
            var path = $"{UPLOAD_BASE}/from_url/?source_url={ sourceUrl}&pub_key={pubKey}";
            if (store != null)
            {
                if (store.Value)
                {
                    path += "&store=1";
                }
                else
                {
                    path += "&store=0";
                }
            }

            var builder = new UriBuilder(new Uri(path));

            return builder.Uri;
        }

        public static Uri UploadFromUrlStatus(string token)
        {
            var path = $"{UPLOAD_BASE}/from_url/status/?token={token}";

            var builder = new UriBuilder(new Uri(path));

            return builder.Uri;
        }
    }
}
