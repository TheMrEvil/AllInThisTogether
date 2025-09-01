using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Analytics
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/UnityAnalyticsCommon/Public/UnityAnalyticsCommon.h")]
	[StructLayout(LayoutKind.Sequential)]
	public static class AnalyticsCommon
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		// (set) Token: 0x06000002 RID: 2
		[StaticAccessor("GetUnityAnalyticsCommon()", StaticAccessorType.Dot)]
		private static extern bool ugsAnalyticsEnabledInternal { [NativeMethod("UGSAnalyticsUserOptStatus")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeMethod("SetUGSAnalyticsUserOptStatus")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002067 File Offset: 0x00000267
		public static bool ugsAnalyticsEnabled
		{
			get
			{
				return AnalyticsCommon.ugsAnalyticsEnabledInternal;
			}
			set
			{
				AnalyticsCommon.ugsAnalyticsEnabledInternal = value;
			}
		}
	}
}
