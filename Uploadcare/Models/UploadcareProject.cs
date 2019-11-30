using System.Collections.Generic;
using System.Linq;
using Uploadcare.DTO;

namespace Uploadcare.Models
{
    public class UploadcareProject
    {
        private readonly ProjectData _projectData;

        internal UploadcareProject(ProjectData projectData)
        {
            _projectData = projectData;

            if (_projectData.Collaborators != null && _projectData.Collaborators.Count > 0)
            {
                Collaborators = new List<UploadcareCollaborator>(_projectData.Collaborators.Count);

                Collaborators.AddRange(_projectData.Collaborators.Select(collaboratorData =>
                    new UploadcareCollaborator(collaboratorData)));
            }
            else
            {
                Collaborators = new List<UploadcareCollaborator>(0);
            }
        }

        public string Name => _projectData.Name;

        public string PubKey => _projectData.PubKey;

        public UploadcareCollaborator Owner =>
            _projectData.Collaborators.Count > 0
                ? new UploadcareCollaborator(_projectData.Collaborators.First())
                : null;

        public List<UploadcareCollaborator> Collaborators { get; }
    }
}