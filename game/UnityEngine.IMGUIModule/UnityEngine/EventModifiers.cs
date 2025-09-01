using System;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	[Flags]
	public enum EventModifiers
	{
		// Token: 0x04000043 RID: 67
		None = 0,
		// Token: 0x04000044 RID: 68
		Shift = 1,
		// Token: 0x04000045 RID: 69
		Control = 2,
		// Token: 0x04000046 RID: 70
		Alt = 4,
		// Token: 0x04000047 RID: 71
		Command = 8,
		// Token: 0x04000048 RID: 72
		Numeric = 16,
		// Token: 0x04000049 RID: 73
		CapsLock = 32,
		// Token: 0x0400004A RID: 74
		FunctionKey = 64
	}
}
