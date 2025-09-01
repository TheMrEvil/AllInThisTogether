using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A4 RID: 420
	internal sealed class GroupByElementSelectorQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey> : GroupByQueryOperatorEnumerator<TSource, TGroupKey, TElement, TOrderKey>
	{
		// Token: 0x06000B06 RID: 2822 RVA: 0x00026B44 File Offset: 0x00024D44
		internal GroupByElementSelectorQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TSource, TGroupKey>, TOrderKey> source, IEqualityComparer<TGroupKey> keyComparer, Func<TSource, TElement> elementSelector, CancellationToken cancellationToken) : base(source, keyComparer, cancellationToken)
		{
			this._elementSelector = elementSelector;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00026B58 File Offset: 0x00024D58
		protected override HashLookup<Wrapper<TGroupKey>, ListChunk<TElement>> BuildHashLookup()
		{
			HashLookup<Wrapper<TGroupKey>, ListChunk<TElement>> hashLookup = new HashLookup<Wrapper<TGroupKey>, ListChunk<TElement>>(new WrapperEqualityComparer<TGroupKey>(this._keyComparer));
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
				ListChunk<TElement> listChunk = null;
				if (!hashLookup.TryGetValue(key, ref listChunk))
				{
					listChunk = new ListChunk<TElement>(2);
					hashLookup.Add(key, listChunk);
				}
				listChunk.Add(this._elementSelector(pair.First));
			}
			return hashLookup;
		}

		// Token: 0x0400079F RID: 1951
		private readonly Func<TSource, TElement> _elementSelector;
	}
}
