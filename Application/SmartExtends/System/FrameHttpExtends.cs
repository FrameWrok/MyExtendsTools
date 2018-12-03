using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using SmartExtends.System;
using System.Collections.Specialized;

namespace System.Web
{
    /// <summary>
    /// 对 Http 类型的扩展
    /// </summary>
    public static class FrameHttpExtends
    {
        /// <summary>
        /// 根据 Page.Request 中的值来初始化 T 对象,取值顺序为 Url,Form,Cookie
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <returns></returns>
        public static T ToObject<T>(this System.Web.UI.Page page) where T : new()
        {
            return page.Request.ToObject<T>();
        }
        /// <summary>
        /// 根据 HttpContext.Request 中的值来初始化 T 对象,取值顺序为 Url,Form,Cookie
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static T ToObject<T>(this HttpContext context) where T : new()
        {
            return context.Request.ToObject<T>();
        }

        /// <summary>
        /// 根据 HttpRequest 中的值来初始化 T 对象,取值顺序为 Url,Form,Cookie
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public static T ToObject<T>(this HttpRequest request) where T : new()
        {
            Type type = typeof(T);
            List<PropertyInfo> propList = type.GetProperties().ToList();
            T t = Activator.CreateInstance<T>();
            string revalue = null;
            var tc = new System.ComponentModel.TypeConverter();
            foreach (PropertyInfo p in propList)
            {
                revalue = request.Params[p.Name];
                if (!revalue.IsNullOrEmptyOrBlank())
                    p.SetValue(t, revalue.ConvertTo(p.PropertyType), null);
            }
            return t;
        }

        /// <summary>
        /// 验证是否缺少必填参数
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool ParamsIsPass(this HttpContext context, Dictionary<string, string> checkparams, out string message)
        {
            return context.Request.ParamsIsPass(checkparams, out message);
        }

        /// <summary>
        /// 验证是否缺少必填参数
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool ParamsIsPass(this HttpRequest httpRequest, Dictionary<string, string> checkparams, out string message)
        {
            message = "缺少参数：";
            checkparams = checkparams ?? new Dictionary<string, string>();
            if (checkparams.Count == 0)
                return true;
            NameValueCollection paramlist = httpRequest.HttpMethod.ToUpper() == "POST" ? httpRequest.Form : httpRequest.QueryString;
            foreach (var item in checkparams)
            {
                if (paramlist[item.Key].IsNullOrEmptyOrBlank())
                {
                    message += (item.Value ?? item.Key) + ",";
                }
            }
            message = message.TrimEnd(',') + "!";
            return message == "缺少参数：!";
        }
        private static StringWriter tw = new StringWriter();
        private static HtmlTextWriter writer = new HtmlTextWriter(tw);
        /// <summary>
        /// 由 System.Web.UI.UserControl 生成HTML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
        public static string ToHtml(this System.Web.UI.Control control)
        {
            writer = new HtmlTextWriter(new StringWriter());
            control.RenderControl(writer);
            return writer.InnerWriter.ToString();

        }
    }
}
