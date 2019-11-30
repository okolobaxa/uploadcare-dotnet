using System.Collections.Generic;
using Newtonsoft.Json;
using Uploadcare.Utils;

namespace Uploadcare.DTO
{
    internal class BasePageData<T> : IPageData<T>
    {
        [JsonProperty("per_page")]
        private int PerPage { get; set; }

        [JsonProperty("total")]
        public int Total { get; private set; }

        [JsonProperty("previous")]
        private string Previous { get; set; }

        [JsonProperty("next")]
        private string Next { get; set; }

        [JsonProperty("results")]
        private List<T> Results { get; set; }

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
