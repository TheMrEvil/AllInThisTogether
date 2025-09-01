using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000E2 RID: 226
	internal sealed class CachingComparerWithChild<TElement, TKey> : CachingComparer<TElement, TKey>
	{
		// Token: 0x060007FC RID: 2044 RVA: 0x0001C048 File Offset: 0x0001A248
		public CachingComparerWithChild(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending, CachingComparer<TElement> child) : base(keySelector, comparer, descending)
		{
			this._child = child;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001C05C File Offset: 0x0001A25C
		internal override int Compare(TElement element, bool cacheLower)
		{
			TKey tkey = this._keySelector(element);
			int num = this._descending ? this._comparer.Compare(this._lastKey, tkey) : this._comparer.Compare(tkey, this._lastKey);
			if (num == 0)
			{
				return this._child.Compare(element, cacheLower);
			}
			if (cacheLower == num < 0)
			{
				this._lastKey = tkey;
				this._child.SetElement(element);
			}
			return num;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001C0D1 File Offset: 0x0001A2D1
		internal override void SetElement(TElement element)
		{
			base.SetElement(element);
			this._child.SetElement(element);
		}

		// Token: 0x040005B1 RID: 1457
		private readonly CachingComparer<TElement> _child;
	}
}
