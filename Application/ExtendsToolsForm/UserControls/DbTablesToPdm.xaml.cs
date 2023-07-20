using ExtendsToolsForm.BLL;
using ExtendsToolsForm.BLL.CHMUtil;
using ExtendsToolsForm.Models.DbModels;
using ExtendsToolsForm.Models.PdmToChm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

namespace ExtendsToolsForm.UserControls
{
    /// <summary>
    /// DbTablesToPdm.xaml 的交互逻辑
    /// </summary>
    public partial class DbTablesToPdm : UserControl
    {
        public ObservableCollection<DbTableModels> dbExclueList = new ObservableCollection<DbTableModels>();
        public ObservableCollection<DbTableModels> dbInclueList = new ObservableCollection<DbTableModels>();
        public string saveFolder = "", chmFilePath = "", bussinessName = "";
        public DbConnectionModel sqlconnection { get; set; }
        public DbTablesToPdm()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.dgExclueTables.ItemsSource = dbExclueList;
            this.dginclueTables.ItemsSource = dbInclueList;
        }

        #region 选择表处理
        /// <summary>
        /// 表过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilterDb_Click(object sender, RoutedEventArgs e)
        {
            getSqlConnection();
            var tableList = DbSchemaBLL.GetDbTablesList(this.txtDbLikeName.Text.Trim(), sqlconnection.ToString());
            if (tableList.Count == 0)
            {
                MessageBox.Show("未查询到符合条件的表！");
                return;
            }
            dbExclueList.Clear();
            foreach (var table in tableList)
            {
                table.sqlconnection = sqlconnection;
                if (this.dbInclueList.FirstOrDefault(p => p.Name == table.Name) != null)
                    table.IsChecked = true;
                dbExclueList.Add(table);
            }

        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddIsCheckdTables_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == "btnAddALLTables")
            {
                foreach (var item in dbExclueList)
                {
                    item.IsChecked = true;
                }
            }

            var checkList = dbExclueList.Where(p => p.IsChecked).ToList();
            foreach (var item in checkList)
            {
                if (dbInclueList.FirstOrDefault(p => p.Name == item.Name) == null)
                    dbInclueList.Add(item);
                dbExclueList.Remove(item);
            }
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveIsCheckdTables_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == "btnRemoveALLTables")
            {
                foreach (var item in dbInclueList)
                {
                    item.IsChecked = true;
                }
            }

            var checkList = dbInclueList.Where(p => p.IsChecked).ToList();
            foreach (var item in checkList)
            {
                if (dbExclueList.FirstOrDefault(p => p.Name == item.Name) == null)
                    dbExclueList.Add(item);
                dbInclueList.Remove(item);
            }
        }
        /// <summary>
        /// 已选择列表全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckInclueAllSelected_Click(object sender, RoutedEventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            foreach (var item in dbInclueList)
            {
                item.IsChecked = ck.IsChecked ?? false;
            }
        }
        /// <summary>
        /// 未选择列表全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckExclueAllSelected_Click(object sender, RoutedEventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            foreach (var item in dbExclueList)
            {
                item.IsChecked = ck.IsChecked ?? false;
            }
        }

        #endregion

        public DbConnectionModel getSqlConnection()
        {
            return sqlconnection = new DbConnectionModel() { host = this.dbhost.Text.Trim(), dbname = this.dbname.Text.Trim(), pwd = this.dbpwd.Text.Trim(), user = this.dbuser.Text.Trim() };
        }

        #region 生成chm

        private void BtnSelectFloder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                saveFolder = folderDialog.SelectedPath.TrimEnd('\\', '/') + "\\";
                this.txtSaveFolder.Text = saveFolder + (this.txtBussinessName.Text.Trim().Length > 0 ? this.txtBussinessName.Text.Trim() + ".chm" : "");
            }
        }
        private void txtBussinessName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.txtSaveFolder.Text.Trim().Length > 0)
            {
                this.txtSaveFolder.Text = saveFolder + (this.txtBussinessName.Text.Trim().Length > 0 ? this.txtBussinessName.Text.Trim() + ".chm" : "");
            }
        }

        /// <summary>
        /// 生成chm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateDbToChm_Click(object sender, RoutedEventArgs e)
        {
            chmFilePath = this.txtSaveFolder.Text.Trim();
            bussinessName = this.txtBussinessName.Text.Trim();
            new Thread(new ParameterizedThreadStart(this.CrateCHM)) { IsBackground = true }.Start(dbInclueList);
        }

        private void CrateCHM(object phs)
        {
            try
            {
                if (phs == null)
                    return;

                ObservableCollection<DbTableModels> tablelist = phs as ObservableCollection<DbTableModels>;

                var lstTabs = GetTables(tablelist);
                string defaultpage = bussinessName + ".html";

                string fullrootPath = System.IO.Path.GetFullPath(saveFolder + "tmp");
                if (!Directory.Exists(fullrootPath))
                    Directory.CreateDirectory(fullrootPath);
                else
                {
                    Directory.Delete(fullrootPath, true);
                    Directory.CreateDirectory(fullrootPath);
                }
                ChmHtmlHelper.CreateDirHtml(bussinessName, lstTabs, System.IO.Path.Combine(fullrootPath, defaultpage));
                string fullPath = System.IO.Path.GetFullPath(fullrootPath + "\\" + bussinessName);
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
                BLL.CHMUtil.ChmHtmlHelper.CreateHtml(lstTabs, fullPath, defaultpage);
                ChmHelp chmHelp = new ChmHelp();
                chmHelp.DefaultPage = defaultpage;


                chmHelp.Title = bussinessName;
                chmHelp.ChmFileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), chmHelp.Title + ".chm");
                if (bussinessName.IsNotNullOrEmptyOrBlank())
                    chmHelp.ChmFileName = System.IO.Path.Combine(chmFilePath);

                chmHelp.SourcePath = fullrootPath;
                chmHelp.Compile();
                this.txtSaveFolder.Dispatcher.Invoke(() =>
                {

                });

                MessageBox.Show("生成成功！文件路径：" + chmHelp.ChmFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private IList<PdmTableInfoModel> GetTables(ObservableCollection<DbTableModels> tableLists)
        {
            List<PdmTableInfoModel> list = new List<PdmTableInfoModel>();
            foreach (var item in tableLists)
            {
                string tableName, tableDescription;
                List<DbTableColumnSchema> dbColumnList = DbSchemaBLL.GetDbTableColumnSchema(item.Name, item.sqlconnection.ToString(), out tableName, out tableDescription);
                if (dbColumnList == null)
                {
                    throw new Exception("table {0} 不存在,请确认表名及数据库名是否正确！".Formats(item.Name));
                }
                PdmTableInfoModel tbmodel = new PdmTableInfoModel() { Code = tableName, Comment = tableDescription, Name = tableDescription };
                dbColumnList.ForEach(p =>
                {
                    PdmColumnInfoModel pcim = new PdmColumnInfoModel(tbmodel);
                    pcim.Code = p.columnName;
                    pcim.Comment = p.Description;
                    pcim.DataType = GetDataType(p);
                    pcim.ColumnId = p.columnName;
                    if (p.isPrimarykey == 1)
                    {
                        var keymodel = new PdmKeyModel(tbmodel) { KeyId = p.columnName };
                        keymodel.AddColumnObjCode(p.columnName);
                        tbmodel.Keys.Add(keymodel);
                        tbmodel.PrimaryKeyRefCode = p.columnName;
                    }
                    pcim.Identity = p.isIdentity == 1;
                    pcim.Mandatory = p.isAllowNull == 1;
                    tbmodel.Columns.Add(pcim);
                });
                list.Add(tbmodel);
            }
            return list;
        }

        public string GetDataType(DbTableColumnSchema column)
        {
            string dbtype = column.dbtype;
            switch (column.dbtype)
            {
                case "varchar":
                case "nvarchar":
                    return dbtype + "({0})".Formats(column.length);
                case "decimal":
                    return dbtype + "({0},{1})".Formats(column.length, column.precision);
            }
            return dbtype;
        }

        #endregion


    }
}
