/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System;
using SmartExtends.System;
using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// 对数学数据类型的扩展
    /// </summary>
    public static class FrameDigitExtends
    {
        /// <summary>
        /// 返回操作数是否介于最小值与最大值之间(不包含最大值最小值)
        /// </summary>
        /// <param name="input">操作数</param>
        /// <param name="mixValue">比较的最小值</param>
        /// <param name="maxValue">比较的最大值</param>
        /// <returns>比较结果</returns>
        public static bool IsButween(this int input, double mixValue, double maxValue)
        {
            return IsButween((double)input, mixValue, maxValue);
        }

        /// <summary>
        /// 返回操作数是否介于最小值与最大值之间(不包含最大值最小值)
        /// </summary>
        /// <param name="input">操作数</param>
        /// <param name="mixValue">比较的最小值</param>
        /// <param name="maxValue">比较的最大值</param>
        /// <returns>比较结果</returns>
        public static bool IsButween(this double input, double mixValue, double maxValue)
        {
            return input > mixValue && input < maxValue;
        }

        /// <summary>
        /// 返回操作数是否介于最小值与最大值之间(不包含最大值最小值)
        /// </summary>
        /// <param name="input">操作数</param>
        /// <param name="mixValue">比较的最小值</param>
        /// <param name="maxValue">比较的最大值</param>
        /// <returns>比较结果</returns>
        public static bool IsButween(this Int64 input, double mixValue, double maxValue)
        {
            return IsButween((double)input, mixValue, maxValue);
        }

        /// <summary>
        /// 返回操作数是否介于最小值与最大值之间(不包含最大值最小值)
        /// </summary>
        /// <param name="input">操作数</param>
        /// <param name="mixValue">比较的最小值</param>
        /// <param name="maxValue">比较的最大值</param>
        /// <returns>比较结果</returns>
        public static bool IsButween(this decimal input, double mixValue, double maxValue)
        {
            return IsButween((double)input, mixValue, maxValue);
        }

        /// <summary>
        /// 返回操作数是否介于最小值与最大值之间(不包含最大值最小值)
        /// </summary>
        /// <param name="input">操作数</param>
        /// <param name="mixValue">比较的最小值</param>
        /// <param name="maxValue">比较的最大值</param>
        /// <returns>比较结果</returns>
        public static bool IsButween(this float input, double mixValue, double maxValue)
        {
            return IsButween((double)input, mixValue, maxValue);
        }
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
}
