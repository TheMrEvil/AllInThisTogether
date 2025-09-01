using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000008 RID: 8
	internal static class SteamAPI
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000029C8 File Offset: 0x00000BC8
		internal static bool Init()
		{
			return SteamAPI.Native.SteamAPI_Init();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000029DF File Offset: 0x00000BDF
		internal static void Shutdown()
		{
			SteamAPI.Native.SteamAPI_Shutdown();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000029E8 File Offset: 0x00000BE8
		internal static HSteamPipe GetHSteamPipe()
		{
			return SteamAPI.Native.SteamAPI_GetHSteamPipe();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A00 File Offset: 0x00000C00
		internal static bool RestartAppIfNecessary(uint unOwnAppID)
		{
			return SteamAPI.Native.SteamAPI_RestartAppIfNecessary(unOwnAppID);
		}

		// Token: 0x0200020F RID: 527
		internal static class Native
		{
			// Token: 0x0600107B RID: 4219
			[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
			[return: MarshalAs(UnmanagedType.I1)]
			public static extern bool SteamAPI_Init();

			// Token: 0x0600107C RID: 4220
			[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
			public static extern void SteamAPI_Shutdown();

			// Token: 0x0600107D RID: 4221
			[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
			public static extern HSteamPipe SteamAPI_GetHSteamPipe();

			// Token: 0x0600107E RID: 4222
			[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
			[return: MarshalAs(UnmanagedType.I1)]
			public static extern bool SteamAPI_RestartAppIfNecessary(uint unOwnAppID);
		}
	}
}
