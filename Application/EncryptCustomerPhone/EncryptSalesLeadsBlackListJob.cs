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
using Auto.DataHelper;

namespace EncryptCustomerPhone
{
   public class EncryptSalesLeadsBlackListJob
    {
        static string logpath = AppDomain.CurrentDomain.BaseDirectory + "log\\EncryptSalesLeadsBlackList.log";
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
            writelog("************  SalesLeadsBlackList 开始加密  ************", write);
            console("************  SalesLeadsBlackList 开始加密  ************");
            EncryptSalesLeadsBlackList(ref clueMobileInfoCount);
            write.Close();
            write = null;
        }
        public static void EncryptSalesLeadsBlackList(ref int updatecount)
        {
            string sql = @"    SELECT TOP 10000 slblid ,
                   mobile 
                   FROM usedcar.dbo.SalesLeadsBlackList WHERE mobile IS NOT NULL AND mobile <> '' AND (mobileext IS NULL OR mobileext = '') ORDER BY slblid";
            List<DbParameter> listdb = new List<DbParameter>();
            DataTable dt = DataBaseOperator.UsedCarRead.ExecuteDataSet(sql, listdb.ToArray()).Tables[0];
            if (dt == null || dt.Rows.Count == 0)
            {
                string log = "************  SalesLeadsBlackList 已加密完成  ************ ";
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
                    sb.AppendLine(string.Format("UPDATE SalesLeadsBlackList SET mobileext = '{1}' WHERE slblid = {0};", dt.Rows[i]["slblid"], Che168.EnDecrypt.DESUtil.MobileEncode(dt.Rows[i]["mobile"].ToString())));
                }
                string error;
                bool result = DataBaseOperator.UsedCarWrite.ExecuteNonQuery(sb.ToString(), CommandType.Text, out error, listdb.ToArray());
                watch.Stop();
                sql = null;
                listdb = null;                
                sb = null;
                if (result && string.IsNullOrEmpty(error))
                {
                    updatecount = updatecount += dt.Rows.Count;
                    writelog(string.Format("已更新成功 {0} 条,耗时{1}秒，当前更新ID区间{2}——{3}", updatecount, (watch.ElapsedMilliseconds / 1000.0).ToString("0.00"), dt.Rows[0]["slblid"], dt.Rows[dt.Rows.Count - 1]["slblid"]), write);
                    dt = null;
                    EncryptSalesLeadsBlackList(ref updatecount);
                }
                else
                {
                    dt = null;
                    string log = string.Format("更新遇到错误,当前更新ID区间{1}——{2}；error：", updatecount, dt.Rows[0]["slblid"], dt.Rows[dt.Rows.Count - 1]["slblid"], error);
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
            Console.WriteLine("SalesLeadsBlackList " + log ?? "");
        }
    }
}
