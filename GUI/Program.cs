using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ServiceRunner.Model;

namespace ServiceRunner.GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 1 && File.Exists(args[0]))
            {
                Settings s = new Settings("Settings.resx");
                s.PathToConfigFile = args[0];
                s.SaveToResource("Settings.resx");
            }

            MainForm mf = new MainForm();
            Application.Run(mf);
        }
    }
}
