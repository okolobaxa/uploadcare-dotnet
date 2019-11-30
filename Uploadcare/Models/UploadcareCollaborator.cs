using Uploadcare.DTO;

namespace Uploadcare.Models
{
    public class UploadcareCollaborator
    {
        private readonly CollaboratorData _collaboratorData;

        internal UploadcareCollaborator(CollaboratorData collaboratorData)
        {
            _collaboratorData = collaboratorData;
        }

        public string Name => _collaboratorData.Name;

        public string Email => _collaboratorData.Email;
    }
}
