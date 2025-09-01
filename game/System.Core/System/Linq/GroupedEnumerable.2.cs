using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000D6 RID: 214
	internal sealed class GroupedEnumerable<TSource, TKey> : IIListProvider<IGrouping<TKey, TSource>>, IEnumerable<IGrouping<TKey, TSource>>, IEnumerable
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x0001AF5E File Offset: 0x0001915E
		public GroupedEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			this._source = source;
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			this._keySelector = keySelector;
			this._comparer = comparer;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001AF99 File Offset: 0x00019199
		public IEnumerator<IGrouping<TKey, TSource>> GetEnumerator()
		{
			return Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer).GetEnumerator();
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001AFB7 File Offset: 0x000191B7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001AFBF File Offset: 0x000191BF
		public IGrouping<TKey, TSource>[] ToArray()
		{
			return ((IIListProvider<IGrouping<TKey, TSource>>)Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer)).ToArray();
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001AFDD File Offset: 0x000191DD
		public List<IGrouping<TKey, TSource>> ToList()
		{
			return ((IIListProvider<IGrouping<TKey, TSource>>)Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer)).ToList();
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001AFFB File Offset: 0x000191FB
		public int GetCount(bool onlyIfCheap)
		{
			if (!onlyIfCheap)
			{
				return Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer).Count;
			}
			return -1;
		}

		// Token: 0x04000589 RID: 1417
		private readonly IEnumerable<TSource> _source;

		// Token: 0x0400058A RID: 1418
		private readonly Func<TSource, TKey> _keySelector;

		// Token: 0x0400058B RID: 1419
		private readonly IEqualityComparer<TKey> _comparer;
	}
}
