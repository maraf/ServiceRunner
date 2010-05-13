using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

using ServiceRunner.Loader;
using ServiceRunner.Model;
using ServiceRunner.ProcessUtil;
using ServiceRunner.Util;

namespace ServiceRunner.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestLogger();
            //TestProcessWrapper();
            //TestStringCutting();
            TestSingleProcessWrapper();

            Console.ReadKey(true);
        }

        static void TestLogger()
        {
            Console.WriteLine("Testing Logger ...");
            Logger log = new Logger(false);
            log.AddError(typeof(Program), "Fatal error.");
            log.AddWarn(typeof(Program), "Some warning.");
            log.AddNote(typeof(Program), "Not important note.");
            log.Dispose();
            Console.WriteLine("Logger OK");
        }

        static void TestProcessWrapper()
        {
            Console.WriteLine("Testing ProcessWrapper ...");

            ConfigurationLoader loader;
            Dictionary<string, ProcessWrapper> processWrappers;

            string rootPath;
            string pathToConfig;
            string taskLogFileNamePattern;

            Logger log = new Logger(true);
            processWrappers = new Dictionary<string, ProcessWrapper>();
            rootPath = @"C:\Temp";
            pathToConfig = rootPath + @"\test.xml";
            taskLogFileNamePattern = @"{0}\{1:yyyy-MM-dd}_{2}.log";

            loader = new XmlLoader(pathToConfig);

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
                    log.AddNote(typeof(Program), String.Format("Tasks.Count = {0}", p.Tasks.Count));
                    foreach (SingleTask t in p.Tasks)
                    {
                        if (!t.Disabled)
                        {
                            string task = String.Format(taskLogFileNamePattern, profile, DateTime.Now, t.Name.Replace(" ", "-"));
                            log.AddNote(typeof(Program), task);
                            StreamWriter writer;
                            if (File.Exists(task))
                            {
                                log.AddNote(typeof(Program), "Log exists");
                                writer = new StreamWriter(File.Open(task, FileMode.Append));
                            }
                            else
                            {
                                log.AddNote(typeof(Program), "Log doesn't exist");
                                writer = new StreamWriter(File.Open(task, FileMode.Create));
                            }
                            log.AddNote(typeof(Program), String.Format("{0}", writer.ToString()));
                            log.AddNote(typeof(Program), "Creating Wrapper");
                            ProcessWrapper pw = new ProcessWrapper();
                            log.AddNote(typeof(Program), "Wrapper created");
                            pw.FileName = t.RunScript;
                            pw.WorkingDirectory = profile;
                            pw.Arguments = t.RunArguments;
                            pw.StdOutput = writer;
                            pw.ErrOutput = writer;
                            pw.TimePattern = "{0:hh:mm:ss}";
                            log.AddNote(typeof(Program), "Starting process");
                            pw.Start();
                            log.AddNote(typeof(Program), "Adding to dictionary");
                            processWrappers.Add(task, pw);
                        }
                    }
                }
            }

            Console.WriteLine("ProcessWrapper OK");
        }

        static void TestStringCutting()
        {
            Console.WriteLine("Testing string cutting");
            string path = @"C:\Temp\Run.bat";

            Console.WriteLine(path.Substring(0, path.LastIndexOf('\\')));
        }

        static void TestSingleProcessWrapper()
        {
            Console.WriteLine("Starting processWrapper");
            ProcessWrapper pw = new ProcessWrapper();
            pw.FileName = @"C:\Temp\Server.exe";
            pw.Arguments = "3030";
            pw.WorkingDirectory = @"C:\Temp";
            StreamWriter sw = new StreamWriter(File.Create(@"C:\Temp\test.log"));
            sw.AutoFlush = true;
            pw.StdOutput = sw;
            pw.ErrOutput = sw;
            pw.TimePattern = "{0:hh:mm:ss}";

            pw.Start();
            Console.WriteLine("Process is running, press any key to send \"exit\"");
            Console.ReadKey(true);
            Console.WriteLine("Process state: {0}", pw.Process.HasExited ? "exited" : "running");
            pw.SendTo("exit");
            Console.WriteLine("\"Exit\" has been sent");
            Thread.Sleep(1000);
            pw.Terminate();
        }
    }
}
