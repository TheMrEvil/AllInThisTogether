using System;

namespace IKVM.Reflection
{
	// Token: 0x02000011 RID: 17
	[Flags]
	public enum AssemblyNameFlags
	{
		// Token: 0x04000041 RID: 65
		None = 0,
		// Token: 0x04000042 RID: 66
		PublicKey = 1,
		// Token: 0x04000043 RID: 67
		Retargetable = 256,
		// Token: 0x04000044 RID: 68
		EnableJITcompileOptimizer = 16384,
		// Token: 0x04000045 RID: 69
		EnableJITcompileTracking = 32768
	}
}
