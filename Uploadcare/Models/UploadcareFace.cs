using System.Collections.Generic;

namespace Uploadcare.Models
{
    public class UploadcareFace
    {
        private readonly List<int> _faceCoordinates;

        internal UploadcareFace(List<int> faceCoordinates)
        {
            _faceCoordinates = faceCoordinates;
        }

        protected UploadcareFace() { }

        /// <summary>
        /// Coordinates of the upper-left corner of an area where a face was found.
        /// </summary>
        public int Left => _faceCoordinates[0];

        /// <summary>
        /// Coordinates of the upper-left corner of an area where a face was found.
        /// </summary>
        public int Top => _faceCoordinates[1];

        /// <summary>
        /// Dimensions of that area
        /// </summary>
        public int Width => _faceCoordinates[2];

        /// <summary>
        /// Dimensions of that area
        /// </summary>
        public int Height => _faceCoordinates[3];
    }
}
