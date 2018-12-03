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
        public string sqlconnection = "", filename = "";
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
            string sqlconnection = getSqlConnection();
            var tableList = DbSchemaBLL.GetDbTablesList(this.txtDbLikeName.Text.Trim(), getSqlConnection());
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

        public string getSqlConnection()
        {
            return sqlconnection = "Data Source={0};Persist Security Info=True;User ID={1};Password={2};Initial Catalog={3};".Formats(this.dbhost.Text.Trim(), this.dbuser.Text.Trim(), this.dbpwd.Text.Trim(), this.dbname.Text.Trim());
        }

        #region 生成chm
        private void btnGenerateDbToChm_Click(object sender, RoutedEventArgs e)
        {
            sqlconnection = getSqlConnection();
            filename = this.txtFileName.Text.Trim();
            new Thread(new ParameterizedThreadStart(this.CrateCHM)) { IsBackground = true }.Start(dbInclueList);
        }

        private void CrateCHM(object phs)
        {
            try
            {
                if (phs == null)
                    return;

                ObservableCollection<DbTableModels> tablelist = phs as  ObservableCollection<DbTableModels>;

                var lstTabs = GetTables(tablelist);
                string defaultpage = filename + ".html";
                string fullPath = System.IO.Path.GetFullPath("tmp");

                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);
                else
                {
                    Directory.Delete(fullPath, true);
                    Directory.CreateDirectory(fullPath);
                }
                ChmHtmlHelper.CreateDirHtml(filename, lstTabs, System.IO.Path.Combine(fullPath, defaultpage));
                fullPath = System.IO.Path.GetFullPath("tmp\\表结构");
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
                BLL.CHMUtil.ChmHtmlHelper.CreateHtml(lstTabs, fullPath, defaultpage);
                ChmHelp chmHelp = new ChmHelp();
                chmHelp.DefaultPage = defaultpage;


                chmHelp.Title = filename;

                chmHelp.ChmFileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), chmHelp.Title + ".chm");
                chmHelp.SourcePath = "tmp";
                chmHelp.Compile();
                this.txtDbToChmFile.Dispatcher.Invoke(() =>
                {
                    this.txtDbToChmFile.Text = chmHelp.ChmFileName;
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
                List<DbTableColumnSchema> dbColumnList = DbSchemaBLL.GetDbTableColumnSchema(item.Name, item.sqlconnection, out tableName, out tableDescription);
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
