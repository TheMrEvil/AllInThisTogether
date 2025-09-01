using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000D4 RID: 212
	internal sealed class GroupedResultEnumerable<TSource, TKey, TResult> : IIListProvider<TResult>, IEnumerable<!2>, IEnumerable
	{
		// Token: 0x06000796 RID: 1942 RVA: 0x0001AD68 File Offset: 0x00018F68
		public GroupedResultEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
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
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			this._resultSelector = resultSelector;
			this._comparer = comparer;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001ADC5 File Offset: 0x00018FC5
		public IEnumerator<TResult> GetEnumerator()
		{
			return Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer).ApplyResultSelector<TResult>(this._resultSelector).GetEnumerator();
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001ADEE File Offset: 0x00018FEE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001ADF6 File Offset: 0x00018FF6
		public TResult[] ToArray()
		{
			return Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer).ToArray<TResult>(this._resultSelector);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001AE1A File Offset: 0x0001901A
		public List<TResult> ToList()
		{
			return Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer).ToList<TResult>(this._resultSelector);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001AE3E File Offset: 0x0001903E
		public int GetCount(bool onlyIfCheap)
		{
			if (!onlyIfCheap)
			{
				return Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer).Count;
			}
			return -1;
		}

		// Token: 0x04000581 RID: 1409
		private readonly IEnumerable<TSource> _source;

		// Token: 0x04000582 RID: 1410
		private readonly Func<TSource, TKey> _keySelector;

		// Token: 0x04000583 RID: 1411
		private readonly IEqualityComparer<TKey> _comparer;

		// Token: 0x04000584 RID: 1412
		private readonly Func<TKey, IEnumerable<TSource>, TResult> _resultSelector;
	}
}
