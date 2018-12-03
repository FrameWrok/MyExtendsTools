using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtendsToolsForm.Models.PdmToChm
{
    /// <summary>
    /// pdm model
    /// </summary>
    public class PdmModel
    {
        public IList<PdmTableInfoModel> Tables
        {
            get;
            private set;
        }

        public PdmModel()
        {
            this.Tables = new List<PdmTableInfoModel>();
        }
    }
}
