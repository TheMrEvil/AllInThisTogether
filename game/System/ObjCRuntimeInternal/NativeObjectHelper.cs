using System;

namespace ObjCRuntimeInternal
{
	// Token: 0x02000116 RID: 278
	internal static class NativeObjectHelper
	{
		// Token: 0x060006AB RID: 1707 RVA: 0x0001259E File Offset: 0x0001079E
		public static IntPtr GetHandle(this INativeObject self)
		{
			if (self != null)
			{
				return self.Handle;
			}
			return IntPtr.Zero;
		}
	}
}
