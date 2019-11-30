using System;
using Uploadcare.DTO;

namespace Uploadcare.Models
{
	public class UploadcareFile
	{
		private readonly FileData _fileData;

		internal UploadcareFile(FileData fileData)
		{
			_fileData = fileData;
		}

	    protected UploadcareFile()
	    {
	        
	    }

	    public virtual string FileId => _fileData.Uuid;

        public bool Stored => _fileData.DatetimeStored.HasValue && _fileData.DatetimeStored != DateTime.MinValue;

        public bool Removed => _fileData.DatetimeRemoved.HasValue && RemovedDateTime != DateTime.MinValue;

        public DateTime? RemovedDateTime => _fileData.DatetimeRemoved;

        public DateTime? StoreDateTime => _fileData.DatetimeStored;

        public long Size => _fileData.Size;

        public DateTime UploadDate => _fileData.DatetimeUploaded;

		/// <summary>
		/// Creates a CDN path builder for this file.
		/// </summary>
		/// <returns> CDN path builder
		/// </returns>
		public CdnPathBuilder CdnPath()
		{
			return new CdnPathBuilder(this);
		}
	}

}