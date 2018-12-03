/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System;

namespace FrameWork.Core.Extends
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
    }
}
