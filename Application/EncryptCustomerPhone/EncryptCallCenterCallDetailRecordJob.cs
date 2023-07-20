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
    public class EncryptCallCenterCallDetailRecordJob
    {
        static string logpath = AppDomain.CurrentDomain.BaseDirectory + "log\\EncryptCallCenterCallDetailRecord.log";
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
            writelog("************  CallCenterCallDetailRecord 开始加密  ************ ", write);
            console("************  CallCenterCallDetailRecord 开始加密  ************ ");
            EncryptCallCenterCallDetailRecord(ref clueMobileInfoCount);
            write.Close();
            write = null;            
        }
        public static void EncryptCallCenterCallDetailRecord(ref int updatecount)
        {
            string sql = @"SELECT TOP 50000 cccdrid ,                 
                 callingnumber 
              FROM CallCenterCallDetailRecord  WHERE callingnumber IS NOT NULL AND callingnumber <> '' AND (callingnumberext IS NULL OR callingnumberext = '') ORDER BY cccdrid";
            List<DbParameter> listdb = new List<DbParameter>();
            DataTable dt = Che168DataBaseOperator.UsedCarDealerRead.ExecuteDataSet(sql, listdb.ToArray()).Tables[0];
            if (dt == null || dt.Rows.Count == 0)
            {
                string log = "************  CallCenterCallDetailRecord 已加密完成  ************";
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
                    sb.AppendLine(string.Format("UPDATE CallCenterCallDetailRecord SET callingnumberext = '{1}' WHERE cccdrid = {0};", dt.Rows[i]["cccdrid"], Che168.EnDecrypt.DESUtil.MobileEncode(dt.Rows[i]["callingnumber"].ToString())));                    
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
                    writelog(string.Format("已更新成功 {0} 条,耗时{1}秒，当前更新ID区间{2}——{3}", updatecount, (watch.ElapsedMilliseconds / 1000.0).ToString("0.00"), dt.Rows[0]["cccdrid"], dt.Rows[dt.Rows.Count - 1]["cccdrid"]), write);
                    dt = null;
                    Thread.Sleep(50 * 1000);
                    EncryptCallCenterCallDetailRecord(ref updatecount);                    
                }
                else
                {
                    dt = null;
                    string log = string.Format("更新遇到错误,当前更新ID区间{1}——{2}；error：", updatecount, dt.Rows[0]["cccdrid"], dt.Rows[dt.Rows.Count - 1]["cccdrid"], error);
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
            Console.WriteLine("CallCenterCallDetailRecord " + log ?? "");
        }
    }
}
