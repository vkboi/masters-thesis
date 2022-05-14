using System;
using System.Drawing;

namespace MalariaRecognition.Model
{
    public class BoundingBox
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Category? Category { get; set; }
        public bool IsSelected { get; set; }

        public string Row => $"{(int)(Category ?? Model.Category.RedBloodCell)};{X};{Y};{Width};{Height}";
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

        public BoundingBox(Prediction prediction)
        {
            X = prediction.X;
            Y = prediction.Y;
            Width = prediction.Width;
            Height = prediction.Height;
            Category = prediction.Category;
        }

        public bool IsPointOnFrame(Point point)
        {
            //bool b1 = point.X == X && Y <= point.Y && point.Y <= Y + Height;
            //bool b2 = point.X == X + Width && Y <= point.Y && point.Y <= Y + Height;
            //bool b3 = point.Y == Y && X <= point.X && point.X <= X + Width;
            //bool b4 = point.Y == Y + Height && X <= point.X && point.X <= X + Width;
            return Math.Abs(point.X - X) <= 5 && Y <= point.Y && point.Y <= Y + Height ||
                   Math.Abs(point.X - X + Width) <= 5 && Y <= point.Y && point.Y <= Y + Height ||
                   Math.Abs(point.Y - Y) <= 5 && X <= point.X && point.X <= X + Width ||
                   Math.Abs(point.Y - Y + Height) <= 5 && X <= point.X && point.X <= X + Width;
        }
    }
}
