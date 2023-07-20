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
namespace NewFrameTest
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal a = 1.3m;

            Console.WriteLine(10000*1.3m);

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

