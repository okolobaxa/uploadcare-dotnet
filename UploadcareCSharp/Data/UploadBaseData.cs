using System;
using Newtonsoft.Json;

namespace UploadcareCSharp.Data
{
	internal class UploadBaseData
	{
        [JsonProperty("file")]
		public Guid File { get; private set; }
	}

}