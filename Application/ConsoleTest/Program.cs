﻿// See https://aka.ms/new-console-template for more information
using Che168.Core.Utils.Util;
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
            //string s = Encrypt3DES(DateTime.Now.ToString(), "CHE168CRMAPI");
            //Console.WriteLine(s);
            //Console.WriteLine(Decrypt3DES(s, "CHE168CRMAPI"));
            //Console.ReadKey();

            //Parallel.For(1, 1_000_000, (i) => { });
            //TestMark();
            string s = Che168.Core.EnDecrypt.DESUtil.DES3CBCEncode("2025-01-01", "libinglonghengshui");
            //Console.WriteLine(Auto2scSqlExtends.B5A4E830C5904921.Decode("MO5xKJkywxg=", "", "L5Fdni+Zr1q3QvV9KXUQ2A=="));

            Console.WriteLine(s);
            Console.ReadLine();

        }

        static async void TestMark()
        {
            string md = @"**日报 2022-12-19 (线索派发日期为 2022-12-16 )**  
**一、全国36%,环比5.04% ;涨价包19%,环比27.22%**  
**1.CC清洗：**  
##### 整体:24% ;环比14.33%
##### 外采:31% ;环比4.03%
##### 自产:19% ;环比36.87%
**2.未CC清洗：**
##### 整体:39% ;环比3.70%
##### 外采:45% ;环比21.94%
##### 自产:39% ;环比3.42%
**3.TTP战败：**  
**二、预警：**  
**1.外采-CC清洗：**  

**2.外采-未CC清洗：**   

##### 58% ;环比-17.37% :2_5_1098:个人车源_外部_平安-权益管家运营位
##### 50% ;环比116.64% :2_5_1210:个人车源_外部_罗马C1高质
**3.自产-CC清洗：**  

**4.自产-未CC清洗：**  
##### 65% ;环比3.54% :2_4_1081:个人车源_内嵌_新车留资结果页弹窗
##### 64% ;环比13.27% :10_4_864:置换信息_内嵌_新车置换结果页-模型测试
##### 60% ;环比-5.04% :25_4_601:我要换车_内嵌_置换计算器换车(内嵌)
##### 60% ;环比50.00% :3_4_923:估值线索_内嵌_主软-二手车估值糖豆入口（测试）
##### 57% ;环比128.56% :25_4_898:我要换车_内嵌_NQ-新卖车管家-换车
##### 56% ;环比-2.77% :15_4_867:平安车主_内嵌_平安好车主享服务-全部服务-估值
##### 56% ;环比3.18% :25_4_1004:我要换车_内嵌_nq新版二手车首页二期-换车糖豆
##### 55% ;环比58.14% :2_4_218:个人车源_内嵌_底部TAB二手车首页顶部卖车(主软)
##### 52% ;环比4.34% :3_4_1261:估值线索_内嵌_主软首页feed流估值
##### 51% ;环比19.22% :2_4_170:个人车源_内嵌_首页顶部TAB服务ICON区域全部卖车(主软)
##### 50% ;环比-10.01% :2_4_362:个人车源_内嵌_停售车型页底部卖车按钮(主软)
##### 49% ;环比-5.12% :2_2_893:个人车源_M端_M-新卖车管家-卖车
##### 47% ;环比-9.43% :2_4_920:个人车源_内嵌_主软-选车tab-顶部二手车频道-卖车
##### 46% ;环比6.59% :3_4_1005:估值线索_内嵌_nq新版二手车首页二期-估值糖豆
##### 45% ;环比2.27% :3_4_922:估值线索_内嵌_主软-选车tab-顶部二手车频道-估值
##### 44% ;环比-11.12% :2_3_485:个人车源_APP端_车源管理去发车(APP)
##### 44% ;环比31.26% :3_2_1124:估值线索_M端_平安好车主app——车辆评估
##### 43% ;环比45.73% :2_5_1240:个人车源_外部_机器人挖掘60天历史高质
##### 43% ;环比71.44% :2_4_912:个人车源_内嵌_主软车源管理去发车NQ
##### 40% ;环比4.58% :2_4_895:个人车源_内嵌_NQ-新卖车管家-卖车
##### 40% ;环比0.00% :2_4_855:个人车源_内嵌_主软-买车详情页-帮您卖车按钮



Markdown cells support standard Markdown syntax as well as GitHub Flavored Markdown (GFM). Open the preview to see these rendered.

### Basics

# H1
## H2
### H3
#### H4
##### H5
###### H6

---

*italic*, **bold**, ~~Scratch this.~~

`inline code`

### Lists

1. First ordered list item
2. Another item
  * Unordered sub-list. 
1. Actual numbers don't matter, just that it's a number
  1. Ordered sub-list
4. And another item.

### Quote

> Peace cannot be kept by force; it can only be achieved by understanding.

### Links

[I'm an inline-style link](https://www.google.com)
http://example.com

You can also create a link to another note: (Note menu -> Copy Note Link -> Paste)
[01 - Getting Started](quiver-note-url/D2A1CC36-CC97-4701-A895-EFC98EF47026)

### Tables

| Tables        | Are           | Cool  |
| ------------- |:-------------:| -----:|
| col 3 is      | right-aligned | $1600 |
| col 2 is      | centered      |   $12 |
| zebra stripes | are neat      |    $1 |

### GFM Task Lists

- [ ] a task list item
- [ ] list syntax required
- [ ] normal **formatting**, @mentions, #1234 refs
- [ ] incomplete
- [x] completed

### Inline LaTeX

You can use inline LaTeX inside Markdown cells as well, for example, $x^2$.";
            MarkDownTools converter = new MarkDownTools();
            string html = converter.MarkDownTextToHtml(md);
            var t = HtmlTools.HtmlToImage(html);
            t.Wait();
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