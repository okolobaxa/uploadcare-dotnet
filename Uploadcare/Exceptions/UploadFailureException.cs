using System;

namespace Uploadcare.Exceptions
{
	public class UploadFailureException : Exception
	{
        public UploadFailureException()
        {

        }

        public UploadFailureException(Exception e) : base("Upload failed", e)
        {

        }

        public UploadFailureException(string message) : base(message)
        {

        }
    }
}