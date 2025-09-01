using System;

namespace IKVM.Reflection
{
	// Token: 0x02000014 RID: 20
	[Flags]
	public enum CallingConventions
	{
		// Token: 0x04000053 RID: 83
		Standard = 1,
		// Token: 0x04000054 RID: 84
		VarArgs = 2,
		// Token: 0x04000055 RID: 85
		Any = 3,
		// Token: 0x04000056 RID: 86
		HasThis = 32,
		// Token: 0x04000057 RID: 87
		ExplicitThis = 64
	}
}
