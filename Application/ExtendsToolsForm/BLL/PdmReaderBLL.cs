using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExtendsToolsForm.Models.PdmToChm;
using System.Xml;

namespace ExtendsToolsForm.BLL
{
    /// <summary>
    /// pdm转chm文件
    /// </summary>
    public static class PdmReaderBLL
    {
        private static DateTime _baseDateTime = new DateTime(1970, 1, 1, 8, 0, 0);
        public static PdmModel PdmReader(string pdmFile)
        {
            PdmModel result;
            if (string.IsNullOrEmpty(pdmFile))
            {
                result = null;
            }
            else
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(pdmFile);
                XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
                xmlNamespaceManager.AddNamespace("a", "attribute");
                xmlNamespaceManager.AddNamespace("c", "collection");
                xmlNamespaceManager.AddNamespace("o", "object");
                PdmModel pdmModels = new PdmModel();
                XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//c:Tables", xmlNamespaceManager);
                if (xmlNodeList != null)
                {
                    foreach (XmlNode current in from XmlNode xmlTables in xmlNodeList
                                                from xnTable in xmlTables.ChildNodes.Cast<XmlNode>()
                                                where xnTable.Name != "o:Shortcut"
                                                select xnTable)
                    {
                        pdmModels.Tables.Add(GetTable(current));
                    }
                }
                result = pdmModels;
            }
            return result;
        }

        private static PdmTableInfoModel GetTable(XmlNode xnTable)
        {
            PdmTableInfoModel PdmTableInfoModel = new PdmTableInfoModel();
            XmlElement xmlElement = (XmlElement)xnTable;
            PdmTableInfoModel.TableId = xmlElement.GetAttribute("Id");
            XmlNodeList childNodes = xmlElement.ChildNodes;
            foreach (XmlNode xmlNode in childNodes)
            {
                string name = xmlNode.Name;
                switch (name)
                {
                    case "a:ObjectID":
                        PdmTableInfoModel.ObjectID = xmlNode.InnerText;
                        break;
                    case "a:Name":
                        PdmTableInfoModel.Name = xmlNode.InnerText;
                        break;
                    case "a:Code":
                        PdmTableInfoModel.Code = xmlNode.InnerText;
                        break;
                    case "a:CreationDate":
                        PdmTableInfoModel.CreationDate = String2DateTime(xmlNode.InnerText);
                        break;
                    case "a:Creator":
                        PdmTableInfoModel.Creator = xmlNode.InnerText;
                        break;
                    case "a:ModificationDate":
                        PdmTableInfoModel.ModificationDate = String2DateTime(xmlNode.InnerText);
                        break;
                    case "a:Modifier":
                        PdmTableInfoModel.Modifier = xmlNode.InnerText;
                        break;
                    case "a:Comment":
                        PdmTableInfoModel.Comment = xmlNode.InnerText;
                        break;
                    case "a:PhysicalOptions":
                        PdmTableInfoModel.PhysicalOptions = xmlNode.InnerText;
                        break;
                    case "c:Columns":
                         InitColumns(xmlNode, PdmTableInfoModel);
                        break;
                    case "c:Keys":
                        InitKeys(xmlNode, PdmTableInfoModel);
                        break;
                    case "c:PrimaryKey":
                        InitPrimaryKey(xmlNode, PdmTableInfoModel);
                        break;
                    case "a:Description":
                        PdmTableInfoModel.Description = xmlNode.InnerText;
                        break;
                }
            }
            return PdmTableInfoModel;
        }

        private static DateTime String2DateTime(string dateString)
        {
            long num = long.Parse(dateString);
            return _baseDateTime.AddSeconds((double)num);
        }

        private static void InitColumns(XmlNode xnColumns, PdmTableInfoModel pTable)
        {
            foreach (XmlNode xnColumn in xnColumns)
            {
                pTable.AddColumn(GetColumn(xnColumn, pTable));
            }
        }

        private static void InitKeys(XmlNode xnKeys, PdmTableInfoModel pTable)
        {
            foreach (XmlNode xnKey in xnKeys)
            {
                pTable.AddKey(GetKey(xnKey, pTable));
            }
        }

        private static void InitPrimaryKey(XmlNode xnPrimaryKey, PdmTableInfoModel pTable)
        {
            pTable.PrimaryKeyRefCode = GetPrimaryKey(xnPrimaryKey);
        }

        private static bool ConvertToBooleanPg(object obj)
        {
            bool result;
            if (obj != null)
            {
                string text = obj.ToString();
                text = text.ToLower();
                if (text.Equals("y") || text.Equals("1") || text.Equals("true"))
                {
                    result = true;
                    return result;
                }
            }
            result = false;
            return result;
        }

        private static PdmColumnInfoModel GetColumn(XmlNode xnColumn, PdmTableInfoModel ownerTable)
        {
            PdmColumnInfoModel PdmColumnInfoModel = new PdmColumnInfoModel(ownerTable);
            XmlElement xmlElement = (XmlElement)xnColumn;
            PdmColumnInfoModel.ColumnId = xmlElement.GetAttribute("Id");
            XmlNodeList childNodes = xmlElement.ChildNodes;
            foreach (XmlNode xmlNode in childNodes)
            {
                string name = xmlNode.Name;
                switch (name)
                {
                    case "a:ObjectID":
                        PdmColumnInfoModel.ObjectID = xmlNode.InnerText;
                        break;
                    case "a:Name":
                        PdmColumnInfoModel.Name = xmlNode.InnerText;
                        break;
                    case "a:Code":
                        PdmColumnInfoModel.Code = xmlNode.InnerText;
                        break;
                    case "a:CreationDate":
                        PdmColumnInfoModel.CreationDate = String2DateTime(xmlNode.InnerText);
                        break;
                    case "a:Creator":
                        PdmColumnInfoModel.Creator = xmlNode.InnerText;
                        break;
                    case "a:ModificationDate":
                        PdmColumnInfoModel.ModificationDate = String2DateTime(xmlNode.InnerText);
                        break;
                    case "a:Modifier":
                        PdmColumnInfoModel.Modifier = xmlNode.InnerText;
                        break;
                    case "a:Comment":
                        PdmColumnInfoModel.Comment = xmlNode.InnerText;
                        break;
                    case "a:DataType":
                        PdmColumnInfoModel.DataType = xmlNode.InnerText;
                        break;
                    case "a:Length":
                        PdmColumnInfoModel.Length = xmlNode.InnerText;
                        break;
                    case "a:Identity":
                        PdmColumnInfoModel.Identity = ConvertToBooleanPg(xmlNode.InnerText);
                        break;
                    case "a:Mandatory":
                        PdmColumnInfoModel.Mandatory = ConvertToBooleanPg(xmlNode.InnerText);
                        break;
                    case "a:PhysicalOptions":
                        PdmColumnInfoModel.PhysicalOptions = xmlNode.InnerText;
                        break;
                    case "a:ExtendedAttributesText":
                        PdmColumnInfoModel.ExtendedAttributesText = xmlNode.InnerText;
                        break;
                    case "a:Precision":
                        PdmColumnInfoModel.Precision = xmlNode.InnerText;
                        break;
                }
            }
            return PdmColumnInfoModel;
        }

        private static string GetPrimaryKey(XmlNode xnKey)
        {
            XmlElement xmlElement = (XmlElement)xnKey;
            string result;
            if (xmlElement.ChildNodes.Count <= 0)
            {
                result = "";
            }
            else
            {
                XmlElement xmlElement2 = (XmlElement)xmlElement.ChildNodes[0];
                result = xmlElement2.GetAttribute("Ref");
            }
            return result;
        }

        private static void InitKeyColumns(XmlNode xnKeyColumns, PdmKeyModel Key)
        {
            XmlElement xmlElement = (XmlElement)xnKeyColumns;
            XmlNodeList childNodes = xmlElement.ChildNodes;
            foreach (string current in from XmlNode xnP in childNodes
                                       select ((XmlElement)xnP).GetAttribute("Ref"))
            {
                Key.AddColumnObjCode(current);
            }
        }

        private static PdmKeyModel GetKey(XmlNode xnKey, PdmTableInfoModel ownerTable)
        {
            PdmKeyModel PdmKeyModel = new PdmKeyModel(ownerTable);
            XmlElement xmlElement = (XmlElement)xnKey;
            PdmKeyModel.KeyId = xmlElement.GetAttribute("Id");
            XmlNodeList childNodes = xmlElement.ChildNodes;
            foreach (XmlNode xmlNode in childNodes)
            {
                string name = xmlNode.Name;
                switch (name)
                {
                    case "a:ObjectID":
                        PdmKeyModel.ObjectID = xmlNode.InnerText;
                        break;
                    case "a:Name":
                        PdmKeyModel.Name = xmlNode.InnerText;
                        break;
                    case "a:Code":
                        PdmKeyModel.Code = xmlNode.InnerText;
                        break;
                    case "a:CreationDate":
                        PdmKeyModel.CreationDate = String2DateTime(xmlNode.InnerText);
                        break;
                    case "a:Creator":
                        PdmKeyModel.Creator = xmlNode.InnerText;
                        break;
                    case "a:ModificationDate":
                        PdmKeyModel.ModificationDate = String2DateTime(xmlNode.InnerText);
                        break;
                    case "a:Modifier":
                        PdmKeyModel.Modifier = xmlNode.InnerText;
                        break;
                    case "c:Key.Columns":
                        InitKeyColumns(xmlNode, PdmKeyModel);
                        break;
                }
            }
            return PdmKeyModel;
        }
    }
}
