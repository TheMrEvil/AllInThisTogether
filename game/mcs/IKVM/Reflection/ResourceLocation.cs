using System;

namespace IKVM.Reflection
{
	// Token: 0x02000021 RID: 33
	[Flags]
	public enum ResourceLocation
	{
		// Token: 0x040000D6 RID: 214
		Embedded = 1,
		// Token: 0x040000D7 RID: 215
		ContainedInAnotherAssembly = 2,
		// Token: 0x040000D8 RID: 216
		ContainedInManifestFile = 4
	}
}
