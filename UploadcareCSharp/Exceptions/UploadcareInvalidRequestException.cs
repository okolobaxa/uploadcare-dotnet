namespace Uploadcare.Exceptions
{
	/// <summary>
	/// Error produced in case the http request sent to the Uploadcare API was invalid.
	/// </summary>
	public class UploadcareInvalidRequestException : UploadcareApiException
	{
		public UploadcareInvalidRequestException(string message) : base(message)
		{
		}
	}

}