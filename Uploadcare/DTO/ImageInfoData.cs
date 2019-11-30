using System;
using Newtonsoft.Json;

namespace Uploadcare.DTO
{
    internal class ImageInfoData
    {
        [JsonProperty("height")] 
        public string Height { get; set; }

        [JsonProperty("width")] 
        public string Width { get; set; }

        [JsonProperty("geo_location")] 
        public GeoLocationData GeoLocation { get; set; }

        [JsonProperty("datetime_original")] 
        public DateTime? DatetimeOriginal { get; set; }

        [JsonProperty("format")] 
        public string Format { get; set; }
    }

    internal class GeoLocationData
    {
        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }
    }
}