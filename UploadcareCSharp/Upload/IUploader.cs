using UploadcareCSharp.API;
using UploadcareCSharp.Enums;

namespace UploadcareCSharp.Upload
{
	public interface IUploader
	{
        UploadcareFile Upload(EStoreType type);
	}

}