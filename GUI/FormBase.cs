using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ServiceRunner.GUI
{
    public delegate void DetailButtonHandler(object sender, EventArgs e);

    public partial class FormBase : Form
    {
        public event DetailButtonHandler SaveButtonClicked;

        public event DetailButtonHandler SaveAndCloseButtonClicked;

        public event DetailButtonHandler CloseButtonClicked;

        public FormBase()
        {
            InitializeComponent();
        }

        public virtual void SetUIComponentsValues()
        {

        }

        public virtual void GetUIComponentsValues() 
        { 
        
        }

        protected void Fire(Delegate dlg, params object[] pList)
        {
            if (dlg != null)
            {
                this.BeginInvoke(dlg, pList);
            }
        }

        protected virtual void btnClose_Click(object sender, EventArgs e)
        {
            Fire(CloseButtonClicked, this, e);
        }

        protected virtual void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            GetUIComponentsValues();
            Fire(SaveAndCloseButtonClicked, this, e);
        }

        protected virtual void btnSave_Click(object sender, EventArgs e)
        {
            GetUIComponentsValues();
            Fire(SaveButtonClicked, this, e);
        }
    }
}
