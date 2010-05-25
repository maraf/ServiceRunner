using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.IO;
using System.Resources;
using System.Threading;

using ServiceRunner.Model;
using ServiceRunner.Loader;
using ServiceRunner.Util;

namespace ServiceRunner.GUI
{
    /// <summary>
    /// Hlavni formular
    /// TODO: Restartovani sluzby.
    /// </summary>
    public partial class MainForm : Form
    {
        // == PRIVATNI PROMENNE =======================================================
        private ConfigurationLoader configLoader;
        private Settings settings;

        private TaskDetail frmTaskDetail;
        private ProfileDetail frmProfileDetail;
        private SettingsForm frmSettingsForm;
        private AboutBox frmAbout;

        private int SelectedListViewItemIndex = -1;
        private int SelectedProfileIndex = -1;
        private int TimerRefreshShortInterval = 2000;
        private int TimerRefreshLongInterval = 20000;

        // == KONSTRUKTOR =============================================================
        public MainForm()
        {
            InitializeComponent();

            if (CheckServiceStatus())
            {
                InitializeLoader();
            }
        }

        // == POMOCNE METODY ==========================================================
        /// <summary>
        /// Inicializuje <code>configLoader</code>, nacte do <code>cobProfiles</code> profily z konfigurace a pokusi se prvni vybrat
        /// </summary>
        public void InitializeLoader()
        {
            settings = new Settings("Settings.resx");

            if (ValidateSettings(settings))
            {
                try
                {
                    configLoader = new XmlLoader(settings.PathToConfigFile);
                    cobProfiles.Items.Clear();
                    foreach (Profile item in configLoader.Configuration.Profiles)
                    {
                        cobProfiles.Items.Add(item.Name);
                    }
                    if (configLoader.Configuration.Profiles.Count > 0)
                    {
                        cobProfiles.SelectedIndex = 0;

                    }
                }
                catch (WrongXmlFormat e)
                {
                    MessageBox.Show(StringFormats.NotValidSettings, StringFormats.WrongValues, MessageBoxButtons.OK);
                    OpenSettingsFrom();
                }
            }
            else
            {
                MessageBox.Show(StringFormats.NotValidSettings, StringFormats.WrongValues, MessageBoxButtons.OK);
                OpenSettingsFrom();
            }
        }

        /// <summary>
        /// Zobrazi formular(<code>frmTaskDetail</code>) pro editaci tasku a naplni ho <code>task</code>
        /// </summary>
        /// <param name="task"></param>
        public void OpenTaskDetail(SingleTask task)
        {
            if (frmTaskDetail == null)
            {
                // mozna opravit
                frmTaskDetail = new TaskDetail(task);
            }
            if (frmTaskDetail.WindowState == FormWindowState.Minimized)
            {
                frmTaskDetail.WindowState = FormWindowState.Normal;
            }
            if (!frmTaskDetail.Visible)
            {
                frmTaskDetail.Show();
            }
            frmTaskDetail.BringToFront();
            frmTaskDetail.FormClosed += delegate { frmTaskDetail = null; };
            frmTaskDetail.SaveButtonClicked += delegate
            {
                if (ValidateTask(frmTaskDetail.Task))
                {
                    int i = SelectedListViewItemIndex;
                    CreateOrUpdateTask(frmTaskDetail.Task);
                    SelectedListViewItemIndex = i;
                }
                else
                {
                    MessageBox.Show(StringFormats.NotValidTask, StringFormats.WrongValues, MessageBoxButtons.OK);
                }
            };
            frmTaskDetail.SaveAndCloseButtonClicked += delegate
            {
                if (ValidateTask(frmTaskDetail.Task))
                {
                    CreateOrUpdateTask(frmTaskDetail.Task);
                    frmTaskDetail.Close();
                }
                else
                {
                    MessageBox.Show(StringFormats.NotValidTask, StringFormats.WrongValues, MessageBoxButtons.OK);
                }
            };
            frmTaskDetail.CloseButtonClicked += delegate
            {
                frmTaskDetail.Close();
            };
        }

        /// <summary>
        /// Zobrazi formular(<code>frmProfileDetail</code>) pro editaci profilu a naplni ho <code>profile</code>
        /// </summary>
        /// <param name="profile"></param>
        private void OpenProfileDetail(Profile profile)
        {
            if (frmProfileDetail == null)
            {
                // mozna opravit
                frmProfileDetail = new ProfileDetail(profile);
            }
            if (frmProfileDetail.WindowState == FormWindowState.Minimized)
            {
                frmProfileDetail.WindowState = FormWindowState.Normal;
            }
            if (!frmProfileDetail.Visible)
            {
                frmProfileDetail.Show();
            }
            frmProfileDetail.BringToFront();
            frmProfileDetail.FormClosed += delegate { frmProfileDetail = null; };
            frmProfileDetail.SaveButtonClicked += delegate
            {
                if (ValidateProfile(frmProfileDetail.Profile))
                {
                    int i = SelectedProfileIndex;
                    CreateOrUpdateProfile(frmProfileDetail.Profile);
                    SelectedProfileIndex = i;
                }
                else
                {
                    MessageBox.Show(StringFormats.NotValidProfile, StringFormats.WrongValues, MessageBoxButtons.OK);
                }
            };
            frmProfileDetail.SaveAndCloseButtonClicked += delegate
            {
                if (ValidateProfile(frmProfileDetail.Profile))
                {
                    CreateOrUpdateProfile(frmProfileDetail.Profile);
                    frmProfileDetail.Close();
                }
                else
                {
                    MessageBox.Show(StringFormats.NotValidProfile, StringFormats.WrongValues, MessageBoxButtons.OK);
                }
            };
            frmProfileDetail.CloseButtonClicked += delegate
            {
                frmProfileDetail.Close();
            };
        }

        /// <summary>
        /// Zobrazi formular(<code>frmSettingsForm</code>) pro editaci vlastnosti
        /// </summary>
        private void OpenSettingsFrom()
        {
            if (frmSettingsForm == null)
            {
                frmSettingsForm = new SettingsForm(settings);
                //frmSettingsForm.ShowDialog(this);
                frmSettingsForm.Show();
                frmSettingsForm.FormClosed += delegate { frmSettingsForm = null; };
                frmSettingsForm.SaveButtonClicked += delegate
                {
                    if (ValidateSettings(frmSettingsForm.Settings))
                    {
                        try
                        {
                            if (configLoader != null)
                            {
                                configLoader.SaveToSource(configLoader.Configuration.Path);
                            }
                            lvTasks.Items.Clear();
                            settings.SaveToResource("Settings.resx");
                            InitializeLoader();
                        }
                        catch (WrongXmlFormat e)
                        {
                            MessageBox.Show(StringFormats.NotValidSettings, StringFormats.WrongValues, MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show(StringFormats.NotValidSettings, StringFormats.WrongValues, MessageBoxButtons.OK);
                    }
                };
                frmSettingsForm.SaveAndCloseButtonClicked += delegate
                {
                    if (ValidateSettings(frmSettingsForm.Settings))
                    {
                        try
                        {
                            if (configLoader != null)
                            {
                                configLoader.SaveToSource(configLoader.Configuration.Path);
                            }
                            lvTasks.Items.Clear();
                            settings.SaveToResource("Settings.resx");
                            InitializeLoader();
                            frmSettingsForm.Close();
                        }
                        catch (WrongXmlFormat e)
                        {
                            MessageBox.Show(StringFormats.NotValidSettings, StringFormats.WrongValues, MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show(StringFormats.NotValidSettings, StringFormats.WrongValues, MessageBoxButtons.OK);
                    }
                };
                frmSettingsForm.CloseButtonClicked += delegate
                {
                    frmSettingsForm.Close();
                };
            }
        }

        /// <summary>
        /// Zobrazi AboutBox
        /// </summary>
        private void OpenAbout()
        {
            if (frmAbout == null)
            {
                frmAbout = new AboutBox();
                frmAbout.ShowDialog(this);
                frmAbout.FormClosed += delegate { frmAbout = null; };
            }
        }

        /// <summary>
        /// Zvaliduje zadany task
        /// </summary>
        /// <param name="task">task k validaci</param>
        /// <returns>true pokud je validni, false jinak</returns>
        private bool ValidateTask(SingleTask task)
        {
            if (!"".Equals(task.Name) && !"".Equals(task.RunScript) && !"".Equals(task.StopScript))
            {
                foreach (SingleTask t in configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks)
                {
                    if (t.Name.Equals(task.Name) && (SelectedListViewItemIndex == -1 || !lvTasks.Items[SelectedListViewItemIndex].SubItems[1].Text.Equals(t.Name)))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Zvaliduje zadany profile
        /// </summary>
        /// <param name="profile">profile k validaci</param>
        /// <returns>true pokud je validni, false jinak</returns>
        private bool ValidateProfile(Profile profile)
        {
            if (!"".Equals(profile.Name))
            {
                foreach (Profile p in configLoader.Configuration.Profiles)
                {
                    if (p.Name.Equals(profile.Name) && (SelectedProfileIndex == -1 || !cobProfiles.Items[SelectedProfileIndex].Equals(p.Name)))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Zvaliduje vlastnosti
        /// </summary>
        /// <param name="settings">settings k validaci</param>
        /// <returns>true pokud jsou validni, false jinak</returns>
        private bool ValidateSettings(Settings settings)
        {
            if (!"".Equals(settings.PathToConfigFile) && File.Exists(settings.PathToConfigFile) && !"".Equals(settings.PathToLogDirectory) && Directory.Exists(settings.PathToLogDirectory))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Vytvori nebo aktualizuje zadany task
        /// </summary>
        /// <param name="task">task</param>
        private void CreateOrUpdateTask(SingleTask task)
        {

            lvTasks.BeginUpdate();
            try
            {
                string[] cols = { String.Format("{0}", task.Order), task.Name, task.RunScript, task.StopScript, task.Disabled ? StringFormats.StatusDisabled : StringFormats.StatusEnabled };
                if (SelectedListViewItemIndex != -1)
                {
                    lvTasks.Items[SelectedListViewItemIndex] = new ListViewItem(cols);

                    configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[SelectedListViewItemIndex].Name = task.Name;
                    configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[SelectedListViewItemIndex].RunScript = task.RunScript;
                    configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[SelectedListViewItemIndex].RunArguments = task.RunArguments;
                    configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[SelectedListViewItemIndex].StopScript = task.StopScript;
                    configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[SelectedListViewItemIndex].StopArguments = task.StopArguments;
                    configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[SelectedListViewItemIndex].Disabled = task.Disabled;
                    SelectedListViewItemIndex = -1;

                    UpdateTasksCount();
                }
                else
                {
                    cols[0] = String.Format("{0}", lvTasks.Items.Count + 1);
                    lvTasks.Items.Add(new ListViewItem(cols));

                    SingleTask st = new SingleTask();
                    st.Order = lvTasks.Items.Count;
                    st.Name = task.Name;
                    st.RunScript = task.RunScript;
                    st.RunArguments = task.RunArguments;
                    st.StopScript = task.StopScript;
                    st.StopArguments = task.StopArguments;
                    st.Disabled = task.Disabled;

                    configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks.Add(st);
                }
                tsslTestStatus.Text = String.Format(StringFormats.TaskSaved, task.Name);
            }
            finally
            {
                lvTasks.EndUpdate();
            }
        }

        /// <summary>
        /// Vytvori nebo aktualizuje profile
        /// </summary>
        /// <param name="profile">profile</param>
        private void CreateOrUpdateProfile(Profile profile)
        {
            // update dropdown + model
            if (profile.Order == -1)
            {
                Profile p = new Profile();
                p.Disabled = profile.Disabled;
                p.Name = profile.Name;
                p.Order = cobProfiles.Items.Count + 1;
                configLoader.Configuration.Profiles.Add(p);

                cobProfiles.Items.Add(p.Name);
                cobProfiles.SelectedIndex = cobProfiles.Items.Count - 1;
            }
            else
            {
                configLoader.Configuration.Profiles[SelectedProfileIndex].Disabled = profile.Disabled;
                configLoader.Configuration.Profiles[SelectedProfileIndex].Name = profile.Name;
            }
            tsslTestStatus.Text = String.Format(StringFormats.ProfileSaved, profile.Name);
        }

        /// <summary>
        /// Naplni <code>lvTasks</code> tasky z aktualne vybraneho profilu
        /// </summary>
        private void FillListViewTasks()
        {
            Cursor lastCursor = this.Cursor;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    lvTasks.BeginUpdate();
                    lvTasks.Items.Clear();
                    foreach (SingleTask task in configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks)
                    {
                        string[] cols = { String.Format("{0}", task.Order), task.Name, task.RunScript, task.StopScript, task.Disabled ? StringFormats.StatusDisabled : StringFormats.StatusEnabled };
                        ListViewItem lvi = new ListViewItem(cols);
                        lvTasks.Items.Add(lvi);
                    }
                    Application.DoEvents();
                    UpdateTasksCount();
                    btnNewTask.Enabled = true;
                }
                finally
                {
                    lvTasks.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, String.Format(StringFormats.ErrorLoading), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = lastCursor;
            }
        }

        /// <summary>
        /// Upravi hodnotu pocitadla tasku v aktualnim profilu
        /// </summary>
        private void UpdateTasksCount()
        {
            lblTasksCount.Text = String.Format(StringFormats.TasksInList, lvTasks.Items.Count);
        }

        /// <summary>
        /// Aktualizuje stav Win sluzby
        /// </summary>
        private bool CheckServiceStatus()
        {
            try
            {
                WinServiceController.Refresh();
                if (WinServiceController.Status == ServiceControllerStatus.Running)
                {
                    tslWinServiceStatus.Text = StringFormats.WinStatusRunnig;
                    tsbStartWinService.Enabled = false;
                    tsbStopWinService.Enabled = true;
                    //TODO: Po zprovozneni restartu, zde bude true!
                    tsbRestart.Enabled = false;
                }
                else
                {
                    tslWinServiceStatus.Text = StringFormats.WinStatusStopped;
                    tsbStartWinService.Enabled = true;
                    tsbStopWinService.Enabled = false;
                    tsbRestart.Enabled = false;
                }
            }
            catch (Exception e)
            {
                if (tmrRefresh != null)
                {
                    tmrRefresh.Enabled = false;
                }
                MessageBox.Show(StringFormats.ServiceUnavailable, StringFormats.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
#if DEBUG
                MessageBox.Show(e.Message, StringFormats.Error, MessageBoxButtons.OK);
#endif
                Close();
                return false;
            }
            return true;
        }

        // == UDALOSTI ================================================================

        private void tsbStartWinService_Click(object sender, EventArgs e)
        {
            if (tsbStartWinService.Enabled == true)
            {
                WinServiceController.Start();
                tmrRefresh.Interval = TimerRefreshLongInterval;
                tslWinServiceStatus.Text = StringFormats.WinStatusStarting;
                tsslTestStatus.Text = StringFormats.WinStatusStartingLong;
                tsbStartWinService.Enabled = false;
                tsbRestart.Enabled = false;
            }
        }

        private void tsbStopWinService_Click(object sender, EventArgs e)
        {
            if (tsbStopWinService.Enabled == true)
            {
                WinServiceController.Stop();
                tmrRefresh.Interval = TimerRefreshLongInterval;
                tslWinServiceStatus.Text = StringFormats.WinStatusStopping;
                tsslTestStatus.Text = StringFormats.WinStatusStoppingLong;
                tsbStopWinService.Enabled = false;
                tsbRestart.Enabled = false;
            }
        }

        private void tsbRestart_Click(object sender, EventArgs e)
        {
            /*tsbStopWinService_Click(sender, e);
            Thread.Sleep(21000);
            tsbStartWinService_Click(sender, e);*/
        }

        private void btnProfiles_Click(object sender, EventArgs e)
        {
            FillListViewTasks();
        }

        private void btnNewTask_Click(object sender, EventArgs e)
        {
            SingleTask t = new SingleTask();
            t.Name = StringFormats.NewTaskName;
            OpenTaskDetail(t);
        }

        private void btnDeleteTasks_Click(object sender, EventArgs e)
        {
            if (lvTasks.SelectedIndices.Count == 0)
            {
                return;
            }
            else if (lvTasks.SelectedIndices.Count == 1)
            {
                tsmiDelete_Click(sender, e);
                return;
            }

            string names = "";
            bool first = true;
            foreach (int index in lvTasks.SelectedIndices)
            {
                SingleTask item = configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[index];
                if (first)
                {
                    names += item.Name;
                    first = false;
                }
                else
                {
                    names += ", " + item.Name;
                }
            }

            if (MessageBox.Show(String.Format(StringFormats.DeleteTasks, names), StringFormats.DeleteTasksTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                for (int i = 0; i < lvTasks.SelectedIndices.Count; )
                {
                    int index = lvTasks.SelectedIndices[lvTasks.SelectedIndices.Count - i - 1];
                    configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks.Remove(configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[index]);
                    lvTasks.Items[index].Remove();
                }
            }
            UpdateTasksCount();
        }

        private void lvTasks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectedListViewItemIndex = lvTasks.GetItemAt(e.Location.X, e.Location.Y).Index;
            OpenTaskDetail(configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[SelectedListViewItemIndex]);
        }

        private void cmsListView_Opening(object sender, CancelEventArgs e)
        {
            if (lvTasks.SelectedItems.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void btnSaveConfiguration_Click(object sender, EventArgs e)
        {
            configLoader.SaveToSource(configLoader.Configuration.Path);
            tsslTestStatus.Text = String.Format(StringFormats.ConfigurationSaved, configLoader.Configuration.Path);
        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            SelectedProfileIndex = cobProfiles.SelectedIndex;
            OpenProfileDetail(configLoader.Configuration.Profiles[cobProfiles.SelectedIndex]);
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            SelectedListViewItemIndex = lvTasks.SelectedIndices[0];
            OpenTaskDetail(configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[lvTasks.SelectedIndices[0]]);
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(String.Format(StringFormats.DeleteTask, configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[lvTasks.SelectedIndices[0]].Name), StringFormats.DeleteTaskTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks.Remove(configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[lvTasks.SelectedIndices[0]]);
                lvTasks.Items[lvTasks.SelectedIndices[0]].Remove();
                UpdateTasksCount();
            }
        }

        private void btnDeleteProfile_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(String.Format(StringFormats.DeleteProfile, configLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Name), StringFormats.DeleteProfileTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                configLoader.Configuration.Profiles.Remove(configLoader.Configuration.Profiles[cobProfiles.SelectedIndex]);
                cobProfiles.Items.Remove(cobProfiles.Items[cobProfiles.SelectedIndex]);
                if (cobProfiles.Items.Count > 0)
                {
                    cobProfiles.SelectedIndex = 0;
                }
            }
        }

        private void btnCreateProfile_Click(object sender, EventArgs e)
        {
            Profile p = new Profile();
            p.Name = StringFormats.NewProfileName;
            OpenProfileDetail(p);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            configLoader.SaveToSource(configLoader.Configuration.Path);
        }

        private void tsbSettings_Click(object sender, EventArgs e)
        {
            OpenSettingsFrom();
        }

        private void tsbAbout_Click(object sender, EventArgs e)
        {
            OpenAbout();
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            CheckServiceStatus();
            if (tsslTestStatus.Text.Equals(StringFormats.WinStatusStarting) || tsslTestStatus.Text.Equals(StringFormats.WinStatusStopping))
            {
                tsslTestStatus.Text = "";
            }
            if (tmrRefresh.Interval == TimerRefreshLongInterval)
            {
                tmrRefresh.Interval = TimerRefreshShortInterval;
            }
        }

        private void tsbHomepage_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://dev.neptuo.com");
        }

        /*private void btnEnableProfile_Click(object sender, EventArgs e)
        {
            if (StringFormats.DisableStatus.Equals(btnEnableProfile.Text))
            {
                ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Disabled = true;
                btnEnableProfile.Text = StringFormats.EnableStatus;
            }
            else
            {
                ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Disabled = false;
                btnEnableProfile.Text = StringFormats.DisableStatus;
            }
        }*/
    }
}
