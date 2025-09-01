using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020000F6 RID: 246
	internal class ParallelEnumerableWrapper : ParallelQuery<object>
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x0001D11C File Offset: 0x0001B31C
		internal ParallelEnumerableWrapper(IEnumerable source) : base(QuerySettings.Empty)
		{
			this._source = source;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001D130 File Offset: 0x0001B330
		internal override IEnumerator GetEnumeratorUntyped()
		{
			return this._source.GetEnumerator();
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001D13D File Offset: 0x0001B33D
		public override IEnumerator<object> GetEnumerator()
		{
			return new EnumerableWrapperWeakToStrong(this._source).GetEnumerator();
		}

		// Token: 0x040005DC RID: 1500
		private readonly IEnumerable _source;
	}
}
