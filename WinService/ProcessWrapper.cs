using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Threading;

using System.Diagnostics;
using System.Windows.Forms;

namespace ServiceRunner.ProcessUtil
{
    /// <summary>
    /// Obaluje proces a uklada vystup automaticky do proudu.
    /// </summary>
    public class ProcessWrapper
    {
        private Process process;
        public Process Process
        {
            get { return process; }
        }

        private StreamWriter stdOutput;
        public StreamWriter StdOutput
        {
            get { return stdOutput; }
            set { stdOutput = value; }
        }

        private StreamWriter errOutput;
        public StreamWriter ErrOutput
        {
            get { return errOutput; }
            set { errOutput = value; }
        }

        public string FileName
        {
            get { return process.StartInfo.FileName; }
            set { process.StartInfo.FileName = value; }
        }

        public string WorkingDirectory
        {
            get { return process.StartInfo.WorkingDirectory; }
            set { process.StartInfo.WorkingDirectory = value; }
        }

        public string Arguments
        {
            get { return process.StartInfo.Arguments; }
            set { process.StartInfo.Arguments = value; }
        }

        private string timePattern;
        public string TimePattern
        {
            get { return timePattern; }
            set { timePattern = value; }
        }

        public ProcessWrapper()
        {
            process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.CreateNoWindow = true;
            process.EnableRaisingEvents = true;
            process.Exited += new EventHandler(OnProcessComplete);
        }

        public void Start()
        {
            StdOutput.WriteLine(String.Format("{0}\t{1}", String.Format(TimePattern, DateTime.Now), "Starting process"));
            process.Start();
            Thread t = new Thread(new ThreadStart(InvokeReadFromOutput));
            t.Start();
        }

        private void InvokeReadFromOutput()
        {
            new MethodInvoker(ReadStdOut).BeginInvoke(null, null);
            new MethodInvoker(ReadStdErr).BeginInvoke(null, null);
        }

        public void Terminate()
        {
            if (!process.HasExited)
            {
                if (StdOutput.BaseStream != null)
                {
                    StdOutput.WriteLine(String.Format("{0}\t{1}", String.Format(TimePattern, DateTime.Now), "Killing process"));
                }
                StdOutput.Close();
                process.Kill();
            }
        }

        public void SendTo(string data)
        {
            if (!process.HasExited)
            {
                process.StandardInput.WriteLine(data);
            }
        }

        private void OnProcessComplete(object sender, EventArgs e)
        {
            StdOutput.WriteLine(String.Format("{0}\t{1}", String.Format(TimePattern, DateTime.Now), "Process finished"));
            StdOutput.Close();
        }

        protected virtual void ReadStdOut()
        {
            string str;
            while ((str = process.StandardOutput.ReadLine()) != null)
            {
                if (!"".Equals(str))
                {
                    StdOutputWrite(str);
                }
            }
        }

        protected virtual void ReadStdErr()
        {
            string str;
            while ((str = process.StandardError.ReadLine()) != null)
            {
                if (!"".Equals(str))
                {
                    ErrOutputWrite(str);
                }
            }
        }

        private void StdOutputWrite(string data)
        {
            StdOutput.WriteLine(String.Format("{0}\t{1}", String.Format(TimePattern, DateTime.Now), data));
        }

        private void ErrOutputWrite(string data)
        {
            ErrOutput.WriteLine(String.Format("{0}\t{1}", String.Format(TimePattern, DateTime.Now), data));
        }
    }
}
