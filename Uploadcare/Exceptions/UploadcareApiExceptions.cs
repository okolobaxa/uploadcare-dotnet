using System;

namespace Uploadcare.Exceptions
{
    /// <summary>
    /// A generic error of the uploadcare API.
    /// </summary>
    public class UploadcareApiException : Exception
    {
        public UploadcareApiException(string message) : base(message) { }

        protected UploadcareApiException(string message, Exception cause) : base(message, cause) { }
    }

    /// <summary>
    ///   An exception thrown in case of authentication error returned by the uploadcare API
    /// </summary>
    public class UploadcareAuthenticationException : UploadcareApiException
    {
        public UploadcareAuthenticationException(string message) : base(message) { }
    }

    /// <summary>
    /// An exception thrown in case the http request sent to the Uploadcare API was invalid.
    /// </summary>
    public class UploadcareInvalidRequestException : UploadcareApiException
    {
        public UploadcareInvalidRequestException(string message) : base(message) { }
    }

    /// <summary>
    /// An exception thrown in case the http response received from Uploadcare API was invalid.
    /// </summary>
    public class UploadcareInvalidResponseException : UploadcareApiException
    {
        public UploadcareInvalidResponseException(string message) : base(message) { }
    }

    /// <summary>
    /// An exception thrown in cases of network failure.
    /// </summary>
    public class UploadcareNetworkException : UploadcareApiException
    {
        public UploadcareNetworkException(Exception cause) : base("Network failure", cause) { }

        public UploadcareNetworkException(string message) : base(message) { }
    }

}