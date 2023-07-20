using System;

namespace PdmModels
{
	public class ColumnInfo
	{
		private TableInfo _OwnerTable;

		private string columnId;

		private string objectID;

		private string name;

		private string code;

		private DateTime creationDate;

		private string creator;

		private DateTime modificationDate;

		private string modifier;

		private string comment;

		private string dataType;

		private string length;

		private bool identity;

		private bool mandatory;

		private string extendedAttributesText;

		public TableInfo OwnerTable
		{
			get
			{
				return this._OwnerTable;
			}
		}

		public bool IsPrimaryKey
		{
			get
			{
				PdmKey primaryKey = this._OwnerTable.PrimaryKey;
				bool result;
				if (primaryKey != null)
				{
					if (primaryKey.ColumnObjCodes.Contains(this.columnId))
					{
						result = true;
						return result;
					}
				}
				result = false;
				return result;
			}
		}

		public string ColumnId
		{
			get
			{
				return this.columnId;
			}
			set
			{
				this.columnId = value;
			}
		}

		public string ObjectID
		{
			get
			{
				return this.objectID;
			}
			set
			{
				this.objectID = value;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		public string Code
		{
			get
			{
				return this.code;
			}
			set
			{
				this.code = value;
			}
		}

		public DateTime CreationDate
		{
			get
			{
				return this.creationDate;
			}
			set
			{
				this.creationDate = value;
			}
		}

		public string Creator
		{
			get
			{
				return this.creator;
			}
			set
			{
				this.creator = value;
			}
		}

		public DateTime ModificationDate
		{
			get
			{
				return this.modificationDate;
			}
			set
			{
				this.modificationDate = value;
			}
		}

		public string Modifier
		{
			get
			{
				return this.modifier;
			}
			set
			{
				this.modifier = value;
			}
		}

		public string Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = value;
			}
		}

		public string DataType
		{
			get
			{
				return this.dataType;
			}
			set
			{
				this.dataType = value;
			}
		}

		public string Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}

		public bool Identity
		{
			get
			{
				return this.identity;
			}
			set
			{
				this.identity = value;
			}
		}

		public bool Mandatory
		{
			get
			{
				return this.mandatory;
			}
			set
			{
				this.mandatory = value;
			}
		}

		public string ExtendedAttributesText
		{
			get
			{
				return this.extendedAttributesText;
			}
			set
			{
				this.extendedAttributesText = value;
			}
		}

		public string PhysicalOptions
		{
			get;
			set;
		}

		public string Precision
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public ColumnInfo(TableInfo OwnerTable)
		{
			this._OwnerTable = OwnerTable;
		}
	}
}
