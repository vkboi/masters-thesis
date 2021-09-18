using System.Collections.Generic;
using MalariaRecognition.Common;
using MalariaRecognition.Model;

namespace MalariaRecognition.BackendLogic
{
    public class RegionProposerManager
    {

        public List<BoundingBox> GetBoundingBoxProposals(string imagePath)
        {
            return ProcessCommon.GetBoundingBoxesForImage(imagePath);
        }
    }
}
