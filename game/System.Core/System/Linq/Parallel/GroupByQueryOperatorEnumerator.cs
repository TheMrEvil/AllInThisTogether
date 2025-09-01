using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A1 RID: 417
	internal abstract class GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey> : QueryOperatorEnumerator<IGrouping<TGroupKey, TElement>, TOrderKey>
	{
		// Token: 0x06000AFF RID: 2815 RVA: 0x000269FC File Offset: 0x00024BFC
		protected GroupByQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> source, IEqualityComparer<TGroupKey> keyComparer, CancellationToken cancellationToken)
		{
			this._source = source;
			this._keyComparer = keyComparer;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00026A1C File Offset: 0x00024C1C
		internal override bool MoveNext(ref IGrouping<TGroupKey, TElement> currentElement, ref TOrderKey currentKey)
		{
			GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables mutables = this._mutables;
			if (mutables == null)
			{
				mutables = (this._mutables = new GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables());
				mutables._hashLookup = this.BuildHashLookup();
				mutables._hashLookupIndex = -1;
			}
			GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables mutables2 = mutables;
			int num = mutables2._hashLookupIndex + 1;
			mutables2._hashLookupIndex = num;
			if (num < mutables._hashLookup.Count)
			{
				currentElement = new GroupByGrouping<TGroupKey, TElement>(mutables._hashLookup[mutables._hashLookupIndex]);
				return true;
			}
			return false;
		}

		// Token: 0x06000B01 RID: 2817
		protected abstract HashLookup<Wrapper<TGroupKey>, ListChunk<TElement>> BuildHashLookup();

		// Token: 0x06000B02 RID: 2818 RVA: 0x00026A8E File Offset: 0x00024C8E
		protected override void Dispose(bool disposing)
		{
			this._source.Dispose();
		}

		// Token: 0x04000799 RID: 1945
		protected readonly QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> _source;

		// Token: 0x0400079A RID: 1946
		protected readonly IEqualityComparer<TGroupKey> _keyComparer;

		// Token: 0x0400079B RID: 1947
		protected readonly CancellationToken _cancellationToken;

		// Token: 0x0400079C RID: 1948
		private GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>.Mutables _mutables;

		// Token: 0x020001A2 RID: 418
		private class Mutables
		{
			// Token: 0x06000B03 RID: 2819 RVA: 0x00002162 File Offset: 0x00000362
			public Mutables()
			{
			}

			// Token: 0x0400079D RID: 1949
			internal HashLookup<Wrapper<TGroupKey>, ListChunk<TElement>> _hashLookup;

			// Token: 0x0400079E RID: 1950
			internal int _hashLookupIndex;
		}
	}
}
