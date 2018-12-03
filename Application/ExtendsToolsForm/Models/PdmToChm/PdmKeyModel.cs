using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtendsToolsForm.Models.PdmToChm
{
    /// <summary>
    /// pdm中主外键model
    /// </summary>
    public class PdmKeyModel
    {
        private string keyId;

        private string objectID;

        private string name;

        private string code;

        private DateTime creationDate;

        private string creator;

        private DateTime modificationDate;

        private string modifier;

        private IList<PdmColumnInfoModel> columns;

        private List<string> _ColumnObjCodes = new List<string>();

        private PdmTableInfoModel _OwnerTable = null;

        public string KeyId
        {
            get
            {
                return this.keyId;
            }
            set
            {
                this.keyId = value;
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

        public IList<PdmColumnInfoModel> Columns
        {
            get
            {
                return this.columns;
            }
        }

        public List<string> ColumnObjCodes
        {
            get
            {
                return this._ColumnObjCodes;
            }
        }

        public void AddColumn(PdmColumnInfoModel mColumn)
        {
            if (this.columns == null)
            {
                this.columns = new List<PdmColumnInfoModel>();
            }
            this.columns.Add(mColumn);
        }

        public void AddColumnObjCode(string ObjCode)
        {
            this._ColumnObjCodes.Add(ObjCode);
        }

        public PdmKeyModel(PdmTableInfoModel table)
        {
            this._OwnerTable = table;
        }
    }
}
