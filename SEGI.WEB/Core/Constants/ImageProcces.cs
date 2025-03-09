using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEGI.Core.Constants;

namespace SEGI.Core.Constants
{
    public struct ImageProcces
    {
        private List<string> _allowedExtenstions = new List<string> { Constant.FilterFileExtentions.jpg, Constant.FilterFileExtentions.png, Constant.FilterFileExtentions.svg };
        private long _maxAllowedPosterSize = Constant.FilterFileExtentions._maxAllowedPosterSize;

        public ImageProcces(List<string> allowedExtenstions, long maxAllowedPosterSize)
        {
            _allowedExtenstions = allowedExtenstions;
            _maxAllowedPosterSize = maxAllowedPosterSize;
        }
    }
}
