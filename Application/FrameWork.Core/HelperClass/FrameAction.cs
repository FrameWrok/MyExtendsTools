/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System;
using System.Diagnostics;

namespace FrameWork.Core.HelperClass
{
    /// <summary>
    /// Action 类
    /// </summary>
    public static class FrameAction
    {
        /// <summary>
        /// 运行时间长度测试
        /// </summary>
        /// <param name="action">等待被测试的函数</param>
        /// <param name="times">执行的次数</param>
        /// <returns>运行的总时间</returns>
        public static TimeSpan ExecuteTime(Action action, int times)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < times; i++)
            {
                action.Invoke();
            }

            stopwatch.Stop();
            return TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds);
        }

        /// <summary>
        /// For 循环器
        /// </summary>
        /// <param name="action">要循环 的动作</param>
        /// <param name="times">循环 的次数</param>
        public static void ForCirculator(Action action, int times)
        {
            for (int i = 0; i < times; i++)
            {
                action.Invoke();
            }
        }
    }
}
