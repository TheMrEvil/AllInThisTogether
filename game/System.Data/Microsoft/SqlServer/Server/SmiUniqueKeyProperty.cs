using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000032 RID: 50
	internal class SmiUniqueKeyProperty : SmiMetaDataProperty
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x00007EBB File Offset: 0x000060BB
		internal SmiUniqueKeyProperty(IList<bool> columnIsKey)
		{
			this._columns = new ReadOnlyCollection<bool>(columnIsKey);
		}

		// Token: 0x17000077 RID: 119
		internal bool this[int ordinal]
		{
			get
			{
				return this._columns.Count > ordinal && this._columns[ordinal];
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007EED File Offset: 0x000060ED
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x040004AF RID: 1199
		private IList<bool> _columns;
	}
}
