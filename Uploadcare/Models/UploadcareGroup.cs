using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Uploadcare.Models
{
    public class UploadcareGroup
    {
        [JsonPropertyName("id")] 
        public string Id { get; set; }

        [JsonPropertyName("datetime_created")] 
        public DateTime DatetimeCreated { get; set; }

        [JsonPropertyName("datetime_stored")] 
        public DateTime? DatetimeStored { get; set; }

        [JsonPropertyName("files_count")] 
        public long FilesCount { get; set; }

        [JsonPropertyName("cdn_url")] 
        public string CdnUrl { get; set; }

        [JsonPropertyName("files")] 
        public List<UploadcareFile> Files { get; set; }

        [JsonPropertyName("url")] 
        public string Url { get; set; }
    }
}