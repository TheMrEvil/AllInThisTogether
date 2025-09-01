using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000035 RID: 53
	internal class SmiDefaultFieldsProperty : SmiMetaDataProperty
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x00007F45 File Offset: 0x00006145
		internal SmiDefaultFieldsProperty(IList<bool> defaultFields)
		{
			this._defaults = new ReadOnlyCollection<bool>(defaultFields);
		}

		// Token: 0x17000079 RID: 121
		internal bool this[int ordinal]
		{
			get
			{
				return this._defaults.Count > ordinal && this._defaults[ordinal];
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00007EED File Offset: 0x000060ED
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x040004B3 RID: 1203
		private IList<bool> _defaults;
	}
}
