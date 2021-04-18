using System.Collections.Generic;
using System.Linq;

namespace MalariaRecognition.Model
{
    public class Annotation
    {
        public List<BoundingBox> BoundingBoxes { get; set; }

        public string ToFileContent => string.Join("\n", BoundingBoxes.Select(x => x.ToRow).ToArray()) + "\n";

        public void FromFile(string fileContent)
        {
            BoundingBoxes = new List<BoundingBox>();

            foreach (string row in fileContent.Trim().Split('\n'))
            {
                BoundingBoxes.Add(new BoundingBox(row));
            }
        }
    }
}
