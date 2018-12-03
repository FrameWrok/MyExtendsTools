
namespace System
{
    using System.Runtime.InteropServices;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data;
    using SmartExtends.Frame;
    using SmartExtends.System;


    /// <summary>
    /// 自增长 GUID 类
    /// </summary>
    public static partial class MyGuid
    {
        /// <summary>
        /// 获取自增长的 GUID
        /// </summary>
        public static Guid Guid
        {
            get
            {
                return NewIdentityGuid();
            }
        }

        /// <summary>
        /// 获取自增长 guid 的字符串表示形式
        /// </summary>
        /// <returns></returns>
        public static string ToString()
        {
            return Guid.ToString();
        }

        #region 获取自动增长的guid

        /// <summary>
        /// 获取下一个自增长的 GUID
        /// </summary>
        /// <returns></returns>
        private static Guid NewIdentityGuid()
        {
            Guid guid;
            int result = UuidCreateSequential(out guid);
            if (result != RPC_S_OK)
            {
                ////throw new ApplicationException("Create sequential guid failed: " + result);
                return Guid.NewGuid();
            }

            return guid;
        }

        private const int RPC_S_OK = 0;

        [DllImport("rpcrt4.dll", SetLastError = true)]
        private static extern int UuidCreateSequential(out Guid guid);
        #endregion

        static MyGuid()
        {
            Authority a = new Authority();
        }

    }


    /// <summary>
    /// 对object对象的扩展
    /// </summary>
    public static partial class FrameObjectExtends
    {
        private static JsonSerializerSettings settings = new JsonSerializerSettings()
         {
             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
         };

        /// <summary>
        /// 将object对象用 Newtonsoft.Json 工具序列化为 json 字符串
        /// </summary>
        /// <param name="o">要序列化的对象</param>
        /// <param name="dateTimeFormat">时间格式</param>
        /// <returns>序列化后的字符串</returns>
        public static string ToJson(this object o, string dateTimeFormat)
        {
            ////if(dateTimeFormat.IsNullOrEmptyOrBlank())
            ////{
            ////    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            ////    MemoryStream ms = new MemoryStream();
            ////    ser.WriteObject(ms, o);
            ////    string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ////    ms.Close();
            ////    return jsonString;
            ////}
            var isoDateTimeConverter = new IsoDateTimeConverter
            {
                DateTimeFormat = string.IsNullOrEmpty(dateTimeFormat) ? "yyyy-MM-dd HH:mm:ss" : dateTimeFormat
            };

            return JsonConvert.SerializeObject(o, isoDateTimeConverter);            
        }        

        /// <summary>
        /// 将object对象用 Newtonsoft.Json 工具序列化为 json 字符串
        /// </summary>
        /// <param name="o">要序列化的对象</param>
        /// <returns>序列化后的字符串</returns>
        public static string ToJson(this object o)
        {
            return JsonConvert.SerializeObject(o, settings);
            ////return Jil.JSON.Serialize(o);
        }

        /// <summary>
        /// 将json字符串用 Newtonsoft.Json 工具反序列化为 T 类型对象
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="json">源字符串</param>
        /// <returns>反序列化的结果</returns>
        public static T FromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }

    /// <summary>
    /// 对object对象的扩展
    /// </summary>
    public static partial class FrameObjectExtends
    {
        #region
        /// <summary>
        /// 根据不同的映射类型及数据库类型返回实体对象的Parameter参数列表
        /// </summary>
        /// <param name="item">实体或匿名实体</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <param name="smartMappingType">映射方式</param>
        /// <returns>Parameter参数列表</returns>
        public static List<DbParameter> GetDbParameters<T>(this T item, DataBaseType smartDataBaseType, FrameMappingType smartMappingType) where T : new()
        {
            return System.Data.FrameDataBase.GetParameters(item, smartDataBaseType, smartMappingType);
        }

        /// <summary>
        /// 根据不同的数据库类型返回实体对象的Parameter参数列表
        /// </summary>
        /// <param name="item">实体或匿名实体</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <returns>Parameter参数列表</returns>
        public static List<DbParameter> GetDbParameters<T>(this T item, DataBaseType smartDataBaseType) where T : new()
        {
            return System.Data.FrameDataBase.GetParameters(item, smartDataBaseType, FrameMappingType.PropertyPublic);
        }

        /// <summary>
        /// 根据实体对象返回由属性生成的SqlServer数据库的Parameter参数列表
        /// </summary>
        /// <param name="item">实体或匿名实体</param>
        /// <returns>Parameter参数列表</returns>
        public static List<DbParameter> GetDbParameters<T>(this T item) where T : new()
        {
            return System.Data.FrameDataBase.GetParameters(item, DataBaseType.SqlServer, FrameMappingType.PropertyPublic);
        }

        #endregion
    }    
}

namespace System.Reflection
{
    /// <summary>
    /// 获取某类型的属性
    /// </summary>
    public static partial class OtherExtensions
    {
        /// <summary>
        /// 返回某类型的属性列表
        /// </summary>
        /// <param name="o">类型</param>
        /// <param name="binding">修饰符条件</param>
        /// <returns>属性列表</returns>
        public static PropertyInfo[] GetPropertyInfoS(this Type o, BindingFlags binding = Reflection.BindingFlags.Public|BindingFlags.IgnoreCase|BindingFlags.Instance)
        {
            return o.GetProperties(binding);
        }
    }
}
