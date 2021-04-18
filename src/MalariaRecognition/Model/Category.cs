using System.Drawing;

namespace MalariaRecognition.Model
{
    public enum Category
    {
        RedBloodCell,
        Trophozoite,
        Schizont,
        Ring,
        Leukocyte,
        Gametocyte
    }

    public static class CategoryExtensions
    {
        public static string ToText(this Category c)
        {
            switch (c)
            {
                case Category.RedBloodCell: return "red blood cell";
                default: return c.ToString().ToLower();
            }
        }

        public static Color GetColor(this Category c)
        {
            switch (c)
            {
                case Category.RedBloodCell: return Color.Red;
                case Category.Trophozoite: return Color.Blue;
                case Category.Schizont: return Color.Green;
                case Category.Ring: return Color.Cyan;
                case Category.Leukocyte: return Color.Magenta;
                case Category.Gametocyte: return Color.Yellow;
                default: return Color.Black;
            }
        }
    }
}
