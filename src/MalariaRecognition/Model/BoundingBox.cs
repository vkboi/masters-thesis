using System;

namespace MalariaRecognition.Model
{
    public class BoundingBox
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Category Category { get; set; }

        public string ToRow => $"{(int)Category};{X};{Y};{Width};{Height}";

        public BoundingBox() { }

        public BoundingBox(string fileRow)
        {
            string[] vals = fileRow.Trim().Split(';');

            X = Convert.ToInt32(vals[1]);
            Y = Convert.ToInt32(vals[2]);
            Width = Convert.ToInt32(vals[3]);
            Height = Convert.ToInt32(vals[4]);
            Category = (Category)Convert.ToInt32(vals[0]);
        }
    }
}
