using Newtonsoft.Json;

namespace Uploadcare.DTO
{
    internal class UploadcareFileBaseData
    {
        [JsonProperty("file")]
        public string File { get; private set; }
    }
}