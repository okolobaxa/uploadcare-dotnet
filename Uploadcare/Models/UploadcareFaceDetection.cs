using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Uploadcare.Models
{
    public class UploadcareFaceDetection
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("orientation")]
        public int? Orientation { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("geo_location")]
        public GeoLocationInfo GeoLocation { get; set; }

        [JsonPropertyName("datetime_original")]
        public DateTime? DatetimeOriginal { get; set; }

        [JsonPropertyName("dpi")]
        public List<int> DPI { get; set; }

        [JsonPropertyName("faces"), JsonConverter(typeof(UploadcareFaceConverter))]
        public List<UploadcareFace> Faces { get; set; }
    }

    public class UploadcareFace
    {
        public UploadcareFace(int[] numbers)
        {
            FaceCoordinates = numbers;
        }

        public int[] FaceCoordinates { get; set; }

        /// <summary>
        /// Coordinates of the upper-left corner of an area where a face was found.
        /// </summary>
        public int Left => FaceCoordinates[0];

        /// <summary>
        /// Coordinates of the upper-left corner of an area where a face was found.
        /// </summary>
        public int Top => FaceCoordinates[1];

        /// <summary>
        /// Dimensions of that area
        /// </summary>
        public int Width => FaceCoordinates[2];

        /// <summary>
        /// Dimensions of that area
        /// </summary>
        public int Height => FaceCoordinates[3];
    }

    public class UploadcareFaceConverter : JsonConverter<List<UploadcareFace>>
    {
        public override List<UploadcareFace> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            var result = new List<UploadcareFace>();

            var startDepth = reader.CurrentDepth;

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.EndArray when reader.CurrentDepth == startDepth:
                        return result;
                    case JsonTokenType.EndArray:
                        continue;
                }

                var numbers = new int[4];
                reader.Read();
                numbers[0] = reader.GetInt32();

                reader.Read();
                numbers[1] = reader.GetInt32();

                reader.Read();
                numbers[2] = reader.GetInt32();

                reader.Read();
                numbers[3] = reader.GetInt32();

                var face = new UploadcareFace(numbers);
                result.Add(face);
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, List<UploadcareFace> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}