using PdmModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CHMUtil
{
    public class ChmHtmlHelper
    {
        public static void CreateDirHtml(string tabDirName, IList<TableInfo> lstTabs, string path)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            stringBuilder.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            stringBuilder.AppendLine("<head>");
            stringBuilder.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\" />");
            stringBuilder.AppendLine("    <title>{0}</title>".FormatString(new object[]
            {
                tabDirName
            }));
            stringBuilder.AppendLine("    <style type=\"text/css\">");
            stringBuilder.AppendLine("        *");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            font-family:'微软雅黑';");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("        body");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            font-size: 9pt;");
            stringBuilder.AppendLine("            font-family:'lucida console';");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("        .styledb");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            font-size: 14px;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("        .styletab");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            font-size: 14px;");
            stringBuilder.AppendLine("            padding-top: 15px;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("        a");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            color: #015FB6;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("        a:link, a:visited, a:active");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            color: #015FB6;");
            stringBuilder.AppendLine("            text-decoration: none;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("        a:hover");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            color: #E33E06;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("    </style>");
            stringBuilder.AppendLine("</head>");
            stringBuilder.AppendLine("<body>");
            stringBuilder.AppendLine("    <div style=\"text-align: center\">");
            stringBuilder.AppendLine("        <div>");
            stringBuilder.AppendLine("            <table border=\"0\" cellpadding=\"5\" cellspacing=\"0\" width=\"90%\">");
            stringBuilder.AppendLine("                <tr>");
            stringBuilder.AppendLine("                    <td bgcolor=\"#FBFBFB\">");
            stringBuilder.AppendLine("                        <table cellspacing=\"0\" cellpadding=\"5\" border=\"1\" width=\"100%\" bordercolorlight=\"#D7D7E5\" bordercolordark=\"#D3D8E0\">");
            stringBuilder.AppendLine("                        <caption>");
            stringBuilder.AppendLine("        <div class=\"styletab\">{0}</div>".FormatString(new object[]
            {
                "<b>" + tabDirName + "</b>"
            }));
            stringBuilder.AppendLine("                        </caption>");
            stringBuilder.AppendLine("                          <tr bgcolor=\"#F0F0F0\"><td>序号</td><td>表名</td><td>表说明</td></tr>");
            int num = 1;
            foreach (TableInfo current in lstTabs)
            {
                stringBuilder.AppendLine("            <tr>");
                stringBuilder.AppendLine("            <td>{0}</td>".FormatString(new object[]
                {
                    num
                }));
                stringBuilder.AppendLine("            <td>{0}</td>".FormatString(new object[]
                {
                    string.Concat(new string[]
                    {
                        "<a href=\"表结构\\",
                        current.Code,
                        " ",
                        current.Name.Replace("/", "▪").Replace("\\", "▪"),
                        ".html\">",
                        current.Code,
                        "</a>"
                    })
                }));
                stringBuilder.AppendLine("            <td>{0}</td>".FormatString(new object[]
                {
                    current.Name
                }));
                stringBuilder.AppendLine("            </tr>");
                num++;
            }
            stringBuilder.AppendLine("                        </table>");
            stringBuilder.AppendLine("                    </td>");
            stringBuilder.AppendLine("                </tr>");
            stringBuilder.AppendLine("            </table>");
            stringBuilder.AppendLine("        </div>");
            stringBuilder.AppendLine("    </div>");
            stringBuilder.AppendLine("</body>");
            stringBuilder.AppendLine("</html>");
            File.WriteAllText(path, stringBuilder.ToString(), Encoding.GetEncoding("gb2312"));
        }

        public static void CreateHtml(IList<TableInfo> lstTabs, string tabsdir)
        {
            foreach (TableInfo current in lstTabs)
            {
                string path = string.Concat(new string[]
                {
                    tabsdir,
                    "\\",
                    current.Code,
                    " ",
                    current.Name.Replace("/", "▪").Replace("\\", "▪"),
                    ".html"
                });
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
                stringBuilder.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
                stringBuilder.AppendLine("<head>");
                stringBuilder.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\" />");
                stringBuilder.AppendLine("    <title>{0}</title>".FormatString(new object[]
                {
                    current.Code
                }));
                stringBuilder.AppendLine("    <style type=\"text/css\">");
                stringBuilder.AppendLine("        *");
                stringBuilder.AppendLine("        {");
                stringBuilder.AppendLine("            font-family:'微软雅黑';");
                stringBuilder.AppendLine("        }");
                stringBuilder.AppendLine("        body");
                stringBuilder.AppendLine("        {");
                stringBuilder.AppendLine("            font-size: 9pt;");
                stringBuilder.AppendLine("            font-family:'lucida console';");
                stringBuilder.AppendLine("        }");
                stringBuilder.AppendLine("        .styledb");
                stringBuilder.AppendLine("        {");
                stringBuilder.AppendLine("            font-size: 14px;");
                stringBuilder.AppendLine("        }");
                stringBuilder.AppendLine("        .styletab");
                stringBuilder.AppendLine("        {");
                stringBuilder.AppendLine("            font-size: 14px;");
                stringBuilder.AppendLine("            padding-top: 15px;");
                stringBuilder.AppendLine("        }");
                stringBuilder.AppendLine("        a");
                stringBuilder.AppendLine("        {");
                stringBuilder.AppendLine("            color: #015FB6;");
                stringBuilder.AppendLine("        }");
                stringBuilder.AppendLine("        a:link, a:visited, a:active");
                stringBuilder.AppendLine("        {");
                stringBuilder.AppendLine("            color: #015FB6;");
                stringBuilder.AppendLine("            text-decoration: none;");
                stringBuilder.AppendLine("        }");
                stringBuilder.AppendLine("        a:hover");
                stringBuilder.AppendLine("        {");
                stringBuilder.AppendLine("            color: #E33E06;");
                stringBuilder.AppendLine("        }");
                stringBuilder.AppendLine("    </style>");
                stringBuilder.AppendLine("</head>");
                stringBuilder.AppendLine("<body>");
                stringBuilder.AppendLine("    <div style=\"text-align: center\">");
                stringBuilder.AppendLine("        <div>");
                stringBuilder.AppendLine("            <table border=\"0\" cellpadding=\"5\" cellspacing=\"0\" width=\"90%\">");
                stringBuilder.AppendLine("                <tr>");
                stringBuilder.AppendLine("                    <td bgcolor=\"#FBFBFB\">");
                stringBuilder.AppendLine("                        <table cellspacing=\"0\" cellpadding=\"5\" border=\"1\" width=\"100%\" bordercolorlight=\"#D7D7E5\" bordercolordark=\"#D3D8E0\">");
                stringBuilder.AppendLine("                        <caption>");
                stringBuilder.AppendLine("        <div class=\"styletab\">{0}{1}</div>".FormatString(new object[]
                {
                    current.Code,
                    "<a href='../数据库表目录.html' style='float: right; margin-top: 6px;'>返回目录</a>"
                }));
                stringBuilder.AppendLine("                        </caption>");
                //				stringBuilder.AppendLine("<tr bgcolor=\"#F0F0F0\"><td>序号</td><td>列名</td><td>数据类型</td><td>长度</td><td>主键</td><td>自增</td><td>允许空</td><td>列说明</td></tr>");
                stringBuilder.AppendLine("<tr bgcolor=\"#F0F0F0\"><td>序号</td><td>列名</td><td>数据类型</td><td>主键</td><td>自增</td><td>允许空</td><td>列说明</td></tr>");
                int num = 1;
                foreach (ColumnInfo current2 in current.Columns)
                {
                    stringBuilder.AppendLine(string.Concat(new object[]
                    {
                        "<tr bgcolor=\"#F0F0F0\"><td>",
                        num,
                        "</td><td>",
                        current2.Code,
                        "</td><td>",
                        current2.DataType,
						//"</td><td>",
						//string.IsNullOrEmpty(current2.Length) ? "&nbsp;" : current2.Length,
						"</td><td>",
                        current2.IsPrimaryKey ? "√" : "&nbsp;",
                        "</td><td>",
                        current2.Identity ? "√" : "&nbsp;",
                        "</td><td>",
                        current2.Mandatory ? "√" : "&nbsp;",
                        "</td><td style=\"text-align:left;\">",
						//current2.Name,                        
                        current2.Comment,
                        "</td></tr>"
                    }));
                    num++;
                }
                stringBuilder.AppendLine("                        </table>");
                stringBuilder.AppendLine("                    </td>");
                stringBuilder.AppendLine("                </tr>");
                stringBuilder.AppendLine("            </table>");
                stringBuilder.AppendLine("        </div>");
                stringBuilder.AppendLine("    </div>");
                stringBuilder.AppendLine("</body>");
                stringBuilder.AppendLine("</html>");
                File.WriteAllText(path, stringBuilder.ToString(), Encoding.GetEncoding("gb2312"));
                num++;
            }
        }
    }
}
