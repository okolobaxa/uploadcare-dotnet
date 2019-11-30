using Newtonsoft.Json;

namespace Uploadcare.DTO
{
    internal class UploadFromUrlStatusData
    {
        [JsonProperty("status")]
        public string Status { get; private set; }
        
        [JsonProperty("file_id")]
        public string FileId { get; private set; }
    }
}
