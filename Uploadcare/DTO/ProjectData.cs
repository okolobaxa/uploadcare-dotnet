using System.Collections.Generic;
using Newtonsoft.Json;

namespace Uploadcare.DTO
{
    internal class ProjectData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("pub_key")]
        public string PubKey { get; set; }
        
        [JsonProperty("collaborators")]
        public IList<CollaboratorData> Collaborators { get; set; }
    }

    internal class CollaboratorData
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}