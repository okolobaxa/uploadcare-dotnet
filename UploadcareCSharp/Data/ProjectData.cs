using System.Collections.Generic;

namespace UploadcareCSharp.Data
{
    public class ProjectData
    {
        public string Name { get; set; }
        public string PubKey { get; set; }
        public IList<CollaboratorData> Collaborators { get; set; }
    }

    public class CollaboratorData
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}