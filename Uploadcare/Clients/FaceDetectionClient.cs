using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uploadcare.DTO;
using Uploadcare.Models;
using Uploadcare.Utils;

namespace Uploadcare.Clients
{
    internal class FaceDetectionClient : IFaceDetectionClient
    {
        private readonly RequestHelper _requestHelper;

        public FaceDetectionClient()
        {
            var connection = new UploadcareConnection(null, null, UploadcareAuthType.NoAuth);

            _requestHelper = new RequestHelper(connection);
        }

        public async Task<IEnumerable<UploadcareFace>> DetectFaces(string fileId)
        {
            if (string.IsNullOrEmpty(fileId))
            {
                throw new ArgumentNullException(nameof(fileId));
            }

            var url = Urls.CdnFileDetectFace(fileId);

            var result = await _requestHelper.Get<FaceDetectionData>(url, default);

            if (result.Faces != null && result.Faces.Any())
            {
                var faces = result.Faces.Select(x => new UploadcareFace(x));

                return faces;
            }

            return Array.Empty<UploadcareFace>();
        }
    }
}
