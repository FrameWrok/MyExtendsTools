using System;
using System.Security.Cryptography;
using System.Text;

namespace Auto2scSqlExtends.Common
{
    public static class DES3
    {
        private const string MOBILE_KEY = "%2@8#5w7k4Q3S6K9";

        private const string IDCARD_KEY = "%1@5#2w7k5E3T4K8";

        private const string PlateNum_KEY = "*@!5@$!1IP@ssW0RD";

        internal static string DES3CBCEncode(string value, string key, string token)
        {
            string result;
            try
            {
                TripleDESCryptoServiceProvider expr_05 = new TripleDESCryptoServiceProvider();
                expr_05.Mode = CipherMode.CBC;
                expr_05.Padding = PaddingMode.PKCS7;
                //string text1 = FormsAuthentication.HashPasswordForStoringInConfigFile(key, "MD5");                
                string text = Md5Hash(key).ToUpper();//Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(key)));
                byte[] key2 = Convert.FromBase64String(text);
                expr_05.Key = key2;
                expr_05.IV = Encoding.UTF8.GetBytes(text.Substring(0, 8));
                ICryptoTransform arg_5B_0 = expr_05.CreateEncryptor();
                byte[] bytes = Encoding.UTF8.GetBytes(value);
                result = (Convert.ToBase64String(arg_5B_0.TransformFinalBlock(bytes, 0, bytes.Length)) ?? "");
            }
            catch (Exception)
            {
                result = "";
            }
            return result;
        }

        internal static string DES3CBCDecode(string value, string key, string token)
        {
            string result;
            try
            {
                byte[] array = Convert.FromBase64String(value);
                TripleDESCryptoServiceProvider expr_0C = new TripleDESCryptoServiceProvider();
                expr_0C.Mode = CipherMode.CBC;
                expr_0C.Padding = PaddingMode.PKCS7;
                new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(key));
                //string text = FormsAuthentication.HashPasswordForStoringInConfigFile(key, "MD5");
                string text = Md5Hash(key).ToUpper();// Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(key)));
                byte[] key2 = Convert.FromBase64String(text);
                expr_0C.Key = key2;
                expr_0C.IV = Encoding.UTF8.GetBytes(text.Substring(0, 8));
                byte[] bytes = expr_0C.CreateDecryptor().TransformFinalBlock(array, 0, array.Length);
                result = (Encoding.UTF8.GetString(bytes) ?? "");
            }
            catch (Exception)
            {
                result = "";
            }
            return result;
        }

        internal static string MobileEncode(string mobile, string token)
        {
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return "";
            }
            if (!TokenChekTools.IsParseToken(token))
                return "";
            return DES3CBCEncode(mobile, MOBILE_KEY, token);
        }

        internal static string MobileDecode(string mobile, string token)
        {
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return "";
            }
            if (!TokenChekTools.IsParseToken(token))
                return "";
            return DES3CBCDecode(mobile, MOBILE_KEY, token);
        }

        internal static string IdCardEncode(string idCard, string token)
        {
            if (string.IsNullOrWhiteSpace(idCard))
            {
                return "";
            }
            if (!TokenChekTools.IsParseToken(token))
                return "";
            return DES3CBCEncode(idCard, IDCARD_KEY, token);
        }

        internal static string IdCardDecode(string idCard, string token)
        {
            if (string.IsNullOrWhiteSpace(idCard))
            {
                return "";
            }
            if (!TokenChekTools.IsParseToken(token))
                return "";
            return DES3CBCDecode(idCard, IDCARD_KEY, token);
        }

        internal static string PlateNumEncode(string platenum, string token)
        {
            if (string.IsNullOrWhiteSpace(platenum))
            {
                return "";
            }
            if (!TokenChekTools.IsParseToken(token))
                return "";
            return DES3CBCEncode(platenum, PlateNum_KEY, token);
        }

        internal static string PlateNumDecode(string platenum, string token)
        {
            if (string.IsNullOrWhiteSpace(platenum))
            {
                return "";
            }
            if (!TokenChekTools.IsParseToken(token))
                return "";
            return DES3CBCDecode(platenum, PlateNum_KEY, token);
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string Md5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
