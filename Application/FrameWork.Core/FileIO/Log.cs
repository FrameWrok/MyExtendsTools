/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System;
using System.IO;
using System.Text;

namespace FrameWork.Core.FileIO
{
    /// <summary>
    /// 操作Log的日志，也可用于一般的文件书写
    /// </summary>
    public static partial class Log
    {
        private const int BUFFER_SIZE = 1024;

        /// <summary>
        /// 将指定的文字信息追加或创建新文件到的文件中
        /// </summary>
        /// <param name="message">添加的文字信息</param>
        /// <param name="filePath">目标文件的目录路径</param>
        /// <param name="fileName">目标文件的文件名称</param>
        /// <param name="fileMode">信息追加方式</param>
        /// <returns>是否执行成功</returns>
        public static bool Writer(string message, string filePath, string fileName, FileMode fileMode)
        {
            filePath = string.Empty + @filePath.TrimEnd(new char[] { '\\', '/' });
            string fileFullName = filePath + "\\" + fileName;
            try
            {
                switch (fileMode)
                {
                    case FileMode.OpenOrCreate:
                    case FileMode.Append:
                        {
                            DirectoryInfo di = new DirectoryInfo(filePath);
                            if (!di.Exists)
                                di.Create();
                            StreamWriter sw = new StreamWriter(fileFullName, true, Encoding.UTF8);
                            sw.WriteLine(message);
                            sw.Close();
                        }

                        break;
                    case FileMode.Create:
                        {
                            FileStream fileStream = new FileStream(fileFullName, FileMode.Create, FileAccess.Write);
                            byte[] bwrite = Encoding.Unicode.GetBytes(message);
                            fileStream.Write(bwrite, 0, bwrite.Length);
                            fileStream.Close();
                        }

                        break;
                    case FileMode.Open:
                        {
                            if (!File.Exists(fileFullName))
                                return false;
                            else
                                return Writer(message, filePath, fileName, FileMode.Append);
                        }
                }
            }
            catch (Exception ex)
            {
                Writer(ex, @"c:\error", "error.log", FileMode.Append);
            }

            return true;
        }

        /// <summary>
        /// 将指定的特定的编码字节流中的文字信息追加或创建新文件到的文件中
        /// </summary>
        /// <param name="sr">特定的编码字节流</param>
        /// <param name="filePath">目标文件的目录路径</param>
        /// <param name="fileName">目标文件的文件名称</param>
        /// <param name="fileMode">信息追加方式</param>
        /// <returns>是否执行成功</returns>
        public static bool Writer(StreamReader sr, string filePath, string fileName, FileMode fileMode)
        {
            string message;
            filePath = string.Empty + @filePath.TrimEnd(new char[] { '\\', '/' });
            string fileFullName = filePath + "\\" + fileName;
            try
            {
                switch (fileMode)
                {
                    case FileMode.OpenOrCreate:
                    case FileMode.Append:
                        {
                            DirectoryInfo di = new DirectoryInfo(filePath);
                            if (!di.Exists)
                                di.Create();
                            StreamWriter sw = new StreamWriter(fileFullName, true, Encoding.UTF8);
                            while ((message = sr.ReadLine()) != null)
                            {
                                sw.WriteLine(message);
                            }

                            sw.Close();
                        }

                        break;
                    case FileMode.Create:
                        {
                            FileStream fileStream = new FileStream(fileFullName, FileMode.Create, FileAccess.Write);
                            byte[] bwrite;
                            while ((message = sr.ReadLine()) != null)
                            {
                                bwrite = Encoding.Unicode.GetBytes(message);
                                fileStream.Write(bwrite, 0, bwrite.Length);
                            }

                            fileStream.Close();
                        }

                        break;
                    case FileMode.Open:
                        {
                            if (!File.Exists(fileFullName))
                                return false;
                            else
                                return Writer(sr, filePath, fileName, FileMode.Append);
                        }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        /// <summary>
        /// 将字节流中的文字信息追加或创建新文件到的文件中
        /// </summary>
        /// <param name="inputStream">特定的编码字节流</param>
        /// <param name="filePath">目标文件的目录路径</param>
        /// <param name="fileName">目标文件的文件名称</param>
        /// <param name="fileMode">信息追加方式</param>
        /// <returns>是否执行成功</returns>
        public static bool Writer(Stream inputStream, string filePath, string fileName, FileMode fileMode)
        {
            filePath = string.Empty + @filePath.TrimEnd(new char[] { '\\', '/' });
            string fileFullName = filePath + "\\" + fileName;
            try
            {
                switch (fileMode)
                {
                    case FileMode.OpenOrCreate:
                    case FileMode.Append:
                        {
                            DirectoryInfo di = new DirectoryInfo(filePath);
                            if (!di.Exists)
                                di.Create();

                            FileStream fs = new FileStream(fileFullName, fileMode, FileAccess.Write);
                            Writer(inputStream, fs);
                            fs.Flush();
                            fs.Close();
                        }

                        break;
                    case FileMode.Create:
                        {
                            FileStream outStream = new FileStream(fileFullName, FileMode.Create, FileAccess.Write);
                            Writer(inputStream, outStream);
                            outStream.Flush();
                            outStream.Close();
                        }

                        break;
                    case FileMode.CreateNew:
                        {
                            FileStream ////outStream = new FileStream(fileFullName, FileMode.CreateNew, FileAccess.Write);
                            outStream = File.Create(fileFullName);
                            Writer(inputStream, outStream);
                            outStream.Flush();
                            outStream.Close();
                        }

                        break;
                    case FileMode.Open:
                        {
                            if (!File.Exists(fileFullName))
                                return false;
                            else
                                return Writer(inputStream, filePath, fileName, FileMode.Append);
                        }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        /// <summary>
        /// 从输入流 inputStream 中读取，输出到 输出流 outputStream 中
        /// 此方法只负责对outputStream写入 需自己执行 flush，close操作
        /// </summary>
        /// <param name="inputStream">输入流</param>
        /// <param name="outputStream">输出流</param>
        /// <returns>返回读取书写结果</returns>
        public static bool Writer(Stream inputStream, Stream outputStream)
        {
            byte[] buffer = new byte[BUFFER_SIZE];
            Int32 count = inputStream.Read(buffer, 0, BUFFER_SIZE);
            while (count > 0)
            {
                outputStream.Write(buffer, 0, count);
                count = inputStream.Read(buffer, 0, BUFFER_SIZE);
            }

            return true;
        }

        /// <summary>
        /// 将信息写入到输出流 outputStream 中
        /// </summary>
        /// <param name="message">要写入到输出流中的信息</param>
        /// <param name="outputStream">要写入的输出流</param>
        /// <returns>返回写入结果</returns>
        public static bool Writer(string message, Stream outputStream)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            outputStream.Write(buffer, 0, buffer.Length);

            return true;
        }
    }

    /// <summary>
    /// 操作Log的日志，也可用于一般的文件书写
    /// </summary>
    public static partial class Log
    {
        /// <summary>
        /// 将客户端请求期间发送的Http值追加或创建新文件到的文件中
        /// </summary>
        /// <param name="httpRequest">客户端请求期间发送的Http值</param>
        /// <param name="filePath">目标文件的目录路径</param>
        /// <param name="fileName">目标文件的文件名称</param>
        /// <param name="fileMode">信息追加方式</param>
        /// <returns>是否执行成功</returns>
        public static bool Writer(System.Web.HttpRequest httpRequest, string filePath, string fileName, FileMode fileMode)
        {
            return Writer(httpRequest.InputStream, filePath, fileName, fileMode);
        }

        /// <summary>
        /// 将客户端请求期间发送的Http值追加或创建新文件到的文件中
        /// </summary>
        /// <param name="httpContext">客户端请求期间发送的Http值</param>
        /// <param name="filePath">目标文件的目录路径</param>
        /// <param name="fileName">目标文件的文件名称</param>
        /// <param name="fileMode">信息追加方式</param>
        /// <returns>是否执行成功</returns>
        public static bool Writer(System.Web.HttpContext httpContext, string filePath, string fileName, FileMode fileMode)
        {
            return Writer(httpContext.Request.InputStream, filePath, fileName, fileMode);
        }
    }

    /// <summary>
    /// 操作Log的日志，也可用于一般的文件书写
    /// </summary>
    public static partial class Log
    {
        public static bool Writer(Exception exception, string filePath, string fileName, FileMode fileMode)
        {
            filePath = string.Empty + @filePath.TrimEnd(new char[] { '\\', '/' });
            string fileFullName = filePath + "\\" + fileName;
            try
            {
                switch (fileMode)
                {
                    case FileMode.OpenOrCreate:
                    case FileMode.Append:
                        {
                            DirectoryInfo di = new DirectoryInfo(filePath);
                            if (!di.Exists)
                                di.Create();
                            StreamWriter sw = new StreamWriter(fileFullName, true, Encoding.UTF8);
                            sw.WriteLine("捕获异常时间" + DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond);
                            sw.WriteLine("异常信息：" + exception.Message);
                            sw.WriteLine("异常方法：" + exception.TargetSite);
                            sw.WriteLine("堆栈信息如下：↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓");
                            while (exception != null)
                            {
                                sw.WriteLine(exception.StackTrace);
                                exception = exception.InnerException;
                            }

                            sw.Close();
                        }

                        break;
                    case FileMode.Create:
                        {
                            FileStream fileStream = new FileStream(fileFullName, FileMode.Create, FileAccess.Write);
                            byte[] bwrite = null;
                            bwrite = Encoding.Unicode.GetBytes(exception.Message);
                            fileStream.Write(bwrite, 0, bwrite.Length);
                            while (exception != null)
                            {
                                bwrite = Encoding.Unicode.GetBytes(exception.StackTrace);
                                fileStream.Write(bwrite, 0, bwrite.Length);
                                exception = exception.InnerException;
                            }

                            fileStream.Close();
                        }

                        break;
                    case FileMode.Open:
                        {
                            if (!File.Exists(fileFullName))
                                return false;
                            else
                                return Writer(exception, filePath, fileName, FileMode.Append);
                        }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
    }
}
