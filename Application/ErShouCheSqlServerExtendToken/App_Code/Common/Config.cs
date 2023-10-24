using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace App_Code.Common
{
    public static class Config
    {
        /// <summary>
        /// 获取 appsettings.json 字符串
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string GetAppSetting(string key)
        {
            return Che168.Core.Utils.Util.ConfigHelper.GetAppSettingsValue(key);
        }
    }
}
