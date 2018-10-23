using System;
using System.Drawing;
using System.Text;
using UploadCareCSharp.API;

namespace UploadcareCSharp.Url
{
	public sealed class CdnPathBuilder
	{
		private readonly StringBuilder _sb = new StringBuilder("/");

		/// <summary>
		/// Creates a new CDN path builder for some image UploadcareFile.
		/// </summary>
		/// <param name="uploadcareFile"> UploadcareFile to be used for the path
		/// </param>
		public CdnPathBuilder(UploadcareFile uploadcareFile)
		{
			_sb.Append(uploadcareFile.FileId);
		}

	    /// <summary>
	    /// Creates a new CDN path builder for some image id.
	    /// </summary>
        /// <param name="fileId">Image id to be used for the path</param>
	    public CdnPathBuilder(Guid fileId)
        {
            _sb.Append(fileId);
        }

        public CdnPathBuilder()
        {
        }

		private static void DimensionGuard(int dim)
		{
			if (dim < 1 || dim > 1024)
			{
				throw new ArgumentException("Dimensions must be in the range 1-1024");
			}
		}

		private static void DimensionsGuard(int width, int height)
		{
			DimensionGuard(width);
			DimensionGuard(height);
			if (width > 634 && height > 634)
			{
				throw new ArgumentException("At least one dimension must be less than 634");
			}
		}

		private static string ColorToHex(Color color)
		{
            return (color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2")).ToLower();
		}

		/// <summary>
		/// Adds top-left-aligned crop.
		/// </summary>
		/// <param name="width"> Crop width </param>
		/// <param name="height"> Crop height </param>
		public CdnPathBuilder Crop(int width, int height)
		{
			DimensionsGuard(width, height);
			_sb.Append("/-/crop/").Append(width).Append("x").Append(height);
			return this;
		}

		/// <summary>
		/// Adds center-aligned crop.
		/// </summary>
		/// <param name="width"> Crop width </param>
		/// <param name="height"> Crop height </param>
		public CdnPathBuilder CropCenter(int width, int height)
		{
			DimensionsGuard(width, height);
			_sb.Append("/-/crop/").Append(width).Append("x").Append(height).Append("/center");
			return this;
		}

		/// <summary>
		/// Adds top-left-aligned crop with a filled background.
		/// </summary>
		/// <param name="width"> Crop width </param>
		/// <param name="height"> Crop height </param>
		/// <param name="color"> Background color </param>
		public CdnPathBuilder CropColor(int width, int height, Color color)
		{
			DimensionsGuard(width, height);
			_sb.Append("/-/crop/").Append(width).Append("x").Append(height).Append("/").Append(ColorToHex(color));
			return this;
		}

		/// <summary>
		/// Adds center-aligned crop with a filled background.
		/// </summary>
		/// <param name="width"> Crop width </param>
		/// <param name="height"> Crop height </param>
		/// <param name="color"> Background color </param>
		public CdnPathBuilder CropCenterColor(int width, int height, Color color)
		{
			DimensionsGuard(width, height);
			_sb.Append("/-/crop/").Append(width).Append("x").Append(height).Append("/center/").Append(ColorToHex(color));
			return this;
		}

		/// <summary>
		/// Resizes width, keeping the aspect ratio.
		/// </summary>
		/// <param name="width"> New width </param>
		public CdnPathBuilder ResizeWidth(int width)
		{
			DimensionGuard(width);
			_sb.Append("/-/resize/").Append(width).Append("x");
			return this;
		}

		/// <summary>
		/// Resizes height, keeping the aspect ratio.
		/// </summary>
		/// <param name="height"> New height </param>
		public CdnPathBuilder ResizeHeight(int height)
		{
			DimensionGuard(height);
			_sb.Append("/-/resize/x").Append(height);
			return this;
		}

		/// <summary>
		/// Resizes width and height
		/// </summary>
		/// <param name="width"> New width </param>
		/// <param name="height"> New height </param>
		public CdnPathBuilder Resize(int width, int height)
		{
			DimensionsGuard(width, height);
			_sb.Append("/-/resize/").Append(width).Append("x").Append(height);
			return this;
		}

		/// <summary>
		/// Scales the image until one of the dimensions fits,
		/// then crops the bottom or right side.
		/// </summary>
		/// <param name="width"> New width </param>
		/// <param name="height"> New height </param>
		public CdnPathBuilder ScaleCrop(int width, int height)
		{
			DimensionsGuard(width, height);
			_sb.Append("/-/scale_crop/").Append(width).Append("x").Append(height);
			return this;
		}

		 /// <summary>
		 /// Scales the image until one of the dimensions fits,
		 /// centers it, then crops the rest.
		 /// </summary>
		 /// <param name="width"> New width </param>
		 /// <param name="height"> New height </param>
		public CdnPathBuilder ScaleCropCenter(int width, int height)
		{
			DimensionsGuard(width, height);
			_sb.Append("/-/scale_crop/").Append(width).Append("x").Append(height).Append("/center");
			return this;
		}

		/// <summary>
		/// Flips the image.
		/// </summary>
		public CdnPathBuilder Flip()
		{
			_sb.Append("/-/effect/flip");
			return this;
		}

		/// <summary>
		/// Adds a grayscale effect.
		/// </summary>
		public CdnPathBuilder Grayscale()
		{
			_sb.Append("/-/effect/grayscale");
			return this;
		}

		/// <summary>
		/// Inverts colors.
		/// </summary>
		public CdnPathBuilder Invert()
		{
			_sb.Append("/-/effect/invert");
			return this;
		}

		/// <summary>
		/// Horizontally mirror image.
		/// </summary>
		public CdnPathBuilder Mirror()
		{
			_sb.Append("/-/effect/mirror");
			return this;
		}

		/// <summary>
		/// Returns the current CDN path as a string.
		/// 
		/// Avoid using directly.
		/// Instead, pass the configured builder to a URL factory.
		/// </summary>
		/// <returns> CDN path
		/// </returns>
		public string Build()
		{
			return _sb.Append("/").ToString();
		}
	}
}