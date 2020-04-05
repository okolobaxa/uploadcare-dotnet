using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Uploadcare.Models
{
    public class UploadcareProject
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("pub_key")]
        public string PubKey { get; set; }
        
        [JsonPropertyName("collaborators")]
        public IList<UploadcareCollaborator> Collaborators { get; set; }


        public UploadcareCollaborator Owner => Collaborators?.FirstOrDefault();
    }

    public class UploadcareCollaborator
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}