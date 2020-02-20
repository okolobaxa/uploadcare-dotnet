using System.Collections.Generic;
using System.Threading.Tasks;
using Uploadcare.Models;

namespace Uploadcare.Clients
{
    public interface IFaceDetectionClient
    {
        /// <summary>
        /// Detect faces in image.
        /// </summary>
        /// <param name="fileId"> File UUID </param>
        /// <returns> List of detected faces coordinates </returns>
        Task<IEnumerable<UploadcareFace>> DetectFaces(string fileId);
    }
}