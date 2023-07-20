using PdmModels;
using System;
using System.Linq;
using System.Xml;

namespace PdmUtil
{
	public class PdmReader
	{
		private DateTime _baseDateTime = new DateTime(1970, 1, 1, 8, 0, 0);

		public PdmModels ReadFromFile(string pdmFile)
		{
			PdmModels result;
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
				PdmModels pdmModels = new PdmModels();
				XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//c:Tables", xmlNamespaceManager);
				if (xmlNodeList != null)
				{
					foreach (XmlNode current in from XmlNode xmlTables in xmlNodeList
					from xnTable in xmlTables.ChildNodes.Cast<XmlNode>()
					where xnTable.Name != "o:Shortcut"
					select xnTable)
					{
						pdmModels.Tables.Add(this.GetTable(current));
					}
				}
				result = pdmModels;
			}
			return result;
		}

		private TableInfo GetTable(XmlNode xnTable)
		{
			TableInfo tableInfo = new TableInfo();
			XmlElement xmlElement = (XmlElement)xnTable;
			tableInfo.TableId = xmlElement.GetAttribute("Id");
			XmlNodeList childNodes = xmlElement.ChildNodes;
			foreach (XmlNode xmlNode in childNodes)
			{
				string name = xmlNode.Name;
				switch (name)
				{
				case "a:ObjectID":
					tableInfo.ObjectID = xmlNode.InnerText;
					break;
				case "a:Name":
					tableInfo.Name = xmlNode.InnerText;
					break;
				case "a:Code":
					tableInfo.Code = xmlNode.InnerText;
					break;
				case "a:CreationDate":
					tableInfo.CreationDate = this.String2DateTime(xmlNode.InnerText);
					break;
				case "a:Creator":
					tableInfo.Creator = xmlNode.InnerText;
					break;
				case "a:ModificationDate":
					tableInfo.ModificationDate = this.String2DateTime(xmlNode.InnerText);
					break;
				case "a:Modifier":
					tableInfo.Modifier = xmlNode.InnerText;
					break;
				case "a:Comment":
					tableInfo.Comment = xmlNode.InnerText;
					break;
				case "a:PhysicalOptions":
					tableInfo.PhysicalOptions = xmlNode.InnerText;
					break;
				case "c:Columns":
					this.InitColumns(xmlNode, tableInfo);
					break;
				case "c:Keys":
					this.InitKeys(xmlNode, tableInfo);
					break;
				case "c:PrimaryKey":
					this.InitPrimaryKey(xmlNode, tableInfo);
					break;
				case "a:Description":
					tableInfo.Description = xmlNode.InnerText;
					break;
				}
			}
			return tableInfo;
		}

		private DateTime String2DateTime(string dateString)
		{
			long num = long.Parse(dateString);
			return this._baseDateTime.AddSeconds((double)num);
		}

		private void InitColumns(XmlNode xnColumns, TableInfo pTable)
		{
			foreach (XmlNode xnColumn in xnColumns)
			{
				pTable.AddColumn(this.GetColumn(xnColumn, pTable));
			}
		}

		private void InitKeys(XmlNode xnKeys, TableInfo pTable)
		{
			foreach (XmlNode xnKey in xnKeys)
			{
				pTable.AddKey(this.GetKey(xnKey, pTable));
			}
		}

		private void InitPrimaryKey(XmlNode xnPrimaryKey, TableInfo pTable)
		{
			pTable.PrimaryKeyRefCode = this.GetPrimaryKey(xnPrimaryKey);
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

		private ColumnInfo GetColumn(XmlNode xnColumn, TableInfo ownerTable)
		{
			ColumnInfo columnInfo = new ColumnInfo(ownerTable);
			XmlElement xmlElement = (XmlElement)xnColumn;
			columnInfo.ColumnId = xmlElement.GetAttribute("Id");
			XmlNodeList childNodes = xmlElement.ChildNodes;
			foreach (XmlNode xmlNode in childNodes)
			{
				string name = xmlNode.Name;
				switch (name)
				{
				case "a:ObjectID":
					columnInfo.ObjectID = xmlNode.InnerText;
					break;
				case "a:Name":
					columnInfo.Name = xmlNode.InnerText;
					break;
				case "a:Code":
					columnInfo.Code = xmlNode.InnerText;
					break;
				case "a:CreationDate":
					columnInfo.CreationDate = this.String2DateTime(xmlNode.InnerText);
					break;
				case "a:Creator":
					columnInfo.Creator = xmlNode.InnerText;
					break;
				case "a:ModificationDate":
					columnInfo.ModificationDate = this.String2DateTime(xmlNode.InnerText);
					break;
				case "a:Modifier":
					columnInfo.Modifier = xmlNode.InnerText;
					break;
				case "a:Comment":
					columnInfo.Comment = xmlNode.InnerText;
					break;
				case "a:DataType":
					columnInfo.DataType = xmlNode.InnerText;
					break;
				case "a:Length":
					columnInfo.Length = xmlNode.InnerText;
					break;
				case "a:Identity":
					columnInfo.Identity = PdmReader.ConvertToBooleanPg(xmlNode.InnerText);
					break;
				case "a:Mandatory":
					columnInfo.Mandatory = PdmReader.ConvertToBooleanPg(xmlNode.InnerText);
					break;
				case "a:PhysicalOptions":
					columnInfo.PhysicalOptions = xmlNode.InnerText;
					break;
				case "a:ExtendedAttributesText":
					columnInfo.ExtendedAttributesText = xmlNode.InnerText;
					break;
				case "a:Precision":
					columnInfo.Precision = xmlNode.InnerText;
					break;
				}
			}
			return columnInfo;
		}

		private string GetPrimaryKey(XmlNode xnKey)
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

		private void InitKeyColumns(XmlNode xnKeyColumns, PdmKey Key)
		{
			XmlElement xmlElement = (XmlElement)xnKeyColumns;
			XmlNodeList childNodes = xmlElement.ChildNodes;
			foreach (string current in from XmlNode xnP in childNodes
			select ((XmlElement)xnP).GetAttribute("Ref"))
			{
				Key.AddColumnObjCode(current);
			}
		}

		private PdmKey GetKey(XmlNode xnKey, TableInfo ownerTable)
		{
			PdmKey pdmKey = new PdmKey(ownerTable);
			XmlElement xmlElement = (XmlElement)xnKey;
			pdmKey.KeyId = xmlElement.GetAttribute("Id");
			XmlNodeList childNodes = xmlElement.ChildNodes;
			foreach (XmlNode xmlNode in childNodes)
			{
				string name = xmlNode.Name;
				switch (name)
				{
				case "a:ObjectID":
					pdmKey.ObjectID = xmlNode.InnerText;
					break;
				case "a:Name":
					pdmKey.Name = xmlNode.InnerText;
					break;
				case "a:Code":
					pdmKey.Code = xmlNode.InnerText;
					break;
				case "a:CreationDate":
					pdmKey.CreationDate = this.String2DateTime(xmlNode.InnerText);
					break;
				case "a:Creator":
					pdmKey.Creator = xmlNode.InnerText;
					break;
				case "a:ModificationDate":
					pdmKey.ModificationDate = this.String2DateTime(xmlNode.InnerText);
					break;
				case "a:Modifier":
					pdmKey.Modifier = xmlNode.InnerText;
					break;
				case "c:Key.Columns":
					this.InitKeyColumns(xmlNode, pdmKey);
					break;
				}
			}
			return pdmKey;
		}
	}
}
