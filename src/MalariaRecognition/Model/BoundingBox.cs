using System;

namespace MalariaRecognition.Model
{
    public class BoundingBox
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Category? Category { get; set; }

        public string Row => $"{(int)Category};{X};{Y};{Width};{Height}";
        public string ModelInput => $"{Y} {Y + Height} {X} {X + Width}";

        public BoundingBox() { }

        public BoundingBox(int category)
        {
            Category = (Category)category;
        }

        public BoundingBox(string fileRow)
        {
            string[] vals = fileRow.Split(new char[] { ' ' }, StringSplitOptions.None);

            X = Convert.ToInt32(vals[1]);
            Y = Convert.ToInt32(vals[2]);
            Width = Convert.ToInt32(vals[3]) - X;
            Height = Convert.ToInt32(vals[4].Trim()) - Y;
            if (vals[0] != "-1") 
            {
                Category = (Category)Convert.ToInt32(vals[0]);
            }
        }
    }
}
