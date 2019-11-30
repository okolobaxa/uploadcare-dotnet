using Newtonsoft.Json;

namespace Uploadcare.DTO
{
    internal class UploadFromUrlData
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
    }
}
