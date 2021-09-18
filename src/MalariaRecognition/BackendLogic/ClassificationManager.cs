using MalariaRecognition.Common;
using MalariaRecognition.Model;
using System.Collections.Generic;

namespace MalariaRecognition.BackendLogic
{
    public class ClassificationManager
    {

        public List<Prediction> GetPredictions(string imagePath, Annotation annotation)
        {
            return ProcessCommon.GetPredictions(imagePath, annotation);
        }
    }
}
