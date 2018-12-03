using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace System
{
    public static class FrameDataTableExtends
    {
        /// <summary>
        /// 由Data反射为 List<Entity>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourcesTable"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable sourcesTable) where T : new()
        {
            var list = new List<T>();
            PropertyInfo[] propertyInfos = null;
            Type type = typeof(T);
            foreach (DataRow row in sourcesTable.Rows)
            {
                T item = Activator.CreateInstance<T>();
                DataRowToEntity(type, ref item, row, ref propertyInfos);
                list.Add(item);
            }

            return list;
        }
        /// <summary>
        /// 将DataRow映射为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T ToObject<T>(this DataRow row)
        {
            PropertyInfo[] propertyInfos = null;
            T item = Activator.CreateInstance<T>();
            Type type = typeof(T);
            DataRowToEntity(type, ref item, row, ref propertyInfos);
            return item;
        }
        private static void DataRowToEntity<T>(Type type, ref T item, DataRow dataRow, ref PropertyInfo[] propertyInfos)
        {
            foreach (PropertyInfo propertyInfo in propertyInfos = propertyInfos != null ? propertyInfos : type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (dataRow.Table.Columns.Contains(propertyInfo.Name))
                {
                    object value = dataRow[propertyInfo.Name];
                    if (value != null)
                    {
                        propertyInfo.SetValue(item, (value.GetType() == propertyInfo.PropertyType ? value : value.ConvertTo(propertyInfo.PropertyType)), null);
                    }
                }
            }
        }

        /// <summary>
        /// 将 DataTable 转化为csv字符串
        /// </summary>
        /// <param name="sourcesTable"></param>
        /// <param name="header">name1,name2,name3</param>
        /// <param name="columns">columnname1,columnname2,columnname3</param>
        /// <returns></returns>
        public static string ToCsvString(this DataTable sourcesTable, string header, string columns)
        {
            List<string> ls = columns.Split(',').ToList();
            StringBuilder sb = new StringBuilder(header);
            sb.AppendLine();
            List<string> csvrowdata = new List<string>();
            foreach (DataRow row in sourcesTable.Rows)
            {
                csvrowdata = new List<string>();
                foreach (var item in ls)
                {
                    csvrowdata.Add((!sourcesTable.Columns.Contains(item) || row[item] == null) ? "" : row[item].ToString().Replace(",", "，"));
                }
                sb.AppendLine(csvrowdata.Join(","));
            }

            return sb.ToString();
        }
        /// <summary>
        /// 将 DataTable 转化为csv字符串并导出到输出流
        /// </summary>
        /// <param name="sourcesTable"></param>
        /// <param name="response"></param>
        /// <param name="filename">文件名称，不带后缀名</param>
        /// <param name="header">name1,name2,name3</param>
        /// <param name="columns">columnname1,columnname2,columnname3</param>
        /// <returns></returns>
        public static void ToCsvString(this DataTable sourcesTable, HttpResponse response, string filename, string header, string columns)
        {
            response.Clear();
            response.Buffer = true;
            response.Charset = "GB2312";
            response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".csv");
            response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            response.ContentType = "application/ms-excel";
            response.Write(sourcesTable.ToCsvString(header, columns));
            response.End();
        }
    }
}
