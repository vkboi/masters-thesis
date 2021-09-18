using System;

namespace MalariaRecognition.Model
{
    public class Prediction : BoundingBox
    {
        public double Confidence { get; set; }

        public Prediction() { }

        public Prediction(string content) : this(content.Trim().Split(' ')) { }

        public Prediction(string[] contentParts) 
        {
            X = Convert.ToInt32(contentParts[1]);
            Y = Convert.ToInt32(contentParts[2]);
            Width = Convert.ToInt32(contentParts[3]);
            Height = Convert.ToInt32(contentParts[4].Trim());
            if (contentParts[0] != "-1")
            {
                Category = (Category)Convert.ToInt32(contentParts[0]);
            }
            Confidence = Convert.ToDouble(contentParts[1]);
        }
    }
}
