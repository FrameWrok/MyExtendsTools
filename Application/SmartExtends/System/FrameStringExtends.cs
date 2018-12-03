/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using SmartExtends.Frame;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using SmartExtends.System;

namespace System
{
    /// <summary>
    /// 对 String 类的 扩展 处理字符串
    /// </summary>
    public static partial class FrameStringExtends
    {
        /// <summary>
        /// 返回 字符串 左侧指定长度的 字符串
        /// </summary>
        /// <param name="input">待 截取 字符串</param>
        /// <param name="length">截取 的长度</param>
        /// <returns>截取后返回的 字符串</returns>
        public static string Left(this string input, int length)
        {
            if (input.Length <= length)
            {
                return input;
            }
            else
            {
                return input.Substring(0, length);
            }
        }

        /// <summary>
        /// 返回 字符串 右侧 指定长度的 字符串
        /// </summary>
        /// <param name="input">待 截取 字符串</param>
        /// <param name="length">截取 的 长度</param>
        /// <returns>截取后 返回的 字符串</returns> 
        public static string Right(this string input, int length)
        {
            if (input.Length <= length)
            {
                return input;
            }
            else
            {
                return input.Substring(input.Length - length, length);
            }
        }

        /// <summary>
        /// 将 字符串 进行 翻转 返回
        /// </summary>
        /// <param name="input">待 翻转 的字符串</param>
        /// <returns>翻转后 的 字符串</returns>
        public static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        /// 返回 字符串 的长度，一个汉字 算 两个字符
        /// </summary>
        /// <param name="input">待检验 字符串</param>
        /// <returns>返回 字符串的长度</returns>
        public static int GetTrueLength(this string input)
        {
            int result = 0;
            byte[] datas = (new ASCIIEncoding()).GetBytes(input);
            for (int i = 0; i < datas.Length; i++)
            {
                if (datas[i] == 63)
                {
                    result++;
                }

                result++;
            }

            return result;
        }

        /// <summary>
        /// 转换为全角或半角格式
        /// </summary>
        /// <param name="input">待转换的字符串</param>
        /// <param name="frameStringCaseType">字符格式类型</param>
        /// <returns>转换之后的结果</returns>
        public static string ToString(this string input, FrameStringCaseType frameStringCaseType)
        {
            string result = string.Empty;
            switch (frameStringCaseType)
            {
                case FrameStringCaseType.SBC:
                    {
                        // 转全角(SBC case)
                        // 全角空格为12288，半角空格为32
                        // 其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
                        char[] chars = input.ToCharArray();
                        for (int i = 0; i < chars.Length; i++)
                        {
                            if (chars[i] == 32)
                            {
                                chars[i] = (char)12288;
                                continue;
                            }

                            if (chars[i] == 46)
                            {
                                chars[i] = (char)12290;
                                continue;
                            }

                            if (chars[i] < 127)
                            {
                                chars[i] = (char)(chars[i] + 65248);
                            }
                        }

                        result = new string(chars);
                        result = result.Replace(".", "。");
                    }

                    break;
                case FrameStringCaseType.DBC:
                    {
                        // 转半角(DBC case)
                        // 全角空格为12288，半角空格为32
                        // 其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
                        char[] chars = input.ToCharArray();
                        for (int i = 0; i < chars.Length; i++)
                        {
                            if (chars[i] == 12288)
                            {
                                chars[i] = (char)32;
                                continue;
                            }

                            if (chars[i] == 12290)
                            {
                                chars[i] = (char)46;
                                continue;
                            }

                            if (chars[i] > 65280 && chars[i] < 65375)
                            {
                                chars[i] = (char)(chars[i] - 65248);
                            }
                        }

                        result = new string(chars);
                        result = result.Replace("。", ",");
                    }

                    break;
            }

            return result;
        }

        /// <summary>
        /// 字符串装换为 bool 值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Boolean ToBoolean(this string input)
        {
            if (input.IsNullOrEmptyOrBlank())
                return false;
            input = input.Trim();
            if (input.IsIntegerOr0())
                return int.Parse(input) > 0;
            switch (input.ToLower())
            {
                case "true": return true;
                default: return false;
            }
        }

        /// <summary>
        /// 十六进制字符串（Hex）转换为byte数组
        /// </summary>
        /// <param name="hexString">十六进制字符串（hex）</param>
        /// <returns>转换后的byte数组</returns>
        public static byte[] HexStringToBytes(this string hexString)
        {
            if (hexString.IsNullOrEmptyOrBlank() || hexString.Length == 0)
            {
                return new byte[] { 0 };
            }

            if (hexString.Length % 2 == 1)
            {
                hexString = "0" + hexString;
            }

            byte[] result = new byte[hexString.Length / 2];

            for (int i = 0; i < hexString.Length / 2; i++)
            {
                result[i] = byte.Parse(hexString.Substring(2 * i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }

            return result;
        }



        /// <summary>
        /// base64字符串转换为byte数组
        /// </summary>
        /// <param name="inputBase64">Base64编码的字符串</param>
        /// <returns>base64字符串转换后的byte数组</returns>
        public static byte[] FromBase64String(this string inputBase64)
        {
            return Convert.FromBase64String(inputBase64);
        }

        /// <summary>
        /// 将字符串转换为byte数组
        /// </summary>
        /// <param name="input">数据源</param>
        /// <returns>字符串转换后的byte数组</returns>
        public static byte[] ToByteArrary(this string input)
        {
            return Encoding.Default.GetBytes(input);
        }

        /// <summary>
        /// 将字符串转换为 Base64编码等效字符串
        /// </summary>
        /// <param name="input">数据源字符串</param>
        /// <returns>转换后的Base64编码字符串</returns>
        public static string ToBase64String(this string input)
        {
            return input.ToByteArrary().ToBase64String();
        }

        /// <summary>
        /// 将字符串转换为16进制字符串hex
        /// </summary>
        /// <param name="input">要转换的字符串</param>
        /// <returns>转换后的16进制字符串</returns>
        public static string ToHexString(this string input)
        {
            return input.ToByteArrary().ToHexString();
        }

        /// <summary>
        /// 该字符串如果超出指定长度，则返回截取制定长度的字符串
        /// 如超出了指定长度，截取后可后面追加字符串
        /// 如 aaaabbbb 需要显示为 aaaa...
        /// </summary>
        /// <param name="input">要操作的字符串</param>
        /// <param name="length">要截取的长度</param>
        /// <param name="appendString">追加的字符串</param>
        /// <returns>返回截取之后的结果</returns>
        public static string Truncate(this string input, int length, string appendString)
        {
            if (!input.IsNullOrEmptyOrBlank() && input.Length > length)
            {
                return input.Left(length - appendString.Length) + (appendString.IsNullOrEmptyOrBlank() ? string.Empty : appendString);
            }

            return input;
        }

        /// <summary>
        /// 如果该字符串不满足制定长度，则追加输入的 appendChar,直至满足制定长度
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="length">需要满足的长度</param>
        /// <param name="appendChar">如不满足指定长度，要追加的字符</param>
        /// <returns>返回满足指定长度的字符串</returns>
        public static string AppendChar(this string input, int length, string appendChar)
        {
            if (input.Length < length)
            {
                StringBuilder sb = new StringBuilder(input);
                while (sb.Length < length)
                {
                    sb.Append(appendChar);
                }

                return sb.ToString();
            }

            return input;
        }

        /// <summary>
        /// 串联类型为 System.String 的 System.Collections.Generic.IEnumerable[string] 构造集合的成员，其中在每个成员之间使用指定的分隔符。
        /// </summary>
        /// <param name="separator">要用作分隔符的字符串。</param>
        /// <param name="values">一个包含要串联的字符串的集合</param>
        /// <returns>一个由 values 的成员组成的字符串，这些成员以 separator 字符串分隔。</returns>
        public static string Join(this string separator, IEnumerable<string> values)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        /// 串联字符串集合的成员，其中在每个成员之间使用指定的分隔符。
        /// </summary>
        /// <typeparam name="T">values 成员的类型。</typeparam>
        /// <param name="separator">要用作分隔符的字符串。</param>
        /// <param name="values">一个包含要串联的对象的集合。</param>
        /// <returns>一个由 values 的成员组成的字符串，这些成员以 separator 字符串分隔。</returns>
        public static string Join<T>(this string separator, IEnumerable<T> values)
        {
            return string.Join<T>(separator, values);
        }

        /// <summary>
        /// 串联对象数组的各个元素，其中在每个元素之间使用指定的分隔符。
        /// </summary>
        /// <param name="separator">要用作分隔符的字符串。</param>
        /// <param name="values">一个数组，其中包含要连接的元素。</param>
        /// <returns>一个由 values 的元素组成的字符串，这些元素以 separator 字符串分隔。</returns>
        public static string Join(this string separator, params object[] values)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        /// 串联字符串数组的所有元素，其中在每个元素之间使用指定的分隔符。
        /// </summary>
        /// <param name="separator">要用作分隔符的字符串。</param>
        /// <param name="value">一个数组，其中包含要连接的元素。</param>
        /// <returns>一个由 value 中的元素组成的字符串，这些元素以 separator 字符串分隔。</returns>
        public static string Join(this string separator, params string[] value)
        {
            return string.Join(separator, value);
        }

        /// <summary>
        /// 串联字符串数组的指定元素，其中在每个元素之间使用指定的分隔符。
        /// </summary>
        /// <param name="separator">要用作分隔符的字符串。</param>
        /// <param name="value">一个数组，其中包含要连接的元素。</param>
        /// <param name="startIndex">value 中要使用的第一个元素。</param>
        /// <param name="count">要使用的 value 的元素数。</param>
        /// <returns>由 value 中的字符串组成的字符串，这些字符串以 separator 字符串分隔。- 或 -如果 count 为零，value 没有元素，或
        /// separator 以及 value 的全部元素均为 System.String.Empty，则为 System.String.Empty。</returns>
        public static string Join(this string separator, string[] value, int startIndex, int count)
        {
            return string.Join(separator, value, startIndex, count);
        }

        /// <summary>
        /// 去除所有Html标记
        /// </summary>
        /// <param name="input">待处理的文本</param>
        /// <returns>处理的结果</returns>
        public static string ClearHtmlTag(this string input)
        {
            string pattern = @"\<.*?>";
            Regex regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return regex.Replace(input, string.Empty);
        }

        /// <summary>
        /// 通过正则表达式查找匹配的字符串
        /// </summary>
        /// <param name="input">待处理的字符串</param>
        /// <param name="pattern">模式</param>
        /// <returns>查找到的结果列表</returns>
        public static List<string> Find(this string input, string pattern)
        {
            Regex regex = new Regex(pattern);
            MatchCollection matchs = regex.Matches(input);
            List<string> list = new List<string>();
            foreach (Match match in matchs)
            {
                list.Add(match.Value);
            }

            return list;
        }

        /// <summary>
        /// 与另一个字符串进行比较是否相等,默认区分大小写
        /// </summary>
        /// <param name="strA">字符串A</param>
        /// <param name="strB">要比较的字符串</param>
        /// <returns>比较结果</returns>
        public static bool Compare(this string strA, string strB)
        {
            return string.Compare(strA, strB) == 0;
        }

        /// <summary>
        /// 与另一个字符串进行比较是否相等,默认区分大小写
        /// </summary>
        /// <param name="strA">字符串A</param>
        /// <param name="strB">要比较的字符串</param>
        /// <param name="ignoreCase">是否不区分大小写</param>
        /// <returns>比较结果</returns>
        public static bool Compare(this string strA, string strB, bool ignoreCase)
        {
            return string.Compare(strA, strB, ignoreCase) == 0;
        }

        /// <summary>
        /// 对 URL 字符串进行编码。
        /// </summary>
        /// <param name="str">要编码的文本</param>
        /// <returns>一个已编码的字符串</returns>
        public static string UrlEncode(this string str)
        {
            return System.Web.HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// 将已经为在 URL 中传输而编码的字符串转换为解码的字符串。
        /// </summary>
        /// <param name="str">要解码的字符串</param>
        /// <returns>一个已解码的字符串</returns>
        public static string UrlDecode(this string str)
        {
            return System.Web.HttpUtility.UrlDecode(str);
        }

        /// <summary>
        /// 将字符串转换为 HTML 编码的字符串
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        /// <returns>一个已编码的字符串</returns>
        public static string HtmlEncode(this string str)
        {
            return System.Web.HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// 将字符串转换为 HTML 编码的字符串
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        /// <returns>一个已编码的字符串</returns>
        public static string HtmlDecode(this string str)
        {
            return System.Web.HttpUtility.HtmlDecode(str);
        }
    }

    /// <summary>
    /// 对 String 类的 扩展 验证
    /// </summary>
    public static partial class FrameStringExtends
    {
        /// <summary>
        /// 通过正则表达式验证，验证通过返回true
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="pattern">要匹配的正则表达式</param>
        /// <returns>找到匹配项则返回True</returns>
        public static bool IsMatch(this string input, string pattern)
        {
            return (new Regex(pattern)).IsMatch(input);
        }

        #region 数学验证相关

        /// <summary>
        /// 验证是否是纯数字组合
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果：True=是，False=否</returns>
        public static bool IsNumber(this string input)
        {
            return IsMatch(input, "^[0-9]+$");
        }

        /// <summary>
        /// 验证是否是小数或整数，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>       
        /// <returns>验证结果：True=是，False=否</returns>
        public static bool IsDigit(this string input)
        {
            double number;
            return double.TryParse(input, out number);
        }

        /// <summary>
        /// 验证是否是非负整数，包含正整数和0（0.0,1,1.00,）
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNoNegativeInteger(this string input)
        {
            return IsMatch(input, "^\\d+$");
            ////return IsMatch(input, @"^[+]?[0-9]+[.]?[0]*$") || IsMatch(input, @"^[+-]?[0]+[.]?[0]*$");
        }

        /// <summary>
        /// 验证是否是正整数，0不包含在内
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsPositiveInteger(this string input)
        {
            return IsMatch(input, @"^[0-9]*[1-9][0-9]*$");
        }

        /// <summary>
        /// 验证是否是非正整数,包含负整数和0（0，-1.0，-3）
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNoPositiveInteger(this string input)
        {
            return IsMatch(input, "^((-\\d+)|(0+))$");
        }

        /// <summary>
        /// 验证是否是负整数，0不包含在内
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNegativeInteger(this string input)
        {
            return IsMatch(input, "^-[0-9]*[1-9][0-9]*$");
        }

        /// <summary>
        /// 验证是否是整数，0不包含在内(正整数和负整数)
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsInteger(this string input)
        {
            return IsNegativeInteger(input) || IsPositiveInteger(input);
        }

        /// <summary>
        /// 验证是否是整数或0
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsIntegerOr0(this string input)
        {
            return IsNoNegativeInteger(input) || IsNoPositiveInteger(input);
        }

        /// <summary>
        /// 验证是否是非负浮点数（正浮点数+0）
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNoNegativeDouble(this string input)
        {
            return IsMatch(input, @"^\d+(\.\d+)?$");
        }

        /// <summary>
        /// 验证是否是负浮点数
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNegativeDouble(this string input)
        {
            return IsMatch(input, @"^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$");
        }

        /// <summary>
        /// 验证是否是非正浮点数（负浮点数+0）
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNoPositiveDouble(this string input)
        {
            return IsMatch(input, @"^((-\d+(\.\d+)?)|(0+(\.0+)?))$");
        }

        /// <summary>
        /// 验证是否是正浮点数
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsPositiveDouble(this string input)
        {
            return IsMatch(input, @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$");
        }

        /// <summary>
        /// 验证是否是浮点数
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsDouble(this string input)
        {
            return IsMatch(input, @"^(-?\d+)(\.\d+)?$");
        }

        /// <summary>
        /// 验证小数位数是不是指定的长度
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="length">指定的长度</param>
        /// <returns>验证的结果：true＝在范围内；false＝不在范围内</returns>
        public static bool IsDecimalDigits(this string input, int length)
        {
            string pattern = @"^[0-9]+(.[0-9]{" + length + "})$";
            return IsMatch(input, pattern);
        }

        /// <summary>
        /// 验证小数位数是不是在指定的范围内
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="start">起始边界</param>
        /// <param name="end">结束边界</param>
        /// <returns>验证的结果：true＝在范围内；false＝不在范围内</returns>
        public static bool IsDecimalDigitsInScope(this string input, int start, int end)
        {
            string pattern = @"^[0-9]+(.[0-9]{" + start + "," + end + "})$";
            return IsMatch(input, pattern);
        }
        #endregion

        #region 数学验证相关 带输出参数

        /// <summary>
        /// 验证是否是纯数字组合，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的字符串等效的字符串</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：True=是，False=否</returns>
        public static bool IsNumber(this string input, out string result, string defaultValue)
        {
            result = IsMatch(input, "^[0-9]+$") ? input : defaultValue;
            return IsMatch(input, "^[0-9]+$");
        }

        /// <summary>
        /// 验证是否是小数或整数，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的 小数或整数 等效 值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：True=是，False=否</returns>
        public static bool IsDigit(this string input, out double result, double defaultValue)
        {
            double number;
            result = double.TryParse(input, out number) ? double.Parse(input) : defaultValue;
            return double.TryParse(input, out number);
        }

        /// <summary>
        /// 验证是否是非负整数，包含正整数和0（0.0,1,1.00,），并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的非负整数等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNoNegativeInteger(this string input, out int result, int defaultValue)
        {
            result = IsMatch(input, "^\\d+$") ? int.Parse(input) : defaultValue;
            return IsMatch(input, "^\\d+$");
        }

        /// <summary>
        /// 验证是否是非负整数，包含正整数和0（0.0,1,1.00,），并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的非负整数(Int64)等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNoNegativeInteger(this string input, out long result, long defaultValue)
        {
            result = IsMatch(input, "^\\d+$") ? long.Parse(input) : defaultValue;
            return IsMatch(input, "^\\d+$");
        }

        /// <summary>
        /// 验证是否是正整数，0不包含在内，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的正整数(Int32)等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsPositiveInteger(this string input, out int result, int defaultValue)
        {
            result = IsMatch(input, @"^[0-9]*[1-9][0-9]*$") ? int.Parse(input) : defaultValue;
            return IsMatch(input, @"^[0-9]*[1-9][0-9]*$");
        }

        /// <summary>
        /// 验证是否是正整数，0不包含在内，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的正整数(Int64)等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsPositiveInteger(this string input, out long result, long defaultValue)
        {
            result = IsMatch(input, @"^[0-9]*[1-9][0-9]*$") ? long.Parse(input) : defaultValue;
            return IsMatch(input, @"^[0-9]*[1-9][0-9]*$");
        }

        /// <summary>
        /// 验证是否是非正整数,包含负整数和0（0，-1.0，-3），并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的非正整数(Int32)等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNoPositiveInteger(this string input, out int result, int defaultValue)
        {
            result = IsMatch(input, "^((-\\d+)|(0+))$") ? int.Parse(input) : defaultValue;
            return IsMatch(input, "^((-\\d+)|(0+))$");
        }

        /// <summary>
        /// 验证是否是非正整数,包含负整数和0（0，-1.0，-3），并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的非正整数(Int64)等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNoPositiveInteger(this string input, out long result, long defaultValue)
        {
            result = IsMatch(input, "^((-\\d+)|(0+))$") ? long.Parse(input) : defaultValue;
            return IsMatch(input, "^((-\\d+)|(0+))$");
        }

        /// <summary>
        /// 验证是否是负整数，0不包含在内，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的非正整数(Int32)等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNegativeInteger(this string input, out int result, int defaultValue)
        {
            result = IsMatch(input, "^-[0-9]*[1-9][0-9]*$") ? int.Parse(input) : defaultValue;
            return IsMatch(input, "^-[0-9]*[1-9][0-9]*$");
        }

        /// <summary>
        /// 验证是否是负整数，0不包含在内，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的非正整数(Int64)等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNegativeInteger(this string input, out long result, long defaultValue)
        {
            result = IsMatch(input, "^-[0-9]*[1-9][0-9]*$") ? long.Parse(input) : defaultValue;
            return IsMatch(input, "^-[0-9]*[1-9][0-9]*$");
        }

        /// <summary>
        /// 验证是否是整数，0不包含在内(正整数和负整数)，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的非正整数(Int32)等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsInteger(this string input, out int result, int defaultValue)
        {
            result = IsNegativeInteger(input) || IsPositiveInteger(input) ? int.Parse(input) : defaultValue;
            return IsNegativeInteger(input) || IsPositiveInteger(input);
        }

        /// <summary>
        /// 验证是否是整数，0不包含在内(正整数和负整数)，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的非正整数(Int64)等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsInteger(this string input, out long result, long defaultValue)
        {
            result = IsNegativeInteger(input) || IsPositiveInteger(input) ? long.Parse(input) : defaultValue;
            return IsNegativeInteger(input) || IsPositiveInteger(input);
        }

        /// <summary>
        /// 验证是否是整数或0，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的非正整数(Int32)等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsIntegerOr0(this string input, out int result, int defaultValue)
        {
            result = IsNoNegativeInteger(input) || IsNoPositiveInteger(input) ? int.Parse(input) : defaultValue;
            return IsNoNegativeInteger(input) || IsNoPositiveInteger(input);
        }

        /// <summary>
        /// 验证是否是整数或0，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的非正整数(Int64)等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsIntegerOr0(this string input, out long result, long defaultValue)
        {
            result = IsNoNegativeInteger(input) || IsNoPositiveInteger(input) ? long.Parse(input) : defaultValue;
            return IsNoNegativeInteger(input) || IsNoPositiveInteger(input);
        }

        /// <summary>
        /// 验证是否是非负浮点数（正浮点数+0），并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的浮点数 (double) 等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNoNegativeDouble(this string input, out double result, double defaultValue)
        {
            result = IsMatch(input, @"^\d+(\.\d+)?$") ? double.Parse(input) : defaultValue;
            return IsMatch(input, @"^\d+(\.\d+)?$");
        }

        /// <summary>
        /// 验证是否是非负浮点数（正浮点数+0），并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的浮点数 (decimal) 等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNoNegativeDouble(this string input, out decimal result, decimal defaultValue)
        {
            result = IsMatch(input, @"^\d+(\.\d+)?$") ? decimal.Parse(input) : defaultValue;
            return IsMatch(input, @"^\d+(\.\d+)?$");
        }

        /// <summary>
        /// 验证是否是负浮点数，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的浮点数 (decimal) 等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNegativeDouble(this string input, out decimal result, decimal defaultValue)
        {
            result = IsMatch(input, @"^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$") ? decimal.Parse(input) : defaultValue;
            return IsMatch(input, @"^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$");
        }

        /// <summary>
        /// 验证是否是负浮点数，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的浮点数 (double) 等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNegativeDouble(this string input, out double result, double defaultValue)
        {
            result = IsMatch(input, @"^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$") ? double.Parse(input) : defaultValue;
            return IsMatch(input, @"^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$");
        }

        /// <summary>
        /// 验证是否是非正浮点数（负浮点数+0），并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的浮点数 (double) 等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNoPositiveDouble(this string input, out double result, double defaultValue)
        {
            result = IsMatch(input, @"^((-\d+(\.\d+)?)|(0+(\.0+)?))$") ? double.Parse(input) : defaultValue;
            return IsMatch(input, @"^((-\d+(\.\d+)?)|(0+(\.0+)?))$");
        }

        /// <summary>
        /// 验证是否是非正浮点数（负浮点数+0），并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的浮点数 (decimal) 等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsNoPositiveDouble(this string input, out decimal result, decimal defaultValue)
        {
            result = IsMatch(input, @"^((-\d+(\.\d+)?)|(0+(\.0+)?))$") ? decimal.Parse(input) : defaultValue;
            return IsMatch(input, @"^((-\d+(\.\d+)?)|(0+(\.0+)?))$");
        }

        /// <summary>
        /// 验证是否是正浮点数，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的浮点数 (double) 等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsPositiveDouble(this string input, out double result, double defaultValue)
        {
            result = IsMatch(input, @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$") ? double.Parse(input) : defaultValue;
            return IsMatch(input, @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$");
        }

        /// <summary>
        /// 验证是否是正浮点数，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的浮点数 (decimal) 等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsPositiveDouble(this string input, out decimal result, decimal defaultValue)
        {
            result = IsMatch(input, @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$") ? decimal.Parse(input) : defaultValue;
            return IsMatch(input, @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$");
        }

        /// <summary>
        /// 验证是否是浮点数，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的浮点数 (double) 等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsDouble(this string input, out double result, double defaultValue)
        {
            result = IsMatch(input, @"^(-?\d+)(\.\d+)?$") ? double.Parse(input) : defaultValue;
            return IsMatch(input, @"^(-?\d+)(\.\d+)?$");
        }

        /// <summary>
        /// 验证是否是浮点数，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的浮点数 (decimal) 等效的值</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：true=是，false=否</returns>
        public static bool IsDouble(this string input, out decimal result, decimal defaultValue)
        {
            result = IsMatch(input, @"^(-?\d+)(\.\d+)?$") ? decimal.Parse(input) : defaultValue;
            return IsMatch(input, @"^(-?\d+)(\.\d+)?$");
        }

        #endregion

        #region 验证日常信息相关

        /// <summary>
        /// 验证是否是Email格式
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果：True=是，False=不是</returns>
        public static bool IsEmail(this string input)
        {
            return IsMatch(input, @"^\w+((-\w+)|(\.\w+))*\@\w+((\.|-)\w+)*\.\w+$");
        }

        /// <summary>
        /// 验证是不是电话号码，匹配010-1234567、010-12345678、0316-1234567、0316-12345678、1234567、12345678。
        /// 以及所有前面的号码加“-”、“转”、“#”之后加分机号码。
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsPhone(this string input)
        {
            return IsMatch(input, @"^\d{7,8}$") || IsMatch(input, @"^\d{7,8}[-转#]\d{1,6}$") || IsMatch(input, @"^\d{3,4}-\d{7,8}$") || IsMatch(input, @"^\d{3,4}-\d{7,8}[-转#]\d{1,6}$");
        }

        /// <summary>
        /// 验证是不是手机号码
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsMobilePhone(this string input)
        {
            return IsMatch(input, @"^[1]{1}[0-9]{10}$");
        }

        /// <summary>
        /// 验证是否是日期时间
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsDateTime(this string input)
        {
            DateTime result;
            return DateTime.TryParse(input, out result);
        }

        /// <summary>
        /// 验证是否是邮编
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsZipCode(this string input)
        {
            return IsMatch(input, @"^\d{6}$");
        }

        /// <summary>
        /// 验证是否是URL
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsUrl(this string input)
        {
            return Uri.IsWellFormedUriString(input, UriKind.Absolute);
            ////return IsMatch(input, "^[a-zA-z]+://(\\w+(-\\w+)*)(\\.(\\w+(-\\w+)*))*(\\?\\S*)?$");
        }

        /// <summary>
        /// 验证身份证号，验证通过返回true，并输出性别，出生日期
        /// 否则返回false，输出错误信息
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <param name="sex">验证通过输出的年龄</param>
        /// <param name="birthday">验证通过输出的出生日期</param>
        /// <param name="errMessage">验证未通过输出的错误信息</param>
        /// <returns>返回验证结果</returns>
        public static bool IsSocialID(this string input, out string sex, out DateTime? birthday, out string errMessage)
        {
            sex = string.Empty;
            birthday = null;
            errMessage = string.Empty;
            if (input.Trim().Length != 15 && input.Trim().Length != 18)
            {
                errMessage = "身份证号长度错误";
                return false;
            }

            ////正则表达式基础验证
            if (!IsMatch(input, @"^[0-9]{17}[0-9X]$") && !IsMatch(input, @"^[0-9]{15}$"))
            {
                errMessage = "Card Number has wrong charactor(s).";
                return false;
            }

            int inputLen = input.Trim().Length;

            if (inputLen == 18)
            {
                ////18位身份证号末尾的验证码
                char[] verifycode = new char[] { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };

                //// 18位身份证中，各个数字的生成校验码时的权值
                int[] verifycodeweight = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
                int cardnumberlength = 18;
                int sum = 0;
                for (int i = 0; i < cardnumberlength - 1; i++)
                {
                    char ch = input[i];
                    sum += ((int)(ch - '0')) * verifycodeweight[i];
                }

                if (input[cardnumberlength - 1] != verifycode[sum % 11])
                {
                    errMessage = "Card Number verified code is not match.";
                    return false;
                }

                sex = ((int)input[cardnumberlength - 2]) % 2 == 0 ? "女" : "男";
                try
                {
                    birthday = DateTime.ParseExact(input.Substring(6, 8), "yyyyMMdd", null);
                }
                catch (Exception)
                {
                    errMessage = "身份证的出生日期无效";
                    return false;
                }
            }

            if (inputLen == 15)
            {
                sex = ((int)input[14]) % 2 == 0 ? "女" : "男";
                try
                {
                    birthday = DateTime.ParseExact("19" + input.Substring(8, 4), "yyyyMMdd", null);
                }
                catch (Exception)
                {
                    errMessage = "身份证的出生日期无效";
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 验证日常信息相关 带输出参数

        /// <summary>
        /// 验证是否是Email格式，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的字符串等效的字符串并Trim</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证结果：True=是，False=不是</returns>
        public static bool IsEmail(this string input, out string result, string defaultValue)
        {
            result = IsMatch(input, @"^\w+((-\w+)|(\.\w+))*\@\w+((\.|-)\w+)*\.\w+$") ? input.Trim() : defaultValue;
            return IsMatch(input, @"^\w+((-\w+)|(\.\w+))*\@\w+((\.|-)\w+)*\.\w+$");
        }

        /// <summary>
        /// 验证是不是电话号码，匹配010-1234567、010-12345678、0316-1234567、0316-12345678、1234567、12345678。
        /// 以及所有前面的号码加“-”、“转”、“#”之后加分机号码。
        /// 并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的字符串等效的字符串并Trim</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsPhone(this string input, out string result, string defaultValue)
        {
            result = IsMatch(input, @"^\d{7,8}$") || IsMatch(input, @"^\d{7,8}[-转#]\d{1,6}$") || IsMatch(input, @"^\d{3,4}-\d{7,8}$") || IsMatch(input, @"^\d{3,4}-\d{7,8}[-转#]\d{1,6}$") ? input.Trim() : defaultValue;
            return IsMatch(input, @"^\d{7,8}$") || IsMatch(input, @"^\d{7,8}[-转#]\d{1,6}$") || IsMatch(input, @"^\d{3,4}-\d{7,8}$") || IsMatch(input, @"^\d{3,4}-\d{7,8}[-转#]\d{1,6}$");
        }

        /// <summary>
        /// 验证是不是手机号码，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的字符串等效的字符串并Trim</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsMobilePhone(this string input, out string result, string defaultValue)
        {
            result = IsMatch(input, @"^[1]{1}[0-9]{10}$") ? input.Trim() : defaultValue;
            return IsMatch(input, @"^[1]{1}[0-9]{10}$");
        }

        /// <summary>
        /// 验证是否是日期时间，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// /// <param name="result">如果转换成功，则包含与 input 所包含的日期等效的日期</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsDateTime(this string input, out Nullable<DateTime> result, Nullable<DateTime> defaultValue)
        {
            result = defaultValue;
            DateTime date;
            bool convertResult = DateTime.TryParse(input, out date);
            if (convertResult)
            {
                result = date;
            }

            return convertResult;
        }

        /// <summary>
        /// 验证是否是邮编，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的字符串等效的字符串并Trim</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsZipCode(this string input, out string result, string defaultValue)
        {
            result = IsMatch(input, @"^\d{6}$") ? input.Trim() : defaultValue;
            return IsMatch(input, @"^\d{6}$");
        }

        /// <summary>
        /// 验证是否是URL，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的字符串等效的字符串并Trim</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsUrl(this string input, out string result, string defaultValue)
        {
            result = Uri.IsWellFormedUriString(input, UriKind.Absolute) ? input.Trim() : defaultValue;
            return Uri.IsWellFormedUriString(input, UriKind.Absolute);
            ////return IsMatch(input, "^[a-zA-z]+://(\\w+(-\\w+)*)(\\.(\\w+(-\\w+)*))*(\\?\\S*)?$");
        }

        #endregion

        #region 编程验证相关

        /// <summary>
        /// 验证是不是只是字母和数字的组合
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsLetterNumber(this string input)
        {
            return IsMatch(input, @"^[A-Za-z0-9]+$");
        }

        /// <summary>
        /// 验证是不是字符、数字或者下划线的组合
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsLetterNumberUnderline(this string input)
        {
            string pattern = @"^[a-zA-Z0-9_]+$";
            return IsMatch(input, pattern);
        }

        /// <summary>
        /// 验证日期是不是在指定的范围内
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="start">起始边界</param>
        /// <param name="end">结束边界</param>
        /// <returns>验证的结果：true＝在范围内；false＝不在范围内</returns>
        public static bool IsDateTimeInScope(this string input, DateTime start, DateTime end)
        {
            DateTime result;
            if (DateTime.TryParse(input, out result))
            {
                if (result >= start && result <= end)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证是不是正确的IP地址
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsIPAddress(this string input)
        {
            IPAddress ip;
            return IPAddress.TryParse(input, out ip);
            ////return IsMatch(input, "^(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5])$");
        }

        /// <summary>
        /// 验证是否符合用户名的格式（字母开头，允许字母数字下划线）,
        /// 是否符合指定长度范围 
        /// 验证是否不包含注入漏洞
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="start">用户名长度范围开始</param>
        /// <param name="end">用户名长度范围结束</param>        
        /// <returns>验证的结果：true＝符合；false＝不符合</returns>
        public static bool IsUserName(this string input, int start, int end)
        {
            if (IsMatch(input, @"^[a-zA-Z][a-zA-Z0-9_]{" + (start - 1) + @"," + (end - 1) + @"}$"))
            {
                if (IsMatch(input.ToUpper(), "SELECT"))
                {
                    return false;
                }

                if (IsMatch(input.ToUpper(), "INSERT"))
                {
                    return false;
                }

                if (IsMatch(input.ToUpper(), "JOIN"))
                {
                    return false;
                }

                if (IsMatch(input.ToUpper(), "TRUNCATE"))
                {
                    return false;
                }

                if (IsMatch(input.ToUpper(), "EXCEPT"))
                {
                    return false;
                }

                if (IsMatch(input.ToUpper(), "INTERSECPT"))
                {
                    return false;
                }

                if (IsMatch(input.ToUpper(), "DELETE"))
                {
                    return false;
                }

                if (IsMatch(input.ToUpper(), "DR0P"))
                {
                    return false;
                }

                if (IsMatch(input.ToUpper(), "UPDATE"))
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是只有26个英文字母组成的字符串
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsLetter(this string input)
        {
            return IsMatch(input, "^[A-Za-z]+$");
        }

        /// <summary>
        /// 验证是不是只有大写字母组成的字符串
        /// </summary>        
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsUpper(this string input)
        {
            return IsMatch(input, "^[A-Z]+$");
        }

        /// <summary>
        /// 验证是不是只有小写字母组成的字符串
        /// </summary>        
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsLower(this string input)
        {
            return IsMatch(input, "^[a-z]+$");
        }

        /// <summary>
        /// 验证是不是中文字符串
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsChiness(this string input)
        {
            return IsMatch(input, @"[\u4e00-\u9fa5]");
        }

        #endregion

        #region 编程验证相关 带输出参数

        /// <summary>
        /// 验证是不是只是字母和数字的组合，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的字符串等效的字符串并Trim</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsLetterNumber(this string input, out string result, string defaultValue)
        {
            result = IsMatch(input, @"^[A-Za-z0-9]+$") ? input.Trim() : defaultValue;
            return IsMatch(input, @"^[A-Za-z0-9]+$");
        }

        /// <summary>
        /// 验证是不是字符、数字或者下划线的组合，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的字符串等效的字符串并Trim</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsLetterNumberUnderline(this string input, out string result, string defaultValue)
        {
            result = IsMatch(input, @"^[a-zA-Z0-9_]+$") ? input.Trim() : defaultValue;
            return IsMatch(input, @"^[a-zA-Z0-9_]+$");
        }

        /// <summary>
        /// 验证日期是不是在指定的范围内
        /// 并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="start">起始边界</param>
        /// <param name="end">结束边界</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的日期等效的日期</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证的结果：true＝在范围内；false＝不在范围内</returns>
        public static bool IsDateTimeInScope(this string input, DateTime start, DateTime end, out Nullable<DateTime> result, Nullable<DateTime> defaultValue)
        {
            DateTime convertTime;
            result = defaultValue;
            if (DateTime.TryParse(input, out convertTime))
            {
                if (convertTime >= start && convertTime <= end)
                {
                    result = convertTime;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证是不是正确的IP地址，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的字符串等效的字符串并Trim</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsIPAddress(this string input, out string result, string defaultValue)
        {
            IPAddress ip;
            bool convertResult = IPAddress.TryParse(input, out ip);
            result = convertResult ? input.Trim() : defaultValue;
            return convertResult;
            ////return IsMatch(input, "^(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5])$");
        }

        /// <summary>
        /// 验证是否符合用户名的格式（字母开头，允许字母数字下划线）,
        /// 是否符合指定长度范围 
        /// 验证是否不包含注入漏洞
        /// 并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="start">用户名长度范围开始</param>
        /// <param name="end">用户名长度范围结束</param> 
        /// <param name="result">如果转换成功，则包含与 input 所包含的字符串等效的字符串并Trim</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证的结果：true＝符合；false＝不符合</returns>
        public static bool IsUserName(this string input, int start, int end, out string result, string defaultValue)
        {
            result = defaultValue;
            if (IsMatch(input, @"^[a-zA-Z][a-zA-Z0-9_]{" + (start - 1) + @"," + (end - 1) + @"}$"))
            {
                if (IsMatch(input.ToUpper(), "SELECT"))
                    return false;
                if (IsMatch(input.ToUpper(), "INSERT"))
                    return false;
                if (IsMatch(input.ToUpper(), "JOIN"))
                    return false;
                if (IsMatch(input.ToUpper(), "TRUNCATE"))
                    return false;
                if (IsMatch(input.ToUpper(), "EXCEPT"))
                    return false;
                if (IsMatch(input.ToUpper(), "INTERSECPT"))
                    return false;
                if (IsMatch(input.ToUpper(), "DELETE"))
                    return false;
                if (IsMatch(input.ToUpper(), "DR0P"))
                    return false;
                if (IsMatch(input.ToUpper(), "UPDATE"))
                    return false;
                result = input.Trim();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是只有26个英文字母组成的字符串，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的字符串等效的字符串并Trim</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsLetter(this string input, out string result, string defaultValue)
        {
            result = IsMatch(input, "^[A-Za-z]+$") ? input.Trim() : defaultValue;
            return IsMatch(input, "^[A-Za-z]+$");
        }

        /// <summary>
        /// 验证是不是只有大写字母组成的字符串，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>        
        /// <param name="input">待验证的字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的字符串等效的字符串并Trim</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsUpper(this string input, out string result, string defaultValue)
        {
            result = IsMatch(input, "^[A-Z]+$") ? input.Trim() : defaultValue;
            return IsMatch(input, "^[A-Z]+$");
        }

        /// <summary>
        /// 验证是不是只有小写字母组成的字符串，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>        
        /// <param name="input">待验证的字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的字符串等效的字符串并Trim</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsLower(this string input, out string result, string defaultValue)
        {
            result = IsMatch(input, "^[a-z]+$") ? input.Trim() : defaultValue;
            return IsMatch(input, "^[a-z]+$");
        }

        /// <summary>
        /// 验证是不是中文字符串，并输出转换结果，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的字符串等效的字符串并Trim</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证的结果：true＝是；false＝不是</returns>
        public static bool IsChiness(this string input, out string result, string defaultValue)
        {
            result = IsMatch(input, @"[\u4e00-\u9fa5]") ? input.Trim() : defaultValue;
            return IsMatch(input, @"[\u4e00-\u9fa5]");
        }

        #endregion

        #region 基础验证
        static FrameStringExtends()
        {
            
        }
        /// <summary>
        /// 验证字符串是否是NULL 或者 "" ，空格默认也认为是空
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>当输入值 是(NULL,"",空格)则返回true，否则返回false</returns>
        public static bool IsNullOrEmptyOrBlank(this string input)
        {
            if ((input + string.Empty).Length == 0)
                return true;

            if (input.Trim().Length == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 验证字符串是否不是NULL 或者 ""，" " ，空格默认也认为是空
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>当输入值 是(NULL,"",空格)则返回true，否则返回false</returns>
        public static bool IsNotNullOrEmptyOrBlank(this string input)
        {
            return !input.IsNullOrEmptyOrBlank();
        }

        /// <summary>
        /// 验证长度是不是指定的长度
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="length">指定的长度</param>
        /// <returns>验证的结果：true＝在范围内；false＝不在范围内</returns>
        public static bool IsLength(this string input, int length)
        {
            string pattern = @"^.{" + length + "}$";
            return IsMatch(input, pattern);
        }

        /// <summary>
        /// 验证长度是不是在指定的范围内
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="start">起始边界</param>
        /// <param name="end">结束边界</param>
        /// <returns>验证的结果：true＝在范围内；false＝不在范围内</returns>
        public static bool IsLengthInScope(this string input, int start, int end)
        {
            string pattern = @"^.{" + start + "," + end + "}$";
            return IsMatch(input, pattern);
        }

        #endregion

        #region 基础验证 带输出参数

        /// <summary>
        /// 验证字符串是否是NULL 或者 "" ，空格默认也认为是空
        /// 如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的字符串等效的字符串并Trim</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>当输入值 是(NULL,"",空格)则返回true，否则返回false</returns>
        public static bool IsNullOrEmptyOrBlank(this string input, out string result, string defaultValue)
        {
            result = defaultValue;
            if (input == null)
            {
                return true;
            }

            if (input.Length == 0)
            {
                return true;
            }

            if (input.Trim().Length == 0)
            {
                return true;
            }

            result = input.Trim();
            return false;
        }

        /// <summary>
        /// 验证长度是不是指定的长度，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="length">指定的长度</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的字符串等效的字符串并Trim</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证的结果：true＝在范围内；false＝不在范围内</returns>
        public static bool IsLength(this string input, int length, out string result, string defaultValue)
        {
            result = IsMatch(input, @"^.{" + length + "}$") ? input.Trim() : defaultValue;
            return IsMatch(input, @"^.{" + length + "}$");
        }

        /// <summary>
        /// 验证长度是不是在指定的范围内，如转换失败则输出该方法输入的默认值
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="start">起始边界</param>
        /// <param name="end">结束边界</param>
        /// <param name="result">如果转换成功，则包含与 input 所包含的字符串等效的字符串并Trim</param>
        /// <param name="defaultValue">如果转换失败，则result等于defaultValue</param>
        /// <returns>验证的结果：true＝在范围内；false＝不在范围内</returns>
        public static bool IsLengthInScope(this string input, int start, int end, out string result, string defaultValue)
        {
            result = IsMatch(input, @"^.{" + start + "," + end + "}$") ? input.Trim() : defaultValue;
            return IsMatch(input, @"^.{" + start + "," + end + "}$");
        }

        #endregion
    }

    /// <summary>
    /// 对 String 类的 扩展
    /// </summary>
    public static partial class FrameStringExtends
    {
        /// <summary>
        /// 返回将指定字符串中的一个或多个格式项替换为指定对象的字符串表示形式。
        /// </summary>
        /// <param name="format">复合格式字符串。</param>
        /// <param name="arg0">要设置格式的对象。</param>
        /// <returns>format 的副本，其中的任何格式项均替换为 arg0 的字符串表示形式。</returns>
        public static string Formats(this string format, object arg0)
        {
            return string.Format(null, format, new object[] { arg0 });
        }

        /// <summary>
        /// 将指定字符串中的格式项替换为指定数组中相应对象的字符串表示形式。
        /// </summary>
        /// <param name="format">复合格式字符串。</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象。</param>
        /// <returns>format 的副本，其中的格式项已替换为 args 中相应对象的字符串表示形式。</returns>
        public static string Formats(this string format, params object[] args)
        {
            return string.Format(null, format, args);
        }

        /// <summary>
        /// 返回 将指定字符串中的格式项替换为指定数组中相应对象的字符串表示形式。指定的参数提供区域性特定的格式设置信息。
        /// </summary>
        /// <param name="format">复合格式字符串。</param>
        /// <param name="provider">一个提供区域性特定的格式设置信息的对象</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象。</param>
        /// <returns>返回 format 的副本，其中的格式项已替换为 args 中相应对象的字符串表示形式。</returns>
        public static string Formats(this string format, IFormatProvider provider, params object[] args)
        {
            if ((format == null) || (args == null))
            {
                throw new ArgumentNullException((format == null) ? "format" : "args");
            }

            StringBuilder builder = new StringBuilder(format.Length + (args.Length * 8));
            builder.AppendFormat(provider, format, args);
            return builder.ToString();
        }

        /// <summary>
        /// 返回 将指定字符串中的格式项替换为两个指定对象的字符串表示形式。
        /// </summary>
        /// <param name="format">复合格式字符串。</param>
        /// <param name="arg0">要设置格式的第一个对象。</param>
        /// <param name="arg1">要设置格式的第二个对象。</param>
        /// <returns>返回 format 的副本，其中的格式项替换为 arg0 和 arg1 的字符串表示形式。</returns>
        public static string Formats(this string format, object arg0, object arg1)
        {
            return string.Format(format, new object[] { arg0, arg1 });
        }

        /// <summary>
        /// 返回 将指定字符串中的格式项替换为三个指定对象的字符串表示形式。
        /// </summary>
        /// <param name="format">复合格式字符串。</param>
        /// <param name="arg0">要设置格式的第一个对象。</param>
        /// <param name="arg1">要设置格式的第二个对象。</param>
        /// <param name="arg2">要设置格式的第三个对象。</param>
        /// <returns>返回 format 的副本，其中的格式项已替换为 arg0、arg1 和 arg2 的字符串表示形式。</returns>
        public static string Formats(this string format, object arg0, object arg1, object arg2)
        {
            return string.Format(format, new object[] { arg0, arg1, arg2 });
        }
    }

    /// <summary>
    /// 对 String 类的 扩展 数据类型转换
    /// </summary>
    public static partial class FrameStringExtends
    {
        #region 数据类型转换扩展

        /// <summary>
        /// 将字符串类型转换为其他数据类型
        /// </summary>
        /// <param name="input">字符串数据源</param>
        /// <param name="targetType">目标数据类型</param>
        /// <returns>目标数据类型的值</returns>
        public static object ConvertTo(this string input, Type targetType)
        {
            return FrameReflection.ConvertTo(input, targetType);
        }

        /// <summary>
        /// 将字符串类型转换为其他数据类型
        /// </summary>
        /// <typeparam name="TtartgetType">目标数据类型</typeparam>
        /// <param name="input">字符串数据源</param>
        /// <returns>目标数据类型的值</returns>
        public static TtartgetType ConvertTo<TtartgetType>(this string input)
        {
            return FrameReflection.ConvertTo<TtartgetType>(input);
        }

        #endregion
    }
}