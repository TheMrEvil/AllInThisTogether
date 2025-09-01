using System;

namespace System.Linq.Parallel
{
	// Token: 0x020000F8 RID: 248
	[Flags]
	internal enum QueryAggregationOptions
	{
		// Token: 0x040005DF RID: 1503
		None = 0,
		// Token: 0x040005E0 RID: 1504
		Associative = 1,
		// Token: 0x040005E1 RID: 1505
		Commutative = 2,
		// Token: 0x040005E2 RID: 1506
		AssociativeCommutative = 3
	}
}
