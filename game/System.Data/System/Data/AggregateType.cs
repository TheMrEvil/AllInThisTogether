using System;

namespace System.Data
{
	// Token: 0x020000A1 RID: 161
	internal enum AggregateType
	{
		// Token: 0x04000755 RID: 1877
		None,
		// Token: 0x04000756 RID: 1878
		Sum = 4,
		// Token: 0x04000757 RID: 1879
		Mean,
		// Token: 0x04000758 RID: 1880
		Min,
		// Token: 0x04000759 RID: 1881
		Max,
		// Token: 0x0400075A RID: 1882
		First,
		// Token: 0x0400075B RID: 1883
		Count,
		// Token: 0x0400075C RID: 1884
		Var,
		// Token: 0x0400075D RID: 1885
		StDev
	}
}
