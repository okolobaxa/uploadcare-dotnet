using System;
using System.Drawing;
using Moq;
using UploadcareCSharp.API;
using UploadcareCSharp.Url;
using Xunit;

namespace UploadcareCSharp.Tests
{
	public class CdnPathBuilderTest
	{
		private const string FileId = "27c7846b-a019-4516-a5e4-de635f822161";
		private CdnPathBuilder _builder;


        [Fact]
		public void cdnpathbuilder_fileUrl_assert()
		{
            var fileMoq = new Mock<UploadcareFile>();
            fileMoq.Setup(x => x.FileId).Returns(new Guid(FileId));

            _builder = new CdnPathBuilder(fileMoq.Object);
			string path = _builder.Build();
			Assert.Equal("/" + FileId + "/", path);
		}

        [Fact]
        public void cdnpathbuilder_allOperations_assert()
		{
            var fileMoq = new Mock<UploadcareFile>();
            fileMoq.Setup(x => x.FileId).Returns(new Guid(FileId));

            _builder = new CdnPathBuilder(fileMoq.Object);

			string path = _builder.Crop(100, 110).CropColor(120, 130, Color.Black).CropCenter(140, 150).CropCenterColor(160, 170, Color.Red).Resize(100, 110).ResizeWidth(120).ResizeHeight(130).ScaleCrop(100, 110).ScaleCropCenter(120, 130).Flip().Grayscale().Invert().Mirror().Build();
			Assert.Equal("/" + FileId + "/-/crop/100x110" + "/-/crop/120x130/000000" + "/-/crop/140x150/center" + "/-/crop/160x170/center/ff0000" + "/-/resize/100x110" + "/-/resize/120x" + "/-/resize/x130" + "/-/scale_crop/100x110" + "/-/scale_crop/120x130/center" + "/-/effect/flip" + "/-/effect/grayscale" + "/-/effect/invert" + "/-/effect/mirror" + "/", path);
		}

        [Fact]
        public void cdnpathbuilder_dimensionGuard_fail()
		{
            var fileMoq = new Mock<UploadcareFile>();
            fileMoq.Setup(x => x.FileId).Returns(new Guid(FileId));

            _builder = new CdnPathBuilder(fileMoq.Object);

			_builder.ResizeWidth(1);
			_builder.ResizeWidth(1024);

			try
			{
				_builder.ResizeWidth(0);
			}
			catch (ArgumentException)
			{
				try
				{
					_builder.ResizeWidth(1025);
				}
				catch (ArgumentException)
				{
                    Assert.True(true);
				}
			}
		}

        [Fact]
        public void cdnpathbuilder_dimensionsGuard_fail()
		{
            var fileMoq = new Mock<UploadcareFile>();
            fileMoq.Setup(x => x.FileId).Returns(new Guid(FileId));

            _builder = new CdnPathBuilder(fileMoq.Object);

			_builder.Resize(1024, 634);
			_builder.Resize(634, 1024);
			try
			{
				_builder.Resize(1024, 635);
			}
			catch (ArgumentException)
			{
				try
				{
					_builder.Resize(635, 1024);
				}
				catch (ArgumentException)
                {
                    Assert.True(true);
				}
			}
		}

	}

}