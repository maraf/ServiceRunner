using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ServiceRunner.Util
{

    public class Logger : IDisposable
    {
        public enum Level { ERROR, WARNING, NOTE };

        private string fileNamePattern = @"C:\Temp\log\{0:yyyy-MM-dd}.log";
        private string timePattern = "{0:hh:mm:ss}";
        private bool openCloseMode = false;

        private StreamWriter writer = null;

        public Logger(bool openCloseMode)
        {
            this.openCloseMode = openCloseMode;
            if (!openCloseMode)
            {
                Open();
            }
        }

        public void Dispose()
        {
            Close();
        }

        private void Open()
        {
            string path = String.Format(fileNamePattern, DateTime.Now);
            if (File.Exists(path))
            {
                writer = new StreamWriter(File.Open(path, FileMode.Append));
            }
            else
            {
                writer = new StreamWriter(File.Open(path, FileMode.Create));
            }
        }

        private void Close()
        {
            if (writer != null)
            {
                writer.Close();
            }
        }

        public void Log(Type type, Level level, string message)
        {
            if (openCloseMode) { Open(); }
            writer.WriteLine(String.Format("{0} {1}\t[{2}]\t{3}", String.Format(timePattern, DateTime.Now), LevelToString(level), type.FullName, message));
            if (openCloseMode) { Close(); }
        }

        private string LevelToString(Level l) {
            if(l.Equals(Level.ERROR))
            {
                return "ERROR";
            }
            else if (l.Equals(Level.WARNING))
            {
                return "WARN";
            }
            else if (l.Equals(Level.NOTE))
            {
                return "NOTE";
            }
            else
            {
                return "";
            }
        }

        public void AddError(Type type, string message)
        {
            this.Log(type, Level.ERROR, message);
        }

        public void AddWarn(Type type, string message)
        {
            this.Log(type, Level.WARNING, message);
        }

        public void AddNote(Type type, string message)
        {
            this.Log(type, Level.NOTE, message);
        }
    }
}
