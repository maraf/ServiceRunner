using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace ServiceRunner.Loader
{
    /// <summary>
    /// Rozhrani pro nacitani konfigurace.
    /// </summary>
    public interface ConfigurationLoader
    {
        ServiceRunner.Model.Configuration Configuration
        {
            get;
            set;
        }

        /// <summary>
        /// Nacte konfiguracni soubor
        /// </summary>
        /// <param name="path">cesta ke konfiguracnimu souboru</param>
        void LoadFromSource(string path);

        /// <summary>
        /// Nacte konfiguraci ze zadaneho retezce
        /// </summary>
        /// <param name="content">retezec obsahujici konfiguraci</param>
        void LoadFromString(string content);

        void SaveToSource(string path);

        String SaveToString();
    }

    /// <summary>
    /// Hloupa (sestovaci) implementace
    /// </summary>
    public class DummyLoader : ConfigurationLoader
    {
        private ServiceRunner.Model.Configuration configuration;
        public ServiceRunner.Model.Configuration Configuration
        {
            get { return configuration; }
            set { configuration = value; }
        }

        public DummyLoader()
        {
            configuration = new ServiceRunner.Model.Configuration();
            configuration.Path = @"C:\ServiceRunner\Confs\Configuration1.seruco";

            ServiceRunner.Model.Profile p1 = new ServiceRunner.Model.Profile();
            p1.Name = "Default profile";
            p1.Order = 1;

            ServiceRunner.Model.SingleTask st1 = new ServiceRunner.Model.SingleTask();
            st1.Order = 1;
            st1.Name = "JBoss";
            st1.RunScript = @"C:\Program Files\JBoss\bin\run.bat";
            st1.StopScript = @"C:\Program Files\JBoss\bin\shutdown.bat";
            st1.StopArguments = "--shudown";
            st1.Disabled = false;
            p1.Tasks.Add(st1);

            ServiceRunner.Model.SingleTask st2 = new ServiceRunner.Model.SingleTask();
            st2.Order = 2;
            st2.Name = "Orther program";
            st2.RunScript = @"C:\run.bat";
            st2.StopScript = @"C:\stop.bat";
            st2.Disabled = false;
            p1.Tasks.Add(st2);

            configuration.Profiles.Add(p1);
        }

        public DummyLoader(string path)
        {

        }

        public void LoadFromSource(string path)
        {
            throw new NotImplementedException();
        }

        public void LoadFromString(string content)
        {
            throw new NotImplementedException();
        }

        public void SaveToSource(string path) 
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            doc.AppendChild(Configuration.SaveToXml(doc));

            doc.Save(path);
        }

        public String SaveToString()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Loader pro nacatani konfigurace z xml souboru
    /// </summary>
    public class XmlLoader : ConfigurationLoader
    {
        private ServiceRunner.Model.Configuration configuration;
        public ServiceRunner.Model.Configuration Configuration
        {
            get { return configuration; }
            set { configuration = value; }
        }

        public XmlLoader(string path)
        {
            this.LoadFromSource(path);
        }

        public void LoadFromSource(string path)
        {
            XmlDocument doc = new XmlDocument();
            StreamReader sr = new StreamReader(path);
            doc.LoadXml(sr.ReadToEnd());
            sr.Close();
            XmlElement xe = doc.DocumentElement;
            if (xe != null)
            {
                Configuration = new ServiceRunner.Model.Configuration();
                Configuration.Path = path;
                Configuration.LoadFromXml(xe);
            }
            else
            {
                throw new ServiceRunner.Model.WrongXmlFormat("Error occured in configuration file, no root element!");
            }
        }

        public void LoadFromString(string content)
        {
            throw new NotImplementedException();
        }

        public void SaveToSource(string path)
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            doc.AppendChild(Configuration.SaveToXml(doc));

            doc.Save(path);
        }

        public string SaveToString()
        {
            throw new NotImplementedException();
        }
    }
}