using System;

namespace IKVM.Reflection
{
	// Token: 0x02000023 RID: 35
	[Flags]
	public enum DllCharacteristics
	{
		// Token: 0x040000FB RID: 251
		HighEntropyVA = 32,
		// Token: 0x040000FC RID: 252
		DynamicBase = 64,
		// Token: 0x040000FD RID: 253
		NoSEH = 1024,
		// Token: 0x040000FE RID: 254
		NXCompat = 256,
		// Token: 0x040000FF RID: 255
		AppContainer = 4096,
		// Token: 0x04000100 RID: 256
		TerminalServerAware = 32768
	}
}
