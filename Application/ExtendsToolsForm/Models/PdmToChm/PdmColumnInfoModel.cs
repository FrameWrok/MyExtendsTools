using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtendsToolsForm.Models.PdmToChm
{
    /// <summary>
    /// pdm 中列信息
    /// </summary>
    public class PdmColumnInfoModel
    {
        private PdmTableInfoModel _OwnerTable;

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

        public PdmTableInfoModel OwnerTable
        {
            get
            {
                return this._OwnerTable;
            }
        }
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey
        {
            get
            {
                PdmKeyModel primaryKey = this._OwnerTable.PrimaryKey;
                if (primaryKey != null)
                {
                    if (primaryKey.ColumnObjCodes.Contains(this.columnId))
                        return true;
                }
                return false;
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
        /// <summary>
        /// pdm 字段名称
        /// </summary>
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
        /// <summary>
        /// 字段code
        /// </summary>
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
        /// <summary>
        /// 字段备注
        /// </summary>
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
        /// <summary>
        /// 字段数据类型
        /// </summary>
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
        /// <summary>
        /// 字段长度
        /// </summary>
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
        /// <summary>
        /// 是否自增
        /// </summary>
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

        /// <summary>
        /// 是否允许为 null
        /// </summary>
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

        public PdmColumnInfoModel(PdmTableInfoModel OwnerTable)
        {
            this._OwnerTable = OwnerTable;
        }
    }
}
