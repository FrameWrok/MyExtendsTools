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
    /// BackUpUpLineFiles_Javaxaml.xaml 的交互逻辑
    /// </summary>
    public partial class BackUpUpLineFiles_Java : UserControl
    {
        DataContractJsonSerializer serializer = null;
        Dictionary<string, string> BindConfig = new Dictionary<string, string>();
        List<OperationRecord> operationRecord = new List<OperationRecord>();
        List<string> BasePaths = new List<string>();
        List<string> RootPath = new List<string>();
        public BackUpUpLineFiles_Java()
        {
            InitializeComponent();
            InitConfig();
        }
        private void btnDelBasePath_Click(object sender, RoutedEventArgs e)
        {
            this.BasePaths.Remove(this.ddlProjectBasePath.Text.Trim());
            this.ddlProjectBasePath.ItemsSource = this.BasePaths;
            this.ddlProjectBasePath.DataContext = this.BasePaths;
            this.ddlProjectBasePath.Text = "";
            if (this.BasePaths.Count > 0)
                this.ddlProjectBasePath.SelectedIndex = 0;
            saveConfig();
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
                this.ddlProjectBasePath.SelectedValue = s.BasePath;
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
                foreach (var item in this.BasePaths.OrderByDescending(p => p.Length).ToList())
                {
                    if (fillter.StartsWith(item))
                    {
                        this.ddlProjectBasePath.SelectedValue = item;
                        break;
                    }
                }
            }
        }
        #region 文件处理
        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            CopyFile(false);
        }
        private void btnRecursionCopy_Click(object sender, RoutedEventArgs e)
        {
            CopyFile(true);
        }
        public void CopyFile(bool isRecursion)
        {
            string basepath = this.ddlProjectBasePath.Text, copytopath = this.ddlCopyToPath.Text.Trim(), filelistinput = this.txtFileList.Text.Trim().Replace("\\\\", "\\");

            //MessageBox.Show(this.ddlBasePath.Text);
            if (string.IsNullOrEmpty(basepath))
            { MessageBox.Show("请输入spring boot根目录！"); this.ddlProjectBasePath.Focus(); return; }
            if (string.IsNullOrEmpty(copytopath))
            { MessageBox.Show("请输入 Copy To Path！"); this.ddlCopyToPath.Focus(); return; }
            if (string.IsNullOrEmpty(filelistinput))
            { MessageBox.Show("请输入 要复制的文件！"); this.txtFileList.Focus(); return; }
            if (!Directory.Exists(basepath))
            { MessageBox.Show("spring boot根目录路径不存在！"); this.ddlProjectBasePath.Focus(); return; }
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
                string fileitem = basepath + item.Replace(basepath, "");
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
                this.txtFileList.Text = "";
            saveConfig(filelistinput);
        }

        private void CopyFile(DirectoryInfo directory, bool isRecursion)
        {
            List<FileInfo> filelist = new List<FileInfo>();
            System.IO.SearchOption searchOption = SearchOption.TopDirectoryOnly;
            if (isRecursion)
                searchOption = SearchOption.AllDirectories;

            filelist.AddRange(directory.GetFiles("*.java", searchOption));
            filelist.AddRange(directory.GetFiles("*.html", searchOption));
            filelist.AddRange(directory.GetFiles("*.properties", searchOption));
            filelist.AddRange(directory.GetFiles("*.xml", searchOption));
            filelist.AddRange(directory.GetFiles("*.js", searchOption));
            filelist.AddRange(directory.GetFiles("*.css", searchOption));
            filelist.AddRange(directory.GetFiles("*.yml", searchOption));
            foreach (var file in filelist)
                CopyFile(file);
        }
        private void CopyFile(FileInfo file)
        {
            string basepath = this.ddlProjectBasePath.Text, copytopath = this.ddlCopyToPath.Text.Trim();

            switch( file.Extension.ToString())
            {
                case ".java":
                    {
                        file = new FileInfo(basepath + @"\target\classes\" + file.FullName.Replace(basepath + @"\src\main\java\", "").Replace(".java", ".class"));
                        if (file.Name.IndexOf("$") < 0)
                        {
                            var extendsFiles = file.Directory.GetFiles(file.Name.Replace(".class", "$*.class"));
                            foreach (var extendfile in extendsFiles)
                            {
                                CopyFile(extendfile);
                            }
                        }
                    }
                    break;
                case ".class":
                    break;
                default:
                    file = new FileInfo(basepath + @"\target\classes\" + file.FullName.Replace(basepath + @"\src\main\resources\", ""));
                    break;

            }   

            DirectoryInfo tobasepath = new DirectoryInfo(this.ddlCopyToPath.Text);
            string tofilepath = file.FullName.Replace(basepath + @"\target\classes\", tobasepath.FullName + "\\" + (new DirectoryInfo(basepath).Name) + "\\");
            if (!new FileInfo(tofilepath).Directory.Exists)
                new FileInfo(tofilepath).Directory.Create();
            if (File.Exists(tofilepath))
            {
                FileInfo fileInfo = new FileInfo(tofilepath);
                fileInfo.Attributes = FileAttributes.Normal;
            }
            file.CopyTo(tofilepath, true);
        }
        private void CopyExtensClass()
        {

        }

        #endregion

        #region config 保存

        private void saveConfig(string filelist = "")
        {
            string basepath = this.ddlProjectBasePath.Text.Trim().TrimEnd('\\').TrimEnd('/');
            string rootpath = this.ddlCopyToPath.Text.Trim().TrimEnd('\\').TrimEnd('/');
            SaveOpertionRecord(basepath, filelist, rootpath);
            this.BasePaths.Remove(basepath);
            this.BasePaths.Insert(0, basepath);
            this.RootPath.Remove(rootpath);
            this.RootPath.Insert(0, rootpath);
            ConfigBLL.SaveConfig(BasePaths, "Java_ProcectBasePath");
            ConfigBLL.SaveConfig(RootPath, "Java_ProcectRootPath");
            //ConfigBLL.SaveConfig(BindConfig, "Java_BindConfigs");

        }
        private void InitConfig()
        {
            BasePaths = ConfigBLL.GetConfig<List<string>>("Java_ProcectBasePath") ?? new List<string>() { };
            RootPath = ConfigBLL.GetConfig<List<string>>("Java_ProcectRootPath") ?? new List<string>();

            this.ddlProjectBasePath.ItemsSource = this.BasePaths;
            this.ddlProjectBasePath.DataContext = this.BasePaths;
            if (this.BasePaths.Count > 0)
                this.ddlProjectBasePath.SelectedIndex = 0;

            this.ddlCopyToPath.ItemsSource = this.RootPath;
            this.ddlCopyToPath.DataContext = this.RootPath;
            if (this.RootPath.Count > 0)
                this.ddlCopyToPath.SelectedIndex = 0;
        }
        void SaveOpertionRecord(string basepath, string filelist, string rootpath)
        {
            if (operationRecord.FirstOrDefault(p => p.FileList == filelist) == null)
                operationRecord.Add(new OperationRecord() { FileList = filelist, RootPath = rootpath, BasePath = basepath });
        }
        #endregion
    }
}
