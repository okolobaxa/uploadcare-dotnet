using System;
using System.Collections.Generic;
using System.Linq;
using Uploadcare.DTO;

namespace Uploadcare.Models
{
    public class UploadcareGroup
    {
        private readonly GroupData _groupData;

        internal UploadcareGroup(GroupData groupData)
        {
            _groupData = groupData;
        }

        protected UploadcareGroup() { }

        public string Id => _groupData.Id;

        public DateTime CreateDate => _groupData.DatetimeCreated;

        public DateTime? StoreDate => _groupData.DatetimeStored;

        public long FilesCount => _groupData.FilesCount;

        public string CdnUrl => _groupData.CdnUrl;

        public string Url => _groupData.Url;

        public IEnumerable<UploadcareFile> Files => _groupData.Files.Select(x => new UploadcareFile(x));
    }
}