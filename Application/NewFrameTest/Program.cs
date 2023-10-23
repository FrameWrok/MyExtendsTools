using System;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Xml;

using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Timers;
using System.Threading;
using System.Security.Policy;
namespace NewFrameTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://www.baidu.com/s?wd=kafka%E5%8F%82%E6%95%B0%E9%85%8D%E7%BD%AE%E8%AF%A6%E8%A7%A3&rsv_spt=?1&rsv_iqid=0x8060a9b200055f2f&issp=1&f=3&rsv_bp=1&rsv_idx=2&ie=utf-8&tn=baiduhome_pg&rsv_enter=1&rsv_dl=ts_0&rsv_sug3=9&rsv_sug1=10&rsv_sug7=101&rsv_sug2=1&rsv_btype=i&prefixsug=kafkaca&rsp=0&inputT=7197&rsv_sug4=8374";
            Console.WriteLine((url.IndexOf("?") > 0 ? url.Substring(0, url.IndexOf("?")) : url));
            url = "https://www.baidu.com/s";
            Console.WriteLine((url.IndexOf("?") > 0 ? url.Substring(0, url.IndexOf("?")) : url));
            url = "https://www.baidu.com/s/";
            Console.WriteLine((url.IndexOf("?") > 0 ? url.Substring(0, url.IndexOf("?")) : url));
            decimal a = 1.3m;
            Console.WriteLine(10000 * 1.3m);

            KafkaTest.testproduct();
            KafkaTest.testconsume();
            Console.ReadKey();
        }
    }
    /// <summary>
	///  通话记录实体
	/// </summary>
	[Serializable]
    public partial class QueryListModel
    {
        #region 自动生成

        /// <summary>
        /// 本地音频路径
        /// </summary>
        public string localurl { get; set; }
        /// <summary>
        /// 自增id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 录音id
        /// </summary>
        public string audioid { get; set; }
        /// <summary>
        /// 通话时长
        /// </summary>
        public int callseconds { get; set; }
        /// <summary>
        /// 录音添加时间
        /// </summary>
        public Nullable<DateTime> audiotime { get; set; }
        /// <summary>
        /// 声道标识 1主叫在左声道 2主叫在右声道 3混音
        /// </summary>
        public int recordmode { get; set; }
        #endregion
    }

}

