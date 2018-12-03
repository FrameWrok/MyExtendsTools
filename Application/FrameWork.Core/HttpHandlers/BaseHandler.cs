/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System;
using System.Web;

namespace FrameWork.Core.HttpHandlers
{
    public class BaseHandler : IHttpHandler
    {
        private static bool isReusable = false;        

        /// <summary>
        /// 通过实现 System.Web.IHttpHandler 接口的自定义 HttpHandler 启用 HTTP Web 请求的处理。        /// 
        /// </summary>
        /// <param name="context">System.Web.HttpContext 对象，它提供对用于为 HTTP 请求提供服务的内部服务器对象（如 Request、Response、Session  和 Server）的引用。</param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("{'a':'abc','time':'" + DateTime.Now.Second.ToString() + "'}");

            try
            {
                string s = "aaa";
                Int32 d = Int32.Parse(s);
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
            /////XmlReaderSettings readerSettings = new XmlReaderSettings ( );
            ////readerSettings.IgnoreComments = true;
            ////readerSettings.IgnoreWhitespace = true;
            ////XmlReader reader = XmlReader.Create ( context.Request.InputStream, readerSettings );
            ////XmlDocument dom = new XmlDocument ( );
            ////dom.Load ( reader );
            context.Response.End();
        }

        /// <summary>
        /// Gets a value indicating whether 获取一个值，该值指示其他请求是否可以使用 System.Web.IHttpHandler 实例。
        /// 如果 System.Web.IHttpHandler 实例可再次使用，则为 true；否则为 false。
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return isReusable;
            }
        }
    }
}
