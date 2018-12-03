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
using ExtendsToolsForm.BLL;
using ExtendsToolsForm.BLL.CHMUtil;
using ExtendsToolsForm.Models.DbModels;
using ExtendsToolsForm.Models.PdmToChm;

namespace ExtendsToolsForm.UserControls
{

    /// <summary>
    /// DbToChm.xaml 的交互逻辑
    /// </summary>
    public partial class DbToChm : UserControl
    {        
        public string sqlconnection = "", filename = "";
        public DbToChm()
        {
            InitializeComponent();
        }

        private void btnGenerateDbToChm_Click(object sender, RoutedEventArgs e)
        {
            sqlconnection = "Data Source={0};Persist Security Info=True;User ID={1};Password={2};Initial Catalog={3};".Formats(this.dbhost.Text.Trim(), this.dbuser.Text.Trim(), this.dbpwd.Text.Trim(), this.dbname.Text.Trim());
            filename = this.txtFileName.Text.Trim();
            new Thread(new ParameterizedThreadStart(this.CrateCHM)) { IsBackground = true }.Start(this.txtTableList.Text.Split(',').ToList());
        }

        #region 生成chm

        private void CrateCHM(object phs)
        {
            try
            {
                if (phs == null)
                    return;

                List<string> pdmPaths = phs as List<string>;

                var lstTabs = GetTables(pdmPaths);
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
        private IList<PdmTableInfoModel> GetTables(List<string> tableNames)
        {
            List<PdmTableInfoModel> list = new List<PdmTableInfoModel>();
            foreach (var item in tableNames)
            {
                string tableName, tableDescription;
                List<DbTableColumnSchema> dbColumnList = DbSchemaBLL.GetDbTableColumnSchema(item, sqlconnection, out tableName, out tableDescription);
                if (dbColumnList == null)
                {
                    throw new Exception("table {0} 不存在,请确认表名及数据库名是否正确！".Formats(item));
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
