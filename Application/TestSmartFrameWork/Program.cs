using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.IO;
using System.Security;
using SchemaExplorer;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Globalization;
using fastJSON;
using System.Net;

namespace TestSmartFrameWork
{
    class Program
    {
        static void Main(string[] args)
        {
            //string r = SendHttpRequestPost("http://10.168.20.27:8080/S3/Put", new Dictionary<string, string>() { { "_appid", "2sc.pc" } }, new Dictionary<string, string>() { { "t", "C:/Users/libinglong/Downloads/011002100511_48575047.pdf" } }, Encoding.UTF8);
            string r = string.Empty;
            Che168.SMS.SendNow.SendMessage("15810929598", "测试短信发送", out r);
            Console.WriteLine(r);
            Console.ReadKey();
        }
        /// <summary>
        /// 向指定的URL地址发起一个POST请求，同时可以上传一些数据项以及上传文件。
        /// </summary>
        /// <param name="url">要请求的URL地址</param>
        /// <param name="keyvalues">要上传的数据项</param>
        /// <param name="fileList">要上传的文件列表</param>
        /// <param name="encoding">发送数据项，接收的字符编码方式</param>
        /// <returns>服务器的返回结果</returns>
        public static string SendHttpRequestPost(string url, Dictionary<string, string> keyvalues, Dictionary<string, string> fileList, Encoding encoding)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");

            if (encoding == null)
                encoding = Encoding.UTF8;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.CookieContainer = ServerCookie.Scookie;
            //WebHeaderCollection handers = new WebHeaderCollection();
            //handers.Add("session", "5F19B9C84BCF88D7522F7C7949FAA3A5");
            //request.Headers = handers;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = 1000 * 60 * 10;
            request.Method = "POST";
            string boundary = "---------------------------" + Guid.NewGuid().ToString("N");
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            request.ContentType = "multipart/form-data; boundary=" + boundary;

            Stream stream = request.GetRequestStream();

            if (keyvalues != null && keyvalues.Count > 0)
            {
                foreach (KeyValuePair<string, string> kvp in keyvalues)
                {
                    stream.Write(boundaryBytes, 0, boundaryBytes.Length);
                    string str = string.Format(
                            "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}",
                            kvp.Key, kvp.Value);

                    byte[] data = encoding.GetBytes(str);
                    stream.Write(data, 0, data.Length);
                }
            }

            foreach (KeyValuePair<string, string> kvp in fileList)
            {
                stream.Write(boundaryBytes, 0, boundaryBytes.Length);
                string description = string.Format(
                        "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                        "Content-Type: application/octet-stream\r\n\r\n",
                        kvp.Key, Path.GetFileName(kvp.Value));

                byte[] header = encoding.GetBytes(description);
                stream.Write(header, 0, header.Length);
                byte[] body = File.ReadAllBytes(kvp.Value);
                stream.Write(body, 0, body.Length);
            }

            boundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            stream.Write(boundaryBytes, 0, boundaryBytes.Length);
            stream.Close();

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }

}
