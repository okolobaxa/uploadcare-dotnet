using Uploadcare.API;

namespace Uploadcare.Upload
{
	public interface IUploader
	{
        UploadcareFile Upload();
	}

}