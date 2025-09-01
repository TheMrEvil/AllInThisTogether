using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000014 RID: 20
	internal class ISteamGameServerStats : SteamInterface
	{
		// Token: 0x06000213 RID: 531 RVA: 0x00005917 File Offset: 0x00003B17
		internal ISteamGameServerStats(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x06000214 RID: 532
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamGameServerStats_v001();

		// Token: 0x06000215 RID: 533 RVA: 0x00005929 File Offset: 0x00003B29
		public override IntPtr GetServerInterfacePointer()
		{
			return ISteamGameServerStats.SteamAPI_SteamGameServerStats_v001();
		}

		// Token: 0x06000216 RID: 534
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_RequestUserStats")]
		private static extern SteamAPICall_t _RequestUserStats(IntPtr self, SteamId steamIDUser);

		// Token: 0x06000217 RID: 535 RVA: 0x00005930 File Offset: 0x00003B30
		internal CallResult<GSStatsReceived_t> RequestUserStats(SteamId steamIDUser)
		{
			SteamAPICall_t call = ISteamGameServerStats._RequestUserStats(this.Self, steamIDUser);
			return new CallResult<GSStatsReceived_t>(call, base.IsServer);
		}

		// Token: 0x06000218 RID: 536
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_GetUserStatInt32")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetUserStat(IntPtr self, SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, ref int pData);

		// Token: 0x06000219 RID: 537 RVA: 0x0000595C File Offset: 0x00003B5C
		internal bool GetUserStat(SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, ref int pData)
		{
			return ISteamGameServerStats._GetUserStat(this.Self, steamIDUser, pchName, ref pData);
		}

		// Token: 0x0600021A RID: 538
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_GetUserStatFloat")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetUserStat(IntPtr self, SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, ref float pData);

		// Token: 0x0600021B RID: 539 RVA: 0x00005980 File Offset: 0x00003B80
		internal bool GetUserStat(SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, ref float pData)
		{
			return ISteamGameServerStats._GetUserStat(this.Self, steamIDUser, pchName, ref pData);
		}

		// Token: 0x0600021C RID: 540
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_GetUserAchievement")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetUserAchievement(IntPtr self, SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, [MarshalAs(UnmanagedType.U1)] ref bool pbAchieved);

		// Token: 0x0600021D RID: 541 RVA: 0x000059A4 File Offset: 0x00003BA4
		internal bool GetUserAchievement(SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, [MarshalAs(UnmanagedType.U1)] ref bool pbAchieved)
		{
			return ISteamGameServerStats._GetUserAchievement(this.Self, steamIDUser, pchName, ref pbAchieved);
		}

		// Token: 0x0600021E RID: 542
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_SetUserStatInt32")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetUserStat(IntPtr self, SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, int nData);

		// Token: 0x0600021F RID: 543 RVA: 0x000059C8 File Offset: 0x00003BC8
		internal bool SetUserStat(SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, int nData)
		{
			return ISteamGameServerStats._SetUserStat(this.Self, steamIDUser, pchName, nData);
		}

		// Token: 0x06000220 RID: 544
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_SetUserStatFloat")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetUserStat(IntPtr self, SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, float fData);

		// Token: 0x06000221 RID: 545 RVA: 0x000059EC File Offset: 0x00003BEC
		internal bool SetUserStat(SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, float fData)
		{
			return ISteamGameServerStats._SetUserStat(this.Self, steamIDUser, pchName, fData);
		}

		// Token: 0x06000222 RID: 546
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_UpdateUserAvgRateStat")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _UpdateUserAvgRateStat(IntPtr self, SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, float flCountThisSession, double dSessionLength);

		// Token: 0x06000223 RID: 547 RVA: 0x00005A10 File Offset: 0x00003C10
		internal bool UpdateUserAvgRateStat(SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, float flCountThisSession, double dSessionLength)
		{
			return ISteamGameServerStats._UpdateUserAvgRateStat(this.Self, steamIDUser, pchName, flCountThisSession, dSessionLength);
		}

		// Token: 0x06000224 RID: 548
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_SetUserAchievement")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetUserAchievement(IntPtr self, SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName);

		// Token: 0x06000225 RID: 549 RVA: 0x00005A34 File Offset: 0x00003C34
		internal bool SetUserAchievement(SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName)
		{
			return ISteamGameServerStats._SetUserAchievement(this.Self, steamIDUser, pchName);
		}

		// Token: 0x06000226 RID: 550
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_ClearUserAchievement")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ClearUserAchievement(IntPtr self, SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName);

		// Token: 0x06000227 RID: 551 RVA: 0x00005A58 File Offset: 0x00003C58
		internal bool ClearUserAchievement(SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName)
		{
			return ISteamGameServerStats._ClearUserAchievement(this.Self, steamIDUser, pchName);
		}

		// Token: 0x06000228 RID: 552
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_StoreUserStats")]
		private static extern SteamAPICall_t _StoreUserStats(IntPtr self, SteamId steamIDUser);

		// Token: 0x06000229 RID: 553 RVA: 0x00005A7C File Offset: 0x00003C7C
		internal CallResult<GSStatsStored_t> StoreUserStats(SteamId steamIDUser)
		{
			SteamAPICall_t call = ISteamGameServerStats._StoreUserStats(this.Self, steamIDUser);
			return new CallResult<GSStatsStored_t>(call, base.IsServer);
		}
	}
}
