using BackFiles_New.BLL;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BackFiles_New.UserControls
{
    /// <summary>
    /// MyWindowsConfigControl.xaml 的交互逻辑
    /// </summary>
    public partial class MyWindowsConfigControl : UserControl
    {
        bool loaded = false;
        public MyWindowsConfigControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 锁屏杀掉钉钉进程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkWindowLockKillDingTalk_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Name == "chkWindowLockKillProcess")
            {
                if (cb.IsChecked ?? false)
                    MonitorWindowLockTools.AddWindowLockEvent(Tool.WindowsLockKillDingTalk);
                else
                    MonitorWindowLockTools.RemoveWindowLockEvent(Tool.WindowsLockKillDingTalk);
                ConfigBLL.SaveConfig(cb.IsChecked.Value.ToString(), "MyWindowsConfigControl.chkWindowLockKillProcess");
                ConfigBLL.SaveConfig(this.txtKillProcessNames.Text.Trim().Replace("，", ",").Replace("；",";"), "MyWindowsConfigControl.txtKillProcessNames");
            }
            if (cb.Name == "chkWindowLoginOpenProcess")
            {
                if (cb.IsChecked ?? false)
                    MonitorWindowLockTools.AddWindowLockEvent(Tool.WindowsUnLockOpenDingTalk);
                else
                    MonitorWindowLockTools.RemoveWindowLockEvent(Tool.WindowsUnLockOpenDingTalk);
                ConfigBLL.SaveConfig(cb.IsChecked.Value.ToString(), "MyWindowsConfigControl.chkWindowLoginOpenProcess");
                ConfigBLL.SaveConfig(this.txtOpenProcessPaths.Text.Trim().Trim(',', '"', '\'').Replace("，", ",").Replace("；", ";"), "MyWindowsConfigControl.txtOpenProcessPaths");
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!loaded)
            {
                this.txtOpenProcessPaths.Text = ConfigBLL.GetConfig<String>("MyWindowsConfigControl.txtOpenProcessPaths");
                this.txtKillProcessNames.Text = ConfigBLL.GetConfig<String>("MyWindowsConfigControl.txtKillProcessNames");

                if (ConfigBLL.GetConfig<String>("MyWindowsConfigControl.chkWindowLoginOpenProcess") == "True")
                { this.chkWindowLoginOpenProcess.IsChecked = true; }
                if (ConfigBLL.GetConfig<String>("MyWindowsConfigControl.chkWindowLockKillProcess") == "True")
                { this.chkWindowLockKillProcess.IsChecked = true; }

                loaded = true;
            }
        }
    }
    public class Tool
    {
        /// <summary>
        /// 锁屏终止钉钉
        /// </summary>
        /// <param name="e"></param>
        public static void WindowsLockKillDingTalk(SessionSwitchReason e)
        {
            switch (e)
            {
                case SessionSwitchReason.SessionLock:
                case SessionSwitchReason.SessionLogoff:
                    {
                        if (ConfigBLL.GetConfig<String>("MyWindowsConfigControl.txtKillProcessNames") != null)
                        {
                            List<string> p = ConfigBLL.GetConfig<String>("MyWindowsConfigControl.txtKillProcessNames").Split(',', ';').ToList();
                            foreach (var item in p)
                            {
                                if (!string.IsNullOrEmpty(item))
                                {
                                    foreach (var process in Process.GetProcessesByName(item))
                                        process.Kill();
                                }
                            }
                        }
                    }
                    break;
            }
        }
        public static void WindowsUnLockOpenDingTalk(SessionSwitchReason e)
        {
            switch (e)
            {
                case SessionSwitchReason.SessionLogon:
                case SessionSwitchReason.SessionUnlock:
                    {
                        if (ConfigBLL.GetConfig<String>("MyWindowsConfigControl.txtOpenProcessPaths") != null)
                        {
                            List<string> p = ConfigBLL.GetConfig<String>("MyWindowsConfigControl.txtOpenProcessPaths").Split(',',';').ToList();
                            foreach (var item in p)
                            {
                                if (!string.IsNullOrEmpty(item))
                                    Process.Start(item);
                            }
                        }
                    }
                    break;
            }
        }
    }
}
