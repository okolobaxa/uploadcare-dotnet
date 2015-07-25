using System.Collections.Generic;

namespace UploadcareCSharp.Data
{
    internal class ProjectData
    {
        public string Name { get; set; }
        public string PubKey { get; set; }
        public IList<CollaboratorData> Collaborators { get; set; }
    }

    internal class CollaboratorData
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}