using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000E6 RID: 230
	internal interface IPartition<TElement> : IIListProvider<TElement>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06000813 RID: 2067
		IPartition<TElement> Skip(int count);

		// Token: 0x06000814 RID: 2068
		IPartition<TElement> Take(int count);

		// Token: 0x06000815 RID: 2069
		TElement TryGetElementAt(int index, out bool found);

		// Token: 0x06000816 RID: 2070
		TElement TryGetFirst(out bool found);

		// Token: 0x06000817 RID: 2071
		TElement TryGetLast(out bool found);
	}
}
