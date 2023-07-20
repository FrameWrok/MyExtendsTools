using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.IO;
using CodeName.APPa;

namespace SmartJSFrameWork.WebSite
{
    public partial class LoginNew : System.Web.UI.Page
    {

        private RSACrypto rsa = new RSACrypto();
        private RSAParameters param;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                GetKey.GetKeyFunction();
            }
            string path = Session["key"].ToString() + ConfigurationManager.AppSettings["rsaPrivateKeyFilePath"];
            rsa.InitCrypto(Server.MapPath("~/MyXml/") + path);
            param = rsa.ExportParameters(true);
        }
        protected string GetRSA_E()
        {
            return StringHelper.BytesToHexString(param.Exponent);
        }
        protected string GetRSA_M()
        {
            return StringHelper.BytesToHexString(param.Modulus);
        }

        protected void btnLogin_ServerClick(object sender, System.EventArgs e)
        {
            string tmp = StringHelper.ASCIIBytesToString(rsa.Decrypt(StringHelper.HexStringToBytes(Request.Params["posx"])));
            string[] parts = tmp.Split('\\');
            string username = StringHelper.ASCIIBytesToString(StringHelper.FromBase64(parts[0]));
            string password = StringHelper.ASCIIBytesToString(StringHelper.FromBase64(parts[1]));

        }
    }
}