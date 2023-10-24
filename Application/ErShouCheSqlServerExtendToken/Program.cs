global using Che168.Core.Utils.Util;
global using ErShouCheSqlServerExtendToken.App_Code.Models;
global using ErShouCheSqlServerExtendToken.Common;
using ErShouCheSqlServerExtendToken.Jobs;
using System.Threading;

/// <summary>
/// 创建时间：2023/10/24 14:01:20
/// 创建人：libinglong
/// 项目：ErShouCheSqlServerExtendToken
/// </summary>
namespace ErShouCheSqlServerExtendToken
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> param = GenerateParams(args);
            GenerateToken.Run(param,Stopwatch.StartNew());
        }
        static Dictionary<string, string> GenerateParams(string[] args)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var arg in args)
            {
                string[] a = arg.Split('=');
                dic[a[0]] = a[1].UrlDecode();
            }
            return dic;
        }
    }

}
