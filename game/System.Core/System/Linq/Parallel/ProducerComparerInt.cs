using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200010C RID: 268
	internal class ProducerComparerInt : IComparer<Producer<int>>
	{
		// Token: 0x060008C6 RID: 2246 RVA: 0x0001E3AC File Offset: 0x0001C5AC
		public int Compare(Producer<int> x, Producer<int> y)
		{
			return y.MaxKey - x.MaxKey;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00002162 File Offset: 0x00000362
		public ProducerComparerInt()
		{
		}
	}
}
