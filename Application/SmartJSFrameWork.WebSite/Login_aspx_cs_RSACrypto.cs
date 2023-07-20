//====================================================================
// This file is generated as part of Web project conversion.
// The extra class 'RSACrypto' in the code behind file in 'Login.aspx.cs' is moved to this file.
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


namespace CodeName.APPa
 {


	public class RSACrypto
	{
		private RSACryptoServiceProvider _sp;

		public RSAParameters ExportParameters(bool includePrivateParameters)
		{
			return _sp.ExportParameters(includePrivateParameters);
		}

		public void InitCrypto(string keyFileName)
		{
			CspParameters cspParams = new CspParameters();
			cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
			// To avoid repeated costly key pair generation
			_sp = new RSACryptoServiceProvider(cspParams);
			string path = keyFileName;
			System.IO.StreamReader reader = new StreamReader(path);
			string data = reader.ReadToEnd();
			_sp.FromXmlString(data);
		}

		public byte[] Encrypt(string txt)
		{
			byte[] result;

			ASCIIEncoding enc = new ASCIIEncoding();
			int numOfChars = enc.GetByteCount(txt);
			byte[] tempArray = enc.GetBytes(txt);		
			result = _sp.Encrypt(tempArray, false);

			return result;	
		}

		public byte[] Decrypt(byte[] txt)
		{
			byte[] result;

			result = _sp.Decrypt(txt, false);

			return result;
		}
	}

}