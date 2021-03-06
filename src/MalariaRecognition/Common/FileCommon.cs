using System;
using System.Drawing;
using MalariaRecognition.Model;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace MalariaRecognition
{
    public static class FileCommon
    {
        private static string lastImageOpenDir;

        private static string lastAnnotationOpenDir;
        private static string lastAnnotationSaveDir;

        private static string lastClassesOpenDir;

        private const string IMAGE_LOAD_TITLE = "Kép betöltése";
        private const string IMAGE_FILTER = "Képfájlok|*.png;*.bmp;*.jpg;*.jpeg";

        private const string ANNOTATION_LOAD_TITLE = "Annotáció betöltése";
        private const string ANNOTATION_SAVE_TITLE = "Annotáció mentése";
        private const string ANNOTATION_FILTER = "Szöveges fájlok (*.txt)|*.txt|CSV fájlok (*.csv)|*.csv|Minden fájl (*.*)|*.*";

        private const string CLASSES_LOAD_TITLE = "Osztályok betöltése";
        private const string CLASSES_FITLER = "Szöveges fájlok (*.txt)|*.txt";

        private const string PYTHON_OPEN_TITLE = "Python interpreter kiválasztása";
        private const string PYTHON_FILTER = "Futtatható fájlok (*.exe)|*.exe";

        private static OpenFileDialog ofd;
        private static SaveFileDialog sfd;

        public static string LastOpenedFilePath => ofd.FileName;

        public static Image LoadImage()
        {
            ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = IMAGE_FILTER,
                Title = IMAGE_LOAD_TITLE,
                InitialDirectory = lastImageOpenDir ?? Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                lastImageOpenDir = Path.GetDirectoryName(ofd.FileName);
                return Image.FromFile(ofd.FileName);
            } 
            else
            {
                return null;
            }
        }

        public static Annotation LoadAnnotation(out string path)
        {
            ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = ANNOTATION_FILTER,
                Title = ANNOTATION_LOAD_TITLE,
                InitialDirectory = lastAnnotationOpenDir ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            path = "";

            Annotation annotation = new Annotation();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                lastAnnotationOpenDir = Path.GetDirectoryName(ofd.FileName);
                path = ofd.FileName;
                annotation.FromFile(ofd.FileName);
            }                

            return (annotation?.BoundingBoxes?.Count ?? 0) > 0 ? annotation : null;
        }

        public static void SaveAnnotation(Annotation annotation, ref string path)
        {
            sfd = new SaveFileDialog
            {
                Filter = ANNOTATION_FILTER,
                Title = ANNOTATION_SAVE_TITLE,
                InitialDirectory = lastAnnotationSaveDir ?? Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                lastAnnotationSaveDir = Path.GetDirectoryName(sfd.FileName);
                path = sfd.FileName;
                File.WriteAllText(sfd.FileName, annotation.FileContent);
            }
        }

        public static void SaveAnnotationSilent(Annotation annotation, string path)
        {
            File.WriteAllText(path, annotation.FileContent);
        }

        public static List<string> LoadClasses()
        {
            ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = ANNOTATION_FILTER,
                Title = ANNOTATION_SAVE_TITLE,
                InitialDirectory = lastClassesOpenDir ?? Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                lastClassesOpenDir = Path.GetDirectoryName(ofd.FileName);
                return new List<string>(File.ReadAllText(ofd.FileName).Trim().Split('\n'));
            }

            return new List<string>();
        }

        public static string SelectPythonExecutable()
        {
            MessageBox.Show("Kérlek válaszd ki a használni kívánt Python interpretert!", "Python", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            ofd = new OpenFileDialog
            {
                Filter = PYTHON_FILTER,
                Title = PYTHON_OPEN_TITLE,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            };

            return ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : null;
        }
    }
}
