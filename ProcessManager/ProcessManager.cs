using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

using ServiceRunner.Loader;
using ServiceRunner.Model;
using ServiceRunner.Util;

namespace ServiceRunner.Util
{
    /// <summary>
    /// Obaluje nacteni a spusteni cele konfigurace.
    /// </summary>
    public class ProcessManager
    {
        private Logger log;
        private ConfigurationLoader loader;
        private Dictionary<string, ProcessWrapper> runProcessWrappers;
        private Dictionary<string, ProcessWrapper> stopProcessWrappers;

        /// <summary>
        /// Absolutni cesta ke slozce, kde se bude hledat konfiguracni soubor a kam se budou ukladat logy
        /// </summary>
        private string rootPath;
        public string RootPath
        {
            get { return rootPath; }
            set { rootPath = value; }
        }

        /// <summary>
        /// Relativni cesta kde se ma hledat konfiguracni soubor vzhledem k <code>rootPath</code>
        /// </summary>
        private string pathToConfig;
        public string PathToConfig
        {
            get { return pathToConfig; }
            set { pathToConfig = value; }
        }

        /// <summary>
        /// Vzor nazvu logu pro ulohu
        /// </summary>
        private string taskLogFileNamePattern;
        public string TaskLogFileNamePattern
        {
            get { return taskLogFileNamePattern; }
            set { taskLogFileNamePattern = value; }
        }

        /// <summary>
        /// Vzor formatu casu k logu
        /// </summary>
        private string timePattern;
        public string TimePattern
        {
            get { return timePattern; }
            set { timePattern = value; }
        }

        /// <summary>
        /// Konfigurace
        /// </summary>
        private Settings settings;
        public Settings Settings
        {
            get { return settings; }
        }

        public ProcessManager(Settings settings)
        {
            log = new Logger(settings.PathToLogDirectory ,true);
            runProcessWrappers = new Dictionary<string, ProcessWrapper>();
            stopProcessWrappers = new Dictionary<string, ProcessWrapper>();
            this.settings = settings;

            rootPath = settings.PathToLogDirectory;
            pathToConfig = settings.PathToConfigFile;
            taskLogFileNamePattern = @"{0}\{1:yyyy-MM-dd}_{2}.log";
            timePattern = "{0:HH:mm:ss,ffff}";
        }

        /// <summary>
        /// Nacte na spusti vsechny sluzby
        /// </summary>
        public void Start() 
        {
            loader = new XmlLoader(pathToConfig);
            StartAllEnabledProfiles();
        }

        /// <summary>
        /// Zastavi vsechny sluzby
        /// </summary>
        public void Stop()
        {
            StopAllEnabledProfiles();
            Thread.Sleep(12000);
            TerminateAllRunnig();
        }

        /// <summary>
        /// Realizuje spusteni vsech spoustecich procesu
        /// </summary>
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
                            pw.TimePattern = TimePattern;
                            AddNote(String.Format("Starting task, profile: {0}, task: {1}, run script: {2}", p.Name, t.Name, t.RunScript));
                            pw.Start();
                            runProcessWrappers.Add(task, pw);
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Realizuje spusteni vsech ukoncovacich procesu
        /// </summary>
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
                            ProcessWrapper pw = new ProcessWrapper();
                            pw.FileName = t.StopScript;
                            pw.WorkingDirectory = t.StopScript.Substring(0, t.StopScript.LastIndexOf('\\'));
                            pw.Arguments = t.StopArguments;
                            ProcessWrapper pwt;
                            runProcessWrappers.TryGetValue(task, out pwt);
                            pw.StdOutput = pwt.StdOutput;
                            pw.ErrOutput = pwt.ErrOutput;
                            pw.TimePattern = TimePattern;
                            AddNote(String.Format("Stopping task, profile: {0}, task: {1}, run script: {2}", p.Name, t.Name, t.RunScript));
                            pw.Start();
                            stopProcessWrappers.Add(task, pw);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// "Zabije" vsechny procesy z <code>runProcessWrappers</code> a <code>stopProcessWrappers</code>
        /// </summary>
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
