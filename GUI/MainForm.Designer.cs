﻿namespace ServiceRunner.GUI
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.WinServiceController = new System.ServiceProcess.ServiceController();
            this.tsMainMenu = new System.Windows.Forms.ToolStrip();
            this.tslWinService = new System.Windows.Forms.ToolStripLabel();
            this.tslWinServiceStatus = new System.Windows.Forms.ToolStripLabel();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbStartWinService = new System.Windows.Forms.ToolStripButton();
            this.tsbStopWinService = new System.Windows.Forms.ToolStripButton();
            this.tsbRestart = new System.Windows.Forms.ToolStripButton();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSaveConfiguration = new System.Windows.Forms.ToolStripButton();
            this.tsbHomepage = new System.Windows.Forms.ToolStripButton();
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.tsslTestStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlNewTask = new System.Windows.Forms.Panel();
            this.lblDeleteTasks = new System.Windows.Forms.Label();
            this.btnDeleteTasks = new System.Windows.Forms.Button();
            this.lblTasksCount = new System.Windows.Forms.Label();
            this.btnNewTask = new System.Windows.Forms.Button();
            this.lblNewTask = new System.Windows.Forms.Label();
            this.pnlConfigurations = new System.Windows.Forms.Panel();
            this.btnDeleteProfile = new System.Windows.Forms.Button();
            this.btnCreateProfile = new System.Windows.Forms.Button();
            this.btnEditProfile = new System.Windows.Forms.Button();
            this.cobProfiles = new System.Windows.Forms.ComboBox();
            this.lblProfiles = new System.Windows.Forms.Label();
            this.lvTasks = new System.Windows.Forms.ListView();
            this.clhOrder = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.clhName = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.clhRunScript = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.clhStopScript = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.clhStatus = ((System.Windows.Forms.ColumnHeader) (new System.Windows.Forms.ColumnHeader()));
            this.cmsListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.tsMainMenu.SuspendLayout();
            this.ssStatus.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlNewTask.SuspendLayout();
            this.pnlConfigurations.SuspendLayout();
            this.cmsListView.SuspendLayout();
            this.SuspendLayout();
            // 
            // WinServiceController
            // 
            this.WinServiceController.MachineName = "7onlenovo";
            this.WinServiceController.ServiceName = "ServiceRunner";
            // 
            // tsMainMenu
            // 
            this.tsMainMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslWinService,
            this.tslWinServiceStatus,
            this.tssSeparator1,
            this.tsbStartWinService,
            this.tsbStopWinService,
            this.tsbRestart,
            this.tsbSettings,
            this.tsbAbout,
            this.tssSeparator2,
            this.tsbSaveConfiguration,
            this.tsbHomepage});
            this.tsMainMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMainMenu.Name = "tsMainMenu";
            this.tsMainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsMainMenu.Size = new System.Drawing.Size(660, 25);
            this.tsMainMenu.TabIndex = 1;
            // 
            // tslWinService
            // 
            this.tslWinService.Name = "tslWinService";
            this.tslWinService.Size = new System.Drawing.Size(82, 22);
            this.tslWinService.Text = "Service Status:";
            // 
            // tslWinServiceStatus
            // 
            this.tslWinServiceStatus.AutoSize = false;
            this.tslWinServiceStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (238)));
            this.tslWinServiceStatus.Name = "tslWinServiceStatus";
            this.tslWinServiceStatus.Size = new System.Drawing.Size(70, 22);
            this.tslWinServiceStatus.Text = "Stopped";
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbStartWinService
            // 
            this.tsbStartWinService.Image = global::WindowsGUI.Properties.Resources.StartIcon;
            this.tsbStartWinService.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStartWinService.Name = "tsbStartWinService";
            this.tsbStartWinService.Size = new System.Drawing.Size(51, 22);
            this.tsbStartWinService.Text = "Start";
            this.tsbStartWinService.Click += new System.EventHandler(this.tsbStartWinService_Click);
            // 
            // tsbStopWinService
            // 
            this.tsbStopWinService.Enabled = false;
            this.tsbStopWinService.Image = global::WindowsGUI.Properties.Resources.StopIcon;
            this.tsbStopWinService.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStopWinService.Name = "tsbStopWinService";
            this.tsbStopWinService.Size = new System.Drawing.Size(51, 22);
            this.tsbStopWinService.Text = "Stop";
            this.tsbStopWinService.Click += new System.EventHandler(this.tsbStopWinService_Click);
            // 
            // tsbRestart
            // 
            this.tsbRestart.Image = global::WindowsGUI.Properties.Resources.RestartIcon;
            this.tsbRestart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRestart.Name = "tsbRestart";
            this.tsbRestart.Size = new System.Drawing.Size(63, 22);
            this.tsbRestart.Text = "Restart";
            this.tsbRestart.Click += new System.EventHandler(this.tsbRestart_Click);
            // 
            // tsbSettings
            // 
            this.tsbSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSettings.Image = global::WindowsGUI.Properties.Resources.Configuration;
            this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(69, 22);
            this.tsbSettings.Text = "Settings";
            this.tsbSettings.Click += new System.EventHandler(this.tsbSettings_Click);
            // 
            // tsbAbout
            // 
            this.tsbAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbAbout.Image = global::WindowsGUI.Properties.Resources.HelpIcon;
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(60, 22);
            this.tsbAbout.Text = "About";
            this.tsbAbout.Click += new System.EventHandler(this.tsbAbout_Click);
            // 
            // tssSeparator2
            // 
            this.tssSeparator2.Name = "tssSeparator2";
            this.tssSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSaveConfiguration
            // 
            this.tsbSaveConfiguration.Image = global::WindowsGUI.Properties.Resources.SaveHS;
            this.tsbSaveConfiguration.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveConfiguration.Name = "tsbSaveConfiguration";
            this.tsbSaveConfiguration.Size = new System.Drawing.Size(128, 22);
            this.tsbSaveConfiguration.Text = "Save Configuration";
            this.tsbSaveConfiguration.Click += new System.EventHandler(this.btnSaveConfiguration_Click);
            // 
            // tsbHomepage
            // 
            this.tsbHomepage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbHomepage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHomepage.Image = global::WindowsGUI.Properties.Resources.Web;
            this.tsbHomepage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHomepage.Name = "tsbHomepage";
            this.tsbHomepage.Size = new System.Drawing.Size(23, 22);
            this.tsbHomepage.Text = "Open project home page, http://dev.neptuo.com";
            this.tsbHomepage.Click += new System.EventHandler(this.tsbHomepage_Click);
            // 
            // ssStatus
            // 
            this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslTestStatus});
            this.ssStatus.Location = new System.Drawing.Point(0, 436);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Size = new System.Drawing.Size(660, 22);
            this.ssStatus.TabIndex = 2;
            // 
            // tsslTestStatus
            // 
            this.tsslTestStatus.Name = "tsslTestStatus";
            this.tsslTestStatus.Size = new System.Drawing.Size(149, 17);
            this.tsslTestStatus.Text = "Welcome to ServiceRunner";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlNewTask);
            this.pnlMain.Controls.Add(this.pnlConfigurations);
            this.pnlMain.Controls.Add(this.lvTasks);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 25);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(660, 411);
            this.pnlMain.TabIndex = 3;
            // 
            // pnlNewTask
            // 
            this.pnlNewTask.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlNewTask.Controls.Add(this.lblDeleteTasks);
            this.pnlNewTask.Controls.Add(this.btnDeleteTasks);
            this.pnlNewTask.Controls.Add(this.lblTasksCount);
            this.pnlNewTask.Controls.Add(this.btnNewTask);
            this.pnlNewTask.Controls.Add(this.lblNewTask);
            this.pnlNewTask.Location = new System.Drawing.Point(3, 377);
            this.pnlNewTask.Name = "pnlNewTask";
            this.pnlNewTask.Size = new System.Drawing.Size(657, 34);
            this.pnlNewTask.TabIndex = 3;
            // 
            // lblDeleteTasks
            // 
            this.lblDeleteTasks.AutoSize = true;
            this.lblDeleteTasks.Location = new System.Drawing.Point(175, 11);
            this.lblDeleteTasks.Name = "lblDeleteTasks";
            this.lblDeleteTasks.Size = new System.Drawing.Size(122, 13);
            this.lblDeleteTasks.TabIndex = 4;
            this.lblDeleteTasks.Text = "Delete all selected tasks";
            // 
            // btnDeleteTasks
            // 
            this.btnDeleteTasks.Location = new System.Drawing.Point(303, 6);
            this.btnDeleteTasks.Name = "btnDeleteTasks";
            this.btnDeleteTasks.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteTasks.TabIndex = 3;
            this.btnDeleteTasks.Text = "Delete";
            this.btnDeleteTasks.UseVisualStyleBackColor = true;
            this.btnDeleteTasks.Click += new System.EventHandler(this.btnDeleteTasks_Click);
            // 
            // lblTasksCount
            // 
            this.lblTasksCount.AutoSize = true;
            this.lblTasksCount.Location = new System.Drawing.Point(9, 11);
            this.lblTasksCount.Name = "lblTasksCount";
            this.lblTasksCount.Size = new System.Drawing.Size(67, 13);
            this.lblTasksCount.TabIndex = 2;
            this.lblTasksCount.Text = "0 tasks in list";
            // 
            // btnNewTask
            // 
            this.btnNewTask.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewTask.Enabled = false;
            this.btnNewTask.Location = new System.Drawing.Point(532, 6);
            this.btnNewTask.Name = "btnNewTask";
            this.btnNewTask.Size = new System.Drawing.Size(113, 23);
            this.btnNewTask.TabIndex = 1;
            this.btnNewTask.Text = "Create task";
            this.btnNewTask.UseVisualStyleBackColor = true;
            this.btnNewTask.Click += new System.EventHandler(this.btnNewTask_Click);
            // 
            // lblNewTask
            // 
            this.lblNewTask.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNewTask.AutoSize = true;
            this.lblNewTask.Location = new System.Drawing.Point(418, 11);
            this.lblNewTask.Name = "lblNewTask";
            this.lblNewTask.Size = new System.Drawing.Size(108, 13);
            this.lblNewTask.TabIndex = 0;
            this.lblNewTask.Text = "Create new task here";
            // 
            // pnlConfigurations
            // 
            this.pnlConfigurations.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlConfigurations.Controls.Add(this.btnDeleteProfile);
            this.pnlConfigurations.Controls.Add(this.btnCreateProfile);
            this.pnlConfigurations.Controls.Add(this.btnEditProfile);
            this.pnlConfigurations.Controls.Add(this.cobProfiles);
            this.pnlConfigurations.Controls.Add(this.lblProfiles);
            this.pnlConfigurations.Location = new System.Drawing.Point(0, 3);
            this.pnlConfigurations.Name = "pnlConfigurations";
            this.pnlConfigurations.Size = new System.Drawing.Size(657, 32);
            this.pnlConfigurations.TabIndex = 2;
            // 
            // btnDeleteProfile
            // 
            this.btnDeleteProfile.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteProfile.Location = new System.Drawing.Point(488, 6);
            this.btnDeleteProfile.Name = "btnDeleteProfile";
            this.btnDeleteProfile.Size = new System.Drawing.Size(53, 23);
            this.btnDeleteProfile.TabIndex = 6;
            this.btnDeleteProfile.Text = "Delete";
            this.btnDeleteProfile.UseVisualStyleBackColor = true;
            this.btnDeleteProfile.Click += new System.EventHandler(this.btnDeleteProfile_Click);
            // 
            // btnCreateProfile
            // 
            this.btnCreateProfile.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateProfile.Location = new System.Drawing.Point(558, 5);
            this.btnCreateProfile.Name = "btnCreateProfile";
            this.btnCreateProfile.Size = new System.Drawing.Size(90, 23);
            this.btnCreateProfile.TabIndex = 5;
            this.btnCreateProfile.Text = "Create profile";
            this.btnCreateProfile.UseVisualStyleBackColor = true;
            this.btnCreateProfile.Click += new System.EventHandler(this.btnCreateProfile_Click);
            // 
            // btnEditProfile
            // 
            this.btnEditProfile.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditProfile.Location = new System.Drawing.Point(447, 6);
            this.btnEditProfile.Name = "btnEditProfile";
            this.btnEditProfile.Size = new System.Drawing.Size(35, 23);
            this.btnEditProfile.TabIndex = 4;
            this.btnEditProfile.Text = "Edit";
            this.btnEditProfile.UseVisualStyleBackColor = true;
            this.btnEditProfile.Click += new System.EventHandler(this.btnEditProfile_Click);
            // 
            // cobProfiles
            // 
            this.cobProfiles.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cobProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobProfiles.FormattingEnabled = true;
            this.cobProfiles.Location = new System.Drawing.Point(80, 7);
            this.cobProfiles.Name = "cobProfiles";
            this.cobProfiles.Size = new System.Drawing.Size(361, 21);
            this.cobProfiles.TabIndex = 1;
            this.cobProfiles.SelectedIndexChanged += new System.EventHandler(this.btnProfiles_Click);
            // 
            // lblProfiles
            // 
            this.lblProfiles.AutoSize = true;
            this.lblProfiles.Location = new System.Drawing.Point(3, 10);
            this.lblProfiles.Name = "lblProfiles";
            this.lblProfiles.Size = new System.Drawing.Size(71, 13);
            this.lblProfiles.TabIndex = 0;
            this.lblProfiles.Text = "Select profile:";
            // 
            // lvTasks
            // 
            this.lvTasks.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhOrder,
            this.clhName,
            this.clhRunScript,
            this.clhStopScript,
            this.clhStatus});
            this.lvTasks.ContextMenuStrip = this.cmsListView;
            this.lvTasks.FullRowSelect = true;
            this.lvTasks.Location = new System.Drawing.Point(0, 41);
            this.lvTasks.Name = "lvTasks";
            this.lvTasks.Size = new System.Drawing.Size(660, 330);
            this.lvTasks.TabIndex = 1;
            this.lvTasks.UseCompatibleStateImageBehavior = false;
            this.lvTasks.View = System.Windows.Forms.View.Details;
            this.lvTasks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvTasks_MouseDoubleClick);
            // 
            // clhOrder
            // 
            this.clhOrder.Text = "Order";
            this.clhOrder.Width = 40;
            // 
            // clhName
            // 
            this.clhName.Text = "Name";
            this.clhName.Width = 110;
            // 
            // clhRunScript
            // 
            this.clhRunScript.Text = "Run script";
            this.clhRunScript.Width = 200;
            // 
            // clhStopScript
            // 
            this.clhStopScript.Text = "Stop script";
            this.clhStopScript.Width = 200;
            // 
            // clhStatus
            // 
            this.clhStatus.Text = "Status";
            // 
            // cmsListView
            // 
            this.cmsListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEdit,
            this.tsmiDelete});
            this.cmsListView.Name = "cmsListView";
            this.cmsListView.Size = new System.Drawing.Size(108, 48);
            this.cmsListView.Opening += new System.ComponentModel.CancelEventHandler(this.cmsListView_Opening);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.Image = global::WindowsGUI.Properties.Resources.EditIcon;
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(107, 22);
            this.tsmiEdit.Text = "&Edit";
            this.tsmiEdit.Click += new System.EventHandler(this.tsmiEdit_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Image = global::WindowsGUI.Properties.Resources.DeleteIcon;
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(107, 22);
            this.tsmiDelete.Text = "&Delete";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Enabled = true;
            this.tmrRefresh.Interval = 2000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 458);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.ssStatus);
            this.Controls.Add(this.tsMainMenu);
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "ServiceRunner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tsMainMenu.ResumeLayout(false);
            this.tsMainMenu.PerformLayout();
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlNewTask.ResumeLayout(false);
            this.pnlNewTask.PerformLayout();
            this.pnlConfigurations.ResumeLayout(false);
            this.pnlConfigurations.PerformLayout();
            this.cmsListView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ServiceProcess.ServiceController WinServiceController;
        private System.Windows.Forms.ToolStrip tsMainMenu;
        private System.Windows.Forms.ToolStripLabel tslWinService;
        private System.Windows.Forms.ToolStripLabel tslWinServiceStatus;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.ToolStripButton tsbStartWinService;
        private System.Windows.Forms.StatusStrip ssStatus;
        private System.Windows.Forms.ToolStripButton tsbStopWinService;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlConfigurations;
        private System.Windows.Forms.ListView lvTasks;
        private System.Windows.Forms.ColumnHeader clhOrder;
        private System.Windows.Forms.ColumnHeader clhName;
        private System.Windows.Forms.ColumnHeader clhRunScript;
        private System.Windows.Forms.ColumnHeader clhStopScript;
        private System.Windows.Forms.Panel pnlNewTask;
        private System.Windows.Forms.ComboBox cobProfiles;
        private System.Windows.Forms.Label lblProfiles;
        private System.Windows.Forms.Label lblTasksCount;
        private System.Windows.Forms.Button btnNewTask;
        private System.Windows.Forms.Label lblNewTask;
        private System.Windows.Forms.ToolStripButton tsbSettings;
        private System.Windows.Forms.ToolStripButton tsbAbout;
        private System.Windows.Forms.ToolStripStatusLabel tsslTestStatus;
        private System.Windows.Forms.ToolStripButton tsbRestart;
        private System.Windows.Forms.ContextMenuStrip cmsListView;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ColumnHeader clhStatus;
        private System.Windows.Forms.Button btnEditProfile;
        private System.Windows.Forms.ToolStripSeparator tssSeparator2;
        private System.Windows.Forms.ToolStripButton tsbSaveConfiguration;
        private System.Windows.Forms.Button btnCreateProfile;
        private System.Windows.Forms.Button btnDeleteProfile;
        private System.Windows.Forms.Label lblDeleteTasks;
        private System.Windows.Forms.Button btnDeleteTasks;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.ToolStripButton tsbHomepage;
    }
}

