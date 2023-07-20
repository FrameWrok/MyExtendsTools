using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MvcApplication.Controllers
{
    public class TestApiController : ApiController
    {
        [HttpGet]
        public object SetSession(string sessionName, string sessionValue)
        {
            HttpContext.Current.Session[sessionName] = sessionValue;
            return new { Message = "设置成功", SessionName = sessionName, SessionValue = sessionValue };
        }
        [HttpGet]
        public object GetSession(string sessionName)
        {
            return HttpContext.Current.Session[sessionName];
        }

    }

    public class TB_CHARGING
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 充电设备名称
        /// </summary>        
        public string SessionName { get; set; }

        /// <summary>
        /// 充电设备描述
        /// </summary>
        public string DES { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CREATETIME { get; set; }
    }

}
