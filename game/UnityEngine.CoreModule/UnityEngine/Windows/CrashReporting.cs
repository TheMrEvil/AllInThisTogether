using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Windows
{
	// Token: 0x02000288 RID: 648
	public static class CrashReporting
	{
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001C2B RID: 7211
		public static extern string crashReportFolder { [NativeHeader("PlatformDependent/WinPlayer/Bindings/CrashReportingBindings.h")] [ThreadSafe] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
