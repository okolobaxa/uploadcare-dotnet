using System;
using System.Text.Json.Serialization;

namespace Uploadcare.Models
{
    public class UploadcareFile
    {
        [JsonPropertyName("uuid")]
        public virtual string Uuid { get; set; }

        [JsonPropertyName("datetime_removed")] 
        public DateTime? DatetimeRemoved { get; set; }

        [JsonPropertyName("datetime_stored")] 
        public DateTime? DatetimeStored { get; set; }

        [JsonPropertyName("datetime_uploaded")] 
        public DateTime DatetimeUploaded { get; set; }

        [JsonPropertyName("is_image")] 
        public bool IsImage { get; set; }

        [JsonPropertyName("is_ready")] 
        public bool IsReady { get; set; }

        [JsonPropertyName("mime_type")] 
        public string MimeType { get; set; }

        [JsonPropertyName("original_file_url")] 
        public string OriginalFileUrl { get; set; }

        [JsonPropertyName("original_filename")] 
        public string OriginalFilename { get; set; }

        [JsonPropertyName("size")] 
        public long Size { get; set; }

        [JsonPropertyName("url")] 
        public string Url { get; set; }

        [JsonPropertyName("image_info")] 
        public ImageInfo ImageInfoData { get; set; }

        public bool Stored => DatetimeStored.HasValue && DatetimeStored != DateTime.MinValue;

        public bool Removed => DatetimeRemoved.HasValue && DatetimeRemoved != DateTime.MinValue;

        /// <summary>
        /// Creates a CDN path builder for this file.
        /// </summary>
        /// <returns> CDN path builder </returns>
        public CdnPathBuilder CdnPath()
        {
            return new CdnPathBuilder(this);
        }
    }

    public class ImageInfo
    {
        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("geo_location")]
        public GeoLocationInfo GeoLocation { get; set; }

        [JsonPropertyName("datetime_original")]
        public DateTime? DatetimeOriginal { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }
    }

    public class GeoLocationInfo
    {
        [JsonPropertyName("latitude")]
        public decimal Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public decimal Longitude { get; set; }
    }
}