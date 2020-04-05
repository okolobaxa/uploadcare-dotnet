using System.Collections.Generic;
using System.Text.Json.Serialization;
using Uploadcare.Models;

namespace Uploadcare.DTO
{
    internal class MassOperationsData
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("problems")]
        public Dictionary<string, string> Problems { get; set; }

        [JsonPropertyName("result")]
        public List<UploadcareFile> Files { get; set; }
    }
}
