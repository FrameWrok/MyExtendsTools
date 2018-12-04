using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Microsoft.VisualBasic.ApplicationServices;

namespace BackFiles_New
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>    
    public partial class App : Application
    {
        public bool IsShowing = false;
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void Main(string[] a)
        {
            SingleMainWindow singleMainWindow = new SingleMainWindow();
            singleMainWindow.Run(a);
        }
    }
    public class SingleMainWindow : Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase
    {
        App a;
        public SingleMainWindow()
        {
            this.IsSingleInstance = true;
        }
        protected override bool OnStartup(Microsoft.VisualBasic.ApplicationServices.StartupEventArgs eventArgs)
        {
            a = new App();
            a.InitializeComponent();
            a.Run();
            return false;
        }
        protected override void OnStartupNextInstance(Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs eventArgs)
        {

        }
    }
}
