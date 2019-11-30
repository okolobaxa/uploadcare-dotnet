using Newtonsoft.Json;

namespace Uploadcare.DTO
{
    internal class BaseCopyFileData
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    internal class CopyFileData : BaseCopyFileData
    {
        [JsonProperty("result")]
        public FileDataBase File { get; set; }
    }

    internal class CopyUrlData : BaseCopyFileData
    {
        [JsonProperty("result")]
        public string Url { get; set; }
    }
}
