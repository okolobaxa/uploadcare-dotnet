using System;

namespace Uploadcare.Exceptions
{
	/// <summary>
	/// An exception thrown in cases of network failure.
	/// </summary>
	public class UploadcareNetworkException : UploadcareApiException
	{
		public UploadcareNetworkException(Exception cause) : base("Network failure!", cause)
		{
		}
	}

}