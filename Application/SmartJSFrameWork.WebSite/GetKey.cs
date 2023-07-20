using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.IO;
using CodeName.APPa;

/// <summary>
/// Summary description for RsaInfo
/// </summary>
public class GetKey
{
    public GetKey()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void GetKeyFunction()
    {
        string keyFileName = System.Web.HttpContext.Current.Server.MapPath("~/MyXml/");
        const int keySize = 1024;

        RSACryptoServiceProvider sp = new RSACryptoServiceProvider(keySize);

        string str = sp.ToXmlString(true);
        string KeyXml = Guid.NewGuid().ToString();
        System.Web.HttpContext.Current.Session["key"] = KeyXml;
        TextWriter writer = new StreamWriter(keyFileName + KeyXml + "private.xml");
        writer.Write(str);
        writer.Close();

        str = sp.ToXmlString(false);
        writer = new StreamWriter(keyFileName + KeyXml + "public.xml");
        writer.Write(str);
        writer.Close();
    }

}
