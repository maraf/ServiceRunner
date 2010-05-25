using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ServiceRunner.Model;
using ServiceRunner.GUI.Controls;

namespace ServiceRunner.GUI
{
    /// <summary>
    /// Trida reprezentujici formular pro editaci tasku.
    /// </summary>
    public partial class TaskDetail : Form
    {
        private SingleTask task;
        public SingleTask Task
        {
            get { return task; }
            set 
            {
                task = value;
                SetUIComponentsValues();
            }
        }

        public event DetailButtonHandler SaveButtonClicked;

        public event DetailButtonHandler SaveAndCloseButtonClicked;

        public event DetailButtonHandler CloseButtonClicked;

        public TaskDetail(SingleTask task)
        {
            InitializeComponent();

            Task = task;

            SetUIComponentsValues();
        }

        public void SetUIComponentsValues()
        {
            tbxName.Text = Task.Name;
            fsrRunScript.Value = Task.RunScript;
            tbxRunArguments.Text = Task.RunArguments;
            fsrStopScript.Value = Task.StopScript;
            tbxStopArguments.Text = Task.StopArguments;
            cbxDisabled.Checked = Task.Disabled;
        }

        public void GetUIComponentsValues()
        {
            Task.Name = tbxName.Text;
            Task.RunScript = fsrRunScript.Value;
            Task.RunArguments = tbxRunArguments.Text;
            Task.StopScript = fsrStopScript.Value;
            Task.StopArguments = tbxStopArguments.Text;
            Task.Disabled = cbxDisabled.Checked;
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
