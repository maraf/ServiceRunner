using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRunner.Util
{
    public static class StringFormats
    {
        public static string TasksInList { get { return "{0} tasks in list"; } }

        public static string DeleteTaskTitle { get { return "Delete task"; } }

        public static string DeleteProfileTitle { get { return "Delete profile"; } }

        public static string DeleteTask { get { return "Do you really want to delete task with name {0}?"; } }

        public static string DeleteProfile { get { return "Do you really want to delete profile with name {0}?"; } }

        public static string StatusDisabled { get { return "Disabled"; } }

        public static string StatusEnabled { get { return "Enabled"; } }

        public static string ErrorLoading { get { return "ERROR while loading tasks from profile!"; } }

        public static string WinStatusRunnig { get { return "Running"; } }

        public static string WinStatusStopped { get { return "Stopped"; } }

        public static string ConfigurationSaved { get { return "Configuration has been saved to {0}"; } }

        public static string EnableStatus { get { return "Enable"; } }

        public static string DisableStatus { get { return "Disable"; } }

        public static string NewTaskName { get { return "New task"; } }

        public static string NewProfileName { get { return "New profile"; } }

        public static string TaskSaved { get { return "Task {0} has been saved."; } }

        public static string ProfileSaved { get { return "Profile {0} has been saved."; } }

        public static string WrongValues { get { return "Some fields has wrong values!"; } }

        public static string NotValidTask { get { return "Name, path to run and stop scripts must be set!"; } }

        public static string NotValidProfile { get { return "Name must be set!"; } }
    }
}
