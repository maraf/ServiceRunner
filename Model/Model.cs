using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ServiceRunner.Model
{
    /// <summary>
    /// Vyjimka hazena pri nacitaci konfirugacniho souboru se spatnou strukturou
    /// </summary>
    public class WrongXmlFormat : Exception
    {
        public WrongXmlFormat(string message)
            : base(message)
        {

        }
    }

    /// <summary>
    /// Interface slouzici k ukladani konfigurace do xml souboru.
    /// TODO: Jak se bude prochazet a ukladat xml soubor? Neco jako DOM??
    /// </summary>
    public interface SerializableToXml
    {
        /// <summary>
        /// Metoda volana pri ukladani polozky do xml
        /// </summary>
        /// <returns>XML</returns>
        XmlElement SaveToXml(XmlDocument doc);

        /// <summary>
        /// Metoda volana pri nacitani xml
        /// </summary>
        /// <param name="xml"></param>
        void LoadFromXml(XmlElement root);
    }

    /// <summary>
    /// Reprezentuje jednotlivou spoustenou ulohu.
    /// </summary>
    public class SingleTask : SerializableToXml
    {
        /// <summary>
        /// Poradi spusteni
        /// </summary>
        private int order = -1;
        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        /// <summary>
        /// Jmeno ulohy
        /// </summary>
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Cesta k spoustecimu programu
        /// </summary>
        private string runScript;
        public string RunScript
        {
            get { return runScript; }
            set { runScript = value; }
        }

        /// <summary>
        /// Argumentu spousteciho programu
        /// </summary>
        private string runArguments;
        public string RunArguments
        {
            get { return runArguments; }
            set { runArguments = value; }
        }

        /// <summary>
        /// Cesta k ukoncovacimu programu
        /// </summary>
        private string stopScript;
        public string StopScript
        {
            get { return stopScript; }
            set { stopScript = value; }
        }

        /// <summary>
        /// Argumenty ukoncovaci programu
        /// </summary>
        private string stopArguments;
        public string StopArguments
        {
            get { return stopArguments; }
            set { stopArguments = value; }
        }

        /// <summary>
        /// Udava jestli je uloha zakazana
        /// </summary>
        private bool disabled;
        public bool Disabled
        {
            get { return disabled; }
            set { disabled = value; }
        }

        /// <summary>
        /// Vytvori novou ulohu, te je pote potreba nastavit poradi(Order),
        /// jmeno(Name), jmeno spousteneho programu(RunScript), jmeno programu pro ukonceni(StopScript)
        /// a jejich argumenty(RunArguments & StopArguments).
        /// </summary>
        public SingleTask() { }

        #region SerializableToXml Members

        public XmlElement SaveToXml(XmlDocument doc)
        {
            XmlElement task = doc.CreateElement("task");
            task.SetAttribute("order", Order.ToString());
            task.SetAttribute("name", Name);
            task.SetAttribute("runScript", RunScript);
            task.SetAttribute("runArguments", RunArguments);
            task.SetAttribute("stopScript", StopScript);
            task.SetAttribute("stopArguments", StopArguments);
            task.SetAttribute("disabled", Disabled ? "true" : "false");

            return task;
        }

        public void LoadFromXml(XmlElement root)
        {
            if (root.Name.Equals("task"))
            {
                if (!root.HasAttribute("name") || !root.HasAttribute("runScript") || !root.HasAttribute("stopScript"))
                {
                    throw new WrongXmlFormat("SingleTask must have defined name, runScript and stopScript!");
                }
                else
                {
                    Int32.TryParse(root.GetAttribute("order"), out this.order);
                    this.Name = root.GetAttribute("name");
                    this.RunScript = root.GetAttribute("runScript");
                    this.StopScript = root.GetAttribute("stopScript");
                    this.RunArguments = root.GetAttribute("runArguments");
                    this.StopArguments = root.GetAttribute("stopArguments");
                    this.Disabled = root.GetAttribute("disabled") == "true" ? true : false;
                }
            }
            else
            {
                throw new WrongXmlFormat("SingleTask can read only single task element!");
            }
        }

        #endregion
    }

    /// <summary>
    /// Reprezentuje jednotlivy spousteny profil.
    /// </summary>
    public class Profile : SerializableToXml
    {
        /// <summary>
        /// Poradi profilu (?)
        /// </summary>
        private int order = -1;
        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        /// <summary>
        /// Nazev profilu
        /// </summary>
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Ulohy profilu
        /// </summary>
        private List<SingleTask> tasks = new List<SingleTask>();
        public List<SingleTask> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }

        /// <summary>
        /// Udava jestli je profil zakazan
        /// </summary>
        private bool disabled;
        public bool Disabled
        {
            get { return disabled; }
            set { disabled = value; }
        }

        /// <summary>
        /// Vytvori novy profil, tomu je potreba nastavit jmeno(Name) a jednotlive ulohy(Tasks)
        /// </summary>
        public Profile() { }

        #region SerializableToXml Members

        public XmlElement SaveToXml(XmlDocument doc)
        {
            XmlElement profile = doc.CreateElement("profile");
            profile.SetAttribute("order", String.Format("{0}", Order));
            profile.SetAttribute("name", Name);
            profile.SetAttribute("disabled", Disabled ? "true" : "false");
            foreach (SingleTask st in tasks)
            {
                profile.AppendChild(st.SaveToXml(doc));
            }
            return profile;
        }

        public void LoadFromXml(XmlElement root)
        {
            if (root.Name.Equals("profile"))
            {
                if (root.HasAttribute("name"))
                {
                    Int32.TryParse(root.GetAttribute("order"), out this.order);
                    this.Name = root.GetAttribute("name");
                    this.Disabled = root.GetAttribute("disabled") == "true" ? true : false;
                    this.Tasks = new List<SingleTask>();
                    foreach (XmlElement xe in root.GetElementsByTagName("task"))
                    {
                        SingleTask st = new SingleTask();
                        st.LoadFromXml(xe);
                        this.Tasks.Add(st);
                    }
                }
                else
                {
                    throw new WrongXmlFormat("Profile must have defined name!");
                }
            }
            else
            {
                throw new WrongXmlFormat("Profile can read only profile element!");
            }
        }

        #endregion
    }

    /// <summary>
    /// Reprezentuje cely konfiguracni soubor.
    /// </summary>
    public class Configuration : SerializableToXml
    {
        /// <summary>
        /// Cesta ke konfiguracnimu souboru
        /// </summary>
        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        /// <summary>
        /// Profily
        /// </summary>
        private List<Profile> profiles = new List<Profile>();
        public List<Profile> Profiles
        {
            get { return profiles; }
            set { profiles = value; }
        }

        /// <summary>
        /// Vytvori novou konfiguraci, te je pote potreba nastavit cestu k souboru(Path) a profily(Profiles)
        /// </summary>
        public Configuration() { }

        #region SerializableToXml Members

        public XmlElement SaveToXml(XmlDocument doc)
        {
            XmlElement configuration = doc.CreateElement("configuration");
            foreach (Profile prof in profiles)
            {
                configuration.AppendChild(prof.SaveToXml(doc));
            }
            return configuration;
        }

        public void LoadFromXml(XmlElement root)
        {
            if(root.Name.Equals("configuration"))
            {
                this.Profiles = new List<Profile>();
                foreach (XmlElement xe in root.GetElementsByTagName("profile"))
                {
                    Profile p = new Profile();
                    p.LoadFromXml(xe);
                    this.Profiles.Add(p);
                }
            }
            else 
            {
                throw new WrongXmlFormat("Configuration can read only configuration element!");
            }
        }

        #endregion
    }
}
