using System;
using UploadcareCSharp.Data;
using UploadcareCSharp.Url;

namespace UploadcareCSharp.API
{
	/// <summary>
	/// The main Uploadcare resource, represents a user-uploaded file.
	/// </summary>
	public class UploadcareFile
	{
		private readonly Client _client;
		private readonly FileData _fileData;

		public UploadcareFile(Client client, FileData fileData)
		{
			_client = client;
			_fileData = fileData;
		}

        public UploadcareFile()
        {
        }

		public virtual Guid FileId
		{
			get
			{
				return _fileData.Uuid;
			}
		}

		public bool Stored
		{
			get
			{
				return _fileData.DatetimeStored.HasValue && _fileData.DatetimeStored != DateTime.MinValue;
			}
		}

		public string MimeType
		{
			get
			{
				return _fileData.MimeType;
			}
		}

		public bool HasOriginalFileUrl()
		{
			return _fileData.OriginalFileUrl != null;
		}

		public Uri OriginalFileUrl
		{
			get
			{
				return new Uri(_fileData.OriginalFileUrl);
			}
		}

		public string OriginalFilename
		{
			get
			{
				return _fileData.OriginalFilename;
			}
		}

		public bool Removed
		{
			get
			{
                return _fileData.DatetimeRemoved.HasValue && RemovedDateTime != DateTime.MinValue;
			}
		}

		public DateTime RemovedDateTime
		{
			get
			{
			    return _fileData.DatetimeRemoved ?? DateTime.MinValue;
			}
		}

		public int Size
		{
			get
			{
				return _fileData.Size;
			}
		}

		public DateTime UploadDate
		{
			get
            {
                return _fileData.DatetimeUploaded ?? DateTime.MinValue;
			}
		}

		/// <summary>
		/// Returns the unique REST URL for this resource.
		/// </summary>
		/// <returns> REST URL </returns>
		public Uri Url
		{
			get
			{
				return new Uri(_fileData.Url);
			}
		}

		/// <summary>
		/// Refreshes file data from Uploadcare.
		/// 
		/// This does not mutate the current {@code UploadcareFile} instance,
		/// but creates a new one.
		/// </summary>
		/// <returns> New file resource instance </returns>
		public UploadcareFile Update()
		{
			return _client.GetFile(_fileData.Uuid);
		}

		/// <summary>
		/// Deletes this file from Uploadcare.
		/// 
		/// This does not mutate the current {@code UploadcareFile} instance,
		/// but creates a new one.
		/// </summary>
		/// <returns> New file resource instance </returns>
		public UploadcareFile Delete()
		{
			_client.DeleteFile(_fileData.Uuid);
			return Update();
		}

		/// <summary>
		/// Saves this file on Uploadcare (marks it to be kept).
		/// 
		/// This does not mutate the current {@code UploadcareFile} instance,
		/// but creates a new one.
		/// </summary>
		/// <returns> New file resource instance </returns>
		public UploadcareFile Save()
		{
            _client.SaveFile(_fileData.Uuid);
			
            return Update();
		}

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