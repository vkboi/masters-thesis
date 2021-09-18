using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MalariaRecognition.Model
{
    public class Annotation
    {
        public string FilePath { get; set; }
        public List<BoundingBox> BoundingBoxes { get; set; }

        public string FileContent => string.Join("\n", BoundingBoxes.Select(x => x.Row).ToArray()) + "\n";

        public string ModelInput => string.Join("\n", BoundingBoxes.Select(x => x.ModelInput).ToArray()) + "\n";

        public void FromFile(string filePath)
        {
            BoundingBoxes = new List<BoundingBox>();
            string content = File.ReadAllText(filePath);
            foreach (string row in content.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row.Trim()))
                {
                    BoundingBoxes.Add(new BoundingBox(row));
                }
            }
        }
    }
}
