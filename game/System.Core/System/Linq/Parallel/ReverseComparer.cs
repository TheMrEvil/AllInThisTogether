using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001FA RID: 506
	internal class ReverseComparer<T> : IComparer<T>
	{
		// Token: 0x06000C64 RID: 3172 RVA: 0x0002B391 File Offset: 0x00029591
		internal ReverseComparer(IComparer<T> comparer)
		{
			this._comparer = comparer;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002B3A0 File Offset: 0x000295A0
		public int Compare(T x, T y)
		{
			return this._comparer.Compare(y, x);
		}

		// Token: 0x040008C1 RID: 2241
		private IComparer<T> _comparer;
	}
}
