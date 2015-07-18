using Uploadcare.API;
using UploadcareCSharp.Enums;

namespace Uploadcare.Upload
{
	public interface IUploader
	{
        UploadcareFile Upload(EStoreType type);
	}

}