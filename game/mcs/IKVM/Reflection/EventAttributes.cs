using System;

namespace IKVM.Reflection
{
	// Token: 0x02000015 RID: 21
	[Flags]
	public enum EventAttributes
	{
		// Token: 0x04000059 RID: 89
		None = 0,
		// Token: 0x0400005A RID: 90
		SpecialName = 512,
		// Token: 0x0400005B RID: 91
		RTSpecialName = 1024,
		// Token: 0x0400005C RID: 92
		ReservedMask = 1024
	}
}
