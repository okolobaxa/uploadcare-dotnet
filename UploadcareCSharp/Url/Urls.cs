using System;

namespace Uploadcare.Url
{
	public static class Urls
	{

		private const string Api_Base = "https://api.uploadcare.com";
		private const string Cdn_Base = "https://ucarecdn.com";
		private const string Upload_Base = "https://upload.uploadcare.com";

		/// <summary>
		/// Creates a URL to a project resource.
		/// </summary>
		public static Uri ApiProject()
		{
			return new Uri(Api_Base + "/project/");
		}

		/// <summary>
		/// Creates a URL to a file resource.
		/// </summary>
		/// <param name="fileId"> UploadcareFile UUID
		/// </param>
		public static Uri ApiFile(Guid fileId)
		{
		   return new Uri(Api_Base + "/files/" + fileId + "/");
		}

		/// <summary>
		/// Creates a URL to the storage action for a file (saving the file).
		/// </summary>
		/// <param name="fileId"> UploadcareFile UUID
		/// </param>
		public static Uri ApiFileStorage(Guid fileId)
		{
            return new Uri(Api_Base + "/files/" + fileId + "/storage/");
		}

		/// <summary>
		/// Creates a URL to the file collection resource.
		/// </summary>
		public static Uri ApiFiles()
		{
			return new Uri(Api_Base + "/files/");
		}

		/// <summary>
		/// Creates a full CDN URL with a CDN path builder.
		/// </summary>
		/// <param name="builder"> Configured CDN path builder </param>
		public static Uri Cdn(CdnPathBuilder builder)
		{
			return new Uri(Cdn_Base + builder.Build());
		}

		/// <summary>
		/// Creates a URL to the file upload endpoint.
		/// </summary>
		public static Uri UploadBase()
		{
            return new Uri(Upload_Base + "/base/");
		}

		/// <summary>
		/// Creates a URL for URL upload.
		/// </summary>
		/// <param name="sourceUrl"> URL to upload from </param>
		/// <param name="pubKey"> Public key
		/// </param>
		public static Uri UploadFromUrl(string sourceUrl, string pubKey)
		{
		    var path = Upload_Base + "/from_url/" + "?source_url=" + sourceUrl + "&pub_key=" + pubKey;
            
            var builder = new UriBuilder(new Uri(path));

            return builder.Uri;
		}

		/// <summary>
		/// Creates a URL for URL upload status (e.g. progress).
		/// </summary>
		/// <param name="token"> Token, received after a URL upload request
		/// </param>
		public static Uri UploadFromUrlStatus(string token)
		{
            var path = Upload_Base + "/from_url/status/" + "?token=" + token;

            var builder = new UriBuilder(new Uri(path));

            return builder.Uri;
		}

	}

}