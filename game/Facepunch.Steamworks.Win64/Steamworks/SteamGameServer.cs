using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000009 RID: 9
	internal static class SteamGameServer
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002A18 File Offset: 0x00000C18
		internal static void RunCallbacks()
		{
			SteamGameServer.Native.SteamGameServer_RunCallbacks();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002A21 File Offset: 0x00000C21
		internal static void Shutdown()
		{
			SteamGameServer.Native.SteamGameServer_Shutdown();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A2C File Offset: 0x00000C2C
		internal static HSteamPipe GetHSteamPipe()
		{
			return SteamGameServer.Native.SteamGameServer_GetHSteamPipe();
		}

		// Token: 0x02000210 RID: 528
		internal static class Native
		{
			// Token: 0x0600107F RID: 4223
			[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
			public static extern void SteamGameServer_RunCallbacks();

			// Token: 0x06001080 RID: 4224
			[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
			public static extern void SteamGameServer_Shutdown();

			// Token: 0x06001081 RID: 4225
			[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
			public static extern HSteamPipe SteamGameServer_GetHSteamPipe();
		}
	}
}
