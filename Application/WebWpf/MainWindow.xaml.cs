using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
using Microsoft.Web.WebView2.Wpf;
using System.IO;
using System.Threading;

namespace WebWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //web.Source = new Uri("https://www.w3school.com.cn/jquery/ajax_ajaxcomplete.asp");
            web.Source = new Uri("https://app.che168.com/csy/web/v270/spa/report/show?vincode=WDDUX6EB7LA518220&fromtype=22&paramkey1=UwyEcS1gFLlTkLJ1bMuf6fibx1WBufM2&paramkey2=b0yycc6MOFGbjYeca4KAtiakl+YLR7Ib&paramkey3=RrDIkbPdRwyUKdfBoY92EnYty+cu98Jz&createtime=2022-06-14%2016:28:36&cid=110100&userkey=964B4E4C038907E6F3FEC3C096B098BA69921CCC1BFE2AEDFE15483FB59A681E3DB116CE4119E1E50338D4E30F3A689624131FBCAB9B224934ED8656348ED8D6B186404F55E523AC95A8F6E7729015FC47F1D918F610FC31DA9EB917742C3E6EA590694639E22C9F68B4E30E7DE0578A83B719CED87F9ECCA98A5FB263CFF9BD&reporttype=1&platform=pc");
            web.CoreWebView2InitializationCompleted += Web_CoreWebView2InitializationCompleted;
        }

        private void btn_openurl_Click(object sender, RoutedEventArgs e)
        {
            //web.Source = new Uri("https://www.w3school.com.cn/jquery/ajax_ajaxcomplete.asp");
            web.Source = new Uri("https://app.che168.com/csy/web/v270/spa/report/show?vincode=WDDUX6EB7LA518220&fromtype=22&paramkey1=UwyEcS1gFLlTkLJ1bMuf6fibx1WBufM2&paramkey2=b0yycc6MOFGbjYeca4KAtiakl+YLR7Ib&paramkey3=RrDIkbPdRwyUKdfBoY92EnYty+cu98Jz&createtime=2022-06-14%2016:28:36&cid=110100&userkey=964B4E4C038907E6F3FEC3C096B098BA69921CCC1BFE2AEDFE15483FB59A681E3DB116CE4119E1E50338D4E30F3A689624131FBCAB9B224934ED8656348ED8D6B186404F55E523AC95A8F6E7729015FC47F1D918F610FC31DA9EB917742C3E6EA590694639E22C9F68B4E30E7DE0578A83B719CED87F9ECCA98A5FB263CFF9BD&reporttype=1&platform=pc");
            web.CoreWebView2InitializationCompleted += Web_CoreWebView2InitializationCompleted;
        }

        private void Web_CoreWebView2InitializationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            web.CoreWebView2.DOMContentLoaded += CoreWebView2_DOMContentLoaded;
        }

        private async void CoreWebView2_DOMContentLoaded(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2DOMContentLoadedEventArgs e)
        {
            web.CoreWebView2.OpenDevToolsWindow();
            string js = await File.ReadAllTextAsync(Environment.CurrentDirectory + "/html2canvas.min.js");
            var re = await web.CoreWebView2.ExecuteScriptAsync(js);
            

            Thread.Sleep(10000);
            var cjs = await File.ReadAllTextAsync(Environment.CurrentDirectory + "/Cap.js");
            re = await web.CoreWebView2.ExecuteScriptAsync(cjs);
            Thread.Sleep(1000);
            string imgbase64 = await web.CoreWebView2.ExecuteScriptAsync("carblobbase64");
            while (imgbase64 == "\"123\"" || imgbase64 == "data:,")
            {
                imgbase64 = await web.CoreWebView2.ExecuteScriptAsync("carblobbase64");                
                await web.CoreWebView2.ExecuteScriptAsync("generate()");                
            }
            SaveImage(imgbase64.Trim('"'));
            //Environment.Exit(0);
        }

        private async void btn_regjs_Click(object sender, RoutedEventArgs e)
        {
            string js = await File.ReadAllTextAsync(Environment.CurrentDirectory + "/html2canvas.min.js");
            var re = await web.CoreWebView2.ExecuteScriptAsync(js);
            var cjs = await File.ReadAllTextAsync(Environment.CurrentDirectory + "/Cap.js");
            re = await web.CoreWebView2.ExecuteScriptAsync(cjs);
            re = await web.CoreWebView2.ExecuteScriptAsync("console.log(123);");
            //var sh = await web.CoreWebView2.ExecuteScriptAsync("$(document).height()");
            //var height = int.Parse(sh);
            //web.CoreWebView2.OpenDevToolsWindow();
            //string s = await web.CoreWebView2.ExecuteScriptAsync("carblobbase64");
            //web.CoreWebView2.OpenDevToolsWindow();
            //var html = await web.ExecuteScriptAsync("document.getElementsByTagName('html')[0].innerHTML");
            //web.Width = Width; web.Height = Height;
        }

        private async void btn_saveimg_Click(object sender, RoutedEventArgs e)
        {
            string s = await web.CoreWebView2.ExecuteScriptAsync("carblobbase64");
            var height = int.Parse(await web.CoreWebView2.ExecuteScriptAsync("$(document).height()"));
            var width = int.Parse(await web.CoreWebView2.ExecuteScriptAsync("$(document).width()"));
            var topLeftCorner = web.PointToScreen(new System.Windows.Point(0, 0));
            var topLeftGdiPoint = new System.Drawing.Point((int)topLeftCorner.X, (int)topLeftCorner.Y);
            var size = new System.Drawing.Size(width, height);

            var screenShot = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(screenShot))
            {
                graphics.CopyFromScreen(topLeftGdiPoint, new System.Drawing.Point(),
                    size, CopyPixelOperation.SourceCopy);
            }

            screenShot.Save(@"D:\screenshot.png", ImageFormat.Png);
        }
        public async void SaveImage(string imgbase64)
        {
            string imgSuffix = ".jpeg";
            if (imgbase64.StartsWith("data:image/png;base64"))
                imgSuffix = ".png";

            byte[] imgbuffer = Convert.FromBase64String(imgbase64.Replace("data:image/png;base64,", "").Replace("data:image/jpeg;base64,", ""));
            await File.WriteAllBytesAsync(Environment.CurrentDirectory + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + imgSuffix, imgbuffer);
        }
    }


}
