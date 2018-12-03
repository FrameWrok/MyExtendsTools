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
using System.Web;

namespace FrameWork.Core.HttpHandlers
{
    public static class HandlerDownfile
    {
        /// <summary>
        /// 通过实现 System.Web.IHttpHandler 接口的自定义 HttpHandler 启用 HTTP Web 请求的处理
        /// </summary>
        /// <param name="context">System.Web.HttpContext 对象，它提供对用于为 HTTP 请求提供服务的内部服务器对象（如 Request、Response、Session  和 Server）的引用。</param>
        public static void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("{'a':'abc','time':'" + DateTime.Now.Second.ToString() + "'}");

            HttpResponse response = context.Response;
            HttpRequest request = context.Request;
            System.IO.Stream istream = null;
            byte[] buffer = new Byte[10240];
            int length;
            long dataToRead;

            try
            {
                FileInfo file = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "/Resource.rar");
                string filepath = file.FullName; ////待下载的文件路径

                istream = new System.IO.FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                response.Clear();

                dataToRead = istream.Length;

                long p = 0;
                if (request.Headers["Range"] != null)
                {
                    response.StatusCode = 206;
                    p = long.Parse(request.Headers["Range"].Replace("bytes=", string.Empty).Replace("-", string.Empty));
                }

                if (p != 0)
                {
                    response.AddHeader("Content-Range", "bytes " + p.ToString() + "-" + ((long)(dataToRead - 1)).ToString() + "/" + dataToRead.ToString());
                }

                response.AddHeader("Content-Length", ((long)(dataToRead - p)).ToString());
                response.ContentType = "application/octet-stream";
                response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(System.Text.Encoding.GetEncoding(65001).GetBytes(Path.GetFileName(file.FullName))));

                istream.Position = p;
                dataToRead = dataToRead - p;

                while (dataToRead > 0)
                {
                    if (response.IsClientConnected)
                    {
                        length = istream.Read(buffer, 0, 10240);

                        response.OutputStream.Write(buffer, 0, length);
                        response.Flush();

                        buffer = new Byte[10240];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (istream != null)
                {
                    istream.Close();
                }

                response.End();
            }
        }
    }
}
