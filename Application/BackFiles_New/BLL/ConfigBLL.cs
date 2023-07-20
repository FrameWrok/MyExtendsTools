using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace BackFiles_New.BLL
{
    /// <summary>
    /// config 操作
    /// </summary>
    public static class ConfigBLL
    {
        //static string jsonConfigPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LiBingLong_SmartTools\applicationConfig.json", System.Configuration.ConfigurationManager.AppSettings["applicationConfigPath"] ?? "");
        static string jsonConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"NewBackFileConfig.json";
        static ConfigBLL()
        {
            if (!File.Exists(jsonConfigPath))
            {
                InitConfig();
            }
        }

        private static void InitConfig(Dictionary<string, string> dic = null)
        {
            File.Delete(jsonConfigPath);
            File.WriteAllText(jsonConfigPath, fastJSON.JSON.ToJSON(dic));
            var file = new FileInfo(jsonConfigPath);
            file.Attributes = FileAttributes.Hidden;
        }
        private static Dictionary<string, string> GetConfigDictionary()
        {
            try
            {
                string stringconfig = File.ReadAllText(jsonConfigPath);
                return fastJSON.JSON.ToObject<Dictionary<string, string>>(stringconfig)??new Dictionary<string, string>();
            }
            catch (Exception ex)
            {
                InitConfig();
                return GetConfigDictionary();
            }
        }

        /// <summary>
        /// 获取配置对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetConfig<T>(string key)
        {
            Dictionary<string, string> dic = GetConfigDictionary();
            if (dic.ContainsKey(key.ToLower()))
            {
                string keyconfig = dic[key.ToLower()];
                byte[] buffer = Convert.FromBase64String(keyconfig);
                keyconfig = Encoding.UTF8.GetString(buffer);
                return fastJSON.JSON.ToObject<T>(keyconfig);
            }
            return default(T);
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="key"></param>
        public static void SaveConfig<T>(T t, string key)
        {
            Dictionary<string, string> dic = GetConfigDictionary();
            string configString = Convert.ToBase64String(Encoding.UTF8.GetBytes(fastJSON.JSON.ToJSON(t)));

            if (dic.ContainsKey(key.ToLower()))
                dic[key.ToLower()] = configString;
            else
                dic.Add(key.ToLower(), configString);
            InitConfig(dic);
        }
    }
}
