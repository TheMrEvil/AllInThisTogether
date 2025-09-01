using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000D3 RID: 211
	internal sealed class GroupedResultEnumerable<TSource, TKey, TElement, TResult> : IIListProvider<TResult>, IEnumerable<!3>, IEnumerable
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x0001AC40 File Offset: 0x00018E40
		public GroupedResultEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
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
			if (elementSelector == null)
			{
				throw Error.ArgumentNull("elementSelector");
			}
			this._elementSelector = elementSelector;
			this._comparer = comparer;
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			this._resultSelector = resultSelector;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001ACB4 File Offset: 0x00018EB4
		public IEnumerator<TResult> GetEnumerator()
		{
			return Lookup<TKey, TElement>.Create<TSource>(this._source, this._keySelector, this._elementSelector, this._comparer).ApplyResultSelector<TResult>(this._resultSelector).GetEnumerator();
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001ACE3 File Offset: 0x00018EE3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001ACEB File Offset: 0x00018EEB
		public TResult[] ToArray()
		{
			return Lookup<TKey, TElement>.Create<TSource>(this._source, this._keySelector, this._elementSelector, this._comparer).ToArray<TResult>(this._resultSelector);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001AD15 File Offset: 0x00018F15
		public List<TResult> ToList()
		{
			return Lookup<TKey, TElement>.Create<TSource>(this._source, this._keySelector, this._elementSelector, this._comparer).ToList<TResult>(this._resultSelector);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001AD3F File Offset: 0x00018F3F
		public int GetCount(bool onlyIfCheap)
		{
			if (!onlyIfCheap)
			{
				return Lookup<TKey, TElement>.Create<TSource>(this._source, this._keySelector, this._elementSelector, this._comparer).Count;
			}
			return -1;
		}

		// Token: 0x0400057C RID: 1404
		private readonly IEnumerable<TSource> _source;

		// Token: 0x0400057D RID: 1405
		private readonly Func<TSource, TKey> _keySelector;

		// Token: 0x0400057E RID: 1406
		private readonly Func<TSource, TElement> _elementSelector;

		// Token: 0x0400057F RID: 1407
		private readonly IEqualityComparer<TKey> _comparer;

		// Token: 0x04000580 RID: 1408
		private readonly Func<TKey, IEnumerable<TElement>, TResult> _resultSelector;
	}
}
