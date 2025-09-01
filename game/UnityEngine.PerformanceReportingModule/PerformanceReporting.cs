using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Analytics
{
	// Token: 0x02000002 RID: 2
	[StaticAccessor("GetPerformanceReportingManager()", StaticAccessorType.Dot)]
	[NativeHeader("Modules/PerformanceReporting/PerformanceReportingManager.h")]
	public static class PerformanceReporting
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		// (set) Token: 0x06000002 RID: 2
		public static extern bool enabled { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3
		public static extern long graphicsInitializationFinishTime { [NativeMethod("GetGfxDoneTime")] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
