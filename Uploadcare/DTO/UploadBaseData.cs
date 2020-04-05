using System.Text.Json.Serialization;

namespace Uploadcare.DTO
{
    internal class UploadcareFileBaseData
    {
        [JsonPropertyName("file")]
        public string File { get; set; }
    }
}