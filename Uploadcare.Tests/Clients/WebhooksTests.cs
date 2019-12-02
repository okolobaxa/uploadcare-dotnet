using System.Threading.Tasks;
using Xunit;

namespace Uploadcare.Tests.Clients
{
    public class WebhooksTests
    {
        [Fact(Skip = "You need to have a special account to use webhooks")]
        public async Task client_getwebhooks_assert()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();

            var webhook = await client.Webhooks.GetAsync();

            Assert.NotNull(webhook);
        }

        [Fact(Skip = "You need to have a special account to use webhooks")]
        public async Task client_webhook_subscribe_assert()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();

            var webhook = await client.Webhooks.SubscribeAsync("file.uploaded", "https://google.com", true);

            Assert.NotNull(webhook);

            //clean-up
            await client.Webhooks.UnsubscribeAsync("file.uploaded", "https://google.com");
        }

        [Fact(Skip = "You need to have a special account to use webhooks")]
        public async Task client_webhook_unsubscribe_assert()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();

            var webhook = await client.Webhooks.SubscribeAsync("file.uploaded", "https://google.com", true);

            await client.Webhooks.UnsubscribeAsync("file.uploaded", "https://google.com");

            Assert.NotNull(webhook);

            //clean-up
            await client.Webhooks.UnsubscribeAsync("file.uploaded", "https://google.com");
        }

        [Fact(Skip = "You need to have a special account to use webhooks")]
        public async Task client_webhook_update_assert()
        {
            var client = UploadcareClient.DemoClientWithSignedAuth();

            var webhook = await client.Webhooks.SubscribeAsync("file.uploaded", "https://google.com", true);
            var updated = await client.Webhooks.UpdateAsync(webhook.Id, "file.uploaded", "https://apple.com", false);

            Assert.NotNull(webhook);
            Assert.Equal("https://apple.com", updated.TargetUrl);

            //clean-up
            await client.Webhooks.UnsubscribeAsync("file.uploaded", "https://apple.com");
        }
    }
}