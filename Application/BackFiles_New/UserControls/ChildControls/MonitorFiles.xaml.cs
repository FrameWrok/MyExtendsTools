using BackFiles_New.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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

namespace BackFiles_New.UserControls.ChildControls
{

    /// <summary>
    /// MonitorFiles.xaml 的交互逻辑
    /// </summary>
    public partial class MonitorFiles : UserControl
    {
        Dictionary<string, FileSystemWatcher> floderMonitors = new Dictionary<string, FileSystemWatcher>();
        bool loaded = false;
        public MonitorFiles()
        {
            InitializeComponent();
        }

        private void chkFloderChangeClearProcess_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            ConfigBLL.SaveConfig(cb.IsChecked.Value.ToString(), "MonitorFiles.chkFloderChangeClearProcess");
            ConfigBLL.SaveConfig(this.txtFloderChangeClearPath.Text.Trim().Replace("，", ","), "MonitorFiles.txtFloderChangeClearPath");
            if (cb.IsChecked ?? false)
            {
                List<string> floderList = this.txtFloderChangeClearPath.Text.Trim().Replace("，", ",").Split(',').ToList();
                if (floderList.Count > 0)
                {
                    foreach (var floder in floderList)
                    {
                        if (floder.Trim().Length < 4) continue;
                        if (Directory.Exists(floder) && !floderMonitors.ContainsKey(floder))
                        {
                            FileSystemWatcher monitor = new FileSystemWatcher(floder);
                            monitor.EnableRaisingEvents = true;
                            monitor.IncludeSubdirectories = true;
                            monitor.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.LastWrite | NotifyFilters.Size;
                            monitor.Changed += (fsender, fe) =>
                            {
                                FileSystemWatcher monitorw = fsender as FileSystemWatcher;
                                var files = (new DirectoryInfo(monitorw.Path)).GetFiles();
                                foreach (var file in files)
                                {
                                    ThreadPool.QueueUserWorkItem((o) => {
                                        Thread.Sleep(3000);
                                        string filename = o as string;
                                        bool isbreak = false;
                                        while (!isbreak)
                                        {
                                            try
                                            {
                                                Thread.Sleep(1000);
                                                File.Delete(filename);
                                                isbreak = true;
                                            }
                                            catch (Exception ex) { if (ex.Message.Contains("的访问被拒绝")) isbreak = true; }
                                        }                                        
                                    },file.FullName);
                                    //new Thread(new ParameterizedThreadStart((o) =>
                                    //{
                                    //    Thread.Sleep(3000);
                                    //    string filename = o as string;
                                    //    bool isbreak = false;
                                    //    while (!isbreak)
                                    //    {
                                    //        try
                                    //        {
                                    //            Thread.Sleep(1000);
                                    //            File.Delete(filename);
                                    //            isbreak = true;
                                    //        }
                                    //        catch (Exception ex) { }
                                    //    }
                                    //}))
                                    //{ IsBackground = true }.Start(file.FullName);
                                }
                                System.GC.Collect();
                            };
                            floderMonitors.Add(floder, monitor);
                        }
                    }
                }
            }
            else
            {
                foreach (var monitor in floderMonitors)
                {
                    monitor.Value.Dispose();
                }
                floderMonitors.Clear();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!loaded)
            {
                this.txtFloderChangeClearPath.Text = ConfigBLL.GetConfig<String>("MonitorFiles.txtFloderChangeClearPath");
                if (ConfigBLL.GetConfig<String>("MonitorFiles.chkFloderChangeClearProcess") == "True")
                { this.chkFloderChangeClearProcess.IsChecked = true; }


                loaded = true;
            }
        }
    }
}
