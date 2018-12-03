using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ExtendsToolsForm.BLL
{
    /// <summary>
    /// config 操作
    /// </summary>
    public static class ConfigBLL
    {
        static DataContractJsonSerializer serializer = null;
        static string jsonConfigPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LiBingLong_SmartTools\applicationConfig.json", System.Configuration.ConfigurationManager.AppSettings["applicationConfigPath"] ?? "");
        static ConfigBLL()
        {
            serializer = new DataContractJsonSerializer(typeof(Dictionary<string, string>));
            if (!File.Exists(jsonConfigPath))
                InitConfig();
        }

        private static void InitConfig(Dictionary<string, string> dic = null)
        {
            File.Delete(jsonConfigPath);
            Stream stream = File.Create(jsonConfigPath);
            serializer.WriteObject(stream, dic ?? new Dictionary<string, string>());
            stream.Close();
            var file = new FileInfo(jsonConfigPath);
            file.Attributes = FileAttributes.Hidden;
        }
        private static Dictionary<string, string> GetConfigDictionary()
        {
            var stream = File.OpenRead(jsonConfigPath);
            try
            {
                Dictionary<string, string> config = (Dictionary<string, string>)serializer.ReadObject(stream);
                stream.Close();
                return config;
            }
            catch (Exception ex)
            {
                stream.Close();
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
                return (T)(new DataContractJsonSerializer(typeof(T)).ReadObject(new MemoryStream(buffer)));
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

            MemoryStream stream = new MemoryStream();
            new DataContractJsonSerializer(typeof(T)).WriteObject(stream, t);
            string configString = Convert.ToBase64String(stream.GetBuffer());

            if (dic.ContainsKey(key.ToLower()))
                dic[key.ToLower()] = configString;
            else
                dic.Add(key.ToLower(), configString);
            InitConfig(dic);
        }
    }
}
