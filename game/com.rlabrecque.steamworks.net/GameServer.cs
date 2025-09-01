using System;

namespace Steamworks
{
	// Token: 0x0200018E RID: 398
	public static class GameServer
	{
		// Token: 0x060008FA RID: 2298 RVA: 0x0000D674 File Offset: 0x0000B874
		public static bool Init(uint unIP, ushort usGamePort, ushort usQueryPort, EServerMode eServerMode, string pchVersionString)
		{
			InteropHelp.TestIfPlatformSupported();
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchVersionString))
			{
				flag = NativeMethods.SteamInternal_GameServer_Init(unIP, 0, usGamePort, usQueryPort, eServerMode, utf8StringHandle);
			}
			if (flag)
			{
				flag = CSteamGameServerAPIContext.Init();
			}
			if (flag)
			{
				CallbackDispatcher.Initialize();
			}
			return flag;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0000D6C8 File Offset: 0x0000B8C8
		public static void Shutdown()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamGameServer_Shutdown();
			CSteamGameServerAPIContext.Clear();
			CallbackDispatcher.Shutdown();
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0000D6DE File Offset: 0x0000B8DE
		public static void RunCallbacks()
		{
			CallbackDispatcher.RunFrame(true);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0000D6E6 File Offset: 0x0000B8E6
		public static void ReleaseCurrentThreadMemory()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamGameServer_ReleaseCurrentThreadMemory();
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0000D6F2 File Offset: 0x0000B8F2
		public static bool BSecure()
		{
			InteropHelp.TestIfPlatformSupported();
			return NativeMethods.SteamGameServer_BSecure();
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0000D6FE File Offset: 0x0000B8FE
		public static CSteamID GetSteamID()
		{
			InteropHelp.TestIfPlatformSupported();
			return (CSteamID)NativeMethods.SteamGameServer_GetSteamID();
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0000D70F File Offset: 0x0000B90F
		public static HSteamPipe GetHSteamPipe()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamPipe)NativeMethods.SteamGameServer_GetHSteamPipe();
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0000D720 File Offset: 0x0000B920
		public static HSteamUser GetHSteamUser()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamUser)NativeMethods.SteamGameServer_GetHSteamUser();
		}
	}
}
