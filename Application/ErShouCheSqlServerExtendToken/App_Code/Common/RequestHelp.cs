namespace ErShouCheSqlServerExtendToken.Common
{
    public static class RequestHelp
    {
        public static T Get<T>(string url, Dictionary<string, object> p)
        {
            try
            {
                string result = Get(url, p);
                if (result.IsNotNullOrEmptyOrBlank())
                    return result.ToObject<T>();
            }
            catch (Exception ex)
            {
                LogInfo.console($"URL异常:{ex.Message}");
            }
            return default(T);
        }
        public static T Post<T>(string url, Dictionary<string, object> p)
        {
            try
            {
                string result = Post(url, p);
                if (result.IsNotNullOrEmptyOrBlank())
                    return result.ToObject<T>();
            }
            catch (Exception ex)
            {
                LogInfo.console($"URL异常:{ex.Message}");
            }
            return default(T);
        }
        public static string Get(string url, Dictionary<string, object> p)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                string result = Che168.Core.Utils.Util.HttpClientHelper.Get(url + $"?_appid=2sc.pc&{p.Select(p => p.Key + "=" + p.Value.ToString()).Join("&")}");
                LogInfo.console(result);

                LogInfo.addMonitor(typeof(RequestHelp), url, stopwatch);
                return result;
            }
            catch (Exception ex)
            {
                LogInfo.console($"URL异常:{ex.Message}");
            }
            return null;
        }

        public static string Post(string url, Dictionary<string, object> p)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                string result = Che168.Core.Utils.Util.HttpClientHelper.Post(url, $"_appid=2sc.pc&{p.Select(p => p.Key + "=" + p.Value.ToString().UrlEncode()).Join("&")})");
                LogInfo.addMonitor(typeof(RequestHelp), url, stopwatch);
                return result;
            }
            catch (Exception ex)
            {
                LogInfo.console($"URL异常:{ex.Message}");
            }
            return null;
        }


    }

}
