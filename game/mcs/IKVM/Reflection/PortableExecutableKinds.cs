using System;

namespace IKVM.Reflection
{
	// Token: 0x0200001D RID: 29
	[Flags]
	public enum PortableExecutableKinds
	{
		// Token: 0x040000C0 RID: 192
		NotAPortableExecutableImage = 0,
		// Token: 0x040000C1 RID: 193
		ILOnly = 1,
		// Token: 0x040000C2 RID: 194
		Required32Bit = 2,
		// Token: 0x040000C3 RID: 195
		PE32Plus = 4,
		// Token: 0x040000C4 RID: 196
		Unmanaged32Bit = 8,
		// Token: 0x040000C5 RID: 197
		Preferred32Bit = 16
	}
}
