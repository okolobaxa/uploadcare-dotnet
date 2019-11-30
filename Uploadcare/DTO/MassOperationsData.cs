using System.Collections.Generic;
using Newtonsoft.Json;

namespace Uploadcare.DTO
{
    internal class MassOperationsData
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("problems")]
        public Dictionary<string, string> Problems { get; set; }

        [JsonProperty("result")]
        public List<FileData> Files { get; set; }
    }
}
