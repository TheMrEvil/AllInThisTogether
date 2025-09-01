using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000DF RID: 223
	internal sealed class OrderedEnumerable<TElement, TKey> : OrderedEnumerable<TElement>
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x0001BEC8 File Offset: 0x0001A0C8
		internal OrderedEnumerable(IEnumerable<TElement> source, Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending, OrderedEnumerable<TElement> parent)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			this._source = source;
			this._parent = parent;
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			this._keySelector = keySelector;
			this._comparer = (comparer ?? Comparer<TKey>.Default);
			this._descending = descending;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001BF28 File Offset: 0x0001A128
		internal override EnumerableSorter<TElement> GetEnumerableSorter(EnumerableSorter<TElement> next)
		{
			EnumerableSorter<TElement> enumerableSorter = new EnumerableSorter<TElement, TKey>(this._keySelector, this._comparer, this._descending, next);
			if (this._parent != null)
			{
				enumerableSorter = this._parent.GetEnumerableSorter(enumerableSorter);
			}
			return enumerableSorter;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001BF64 File Offset: 0x0001A164
		internal override CachingComparer<TElement> GetComparer(CachingComparer<TElement> childComparer)
		{
			CachingComparer<TElement> cachingComparer = (childComparer == null) ? new CachingComparer<TElement, TKey>(this._keySelector, this._comparer, this._descending) : new CachingComparerWithChild<TElement, TKey>(this._keySelector, this._comparer, this._descending, childComparer);
			if (this._parent == null)
			{
				return cachingComparer;
			}
			return this._parent.GetComparer(cachingComparer);
		}

		// Token: 0x040005A9 RID: 1449
		private readonly OrderedEnumerable<TElement> _parent;

		// Token: 0x040005AA RID: 1450
		private readonly Func<TElement, TKey> _keySelector;

		// Token: 0x040005AB RID: 1451
		private readonly IComparer<TKey> _comparer;

		// Token: 0x040005AC RID: 1452
		private readonly bool _descending;
	}
}
