using App_Code.Common;
using log4net;
using log4net.Config;

public class LogInfo
{
    public static ILog logger;
    static object lockobj = new object();
    /// <summary>
    /// icloudapi 域名
    /// </summary>
    static string IcludApiHost = Config.GetAppSetting("icloudapi");
    /// <summary>
    /// 当前任务
    /// </summary>
    public static string currenttask { get; set; }
    /// <summary>
    /// 是否打印业务日志
    /// </summary>
    static bool writelog = true;

    static Dictionary<string, Dictionary<string, List<long>>> monitorStatistics = new Dictionary<string, Dictionary<string, List<long>>>();

    static LogInfo()
    {
        if (logger == null)
        {
            var repository = LogManager.CreateRepository("NETCoreRepository");
            //log4net从log4net.config文件中读取配置信息
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            logger = LogManager.GetLogger(repository.Name, "InfoLogger");
        }
    }
    public static void Init(Dictionary<string, string> param)
    {
        if (param.ContainsKey("writelog") && param["writelog"] == "0")
            writelog = false;

    }

    /// <summary>
    /// 控制台和日志文件同时打印
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="arguments"></param>
    public static void consoleAndInfo(String msg, params object[] arguments)
    {
        if (!writelog) return;
        systemconsoleAndInfo(msg, arguments);


    }

    /// <summary>
    /// 控制台和日志文件同时打印
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="arguments"></param>
    public static void systemconsoleAndInfo(String msg, params object[] arguments)
    {
        if (arguments == null || arguments.Length == 0)
            logger.Info(msg);
        else
            logger.Info(msg.Formats(arguments));
        systemconsole(msg, arguments);
    }

    public static void addMonitor(Type type, string action, Stopwatch stopWatch)
    {
        long milliseconds = stopWatch.ElapsedMilliseconds;
        stopWatch.Stop();
        stopWatch.Reset();
        if (!monitorStatistics.ContainsKey(type.Name))
            lock (lockobj)
            {
                if (!monitorStatistics.ContainsKey(type.Name))
                { monitorStatistics.Add(type.Name, new Dictionary<string, List<long>>() { { action, new List<long>() } }); };
            }
        if (!monitorStatistics[type.Name].ContainsKey(action))
        {
            lock (lockobj)
            {
                if (!monitorStatistics[type.Name].ContainsKey(action))
                    monitorStatistics[type.Name][action] = new List<long>();
            }
        }
        monitorStatistics[type.Name][action].Add(milliseconds);
        stopWatch.Start();
    }

    public static void printMonitor()
    {
        foreach (var classt in monitorStatistics)
        {
            systemconsole("Class:" + classt.Key);
            foreach (var action in classt.Value)
            {
                systemconsole("       总耗时:{1}秒,平均耗时:{2}秒,最长耗时:{3}秒,最短耗时:{4}秒,次数:{5},action:{0}",
                    action.Key,
                    action.Value.Sum() / 1000.0,
                    action.Value.Sum() / action.Value.Count() / 1000.0,
                    action.Value.Max() / 1000.0,
                    action.Value.Min() / 1000.0,
                    action.Value.Count()
                    );
            }
        }
    }


    public static void stopWatchDebugConsole(String fix, Stopwatch stopWatch)
    {
        systemconsole("{0}耗时{1}秒".Formats(fix, (stopWatch.ElapsedMilliseconds * 1.0 / 1000).ToString("f2")));
        stopWatch.Stop();
        stopWatch.Reset();
        stopWatch.Start();
    }
    /// <summary>
    /// 不受日志开关影响，必然打印
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="arguments"></param>
    public static void systemconsole(String msg, params object[] arguments)
    {
        //if (arguments == null || arguments.Length == 0)
        //    System.Console.WriteLine(msg);
        //else
        //    System.Console.WriteLine(msg.Formats(arguments));
        //临时添加打印文本日志
        if (arguments == null || arguments.Length == 0)
        {
            logger.Info(msg);
            System.Console.WriteLine(msg);
        }
        else
        {
            logger.Info(msg.Formats(arguments));
            System.Console.WriteLine(msg.Formats(arguments));
        }

    }
    public static void console(String msg, params object[] arguments)
    {
        if (!writelog) return;
        systemconsole(msg, arguments);
    }



    /// <summary>
    /// 记录异常日志并发送短信
    /// </summary>
    /// <param name="t"></param>
    public static void errorAndSendMsg(Exception t)
    {
        consoleAndInfo(t.Message);
        consoleAndInfo(t.StackTrace);
        errorAndSendMsg(t, Config.GetAppSetting("errorSendPhone").Split(',').ToList());
    }

    /// <summary>
    /// 记录异常日志并发送短信
    /// </summary>
    /// <param name="t"></param>
    /// <param name="phoneList">接收手机号列表</param>
    public static void errorAndSendMsg(Exception t, List<String> phoneList)
    {
        logger.Error(t);
        foreach (string phone in phoneList)
        {
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("mobile", phone);
                p.Add("message", "任务:" + currenttask + " 异常：" + t.Message);
                p.Add("_appid", "2sc.pc");
                p.Add("appid", "2sc.pc");
                p.Add("sign", "2sc.pc");
                Che168.Core.Utils.Util.HttpClientHelper.Post(Config.GetAppSetting("Transaction") + "V1/Common/SendMessage.ashx", p.Select(p => p.Key + "=" + p.Value.ToString()).Join("&"));
            }
            catch (Exception ex)
            {
                consoleAndInfo(ex.Message);
            }
        }
    }
    /// <summary>
    /// 钉钉消息发送
    /// </summary>
    /// <param name="strwho">接收人 各种ID【UserID、工号、邮箱、帐号，多个拿逗号隔开，可混搭，必填】</param>
    /// <param name="typename">发送类型【ding】</param>
    /// <param name="title">发送的标题</param>
    /// <param name="message">发送的内容</param>
    /// <param name="strdesc">消息描述【帮助记录自定义数据】</param>
    public static string SendDing(string strwho, string typename, string title, string message, string strdesc)
    {
        if (IcludApiHost.IsNullOrEmptyOrBlank())
            throw new Exception("域名配置 IcludApi 节点不存在");
        string url = IcludApiHost.Trim('/') + "/V1/OA/SendDingDingMsg.ashx";

        Dictionary<string, object> requestParams = new Dictionary<string, object>();
        requestParams["sendwho"] = strwho;
        requestParams["type"] = typename;
        requestParams["title"] = title;
        requestParams["message"] = message;
        requestParams["reminddesc"] = strdesc;
        requestParams["_appid"] = "2sc.pc";
        requestParams["appid"] = "2sc.pc";

        string result = RequestHelp.Get(url, requestParams);
        return result;

    }

    /// <summary>
    /// 钉钉群机器人
    /// </summary>
    /// <param name="access_token"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    public static string SendRobot(string access_token, string body)
    {
        string url = $@"https://oapi.dingtalk.com/robot/send?access_token={access_token}";
        return Che168.Core.Utils.Util.HttpClientHelper.Post(url, body, "application/json", "utf-8");
    }
}

