using System;
using Newtonsoft.Json;

namespace Uploadcare.DTO
{
    internal class WebhookData
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        
        [JsonProperty("updated")]
        public DateTime? Updated { get; set; }
        
        [JsonProperty("event")]
        public string EventType { get; set; }
        
        [JsonProperty("target_url")]
        public string TargetUrl { get; set; }
        
        [JsonProperty("project")]
        public int Project { get; set; }
        
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
    }
}