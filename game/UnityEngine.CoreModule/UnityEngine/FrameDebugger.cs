using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000258 RID: 600
	[StaticAccessor("FrameDebugger", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Profiler/PerformanceTools/FrameDebugger.h")]
	public static class FrameDebugger
	{
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x0002A1EE File Offset: 0x000283EE
		public static bool enabled
		{
			get
			{
				return FrameDebugger.IsLocalEnabled() || FrameDebugger.IsRemoteEnabled();
			}
		}

		// Token: 0x06001A03 RID: 6659
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsLocalEnabled();

		// Token: 0x06001A04 RID: 6660
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsRemoteEnabled();
	}
}
