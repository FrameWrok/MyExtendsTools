using System;

namespace FrameWork.Core.Extends
{
    /// <summary>
    /// 异常信息的扩展类
    /// </summary>
    public static partial class FrameExceptionExtends
    {
        /// <summary>
        /// 输出异常信息
        /// </summary>
        /// <param name="exception">异常信息实例</param>
        /// <param name="filePath">输出到的文件路径</param>
        /// <param name="fileName">输出的文件名称</param>
        /// <param name="fileMode">文件的打开方式</param>
        public static void OutPut(this Exception exception, string filePath, string fileName, System.IO.FileMode fileMode)
        {
            FileIO.Log.Writer(exception, filePath, fileName, fileMode);
        }
    }
}
