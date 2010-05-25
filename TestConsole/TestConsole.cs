using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using ServiceRunner.Loader;
using ServiceRunner.Model;
using ServiceRunner.Util;
using System.Reflection;

namespace ServiceRunner.TestConsole
{
    class Program
    {
        enum InstallType { Install, Uninstall, Both, ProjectInstaller };

        static void Main(string[] args)
        {
            ConsoleKey k;
            do
            {
                Console.WriteLine("TestConsole, select from menu:");
                Console.WriteLine(" 1)  Run TestLogger");
                Console.WriteLine(" 2)  Run TestProcessManager");
                Console.WriteLine(" 3)  Run TestStringCutting");
                Console.WriteLine(" 4)  Run TestSingleProcessWrapper");
                Console.WriteLine(" 5)  Run ServiceInstaller");
                Console.WriteLine(" 6)  Run TestLocation");
                Console.WriteLine(" x)  To exit");

                k = Console.ReadKey(true).Key;
                Console.WriteLine();

                #region PODMINKY PRO VYBER
                if (k.Equals(ConsoleKey.D1))
                {
                    TestLogger();
                }
                else if (k.Equals(ConsoleKey.D2))
                {
                    TestProcessManager();
                }
                else if (k.Equals(ConsoleKey.D3))
                {
                    TestStringCutting();
                }
                else if (k.Equals(ConsoleKey.D4))
                {
                    TestSingleProcessWrapper();
                }
                else if (k.Equals(ConsoleKey.D5))
                {
                    Console.WriteLine("Select action:");
                    Console.WriteLine(" 1) Install");
                    Console.WriteLine(" 2) Uninstall");

                    ConsoleKey k2 = Console.ReadKey(true).Key;
                    Console.WriteLine();

                    if (k2.Equals(ConsoleKey.D1))
                    {
                        ServiceInstaller(InstallType.Install);
                    }
                    else if (k2.Equals(ConsoleKey.D2))
                    {
                        ServiceInstaller(InstallType.Uninstall);
                    }
                }
                else if (k.Equals(ConsoleKey.D6))
                {
                    TestLocation();
                }
                #endregion

                Console.WriteLine();
            } while (!k.Equals(ConsoleKey.X));
        }

        /// <summary>
        /// Jednoduchy test Loggeru
        /// </summary>
        static void TestLogger()
        {
            Console.WriteLine("Testing Logger ...");
            Logger log = new Logger(@"C:\Temp\Log", false);
            log.AddError(typeof(Program), "Fatal error.");
            log.AddWarn(typeof(Program), "Some warning.");
            log.AddNote(typeof(Program), "Not important note.");
            log.Dispose();
            Console.WriteLine("Test Logger has finished");
        }

        /// <summary>
        /// Testuje cely ProcessManager proti konfiguraci z Settings.resx
        /// </summary>
        static void TestProcessManager()
        {
            Console.WriteLine("Testing ProcessManager ...");

            // Pouzit settings.resx nebo nejak upravit!
            Settings s = new Settings(@"..\..\..\GUI\bin\Debug\Settings.resx");
            ProcessManager manager = new ProcessManager(s);
            manager.Start();
            Console.WriteLine("Press key to run stop script");
            Console.ReadKey(true);
            Console.WriteLine("Starting stop script");
            manager.Stop();

            Console.WriteLine("Test ProcessManager has finished");
        }

        /// <summary>
        /// Test parsovani stringu s cestou k souboru
        /// </summary>
        static void TestStringCutting()
        {
            Console.WriteLine("Testing string cutting ...");
            string path = @"C:\Temp\Run.bat";

            Console.WriteLine(path.Substring(0, path.LastIndexOf('\\')));
        }

        /// <summary>
        /// Testuje jednotlivy ProcessWrapper
        /// </summary>
        static void TestSingleProcessWrapper()
        {
            Console.WriteLine("Starting processWrapper ...");
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

        /// <summary>
        /// Umoznuje nainstalovat/odinstalovat WinService pro ServiceRunner
        /// TODO: Vyresit cestu!!
        /// </summary>
        /// <param name="type"></param>
        static void ServiceInstaller(InstallType type)
        {
            Console.WriteLine("Starting ServiceInstaller ...");

            // Osklivy zpusob jak najit vystup projektu WinServicer v absolutni ceste pro instalaci Win Service
            string asspath = Assembly.GetExecutingAssembly().Location;
            for (int i = 0; i < 4; i++)
            {
                asspath = asspath.Substring(0, asspath.LastIndexOf('\\'));
            }
            string svcPath = asspath + @"\WinService\bin\Debug\ServicerRunner.exe";
            Console.WriteLine(svcPath);
            //string svcPath = @"D:\Projects\VS10\Projects\ServiceRunner2\WinService\bin\Debug\ServiceRunner.exe";
            string svcName = "ServiceRunner";
            string svcDispName = "ServiceRunner";
            ServiceInstaller c = new ServiceInstaller();

            if (type.Equals(InstallType.Uninstall) || type.Equals(InstallType.Both))
            {
                Console.WriteLine("Uninstall service ...");
                c.UninstallService(svcName);
                Console.WriteLine("Service has been uninstalled");
            }

            if (type.Equals(InstallType.Both))
            {
                Console.ReadKey(true);
            }

            if (type.Equals(InstallType.Install) || type.Equals(InstallType.Both))
            {
                Console.WriteLine("Installing service ...");
                c = new ServiceInstaller();
                c.InstallService(svcPath, svcName, svcDispName);
                Console.WriteLine("Service has been installed");
            }

            if (type.Equals(InstallType.ProjectInstaller))
            {
                Console.WriteLine("Installing service using ProjectInstaller");
                new ProjectInstaller();
            }

            Console.WriteLine("Test ServiceInstaller has finished");
        }

        /// <summary>
        /// Zjisteni umisteni
        /// </summary>
        static void TestLocation()
        {
            Console.WriteLine("Location from Assembly: " + Assembly.GetExecutingAssembly().Location);
        }
    }
}
