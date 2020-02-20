using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Uploadcare.DTO
{
    internal class FaceDetectionData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("orientation")]
        public string Orientation { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("geo_location")]
        public GeoLocationData GeoLocation { get; set; }

        [JsonProperty("datetime_original")]
        public DateTime? DatetimeOriginal { get; set; }

        [JsonProperty("dpi")]
        public string DPI { get; set; }

        [JsonProperty("faces")]
        public List<List<int>> Faces { get; set; }
    }
}