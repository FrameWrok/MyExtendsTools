using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using Che168.Utils.DataHelper;
using System.Text;
using System.Data.Common;
using Auto.DataHelper.DbProviderFactory;
using System.Diagnostics;
using System.Threading;

namespace EncryptCustomerPhone
{
    public class EncryptClueCarMatchJob
    {
        static string logpath = AppDomain.CurrentDomain.BaseDirectory + "log\\EncryptClueCarMatch.log";
        static StreamWriter write = null;
        public static void run(object o)
        {
            if (write == null)
                if (File.Exists(logpath))
                    write = File.AppendText(logpath);
                else
                    write = File.CreateText(logpath);
            write.AutoFlush = true;

            int clueMobileInfoCount = 0;
            writelog("************  ClueCarMatch 开始加密  ************", write);
            console(" * ***********ClueCarMatch 开始加密 * ***********");
            EncryptClueCarMatch(ref clueMobileInfoCount);
            write.Close();
            write = null;
        }
        public static void EncryptClueCarMatch(ref int updatecount)
        {
            string sql = @"SELECT TOP 10000 ccmid ,
              phone 
              FROM ClueCarMatch  WHERE phone IS NOT NULL AND phone <> '' AND (phoneext IS NULL OR phoneext = '') ORDER BY ccmid";
            List<DbParameter> listdb = new List<DbParameter>();
            DataTable dt = Che168DataBaseOperator.UsedCarDealerRead.ExecuteDataSet(sql, listdb.ToArray()).Tables[0];
            if (dt == null || dt.Rows.Count == 0)
            {
                string log = "************  ClueCarMatch 已加密完成  ************";
                writelog(log, write);
                console(log);
            }
            else
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.AppendLine(string.Format("UPDATE ClueCarMatch SET phoneext = '{1}' WHERE ccmid = {0};", dt.Rows[i]["ccmid"], Che168.EnDecrypt.DESUtil.MobileEncode(dt.Rows[i]["phone"].ToString())));
                }
                string error;
                bool result = Che168DataBaseOperator.UsedCarDealerWrite.ExecuteNonQuery(sb.ToString(), CommandType.Text, out error, listdb.ToArray());
                watch.Stop();
                sql = null;
                listdb = null;                
                sb = null;
                if (result)
                {
                    updatecount = updatecount += dt.Rows.Count;                    
                    writelog(string.Format("已更新成功 {0} 条,耗时{1}秒，当前更新ID区间{2}——{3}", updatecount, (watch.ElapsedMilliseconds / 1000.0).ToString("0.00"), dt.Rows[0]["ccmid"], dt.Rows[dt.Rows.Count - 1]["ccmid"]), write);
                    dt = null;
                    //Thread.Sleep(50 * 1000);
                    EncryptClueCarMatch(ref updatecount);
                }
                else
                {
                    dt = null;
                    string log = string.Format("更新遇到错误,当前更新ID区间{1}——{2}；error：", updatecount, dt.Rows[0]["ccmid"], dt.Rows[dt.Rows.Count - 1]["ccmid"], error);
                    writelog(log, write);
                    console(log);
                }
            }
        }
        public static void writelog(string log, StreamWriter logwriter)
        {            
            logwriter.WriteLine("{0} : {1}", DateTime.Now, log ?? "");
        }
        public static void console(string log)
        {
            Console.WriteLine("ClueCarMatch " + log ?? "");
        }
    }
}
