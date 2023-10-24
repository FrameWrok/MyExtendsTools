using Newtonsoft.Json;

//using VB = Microsoft.VisualBasic;

/// <summary>
/// 字符串格式类型枚举，全角和半角
/// </summary>
public enum FrameStringCaseType
{
    /// <summary>
    /// 全角
    /// </summary>
    SBC,

    /// <summary>
    /// 半角
    /// </summary>
    DBC
}
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
        //VB.Strings.Replace(paramlist.Get(i), "<script", "", 1, -1, VB.CompareMethod.Text)
    }

    ///// <summary>
    ///// 去除script标签
    ///// </summary>
    ///// <param name="inpur"></param>
    ///// <returns></returns>
    //public static string ClearScriptTag(this string inpur)
    //{
    //    string s= VB.Strings.Replace(VB.Strings.Replace(inpur, "<script", "", 1, -1, VB.CompareMethod.Text), "%3Cscript", "", 1, -1, VB.CompareMethod.Text);
    //    return VB.Strings.Replace(VB.Strings.Replace(s, "script>", "", 1, -1, VB.CompareMethod.Text), "%3Cscript", "", 1, -1, VB.CompareMethod.Text);
    //}

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
    /// 转换为时间
    /// </summary>
    /// <param name="input"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public static DateTime? ToDateTime(this string input, string format = "yyyy-MM-dd HH:mm:ss")
    {
        IFormatProvider ifp = new CultureInfo("zh-CN", true);
        DateTime dt;
        if (DateTime.TryParseExact(input, format, ifp, DateTimeStyles.None, out dt))
            return dt;
        return null;
    }
    /// <summary>
    /// 对 URL 字符串进行编码。
    /// </summary>
    /// <param name="str">要编码的文本</param>
    /// <param name="encoding">编码格式</param>
    /// <returns>一个已编码的字符串</returns>
    public static string UrlEncode(this string str, Encoding encoding)
    {
        return System.Web.HttpUtility.UrlEncode(str, encoding);
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

    /// <summary>
    /// 将手机号处理为 131****1234，0313-12****14
    /// </summary>
    /// <param name="phone"></param>
    /// <returns></returns>
    public static string EncodePhone(this string phone)
    {
        if (phone.IsNotNullOrEmptyOrBlank() && phone.Length > 7)
        {
            if (phone.IsMobilePhone())
                return phone.Left(3) + "****" + phone.Right(4);
            if (phone.IsPhone())
            {
                return phone.Left(phone.Length - 6) + "****" + phone.Right(2);
            }
        }
        return phone;
    }
}

/// <summary>
/// 对 String 类的 扩展
/// </summary>
public static partial class FrameStringExtends
{
    private static readonly Type int16type = typeof(System.Int16);
    private static readonly Type int32type = typeof(System.Int32);
    private static readonly Type int64type = typeof(System.Int64);
    private static readonly Type decimaltype = typeof(System.Decimal);
    private static readonly Type doubletype = typeof(System.Double);
    private static readonly Type stringtype = typeof(string);
    private static readonly Type boolType = typeof(Boolean);
    private static readonly Type datetimetype = typeof(DateTime);
    private static readonly Type timespanType = typeof(TimeSpan);



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
    /// <summary>
    /// 将字符串类型转换为其他数据类型
    /// </summary>
    /// <param name="resources">字符串数据源</param>
    /// <param name="targetType">目标数据类型</param>
    /// <returns>目标数据类型的值</returns>
    public static object ConvertTo(this object resources, Type targetType)
    {
        object result = null;
        if (targetType.IsEnum)
        {
            ////如果目标类型是枚举，则按照枚举的方式解析
            result = Enum.Parse(targetType, resources.ToString(), true);
        }
        else if (targetType.Name.StartsWith("List"))
        {
            ////如果目标类型是List列表类型，则将Value以‘,’分隔
            if (targetType.GenericTypeArguments[0] == int16type)
                result = resources.ToString().Split(',').ToList().Select(q => { return Convert.ToInt16(q); }).ToList();
            if (targetType.GenericTypeArguments[0] == int32type)
                result = resources.ToString().Split(',').ToList().Select(q => { return Convert.ToInt32(q); }).ToList();
            if (targetType.GenericTypeArguments[0] == int64type)
                result = resources.ToString().Split(',').ToList().Select(q => { return Convert.ToInt64(q); }).ToList();
            if (targetType.GenericTypeArguments[0] == decimaltype)
                result = resources.ToString().Split(',').ToList().Select(q => { return Convert.ToDecimal(q); }).ToList();
            if (targetType.GenericTypeArguments[0] == doubletype)
                result = resources.ToString().Split(',').ToList().Select(q => { return Convert.ToDouble(q); }).ToList();
            if (targetType.GenericTypeArguments[0] == stringtype)
                result = resources.ToString().Split(',').ToList();

        }
        else if (targetType == timespanType)
        {
            ////如果目标类型是时间间隔，按照字符到时间间隔的方式解析，时间解析为毫秒
            result = Convert.ToDouble(resources);
        }
        if (targetType == datetimetype)
        {
            if (resources.ToString().IsDateTime())
                result = DateTime.Parse(resources.ToString());
        }
        else if (Nullable.GetUnderlyingType(targetType) != null)
        {
            // 可以分配NULL值的目标类型
            result = Convert.ChangeType(resources, Nullable.GetUnderlyingType(targetType));
        }
        else if (targetType == typeof(Boolean))
        {
            result = resources.ToString().ToBoolean();
        }
        else
        {
            // 其他数据类型
            result = Convert.ChangeType(resources, targetType);
        }

        return result;
    }
}

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
}

public static partial class FrameDataTableExtends
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
    /// 由Data反射为 List<int>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sourcesTable"></param>
    /// <returns></returns>
    public static List<T> ToListStruct<T>(this DataTable sourcesTable) where T : struct
    {
        var list = new List<T>();
        Type type = typeof(T);

        foreach (DataRow row in sourcesTable.Rows)
        {
            if (row[0] != null)
                list.Add((T)(row[0].ConvertTo(type)));
        }

        return list;
    }

    /// <summary>
    /// 由Data反射为 List<int>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sourcesTable"></param>
    /// <returns></returns>
    public static List<String> ToListString(this DataTable sourcesTable, string coulumnName = null)
    {
        var list = new List<String>();
        foreach (DataRow row in sourcesTable.Rows)
        {
            if (coulumnName.IsNotNullOrEmptyOrBlank())
            {
                if (row[coulumnName] != null)
                    list.Add(row[0].ToString());
                continue;
            }
            if (row[0] != null)
                list.Add(row[0].ToString());
        }
        return list;
    }
    /// <summary>
    /// 由Data反射为 List<int>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sourcesTable"></param>
    /// <returns></returns>
    public static List<int> ToListInt(this DataTable sourcesTable, string coulumnName = null)
    {
        var list = new List<int>();
        foreach (DataRow row in sourcesTable.Rows)
        {
            if (coulumnName.IsNotNullOrEmptyOrBlank())
            {
                if (row[coulumnName] != null)
                    list.Add(int.Parse(row[0].ToString()));
                continue;
            }
            if (row[0] != null && row[0] != DBNull.Value)
                list.Add(int.Parse(row[0].ToString()));
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
                if (value != DBNull.Value && value != null)
                {
                    propertyInfo.SetValue(item, (value.GetType() == propertyInfo.PropertyType ? value : value.ConvertTo(propertyInfo.PropertyType)), null);
                }
            }
        }
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

}

public static partial class FrameObjectExtends
{
    ///// <summary>
    ///// 由 object对象的属性和属性值映射到 Dictionary<string,object>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="o"></param>
    ///// <returns></returns>
    //public static Dictionary<object, object> ToDictionary<T>(this RequestPager<T> o)
    //{
    //    Dictionary<object, object> dicResult = new Dictionary<object, object>();
    //    if (o.RequestModel == null)
    //        return dicResult;
    //    Type type = typeof(T);
    //    foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
    //    {
    //        dicResult.Add(propertyInfo.Name, propertyInfo.GetValue(o.RequestModel));
    //    }
    //    dicResult.Add("PageIndex", o.PageIndex);
    //    dicResult.Add("PageSize", o.PageSize);
    //    return dicResult;
    //}

    private static JsonSerializerSettings defaultJsonParam = new JsonSerializerSettings() { };
    /// <summary>
    /// 将 object 序列化为 json 字符串，UTC时间(时间后面带 Z)
    /// 默认值为 SerializeNullValues = false, ShowReadOnlyProperties = true, EnableAnonymousTypes = true, UseUTCDateTime = false
    /// </summary>
    /// <param name="o"></param>    
    /// <returns></returns>
    public static string ToJson(this object o)
    {
        return ToJson(o, defaultJsonParam);
    }
    /// <summary>
    /// 将 object 序列化为 json 字符串
    /// </summary>
    /// <param name="o"></param>
    /// <param name="p">序列化的参数 JSONParameters</param>
    /// <returns></returns>
    public static string ToJson(this object o, JsonSerializerSettings p)
    {
        if (o is DataTable)
        {
            if (o != null)
            {
                DataTable dt = o as DataTable;

                if (dt.Rows.Count > 0)
                {
                    List<Dictionary<string, object>> jl = new List<Dictionary<string, object>>();
                    Dictionary<string, object> jd = new Dictionary<string, object>();
                    foreach (DataRow row in dt.Rows)
                    {
                        jd = new Dictionary<string, object>();
                        foreach (DataColumn c in dt.Columns)
                        {
                            jd.Add(c.ColumnName, row[c]);
                        }
                        jl.Add(jd);
                    }
                    return JsonConvert.SerializeObject(jl, p ?? defaultJsonParam);
                }
            }
        }
        return JsonConvert.SerializeObject(o, p ?? defaultJsonParam);
    }

    /// <summary>
    /// 将 json 字符串 反序列化为 object对象
    /// </summary>
    /// <param name="o"></param>
    /// <param name="useUTCDateTime">是否使用UTC时间</param>
    /// <returns></returns>
    public static T ToObject<T>(this string o)
    {
        return o.ToObject<T>(defaultJsonParam);
    }

    /// <summary>
    /// 将 json 字符串 反序列化为 object对象
    /// </summary>
    /// <param name="o"></param>
    /// <param name="param">反序列化参数</param>
    /// <returns></returns>
    public static T ToObject<T>(this string o, JsonSerializerSettings param)
    {
        return JsonConvert.DeserializeObject<T>(o, param);
    }
    public static dynamic ToDynamic(this string o)
    {
        return JsonConvert.DeserializeObject(o);
    }

}

/// <summary>
/// 对Int类型的扩展
/// </summary>
public static partial class FrameDigitExtends
{
    private static String[] hanArr = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
    private static String[] unitArr = { "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟" };
    private static string[] unitMoney = { "", "万", "亿", "万" };


    public static string ToMoneyChiness(this int sources)
    {
        string result = "";
        string sourcesstring = sources.ToString();
        int moneyClassLength = (sourcesstring.Length + 3) / 4;
        sourcesstring = sourcesstring.PadLeft(moneyClassLength * 4, '0');

        List<string> splitSources = new List<string>();

        for (int i = moneyClassLength; i > 0; i--)
        {
            splitSources.Add(sourcesstring.Substring((moneyClassLength - i) * 4, 4));
        }

        for (int i = 0; i < splitSources.Count; i++)
        {
            string currentSource = splitSources[i];
            currentSource = currentSource.TrimStart('0');
            if (i > 0 && (splitSources[i].Length - currentSource.Length) > 1)
                currentSource = "0" + currentSource;
            result += currentSource.ToMoneyChiness() + unitMoney[splitSources.Count - i - 1];
        }

        return result + "元整";
    }


    /// <summary>
    /// 将int类型转换为人民币需要的汉字形式
    /// </summary>
    /// <param name="sources"></param>
    /// <returns></returns>
    private static string ToMoneyChiness(this string sources)
    {
        string numStr = sources, result = "";
        int numLen = numStr.Length;
        string prvehan = "";
        for (int i = 0; i < numLen; i++)
        {
            int num = numStr[i] - 48;
            if (i != numLen - 1 && num != 0)
            {
                result += hanArr[num] + unitArr[numLen - 2 - i];
            }
            else
            {
                if (hanArr[num] != prvehan)
                    result += hanArr[num];
                prvehan = hanArr[num];
            }
        }
        if (result.EndsWith("零"))
            result = result.Left(result.Length - 1);
        return result;
    }
    /// <summary>
    /// 将int类型转换为人民币需要的汉字形式
    /// </summary>
    /// <param name="sources"></param>
    /// <returns></returns>
    public static string ToMoneyChiness(this decimal sources)
    {
        string numStr = ((int)sources).ToString(), result = "";
        int numLen = numStr.Length;
        result = ((int)sources).ToMoneyChiness();
        int deci = (int)((sources % 1) * 100);
        if (deci > 0)
        {
            result = result.Substring(0, result.Length - 1);
            numStr = deci.ToString();
            int index = (int)numStr[0] - 48;
            result += hanArr[index];
            if (index >= 0)
                result += "角";
            index = (int)numStr[1] - 48;
            if (index > 0)
                result += hanArr[index] + "分";
        }
        return result;
    }
}
public static partial class FrameDateTimeExtends
{
    static DateTime time1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0);
    /// <summary>
    /// 获取时间戳，如获取系统的可传入 DateTime.UtcNow
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static long GetTimeMillis(this DateTime dateTime)
    {
        return (long)((dateTime - time1970).TotalMilliseconds);
    }
}
public static partial class AutoHomeExtends
{
    /// <summary>
    /// 调用Che168.EnDecrypt.DESUtil.MobileDecod解密手机号，解密失败返回原文
    /// </summary>
    /// <param name="mobile"></param>
    /// <returns></returns>
    public static string MobileEnDecryptDecode(this string mobile)
    {
        string m = Che168.Core.EnDecrypt.DESUtil.MobileDecode(mobile);
        if (m.IsNullOrEmptyOrBlank())
            return mobile;
        return m;
    }
    /// <summary>
    /// 调用Che168.EnDecrypt.DESUtil.IdCardDecode解密身份证号，解密失败返回原文
    /// </summary>
    /// <param name="mobile"></param>
    /// <returns></returns>
    public static string IdCardEnDecryptDecode(this string idcardno)
    {
        string m = Che168.Core.EnDecrypt.DESUtil.IdCardDecode(idcardno);
        if (m.IsNullOrEmptyOrBlank())
            return idcardno;
        return m;
    }
    /// <summary>
    /// 调用Che168.EnDecrypt.DESUtil.IdCardDecode解密email，解密失败返回原文
    /// </summary>
    /// <param name="mobile"></param>
    /// <returns></returns>
    public static string EmailEnDecryptDecode(this string email)
    {
        string m = Che168.Core.EnDecrypt.DESUtil.IdCardDecode(email);
        if (m.IsNullOrEmptyOrBlank())
            return email;
        return m;
    }
    /// <summary>
    /// 调用Che168.EnDecrypt.DESUtil.MobileEncode 加密手机号，加密失败返回原文
    /// </summary>
    /// <param name="mobile"></param>
    /// <returns></returns>
    public static string MobileEnDecryptEncode(this string mobile)
    {
        string m = Che168.Core.EnDecrypt.DESUtil.MobileEncode(mobile);
        if (m.IsNullOrEmptyOrBlank())
            return mobile;
        return m;
    }
    /// <summary>
    /// 调用Che168.EnDecrypt.DESUtil.IdCardEncode 加密身份证号，加密失败返回原文
    /// </summary>
    /// <param name="mobile"></param>
    /// <returns></returns>
    public static string IdCardEnDecryptEncode(this string idcardno)
    {
        string m = Che168.Core.EnDecrypt.DESUtil.IdCardEncode(idcardno);
        if (m.IsNullOrEmptyOrBlank())
            return idcardno;
        return m;
    }
    /// <summary>
    /// 调用Che168.EnDecrypt.DESUtil.IdCardEncode 加密email，加密失败返回原文
    /// </summary>
    /// <param name="mobile"></param>
    /// <returns></returns>
    public static string EmailEnDecryptEncode(this string email)
    {
        string m = Che168.Core.EnDecrypt.DESUtil.IdCardEncode(email);
        if (m.IsNullOrEmptyOrBlank())
            return email;
        return m;
    }

}