using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000033 RID: 51
	internal class SmiOrderProperty : SmiMetaDataProperty
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x00007EEF File Offset: 0x000060EF
		internal SmiOrderProperty(IList<SmiOrderProperty.SmiColumnOrder> columnOrders)
		{
			this._columns = new ReadOnlyCollection<SmiOrderProperty.SmiColumnOrder>(columnOrders);
		}

		// Token: 0x17000078 RID: 120
		internal SmiOrderProperty.SmiColumnOrder this[int ordinal]
		{
			get
			{
				if (this._columns.Count <= ordinal)
				{
					return new SmiOrderProperty.SmiColumnOrder
					{
						Order = SortOrder.Unspecified,
						SortOrdinal = -1
					};
				}
				return this._columns[ordinal];
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00007EED File Offset: 0x000060ED
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x040004B0 RID: 1200
		private IList<SmiOrderProperty.SmiColumnOrder> _columns;

		// Token: 0x02000034 RID: 52
		internal struct SmiColumnOrder
		{
			// Token: 0x040004B1 RID: 1201
			internal int SortOrdinal;

			// Token: 0x040004B2 RID: 1202
			internal SortOrder Order;
		}
	}
}
