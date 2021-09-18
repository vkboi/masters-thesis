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
        private const string MIN_PYTHON_VERSION = "3.4.0";
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

                    string pythonVersion = stdout.ReadToEnd();

                    Program.IsPATHPythonOutdated = pythonVersion.Trim().Split(' ')[1].CompareTo(MIN_PYTHON_VERSION) < 0;
                    Console.WriteLine(pythonVersion);
                }

                Program.IsPythonInPATH = true;
            }
            catch (Win32Exception)
            {
                Console.WriteLine("Python not found in PATH.");
            }
        }

        public static List<BoundingBox> GetBoundingBoxesForImage(string imagePath)
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
                return a.BoundingBoxes;
            }
        }

        public static List<Prediction> GetPredictions(string imagePath, Annotation annotation)
        {
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
                    Arguments = $"{CLASSIFIER_FILENAME}{((imagePath?.Length ?? 0) == 0 ? "" : $" -i {imagePath} -a {annotationsPath}")}",
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
                    Category = (Category)Convert.ToInt32(second.Split(' ')[0]),
                    Confidence = Convert.ToDouble(second.Split(' ')[1].Replace(".", ","))
                }).ToList();
            }
        }
    }
}
