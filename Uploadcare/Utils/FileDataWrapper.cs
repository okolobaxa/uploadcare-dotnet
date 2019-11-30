using Uploadcare.DTO;
using Uploadcare.Models;

namespace Uploadcare.Utils
{
    internal class FileDataWrapper : IDataWrapper<UploadcareFile, FileData>
    {
        public UploadcareFile Wrap(FileData data)
        {
            return new UploadcareFile(data);
        }
    }
}
