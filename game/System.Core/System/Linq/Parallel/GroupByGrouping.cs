using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001AA RID: 426
	internal class GroupByGrouping<TGroupKey, TElement> : IGrouping<TGroupKey, TElement>, IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x06000B12 RID: 2834 RVA: 0x00026F1C File Offset: 0x0002511C
		internal GroupByGrouping(KeyValuePair<Wrapper<TGroupKey>, ListChunk<TElement>> keyValues)
		{
			this._keyValues = keyValues;
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00026F2B File Offset: 0x0002512B
		TGroupKey IGrouping<!0, !1>.Key
		{
			get
			{
				return this._keyValues.Key.Value;
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00026F3D File Offset: 0x0002513D
		IEnumerator<TElement> IEnumerable<!1>.GetEnumerator()
		{
			return this._keyValues.Value.GetEnumerator();
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00026F4F File Offset: 0x0002514F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<!1>)this).GetEnumerator();
		}

		// Token: 0x040007AB RID: 1963
		private KeyValuePair<Wrapper<TGroupKey>, ListChunk<TElement>> _keyValues;
	}
}
