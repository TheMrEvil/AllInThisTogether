using System;

namespace IKVM.Reflection
{
	// Token: 0x0200001F RID: 31
	[Flags]
	public enum PropertyAttributes
	{
		// Token: 0x040000CE RID: 206
		None = 0,
		// Token: 0x040000CF RID: 207
		SpecialName = 512,
		// Token: 0x040000D0 RID: 208
		RTSpecialName = 1024,
		// Token: 0x040000D1 RID: 209
		HasDefault = 4096
	}
}
