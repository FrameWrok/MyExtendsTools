using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartExtends.System.Attributes
{
    /// <summary>
    /// 标识程序集授权信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class AuthorityAttribute : Attribute
    {
        private string authorityGuid;
        /// <summary>
        /// 授权信息ID
        /// </summary>
        public string AuthorityGuid
        {
            get { return authorityGuid; }
            set { authorityGuid = value; }
        }

        /// <summary>
        /// 标识该程序集需要引用SmartExtend程序集，传入授权ID
        /// </summary>
        /// <param name="authorityGuid"></param>
        public AuthorityAttribute(string authorityGuid)
        {
            this.AuthorityGuid = authorityGuid;
        }
    }
}
