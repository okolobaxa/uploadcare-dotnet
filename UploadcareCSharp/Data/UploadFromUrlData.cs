using Newtonsoft.Json;

namespace UploadcareCSharp.Data
{
    internal class UploadFromUrlData
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
    }
}
