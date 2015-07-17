using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Uploadcare.API;
using Uploadcare.Url;

namespace Uploadcare.Test
{
    [TestClass]
	public class CdnPathBuilderTest
	{
		private const string FileId = "27c7846b-a019-4516-a5e4-de635f822161";
		private CdnPathBuilder _builder;

        [TestInitialize]
		public void SetUp()
		{
			var fileMoq = new Mock<UploadcareFile>();
            fileMoq.Setup(x => x.FileId).Returns(new Guid(FileId));

            _builder = new CdnPathBuilder(fileMoq.Object);
		}

        [TestMethod]
		public void cdnpathbuilder_fileUrl_assert()
		{
			string path = _builder.Build();
			Assert.AreEqual("/" + FileId + "/", path);
		}

        [TestMethod]
        public void cdnpathbuilder_allOperations_assert()
		{
			string path = _builder.Crop(100, 110).CropColor(120, 130, Color.Black).CropCenter(140, 150).CropCenterColor(160, 170, Color.Red).Resize(100, 110).ResizeWidth(120).ResizeHeight(130).ScaleCrop(100, 110).ScaleCropCenter(120, 130).Flip().Grayscale().Invert().Mirror().Build();
			Assert.AreEqual("/" + FileId + "/-/crop/100x110" + "/-/crop/120x130/000000" + "/-/crop/140x150/center" + "/-/crop/160x170/center/ff0000" + "/-/resize/100x110" + "/-/resize/120x" + "/-/resize/x130" + "/-/scale_crop/100x110" + "/-/scale_crop/120x130/center" + "/-/effect/flip" + "/-/effect/grayscale" + "/-/effect/invert" + "/-/effect/mirror" + "/", path);
		}

        [TestMethod]
        public void cdnpathbuilder_dimensionGuard_fail()
		{
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
                    Assert.IsTrue(true);
				}
			}
		}

        [TestMethod]
        public void cdnpathbuilder_dimensionsGuard_fail()
		{
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
                    Assert.IsTrue(true);
				}
			}
		}

	}

}