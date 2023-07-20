using System;
using System.Collections.Generic;

namespace PdmModels
{
	public class PdmModels
	{
		public IList<TableInfo> Tables
		{
			get;
			private set;
		}

		public PdmModels()
		{
			this.Tables = new List<TableInfo>();
		}
	}
}
