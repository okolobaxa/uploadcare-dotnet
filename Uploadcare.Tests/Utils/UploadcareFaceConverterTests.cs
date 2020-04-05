using System.IO;
using System.Text.Json;
using Uploadcare.Models;
using Xunit;

namespace Uploadcare.Tests.Utils
{
    public class UploadcareFaceConverterTests
    {
        [Fact]
        public void Deserialize()
        {
            var json = File.ReadAllText("detectedfaces.json");

            var data = JsonSerializer.Deserialize<UploadcareFaceDetection>(json);

            Assert.NotNull(data);
            Assert.NotNull(data.Faces);
            Assert.Equal(2, data.Faces.Count);

            foreach (var face in data.Faces)
            {
                Assert.NotNull(face.FaceCoordinates);
                Assert.Equal(4, face.FaceCoordinates.Length);
                Assert.NotEqual(0, face.Height);
                Assert.NotEqual(0, face.Width);
                Assert.NotEqual(0, face.Top);
                Assert.NotEqual(0, face.Left);
            }
        }
    }
}