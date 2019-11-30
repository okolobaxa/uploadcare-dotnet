using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Uploadcare.DTO
{
    internal class GroupData
    {
        [JsonProperty("id")] 
        public string Id { get; set; }

        [JsonProperty("datetime_created")] 
        public DateTime DatetimeCreated { get; set; }

        [JsonProperty("datetime_stored")] 
        public DateTime? DatetimeStored { get; set; }

        [JsonProperty("files_count")] 
        public long FilesCount { get; set; }

        [JsonProperty("cdn_url")] 
        public string CdnUrl { get; set; }

        [JsonProperty("files")] 
        public List<FileData> Files { get; set; }

        [JsonProperty("url")] 
        public string Url { get; set; }
    }
}