using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Threading;

using ServiceRunner.Loader;
using ServiceRunner.Model;
using ServiceRunner.Util;
using System.Reflection;

namespace ServiceRunner
{
    /// <summary>
    /// Windows sluzba zajistujici spousteni procesu
    /// </summary>
    public partial class WinService : ServiceBase
    {
        private Logger log;
        private ProcessManager manager;
        private Settings settings;

        public WinService()
        {
            InitializeComponent();

            string path = "Settings.resx";
#if DEBUG
            // Osklivy zpusob jak pro spousteni z VS najit soubor Settings.resx v ouputu GUI projektu!!
            string asspath = Assembly.GetExecutingAssembly().Location;
            for (int i = 0; i < 4; i++)
            {
                asspath = asspath.Substring(0, asspath.LastIndexOf('\\'));
            }
            path = asspath + @"\GUI\bin\Debug\" + path;
#endif
            settings = new Settings(path);

            log = new Logger(settings.PathToLogDirectory, true); AddNote("Log created");
            manager = new ProcessManager(settings);
        }

        protected override void OnStart(string[] args)
        {
            AddNote("Starting windows service");

            try
            {
                manager.Start();
            }
            catch (WrongXmlFormat e)
            {
                AddError(String.Format("Error loading configuration file. {0}", e.Message));
            }
        }

        protected override void OnStop()
        {
            log.AddNote(this.GetType(), "Stopping windows service");

            manager.Stop();
        }

        #region ZJEDNODUSENE LOGOVANI
        private void AddError(string message)
        {
            log.AddError(this.GetType(), message);
        }

        private void AddWarn(string message)
        {
            log.AddWarn(this.GetType(), message);
        }

        private void AddNote(string message)
        {
            log.AddNote(this.GetType(), message);
        }
        #endregion
    }
}
