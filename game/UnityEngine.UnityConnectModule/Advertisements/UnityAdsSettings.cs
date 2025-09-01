using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Advertisements
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/UnityConnect/UnityAds/UnityAdsSettings.h")]
	internal static class UnityAdsSettings
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		// (set) Token: 0x06000002 RID: 2
		[StaticAccessor("GetUnityAdsSettings()", StaticAccessorType.Dot)]
		public static extern bool enabled { [ThreadSafe] [MethodImpl(MethodImplOptions.InternalCall)] get; [ThreadSafe] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000003 RID: 3 RVA: 0x00002050 File Offset: 0x00000250
		[Obsolete("warning No longer supported and will always return true")]
		public static bool IsPlatformEnabled(RuntimePlatform platform)
		{
			return true;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002063 File Offset: 0x00000263
		[Obsolete("warning No longer supported and will do nothing")]
		public static void SetPlatformEnabled(RuntimePlatform platform, bool value)
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5
		// (set) Token: 0x06000006 RID: 6
		[StaticAccessor("GetUnityAdsSettings()", StaticAccessorType.Dot)]
		public static extern bool initializeOnStartup { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7
		// (set) Token: 0x06000008 RID: 8
		[StaticAccessor("GetUnityAdsSettings()", StaticAccessorType.Dot)]
		public static extern bool testMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000009 RID: 9
		[StaticAccessor("GetUnityAdsSettings()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetGameId(RuntimePlatform platform);

		// Token: 0x0600000A RID: 10
		[StaticAccessor("GetUnityAdsSettings()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGameId(RuntimePlatform platform, string gameId);
	}
}
