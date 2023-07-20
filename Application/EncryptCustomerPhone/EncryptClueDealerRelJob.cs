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

namespace EncryptCustomerPhone
{
    public class EncryptClueDealerRelJob
    {
        static string logpath = AppDomain.CurrentDomain.BaseDirectory + "log\\EncryptClueDealerRel.log";
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
            writelog("************  ClueDealerRel 开始加密  ************", write);
            console("************  ClueDealerRel 开始加密  ************");
            EncryptClueDealerRel(ref clueMobileInfoCount);
            write.Close();
            write = null;            
        }
        public static void EncryptClueDealerRel(ref int updatecount)
        {
            string sql = @"SELECT TOP 10000 cdrid ,                
                 phone 
              FROM ClueDealerRel WHERE phone IS NOT NULL AND phone <> '' AND (phoneext IS NULL OR phoneext = '') ORDER BY cdrid";
            List<DbParameter> listdb = new List<DbParameter>();
            DataTable dt = Che168DataBaseOperator.UsedCarDealerRead.ExecuteDataSet(sql, listdb.ToArray()).Tables[0];
            if (dt == null || dt.Rows.Count == 0)
            {
                string log = "************  ClueDealerRel 已加密完成  ************ ";
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
                    sb.AppendLine(string.Format("UPDATE ClueDealerRel SET phoneext = '{1}' WHERE cdrid = {0};", dt.Rows[i]["cdrid"], Che168.EnDecrypt.DESUtil.MobileEncode(dt.Rows[i]["phone"].ToString())));                    
                }
                string error;
                bool result = Che168DataBaseOperator.UsedCarDealerWrite.ExecuteNonQuery(sb.ToString(), CommandType.Text, out error, listdb.ToArray());
                watch.Stop();
                sql = null;
                listdb = null;                
                sb = null;
                if (result && string.IsNullOrEmpty(error))
                {
                    updatecount = updatecount += dt.Rows.Count;                    
                    writelog(string.Format("已更新成功 {0} 条,耗时{1}秒，当前更新ID区间{2}——{3}", updatecount, (watch.ElapsedMilliseconds / 1000.0).ToString("0.00"), dt.Rows[0]["cdrid"], dt.Rows[dt.Rows.Count - 1]["cdrid"]), write);
                    dt = null;
                    EncryptClueDealerRel(ref updatecount);
                }
                else
                {
                    dt = null;
                    string log = string.Format("更新遇到错误,当前更新ID区间{1}——{2}；error：", updatecount, dt.Rows[0]["cdrid"], dt.Rows[dt.Rows.Count - 1]["cdrid"], error);
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
            Console.WriteLine("ClueDealerRel " + log ?? "");
        }
    }
}
