using System;

namespace UploadcareCSharp.Url
{
	public static class Urls
	{
		private const string API_BASE = "https://api.uploadcare.com";
		private const string CDN_BASE = "https://ucarecdn.com";
		private const string UPLOAD_BASE = "https://upload.uploadcare.com";

		/// <summary>
		/// Creates a URL to a project resource.
		/// </summary>
		public static Uri ApiProject()
		{
			return new Uri(API_BASE + "/project/");
		}

		/// <summary>
		/// Creates a URL to a file resource.
		/// </summary>
		/// <param name="fileId"> UploadcareFile UUID
		/// </param>
		public static Uri ApiFile(Guid fileId)
		{
		   return new Uri(API_BASE + "/files/" + fileId + "/");
		}

		/// <summary>
		/// Creates a URL to the storage action for a file (saving the file).
		/// </summary>
		/// <param name="fileId"> UploadcareFile UUID
		/// </param>
		public static Uri ApiFileStorage(Guid fileId)
		{
            return new Uri(API_BASE + "/files/" + fileId + "/storage/");
		}

		/// <summary>
		/// Creates a URL to the file collection resource.
		/// </summary>
		public static Uri ApiFiles()
		{
			return new Uri(API_BASE + "/files/");
		}

		/// <summary>
		/// Creates a full CDN URL with a CDN path builder.
		/// </summary>
		/// <param name="builder"> Configured CDN path builder </param>
		public static Uri Cdn(CdnPathBuilder builder)
		{
			return new Uri(CDN_BASE + builder.Build());
		}

		/// <summary>
		/// Creates a URL to the file upload endpoint.
		/// </summary>
		public static Uri UploadBase()
		{
            return new Uri(UPLOAD_BASE + "/base/");
		}

		/// <summary>
		/// Creates a URL for URL upload.
		/// </summary>
		/// <param name="sourceUrl"> URL to upload from </param>
		/// <param name="pubKey"> Public key
		/// </param>
		public static Uri UploadFromUrl(string sourceUrl, string pubKey, bool? store = null)
		{
			var path = UPLOAD_BASE + "/from_url/" + "?source_url=" + sourceUrl + "&pub_key=" + pubKey;
            		if (store != null) {
		        	if (store.Value) { path = path + "&store=1"; } else { path = path + "&store=0"; }
	            	}
            		
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
            var path = UPLOAD_BASE + "/from_url/status/" + "?token=" + token;

            var builder = new UriBuilder(new Uri(path));

            return builder.Uri;
		}
	}
}
