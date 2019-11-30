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

        public virtual string FileId => _fileData.Uuid;

        public bool IsImage => _fileData.IsImage;

        public bool IsReady => _fileData.IsReady;

        public string MimeType => _fileData.Uuid;

        public string OriginalFileUrl => _fileData.OriginalFileUrl;

        public string OriginalFilename => _fileData.OriginalFilename;

        public string Url => _fileData.Url;

        public bool Stored => _fileData.DatetimeStored.HasValue && StoreAt != DateTime.MinValue;

        public bool Removed => _fileData.DatetimeRemoved.HasValue && RemovedAt != DateTime.MinValue;

        public long Size => _fileData.Size;

        public DateTime? RemovedAt => _fileData.DatetimeRemoved;

        public DateTime? StoreAt => _fileData.DatetimeStored;

        public DateTime UploadAt => _fileData.DatetimeUploaded;

        public UploadcareImageInfo ImageInfo => IsImage ? new UploadcareImageInfo(_fileData.ImageInfoData) : null;

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


    public class UploadcareImageInfo
    {
        private readonly ImageInfoData _data;

        internal UploadcareImageInfo(ImageInfoData data)
        {
            _data = data;
        }

        public string Height => _data.Height;

        public string Width => _data.Width;

        public DateTime? DatetimeOriginal => _data.DatetimeOriginal;

        public string Format => _data.Format;

        public UploadcareGeolocation GeoLocation => new UploadcareGeolocation(_data.GeoLocation);
    }

    public class UploadcareGeolocation
    {
        private readonly GeoLocationData _data;

        internal UploadcareGeolocation(GeoLocationData data)
        {
            _data = data;
        }

        public string Latitude => _data.Latitude;

        public string Longitude => _data.Longitude;
    }
}
