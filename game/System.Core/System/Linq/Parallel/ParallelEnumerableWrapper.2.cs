using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020000F7 RID: 247
	internal class ParallelEnumerableWrapper<T> : ParallelQuery<T>
	{
		// Token: 0x0600087C RID: 2172 RVA: 0x0001D14F File Offset: 0x0001B34F
		internal ParallelEnumerableWrapper(IEnumerable<T> wrappedEnumerable) : base(QuerySettings.Empty)
		{
			this._wrappedEnumerable = wrappedEnumerable;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0001D163 File Offset: 0x0001B363
		internal IEnumerable<T> WrappedEnumerable
		{
			get
			{
				return this._wrappedEnumerable;
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001D16B File Offset: 0x0001B36B
		public override IEnumerator<T> GetEnumerator()
		{
			return this._wrappedEnumerable.GetEnumerator();
		}

		// Token: 0x040005DD RID: 1501
		private readonly IEnumerable<T> _wrappedEnumerable;
	}
}
