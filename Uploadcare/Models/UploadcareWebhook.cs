using System;
using System.Text.Json.Serialization;

namespace Uploadcare.Models
{
    public class UploadcareWebhook
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }
        
        [JsonPropertyName("updated")]
        public DateTime? Updated { get; set; }
        
        [JsonPropertyName("event")]
        public string EventType { get; set; }
        
        [JsonPropertyName("target_url")]
        public string TargetUrl { get; set; }
        
        [JsonPropertyName("project")]
        public int Project { get; set; }
        
        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }
    }
}