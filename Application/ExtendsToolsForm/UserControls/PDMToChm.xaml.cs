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

using ExtendsToolsForm.Models.DbModels;
using System.Collections.ObjectModel;
using System.IO;
using ExtendsToolsForm.BLL.CHMUtil;
using ExtendsToolsForm.Models.PdmToChm;
using ExtendsToolsForm.BLL;
using System.Threading;

namespace ExtendsToolsForm.UserControls
{
    /// <summary>
    /// PDMToChm.xaml 的交互逻辑
    /// </summary>
    public partial class PDMToChm : UserControl
    {
        public ObservableCollection<DbTableModels> list = new ObservableCollection<DbTableModels>();
        public PDMToChm()
        {
            InitializeComponent();
        }
        private void btnFilePDMSelect_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
            fileDialog.Filter = "(*.pdm)|*.pdm";
            fileDialog.Multiselect = true;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtPdmList.Text = string.Join("\r\n", fileDialog.FileNames);
                this.txtChmFile.Text = fileDialog.FileNames[0].Replace(".pdm", ".chm");

            }
        }

        private void btnGeneratePDM_Click(object sender, RoutedEventArgs e)
        {
            PdmToChm(this.txtPdmList.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList());
        }

        #region pdm to chm
        public void PdmToChm(List<string> pdmpaths)
        {
            new Thread(new ParameterizedThreadStart(this.CrateCHM)) { IsBackground = true }.Start(pdmpaths.ToList());
        }
        private void CrateCHM(object phs)
        {
            try
            {
                if (phs == null)
                    return;

                List<string> pdmPaths = phs as List<string>;
                var lstTabs = GetTables(pdmPaths);


                string filename = Path.GetFileNameWithoutExtension(pdmPaths[0]);
                string defaultpage = filename + ".html";
                string fullPath = Path.GetFullPath("tmp");
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
                else
                {
                    Directory.Delete(fullPath, true);
                    Directory.CreateDirectory(fullPath);
                }
                ChmHtmlHelper.CreateDirHtml(filename, lstTabs, Path.Combine(fullPath, defaultpage));
                fullPath = System.IO.Path.GetFullPath("tmp\\表结构");
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
                BLL.CHMUtil.ChmHtmlHelper.CreateHtml(lstTabs, fullPath, defaultpage);
                ChmHelp chmHelp = new ChmHelp();
                chmHelp.DefaultPage = defaultpage;
                this.txtChmFile.Dispatcher.Invoke(new Action(delegate
                {
                    string chmfile = this.txtChmFile.Text;
                    chmfile = chmfile.TrimEnd(new char[] { '.', 'c', 'h', 'm', 'C', 'H', 'M' });
                    chmHelp.Title = chmfile;
                }));

                chmHelp.ChmFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), chmHelp.Title + ".chm");
                chmHelp.SourcePath = "tmp";
                chmHelp.Compile();
                MessageBox.Show("生成成功！文件路径：" + chmHelp.ChmFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static IList<PdmTableInfoModel> GetTables(List<string> pdmPaths)
        {
            List<PdmTableInfoModel> list = new List<PdmTableInfoModel>();

            for (int i = 0; i < pdmPaths.Count; i++)
            {
                string text = pdmPaths[i];
                if (File.Exists(text))
                {
                    PdmModel pdmModels = PdmReaderBLL.PdmReader(text);
                    list.AddRange(pdmModels.Tables);
                }
            }
            return (from t in list
                    orderby t.Code
                    select t).ToList<PdmTableInfoModel>();
        }
        #endregion
    }
}
