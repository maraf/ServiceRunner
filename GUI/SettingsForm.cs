using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using ServiceRunner.GUI.Controls;
using ServiceRunner.Model;

namespace ServiceRunner.GUI
{
    /// <summary>
    /// Formular pro nastaveni aplikace, jako je cesta ke konfiguracnimu souboru, ...
    /// </summary>
    public partial class SettingsForm : Form
    {
        private Settings settings;
        public Settings Settings
        {
            get { return settings; }
            set
            {
                settings = value;
                SetUIComponentsValues();
            }
        }

        public event DetailButtonHandler SaveButtonClicked;

        public event DetailButtonHandler SaveAndCloseButtonClicked;

        public event DetailButtonHandler CloseButtonClicked;

        public SettingsForm(Settings settings)
        {
            InitializeComponent();

            this.settings = settings;

            SetUIComponentsValues();
        }

        public void SetUIComponentsValues()
        {
            fsrPathToConfigFile.Value = settings.PathToConfigFile;
            dsrLogDirectory.Value = settings.PathToLogDirectory;
        }

        public void GetUIComponentsValues()
        {
            settings.PathToConfigFile = fsrPathToConfigFile.Value;
            settings.PathToLogDirectory = dsrLogDirectory.Value;
        }

        private void Fire(Delegate dlg, params object[] pList)
        {
            if (dlg != null)
            {
                this.BeginInvoke(dlg, pList);
            }
        }

        private void CreateConfigFile(string path)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement configuration = doc.CreateElement("configuration");
            doc.AppendChild(configuration);
            doc.Save(path);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Fire(CloseButtonClicked, this, e);
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            GetUIComponentsValues();
            Fire(SaveAndCloseButtonClicked, this, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GetUIComponentsValues();
            Fire(SaveButtonClicked, this, e);
        }

        private void btnCreateConfigFile_Click(object sender, EventArgs e)
        {
            if (sfdCreateConfigFile.ShowDialog() == DialogResult.OK)
            {
                CreateConfigFile(sfdCreateConfigFile.FileName);
                fsrPathToConfigFile.Value = sfdCreateConfigFile.FileName;
            }
        }
    }
}
