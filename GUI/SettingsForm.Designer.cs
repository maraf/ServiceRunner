namespace ServiceRunner.GUI
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.lblPathToConfigFile = new System.Windows.Forms.Label();
            this.btnSaveAndClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblLogDirectory = new System.Windows.Forms.Label();
            this.btnCreateConfigFile = new System.Windows.Forms.Button();
            this.sfdCreateConfigFile = new System.Windows.Forms.SaveFileDialog();
            this.dsrLogDirectory = new ServiceRunner.GUI.Controls.DirectorySelector();
            this.fsrPathToConfigFile = new ServiceRunner.GUI.Controls.FileSelector();
            this.SuspendLayout();
            // 
            // lblPathToConfigFile
            // 
            this.lblPathToConfigFile.AutoSize = true;
            this.lblPathToConfigFile.Location = new System.Drawing.Point(12, 9);
            this.lblPathToConfigFile.Name = "lblPathToConfigFile";
            this.lblPathToConfigFile.Size = new System.Drawing.Size(120, 13);
            this.lblPathToConfigFile.TabIndex = 0;
            this.lblPathToConfigFile.Text = "Select configuration file:";
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAndClose.Location = new System.Drawing.Point(179, 136);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(103, 23);
            this.btnSaveAndClose.TabIndex = 27;
            this.btnSaveAndClose.Text = "Save and Close";
            this.btnSaveAndClose.UseVisualStyleBackColor = true;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(299, 136);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 26;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(380, 136);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 25;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblLogDirectory
            // 
            this.lblLogDirectory.AutoSize = true;
            this.lblLogDirectory.Location = new System.Drawing.Point(12, 64);
            this.lblLogDirectory.Name = "lblLogDirectory";
            this.lblLogDirectory.Size = new System.Drawing.Size(140, 13);
            this.lblLogDirectory.TabIndex = 29;
            this.lblLogDirectory.Text = "Select log directory location:";
            // 
            // btnCreateConfigFile
            // 
            this.btnCreateConfigFile.Location = new System.Drawing.Point(12, 136);
            this.btnCreateConfigFile.Name = "btnCreateConfigFile";
            this.btnCreateConfigFile.Size = new System.Drawing.Size(130, 23);
            this.btnCreateConfigFile.TabIndex = 30;
            this.btnCreateConfigFile.Text = "Create new config file";
            this.btnCreateConfigFile.UseVisualStyleBackColor = true;
            this.btnCreateConfigFile.Click += new System.EventHandler(this.btnCreateConfigFile_Click);
            // 
            // sfdCreateConfigFile
            // 
            this.sfdCreateConfigFile.CreatePrompt = true;
            this.sfdCreateConfigFile.DefaultExt = "securo";
            this.sfdCreateConfigFile.FileName = "New config";
            this.sfdCreateConfigFile.SupportMultiDottedExtensions = true;
            this.sfdCreateConfigFile.Title = "Select location of new config file";
            // 
            // dsrLogDirectory
            // 
            this.dsrLogDirectory.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dsrLogDirectory.Location = new System.Drawing.Point(12, 80);
            this.dsrLogDirectory.MinimumSize = new System.Drawing.Size(200, 31);
            this.dsrLogDirectory.Name = "dsrLogDirectory";
            this.dsrLogDirectory.Size = new System.Drawing.Size(443, 31);
            this.dsrLogDirectory.TabIndex = 28;
            this.dsrLogDirectory.Value = "";
            // 
            // fsrPathToConfigFile
            // 
            this.fsrPathToConfigFile.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fsrPathToConfigFile.Location = new System.Drawing.Point(12, 25);
            this.fsrPathToConfigFile.MaximumSize = new System.Drawing.Size(2000, 31);
            this.fsrPathToConfigFile.MinimumSize = new System.Drawing.Size(200, 31);
            this.fsrPathToConfigFile.Name = "fsrPathToConfigFile";
            this.fsrPathToConfigFile.Size = new System.Drawing.Size(443, 31);
            this.fsrPathToConfigFile.TabIndex = 1;
            this.fsrPathToConfigFile.Value = "";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 171);
            this.Controls.Add(this.btnCreateConfigFile);
            this.Controls.Add(this.lblLogDirectory);
            this.Controls.Add(this.dsrLogDirectory);
            this.Controls.Add(this.btnSaveAndClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.fsrPathToConfigFile);
            this.Controls.Add(this.lblPathToConfigFile);
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(483, 209);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPathToConfigFile;
        private ServiceRunner.GUI.Controls.FileSelector fsrPathToConfigFile;
        private System.Windows.Forms.Button btnSaveAndClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private Controls.DirectorySelector dsrLogDirectory;
        private System.Windows.Forms.Label lblLogDirectory;
        private System.Windows.Forms.Button btnCreateConfigFile;
        private System.Windows.Forms.SaveFileDialog sfdCreateConfigFile;
    }
}