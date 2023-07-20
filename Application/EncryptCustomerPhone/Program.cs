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
using System.Threading;

namespace EncryptCustomerPhone
{
    class Program
    {
        static string logpath = AppDomain.CurrentDomain.BaseDirectory + "log\\encrypt.log";
        static StreamWriter write = null;
        static void Main(string[] args)
        {
            string error;
            Console.WriteLine(test(out error).ToString());
            Console.WriteLine(error ?? "");
            Console.ReadKey();
            return;



            if (File.Exists(logpath))
                write = File.AppendText(logpath);
            else
                write = File.CreateText(logpath);
            write.AutoFlush = true;
            try
            {

                writelog("************  加密任务开始  ************", write);

                Thread encryptClueDealerRelJobThread = new Thread(new ParameterizedThreadStart(EncryptClueDealerRelJob.run));
                encryptClueDealerRelJobThread.IsBackground = true;
                encryptClueDealerRelJobThread.SetApartmentState(ApartmentState.STA);
                encryptClueDealerRelJobThread.Start();


                Thread encryptClueCarMatchJobThread = new Thread(new ParameterizedThreadStart(EncryptClueCarMatchJob.run));
                encryptClueCarMatchJobThread.IsBackground = true;
                encryptClueCarMatchJobThread.SetApartmentState(ApartmentState.STA);
                encryptClueCarMatchJobThread.Start();


                Thread encryptClueMobileFeatureJobThread = new Thread(new ParameterizedThreadStart(EncryptClueMobileFeatureJob.run));
                encryptClueMobileFeatureJobThread.IsBackground = true;
                encryptClueMobileFeatureJobThread.SetApartmentState(ApartmentState.STA);
                encryptClueMobileFeatureJobThread.Start();

                Thread encryptSalesLeadsBlackListJobThread = new Thread(new ParameterizedThreadStart(EncryptSalesLeadsBlackListJob.run));
                encryptSalesLeadsBlackListJobThread.IsBackground = true;
                encryptSalesLeadsBlackListJobThread.SetApartmentState(ApartmentState.STA);
                encryptSalesLeadsBlackListJobThread.Start();



                /**************************订阅相关表延时更新***************************/

                Thread encryptClueMobileInfoJobThread = new Thread(new ParameterizedThreadStart(EncryptClueMobileInfoJob.run));
                encryptClueMobileInfoJobThread.IsBackground = true;
                encryptClueMobileInfoJobThread.SetApartmentState(ApartmentState.STA);
                encryptClueMobileInfoJobThread.Start();

                Thread encryptCallCenterCallDetailRecordJobThread = new Thread(new ParameterizedThreadStart(EncryptCallCenterCallDetailRecordJob.run));
                encryptCallCenterCallDetailRecordJobThread.IsBackground = true;
                encryptCallCenterCallDetailRecordJobThread.SetApartmentState(ApartmentState.STA);
                encryptCallCenterCallDetailRecordJobThread.Start();

                Thread encryptCallCenterCallBackDetailRecordJobThread = new Thread(new ParameterizedThreadStart(EncryptCallCenterCallBackDetailRecordJob.run));
                encryptCallCenterCallBackDetailRecordJobThread.IsBackground = true;
                encryptCallCenterCallBackDetailRecordJobThread.SetApartmentState(ApartmentState.STA);
                encryptCallCenterCallBackDetailRecordJobThread.Start();


                //writelog("*********************************************  加密任务完成  **************************************** ", write);

            }
            catch (Exception ex)
            {
                writelog(string.Format("遇到错误：", ex.Message), write);
                Console.WriteLine(ex.Message);
            }

            write.Close();
            Console.ReadKey();
        }
        public static void writelog(string log, StreamWriter logwriter)
        {
            Console.WriteLine(log ?? "");
            logwriter.WriteLine("{0} : {1}", DateTime.Now, log ?? "");
        }


        public static bool test(out string error)
        {
            string sql = @"
DELETE ClueMobileInfo WHERE clueid IN (3181304,
3175497,3127111,3159736,3171227,3181019,3172583,3136267,3168153,3157339,3162717,3162779,3135419,3142481,3134845,3162563,3162724,3175501,3148898,3162722,3131574,3162560,3126811,
3159842,3180195,3134881,3127109,3162728,3142687,3140256,3171399,3167419,3162562,3174384,3175498,3127114,3131972,3162725,3151944,3179643,3131973,3171352,3136642)



UPDATE cdr SET clueid=cmi.clueid FROM ClueDealerRel AS cdr WITH(NOLOCK) INNER JOIN ClueMobileInfo cmi WITH(NOLOCK) ON cdr.phoneext = cmi.phoneext
WHERE cmi.clueid!=cdr.clueid AND cdr.clueid IN (3181304,
3175497,3127111,3159736,3171227,3181019,3172583,3136267,3168153,3157339,3162717,3162779,3135419,3142481,3134845,3162563,3162724,3175501,3148898,3162722,3131574,3162560,3126811,
3159842,3180195,3134881,3127109,3162728,3142687,3140256,3171399,3167419,3162562,3174384,3175498,3127114,3131972,3162725,3151944,3179643,3131973,3171352,3136642)";

            return Che168DataBaseOperator.UsedCarDealerWrite.ExecuteNonQuery(sql, CommandType.Text, out error, null);
        }
    }

}
