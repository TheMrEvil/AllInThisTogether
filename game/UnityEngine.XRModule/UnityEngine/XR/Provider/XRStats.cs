using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.XR.Provider
{
	// Token: 0x02000034 RID: 52
	public static class XRStats
	{
		// Token: 0x0600017B RID: 379 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public static bool TryGetStat(IntegratedSubsystem xrSubsystem, string tag, out float value)
		{
			return XRStats.TryGetStat_Internal(xrSubsystem.m_Ptr, tag, out value);
		}

		// Token: 0x0600017C RID: 380
		[NativeConditional("ENABLE_XR")]
		[NativeHeader("Modules/XR/Stats/XRStats.h")]
		[NativeMethod("TryGetStatByName_Internal")]
		[StaticAccessor("XRStats::Get()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetStat_Internal(IntPtr ptr, string tag, out float value);
	}
}
