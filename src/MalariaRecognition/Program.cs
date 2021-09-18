using System;
using System.Windows.Forms;
using MalariaRecognition.Common;
using MalariaRecognition.View;

namespace MalariaRecognition
{
    public static class Program
    {
        public static bool IsPythonInPATH { get; set; }
        public static bool IsPATHPythonOutdated { get; set; }
        public static string PythonExecutablePath { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ProcessCommon.CheckPythonVersion();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainView());
        }
    }
}
