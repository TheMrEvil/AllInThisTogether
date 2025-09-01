using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001F9 RID: 505
	internal sealed class PairComparer<T, U> : IComparer<Pair<T, U>>
	{
		// Token: 0x06000C62 RID: 3170 RVA: 0x0002B336 File Offset: 0x00029536
		public PairComparer(IComparer<T> comparer1, IComparer<U> comparer2)
		{
			this._comparer1 = comparer1;
			this._comparer2 = comparer2;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002B34C File Offset: 0x0002954C
		public int Compare(Pair<T, U> x, Pair<T, U> y)
		{
			int num = this._comparer1.Compare(x.First, y.First);
			if (num != 0)
			{
				return num;
			}
			return this._comparer2.Compare(x.Second, y.Second);
		}

		// Token: 0x040008BF RID: 2239
		private readonly IComparer<T> _comparer1;

		// Token: 0x040008C0 RID: 2240
		private readonly IComparer<U> _comparer2;
	}
}
