﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

using ServiceRunner.Loader;
using ServiceRunner.Model;
using ServiceRunner.Util;

namespace ServiceRunner.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestLogger();
            TestProcessManager();
            //TestStringCutting();
            //TestSingleProcessWrapper();

            Console.WriteLine("Press key to exit");
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
            Console.WriteLine("Test Logger has finished");
        }

        static void TestProcessManager()
        {
            Console.WriteLine("Testing ProcessManager ...");

            ProcessManager manager = new ProcessManager();
            manager.Start();
            Console.WriteLine("Press key to run stop script");
            Console.ReadKey(true);
            Console.WriteLine("Starting stop script");
            manager.Stop();

            Console.WriteLine("Test ProcessManager has finished");
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
            Console.WriteLine("Test SingleProcessWrapper has finished");
        }
    }
}
