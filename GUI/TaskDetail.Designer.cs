namespace ServiceRunner.GUI
{
    partial class TaskDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.lblRunScript = new System.Windows.Forms.Label();
            this.lblStopScript = new System.Windows.Forms.Label();
            this.lblRunArguments = new System.Windows.Forms.Label();
            this.lblStopArguments = new System.Windows.Forms.Label();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.tbxRunArguments = new System.Windows.Forms.TextBox();
            this.tbxStopArguments = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSaveAndClose = new System.Windows.Forms.Button();
            this.cbxDisabled = new System.Windows.Forms.CheckBox();
            this.fsrStopScript = new WindowsGUI.FileSelector();
            this.fsrRunScript = new WindowsGUI.FileSelector();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(7, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(63, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Task name:";
            // 
            // lblRunScript
            // 
            this.lblRunScript.AutoSize = true;
            this.lblRunScript.Location = new System.Drawing.Point(7, 43);
            this.lblRunScript.Name = "lblRunScript";
            this.lblRunScript.Size = new System.Drawing.Size(90, 13);
            this.lblRunScript.TabIndex = 1;
            this.lblRunScript.Text = "Path to run script:";
            // 
            // lblStopScript
            // 
            this.lblStopScript.AutoSize = true;
            this.lblStopScript.Location = new System.Drawing.Point(7, 147);
            this.lblStopScript.Name = "lblStopScript";
            this.lblStopScript.Size = new System.Drawing.Size(95, 13);
            this.lblStopScript.TabIndex = 2;
            this.lblStopScript.Text = "Path to stop script:";
            // 
            // lblRunArguments
            // 
            this.lblRunArguments.AutoSize = true;
            this.lblRunArguments.Location = new System.Drawing.Point(7, 93);
            this.lblRunArguments.Name = "lblRunArguments";
            this.lblRunArguments.Size = new System.Drawing.Size(110, 13);
            this.lblRunArguments.TabIndex = 3;
            this.lblRunArguments.Text = "Run script arguments:";
            // 
            // lblStopArguments
            // 
            this.lblStopArguments.AutoSize = true;
            this.lblStopArguments.Location = new System.Drawing.Point(7, 197);
            this.lblStopArguments.Name = "lblStopArguments";
            this.lblStopArguments.Size = new System.Drawing.Size(112, 13);
            this.lblStopArguments.TabIndex = 4;
            this.lblStopArguments.Text = "Stop script arguments:";
            // 
            // tbxName
            // 
            this.tbxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxName.Location = new System.Drawing.Point(76, 6);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(365, 20);
            this.tbxName.TabIndex = 1;
            // 
            // tbxRunArguments
            // 
            this.tbxRunArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxRunArguments.Location = new System.Drawing.Point(10, 109);
            this.tbxRunArguments.Name = "tbxRunArguments";
            this.tbxRunArguments.Size = new System.Drawing.Size(431, 20);
            this.tbxRunArguments.TabIndex = 3;
            // 
            // tbxStopArguments
            // 
            this.tbxStopArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxStopArguments.Location = new System.Drawing.Point(10, 213);
            this.tbxStopArguments.Name = "tbxStopArguments";
            this.tbxStopArguments.Size = new System.Drawing.Size(431, 20);
            this.tbxStopArguments.TabIndex = 5;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(361, 280);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(280, 280);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAndClose.Location = new System.Drawing.Point(160, 280);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(103, 23);
            this.btnSaveAndClose.TabIndex = 7;
            this.btnSaveAndClose.Text = "Save and Close";
            this.btnSaveAndClose.UseVisualStyleBackColor = true;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // cbxDisabled
            // 
            this.cbxDisabled.AutoSize = true;
            this.cbxDisabled.Location = new System.Drawing.Point(10, 251);
            this.cbxDisabled.Name = "cbxDisabled";
            this.cbxDisabled.Size = new System.Drawing.Size(67, 17);
            this.cbxDisabled.TabIndex = 6;
            this.cbxDisabled.Text = "Disabled";
            this.cbxDisabled.UseVisualStyleBackColor = true;
            // 
            // fsrStopScript
            // 
            this.fsrStopScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fsrStopScript.Location = new System.Drawing.Point(10, 163);
            this.fsrStopScript.MaximumSize = new System.Drawing.Size(2000, 31);
            this.fsrStopScript.MinimumSize = new System.Drawing.Size(200, 31);
            this.fsrStopScript.Name = "fsrStopScript";
            this.fsrStopScript.Size = new System.Drawing.Size(431, 31);
            this.fsrStopScript.TabIndex = 4;
            this.fsrStopScript.Value = "";
            // 
            // fsrRunScript
            // 
            this.fsrRunScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fsrRunScript.Location = new System.Drawing.Point(10, 59);
            this.fsrRunScript.MaximumSize = new System.Drawing.Size(2000, 31);
            this.fsrRunScript.MinimumSize = new System.Drawing.Size(200, 31);
            this.fsrRunScript.Name = "fsrRunScript";
            this.fsrRunScript.Size = new System.Drawing.Size(431, 31);
            this.fsrRunScript.TabIndex = 2;
            this.fsrRunScript.Value = "";
            // 
            // TaskDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 315);
            this.Controls.Add(this.fsrStopScript);
            this.Controls.Add(this.fsrRunScript);
            this.Controls.Add(this.cbxDisabled);
            this.Controls.Add(this.btnSaveAndClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tbxStopArguments);
            this.Controls.Add(this.tbxRunArguments);
            this.Controls.Add(this.tbxName);
            this.Controls.Add(this.lblStopArguments);
            this.Controls.Add(this.lblRunArguments);
            this.Controls.Add(this.lblStopScript);
            this.Controls.Add(this.lblRunScript);
            this.Controls.Add(this.lblName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(456, 330);
            this.Name = "TaskDetail";
            this.Text = "TaskDetail";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblRunScript;
        private System.Windows.Forms.Label lblStopScript;
        private System.Windows.Forms.Label lblRunArguments;
        private System.Windows.Forms.Label lblStopArguments;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.TextBox tbxRunArguments;
        private System.Windows.Forms.TextBox tbxStopArguments;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSaveAndClose;
        private System.Windows.Forms.CheckBox cbxDisabled;
        private WindowsGUI.FileSelector fsrRunScript;
        private WindowsGUI.FileSelector fsrStopScript;
    }
}