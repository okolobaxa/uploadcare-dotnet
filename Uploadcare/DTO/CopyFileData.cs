using System.Text.Json.Serialization;

namespace Uploadcare.DTO
{
    internal class BaseCopyFileData
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    internal class CopyFileData : BaseCopyFileData
    {
        [JsonPropertyName("result")]
        public FileDataBase File { get; set; }
    }

    internal class CopyUrlData : BaseCopyFileData
    {
        [JsonPropertyName("result")]
        public string Url { get; set; }
    }

    internal class FileDataBase
    {
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }
    }
}
