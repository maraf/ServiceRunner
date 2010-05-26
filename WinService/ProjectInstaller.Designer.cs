namespace ServiceRunner
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.winServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.winServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // winServiceProcessInstaller
            // 
            this.winServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.winServiceProcessInstaller.Password = null;
            this.winServiceProcessInstaller.Username = null;
            // 
            // winServiceInstaller
            // 
            this.winServiceInstaller.Description = "Windows service for ServiceRunner";
            this.winServiceInstaller.DisplayName = "ServiceRunner";
            this.winServiceInstaller.ServiceName = "ServiceRunner";
            this.winServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.winServiceProcessInstaller,
            this.winServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller winServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller winServiceInstaller;
    }
}