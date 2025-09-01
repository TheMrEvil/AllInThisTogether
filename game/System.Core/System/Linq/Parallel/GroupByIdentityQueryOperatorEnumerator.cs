using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A3 RID: 419
	internal sealed class GroupByIdentityQueryOperatorEnumerator<TSource, TGroupKey, TOrderKey> : GroupByQueryOperatorEnumerator<TSource, TGroupKey, TSource, TOrderKey>
	{
		// Token: 0x06000B04 RID: 2820 RVA: 0x00026A9B File Offset: 0x00024C9B
		internal GroupByIdentityQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> source, IEqualityComparer<TGroupKey> keyComparer, CancellationToken cancellationToken) : base(source, keyComparer, cancellationToken)
		{
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00026AA8 File Offset: 0x00024CA8
		protected override HashLookup<Wrapper<TGroupKey>, ListChunk<TSource>> BuildHashLookup()
		{
			HashLookup<Wrapper<TGroupKey>, ListChunk<TSource>> hashLookup = new HashLookup<Wrapper<TGroupKey>, ListChunk<TSource>>(new WrapperEqualityComparer<TGroupKey>(this._keyComparer));
			Pair<TSource, TGroupKey> pair = default(Pair<TSource, TGroupKey>);
			TOrderKey torderKey = default(TOrderKey);
			int num = 0;
			while (this._source.MoveNext(ref pair, ref torderKey))
			{
				if ((num++ & 63) == 0)
				{
					CancellationState.ThrowIfCanceled(this._cancellationToken);
				}
				Wrapper<TGroupKey> key = new Wrapper<TGroupKey>(pair.Second);
				ListChunk<TSource> listChunk = null;
				if (!hashLookup.TryGetValue(key, ref listChunk))
				{
					listChunk = new ListChunk<TSource>(2);
					hashLookup.Add(key, listChunk);
				}
				listChunk.Add(pair.First);
			}
			return hashLookup;
		}
	}
}
