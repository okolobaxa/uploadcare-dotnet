using System;
using Newtonsoft.Json;

namespace UploadcareCSharp.Data
{
    internal class UploadFromUrlStatusData
    {
        [JsonProperty("status")]
        public string Status { get; private set; }
        [JsonProperty("file_id")]
        public Guid FileId { get; private set; }
    }
}
