using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;

using ServiceRunner.Model;
using ServiceRunner.Loader;
using ServiceRunner.Util;

namespace ServiceRunner.GUI
{
    public partial class MainForm : Form
    {
        private ConfigurationLoader ConfigLoader;

        private TaskDetail frmTaskDetail;
        private ProfileDetail frmProfileDetail;

        private int SelectedListViewItemIndex = -1;
        private int SelectedProfileIndex = -1;

        public MainForm()
        {
            InitializeComponent();
            InitializeLoader();

            try
            {
                if (WinServiceController.Status == ServiceControllerStatus.Running)
                {
                    tslWinServiceStatus.Text = StringFormats.WinStatusRunnig;
                    tsbStartWinService.Enabled = false;
                    tsbStopWinService.Enabled = true;
                }
                else
                {
                    tslWinServiceStatus.Text = StringFormats.WinStatusStopped;
                    tsbStartWinService.Enabled = true;
                    tsbStopWinService.Enabled = false;
                }
            }
            catch (Exception e)
            {
                Close();
            }
        }

        public void InitializeLoader()
        {
            // Mozna zde nebude default!
            ConfigLoader = new XmlLoader(Messages.Default.ConfigurationFilePath);

            cobProfiles.Items.Clear();
            foreach (Profile item in ConfigLoader.Configuration.Profiles)
            {
                cobProfiles.Items.Add(item.Name);
            }
            if (ConfigLoader.Configuration.Profiles.Count > 0)
            {
                cobProfiles.SelectedIndex = 0;
            }
        }

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
                    CreateOrUpdateTask(frmTaskDetail.Task);
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
                    CreateOrUpdateProfile(frmProfileDetail.Profile);
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

        private bool ValidateTask(SingleTask task)
        {
            if (!"".Equals(task.Name) && !"".Equals(task.RunScript) && !"".Equals(task.StopScript))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateProfile(Profile profile)
        {
            return false;
        }

        private void CreateOrUpdateTask(SingleTask task)
        {

            lvTasks.BeginUpdate();
            try
            {
                string[] cols = { String.Format("{0}", task.Order), task.Name, task.RunScript, task.StopScript, task.Disabled ? StringFormats.StatusDisabled : StringFormats.StatusEnabled };
                if (SelectedListViewItemIndex != -1)
                {
                    lvTasks.Items[SelectedListViewItemIndex] = new ListViewItem(cols);

                    ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[SelectedListViewItemIndex].Name = task.Name;
                    ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[SelectedListViewItemIndex].RunScript = task.RunScript;
                    ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[SelectedListViewItemIndex].RunArguments = task.RunArguments;
                    ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[SelectedListViewItemIndex].StopScript = task.StopScript;
                    ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[SelectedListViewItemIndex].StopArguments = task.StopArguments;
                    ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[SelectedListViewItemIndex].Disabled = task.Disabled;
                    SelectedListViewItemIndex = -1;
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

                    ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks.Add(st);
                }
                tsslTestStatus.Text = String.Format(StringFormats.TaskSaved, task.Name);
            }
            finally
            {
                lvTasks.EndUpdate();
            }
        }

        private void CreateOrUpdateProfile(Profile profile)
        {
            // update dropdown + model
            if (profile.Order == -1)
            {
                Profile p = new Profile();
                p.Disabled = profile.Disabled;
                p.Name = profile.Name;
                p.Order = cobProfiles.Items.Count + 1;
                ConfigLoader.Configuration.Profiles.Add(p);

                cobProfiles.Items.Add(p.Name);
                cobProfiles.SelectedIndex = cobProfiles.Items.Count - 1;
            }
            else
            {
                ConfigLoader.Configuration.Profiles[SelectedProfileIndex].Disabled = profile.Disabled;
                ConfigLoader.Configuration.Profiles[SelectedProfileIndex].Name = profile.Name;
            }
            tsslTestStatus.Text = String.Format(StringFormats.ProfileSaved, profile.Name);
        }

        private void tsbStartWinService_Click(object sender, EventArgs e)
        {
            if (tsbStartWinService.Enabled == true)
            {
                WinServiceController.Start();
                tslWinServiceStatus.Text = StringFormats.WinStatusRunnig;
                tsbStartWinService.Enabled = false;
                tsbStopWinService.Enabled = true;
            }
        }

        private void tsbStopWinService_Click(object sender, EventArgs e)
        {
            if (tsbStopWinService.Enabled == true)
            {
                WinServiceController.Stop();
                tslWinServiceStatus.Text = StringFormats.WinStatusStopped;
                tsbStartWinService.Enabled = true;
                tsbStopWinService.Enabled = false;
            }
        }

        private void tsbRestart_Click(object sender, EventArgs e)
        {
            // Takhle ne, ale to zakomentovane taky nejde!!!
            WinServiceController.Refresh();
            //tsbStopWinService_Click(sender, e);
            //tsbStartWinService_Click(sender, e);
        }

        private void btnProfiles_Click(object sender, EventArgs e)
        {
            Cursor lastCursor = this.Cursor;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    lvTasks.BeginUpdate();
                    lvTasks.Items.Clear();
                    foreach (SingleTask task in ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks)
                    {
                        string[] cols = { String.Format("{0}", task.Order), task.Name, task.RunScript, task.StopScript, task.Disabled ? StringFormats.StatusDisabled : StringFormats.StatusEnabled };
                        ListViewItem lvi = new ListViewItem(cols);
                        lvTasks.Items.Add(lvi);
                    }
                    Application.DoEvents();
                    lblTasksCount.Text = String.Format(StringFormats.TasksInList, ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks.Count);
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

        private void btnNewTask_Click(object sender, EventArgs e)
        {
            SingleTask t = new SingleTask();
            t.Name = StringFormats.NewTaskName;
            OpenTaskDetail(t);
        }

        private void lvTasks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectedListViewItemIndex = lvTasks.GetItemAt(e.Location.X, e.Location.Y).Index;
            OpenTaskDetail(ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[SelectedListViewItemIndex]);
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
            ConfigLoader.SaveToSource(ConfigLoader.Configuration.Path);
            tsslTestStatus.Text = String.Format(StringFormats.ConfigurationSaved, ConfigLoader.Configuration.Path);
        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            SelectedProfileIndex = cobProfiles.SelectedIndex;
            OpenProfileDetail(ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex]);
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            SelectedListViewItemIndex = lvTasks.SelectedIndices[0];
            OpenTaskDetail(ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[lvTasks.SelectedIndices[0]]);
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(String.Format(StringFormats.DeleteTask, ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[lvTasks.SelectedIndices[0]].Name), StringFormats.DeleteTaskTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks.Remove(ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Tasks[lvTasks.SelectedIndices[0]]);
                lvTasks.Items[lvTasks.SelectedIndices[0]].Remove();
            }
        }

        private void btnDeleteProfile_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(String.Format(StringFormats.DeleteProfile, ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex].Name), StringFormats.DeleteProfileTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ConfigLoader.Configuration.Profiles.Remove(ConfigLoader.Configuration.Profiles[cobProfiles.SelectedIndex]);
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
            ConfigLoader.SaveToSource(ConfigLoader.Configuration.Path);
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
