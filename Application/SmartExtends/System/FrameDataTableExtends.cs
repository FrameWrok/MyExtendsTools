/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System.Collections.Generic;
using System.Data;
using System.Text;
using FrameWork.Core.Office;

namespace System
{
    /// <summary>
    /// DataTable扩展类
    /// </summary>
    public static partial class FrameDataTableExtends
    {
        /// <summary>
        /// 获取该DataTable对应的 INSERT INTO 语句
        /// VALUES中可以Format进行格式化
        /// </summary>
        /// <param name="inputDataTable">数据表</param>
        /// <returns>生成的sql语句</returns>
        public static string GetInsertSql(this DataTable inputDataTable)
        {
            Dictionary<string, string> dicColumnType = GetTableColumnTypeDic(inputDataTable);
            StringBuilder columnString = new StringBuilder();
            columnString.Append("INSERT INTO " + inputDataTable.TableName + "( ");
            StringBuilder valuesString = new StringBuilder(" VALUES ( ");
            int index = -1;
            foreach (string columnName in dicColumnType.Keys)
            {
                index++;
                columnString.Append(columnName + ", ");
                switch (dicColumnType[columnName])
                {
                    case "SYSTEM.INT64":
                    case "SYSTEM.BOOLEAN":
                    case "SYSTEM.DOUBLE":
                    case "SYSTEM.INT32":
                    case "SYSTEM.DECIMAL":
                    case "SYSTEM.INT16":
                    case "SYSTEM.LONG":
                        {
                            valuesString.Append("{" + index.ToString() + "}, ");
                        }

                        break;
                    default:
                        {
                            valuesString.Append("'{" + index.ToString() + "}', ");
                        }

                        break;
                }
            }

            return columnString.ToString().Trim().TrimEnd(',') + " ) " + valuesString.ToString().Trim().TrimEnd(',') + " ) ";
        }

        /// <summary>
        /// 根据输入表获取数据的插入语句
        /// </summary>
        /// <param name="inputDataTable">输入表</param>
        /// <returns>数据的插入语句</returns>
        public static string GetInsertDataSql(this DataTable inputDataTable)
        {
            string insertSql = GetInsertSql(inputDataTable);
            StringBuilder insertDataSql = new StringBuilder();
            List<string> listArgs = null;
            foreach (DataRow row in inputDataTable.Rows)
            {
                listArgs = new List<string>();
                foreach (DataColumn c in inputDataTable.Columns)
                {
                    listArgs.Add(row[c].ToString());
                }

                insertDataSql.AppendFormat(insertSql, listArgs.ToArray());
            }

            return insertDataSql.ToString();
        }

        /// <summary>
        /// 获取该DataTable对应的 UPDATE 语句
        /// 值可以Format进行格式化
        /// </summary>
        /// <param name="inputDataTable">数据表</param>
        /// <returns>生成的sql语句</returns>
        public static string GetUpdateSql(this DataTable inputDataTable)
        {
            Dictionary<string, string> dicColumnType = GetTableColumnTypeDic(inputDataTable);
            StringBuilder updateString = new StringBuilder();
            updateString.Append("UPDATE " + inputDataTable.TableName + " SET ");
            int index = -1;
            foreach (string columnName in dicColumnType.Keys)
            {
                index++;
                updateString.Append(columnName + " = ");
                switch (dicColumnType[columnName])
                {
                    case "SYSTEM.INT64":
                    case "SYSTEM.BOOLEAN":
                    case "SYSTEM.DOUBLE":
                    case "SYSTEM.INT32":
                    case "SYSTEM.DECIMAL":
                    case "SYSTEM.INT16":
                    case "SYSTEM.LONG":
                        {
                            updateString.Append("{" + index.ToString() + "}, ");
                        }

                        break;
                    default:
                        {
                            updateString.Append("'{" + index.ToString() + "}', ");
                        }

                        break;
                }
            }

            return updateString.ToString().Trim().TrimEnd(',');
        }

        /// <summary>
        /// 将 DataTable 数据源导出到 Excel 
        /// </summary>
        /// <param name="dataSource">要导出的Datatabel</param>
        /// <param name="fileName">导出的文件名称</param>
        /// <param name="errorInfo">错误信息</param>
        public static void ToExcel(this DataTable dataSource, string fileName, ref string errorInfo)
        {
            Excel excel = new Excel(dataSource);
            excel.ExportToExcel(fileName, null, ref errorInfo);
        }

        /// <summary>
        /// 将 DataTable 数据源保存到本地路径，fileName为路径加文件名
        /// </summary>
        /// <param name="dataSource">要导出的Datatabel</param>
        /// <param name="fileName">导出的文件名称</param>
        /// <param name="errorInfo">错误信息</param>
        public static void SaveExcel(this DataTable dataSource, string fileName, ref string errorInfo)
        {
            Excel excel = new Excel(dataSource);
            excel.Save(fileName, null, ref errorInfo);
        }

        /// <summary>
        /// 获取输入表的列名以及列的数据类型
        /// </summary>
        /// <param name="inputDataTable">输入表</param>
        /// <returns>列名以及数据类型字典值 </returns>
        private static Dictionary<string, string> GetTableColumnTypeDic(DataTable inputDataTable)
        {
            Dictionary<string, string> dicColumnType = new Dictionary<string, string>();
            foreach (DataColumn column in inputDataTable.Columns)
            {
                dicColumnType.Add(column.ColumnName, column.DataType.ToString().ToUpper());
            }

            return dicColumnType;
        }
    }
}
