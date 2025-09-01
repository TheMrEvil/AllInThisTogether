using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A5 RID: 421
	internal abstract class OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey> : QueryOperatorEnumerator<IGrouping<TGroupKey, TElement>, TOrderKey>
	{
		// Token: 0x06000B08 RID: 2824 RVA: 0x00026BFF File Offset: 0x00024DFF
		protected OrderedGroupByQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> source, Func<TSource, TGroupKey> keySelector, IEqualityComparer<TGroupKey> keyComparer, IComparer<TOrderKey> orderComparer, CancellationToken cancellationToken)
		{
			this._source = source;
			this._keySelector = keySelector;
			this._keyComparer = keyComparer;
			this._orderComparer = orderComparer;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00026C2C File Offset: 0x00024E2C
		internal override bool MoveNext(ref IGrouping<TGroupKey, TElement> currentElement, ref TOrderKey currentKey)
		{
			OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables mutables = this._mutables;
			if (mutables == null)
			{
				mutables = (this._mutables = new OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables());
				mutables._hashLookup = this.BuildHashLookup();
				mutables._hashLookupIndex = -1;
			}
			OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables mutables2 = mutables;
			int num = mutables2._hashLookupIndex + 1;
			mutables2._hashLookupIndex = num;
			if (num < mutables._hashLookup.Count)
			{
				OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.GroupKeyData value = mutables._hashLookup[mutables._hashLookupIndex].Value;
				currentElement = value._grouping;
				currentKey = value._orderKey;
				return true;
			}
			return false;
		}

		// Token: 0x06000B0A RID: 2826
		protected abstract HashLookup<Wrapper<TGroupKey>, OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.GroupKeyData> BuildHashLookup();

		// Token: 0x06000B0B RID: 2827 RVA: 0x00026CB5 File Offset: 0x00024EB5
		protected override void Dispose(bool disposing)
		{
			this._source.Dispose();
		}

		// Token: 0x040007A0 RID: 1952
		protected readonly QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> _source;

		// Token: 0x040007A1 RID: 1953
		private readonly Func<TSource, TGroupKey> _keySelector;

		// Token: 0x040007A2 RID: 1954
		protected readonly IEqualityComparer<TGroupKey> _keyComparer;

		// Token: 0x040007A3 RID: 1955
		protected readonly IComparer<TOrderKey> _orderComparer;

		// Token: 0x040007A4 RID: 1956
		protected readonly CancellationToken _cancellationToken;

		// Token: 0x040007A5 RID: 1957
		private OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables _mutables;

		// Token: 0x020001A6 RID: 422
		private class Mutables
		{
			// Token: 0x06000B0C RID: 2828 RVA: 0x00002162 File Offset: 0x00000362
			public Mutables()
			{
			}

			// Token: 0x040007A6 RID: 1958
			internal HashLookup<Wrapper<TGroupKey>, OrderedGroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.GroupKeyData> _hashLookup;

			// Token: 0x040007A7 RID: 1959
			internal int _hashLookupIndex;
		}

		// Token: 0x020001A7 RID: 423
		protected class GroupKeyData
		{
			// Token: 0x06000B0D RID: 2829 RVA: 0x00026CC2 File Offset: 0x00024EC2
			internal GroupKeyData(TOrderKey orderKey, TGroupKey hashKey, IComparer<TOrderKey> orderComparer)
			{
				this._orderKey = orderKey;
				this._grouping = new OrderedGroupByGrouping<TGroupKey, TOrderKey, TElement>(hashKey, orderComparer);
			}

			// Token: 0x040007A8 RID: 1960
			internal TOrderKey _orderKey;

			// Token: 0x040007A9 RID: 1961
			internal OrderedGroupByGrouping<TGroupKey, TOrderKey, TElement> _grouping;
		}
	}
}
