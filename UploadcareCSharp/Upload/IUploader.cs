using System;
﻿using UploadcareCSharp.API;

namespace UploadcareCSharp.Upload
{
	public interface IUploader
	{
        UploadcareFile Upload(bool? store);
	}
}
