using System;
using System.Diagnostics;

namespace System.Linq
{
	// Token: 0x020000CF RID: 207
	internal sealed class SystemLinq_LookupDebugView<TKey, TElement>
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x0001AA5B File Offset: 0x00018C5B
		public SystemLinq_LookupDebugView(Lookup<TKey, TElement> lookup)
		{
			this._lookup = lookup;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0001AA6C File Offset: 0x00018C6C
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public IGrouping<TKey, TElement>[] Groupings
		{
			get
			{
				IGrouping<TKey, TElement>[] result;
				if ((result = this._cachedGroupings) == null)
				{
					result = (this._cachedGroupings = this._lookup.ToArray<IGrouping<TKey, TElement>>());
				}
				return result;
			}
		}

		// Token: 0x04000570 RID: 1392
		private readonly Lookup<TKey, TElement> _lookup;

		// Token: 0x04000571 RID: 1393
		private IGrouping<TKey, TElement>[] _cachedGroupings;
	}
}
