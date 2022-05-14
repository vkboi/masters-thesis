using System;
using System.Collections.Generic;
using System.Diagnostics;
using MalariaRecognition.Model;
using System.IO;
using System.ComponentModel;
using System.Linq;

namespace MalariaRecognition.Common
{
    public static class ProcessCommon
    {
        private const int MIN_MAIN_PYTHON_VERSION = 3;
        private const int MIN_MINOR_PYTHON_VERSION = 4;
        private const string REGION_PROPOSER_FILENAME = "segmentation.py";
        private const string CLASSIFIER_FILENAME = "predict.py";

        private static string GetPythonFilename()
        {
            if (string.IsNullOrEmpty(Program.PythonExecutablePath))
            {
                Program.PythonExecutablePath = Program.IsPATHPythonOutdated || !Program.IsPythonInPATH ?
                    Program.PythonExecutablePath ?? FileCommon.SelectPythonExecutable() ?? throw new ArgumentNullException() :
                    "python";
            }

            return Program.PythonExecutablePath;
        }

        public static void CheckPythonVersion()
        {
            try
            {
                using (Process pythonCheck = new Process())
                {
                    pythonCheck.StartInfo = new ProcessStartInfo {
                        FileName = "python",
                        Arguments = "--version",
                        RedirectStandardOutput = true,
                        UseShellExecute = false
                    };


                    pythonCheck.Start();
                    pythonCheck.WaitForExit();

                    StreamReader stdout = pythonCheck.StandardOutput;

                    string pythonVersion = stdout.ReadToEnd().Trim().Split(' ')[1];

                    int[] versionNumbers = pythonVersion.Split('.').Select(x => Convert.ToInt32(x)).ToArray();

                    Program.IsPATHPythonOutdated = versionNumbers[0] < MIN_MAIN_PYTHON_VERSION || versionNumbers[1] < MIN_MINOR_PYTHON_VERSION;
                    Console.WriteLine(pythonVersion);
                }

                Program.IsPythonInPATH = true;
            }
            catch (Win32Exception)
            {
                Console.WriteLine("Python not found in PATH.");
            }
        }

        private static Annotation GetAnnotationFromFile(string imagePath)
        {
            using (Process regionProposerProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = GetPythonFilename(),
                    Arguments = $"{REGION_PROPOSER_FILENAME} --image \"{imagePath}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            })
            {
                regionProposerProcess.Start();
                regionProposerProcess.WaitForExit();
                Annotation a = new Annotation();
                a.FromFile("annotations.txt");
                return a;
            }
        }

        public static List<BoundingBox> GetBoundingBoxesForImage(string imagePath)
        {
            return GetAnnotationFromFile(imagePath).BoundingBoxes;
        }

        public static List<Prediction> GetPredictions(string imagePath, Annotation annotation, bool isBinary = false)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                FileCommon.LoadImage();
                imagePath = FileCommon.LastOpenedFilePath;
            }

            annotation = annotation ?? GetAnnotationFromFile(imagePath);

            string annotationsPath = "";

            if (string.IsNullOrEmpty(annotation.FilePath))
            {
                annotationsPath = "annotations.txt";
                File.WriteAllText(annotationsPath, string.Join(";", annotation.BoundingBoxes.Select(x => x.ModelInput)) + "\n");
            }
            else
            {
                annotationsPath = imagePath.Substring(0, imagePath.Length - 3) + "txt";
            }

            using (Process p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = GetPythonFilename(),
                    Arguments = $"{CLASSIFIER_FILENAME}{((imagePath?.Length ?? 0) == 0 ? "" : $" -i {imagePath} -a {annotationsPath}")}{(isBinary ? " -b" : "")}",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            })
            {
                p.Start();
                p.WaitForExit();

                string[] preds = File.ReadAllText("preds.txt").Trim().Split(';');

                return annotation.BoundingBoxes.Zip(preds, (first, second) => new Prediction
                {
                    X = first.X,
                    Y = first.Y,
                    Width = first.Width,
                    Height = first.Height,
                    Category = isBinary ? (Category)(-1 * Convert.ToInt32(second.Split(' ')[0])) : (Category)Convert.ToInt32(second.Split(' ')[0]),
                    Confidence = Convert.ToDouble(second.Split(' ')[1].Replace(".", ","))
                }).ToList();
            }
        }
    }
}
