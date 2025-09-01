using System;
using System.Diagnostics;

namespace System.Linq
{
	// Token: 0x020000CE RID: 206
	internal sealed class SystemLinq_GroupingDebugView<TKey, TElement>
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x0001AA14 File Offset: 0x00018C14
		public SystemLinq_GroupingDebugView(Grouping<TKey, TElement> grouping)
		{
			this._grouping = grouping;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0001AA23 File Offset: 0x00018C23
		public TKey Key
		{
			get
			{
				return this._grouping.Key;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0001AA30 File Offset: 0x00018C30
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TElement[] Values
		{
			get
			{
				TElement[] result;
				if ((result = this._cachedValues) == null)
				{
					result = (this._cachedValues = this._grouping.ToArray<TElement>());
				}
				return result;
			}
		}

		// Token: 0x0400056E RID: 1390
		private readonly Grouping<TKey, TElement> _grouping;

		// Token: 0x0400056F RID: 1391
		private TElement[] _cachedValues;
	}
}
