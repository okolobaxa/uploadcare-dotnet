using UploadcareCSharp.API;

namespace UploadcareCSharp.Upload
{
	public interface IUploader
	{
        UploadcareFile Upload();
	}
}