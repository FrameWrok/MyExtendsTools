/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FrameWork.Core.HelperClass
{
    /// <summary>
    /// 随机函数类
    /// </summary>
    public static partial class FrameRandom
    {
        private static Random rand = new Random();

        /// <summary>
        /// 返回非负随机数
        /// </summary>
        /// <returns>大于等于零且小于 System.Int32.MaxValue 的 32 位带符号整数</returns>
        public static Int32 Next()
        {
            return rand.Next();
        }

        /// <summary>
        /// 返回一个小于所指定最大值的非负随机数
        /// </summary>
        /// <param name="maxValue">要生成的随机数的上界（随机数不能取该上界值）。maxValue 必须大于等于零</param>
        /// <returns>大于等于零且小于 maxValue 的 32 位带符号整数，即：返回值的范围通常包括零但不包括 maxValue。不过，如果 maxValue 等于零，则返回maxValue</returns>
        public static Int32 Next(Int32 maxValue)
        {
            return rand.Next(maxValue);
        }

        /// <summary>
        /// 返回一个指定范围内的随机数
        /// </summary>
        /// <param name="minValue">返回的随机数的下界（随机数可取该下界值）</param>
        /// <param name="maxValue">返回的随机数的上界（随机数不能取该上界值）。maxValue 必须大于等于 minValue</param>
        /// <returns>一个大于等于 minValue 且小于 maxValue 的 32 位带符号整数，即：返回的值范围包括 minValue 但不包括 maxValue。如果 minValue 等于 maxValue，则返回 minValue</returns>
        public static Int32 Next(Int32 minValue, Int32 maxValue)
        {
            return rand.Next(minValue, maxValue);
        }

        /// <summary>
        /// 用随机数填充指定字节数组的元素
        /// </summary>
        /// <param name="buffer">包含随机数的字节数组</param>
        public static void NextBytes(byte[] buffer)
        {
            rand.NextBytes(buffer);
        }

        /// <summary>
        /// 返回一个介于 0.0 和 1.0 之间的随机数
        /// </summary>
        /// <returns>大于等于 0.0 并且小于 1.0 的双精度浮点数</returns>
        public static double NextDouble()
        {
            return rand.NextDouble();
        }
    }

    /// <summary>
    /// 随机函数类
    /// </summary>
    public static partial class FrameRandom
    {
        /// <summary>
        /// 返回指定长度的大小写字母的随机组合
        /// </summary>
        /// <param name="length">字母随机组合的长度</param>
        /// <returns>大小写字母的随机组合结果</returns>
        public static string NextLetter(Int32 length)
        {
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(PrivateClass.RandBytes[Next(10, 62)]);
            }

            return result.ToString();
        }

        /// <summary>
        /// 返回指定长度的数字与大小写字母的随机组合
        /// </summary>
        /// <param name="length">数字与字母随机组合的长度</param>
        /// <returns>数字与大小写字母的随机组合结果</returns>
        public static string NextNumberAndLetter(Int32 length)
        {
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(PrivateClass.RandBytes[Next(0, 62)]);
            }

            return result.ToString();
        }

        /// <summary>
        /// 返回指定长度的大写字母的随机组合
        /// </summary>
        /// <param name="length">大写字母的随机组合的长度</param>
        /// <returns>大写字母的随机组合结果</returns>
        public static string NextUpper(Int32 length)
        {
            return NextLower(length).ToUpper();
        }

        /// <summary>
        /// 返回指定长度的小写字母的随机组合
        /// </summary>
        /// <param name="length">小写字母的随机组合的长度</param>
        /// <returns>小写字母的随机组合结果</returns>
        public static string NextLower(Int32 length)
        {
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(PrivateClass.RandBytes[Next(10, 36)]);
            }

            return result.ToString();
        }

        /// <summary>
        /// 返回指定长度的数字与小写字母的随机组合
        /// </summary>
        /// <param name="length">数字与小写字母的随机组合的长度</param>
        /// <returns>数字与小写字母的随机组合结果</returns>
        public static string NextNumberAndLower(Int32 length)
        {
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(PrivateClass.RandBytes[Next(0, 36)]);
            }

            return result.ToString();
        }

        /// <summary>
        /// 返回指定长度的数字与大写字母的随机组合
        /// </summary>
        /// <param name="length">数字与大写字母的随机组合的长度</param>
        /// <returns>数字与大写字母的随机组合结果</returns>
        public static string NextNumberAndUpper(Int32 length)
        {
            return NextNumberAndLower(length).ToUpper();
        }
    }

    /// <summary>
    /// 随机函数类
    /// </summary>
    public static partial class FrameRandom
    {
        /// <summary>
        /// 返回输入参数的指定长度的随机列表
        /// </summary>
        /// <param name="arrayInput">传入的列表</param>
        /// <param name="length">长度</param>
        /// <returns>生成的随机列表</returns>
        public static List<object> NextObject(List<object> arrayInput, Int32 length)
        {
            List<object> resultList = new List<object>();
            for (int i = 0; i < length; i++)
            {
                resultList.Add(arrayInput[Next(0, arrayInput.Count)]);
            }

            return resultList;
        }

        /// <summary>
        /// 返回输入参数的指定长度的随机对象列表
        /// </summary>
        /// <param name="arrayInput">传入的列表</param>
        /// <param name="length">长度</param>
        /// <returns>对象列表</returns>
        public static object[] NextObject(object[] arrayInput, Int32 length)
        {
            List<object> resultList = new List<object>();
            for (int i = 0; i < length; i++)
            {
                resultList.Add(arrayInput[Next(0, arrayInput.Length)]);
            }

            return resultList.ToArray();
        }
    }
}
