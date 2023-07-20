using System;
using System.Collections.Generic;

namespace PdmModels
{
	public class TableInfo
	{
		private string tableId;

		private string objectID;

		private string name;

		private string code;

		private DateTime creationDate;

		private string creator;

		private DateTime modificationDate;

		private string modifier;

		private string comment;

		private string physicalOptions;

		private IList<ColumnInfo> columns;

		private IList<PdmKey> keys;

		public string TableId
		{
			get
			{
				return this.tableId;
			}
			set
			{
				this.tableId = value;
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

		public string PhysicalOptions
		{
			get
			{
				return this.physicalOptions;
			}
			set
			{
				this.physicalOptions = value;
			}
		}

		public IList<ColumnInfo> Columns
		{
			get
			{
				return this.columns;
			}
		}

		public IList<PdmKey> Keys
		{
			get
			{
				return this.keys;
			}
		}

		public string PrimaryKeyRefCode
		{
			get;
			set;
		}

		public PdmKey PrimaryKey
		{
			get
			{
				PdmKey result;
				foreach (PdmKey current in this.keys)
				{
					if (current.KeyId == this.PrimaryKeyRefCode)
					{
						result = current;
						return result;
					}
				}
				result = null;
				return result;
			}
		}

		public string Description
		{
			get;
			set;
		}

		public TableInfo()
		{
			this.keys = new List<PdmKey>();
			this.columns = new List<ColumnInfo>();
		}

		public void AddColumn(ColumnInfo mColumn)
		{
			if (this.columns == null)
			{
				this.columns = new List<ColumnInfo>();
			}
			this.columns.Add(mColumn);
		}

		public void AddKey(PdmKey mKey)
		{
			if (this.keys == null)
			{
				this.keys = new List<PdmKey>();
			}
			this.keys.Add(mKey);
		}
	}
}
