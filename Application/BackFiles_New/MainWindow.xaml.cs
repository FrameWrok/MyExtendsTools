using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BackFiles_New.Models;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Web;
using System.IO;
using System.Text.RegularExpressions;
using ExtendsToolsForm.UserControls;
using System.Windows.Forms;

namespace BackFiles_New
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        NotifyIcon notifyIcon;
        WindowState wsl;
        bool isshowing = true;
        public MainWindow()
        {
            InitializeComponent();
            InitNotifyIcon();
            wsl = WindowState;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                //this.Hide();
                //this.ShowInTaskbar = false;
                isshowing = false;
            }
        }

        public void InitNotifyIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.BalloonTipText = "备份文件小工具";
            notifyIcon.Text = "双击打开文件备份小工具";
            notifyIcon.Visible = true;
            notifyIcon.Icon = new System.Drawing.Icon("icon.ico");//程序图标
            notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
            var notifyContextMenu = new System.Windows.Forms.ContextMenu();
            notifyContextMenu.MenuItems.Add(new System.Windows.Forms.MenuItem("任务栏隐藏", (sened, e) =>
            {
                this.Hide();
                this.ShowInTaskbar = false;
                this.isshowing = true;
                NotifyIcon_MouseDoubleClick(null, null);
            }));
            notifyContextMenu.MenuItems.Add(new System.Windows.Forms.MenuItem("还原", (sened, e) =>
            {
                this.isshowing = false;
                NotifyIcon_MouseDoubleClick(null, null);
            }));
            notifyContextMenu.MenuItems.Add(new System.Windows.Forms.MenuItem("最小化", (sened, e) =>
            {
                this.isshowing = false;
                NotifyIcon_MouseDoubleClick(null, null);
                this.WindowState = WindowState.Minimized;
            }));
            notifyContextMenu.MenuItems.Add(new System.Windows.Forms.MenuItem("最大化", (sened, e) =>
            {
                this.isshowing = false;
                NotifyIcon_MouseDoubleClick(null, null);
                this.WindowState = WindowState.Maximized;
            }));
            notifyContextMenu.MenuItems.Add(new System.Windows.Forms.MenuItem("退出", (sened, e) =>
            {
                this.Close();
            }));

            notifyIcon.ContextMenu = notifyContextMenu;
            this.notifyIcon.ShowBalloonTip(1000);
        }

        private void NotifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (isshowing)
            {
                //this.Hide();
                //this.ShowInTaskbar = false;
                this.WindowState = WindowState.Minimized;
                isshowing = false;
            }
            else
            {
                this.Show();
                this.ShowInTaskbar = true;
                this.Activate();
                this.WindowState = WindowState.Normal;
                isshowing = true;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
        }
    }
}
