using System.Text.Json.Serialization;

namespace Uploadcare.DTO
{
    internal class UploadFromUrlStatusData
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }
    }
}
