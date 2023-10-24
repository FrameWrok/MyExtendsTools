using log4net;
using log4net.Repository;

namespace App_Code.Common
{
    public static partial class LogHelper
    {
        public static ILoggerRepository repository = LogManager.CreateRepository("NETCoreRespository");
        private static ILog logError = LogManager.GetLogger(repository.Name, "LogError");
        private static ILog logInfo = LogManager.GetLogger(repository.Name, "LogInfo");
        private static ILog logVisit = LogManager.GetLogger(repository.Name, "LogVisit");

        static LogHelper()
        {
            log4net.Config.XmlConfigurator.Configure(LogHelper.repository, new FileInfo("log4net.config"));
        }

        /// <summary>
        /// 记录info日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Info(string message)
        {
            logInfo.Info(message);
        }

        /// <summary>
        /// 记录info日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void InfoAndConsoleOut(string message)
        {
            logInfo.Info(message);
            Console.WriteLine(message);
        }

        /// <summary>
        /// 记录Debug日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Debug(string message)
        {
            logInfo.Debug(message);
        }

        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="ex"></param>
        public static void Warn(string message, Exception ex = null)
        {
            logInfo.Warn(message, ex);
        }

        /// <summary>
        /// 记录error日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="ex"></param>
        public static void Error(string message, Exception ex = null)
        {
            logError.Error(message, ex);
        }

        /// <summary>
        /// 记录访问日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Visit(string message)
        {
            logVisit.Info(message);
        }
    }
}
