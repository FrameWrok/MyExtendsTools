using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtendsToolsForm.Models.PdmToChm
{
    /// <summary>
    /// pdm文件中表model
    /// </summary>
    public class PdmTableInfoModel
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

        private List<PdmColumnInfoModel> columns;

        private List<PdmKeyModel> keys;

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

        public List<PdmColumnInfoModel> Columns
        {
            get
            {
                return this.columns;
            }
        }

        public IList<PdmKeyModel> Keys
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

        public PdmKeyModel PrimaryKey
        {
            get
            {
                foreach (PdmKeyModel current in this.keys)
                {
                    if (current.KeyId == this.PrimaryKeyRefCode)
                        return current;
                }
                return null;
            }
        }

        public string Description
        {
            get;
            set;
        }

        public PdmTableInfoModel()
        {
            this.keys = new List<PdmKeyModel>();
            this.columns = new List<PdmColumnInfoModel>();
        }

        public void AddColumn(PdmColumnInfoModel mColumn)
        {
            if (this.columns == null)
            {
                this.columns = new List<PdmColumnInfoModel>();
            }
            this.columns.Add(mColumn);
        }

        public void AddKey(PdmKeyModel mKey)
        {
            if (this.keys == null)
            {
                this.keys = new List<PdmKeyModel>();
            }
            this.keys.Add(mKey);
        }
    }
}
