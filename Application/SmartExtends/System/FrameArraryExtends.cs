/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SmartExtends.System;
using System.Web;

namespace System.Collections
{
    /// <summary>
    /// 对IEnumerable实例的扩展
    /// </summary>
    public static partial class FrameArraryExtends
    {
        /// <summary>
        /// 串联对象数组的各个元素，其中在每个元素之间使用指定的分隔符。
        /// </summary>
        /// <param name="values">一个数组，其中包含要连接的元素。</param>
        /// <param name="separator">要用作分隔符的字符串。</param>        
        /// <returns>一个由 values 的元素组成的字符串，这些元素以 separator 字符串分隔。</returns>
        public static string Join(this object[] values, string separator)
        {
            return separator.Join(values);
        }

        /// <summary>
        /// 串联字符串集合的成员，其中在每个成员之间使用指定的分隔符。
        /// </summary>
        /// <typeparam name="T">values 成员的类型。</typeparam>
        /// <param name="values">一个包含要串联的对象的集合。</param>
        /// <param name="separator">要用作分隔符的字符串。</param>        
        /// <returns>一个由 values 的成员组成的字符串，这些成员以 separator 字符串分隔。</returns>
        public static string Join<T>(this IEnumerable<T> values, string separator)
        {
            return separator.Join<T>(values);
        }

        /// <summary>
        /// 串联类型为 System.String 的 System.Collections.Generic.IEnumerable[string] 构造集合的成员，其中在每个成员之间使用指定的分隔符。
        /// </summary>
        /// <param name="values">一个包含要串联的字符串的集合</param>
        /// <param name="separator">要用作分隔符的字符串。</param>
        /// <returns>一个由 values 的成员组成的字符串，这些成员以 separator 字符串分隔。</returns>
        public static string Join(this IEnumerable<string> values, string separator)
        {
            return separator.Join(values);
        }

        /// <summary>
        /// 串联字符串数组的所有元素，其中在每个元素之间使用指定的分隔符。
        /// </summary>
        /// <param name="value">一个数组，其中包含要连接的元素。</param>
        /// <param name="separator">要用作分隔符的字符串。</param>        
        /// <returns>一个由 value 中的元素组成的字符串，这些元素以 separator 字符串分隔。</returns>
        public static string Join(this string[] value, string separator)
        {
            return separator.Join(value);
        }

        /// <summary>
        /// 串联字符串数组的指定元素，其中在每个元素之间使用指定的分隔符。
        /// </summary>
        /// <param name="value">一个数组，其中包含要连接的元素。</param>
        /// <param name="separator">要用作分隔符的字符串。</param>        
        /// <param name="startIndex">value 中要使用的第一个元素。</param>
        /// <param name="count">要使用的 value 的元素数。</param>
        /// <returns>由 value 中的字符串组成的字符串，这些字符串以 separator 字符串分隔。- 或 -如果 count 为零，value 没有元素，或
        /// separator 以及 value 的全部元素均为 System.String.Empty，则为 System.String.Empty。</returns>
        public static string Join(this string[] value, string separator, int startIndex, int count)
        {
            return separator.Join(value, startIndex, count);
        }

        /// <summary>
        /// 将byte[]数组转换为16进制字符串hex
        /// </summary>
        /// <param name="buffer">byte数组</param>
        /// <returns>转换后的16进制字符串</returns>
        public static string ToHexString(this byte[] buffer)
        {
            ////StringBuilder hexString = new StringBuilder(64);

            ////for (int i = 0; i < buffer.Length; i++)
            ////{
            ////    hexString.Append(String.Format("{0:X2}", buffer[i]));
            ////}

            ////return hexString.ToString();

            int iLen = 0;

            ////通过反射获取 MachineKeySection 中的 ByteArrayToHexString 方法，该方法用于将字节数组转换为 16 进制表示的字符串。
            Type type = typeof(System.Web.Configuration.MachineKeySection);
            MethodInfo byteArrayToHexString = type.GetMethod("ByteArrayToHexString", BindingFlags.Static | BindingFlags.NonPublic);
            // 字节数组转换为 16 进制表示的字符串
            return (string)byteArrayToHexString.Invoke(null, new object[] { buffer, iLen });
        }

        /// <summary>
        /// ASCII字符编码数组转换为字符串
        /// </summary>
        /// <param name="input">ASCII字符编码数组</param>
        /// <returns>ASCII字符编码数组转换后的字符串</returns>
        public static string ASCIIBytesToString(this byte[] input)
        {
            System.Text.ASCIIEncoding enc = new ASCIIEncoding();
            return enc.GetString(input);
        }

        /// <summary>
        /// UTF-16 编码数组转换为字符串
        /// </summary>
        /// <param name="input">UTF-16 编码数组</param>
        /// <returns>UTF-16 编码数组转换后的字符串</returns>
        public static string UTF16BytesToString(this byte[] input)
        {
            System.Text.UnicodeEncoding enc = new UnicodeEncoding();
            return enc.GetString(input);
        }

        /// <summary>
        /// UTF-8编码数组转换为字符串
        /// </summary>
        /// <param name="input">UTF-8编码数组</param>
        /// <returns>UTF-8编码数组转换后的字符串</returns>
        public static string UTF8BytesToString(this byte[] input)
        {
            System.Text.UTF8Encoding enc = new UTF8Encoding();
            return enc.GetString(input);
        }

        /// <summary>
        /// byte 数组转换为 Base64编码字符串
        /// </summary>
        /// <param name="input">byte数组</param>
        /// <returns>byte 数组转换为 Base64编码后的字符串</returns>
        public static string ToBase64String(this byte[] input)
        {
            return Convert.ToBase64String(input);
        }

        /// <summary>
        /// 将 List列表 转化为csv字符串
        /// </summary>
        /// <param name="sourcesList"></param>
        /// <param name="header">name1,name2,name3</param>
        /// <param name="columns">columnname1,columnname2,columnname3</param>
        /// <returns></returns>
        public static string ToCsvString<T>(this List<T> sourcesList, string header, string columns)
        {
            List<string> ls = columns.Split(',').ToList();
            StringBuilder sb = new StringBuilder(header);
            sb.AppendLine();
            List<string> csvrowdata = new List<string>();
            var propertyList = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (T t in sourcesList)
            {
                csvrowdata = new List<string>();
                foreach (var item in ls)
                {
                    var pro = propertyList.FirstOrDefault(p => p.Name.Compare(item, true));
                    csvrowdata.Add((pro == null || pro.GetValue(t, null) == null) ? "" : pro.GetValue(t, null).ToString().Replace(",", "，"));
                }
                sb.AppendLine(csvrowdata.Join(","));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 将 List列表 转化为csv字符串
        /// </summary>
        /// <param name="sourcesList"></param>
        /// <param name="filename">文件名称，不带后缀名</param>
        /// <param name="header">name1,name2,name3</param>
        /// <param name="columns">columnname1,columnname2,columnname3</param>
        /// <returns></returns>
        public static void ToCsvString<T>(this List<T> sourcesList, HttpResponse response, string filename, string header, string columns)
        {
            response.Clear();
            response.Buffer = true;
            response.Charset = "GB2312";
            response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".csv");
            response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            response.ContentType = "application/ms-excel";
            response.Write(sourcesList.ToCsvString(header, columns));
            response.End();
        }
        static FrameArraryExtends()
        {
            Authority a = new Authority();
        }
    }
}