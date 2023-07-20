//====================================================================
// This file is generated as part of Web project conversion.
// The extra class 'ADAuthManager' in the code behind file in 'Login.aspx.cs' is moved to this file.
//====================================================================


using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Web.Security;
using System.Security.Cryptography;
using System.IO;
using System.Text;


namespace CodeName.Util
 {


	public class ADAuthManager
	{
		public static bool IsAuthenticated(string connection, string domain, string username, string pwd)
		{
			// Testing only
			if ((username == "test1" && pwd =="test1") ||
				(username == "test2" && pwd =="test2"))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static string GetGroups(string connection, string domain, string username)
		{
			// Testing only
			if (username == "test1" ||
				username == "test2")
			{
				return "Users|APPa_Users|APPa_MODa_Users";
			}
			else
			{
				return "";
			}
		}
	}

}