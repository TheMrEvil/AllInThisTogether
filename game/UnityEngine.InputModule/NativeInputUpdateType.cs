using System;

namespace UnityEngineInternal.Input
{
	// Token: 0x02000006 RID: 6
	[Flags]
	internal enum NativeInputUpdateType
	{
		// Token: 0x04000013 RID: 19
		Dynamic = 1,
		// Token: 0x04000014 RID: 20
		Fixed = 2,
		// Token: 0x04000015 RID: 21
		BeforeRender = 4,
		// Token: 0x04000016 RID: 22
		Editor = 8,
		// Token: 0x04000017 RID: 23
		IgnoreFocus = -2147483648
	}
}
