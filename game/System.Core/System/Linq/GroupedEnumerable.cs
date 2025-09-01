using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000D5 RID: 213
	internal sealed class GroupedEnumerable<TSource, TKey, TElement> : IIListProvider<IGrouping<TKey, TElement>>, IEnumerable<IGrouping<TKey, TElement>>, IEnumerable
	{
		// Token: 0x0600079C RID: 1948 RVA: 0x0001AE64 File Offset: 0x00019064
		public GroupedEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
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
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001AEC1 File Offset: 0x000190C1
		public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
		{
			return Lookup<TKey, TElement>.Create<TSource>(this._source, this._keySelector, this._elementSelector, this._comparer).GetEnumerator();
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001AEE5 File Offset: 0x000190E5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001AEED File Offset: 0x000190ED
		public IGrouping<TKey, TElement>[] ToArray()
		{
			return ((IIListProvider<IGrouping<TKey, TElement>>)Lookup<TKey, TElement>.Create<TSource>(this._source, this._keySelector, this._elementSelector, this._comparer)).ToArray();
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001AF11 File Offset: 0x00019111
		public List<IGrouping<TKey, TElement>> ToList()
		{
			return ((IIListProvider<IGrouping<TKey, TElement>>)Lookup<TKey, TElement>.Create<TSource>(this._source, this._keySelector, this._elementSelector, this._comparer)).ToList();
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001AF35 File Offset: 0x00019135
		public int GetCount(bool onlyIfCheap)
		{
			if (!onlyIfCheap)
			{
				return Lookup<TKey, TElement>.Create<TSource>(this._source, this._keySelector, this._elementSelector, this._comparer).Count;
			}
			return -1;
		}

		// Token: 0x04000585 RID: 1413
		private readonly IEnumerable<TSource> _source;

		// Token: 0x04000586 RID: 1414
		private readonly Func<TSource, TKey> _keySelector;

		// Token: 0x04000587 RID: 1415
		private readonly Func<TSource, TElement> _elementSelector;

		// Token: 0x04000588 RID: 1416
		private readonly IEqualityComparer<TKey> _comparer;
	}
}
