using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000021 RID: 33
	[AttributeUsage(AttributeTargets.Method)]
	[VisibleToOtherModules]
	internal class ThreadSafeAttribute : NativeMethodAttribute
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00002533 File Offset: 0x00000733
		public ThreadSafeAttribute()
		{
			base.IsThreadSafe = true;
		}
	}
}
