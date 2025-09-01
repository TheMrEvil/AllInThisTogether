using System;

namespace IKVM.Reflection
{
	// Token: 0x02000013 RID: 19
	[Flags]
	public enum BindingFlags
	{
		// Token: 0x0400004A RID: 74
		Default = 0,
		// Token: 0x0400004B RID: 75
		IgnoreCase = 1,
		// Token: 0x0400004C RID: 76
		DeclaredOnly = 2,
		// Token: 0x0400004D RID: 77
		Instance = 4,
		// Token: 0x0400004E RID: 78
		Static = 8,
		// Token: 0x0400004F RID: 79
		Public = 16,
		// Token: 0x04000050 RID: 80
		NonPublic = 32,
		// Token: 0x04000051 RID: 81
		FlattenHierarchy = 64
	}
}
