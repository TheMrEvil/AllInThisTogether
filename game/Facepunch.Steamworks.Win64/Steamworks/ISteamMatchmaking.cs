using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000019 RID: 25
	internal class ISteamMatchmaking : SteamInterface
	{
		// Token: 0x06000346 RID: 838 RVA: 0x00006A9D File Offset: 0x00004C9D
		internal ISteamMatchmaking(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x06000347 RID: 839
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamMatchmaking_v009();

		// Token: 0x06000348 RID: 840 RVA: 0x00006AAF File Offset: 0x00004CAF
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamMatchmaking.SteamAPI_SteamMatchmaking_v009();
		}

		// Token: 0x06000349 RID: 841
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetFavoriteGameCount")]
		private static extern int _GetFavoriteGameCount(IntPtr self);

		// Token: 0x0600034A RID: 842 RVA: 0x00006AB8 File Offset: 0x00004CB8
		internal int GetFavoriteGameCount()
		{
			return ISteamMatchmaking._GetFavoriteGameCount(this.Self);
		}

		// Token: 0x0600034B RID: 843
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetFavoriteGame")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetFavoriteGame(IntPtr self, int iGame, ref AppId pnAppID, ref uint pnIP, ref ushort pnConnPort, ref ushort pnQueryPort, ref uint punFlags, ref uint pRTime32LastPlayedOnServer);

		// Token: 0x0600034C RID: 844 RVA: 0x00006AD8 File Offset: 0x00004CD8
		internal bool GetFavoriteGame(int iGame, ref AppId pnAppID, ref uint pnIP, ref ushort pnConnPort, ref ushort pnQueryPort, ref uint punFlags, ref uint pRTime32LastPlayedOnServer)
		{
			return ISteamMatchmaking._GetFavoriteGame(this.Self, iGame, ref pnAppID, ref pnIP, ref pnConnPort, ref pnQueryPort, ref punFlags, ref pRTime32LastPlayedOnServer);
		}

		// Token: 0x0600034D RID: 845
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddFavoriteGame")]
		private static extern int _AddFavoriteGame(IntPtr self, AppId nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags, uint rTime32LastPlayedOnServer);

		// Token: 0x0600034E RID: 846 RVA: 0x00006B04 File Offset: 0x00004D04
		internal int AddFavoriteGame(AppId nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags, uint rTime32LastPlayedOnServer)
		{
			return ISteamMatchmaking._AddFavoriteGame(this.Self, nAppID, nIP, nConnPort, nQueryPort, unFlags, rTime32LastPlayedOnServer);
		}

		// Token: 0x0600034F RID: 847
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_RemoveFavoriteGame")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _RemoveFavoriteGame(IntPtr self, AppId nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags);

		// Token: 0x06000350 RID: 848 RVA: 0x00006B2C File Offset: 0x00004D2C
		internal bool RemoveFavoriteGame(AppId nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags)
		{
			return ISteamMatchmaking._RemoveFavoriteGame(this.Self, nAppID, nIP, nConnPort, nQueryPort, unFlags);
		}

		// Token: 0x06000351 RID: 849
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_RequestLobbyList")]
		private static extern SteamAPICall_t _RequestLobbyList(IntPtr self);

		// Token: 0x06000352 RID: 850 RVA: 0x00006B54 File Offset: 0x00004D54
		internal CallResult<LobbyMatchList_t> RequestLobbyList()
		{
			SteamAPICall_t call = ISteamMatchmaking._RequestLobbyList(this.Self);
			return new CallResult<LobbyMatchList_t>(call, base.IsServer);
		}

		// Token: 0x06000353 RID: 851
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListStringFilter")]
		private static extern void _AddRequestLobbyListStringFilter(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKeyToMatch, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValueToMatch, LobbyComparison eComparisonType);

		// Token: 0x06000354 RID: 852 RVA: 0x00006B7E File Offset: 0x00004D7E
		internal void AddRequestLobbyListStringFilter([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKeyToMatch, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValueToMatch, LobbyComparison eComparisonType)
		{
			ISteamMatchmaking._AddRequestLobbyListStringFilter(this.Self, pchKeyToMatch, pchValueToMatch, eComparisonType);
		}

		// Token: 0x06000355 RID: 853
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListNumericalFilter")]
		private static extern void _AddRequestLobbyListNumericalFilter(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKeyToMatch, int nValueToMatch, LobbyComparison eComparisonType);

		// Token: 0x06000356 RID: 854 RVA: 0x00006B90 File Offset: 0x00004D90
		internal void AddRequestLobbyListNumericalFilter([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKeyToMatch, int nValueToMatch, LobbyComparison eComparisonType)
		{
			ISteamMatchmaking._AddRequestLobbyListNumericalFilter(this.Self, pchKeyToMatch, nValueToMatch, eComparisonType);
		}

		// Token: 0x06000357 RID: 855
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListNearValueFilter")]
		private static extern void _AddRequestLobbyListNearValueFilter(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKeyToMatch, int nValueToBeCloseTo);

		// Token: 0x06000358 RID: 856 RVA: 0x00006BA2 File Offset: 0x00004DA2
		internal void AddRequestLobbyListNearValueFilter([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKeyToMatch, int nValueToBeCloseTo)
		{
			ISteamMatchmaking._AddRequestLobbyListNearValueFilter(this.Self, pchKeyToMatch, nValueToBeCloseTo);
		}

		// Token: 0x06000359 RID: 857
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable")]
		private static extern void _AddRequestLobbyListFilterSlotsAvailable(IntPtr self, int nSlotsAvailable);

		// Token: 0x0600035A RID: 858 RVA: 0x00006BB3 File Offset: 0x00004DB3
		internal void AddRequestLobbyListFilterSlotsAvailable(int nSlotsAvailable)
		{
			ISteamMatchmaking._AddRequestLobbyListFilterSlotsAvailable(this.Self, nSlotsAvailable);
		}

		// Token: 0x0600035B RID: 859
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListDistanceFilter")]
		private static extern void _AddRequestLobbyListDistanceFilter(IntPtr self, LobbyDistanceFilter eLobbyDistanceFilter);

		// Token: 0x0600035C RID: 860 RVA: 0x00006BC3 File Offset: 0x00004DC3
		internal void AddRequestLobbyListDistanceFilter(LobbyDistanceFilter eLobbyDistanceFilter)
		{
			ISteamMatchmaking._AddRequestLobbyListDistanceFilter(this.Self, eLobbyDistanceFilter);
		}

		// Token: 0x0600035D RID: 861
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListResultCountFilter")]
		private static extern void _AddRequestLobbyListResultCountFilter(IntPtr self, int cMaxResults);

		// Token: 0x0600035E RID: 862 RVA: 0x00006BD3 File Offset: 0x00004DD3
		internal void AddRequestLobbyListResultCountFilter(int cMaxResults)
		{
			ISteamMatchmaking._AddRequestLobbyListResultCountFilter(this.Self, cMaxResults);
		}

		// Token: 0x0600035F RID: 863
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter")]
		private static extern void _AddRequestLobbyListCompatibleMembersFilter(IntPtr self, SteamId steamIDLobby);

		// Token: 0x06000360 RID: 864 RVA: 0x00006BE3 File Offset: 0x00004DE3
		internal void AddRequestLobbyListCompatibleMembersFilter(SteamId steamIDLobby)
		{
			ISteamMatchmaking._AddRequestLobbyListCompatibleMembersFilter(this.Self, steamIDLobby);
		}

		// Token: 0x06000361 RID: 865
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyByIndex")]
		private static extern SteamId _GetLobbyByIndex(IntPtr self, int iLobby);

		// Token: 0x06000362 RID: 866 RVA: 0x00006BF4 File Offset: 0x00004DF4
		internal SteamId GetLobbyByIndex(int iLobby)
		{
			return ISteamMatchmaking._GetLobbyByIndex(this.Self, iLobby);
		}

		// Token: 0x06000363 RID: 867
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_CreateLobby")]
		private static extern SteamAPICall_t _CreateLobby(IntPtr self, LobbyType eLobbyType, int cMaxMembers);

		// Token: 0x06000364 RID: 868 RVA: 0x00006C14 File Offset: 0x00004E14
		internal CallResult<LobbyCreated_t> CreateLobby(LobbyType eLobbyType, int cMaxMembers)
		{
			SteamAPICall_t call = ISteamMatchmaking._CreateLobby(this.Self, eLobbyType, cMaxMembers);
			return new CallResult<LobbyCreated_t>(call, base.IsServer);
		}

		// Token: 0x06000365 RID: 869
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_JoinLobby")]
		private static extern SteamAPICall_t _JoinLobby(IntPtr self, SteamId steamIDLobby);

		// Token: 0x06000366 RID: 870 RVA: 0x00006C40 File Offset: 0x00004E40
		internal CallResult<LobbyEnter_t> JoinLobby(SteamId steamIDLobby)
		{
			SteamAPICall_t call = ISteamMatchmaking._JoinLobby(this.Self, steamIDLobby);
			return new CallResult<LobbyEnter_t>(call, base.IsServer);
		}

		// Token: 0x06000367 RID: 871
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_LeaveLobby")]
		private static extern void _LeaveLobby(IntPtr self, SteamId steamIDLobby);

		// Token: 0x06000368 RID: 872 RVA: 0x00006C6B File Offset: 0x00004E6B
		internal void LeaveLobby(SteamId steamIDLobby)
		{
			ISteamMatchmaking._LeaveLobby(this.Self, steamIDLobby);
		}

		// Token: 0x06000369 RID: 873
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_InviteUserToLobby")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _InviteUserToLobby(IntPtr self, SteamId steamIDLobby, SteamId steamIDInvitee);

		// Token: 0x0600036A RID: 874 RVA: 0x00006C7C File Offset: 0x00004E7C
		internal bool InviteUserToLobby(SteamId steamIDLobby, SteamId steamIDInvitee)
		{
			return ISteamMatchmaking._InviteUserToLobby(this.Self, steamIDLobby, steamIDInvitee);
		}

		// Token: 0x0600036B RID: 875
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetNumLobbyMembers")]
		private static extern int _GetNumLobbyMembers(IntPtr self, SteamId steamIDLobby);

		// Token: 0x0600036C RID: 876 RVA: 0x00006CA0 File Offset: 0x00004EA0
		internal int GetNumLobbyMembers(SteamId steamIDLobby)
		{
			return ISteamMatchmaking._GetNumLobbyMembers(this.Self, steamIDLobby);
		}

		// Token: 0x0600036D RID: 877
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyMemberByIndex")]
		private static extern SteamId _GetLobbyMemberByIndex(IntPtr self, SteamId steamIDLobby, int iMember);

		// Token: 0x0600036E RID: 878 RVA: 0x00006CC0 File Offset: 0x00004EC0
		internal SteamId GetLobbyMemberByIndex(SteamId steamIDLobby, int iMember)
		{
			return ISteamMatchmaking._GetLobbyMemberByIndex(this.Self, steamIDLobby, iMember);
		}

		// Token: 0x0600036F RID: 879
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyData")]
		private static extern Utf8StringPointer _GetLobbyData(IntPtr self, SteamId steamIDLobby, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey);

		// Token: 0x06000370 RID: 880 RVA: 0x00006CE4 File Offset: 0x00004EE4
		internal string GetLobbyData(SteamId steamIDLobby, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey)
		{
			Utf8StringPointer p = ISteamMatchmaking._GetLobbyData(this.Self, steamIDLobby, pchKey);
			return p;
		}

		// Token: 0x06000371 RID: 881
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyData")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetLobbyData(IntPtr self, SteamId steamIDLobby, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue);

		// Token: 0x06000372 RID: 882 RVA: 0x00006D0C File Offset: 0x00004F0C
		internal bool SetLobbyData(SteamId steamIDLobby, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue)
		{
			return ISteamMatchmaking._SetLobbyData(this.Self, steamIDLobby, pchKey, pchValue);
		}

		// Token: 0x06000373 RID: 883
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyDataCount")]
		private static extern int _GetLobbyDataCount(IntPtr self, SteamId steamIDLobby);

		// Token: 0x06000374 RID: 884 RVA: 0x00006D30 File Offset: 0x00004F30
		internal int GetLobbyDataCount(SteamId steamIDLobby)
		{
			return ISteamMatchmaking._GetLobbyDataCount(this.Self, steamIDLobby);
		}

		// Token: 0x06000375 RID: 885
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyDataByIndex")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetLobbyDataByIndex(IntPtr self, SteamId steamIDLobby, int iLobbyData, IntPtr pchKey, int cchKeyBufferSize, IntPtr pchValue, int cchValueBufferSize);

		// Token: 0x06000376 RID: 886 RVA: 0x00006D50 File Offset: 0x00004F50
		internal bool GetLobbyDataByIndex(SteamId steamIDLobby, int iLobbyData, out string pchKey, out string pchValue)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			IntPtr intPtr2 = Helpers.TakeMemory();
			bool result = ISteamMatchmaking._GetLobbyDataByIndex(this.Self, steamIDLobby, iLobbyData, intPtr, 32768, intPtr2, 32768);
			pchKey = Helpers.MemoryToString(intPtr);
			pchValue = Helpers.MemoryToString(intPtr2);
			return result;
		}

		// Token: 0x06000377 RID: 887
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_DeleteLobbyData")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _DeleteLobbyData(IntPtr self, SteamId steamIDLobby, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey);

		// Token: 0x06000378 RID: 888 RVA: 0x00006D9C File Offset: 0x00004F9C
		internal bool DeleteLobbyData(SteamId steamIDLobby, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey)
		{
			return ISteamMatchmaking._DeleteLobbyData(this.Self, steamIDLobby, pchKey);
		}

		// Token: 0x06000379 RID: 889
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyMemberData")]
		private static extern Utf8StringPointer _GetLobbyMemberData(IntPtr self, SteamId steamIDLobby, SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey);

		// Token: 0x0600037A RID: 890 RVA: 0x00006DC0 File Offset: 0x00004FC0
		internal string GetLobbyMemberData(SteamId steamIDLobby, SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey)
		{
			Utf8StringPointer p = ISteamMatchmaking._GetLobbyMemberData(this.Self, steamIDLobby, steamIDUser, pchKey);
			return p;
		}

		// Token: 0x0600037B RID: 891
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyMemberData")]
		private static extern void _SetLobbyMemberData(IntPtr self, SteamId steamIDLobby, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue);

		// Token: 0x0600037C RID: 892 RVA: 0x00006DE7 File Offset: 0x00004FE7
		internal void SetLobbyMemberData(SteamId steamIDLobby, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue)
		{
			ISteamMatchmaking._SetLobbyMemberData(this.Self, steamIDLobby, pchKey, pchValue);
		}

		// Token: 0x0600037D RID: 893
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SendLobbyChatMsg")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SendLobbyChatMsg(IntPtr self, SteamId steamIDLobby, IntPtr pvMsgBody, int cubMsgBody);

		// Token: 0x0600037E RID: 894 RVA: 0x00006DFC File Offset: 0x00004FFC
		internal bool SendLobbyChatMsg(SteamId steamIDLobby, IntPtr pvMsgBody, int cubMsgBody)
		{
			return ISteamMatchmaking._SendLobbyChatMsg(this.Self, steamIDLobby, pvMsgBody, cubMsgBody);
		}

		// Token: 0x0600037F RID: 895
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyChatEntry")]
		private static extern int _GetLobbyChatEntry(IntPtr self, SteamId steamIDLobby, int iChatID, ref SteamId pSteamIDUser, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType);

		// Token: 0x06000380 RID: 896 RVA: 0x00006E20 File Offset: 0x00005020
		internal int GetLobbyChatEntry(SteamId steamIDLobby, int iChatID, ref SteamId pSteamIDUser, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType)
		{
			return ISteamMatchmaking._GetLobbyChatEntry(this.Self, steamIDLobby, iChatID, ref pSteamIDUser, pvData, cubData, ref peChatEntryType);
		}

		// Token: 0x06000381 RID: 897
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_RequestLobbyData")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _RequestLobbyData(IntPtr self, SteamId steamIDLobby);

		// Token: 0x06000382 RID: 898 RVA: 0x00006E48 File Offset: 0x00005048
		internal bool RequestLobbyData(SteamId steamIDLobby)
		{
			return ISteamMatchmaking._RequestLobbyData(this.Self, steamIDLobby);
		}

		// Token: 0x06000383 RID: 899
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyGameServer")]
		private static extern void _SetLobbyGameServer(IntPtr self, SteamId steamIDLobby, uint unGameServerIP, ushort unGameServerPort, SteamId steamIDGameServer);

		// Token: 0x06000384 RID: 900 RVA: 0x00006E68 File Offset: 0x00005068
		internal void SetLobbyGameServer(SteamId steamIDLobby, uint unGameServerIP, ushort unGameServerPort, SteamId steamIDGameServer)
		{
			ISteamMatchmaking._SetLobbyGameServer(this.Self, steamIDLobby, unGameServerIP, unGameServerPort, steamIDGameServer);
		}

		// Token: 0x06000385 RID: 901
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyGameServer")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetLobbyGameServer(IntPtr self, SteamId steamIDLobby, ref uint punGameServerIP, ref ushort punGameServerPort, ref SteamId psteamIDGameServer);

		// Token: 0x06000386 RID: 902 RVA: 0x00006E7C File Offset: 0x0000507C
		internal bool GetLobbyGameServer(SteamId steamIDLobby, ref uint punGameServerIP, ref ushort punGameServerPort, ref SteamId psteamIDGameServer)
		{
			return ISteamMatchmaking._GetLobbyGameServer(this.Self, steamIDLobby, ref punGameServerIP, ref punGameServerPort, ref psteamIDGameServer);
		}

		// Token: 0x06000387 RID: 903
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyMemberLimit")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetLobbyMemberLimit(IntPtr self, SteamId steamIDLobby, int cMaxMembers);

		// Token: 0x06000388 RID: 904 RVA: 0x00006EA0 File Offset: 0x000050A0
		internal bool SetLobbyMemberLimit(SteamId steamIDLobby, int cMaxMembers)
		{
			return ISteamMatchmaking._SetLobbyMemberLimit(this.Self, steamIDLobby, cMaxMembers);
		}

		// Token: 0x06000389 RID: 905
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyMemberLimit")]
		private static extern int _GetLobbyMemberLimit(IntPtr self, SteamId steamIDLobby);

		// Token: 0x0600038A RID: 906 RVA: 0x00006EC4 File Offset: 0x000050C4
		internal int GetLobbyMemberLimit(SteamId steamIDLobby)
		{
			return ISteamMatchmaking._GetLobbyMemberLimit(this.Self, steamIDLobby);
		}

		// Token: 0x0600038B RID: 907
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyType")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetLobbyType(IntPtr self, SteamId steamIDLobby, LobbyType eLobbyType);

		// Token: 0x0600038C RID: 908 RVA: 0x00006EE4 File Offset: 0x000050E4
		internal bool SetLobbyType(SteamId steamIDLobby, LobbyType eLobbyType)
		{
			return ISteamMatchmaking._SetLobbyType(this.Self, steamIDLobby, eLobbyType);
		}

		// Token: 0x0600038D RID: 909
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyJoinable")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetLobbyJoinable(IntPtr self, SteamId steamIDLobby, [MarshalAs(UnmanagedType.U1)] bool bLobbyJoinable);

		// Token: 0x0600038E RID: 910 RVA: 0x00006F08 File Offset: 0x00005108
		internal bool SetLobbyJoinable(SteamId steamIDLobby, [MarshalAs(UnmanagedType.U1)] bool bLobbyJoinable)
		{
			return ISteamMatchmaking._SetLobbyJoinable(this.Self, steamIDLobby, bLobbyJoinable);
		}

		// Token: 0x0600038F RID: 911
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyOwner")]
		private static extern SteamId _GetLobbyOwner(IntPtr self, SteamId steamIDLobby);

		// Token: 0x06000390 RID: 912 RVA: 0x00006F2C File Offset: 0x0000512C
		internal SteamId GetLobbyOwner(SteamId steamIDLobby)
		{
			return ISteamMatchmaking._GetLobbyOwner(this.Self, steamIDLobby);
		}

		// Token: 0x06000391 RID: 913
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyOwner")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetLobbyOwner(IntPtr self, SteamId steamIDLobby, SteamId steamIDNewOwner);

		// Token: 0x06000392 RID: 914 RVA: 0x00006F4C File Offset: 0x0000514C
		internal bool SetLobbyOwner(SteamId steamIDLobby, SteamId steamIDNewOwner)
		{
			return ISteamMatchmaking._SetLobbyOwner(this.Self, steamIDLobby, steamIDNewOwner);
		}

		// Token: 0x06000393 RID: 915
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLinkedLobby")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetLinkedLobby(IntPtr self, SteamId steamIDLobby, SteamId steamIDLobbyDependent);

		// Token: 0x06000394 RID: 916 RVA: 0x00006F70 File Offset: 0x00005170
		internal bool SetLinkedLobby(SteamId steamIDLobby, SteamId steamIDLobbyDependent)
		{
			return ISteamMatchmaking._SetLinkedLobby(this.Self, steamIDLobby, steamIDLobbyDependent);
		}
	}
}
