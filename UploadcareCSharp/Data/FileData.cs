using System;
using Newtonsoft.Json;

namespace UploadcareCSharp.Data
{
	internal class FileData
	{
        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }
        [JsonProperty("original_file_url")]
		public string OriginalFileUrl { get; set; }
        [JsonProperty("mime_type")]
        public string MimeType { get; set; }
        [JsonProperty("is_ready")]
        public bool IsReady { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("original_filename")]
        public string OriginalFilename { get; set; }
        [JsonProperty("size")]
        public int Size { get; set; }
        [JsonProperty("is_image")]
        public bool IsImage { get; set; }
        [JsonProperty("datetime_uploaded")]
        public DateTime? DatetimeUploaded { get; set; }
        [JsonProperty("datetime_stored")]
        public DateTime? DatetimeStored { get; set; }
        [JsonProperty("datetime_removed")]
        public DateTime? DatetimeRemoved { get; set; }
    }

}