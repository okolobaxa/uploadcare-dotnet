using Uploadcare.DTO;
using Uploadcare.Models;

namespace Uploadcare.Utils
{
    internal class GroupDataWrapper : IDataWrapper<UploadcareGroup, GroupData>
    {
        public UploadcareGroup Wrap(GroupData data)
        {
            return new UploadcareGroup(data);
        }
    }
}
