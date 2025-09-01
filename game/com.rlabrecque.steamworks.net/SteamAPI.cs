using System;

namespace Steamworks
{
	// Token: 0x0200018D RID: 397
	public static class SteamAPI
	{
		// Token: 0x060008F2 RID: 2290 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public static bool Init()
		{
			InteropHelp.TestIfPlatformSupported();
			bool flag = NativeMethods.SteamAPI_Init();
			if (flag)
			{
				flag = CSteamAPIContext.Init();
			}
			if (flag)
			{
				CallbackDispatcher.Initialize();
			}
			return flag;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0000D60E File Offset: 0x0000B80E
		public static void Shutdown()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamAPI_Shutdown();
			CSteamAPIContext.Clear();
			CallbackDispatcher.Shutdown();
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0000D624 File Offset: 0x0000B824
		public static bool RestartAppIfNecessary(AppId_t unOwnAppID)
		{
			InteropHelp.TestIfPlatformSupported();
			return NativeMethods.SteamAPI_RestartAppIfNecessary(unOwnAppID);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0000D631 File Offset: 0x0000B831
		public static void ReleaseCurrentThreadMemory()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamAPI_ReleaseCurrentThreadMemory();
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0000D63D File Offset: 0x0000B83D
		public static void RunCallbacks()
		{
			CallbackDispatcher.RunFrame(false);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0000D645 File Offset: 0x0000B845
		public static bool IsSteamRunning()
		{
			InteropHelp.TestIfPlatformSupported();
			return NativeMethods.SteamAPI_IsSteamRunning();
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0000D651 File Offset: 0x0000B851
		public static HSteamPipe GetHSteamPipe()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamPipe)NativeMethods.SteamAPI_GetHSteamPipe();
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0000D662 File Offset: 0x0000B862
		public static HSteamUser GetHSteamUser()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamUser)NativeMethods.SteamAPI_GetHSteamUser();
		}
	}
}
