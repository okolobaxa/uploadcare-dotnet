using System.Collections.Generic;
using System.Text.Json.Serialization;
using Uploadcare.Utils;

namespace Uploadcare.DTO
{
    internal class BasePageData<T> : IPageData<T>
    {
        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("previous")]
        public string Previous { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("results")]
        public List<T> Results { get; set; }

        public List<T> GetResults()
        {
            return Results;
        }

        public bool HasMore()
        {
            return Next != null;
        }
    }
}
