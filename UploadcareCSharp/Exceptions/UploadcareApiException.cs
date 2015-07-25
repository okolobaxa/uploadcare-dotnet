using System;

namespace UploadcareCSharp.Exceptions
{

	/// <summary>
	/// A generic error of the uploadcare API.
	/// </summary>
	public class UploadcareApiException : Exception
	{
		public UploadcareApiException(string message) : base(message)
		{
		}

	    protected UploadcareApiException(string message, Exception cause) : base(message, cause)
		{
		}
	}

}