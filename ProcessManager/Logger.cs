using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ServiceRunner.Util
{
    /// <summary>
    /// Jednoduchy logger
    /// TODO: Ukladani jen urcite urovne udalosti, moznost nastaveni <code>fileNamePattern</code>, <code>timePattern</code>
    /// </summary>
    public class Logger : IDisposable
    {
        /// <summary>
        /// Uroven logovane udalost
        /// </summary>
        public enum Level { ERROR, WARNING, NOTE };

        private string fileNamePattern = @"{0:yyyy-MM-dd}.log";
        private string pathToLog = "";
        private string timePattern = "{0:HH:mm:ss}";
        private bool openCloseMode = false;

        private StreamWriter writer = null;

        /// <summary>
        /// Vytvori instanci a inicializuje
        /// </summary>
        /// <param name="path">slozka, kam se ma log ukladat, samotny soubor je pak zde vytvoren pomoci fileNamePattern</param>
        /// <param name="openCloseMode">pokud je true, logger po kazdem zapisu do logu soubor zavre a ulozi</param>
        public Logger(string path, bool openCloseMode)
        {
            this.pathToLog = path;
            this.openCloseMode = openCloseMode;
            if (!openCloseMode)
            {
                Open();
            }
        }

        /// <summary>
        /// Uzavre proud souboru
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        /// <summary>
        /// Pokud soubor pro log jiz existuje, jen ho otevre, jinak vytvori
        /// </summary>
        private void Open()
        {
            string path = pathToLog + @"\" + String.Format(fileNamePattern, DateTime.Now);
            if (File.Exists(path))
            {
                writer = new StreamWriter(File.Open(path, FileMode.Append));
            }
            else
            {
                writer = new StreamWriter(File.Open(path, FileMode.Create));
            }
        }

        /// <summary>
        /// Zavre proud souboru
        /// </summary>
        private void Close()
        {
            if (writer != null)
            {
                writer.Close();
            }
        }

        /// <summary>
        /// Zapise udalost do logu
        /// </summary>
        /// <param name="type">Komponenta, ktera zapisuje</param>
        /// <param name="level">Uroven udalosti</param>
        /// <param name="message">Popis udalosti</param>
        public void Log(Type type, Level level, string message)
        {
            if (openCloseMode) { Open(); }
            writer.WriteLine(String.Format("{0} {1}\t[{2}]\t{3}", String.Format(timePattern, DateTime.Now), LevelToString(level), type.FullName, message));
            if (openCloseMode) { Close(); }
        }

        /// <summary>
        /// Pomocna metoda provede Levelu na string
        /// </summary>
        /// <param name="l">log level</param>
        /// <returns>vstupni log level jako string</returns>
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

        /// <summary>
        /// Zapise udalost urovne ERROR
        /// </summary>
        /// <param name="type">Komponenta, ktera zapisuje</param>
        /// <param name="message">Popis udalosti</param>
        public void AddError(Type type, string message)
        {
            this.Log(type, Level.ERROR, message);
        }

        /// <summary>
        /// Zapise udalost urovne WARNING
        /// </summary>
        /// <param name="type">Komponenta, ktera zapisuje</param>
        /// <param name="message">Popis udalosti</param>
        public void AddWarn(Type type, string message)
        {
            this.Log(type, Level.WARNING, message);
        }

        /// <summary>
        /// Zapise udalost urovne NOTE
        /// </summary>
        /// <param name="type">Komponenta, ktera zapisuje</param>
        /// <param name="message">Popis udalosti</param>
        public void AddNote(Type type, string message)
        {
            this.Log(type, Level.NOTE, message);
        }
    }
}
