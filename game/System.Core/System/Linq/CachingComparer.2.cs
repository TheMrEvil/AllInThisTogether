using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000E1 RID: 225
	internal class CachingComparer<TElement, TKey> : CachingComparer<TElement>
	{
		// Token: 0x060007F9 RID: 2041 RVA: 0x0001BFBC File Offset: 0x0001A1BC
		public CachingComparer(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
		{
			this._keySelector = keySelector;
			this._comparer = comparer;
			this._descending = descending;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		internal override int Compare(TElement element, bool cacheLower)
		{
			TKey tkey = this._keySelector(element);
			int num = this._descending ? this._comparer.Compare(this._lastKey, tkey) : this._comparer.Compare(tkey, this._lastKey);
			if (cacheLower == num < 0)
			{
				this._lastKey = tkey;
			}
			return num;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001C034 File Offset: 0x0001A234
		internal override void SetElement(TElement element)
		{
			this._lastKey = this._keySelector(element);
		}

		// Token: 0x040005AD RID: 1453
		protected readonly Func<TElement, TKey> _keySelector;

		// Token: 0x040005AE RID: 1454
		protected readonly IComparer<TKey> _comparer;

		// Token: 0x040005AF RID: 1455
		protected readonly bool _descending;

		// Token: 0x040005B0 RID: 1456
		protected TKey _lastKey;
	}
}
