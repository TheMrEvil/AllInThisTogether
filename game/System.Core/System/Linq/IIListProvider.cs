using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000E5 RID: 229
	internal interface IIListProvider<TElement> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06000810 RID: 2064
		TElement[] ToArray();

		// Token: 0x06000811 RID: 2065
		List<TElement> ToList();

		// Token: 0x06000812 RID: 2066
		int GetCount(bool onlyIfCheap);
	}
}
