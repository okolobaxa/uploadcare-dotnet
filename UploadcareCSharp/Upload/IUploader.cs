using UploadCareCSharp.API;

namespace UploadcareCSharp.Upload
{
	public interface IUploader
	{
        UploadcareFile Upload(bool? store);
	}
}
