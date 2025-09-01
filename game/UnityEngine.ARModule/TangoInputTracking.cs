using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.XR.Tango
{
	// Token: 0x02000004 RID: 4
	[NativeConditional("PLATFORM_ANDROID")]
	[NativeHeader("Modules/AR/ARCore/ARCoreScriptApi.h")]
	internal static class TangoInputTracking
	{
		// Token: 0x06000003 RID: 3
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_TryGetPoseAtTime(out PoseData pose);

		// Token: 0x06000004 RID: 4 RVA: 0x000020B0 File Offset: 0x000002B0
		internal static bool TryGetPoseAtTime(out PoseData pose)
		{
			return TangoInputTracking.Internal_TryGetPoseAtTime(out pose);
		}
	}
}
