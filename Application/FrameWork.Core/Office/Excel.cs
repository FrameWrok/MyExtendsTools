/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;
using Aspose.Cells;

namespace FrameWork.Core.Office
{
    /// <summary>
    /// 由DataTable或DataSet生成Excel的类 
    /// </summary>
    public partial class Excel
    {
        private DataTable dataTableSources;

        private DataSet dataSetSources;

        private Workbook exportWorkbook;

        public Workbook ExportWorkbook
        {
            get
            {
                return this.exportWorkbook;
            }
        }

        public Excel(DataTable dataSources)
        {
            this.license = new License();
            this.license.SetLicense("Aspose.Cells.lic");
            this.dataTableSources = dataSources;
        }

        public Excel(DataSet dataSources)
        {
            this.license = new License();
            this.license.SetLicense("Aspose.Cells.lic");
            this.dataSetSources = dataSources;
        }

        private License license;

        #region 导出或保存方法

        /// <summary>
        /// 将数据源导出到Excel中并输出到客户端下载
        /// 默认保存为Excel2003
        /// </summary>        
        /// <param name="fileName">文件的名称</param>
        /// <param name="passWord">工作薄的保护密码</param>
        /// <param name="errorMessage">导出过程中的错误信息</param>
        /// <returns>导出结果，是否成功</returns>
        public bool ExportToExcel(string fileName, string passWord, ref string errorMessage)
        {
            return this.ExportToExcel(fileName, passWord, ref errorMessage, FileFormatType.Excel2003);
        }

        /// <summary>
        /// 将数据源导出到Excel中并输出到客户端下载
        /// </summary>
        /// <param name="fileName">文件的名称</param>
        /// <param name="passWord">工作薄的保护密码</param>
        /// <param name="errorMessage">导出过程中的错误信息</param>
        /// <param name="fileFormatType">导出的文件的类型(Excel2007，Excel2003 ...)</param>
        /// <returns>导出结果，是否成功</returns>
        public bool ExportToExcel(string fileName, string passWord, ref string errorMessage, FrameWork.Core.Office.FileFormatType fileFormatType)
        {
            return this.ExportToExcel(fileName, passWord, ref errorMessage, fileFormatType, Encoding.Default);
        }

        /// <summary>
        /// 将数据源导出到Excel中并输出到客户端下载
        /// 默认保存为Excel2003
        /// </summary>
        /// <param name="fileName">文件的名称</param>
        /// <param name="passWord">工作薄的保护密码</param>
        /// <param name="errorMessage">导出过程中的错误信息</param>
        /// <param name="encoding">导出文件所用的文本编码</param>
        /// <returns>导出结果，是否成功</returns>
        public bool ExportToExcel(string fileName, string passWord, ref string errorMessage, Encoding encoding)
        {
            return this.ExportToExcel(fileName, passWord, ref errorMessage, FileFormatType.Default, encoding);
        }

        /// <summary>
        /// 将数据源导出到Excel中并输出到客户端下载
        /// </summary>
        /// <param name="fileName">文件的名称</param>
        /// <param name="passWord">工作薄的保护密码</param>
        /// <param name="errorMessage">导出过程中的错误信息</param>
        /// <param name="fileFormatType">导出的文件的类型(Excel2007，Excel2003 ...)</param>
        /// <param name="encoding">导出文件所用的文本编码</param>
        /// <returns>导出结果，是否成功</returns>
        public bool ExportToExcel(string fileName, string passWord, ref string errorMessage, FrameWork.Core.Office.FileFormatType fileFormatType, Encoding encoding)
        {
            return this.SaveAndExport(fileName, passWord, ref errorMessage, fileFormatType, encoding, SaveType.OpenInExcel);
        }

        /// <summary>
        /// 将数据源导出到Excel中,并保存到本地路径
        /// 默认保存为Excel2003
        /// </summary>
        /// <param name="filePath">Excel文件导出的路径，需包含文件名称</param>
        /// <param name="passWord">工作薄的保护密码</param>
        /// <param name="errorMessage">生成过程中的异常信息</param>        
        /// <returns>生成的结果</returns>
        public bool Save(string filePath, string passWord, ref string errorMessage)
        {
            return this.Save(filePath, passWord, ref errorMessage, Encoding.UTF8, FileFormatType.Excel2003);
        }

        /// <summary>
        /// 将数据源导出到Excel中,并保存到本地路径
        /// 默认保存为Excel2003
        /// </summary>
        /// <param name="filePath">Excel文件导出的路径，需包含文件名称</param>
        /// <param name="passWord">工作薄的保护密码</param>
        /// <param name="errorMessage">生成过程中的异常信息</param>        
        /// <param name="encoding">导出文件所用的文本编码</param>
        /// <returns>生成的结果</returns>
        public bool Save(string filePath, string passWord, ref string errorMessage, Encoding encoding)
        {
            return Save(filePath, passWord, ref errorMessage, encoding, FileFormatType.Default);
        }

        /// <summary>
        /// 将数据源导出到Excel中,并保存到本地路径
        /// </summary>
        /// <param name="filePath">Excel文件导出的路径，需包含文件名称</param>
        /// <param name="passWord">工作薄的保护密码</param>
        /// <param name="errorMessage">生成过程中的异常信息</param>
        /// <param name="fileFormatType">保存的文件的类型(Excel2007，Excel2003 ...)</param>
        /// <returns>生成的结果</returns>
        public bool Save(string filePath, string passWord, ref string errorMessage, FrameWork.Core.Office.FileFormatType fileFormatType)
        {
            return this.Save(filePath, passWord, ref errorMessage, Encoding.UTF8, fileFormatType);
        }

        /// <summary>
        /// 将数据源导出到Excel中,并保存到本地路径
        /// </summary>
        /// <param name="filePath">Excel文件导出的路径，需包含文件名称</param>
        /// <param name="passWord">工作薄的保护密码</param>
        /// <param name="errorMessage">生成过程中的异常信息</param>
        /// <param name="encoding">导出文件所用的文本编码</param>
        /// <param name="fileFormatType">保存的文件的类型(Excel2007，Excel2003 ...)</param>        
        /// <returns>生成的结果</returns>
        public bool Save(string filePath, string passWord, ref string errorMessage, Encoding encoding, FrameWork.Core.Office.FileFormatType fileFormatType)
        {
            return this.SaveAndExport(filePath, passWord, ref errorMessage, fileFormatType, encoding, SaveType.Default);
        }

        #endregion
    }

    /// <summary>
    /// 由DataTable或DataSet生成Excel的类 
    /// </summary>
    public partial class Excel
    {
        #region

        /// <summary>
        /// 由DataTable 数据源 生成Excel
        /// </summary>
        /// <param name="sourceDataTable">DataTable数据源</param>        
        /// <param name="errorMessage">生成过程中的异常信息</param>        
        /// <returns>生成Excel是否成功</returns>
        private bool GenerateExcel(DataTable sourceDataTable, ref string errorMessage)
        {
            if (errorMessage == null)
                errorMessage = string.Empty;

            this.exportWorkbook = new Workbook();
            StringBuilder sberrorMessage = new StringBuilder(errorMessage);
            bool resoult = this.AddBody(sourceDataTable, 0, sourceDataTable.TableName, ref sberrorMessage);
            errorMessage = sberrorMessage.ToString();

            return true;
        }

        /// <summary>
        /// 由DataTable 数据源 生成Excel
        /// </summary>
        /// <param name="sourceDataSet">DataSet数据源</param>        
        /// <param name="errorMessage">生成过程中的异常信息</param>        
        /// <returns>生成Excel是否成功</returns>
        private bool GenerateExcel(DataSet sourceDataSet, ref string errorMessage)
        {
            if (errorMessage == null)
                errorMessage = string.Empty;

            this.exportWorkbook = new Workbook();
            StringBuilder sberrorMessage = new StringBuilder(errorMessage);
            bool result = true;
            int tableIndex = 0;

            foreach (DataTable sourceTable in sourceDataSet.Tables)
            {
                result = this.AddBody(sourceDataSet.Tables[tableIndex], tableIndex, sourceDataSet.Tables[tableIndex].TableName, ref sberrorMessage);
                if (!result)
                    break;
                tableIndex++;
            }

            errorMessage = sberrorMessage.ToString();

            return result;
        }

        /// <summary>
        /// 保存或导出的方法
        /// </summary>
        /// <param name="fileName">文件的名称</param>
        /// <param name="passWord">工作薄的保护密码</param>
        /// <param name="errorMessage">导出过程中的错误信息</param>
        /// <param name="fileFormatType">导出的文件的类型(Excel2007，Excel2003 ...)</param>
        /// <param name="encoding">导出文件所用的文本编码</param>
        /// <param name="saveType">文件保存类型</param>
        /// <returns>保存或导出是否成功</returns>
        private bool SaveAndExport(string fileName, string passWord, ref string errorMessage, FrameWork.Core.Office.FileFormatType fileFormatType, Encoding encoding, SaveType saveType)
        {
            bool result = false;
            if (this.dataSetSources == null && this.dataTableSources == null)
            {
                throw new ArgumentNullException("DataSources");
            }

            if (this.ExportWorkbook == null)
            {
                if (this.dataSetSources != null)
                    result = this.GenerateExcel(this.dataSetSources, ref errorMessage);
                else
                    result = this.GenerateExcel(this.dataTableSources, ref errorMessage);
            }

            if (!passWord.IsNullOrEmptyOrBlank())
                this.ExportWorkbook.Password = passWord;

            if (result)
                this.Save(this.GetAsposeExcelFileFormatType(fileFormatType), fileName, Encoding.Unicode, saveType);

            return result;
        }

        /// <summary>
        /// 保存的方法
        /// </summary>        
        /// <param name="fileFormatType">文件的保存类型</param>
        /// <param name="fileName">文件的名称或路径</param>
        /// <param name="encoding">文件所应用的编码</param>
        /// <param name="saveType">文件保存类型</param>
        private void Save(Aspose.Cells.FileFormatType fileFormatType, string fileName, Encoding encoding, SaveType saveType)
        {
            switch (saveType)
            {
                case SaveType.OpenInBrowser:
                case SaveType.OpenInExcel:
                    {
                        this.ExportWorkbook.Save(fileName, fileFormatType, saveType, HttpContext.Current.Response, encoding);
                    }

                    break;
                case SaveType.Default:
                    {
                        this.ExportWorkbook.Save(fileName);
                    }

                    break;
            }
        }

        /// <summary>
        /// 将 DataTable 中的内容添加到 Excel的Sheet页中
        /// </summary>        
        /// <param name="datatable">数据源</param>
        /// <param name="sheetIndex">添加的 Sheet 页索引</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <param name="error">错误信息</param>
        /// <returns>添加结果</returns>
        private bool AddBody(DataTable datatable, int sheetIndex, string sheetName, ref StringBuilder error)
        {
            error.Append(string.Empty);
            try
            {
                if (datatable == null)
                {
                    error.AppendLine((new ArgumentNullException("dataTable")).Message);

                    return false;
                }

                Aspose.Cells.Worksheet sheet = this.ExportWorkbook.Worksheets[0];

                if (sheetIndex != 0)
                {
                    this.ExportWorkbook.Worksheets.Add();
                    sheet = this.ExportWorkbook.Worksheets[sheetIndex];
                }

                if (!sheetName.IsNullOrEmptyOrBlank())
                    sheet.Name = sheetName;

                Aspose.Cells.Cells cells = sheet.Cells;
                ////添加表头信息
                this.AddHeader(sheet, datatable);

                int nrow = 0;
                foreach (DataRow row in datatable.Rows)
                {
                    nrow++;
                    try
                    {
                        for (int i = 0; i < datatable.Columns.Count; i++)
                        {
                            if (row[i].GetType().ToString() == "System.Drawing.Bitmap")
                            {
                                //------插入图片数据-------
                                System.Drawing.Image image = (System.Drawing.Image)row[i];
                                MemoryStream mstream = new MemoryStream();
                                image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                sheet.Pictures.Add(nrow, i, mstream);
                            }
                            else
                            {
                                cells[nrow, i].PutValue(row[i].ToString(), false);
                            }
                        }
                    }
                    catch (System.Exception e)
                    {
                        error.AppendLine(" DataTableToExcel: " + e.Message);
                        break;
                    }
                }

                return true;
            }
            catch (System.Exception e)
            {
                error.AppendLine(" DataTableToExcel: " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// 增加 Excel 的头部信息
        /// </summary>
        /// <param name="sheet">增加头部信息的 sheet页</param>
        /// <param name="sourceTable">导出到excel的数据源Table</param>
        /// <returns>增加头信息结果 </returns>
        private bool AddHeader(Worksheet sheet, DataTable sourceTable)
        {
            int columnIndex = 0;
            Cells cells = sheet.Cells;
            foreach (DataColumn column in sourceTable.Columns)
            {
                cells[0, columnIndex].PutValue(column.ColumnName);
                columnIndex++;
            }

            return true;
        }

        private Aspose.Cells.FileFormatType GetAsposeExcelFileFormatType(FrameWork.Core.Office.FileFormatType fileFormatType)
        {
            switch (fileFormatType)
            {
                case FileFormatType.AsposePdf:
                    return Aspose.Cells.FileFormatType.AsposePdf;
                case FileFormatType.CSV:
                    return Aspose.Cells.FileFormatType.CSV;
                case FileFormatType.Excel2000:
                    return Aspose.Cells.FileFormatType.Excel2000;
                case FileFormatType.Excel2007Xlsm:
                    return Aspose.Cells.FileFormatType.Excel2007Xlsm;
                case FileFormatType.Excel2007Xlsx:
                    return Aspose.Cells.FileFormatType.Excel2007Xlsx;
                case FileFormatType.Excel2007Xltm:
                    return Aspose.Cells.FileFormatType.Excel2007Xltm;
                case FileFormatType.Excel2007Xltx:
                    return Aspose.Cells.FileFormatType.Excel2007Xltx;
                case FileFormatType.Excel2003:
                    return Aspose.Cells.FileFormatType.Excel2003;
                case FileFormatType.Excel97:
                    return Aspose.Cells.FileFormatType.Excel97;
                case FileFormatType.ExcelXP:
                    return Aspose.Cells.FileFormatType.ExcelXP;
                case FileFormatType.Html:
                    return Aspose.Cells.FileFormatType.Html;
                case FileFormatType.Pdf:
                    return Aspose.Cells.FileFormatType.Pdf;
                case FileFormatType.SpreadsheetML:
                    return Aspose.Cells.FileFormatType.SpreadsheetML;
                case FileFormatType.TabDelimited:
                    return Aspose.Cells.FileFormatType.TabDelimited;
                default:
                    return Aspose.Cells.FileFormatType.Default;
            }

            ////switch (fileFormatType)
            ////{
            ////    case FileFormatType.CSV:
            ////        return Aspose.Cells.FileFormatType.CSV;
            ////    case FileFormatType.Dif:
            ////        return Aspose.Cells.FileFormatType.Dif;
            ////    case FileFormatType.Docx:
            ////        return Aspose.Cells.FileFormatType.Docx;
            ////    case FileFormatType.Excel2:
            ////        return Aspose.Cells.FileFormatType.Excel2;
            ////    case FileFormatType.Excel2003XML:
            ////        return Aspose.Cells.FileFormatType.Excel2003XML;
            ////    case FileFormatType.Excel3:
            ////        return Aspose.Cells.FileFormatType.Excel3;
            ////    case FileFormatType.Excel4:
            ////        return Aspose.Cells.FileFormatType.Excel4;
            ////    case FileFormatType.Excel95:
            ////        return Aspose.Cells.FileFormatType.Excel95;
            ////    case FileFormatType.Excel97To2003:
            ////        return Aspose.Cells.FileFormatType.Excel97To2003;
            ////    case FileFormatType.Html:
            ////        return Aspose.Cells.FileFormatType.Html;
            ////    case FileFormatType.MHtml:
            ////        return Aspose.Cells.FileFormatType.MHtml;
            ////    case FileFormatType.ODS:
            ////        return Aspose.Cells.FileFormatType.ODS;
            ////    case FileFormatType.Pdf:
            ////        return Aspose.Cells.FileFormatType.Pdf;
            ////    case FileFormatType.Pptx:
            ////        return Aspose.Cells.FileFormatType.Pptx;
            ////    case FileFormatType.SVG:
            ////        return Aspose.Cells.FileFormatType.SVG;
            ////    case FileFormatType.TabDelimited:
            ////        return Aspose.Cells.FileFormatType.TabDelimited;
            ////    case FileFormatType.TIFF:
            ////        return Aspose.Cells.FileFormatType.TIFF;
            ////    case FileFormatType.Unknown:
            ////        return Aspose.Cells.FileFormatType.Unknown;
            ////    case FileFormatType.Xlsb:
            ////        return Aspose.Cells.FileFormatType.Xlsb;
            ////    case FileFormatType.Xlsm:
            ////        return Aspose.Cells.FileFormatType.Xlsm;
            ////    case FileFormatType.Xlsx:
            ////        return Aspose.Cells.FileFormatType.Xlsx;
            ////    case FileFormatType.Xltm:
            ////        return Aspose.Cells.FileFormatType.Xltm;
            ////    case FileFormatType.Xltx:
            ////        return Aspose.Cells.FileFormatType.Xltx;
            ////    case FileFormatType.XPS:
            ////        return Aspose.Cells.FileFormatType.XPS;
            ////    default:
            ////        return Aspose.Cells.FileFormatType.Excel97To2003;
            ////}            
        }

        #endregion
    }
}
