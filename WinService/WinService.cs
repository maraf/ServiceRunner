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
using ServiceRunner.ProcessUtil;

namespace ServiceRunner
{
    public partial class WinService : ServiceBase
    {
        private Logger log;
        private ConfigurationLoader loader;
        private Dictionary<string, ProcessWrapper> runProcessWrappers;
        private Dictionary<string, ProcessWrapper> stopProcessWrappers;

        private string rootPath;
        private string pathToConfig;
        private string taskLogFileNamePattern;

        public WinService()
        {
            InitializeComponent();

            log = new Logger(true);
            runProcessWrappers = new Dictionary<string, ProcessWrapper>();
            stopProcessWrappers = new Dictionary<string, ProcessWrapper>();
            rootPath = @"C:\Temp";
            pathToConfig = rootPath + @"\test.xml";
            taskLogFileNamePattern = @"{0}\{1:yyyy-MM-dd}_{2}.log";
        }

        protected override void OnStart(string[] args)
        {
            AddNote("Starting windows service");

            try
            {
                loader = new XmlLoader(pathToConfig);
                StartAllEnabledProfiles();
            }
            catch (WrongXmlFormat e)
            {
                AddError(String.Format("Error loading configuration file. {0}", e.Message));
            }
        }

        protected override void OnStop()
        {
            log.AddNote(this.GetType(), "Stopping windows service");

            // Spustit vsechny stop scripty,
            StopAllEnabledProfiles();
            // a pod chvili zait zijici procesy
            Thread.Sleep(5000);
            TerminateAllRunnig();
        }

        private void StartAllEnabledProfiles()
        {
            if (!Directory.Exists(rootPath + @"\profiles"))
            {
                Directory.CreateDirectory(rootPath + @"\profiles");
            }
            foreach (Profile p in loader.Configuration.Profiles)
            {
                string profile = rootPath + @"\profiles\" + p.Name.Replace(" ", "-");
                if (!p.Disabled)
                {
                    if (!Directory.Exists(profile))
                    {
                        Directory.CreateDirectory(profile);

                    }
                    foreach (SingleTask t in p.Tasks)
                    {
                        if (!t.Disabled)
                        {
                            string task = String.Format(taskLogFileNamePattern, profile, DateTime.Now, t.Name.Replace(" ", "-"));
                            StreamWriter writer;
                            if (File.Exists(task))
                            {
                                writer = new StreamWriter(File.Open(task, FileMode.Append));
                            }
                            else
                            {
                                writer = new StreamWriter(File.Open(task, FileMode.Create));
                            }
                            ProcessWrapper pw = new ProcessWrapper();
                            pw.FileName = t.RunScript;
                            pw.WorkingDirectory = t.RunScript.Substring(0, t.RunScript.LastIndexOf('\\'));
                            pw.Arguments = t.RunArguments;
                            pw.StdOutput = writer;
                            pw.ErrOutput = writer;
                            pw.TimePattern = "{0:hh:mm:ss}";
                            AddNote(String.Format("Starting task, profile: {0}, task: {1}, run script: {2}", p.Name, t.Name, t.RunScript));
                            pw.Start();
                            runProcessWrappers.Add(task, pw);
                        }
                    }
                }
            }
        }

        private void StopAllEnabledProfiles() 
        {
            foreach (Profile p in loader.Configuration.Profiles)
            {
                string profile = rootPath + @"\profiles\" + p.Name.Replace(" ", "-");
                if (!p.Disabled)
                {
                    foreach (SingleTask t in p.Tasks)
                    {
                        if (!t.Disabled)
                        {
                            string task = String.Format(taskLogFileNamePattern, profile, DateTime.Now, t.Name.Replace(" ", "-"));
                            StreamWriter writer;
                            if (File.Exists(task))
                            {
                                writer = new StreamWriter(File.Open(task, FileMode.Append));
                            }
                            else
                            {
                                writer = new StreamWriter(File.Open(task, FileMode.Create));
                            }
                            ProcessWrapper pw = new ProcessWrapper();
                            pw.FileName = t.StopScript;
                            pw.WorkingDirectory = t.StopScript.Substring(0, t.StopScript.LastIndexOf('\\'));
                            pw.Arguments = t.StopArguments;
                            pw.StdOutput = writer;
                            pw.ErrOutput = writer;
                            pw.TimePattern = "{0:hh:mm:ss}";
                            AddNote(String.Format("Stopping task, profile: {0}, task: {1}, run script: {2}", p.Name, t.Name, t.RunScript));
                            pw.Start();
                            stopProcessWrappers.Add(task, pw);
                        }
                    }
                }
            }
        }

        private void TerminateAllRunnig()
        {
            foreach (KeyValuePair<string, ProcessWrapper> pw in runProcessWrappers)
            {
                if (!pw.Value.Process.HasExited)
                {
                    pw.Value.Terminate();
                }
            }
            foreach (KeyValuePair<string, ProcessWrapper> pw in stopProcessWrappers)
            {
                if (!pw.Value.Process.HasExited)
                {
                    pw.Value.Terminate();
                }
            }
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
