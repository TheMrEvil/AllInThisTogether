using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200002E RID: 46
	internal class ISteamUserStats : SteamInterface
	{
		// Token: 0x06000665 RID: 1637 RVA: 0x00009DBA File Offset: 0x00007FBA
		internal ISteamUserStats(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x06000666 RID: 1638
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamUserStats_v011();

		// Token: 0x06000667 RID: 1639 RVA: 0x00009DCC File Offset: 0x00007FCC
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamUserStats.SteamAPI_SteamUserStats_v011();
		}

		// Token: 0x06000668 RID: 1640
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_RequestCurrentStats")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _RequestCurrentStats(IntPtr self);

		// Token: 0x06000669 RID: 1641 RVA: 0x00009DD4 File Offset: 0x00007FD4
		internal bool RequestCurrentStats()
		{
			return ISteamUserStats._RequestCurrentStats(this.Self);
		}

		// Token: 0x0600066A RID: 1642
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetStatInt32")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetStat(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, ref int pData);

		// Token: 0x0600066B RID: 1643 RVA: 0x00009DF4 File Offset: 0x00007FF4
		internal bool GetStat([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, ref int pData)
		{
			return ISteamUserStats._GetStat(this.Self, pchName, ref pData);
		}

		// Token: 0x0600066C RID: 1644
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetStatFloat")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetStat(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, ref float pData);

		// Token: 0x0600066D RID: 1645 RVA: 0x00009E18 File Offset: 0x00008018
		internal bool GetStat([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, ref float pData)
		{
			return ISteamUserStats._GetStat(this.Self, pchName, ref pData);
		}

		// Token: 0x0600066E RID: 1646
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_SetStatInt32")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetStat(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, int nData);

		// Token: 0x0600066F RID: 1647 RVA: 0x00009E3C File Offset: 0x0000803C
		internal bool SetStat([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, int nData)
		{
			return ISteamUserStats._SetStat(this.Self, pchName, nData);
		}

		// Token: 0x06000670 RID: 1648
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_SetStatFloat")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetStat(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, float fData);

		// Token: 0x06000671 RID: 1649 RVA: 0x00009E60 File Offset: 0x00008060
		internal bool SetStat([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, float fData)
		{
			return ISteamUserStats._SetStat(this.Self, pchName, fData);
		}

		// Token: 0x06000672 RID: 1650
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_UpdateAvgRateStat")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _UpdateAvgRateStat(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, float flCountThisSession, double dSessionLength);

		// Token: 0x06000673 RID: 1651 RVA: 0x00009E84 File Offset: 0x00008084
		internal bool UpdateAvgRateStat([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, float flCountThisSession, double dSessionLength)
		{
			return ISteamUserStats._UpdateAvgRateStat(this.Self, pchName, flCountThisSession, dSessionLength);
		}

		// Token: 0x06000674 RID: 1652
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievement")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetAchievement(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, [MarshalAs(UnmanagedType.U1)] ref bool pbAchieved);

		// Token: 0x06000675 RID: 1653 RVA: 0x00009EA8 File Offset: 0x000080A8
		internal bool GetAchievement([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, [MarshalAs(UnmanagedType.U1)] ref bool pbAchieved)
		{
			return ISteamUserStats._GetAchievement(this.Self, pchName, ref pbAchieved);
		}

		// Token: 0x06000676 RID: 1654
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_SetAchievement")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetAchievement(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName);

		// Token: 0x06000677 RID: 1655 RVA: 0x00009ECC File Offset: 0x000080CC
		internal bool SetAchievement([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName)
		{
			return ISteamUserStats._SetAchievement(this.Self, pchName);
		}

		// Token: 0x06000678 RID: 1656
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_ClearAchievement")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ClearAchievement(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName);

		// Token: 0x06000679 RID: 1657 RVA: 0x00009EEC File Offset: 0x000080EC
		internal bool ClearAchievement([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName)
		{
			return ISteamUserStats._ClearAchievement(this.Self, pchName);
		}

		// Token: 0x0600067A RID: 1658
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementAndUnlockTime")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetAchievementAndUnlockTime(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, [MarshalAs(UnmanagedType.U1)] ref bool pbAchieved, ref uint punUnlockTime);

		// Token: 0x0600067B RID: 1659 RVA: 0x00009F0C File Offset: 0x0000810C
		internal bool GetAchievementAndUnlockTime([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, [MarshalAs(UnmanagedType.U1)] ref bool pbAchieved, ref uint punUnlockTime)
		{
			return ISteamUserStats._GetAchievementAndUnlockTime(this.Self, pchName, ref pbAchieved, ref punUnlockTime);
		}

		// Token: 0x0600067C RID: 1660
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_StoreStats")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _StoreStats(IntPtr self);

		// Token: 0x0600067D RID: 1661 RVA: 0x00009F30 File Offset: 0x00008130
		internal bool StoreStats()
		{
			return ISteamUserStats._StoreStats(this.Self);
		}

		// Token: 0x0600067E RID: 1662
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementIcon")]
		private static extern int _GetAchievementIcon(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName);

		// Token: 0x0600067F RID: 1663 RVA: 0x00009F50 File Offset: 0x00008150
		internal int GetAchievementIcon([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName)
		{
			return ISteamUserStats._GetAchievementIcon(this.Self, pchName);
		}

		// Token: 0x06000680 RID: 1664
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementDisplayAttribute")]
		private static extern Utf8StringPointer _GetAchievementDisplayAttribute(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey);

		// Token: 0x06000681 RID: 1665 RVA: 0x00009F70 File Offset: 0x00008170
		internal string GetAchievementDisplayAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey)
		{
			Utf8StringPointer p = ISteamUserStats._GetAchievementDisplayAttribute(this.Self, pchName, pchKey);
			return p;
		}

		// Token: 0x06000682 RID: 1666
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_IndicateAchievementProgress")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IndicateAchievementProgress(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, uint nCurProgress, uint nMaxProgress);

		// Token: 0x06000683 RID: 1667 RVA: 0x00009F98 File Offset: 0x00008198
		internal bool IndicateAchievementProgress([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, uint nCurProgress, uint nMaxProgress)
		{
			return ISteamUserStats._IndicateAchievementProgress(this.Self, pchName, nCurProgress, nMaxProgress);
		}

		// Token: 0x06000684 RID: 1668
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetNumAchievements")]
		private static extern uint _GetNumAchievements(IntPtr self);

		// Token: 0x06000685 RID: 1669 RVA: 0x00009FBC File Offset: 0x000081BC
		internal uint GetNumAchievements()
		{
			return ISteamUserStats._GetNumAchievements(this.Self);
		}

		// Token: 0x06000686 RID: 1670
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementName")]
		private static extern Utf8StringPointer _GetAchievementName(IntPtr self, uint iAchievement);

		// Token: 0x06000687 RID: 1671 RVA: 0x00009FDC File Offset: 0x000081DC
		internal string GetAchievementName(uint iAchievement)
		{
			Utf8StringPointer p = ISteamUserStats._GetAchievementName(this.Self, iAchievement);
			return p;
		}

		// Token: 0x06000688 RID: 1672
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_RequestUserStats")]
		private static extern SteamAPICall_t _RequestUserStats(IntPtr self, SteamId steamIDUser);

		// Token: 0x06000689 RID: 1673 RVA: 0x0000A004 File Offset: 0x00008204
		internal CallResult<UserStatsReceived_t> RequestUserStats(SteamId steamIDUser)
		{
			SteamAPICall_t call = ISteamUserStats._RequestUserStats(this.Self, steamIDUser);
			return new CallResult<UserStatsReceived_t>(call, base.IsServer);
		}

		// Token: 0x0600068A RID: 1674
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetUserStatInt32")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetUserStat(IntPtr self, SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, ref int pData);

		// Token: 0x0600068B RID: 1675 RVA: 0x0000A030 File Offset: 0x00008230
		internal bool GetUserStat(SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, ref int pData)
		{
			return ISteamUserStats._GetUserStat(this.Self, steamIDUser, pchName, ref pData);
		}

		// Token: 0x0600068C RID: 1676
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetUserStatFloat")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetUserStat(IntPtr self, SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, ref float pData);

		// Token: 0x0600068D RID: 1677 RVA: 0x0000A054 File Offset: 0x00008254
		internal bool GetUserStat(SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, ref float pData)
		{
			return ISteamUserStats._GetUserStat(this.Self, steamIDUser, pchName, ref pData);
		}

		// Token: 0x0600068E RID: 1678
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetUserAchievement")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetUserAchievement(IntPtr self, SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, [MarshalAs(UnmanagedType.U1)] ref bool pbAchieved);

		// Token: 0x0600068F RID: 1679 RVA: 0x0000A078 File Offset: 0x00008278
		internal bool GetUserAchievement(SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, [MarshalAs(UnmanagedType.U1)] ref bool pbAchieved)
		{
			return ISteamUserStats._GetUserAchievement(this.Self, steamIDUser, pchName, ref pbAchieved);
		}

		// Token: 0x06000690 RID: 1680
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetUserAchievementAndUnlockTime")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetUserAchievementAndUnlockTime(IntPtr self, SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, [MarshalAs(UnmanagedType.U1)] ref bool pbAchieved, ref uint punUnlockTime);

		// Token: 0x06000691 RID: 1681 RVA: 0x0000A09C File Offset: 0x0000829C
		internal bool GetUserAchievementAndUnlockTime(SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, [MarshalAs(UnmanagedType.U1)] ref bool pbAchieved, ref uint punUnlockTime)
		{
			return ISteamUserStats._GetUserAchievementAndUnlockTime(this.Self, steamIDUser, pchName, ref pbAchieved, ref punUnlockTime);
		}

		// Token: 0x06000692 RID: 1682
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_ResetAllStats")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ResetAllStats(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bAchievementsToo);

		// Token: 0x06000693 RID: 1683 RVA: 0x0000A0C0 File Offset: 0x000082C0
		internal bool ResetAllStats([MarshalAs(UnmanagedType.U1)] bool bAchievementsToo)
		{
			return ISteamUserStats._ResetAllStats(this.Self, bAchievementsToo);
		}

		// Token: 0x06000694 RID: 1684
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_FindOrCreateLeaderboard")]
		private static extern SteamAPICall_t _FindOrCreateLeaderboard(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchLeaderboardName, LeaderboardSort eLeaderboardSortMethod, LeaderboardDisplay eLeaderboardDisplayType);

		// Token: 0x06000695 RID: 1685 RVA: 0x0000A0E0 File Offset: 0x000082E0
		internal CallResult<LeaderboardFindResult_t> FindOrCreateLeaderboard([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchLeaderboardName, LeaderboardSort eLeaderboardSortMethod, LeaderboardDisplay eLeaderboardDisplayType)
		{
			SteamAPICall_t call = ISteamUserStats._FindOrCreateLeaderboard(this.Self, pchLeaderboardName, eLeaderboardSortMethod, eLeaderboardDisplayType);
			return new CallResult<LeaderboardFindResult_t>(call, base.IsServer);
		}

		// Token: 0x06000696 RID: 1686
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_FindLeaderboard")]
		private static extern SteamAPICall_t _FindLeaderboard(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchLeaderboardName);

		// Token: 0x06000697 RID: 1687 RVA: 0x0000A110 File Offset: 0x00008310
		internal CallResult<LeaderboardFindResult_t> FindLeaderboard([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchLeaderboardName)
		{
			SteamAPICall_t call = ISteamUserStats._FindLeaderboard(this.Self, pchLeaderboardName);
			return new CallResult<LeaderboardFindResult_t>(call, base.IsServer);
		}

		// Token: 0x06000698 RID: 1688
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardName")]
		private static extern Utf8StringPointer _GetLeaderboardName(IntPtr self, SteamLeaderboard_t hSteamLeaderboard);

		// Token: 0x06000699 RID: 1689 RVA: 0x0000A13C File Offset: 0x0000833C
		internal string GetLeaderboardName(SteamLeaderboard_t hSteamLeaderboard)
		{
			Utf8StringPointer p = ISteamUserStats._GetLeaderboardName(this.Self, hSteamLeaderboard);
			return p;
		}

		// Token: 0x0600069A RID: 1690
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardEntryCount")]
		private static extern int _GetLeaderboardEntryCount(IntPtr self, SteamLeaderboard_t hSteamLeaderboard);

		// Token: 0x0600069B RID: 1691 RVA: 0x0000A164 File Offset: 0x00008364
		internal int GetLeaderboardEntryCount(SteamLeaderboard_t hSteamLeaderboard)
		{
			return ISteamUserStats._GetLeaderboardEntryCount(this.Self, hSteamLeaderboard);
		}

		// Token: 0x0600069C RID: 1692
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardSortMethod")]
		private static extern LeaderboardSort _GetLeaderboardSortMethod(IntPtr self, SteamLeaderboard_t hSteamLeaderboard);

		// Token: 0x0600069D RID: 1693 RVA: 0x0000A184 File Offset: 0x00008384
		internal LeaderboardSort GetLeaderboardSortMethod(SteamLeaderboard_t hSteamLeaderboard)
		{
			return ISteamUserStats._GetLeaderboardSortMethod(this.Self, hSteamLeaderboard);
		}

		// Token: 0x0600069E RID: 1694
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardDisplayType")]
		private static extern LeaderboardDisplay _GetLeaderboardDisplayType(IntPtr self, SteamLeaderboard_t hSteamLeaderboard);

		// Token: 0x0600069F RID: 1695 RVA: 0x0000A1A4 File Offset: 0x000083A4
		internal LeaderboardDisplay GetLeaderboardDisplayType(SteamLeaderboard_t hSteamLeaderboard)
		{
			return ISteamUserStats._GetLeaderboardDisplayType(this.Self, hSteamLeaderboard);
		}

		// Token: 0x060006A0 RID: 1696
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_DownloadLeaderboardEntries")]
		private static extern SteamAPICall_t _DownloadLeaderboardEntries(IntPtr self, SteamLeaderboard_t hSteamLeaderboard, LeaderboardDataRequest eLeaderboardDataRequest, int nRangeStart, int nRangeEnd);

		// Token: 0x060006A1 RID: 1697 RVA: 0x0000A1C4 File Offset: 0x000083C4
		internal CallResult<LeaderboardScoresDownloaded_t> DownloadLeaderboardEntries(SteamLeaderboard_t hSteamLeaderboard, LeaderboardDataRequest eLeaderboardDataRequest, int nRangeStart, int nRangeEnd)
		{
			SteamAPICall_t call = ISteamUserStats._DownloadLeaderboardEntries(this.Self, hSteamLeaderboard, eLeaderboardDataRequest, nRangeStart, nRangeEnd);
			return new CallResult<LeaderboardScoresDownloaded_t>(call, base.IsServer);
		}

		// Token: 0x060006A2 RID: 1698
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_DownloadLeaderboardEntriesForUsers")]
		private static extern SteamAPICall_t _DownloadLeaderboardEntriesForUsers(IntPtr self, SteamLeaderboard_t hSteamLeaderboard, [In] [Out] SteamId[] prgUsers, int cUsers);

		// Token: 0x060006A3 RID: 1699 RVA: 0x0000A1F4 File Offset: 0x000083F4
		internal CallResult<LeaderboardScoresDownloaded_t> DownloadLeaderboardEntriesForUsers(SteamLeaderboard_t hSteamLeaderboard, [In] [Out] SteamId[] prgUsers, int cUsers)
		{
			SteamAPICall_t call = ISteamUserStats._DownloadLeaderboardEntriesForUsers(this.Self, hSteamLeaderboard, prgUsers, cUsers);
			return new CallResult<LeaderboardScoresDownloaded_t>(call, base.IsServer);
		}

		// Token: 0x060006A4 RID: 1700
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetDownloadedLeaderboardEntry")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetDownloadedLeaderboardEntry(IntPtr self, SteamLeaderboardEntries_t hSteamLeaderboardEntries, int index, ref LeaderboardEntry_t pLeaderboardEntry, [In] [Out] int[] pDetails, int cDetailsMax);

		// Token: 0x060006A5 RID: 1701 RVA: 0x0000A224 File Offset: 0x00008424
		internal bool GetDownloadedLeaderboardEntry(SteamLeaderboardEntries_t hSteamLeaderboardEntries, int index, ref LeaderboardEntry_t pLeaderboardEntry, [In] [Out] int[] pDetails, int cDetailsMax)
		{
			return ISteamUserStats._GetDownloadedLeaderboardEntry(this.Self, hSteamLeaderboardEntries, index, ref pLeaderboardEntry, pDetails, cDetailsMax);
		}

		// Token: 0x060006A6 RID: 1702
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_UploadLeaderboardScore")]
		private static extern SteamAPICall_t _UploadLeaderboardScore(IntPtr self, SteamLeaderboard_t hSteamLeaderboard, LeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod, int nScore, [In] [Out] int[] pScoreDetails, int cScoreDetailsCount);

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000A24C File Offset: 0x0000844C
		internal CallResult<LeaderboardScoreUploaded_t> UploadLeaderboardScore(SteamLeaderboard_t hSteamLeaderboard, LeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod, int nScore, [In] [Out] int[] pScoreDetails, int cScoreDetailsCount)
		{
			SteamAPICall_t call = ISteamUserStats._UploadLeaderboardScore(this.Self, hSteamLeaderboard, eLeaderboardUploadScoreMethod, nScore, pScoreDetails, cScoreDetailsCount);
			return new CallResult<LeaderboardScoreUploaded_t>(call, base.IsServer);
		}

		// Token: 0x060006A8 RID: 1704
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_AttachLeaderboardUGC")]
		private static extern SteamAPICall_t _AttachLeaderboardUGC(IntPtr self, SteamLeaderboard_t hSteamLeaderboard, UGCHandle_t hUGC);

		// Token: 0x060006A9 RID: 1705 RVA: 0x0000A280 File Offset: 0x00008480
		internal CallResult<LeaderboardUGCSet_t> AttachLeaderboardUGC(SteamLeaderboard_t hSteamLeaderboard, UGCHandle_t hUGC)
		{
			SteamAPICall_t call = ISteamUserStats._AttachLeaderboardUGC(this.Self, hSteamLeaderboard, hUGC);
			return new CallResult<LeaderboardUGCSet_t>(call, base.IsServer);
		}

		// Token: 0x060006AA RID: 1706
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetNumberOfCurrentPlayers")]
		private static extern SteamAPICall_t _GetNumberOfCurrentPlayers(IntPtr self);

		// Token: 0x060006AB RID: 1707 RVA: 0x0000A2AC File Offset: 0x000084AC
		internal CallResult<NumberOfCurrentPlayers_t> GetNumberOfCurrentPlayers()
		{
			SteamAPICall_t call = ISteamUserStats._GetNumberOfCurrentPlayers(this.Self);
			return new CallResult<NumberOfCurrentPlayers_t>(call, base.IsServer);
		}

		// Token: 0x060006AC RID: 1708
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_RequestGlobalAchievementPercentages")]
		private static extern SteamAPICall_t _RequestGlobalAchievementPercentages(IntPtr self);

		// Token: 0x060006AD RID: 1709 RVA: 0x0000A2D8 File Offset: 0x000084D8
		internal CallResult<GlobalAchievementPercentagesReady_t> RequestGlobalAchievementPercentages()
		{
			SteamAPICall_t call = ISteamUserStats._RequestGlobalAchievementPercentages(this.Self);
			return new CallResult<GlobalAchievementPercentagesReady_t>(call, base.IsServer);
		}

		// Token: 0x060006AE RID: 1710
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetMostAchievedAchievementInfo")]
		private static extern int _GetMostAchievedAchievementInfo(IntPtr self, IntPtr pchName, uint unNameBufLen, ref float pflPercent, [MarshalAs(UnmanagedType.U1)] ref bool pbAchieved);

		// Token: 0x060006AF RID: 1711 RVA: 0x0000A304 File Offset: 0x00008504
		internal int GetMostAchievedAchievementInfo(out string pchName, ref float pflPercent, [MarshalAs(UnmanagedType.U1)] ref bool pbAchieved)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			int result = ISteamUserStats._GetMostAchievedAchievementInfo(this.Self, intPtr, 32768U, ref pflPercent, ref pbAchieved);
			pchName = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x060006B0 RID: 1712
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetNextMostAchievedAchievementInfo")]
		private static extern int _GetNextMostAchievedAchievementInfo(IntPtr self, int iIteratorPrevious, IntPtr pchName, uint unNameBufLen, ref float pflPercent, [MarshalAs(UnmanagedType.U1)] ref bool pbAchieved);

		// Token: 0x060006B1 RID: 1713 RVA: 0x0000A33C File Offset: 0x0000853C
		internal int GetNextMostAchievedAchievementInfo(int iIteratorPrevious, out string pchName, ref float pflPercent, [MarshalAs(UnmanagedType.U1)] ref bool pbAchieved)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			int result = ISteamUserStats._GetNextMostAchievedAchievementInfo(this.Self, iIteratorPrevious, intPtr, 32768U, ref pflPercent, ref pbAchieved);
			pchName = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x060006B2 RID: 1714
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementAchievedPercent")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetAchievementAchievedPercent(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, ref float pflPercent);

		// Token: 0x060006B3 RID: 1715 RVA: 0x0000A374 File Offset: 0x00008574
		internal bool GetAchievementAchievedPercent([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, ref float pflPercent)
		{
			return ISteamUserStats._GetAchievementAchievedPercent(this.Self, pchName, ref pflPercent);
		}

		// Token: 0x060006B4 RID: 1716
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_RequestGlobalStats")]
		private static extern SteamAPICall_t _RequestGlobalStats(IntPtr self, int nHistoryDays);

		// Token: 0x060006B5 RID: 1717 RVA: 0x0000A398 File Offset: 0x00008598
		internal CallResult<GlobalStatsReceived_t> RequestGlobalStats(int nHistoryDays)
		{
			SteamAPICall_t call = ISteamUserStats._RequestGlobalStats(this.Self, nHistoryDays);
			return new CallResult<GlobalStatsReceived_t>(call, base.IsServer);
		}

		// Token: 0x060006B6 RID: 1718
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatInt64")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetGlobalStat(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchStatName, ref long pData);

		// Token: 0x060006B7 RID: 1719 RVA: 0x0000A3C4 File Offset: 0x000085C4
		internal bool GetGlobalStat([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchStatName, ref long pData)
		{
			return ISteamUserStats._GetGlobalStat(this.Self, pchStatName, ref pData);
		}

		// Token: 0x060006B8 RID: 1720
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatDouble")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetGlobalStat(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchStatName, ref double pData);

		// Token: 0x060006B9 RID: 1721 RVA: 0x0000A3E8 File Offset: 0x000085E8
		internal bool GetGlobalStat([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchStatName, ref double pData)
		{
			return ISteamUserStats._GetGlobalStat(this.Self, pchStatName, ref pData);
		}

		// Token: 0x060006BA RID: 1722
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatHistoryInt64")]
		private static extern int _GetGlobalStatHistory(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchStatName, [In] [Out] long[] pData, uint cubData);

		// Token: 0x060006BB RID: 1723 RVA: 0x0000A40C File Offset: 0x0000860C
		internal int GetGlobalStatHistory([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchStatName, [In] [Out] long[] pData, uint cubData)
		{
			return ISteamUserStats._GetGlobalStatHistory(this.Self, pchStatName, pData, cubData);
		}

		// Token: 0x060006BC RID: 1724
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatHistoryDouble")]
		private static extern int _GetGlobalStatHistory(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchStatName, [In] [Out] double[] pData, uint cubData);

		// Token: 0x060006BD RID: 1725 RVA: 0x0000A430 File Offset: 0x00008630
		internal int GetGlobalStatHistory([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchStatName, [In] [Out] double[] pData, uint cubData)
		{
			return ISteamUserStats._GetGlobalStatHistory(this.Self, pchStatName, pData, cubData);
		}
	}
}
