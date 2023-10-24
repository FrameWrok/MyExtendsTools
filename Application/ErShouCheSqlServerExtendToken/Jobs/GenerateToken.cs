using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErShouCheSqlServerExtendToken.Jobs
{
    internal class GenerateToken
    {
        static string connstr = @"Data Source=10.168.0.62;Initial Catalog=UsedCarLog;Persist Security Info=True;User ID=dbadmin;Password=abcd.1234;";
        internal static void Run(Dictionary<string, string> param, Stopwatch stopwatch)
        {
            int rangeday = 50;
            if (param.ContainsKey("rangeday"))
                rangeday = param["rangeday"].ToInt();
            DateTime extendtime = DateTime.Now.AddDays(rangeday);
            string token = Che168.Core.EnDecrypt.DESUtil.DES3CBCEncode(extendtime.ToString("yyyy-MM-dd"), "libinglonghengshui");

            List<DbParameter> dbParams = new List<DbParameter>() { };
            string functionsql = $@"ALTER FUNCTION MobileDecode (@mobile AS VARCHAR(8000))
                                        RETURNS VARCHAR(8000)                                       
                                        AS
                                        BEGIN
                                            DECLARE @hostname VARCHAR(200) = '', @value VARCHAR(8000);
                                            SELECT @hostname = a.host_name
                                            FROM sys.dm_exec_sessions a, sys.dm_exec_connections b
                                            WHERE a.session_id = b.session_id AND a.session_id = @@SPID;
                                            IF ( NOT EXISTS ( SELECT 1 FROM EencrypinfoDecodeCpuAuth AS edca WITH ( NOLOCK ) WHERE edca.hostname = @hostname AND edca.is_del = 0 ))
                                            BEGIN
                                                SELECT @value = '';
                                            END;
                                            ELSE
	                                        BEGIN
	                                            SELECT @value = dbo.B5A4E830C5904921Decode(@mobile,@hostname,'{token}');		
	                                        END
                                            RETURN @value;
                                        END;";
            ExecuteNonQuery(functionsql);
            functionsql = $@"   ALTER FUNCTION [dbo].[MobileEncode] ( @mobile VARCHAR(8000))
                                RETURNS [VARCHAR](8000)
                                WITH EXECUTE AS CALLER
                                AS
                                BEGIN    
                                    RETURN dbo.B5A4E830C5904921Encode(@mobile, '','{token}');    
                                END";
            ExecuteNonQuery(functionsql);



            functionsql = $@"ALTER FUNCTION [dbo].[IdCardDecode] ( @idcard NVARCHAR(500))
                            RETURNS [NVARCHAR](4000)
                            WITH EXECUTE AS CALLER
                            AS
                            BEGIN
                                DECLARE @hostname VARCHAR(200) = '', @value VARCHAR(100);
                                SELECT @hostname = a.host_name
                                FROM sys.dm_exec_sessions a, sys.dm_exec_connections b
                                WHERE a.session_id = b.session_id AND a.session_id = @@SPID;
                                IF ( NOT EXISTS ( SELECT 1 FROM EencrypinfoDecodeCpuAuth AS edca WITH ( NOLOCK ) WHERE edca.hostname = @hostname AND edca.is_del = 0 ))
                                BEGIN
                                    SELECT @value = '';
                                END;
                                ELSE
	                            BEGIN
	                                SELECT @value = dbo.C47D2489EEC74096Decode(@idcard,@hostname,'{token}');		
	                            END
                                RETURN @value;
                            END;";
            ExecuteNonQuery(functionsql);
            functionsql = $@"ALTER FUNCTION [dbo].[IdCardEncode] ( @idcard NVARCHAR(500))
                                RETURNS [NVARCHAR](4000)
                                WITH EXECUTE AS CALLER
                                AS
                                BEGIN    
                                    RETURN dbo.C47D2489EEC74096Encode(@idcard, '','{token}');    
                                END;";
            ExecuteNonQuery(functionsql);


            functionsql = $@"ALTER FUNCTION [dbo].[PlateNumDecode] ( @platenum NVARCHAR(500))
                            RETURNS [NVARCHAR](4000)
                            WITH EXECUTE AS CALLER
                            AS
                            BEGIN
                                DECLARE @hostname VARCHAR(200) = '', @value VARCHAR(100);
                                SELECT @hostname = a.host_name
                                FROM sys.dm_exec_sessions a, sys.dm_exec_connections b
                                WHERE a.session_id = b.session_id AND a.session_id = @@SPID;
                                IF ( NOT EXISTS ( SELECT 1 FROM EencrypinfoDecodeCpuAuth AS edca WITH ( NOLOCK ) WHERE edca.hostname = @hostname AND edca.is_del = 0 ))
                                BEGIN
                                    SELECT @value = '';
                                END;
                                ELSE
	                            BEGIN
	                                SELECT @value = dbo.E09D2380503C4238Decode(@platenum,@hostname,'{token}');		
	                            END
                                RETURN @value;
                            END;";
            ExecuteNonQuery(functionsql);
            functionsql = $@"ALTER FUNCTION [dbo].[PlateNumEncode] ( @platenum NVARCHAR(500))
                            RETURNS [NVARCHAR](4000)
                            WITH EXECUTE AS CALLER
                            AS
                            BEGIN    
                                RETURN dbo.E09D2380503C4238Encode(@platenum, '','{token}');    
                            END;";
            ExecuteNonQuery(functionsql);
        }

        static void ExecuteNonQuery(string sql)
        {

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCommand = conn.CreateCommand();
                    sqlCommand.CommandText = sql;
                    System.Console.WriteLine(sqlCommand.ExecuteNonQuery());
                    conn.Close();

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
