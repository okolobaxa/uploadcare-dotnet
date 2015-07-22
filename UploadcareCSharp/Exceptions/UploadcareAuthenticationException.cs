namespace UploadcareCSharp.Exceptions
{
	/// <summary>
	///  An authentication error returned by the uploadcare API
	/// </summary>
	public class UploadcareAuthenticationException : UploadcareApiException
	{
		public UploadcareAuthenticationException(string message) : base(message)
		{
		}
	}

}