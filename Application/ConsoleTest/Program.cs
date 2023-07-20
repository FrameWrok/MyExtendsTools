// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            string s = Encrypt3DES(DateTime.Now.ToString(), "CHE168CRMAPI");
            Console.WriteLine(s);
            Console.WriteLine(Decrypt3DES(s, "CHE168CRMAPI"));
            Console.ReadKey();
        }

        public static string Encrypt3DES(string plainText, string key)
        {
            TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            tripleDESCryptoServiceProvider.Key = mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(key));
            tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
            ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateEncryptor();
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length));
        }


        public static string Decrypt3DES(string entryptText, string key)

        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

            DES.Key = hashMD5.ComputeHash(Encoding.UTF8.GetBytes(key));

            DES.Mode = CipherMode.ECB;

            ICryptoTransform DESDecrypt = DES.CreateDecryptor();

            string result;

            try

            {
                byte[] Buffer = Convert.FromBase64String(entryptText);

                result = Encoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception e)

            {
                throw (new Exception("无效的密钥或解密串不是有效的base64串", e));
            }

            return result;
        }

        public static async Task<string> HttpClientSendHttpRequestPost(string url, string imgPath, Encoding encoding)
        {
            //using (HttpClient _client = new HttpClient())
            //{
            //    using (var multiContent = new MultipartFormDataContent())
            //    {
            //        var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(imgPath));
            //        multiContent.Add(fileContent, "file", Path.GetFileName(imgPath));                           
            //        HttpResponseMessage response = _client.PostAsync(url, multiContent).Result;

            //        return response.Content.ReadAsStringAsync().Result;
            //    }
            //}

            using (HttpClient client = new HttpClient())
            {
                using (var multiContent = new MultipartFormDataContent())
                {
                    var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(imgPath));
                    multiContent.Add(fileContent, "file", Path.GetFileName(imgPath));
                    var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
                    {
                        Version = HttpVersion.Version10,
                        Content = multiContent
                    };
                    var d = await client.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead);
                    return await d.Content.ReadAsStringAsync();
                }
            }
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