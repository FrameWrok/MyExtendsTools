using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SmartJSFrameWork.WebSite
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TestSmartFrameWork.Test t = new TestSmartFrameWork.Test();
            bool result = t.test("");
            result = "".IsNullOrEmptyOrBlank();

            DataTable db = new DataTable("db");
            db.Columns.Add("dba");
            db.Columns.Add("b");
            db.Columns.Add("c");

            DataTable db1 = new DataTable("db1");
            db1.Columns.Add("dba");
            db1.Columns.Add("d");
            db1.Columns.Add("e");

            DataSet ds = new DataSet();
            ds.Tables.Add(db);
            ds.Tables.Add(db1);
            ds.Relations.Add("db1_dba_db", ds.Tables["db"].Columns["dba"], ds.Tables["db1"].Columns["dba"]);

            DataRow dbrow = db.NewRow();
            dbrow["dba"] = 1;
            dbrow["b"] = "a";
            dbrow["c"] = "a";
            db.Rows.Add(dbrow);

            dbrow = db.NewRow();
            dbrow["dba"] = 2;
            dbrow["b"] = "b";
            dbrow["c"] = "b";
            db.Rows.Add(dbrow);

            dbrow = db.NewRow();
            dbrow["dba"] = 3;
            dbrow["b"] = "c";
            dbrow["c"] = "c";
            db.Rows.Add(dbrow);

            DataRow db1row = db1.NewRow();
            db1row["dba"] = 3;
            db1row["d"] = "q";
            db1row["e"] = "q";
            db1.Rows.Add(db1row);

            db1row = db1.NewRow();
            db1row["dba"] = 2;
            db1row["d"] = "q1";
            db1row["e"] = "q1";
            db1.Rows.Add(db1row);

            db1row = db1.NewRow();
            db1row["dba"] = 1;
            db1row["d"] = "q2";
            db1row["e"] = "q2";
            db1.Rows.Add(db1row);
            ////Excel excel = new Excel(db);
            string err = string.Empty;
            ////excel.ExportToExcel(@"ddd.xls", "", ref err, FileFormatType.Excel2003);
        }
    }
}
