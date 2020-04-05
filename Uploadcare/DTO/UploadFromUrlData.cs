using System.Text.Json.Serialization;

namespace Uploadcare.DTO
{
    internal class UploadFromUrlData
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
