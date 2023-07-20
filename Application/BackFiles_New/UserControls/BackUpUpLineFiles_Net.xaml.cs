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
using System.Diagnostics;
using BackFiles_New.BLL;

namespace BackFiles_New.UserControls
{
    /// <summary>
    /// BackUpUpLineFiles.xaml 的交互逻辑
    /// </summary>
    public partial class BackUpUpLineFiles_Net : UserControl
    {
        DataContractJsonSerializer serializer = null;
        Configs configs = new Configs() { BasePaths = new List<string>(), RootPath = new List<string>() };
        List<OperationRecord> operationRecord = new List<OperationRecord>();
        static string appdataDirectory = AppDomain.CurrentDomain.BaseDirectory;// + @"\BackFile\";
        static string conifgpath = appdataDirectory + @"BackFileConfig.json";
        public BackUpUpLineFiles_Net()
        {
            InitializeComponent();
            serializer = new DataContractJsonSerializer(typeof(Configs));
            initConfig(true);
        }
        #region 事件
        private void btnDelBasePath_Click(object sender, RoutedEventArgs e)
        {
            this.configs.BasePaths.Remove(this.ddlBasePath.Text.Trim());
            this.ddlBasePath.ItemsSource = this.configs.BasePaths;
            this.ddlBasePath.DataContext = this.configs.BasePaths;
            this.ddlBasePath.Text = "";
            if (this.configs.BasePaths.Count > 0)
                this.ddlBasePath.SelectedIndex = 0;

            saveConfig("");
        }
        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            CopyFile(false);
        }
        private void btnRecursionCopy_Click(object sender, RoutedEventArgs e)
        {
            CopyFile(true);
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            this.txtFileList.Text = "";
            this.txtFileList.SelectionBrush = new TextBox().SelectionBrush;
        }
        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.operationRecord.Count > 0)
            {
                var s = this.operationRecord[this.operationRecord.Count - 1];
                this.txtFileList.Text = s.FileList;
                this.ddlBasePath.SelectedValue = s.BasePath;
                this.ddlCopyToPath.SelectedValue = s.RootPath;
                this.operationRecord.RemoveAt(this.operationRecord.Count - 1);
            }
        }
        private void txtFileList_KeyUp(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
            {
                if (this.txtFileList.Text.Trim().Length > 0)
                {
                    this.txtFileList.Text = this.txtFileList.Text.Trim() + "\r\n";
                    this.txtFileList.SelectionStart = this.txtFileList.Text.Length;
                }
            }
            string filelistinput = this.txtFileList.Text.Trim().Replace("\\\\", "\\");
            if (!string.IsNullOrWhiteSpace(filelistinput))
            {
                List<string> filelist = filelistinput.Trim('\n', '\r', '\t').Split(new string[]
                        {
                        "\r\n"
                        }, StringSplitOptions.RemoveEmptyEntries).ToList();
                string fillter = filelist[0];
                foreach (var item in configs.BasePaths.OrderByDescending(p => p.Length).ToList())
                {
                    if (fillter.StartsWith(item))
                    {
                        this.ddlBasePath.SelectedValue = item;
                        break;
                    }
                }
            }
        }
        #endregion

        #region 复制文件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRecursion">是否递归</param>
        private void CopyFile(bool isRecursion)
        {
            string basepath = this.ddlBasePath.Text, copytopath = this.ddlCopyToPath.Text.Trim(), filelistinput = this.txtFileList.Text.Trim().Replace("\\\\", "\\");

            //MessageBox.Show(this.ddlBasePath.Text);
            if (string.IsNullOrEmpty(basepath))
            { MessageBox.Show("请输入 Base Path！"); this.ddlBasePath.Focus(); return; }
            if (string.IsNullOrEmpty(copytopath))
            { MessageBox.Show("请输入 Copy To Path！"); this.ddlCopyToPath.Focus(); return; }
            if (string.IsNullOrEmpty(filelistinput))
            { MessageBox.Show("请输入 要复制的文件！"); this.txtFileList.Focus(); return; }
            if (!Directory.Exists(basepath))
            { MessageBox.Show("Base Path 路径不存在！"); this.ddlBasePath.Focus(); return; }
            if (!Directory.Exists(copytopath))
                Directory.CreateDirectory(copytopath);


            ////string fileliststring = Regex.Replace(filelistinput, "(\\r+)|(\\t+)|(\\n+)", "", RegexOptions.IgnoreCase);
            StringBuilder errorlist = new StringBuilder();


            List<string> filelist = filelistinput.Trim('\n', '\r', '\t').Split(new string[]
                    {
                        "\r\n"
                    }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var item in filelist)
            {

                string fileitem = this.ddlBasePath.Text + item.Replace(this.ddlBasePath.Text, "");
                if (!Directory.Exists(fileitem) && !File.Exists(fileitem))
                {
                    errorlist.AppendLine(fileitem);
                    continue;
                }

                if (Directory.Exists(fileitem))
                    CopyFile(new DirectoryInfo(fileitem), isRecursion);

                if (File.Exists(fileitem))
                    CopyFile(new FileInfo(fileitem));
            }

            if (errorlist.Length > 0)
            {
                this.txtFileList.Text = errorlist.ToString().Trim();
                this.txtFileList.Select(0, this.txtFileList.Text.Length);
                this.txtFileList.SelectionBrush = Brushes.Red;
                this.txtFileList.Focus();
                MessageBox.Show("列表中文件复制失败！");
            }
            else
            {
                this.txtFileList.Text = "";
                if (this.cbOpenRootPath.IsChecked ?? false)
                    System.Diagnostics.Process.Start("explorer.exe", copytopath);
            }
            saveConfig(filelistinput);
        }
        private void CopyFile(DirectoryInfo directory, bool isRecursion)
        {
            List<FileInfo> filelist = new List<FileInfo>();
            System.IO.SearchOption searchOption = SearchOption.TopDirectoryOnly;
            if (isRecursion)
                searchOption = SearchOption.AllDirectories;

            filelist.AddRange(directory.GetFiles("*.aspx", searchOption));
            filelist.AddRange(directory.GetFiles("*.html", searchOption));
            filelist.AddRange(directory.GetFiles("*.cs", searchOption));
            filelist.AddRange(directory.GetFiles("*.inc", searchOption));
            filelist.AddRange(directory.GetFiles("*.js", searchOption));
            filelist.AddRange(directory.GetFiles("*.css", searchOption));
            filelist.AddRange(directory.GetFiles("*.ashx", searchOption));
            foreach (var file in filelist)
                CopyFile(file);
        }
        private void CopyFile(FileInfo file)
        {
            DirectoryInfo tobasepath = new DirectoryInfo(this.ddlCopyToPath.Text);
            string tofilepath = file.FullName.Replace(this.ddlBasePath.Text, tobasepath.FullName + "\\");
            if (!new FileInfo(tofilepath).Directory.Exists)
                new FileInfo(tofilepath).Directory.Create();
            if (File.Exists(tofilepath))
            {
                FileInfo fileInfo = new FileInfo(tofilepath);
                fileInfo.Attributes = FileAttributes.Normal;
            }
            file.CopyTo(tofilepath, true);
        }

        void SaveOpertionRecord(string basepath, string filelist, string rootpath)
        {
            if (operationRecord.FirstOrDefault(p => p.FileList == filelist) == null)
                operationRecord.Add(new OperationRecord() { FileList = filelist, RootPath = rootpath, BasePath = basepath });
        }
        #endregion

        #region 处理配置文件

        private void initConfig(bool isinitwindows = false)
        {
            configs = ConfigBLL.GetConfig<Configs>("Net_Configs") ?? new Configs() {  BasePaths=new List<string>(), RootPath=new List<string>()};
            if (this.configs.RootPath.Count == 0)
            { this.configs.RootPath.Add(string.Format(@"C:\Users\{0}\Desktop\update\", Environment.UserName)); }

            this.ddlBasePath.ItemsSource = this.configs.BasePaths;
            this.ddlCopyToPath.ItemsSource = this.configs.RootPath;
            if (this.configs.BasePaths.Count > 0)
                this.ddlBasePath.SelectedIndex = 0;
            if (this.configs.RootPath.Count > 0)
                this.ddlCopyToPath.SelectedIndex = 0;
            this.cbOpenRootPath.IsChecked = this.configs.OperationedOpenRootPath;
            this.txtTfsUserName.Text = this.configs.TfsUserName ?? "";
            this.txtTfsPwd.Text = this.configs.TfsPwd ?? "";
        }
        private void saveConfig(string filelist)
        {
            string basepath = this.ddlBasePath.Text.Trim().TrimEnd('\\').TrimEnd('/');
            string rootpath = this.ddlCopyToPath.Text.Trim().TrimEnd('\\').TrimEnd('/');
            SaveOpertionRecord(basepath, filelist, rootpath);

            this.configs.BasePaths.Remove(basepath);
            this.configs.BasePaths.Insert(0, basepath);
            this.configs.RootPath.Remove(rootpath);
            this.configs.RootPath.Insert(0, rootpath);
            this.configs.TfsUserName = this.txtTfsUserName.Text.Trim();
            this.configs.TfsPwd = this.txtTfsPwd.Text.Trim();
            this.configs.OperationedOpenRootPath = this.cbOpenRootPath.IsChecked ?? false;
            ConfigBLL.SaveConfig(this.configs, "Net_Configs");
            initConfig();

        }
        #endregion

        #region 命令提交文件        

        /// <summary>
        /// 获取要提交的文件
        /// </summary>
        /// <param name="filePattern"></param>
        /// <returns></returns>
        private List<string> GetCheckInFileList(string fileDirectory, List<string> filePattern)
        {
            var filelistinput = fileDirectory.Trim().Replace("\\\\", "\\");
            List<string> filelist = new List<string>();
            if (!string.IsNullOrEmpty(filelistinput))
            {
                List<string> inputfilelist = filelistinput.Trim('\n', '\r', '\t').Split(new string[]
                       {
                        "\r\n"
                       }, StringSplitOptions.RemoveEmptyEntries).ToList();


                foreach (var item in inputfilelist)
                {
                    if (File.Exists(item))
                        filelist.Add(item);
                    else
                    {
                        if (Directory.Exists(item))
                        {
                            foreach (var parrern in filePattern)
                            {
                                filelist.AddRange(Directory.GetFiles(item, parrern, SearchOption.AllDirectories));
                            }
                        }
                    }
                }
            }
            return filelist;
        }

        private void btnTfsCheckIn_Click(object sender, RoutedEventArgs e)
        {
            var filelistinput = this.txtFileList.Text.Trim().Replace("\\\\", "\\");
            if (!string.IsNullOrEmpty(filelistinput))
            {
                List<string> filelist = GetCheckInFileList(filelistinput, new List<string>() { "*.aspx", "*.html", "*.cs", "*.inc", "*.js", "*.ashx" });
                string command = "tf checkin " + string.Join(" ", filelist);
                if (this.txtCheckInComment.Text.Trim() != "")
                    command += string.Format(" /comment:\"{0}\"", this.txtCheckInComment.Text.Trim());
                if (this.txtTfsUserName.Text.Trim() != "")
                {
                    command += " /login:" + this.txtTfsUserName.Text;
                    if (!string.IsNullOrEmpty(this.txtTfsPwd.Text))
                        command += "," + this.txtTfsPwd.Text.Trim();
                }

                Clipboard.SetDataObject(command);
                MessageBox.Show("签入命令已复制到剪贴板，请打开vs 开发人员命令工具执行签入");
            }
        }

        private void btnGitCheckIn_Click(object sender, RoutedEventArgs e)
        {
            var filelistinput = this.txtFileList.Text.Trim().Replace("\\\\", "\\");
            if (!string.IsNullOrEmpty(filelistinput))
            {
                if (this.txtCheckInComment.Text.Trim() == "")
                {
                    MessageBox.Show("请输入提交注释");
                    this.txtCheckInComment.Focus();
                    return;
                }

                List<string> filelist = GetCheckInFileList(filelistinput, new List<string>() { "*.java", "*.html", "*.xml", "*.yml", "*.js", "*.properties" });

                var errorFile = filelist.Where(d => !d.StartsWith(this.ddlBasePath.Text.Trim())).ToList();
                if (errorFile.Count > 0)
                {
                    StringBuilder errorSB = new StringBuilder();
                    foreach (var err in errorFile)
                    {
                        errorSB.AppendLine(err);
                    }
                    MessageBox.Show("以下文件不属于该版本库：" + errorSB.ToString());
                    return;
                }

                string cmdworkPath = this.ddlBasePath.Text.Trim();
                List<string> paths = filelist.First().Replace(cmdworkPath, "").Trim('\\', '/').Split('\\').ToList();
                if (paths.Count < 1)
                    paths = filelist.First().Replace(cmdworkPath, "").Trim('\\', '/').Split('/').ToList();
                if (paths.Count < 1)
                {
                    MessageBox.Show("git文件列表有误");
                    return;
                }


                if ((cmdworkPath = getGitWorkPath(cmdworkPath)) == null && (cmdworkPath = getGitWorkPath(cmdworkPath + "\\" + paths[0])) == null && (cmdworkPath = getGitWorkPath(cmdworkPath + "\\" + paths[0] + "\\" + paths[1])) == null)
                {
                    MessageBox.Show(string.Format("未找到git版本库"));
                    return;
                }


                StringBuilder sbcommand = new StringBuilder();
                sbcommand.AppendLine("git add " + string.Join(" ", filelist));
                sbcommand.AppendLine(string.Format("git commit -m \"{0}\"", this.txtCheckInComment.Text.Trim()));
                if (this.cbGitPush.IsChecked.HasValue && this.cbGitPush.IsChecked.Value)
                    sbcommand.AppendLine("git push");
                Clipboard.SetDataObject(sbcommand.ToString());
                MessageBox.Show("签入命令已复制到剪贴板，请将命令复制到命令窗口中执行！");
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.WorkingDirectory = cmdworkPath;
                p.Start();
            }
        }
        /// <summary>
        /// 判断是否是git版本库
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string getGitWorkPath(string path)
        {
            if (!Directory.Exists(path + "\\.git"))
            {
                return null;
            }
            return path;
        }
        #endregion        
    }
}
