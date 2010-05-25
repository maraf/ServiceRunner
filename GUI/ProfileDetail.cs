using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ServiceRunner.Model;
using ServiceRunner.Util;
using ServiceRunner.GUI.Controls;

namespace ServiceRunner.GUI
{
    /// <summary>
    /// Trida reprezentujici formular pro editaci profilu.
    /// </summary>
    public partial class ProfileDetail : Form
    {
        private Profile profile;
        public Profile Profile
        {
            get { return profile; }
            set { profile = value; }
        }

        public event DetailButtonHandler SaveButtonClicked;

        public event DetailButtonHandler SaveAndCloseButtonClicked;

        public event DetailButtonHandler CloseButtonClicked;

        public ProfileDetail(Profile profile)
        {
            InitializeComponent();

            Profile = profile;

            SetUIComponentsValues();
        }

        public void SetUIComponentsValues()
        {
            tbxName.Text = Profile.Name;
            cbxDisabled.Checked = Profile.Disabled;
        }

        public void GetUIComponentsValues()
        {
            Profile.Name = tbxName.Text;
            Profile.Disabled = cbxDisabled.Checked;
        }

        protected void Fire(Delegate dlg, params object[] pList)
        {
            if (dlg != null)
            {
                this.BeginInvoke(dlg, pList);
            }
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
    }
}
