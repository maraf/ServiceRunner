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

namespace ServiceRunner
{
    public partial class WinService : ServiceBase
    {
        private Logger log;
        private ProcessManager manager;

        public WinService()
        {
            InitializeComponent();

            log = new Logger(true);
            manager = new ProcessManager();
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

        #region LOGOVANI
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
