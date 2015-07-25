using System.Collections.Generic;
using Newtonsoft.Json;

namespace UploadcareCSharp.Data
{
    internal class FilePageData : IPageData<FileData>
    {
        [JsonProperty("page")]
        private int Page { get; set; }
        [JsonProperty("pages")]
        private int Pages { get; set; }
        [JsonProperty("results")]
        private List<FileData> Results { get; set; }

        public List<FileData> GetResults()
        {
            return Results;
        }

        public bool HasMore()
        {
            return Page < Pages;
        }
    }
}
