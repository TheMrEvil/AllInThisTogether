using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000206 RID: 518
	internal struct WrapperEqualityComparer<T> : IEqualityComparer<Wrapper<T>>
	{
		// Token: 0x06000C81 RID: 3201 RVA: 0x0002BD53 File Offset: 0x00029F53
		internal WrapperEqualityComparer(IEqualityComparer<T> comparer)
		{
			if (comparer == null)
			{
				this._comparer = EqualityComparer<T>.Default;
				return;
			}
			this._comparer = comparer;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0002BD6B File Offset: 0x00029F6B
		public bool Equals(Wrapper<T> x, Wrapper<T> y)
		{
			return this._comparer.Equals(x.Value, y.Value);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0002BD84 File Offset: 0x00029F84
		public int GetHashCode(Wrapper<T> x)
		{
			return this._comparer.GetHashCode(x.Value);
		}

		// Token: 0x040008D3 RID: 2259
		private IEqualityComparer<T> _comparer;
	}
}
