//====================================================================
// This file is generated as part of Web project conversion.
// The extra class 'StringHelper' in the code behind file in 'Login.aspx.cs' is moved to this file.
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


	public class StringHelper
	{
		public static byte[] HexStringToBytes(string hex) 
		{
			if (hex.Length==0)
			{
				return new byte[] {0};
			}

			if (hex.Length % 2 == 1)
			{
				hex = "0" + hex;
			}

			byte[] result = new byte[hex.Length/2];

			for (int i=0; i<hex.Length/2; i++)
			{					
				result[i] = byte.Parse(hex.Substring(2*i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
			}

			return result;
		}

		public static string BytesToHexString(byte[] input) 
		{
			StringBuilder hexString = new StringBuilder(64);

			for (int i = 0; i < input.Length; i++) 
			{
				hexString.Append(String.Format("{0:X2}", input[i]));
			}
			return hexString.ToString();
		}

		public static string BytesToDecString(byte[] input)
		{
			StringBuilder decString = new StringBuilder(64);

			for (int i = 0; i < input.Length; i++) 
			{
				decString.Append(String.Format(i==0?"{0:D3}":"-{0:D3}", input[i]));
			}
			return decString.ToString();
		}

		// Bytes are string
		public static string ASCIIBytesToString(byte[] input)
		{
			System.Text.ASCIIEncoding enc = new ASCIIEncoding();
			return enc.GetString(input);
		}
		public static string UTF16BytesToString(byte[] input)
		{
			System.Text.UnicodeEncoding enc = new UnicodeEncoding();
			return enc.GetString(input);
		}
		public static string UTF8BytesToString(byte[] input)
		{
			System.Text.UTF8Encoding enc = new UTF8Encoding();
			return enc.GetString(input);
		}

		// Base64
		public static string ToBase64(byte[] input)
		{
			return Convert.ToBase64String(input);
		}

		public static byte[] FromBase64(string base64)
		{
			return Convert.FromBase64String(base64);
		}
	}

}