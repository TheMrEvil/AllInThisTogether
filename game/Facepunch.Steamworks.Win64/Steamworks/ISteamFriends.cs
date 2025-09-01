using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000011 RID: 17
	internal class ISteamFriends : SteamInterface
	{
		// Token: 0x06000104 RID: 260 RVA: 0x0000496A File Offset: 0x00002B6A
		internal ISteamFriends(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x06000105 RID: 261
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamFriends_v017();

		// Token: 0x06000106 RID: 262 RVA: 0x0000497C File Offset: 0x00002B7C
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamFriends.SteamAPI_SteamFriends_v017();
		}

		// Token: 0x06000107 RID: 263
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetPersonaName")]
		private static extern Utf8StringPointer _GetPersonaName(IntPtr self);

		// Token: 0x06000108 RID: 264 RVA: 0x00004984 File Offset: 0x00002B84
		internal string GetPersonaName()
		{
			Utf8StringPointer p = ISteamFriends._GetPersonaName(this.Self);
			return p;
		}

		// Token: 0x06000109 RID: 265
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_SetPersonaName")]
		private static extern SteamAPICall_t _SetPersonaName(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPersonaName);

		// Token: 0x0600010A RID: 266 RVA: 0x000049A8 File Offset: 0x00002BA8
		internal CallResult<SetPersonaNameResponse_t> SetPersonaName([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPersonaName)
		{
			SteamAPICall_t call = ISteamFriends._SetPersonaName(this.Self, pchPersonaName);
			return new CallResult<SetPersonaNameResponse_t>(call, base.IsServer);
		}

		// Token: 0x0600010B RID: 267
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetPersonaState")]
		private static extern FriendState _GetPersonaState(IntPtr self);

		// Token: 0x0600010C RID: 268 RVA: 0x000049D4 File Offset: 0x00002BD4
		internal FriendState GetPersonaState()
		{
			return ISteamFriends._GetPersonaState(this.Self);
		}

		// Token: 0x0600010D RID: 269
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendCount")]
		private static extern int _GetFriendCount(IntPtr self, int iFriendFlags);

		// Token: 0x0600010E RID: 270 RVA: 0x000049F4 File Offset: 0x00002BF4
		internal int GetFriendCount(int iFriendFlags)
		{
			return ISteamFriends._GetFriendCount(this.Self, iFriendFlags);
		}

		// Token: 0x0600010F RID: 271
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendByIndex")]
		private static extern SteamId _GetFriendByIndex(IntPtr self, int iFriend, int iFriendFlags);

		// Token: 0x06000110 RID: 272 RVA: 0x00004A14 File Offset: 0x00002C14
		internal SteamId GetFriendByIndex(int iFriend, int iFriendFlags)
		{
			return ISteamFriends._GetFriendByIndex(this.Self, iFriend, iFriendFlags);
		}

		// Token: 0x06000111 RID: 273
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendRelationship")]
		private static extern Relationship _GetFriendRelationship(IntPtr self, SteamId steamIDFriend);

		// Token: 0x06000112 RID: 274 RVA: 0x00004A38 File Offset: 0x00002C38
		internal Relationship GetFriendRelationship(SteamId steamIDFriend)
		{
			return ISteamFriends._GetFriendRelationship(this.Self, steamIDFriend);
		}

		// Token: 0x06000113 RID: 275
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendPersonaState")]
		private static extern FriendState _GetFriendPersonaState(IntPtr self, SteamId steamIDFriend);

		// Token: 0x06000114 RID: 276 RVA: 0x00004A58 File Offset: 0x00002C58
		internal FriendState GetFriendPersonaState(SteamId steamIDFriend)
		{
			return ISteamFriends._GetFriendPersonaState(this.Self, steamIDFriend);
		}

		// Token: 0x06000115 RID: 277
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendPersonaName")]
		private static extern Utf8StringPointer _GetFriendPersonaName(IntPtr self, SteamId steamIDFriend);

		// Token: 0x06000116 RID: 278 RVA: 0x00004A78 File Offset: 0x00002C78
		internal string GetFriendPersonaName(SteamId steamIDFriend)
		{
			Utf8StringPointer p = ISteamFriends._GetFriendPersonaName(this.Self, steamIDFriend);
			return p;
		}

		// Token: 0x06000117 RID: 279
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendGamePlayed")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetFriendGamePlayed(IntPtr self, SteamId steamIDFriend, ref FriendGameInfo_t pFriendGameInfo);

		// Token: 0x06000118 RID: 280 RVA: 0x00004AA0 File Offset: 0x00002CA0
		internal bool GetFriendGamePlayed(SteamId steamIDFriend, ref FriendGameInfo_t pFriendGameInfo)
		{
			return ISteamFriends._GetFriendGamePlayed(this.Self, steamIDFriend, ref pFriendGameInfo);
		}

		// Token: 0x06000119 RID: 281
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendPersonaNameHistory")]
		private static extern Utf8StringPointer _GetFriendPersonaNameHistory(IntPtr self, SteamId steamIDFriend, int iPersonaName);

		// Token: 0x0600011A RID: 282 RVA: 0x00004AC4 File Offset: 0x00002CC4
		internal string GetFriendPersonaNameHistory(SteamId steamIDFriend, int iPersonaName)
		{
			Utf8StringPointer p = ISteamFriends._GetFriendPersonaNameHistory(this.Self, steamIDFriend, iPersonaName);
			return p;
		}

		// Token: 0x0600011B RID: 283
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendSteamLevel")]
		private static extern int _GetFriendSteamLevel(IntPtr self, SteamId steamIDFriend);

		// Token: 0x0600011C RID: 284 RVA: 0x00004AEC File Offset: 0x00002CEC
		internal int GetFriendSteamLevel(SteamId steamIDFriend)
		{
			return ISteamFriends._GetFriendSteamLevel(this.Self, steamIDFriend);
		}

		// Token: 0x0600011D RID: 285
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetPlayerNickname")]
		private static extern Utf8StringPointer _GetPlayerNickname(IntPtr self, SteamId steamIDPlayer);

		// Token: 0x0600011E RID: 286 RVA: 0x00004B0C File Offset: 0x00002D0C
		internal string GetPlayerNickname(SteamId steamIDPlayer)
		{
			Utf8StringPointer p = ISteamFriends._GetPlayerNickname(this.Self, steamIDPlayer);
			return p;
		}

		// Token: 0x0600011F RID: 287
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupCount")]
		private static extern int _GetFriendsGroupCount(IntPtr self);

		// Token: 0x06000120 RID: 288 RVA: 0x00004B34 File Offset: 0x00002D34
		internal int GetFriendsGroupCount()
		{
			return ISteamFriends._GetFriendsGroupCount(this.Self);
		}

		// Token: 0x06000121 RID: 289
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupIDByIndex")]
		private static extern FriendsGroupID_t _GetFriendsGroupIDByIndex(IntPtr self, int iFG);

		// Token: 0x06000122 RID: 290 RVA: 0x00004B54 File Offset: 0x00002D54
		internal FriendsGroupID_t GetFriendsGroupIDByIndex(int iFG)
		{
			return ISteamFriends._GetFriendsGroupIDByIndex(this.Self, iFG);
		}

		// Token: 0x06000123 RID: 291
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupName")]
		private static extern Utf8StringPointer _GetFriendsGroupName(IntPtr self, FriendsGroupID_t friendsGroupID);

		// Token: 0x06000124 RID: 292 RVA: 0x00004B74 File Offset: 0x00002D74
		internal string GetFriendsGroupName(FriendsGroupID_t friendsGroupID)
		{
			Utf8StringPointer p = ISteamFriends._GetFriendsGroupName(this.Self, friendsGroupID);
			return p;
		}

		// Token: 0x06000125 RID: 293
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupMembersCount")]
		private static extern int _GetFriendsGroupMembersCount(IntPtr self, FriendsGroupID_t friendsGroupID);

		// Token: 0x06000126 RID: 294 RVA: 0x00004B9C File Offset: 0x00002D9C
		internal int GetFriendsGroupMembersCount(FriendsGroupID_t friendsGroupID)
		{
			return ISteamFriends._GetFriendsGroupMembersCount(this.Self, friendsGroupID);
		}

		// Token: 0x06000127 RID: 295
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupMembersList")]
		private static extern void _GetFriendsGroupMembersList(IntPtr self, FriendsGroupID_t friendsGroupID, [In] [Out] SteamId[] pOutSteamIDMembers, int nMembersCount);

		// Token: 0x06000128 RID: 296 RVA: 0x00004BBC File Offset: 0x00002DBC
		internal void GetFriendsGroupMembersList(FriendsGroupID_t friendsGroupID, [In] [Out] SteamId[] pOutSteamIDMembers, int nMembersCount)
		{
			ISteamFriends._GetFriendsGroupMembersList(this.Self, friendsGroupID, pOutSteamIDMembers, nMembersCount);
		}

		// Token: 0x06000129 RID: 297
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_HasFriend")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _HasFriend(IntPtr self, SteamId steamIDFriend, int iFriendFlags);

		// Token: 0x0600012A RID: 298 RVA: 0x00004BD0 File Offset: 0x00002DD0
		internal bool HasFriend(SteamId steamIDFriend, int iFriendFlags)
		{
			return ISteamFriends._HasFriend(this.Self, steamIDFriend, iFriendFlags);
		}

		// Token: 0x0600012B RID: 299
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanCount")]
		private static extern int _GetClanCount(IntPtr self);

		// Token: 0x0600012C RID: 300 RVA: 0x00004BF4 File Offset: 0x00002DF4
		internal int GetClanCount()
		{
			return ISteamFriends._GetClanCount(this.Self);
		}

		// Token: 0x0600012D RID: 301
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanByIndex")]
		private static extern SteamId _GetClanByIndex(IntPtr self, int iClan);

		// Token: 0x0600012E RID: 302 RVA: 0x00004C14 File Offset: 0x00002E14
		internal SteamId GetClanByIndex(int iClan)
		{
			return ISteamFriends._GetClanByIndex(this.Self, iClan);
		}

		// Token: 0x0600012F RID: 303
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanName")]
		private static extern Utf8StringPointer _GetClanName(IntPtr self, SteamId steamIDClan);

		// Token: 0x06000130 RID: 304 RVA: 0x00004C34 File Offset: 0x00002E34
		internal string GetClanName(SteamId steamIDClan)
		{
			Utf8StringPointer p = ISteamFriends._GetClanName(this.Self, steamIDClan);
			return p;
		}

		// Token: 0x06000131 RID: 305
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanTag")]
		private static extern Utf8StringPointer _GetClanTag(IntPtr self, SteamId steamIDClan);

		// Token: 0x06000132 RID: 306 RVA: 0x00004C5C File Offset: 0x00002E5C
		internal string GetClanTag(SteamId steamIDClan)
		{
			Utf8StringPointer p = ISteamFriends._GetClanTag(this.Self, steamIDClan);
			return p;
		}

		// Token: 0x06000133 RID: 307
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanActivityCounts")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetClanActivityCounts(IntPtr self, SteamId steamIDClan, ref int pnOnline, ref int pnInGame, ref int pnChatting);

		// Token: 0x06000134 RID: 308 RVA: 0x00004C84 File Offset: 0x00002E84
		internal bool GetClanActivityCounts(SteamId steamIDClan, ref int pnOnline, ref int pnInGame, ref int pnChatting)
		{
			return ISteamFriends._GetClanActivityCounts(this.Self, steamIDClan, ref pnOnline, ref pnInGame, ref pnChatting);
		}

		// Token: 0x06000135 RID: 309
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_DownloadClanActivityCounts")]
		private static extern SteamAPICall_t _DownloadClanActivityCounts(IntPtr self, [In] [Out] SteamId[] psteamIDClans, int cClansToRequest);

		// Token: 0x06000136 RID: 310 RVA: 0x00004CA8 File Offset: 0x00002EA8
		internal CallResult<DownloadClanActivityCountsResult_t> DownloadClanActivityCounts([In] [Out] SteamId[] psteamIDClans, int cClansToRequest)
		{
			SteamAPICall_t call = ISteamFriends._DownloadClanActivityCounts(this.Self, psteamIDClans, cClansToRequest);
			return new CallResult<DownloadClanActivityCountsResult_t>(call, base.IsServer);
		}

		// Token: 0x06000137 RID: 311
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendCountFromSource")]
		private static extern int _GetFriendCountFromSource(IntPtr self, SteamId steamIDSource);

		// Token: 0x06000138 RID: 312 RVA: 0x00004CD4 File Offset: 0x00002ED4
		internal int GetFriendCountFromSource(SteamId steamIDSource)
		{
			return ISteamFriends._GetFriendCountFromSource(this.Self, steamIDSource);
		}

		// Token: 0x06000139 RID: 313
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendFromSourceByIndex")]
		private static extern SteamId _GetFriendFromSourceByIndex(IntPtr self, SteamId steamIDSource, int iFriend);

		// Token: 0x0600013A RID: 314 RVA: 0x00004CF4 File Offset: 0x00002EF4
		internal SteamId GetFriendFromSourceByIndex(SteamId steamIDSource, int iFriend)
		{
			return ISteamFriends._GetFriendFromSourceByIndex(this.Self, steamIDSource, iFriend);
		}

		// Token: 0x0600013B RID: 315
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_IsUserInSource")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsUserInSource(IntPtr self, SteamId steamIDUser, SteamId steamIDSource);

		// Token: 0x0600013C RID: 316 RVA: 0x00004D18 File Offset: 0x00002F18
		internal bool IsUserInSource(SteamId steamIDUser, SteamId steamIDSource)
		{
			return ISteamFriends._IsUserInSource(this.Self, steamIDUser, steamIDSource);
		}

		// Token: 0x0600013D RID: 317
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_SetInGameVoiceSpeaking")]
		private static extern void _SetInGameVoiceSpeaking(IntPtr self, SteamId steamIDUser, [MarshalAs(UnmanagedType.U1)] bool bSpeaking);

		// Token: 0x0600013E RID: 318 RVA: 0x00004D39 File Offset: 0x00002F39
		internal void SetInGameVoiceSpeaking(SteamId steamIDUser, [MarshalAs(UnmanagedType.U1)] bool bSpeaking)
		{
			ISteamFriends._SetInGameVoiceSpeaking(this.Self, steamIDUser, bSpeaking);
		}

		// Token: 0x0600013F RID: 319
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlay")]
		private static extern void _ActivateGameOverlay(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchDialog);

		// Token: 0x06000140 RID: 320 RVA: 0x00004D4A File Offset: 0x00002F4A
		internal void ActivateGameOverlay([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchDialog)
		{
			ISteamFriends._ActivateGameOverlay(this.Self, pchDialog);
		}

		// Token: 0x06000141 RID: 321
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayToUser")]
		private static extern void _ActivateGameOverlayToUser(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchDialog, SteamId steamID);

		// Token: 0x06000142 RID: 322 RVA: 0x00004D5A File Offset: 0x00002F5A
		internal void ActivateGameOverlayToUser([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchDialog, SteamId steamID)
		{
			ISteamFriends._ActivateGameOverlayToUser(this.Self, pchDialog, steamID);
		}

		// Token: 0x06000143 RID: 323
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayToWebPage")]
		private static extern void _ActivateGameOverlayToWebPage(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchURL, ActivateGameOverlayToWebPageMode eMode);

		// Token: 0x06000144 RID: 324 RVA: 0x00004D6B File Offset: 0x00002F6B
		internal void ActivateGameOverlayToWebPage([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchURL, ActivateGameOverlayToWebPageMode eMode)
		{
			ISteamFriends._ActivateGameOverlayToWebPage(this.Self, pchURL, eMode);
		}

		// Token: 0x06000145 RID: 325
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayToStore")]
		private static extern void _ActivateGameOverlayToStore(IntPtr self, AppId nAppID, OverlayToStoreFlag eFlag);

		// Token: 0x06000146 RID: 326 RVA: 0x00004D7C File Offset: 0x00002F7C
		internal void ActivateGameOverlayToStore(AppId nAppID, OverlayToStoreFlag eFlag)
		{
			ISteamFriends._ActivateGameOverlayToStore(this.Self, nAppID, eFlag);
		}

		// Token: 0x06000147 RID: 327
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_SetPlayedWith")]
		private static extern void _SetPlayedWith(IntPtr self, SteamId steamIDUserPlayedWith);

		// Token: 0x06000148 RID: 328 RVA: 0x00004D8D File Offset: 0x00002F8D
		internal void SetPlayedWith(SteamId steamIDUserPlayedWith)
		{
			ISteamFriends._SetPlayedWith(this.Self, steamIDUserPlayedWith);
		}

		// Token: 0x06000149 RID: 329
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayInviteDialog")]
		private static extern void _ActivateGameOverlayInviteDialog(IntPtr self, SteamId steamIDLobby);

		// Token: 0x0600014A RID: 330 RVA: 0x00004D9D File Offset: 0x00002F9D
		internal void ActivateGameOverlayInviteDialog(SteamId steamIDLobby)
		{
			ISteamFriends._ActivateGameOverlayInviteDialog(this.Self, steamIDLobby);
		}

		// Token: 0x0600014B RID: 331
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetSmallFriendAvatar")]
		private static extern int _GetSmallFriendAvatar(IntPtr self, SteamId steamIDFriend);

		// Token: 0x0600014C RID: 332 RVA: 0x00004DB0 File Offset: 0x00002FB0
		internal int GetSmallFriendAvatar(SteamId steamIDFriend)
		{
			return ISteamFriends._GetSmallFriendAvatar(this.Self, steamIDFriend);
		}

		// Token: 0x0600014D RID: 333
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetMediumFriendAvatar")]
		private static extern int _GetMediumFriendAvatar(IntPtr self, SteamId steamIDFriend);

		// Token: 0x0600014E RID: 334 RVA: 0x00004DD0 File Offset: 0x00002FD0
		internal int GetMediumFriendAvatar(SteamId steamIDFriend)
		{
			return ISteamFriends._GetMediumFriendAvatar(this.Self, steamIDFriend);
		}

		// Token: 0x0600014F RID: 335
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetLargeFriendAvatar")]
		private static extern int _GetLargeFriendAvatar(IntPtr self, SteamId steamIDFriend);

		// Token: 0x06000150 RID: 336 RVA: 0x00004DF0 File Offset: 0x00002FF0
		internal int GetLargeFriendAvatar(SteamId steamIDFriend)
		{
			return ISteamFriends._GetLargeFriendAvatar(this.Self, steamIDFriend);
		}

		// Token: 0x06000151 RID: 337
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_RequestUserInformation")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _RequestUserInformation(IntPtr self, SteamId steamIDUser, [MarshalAs(UnmanagedType.U1)] bool bRequireNameOnly);

		// Token: 0x06000152 RID: 338 RVA: 0x00004E10 File Offset: 0x00003010
		internal bool RequestUserInformation(SteamId steamIDUser, [MarshalAs(UnmanagedType.U1)] bool bRequireNameOnly)
		{
			return ISteamFriends._RequestUserInformation(this.Self, steamIDUser, bRequireNameOnly);
		}

		// Token: 0x06000153 RID: 339
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_RequestClanOfficerList")]
		private static extern SteamAPICall_t _RequestClanOfficerList(IntPtr self, SteamId steamIDClan);

		// Token: 0x06000154 RID: 340 RVA: 0x00004E34 File Offset: 0x00003034
		internal CallResult<ClanOfficerListResponse_t> RequestClanOfficerList(SteamId steamIDClan)
		{
			SteamAPICall_t call = ISteamFriends._RequestClanOfficerList(this.Self, steamIDClan);
			return new CallResult<ClanOfficerListResponse_t>(call, base.IsServer);
		}

		// Token: 0x06000155 RID: 341
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanOwner")]
		private static extern SteamId _GetClanOwner(IntPtr self, SteamId steamIDClan);

		// Token: 0x06000156 RID: 342 RVA: 0x00004E60 File Offset: 0x00003060
		internal SteamId GetClanOwner(SteamId steamIDClan)
		{
			return ISteamFriends._GetClanOwner(this.Self, steamIDClan);
		}

		// Token: 0x06000157 RID: 343
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanOfficerCount")]
		private static extern int _GetClanOfficerCount(IntPtr self, SteamId steamIDClan);

		// Token: 0x06000158 RID: 344 RVA: 0x00004E80 File Offset: 0x00003080
		internal int GetClanOfficerCount(SteamId steamIDClan)
		{
			return ISteamFriends._GetClanOfficerCount(this.Self, steamIDClan);
		}

		// Token: 0x06000159 RID: 345
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanOfficerByIndex")]
		private static extern SteamId _GetClanOfficerByIndex(IntPtr self, SteamId steamIDClan, int iOfficer);

		// Token: 0x0600015A RID: 346 RVA: 0x00004EA0 File Offset: 0x000030A0
		internal SteamId GetClanOfficerByIndex(SteamId steamIDClan, int iOfficer)
		{
			return ISteamFriends._GetClanOfficerByIndex(this.Self, steamIDClan, iOfficer);
		}

		// Token: 0x0600015B RID: 347
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetUserRestrictions")]
		private static extern uint _GetUserRestrictions(IntPtr self);

		// Token: 0x0600015C RID: 348 RVA: 0x00004EC4 File Offset: 0x000030C4
		internal uint GetUserRestrictions()
		{
			return ISteamFriends._GetUserRestrictions(this.Self);
		}

		// Token: 0x0600015D RID: 349
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_SetRichPresence")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetRichPresence(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue);

		// Token: 0x0600015E RID: 350 RVA: 0x00004EE4 File Offset: 0x000030E4
		internal bool SetRichPresence([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue)
		{
			return ISteamFriends._SetRichPresence(this.Self, pchKey, pchValue);
		}

		// Token: 0x0600015F RID: 351
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ClearRichPresence")]
		private static extern void _ClearRichPresence(IntPtr self);

		// Token: 0x06000160 RID: 352 RVA: 0x00004F05 File Offset: 0x00003105
		internal void ClearRichPresence()
		{
			ISteamFriends._ClearRichPresence(this.Self);
		}

		// Token: 0x06000161 RID: 353
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendRichPresence")]
		private static extern Utf8StringPointer _GetFriendRichPresence(IntPtr self, SteamId steamIDFriend, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey);

		// Token: 0x06000162 RID: 354 RVA: 0x00004F14 File Offset: 0x00003114
		internal string GetFriendRichPresence(SteamId steamIDFriend, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey)
		{
			Utf8StringPointer p = ISteamFriends._GetFriendRichPresence(this.Self, steamIDFriend, pchKey);
			return p;
		}

		// Token: 0x06000163 RID: 355
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendRichPresenceKeyCount")]
		private static extern int _GetFriendRichPresenceKeyCount(IntPtr self, SteamId steamIDFriend);

		// Token: 0x06000164 RID: 356 RVA: 0x00004F3C File Offset: 0x0000313C
		internal int GetFriendRichPresenceKeyCount(SteamId steamIDFriend)
		{
			return ISteamFriends._GetFriendRichPresenceKeyCount(this.Self, steamIDFriend);
		}

		// Token: 0x06000165 RID: 357
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendRichPresenceKeyByIndex")]
		private static extern Utf8StringPointer _GetFriendRichPresenceKeyByIndex(IntPtr self, SteamId steamIDFriend, int iKey);

		// Token: 0x06000166 RID: 358 RVA: 0x00004F5C File Offset: 0x0000315C
		internal string GetFriendRichPresenceKeyByIndex(SteamId steamIDFriend, int iKey)
		{
			Utf8StringPointer p = ISteamFriends._GetFriendRichPresenceKeyByIndex(this.Self, steamIDFriend, iKey);
			return p;
		}

		// Token: 0x06000167 RID: 359
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_RequestFriendRichPresence")]
		private static extern void _RequestFriendRichPresence(IntPtr self, SteamId steamIDFriend);

		// Token: 0x06000168 RID: 360 RVA: 0x00004F82 File Offset: 0x00003182
		internal void RequestFriendRichPresence(SteamId steamIDFriend)
		{
			ISteamFriends._RequestFriendRichPresence(this.Self, steamIDFriend);
		}

		// Token: 0x06000169 RID: 361
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_InviteUserToGame")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _InviteUserToGame(IntPtr self, SteamId steamIDFriend, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchConnectString);

		// Token: 0x0600016A RID: 362 RVA: 0x00004F94 File Offset: 0x00003194
		internal bool InviteUserToGame(SteamId steamIDFriend, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchConnectString)
		{
			return ISteamFriends._InviteUserToGame(this.Self, steamIDFriend, pchConnectString);
		}

		// Token: 0x0600016B RID: 363
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetCoplayFriendCount")]
		private static extern int _GetCoplayFriendCount(IntPtr self);

		// Token: 0x0600016C RID: 364 RVA: 0x00004FB8 File Offset: 0x000031B8
		internal int GetCoplayFriendCount()
		{
			return ISteamFriends._GetCoplayFriendCount(this.Self);
		}

		// Token: 0x0600016D RID: 365
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetCoplayFriend")]
		private static extern SteamId _GetCoplayFriend(IntPtr self, int iCoplayFriend);

		// Token: 0x0600016E RID: 366 RVA: 0x00004FD8 File Offset: 0x000031D8
		internal SteamId GetCoplayFriend(int iCoplayFriend)
		{
			return ISteamFriends._GetCoplayFriend(this.Self, iCoplayFriend);
		}

		// Token: 0x0600016F RID: 367
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendCoplayTime")]
		private static extern int _GetFriendCoplayTime(IntPtr self, SteamId steamIDFriend);

		// Token: 0x06000170 RID: 368 RVA: 0x00004FF8 File Offset: 0x000031F8
		internal int GetFriendCoplayTime(SteamId steamIDFriend)
		{
			return ISteamFriends._GetFriendCoplayTime(this.Self, steamIDFriend);
		}

		// Token: 0x06000171 RID: 369
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendCoplayGame")]
		private static extern AppId _GetFriendCoplayGame(IntPtr self, SteamId steamIDFriend);

		// Token: 0x06000172 RID: 370 RVA: 0x00005018 File Offset: 0x00003218
		internal AppId GetFriendCoplayGame(SteamId steamIDFriend)
		{
			return ISteamFriends._GetFriendCoplayGame(this.Self, steamIDFriend);
		}

		// Token: 0x06000173 RID: 371
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_JoinClanChatRoom")]
		private static extern SteamAPICall_t _JoinClanChatRoom(IntPtr self, SteamId steamIDClan);

		// Token: 0x06000174 RID: 372 RVA: 0x00005038 File Offset: 0x00003238
		internal CallResult<JoinClanChatRoomCompletionResult_t> JoinClanChatRoom(SteamId steamIDClan)
		{
			SteamAPICall_t call = ISteamFriends._JoinClanChatRoom(this.Self, steamIDClan);
			return new CallResult<JoinClanChatRoomCompletionResult_t>(call, base.IsServer);
		}

		// Token: 0x06000175 RID: 373
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_LeaveClanChatRoom")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _LeaveClanChatRoom(IntPtr self, SteamId steamIDClan);

		// Token: 0x06000176 RID: 374 RVA: 0x00005064 File Offset: 0x00003264
		internal bool LeaveClanChatRoom(SteamId steamIDClan)
		{
			return ISteamFriends._LeaveClanChatRoom(this.Self, steamIDClan);
		}

		// Token: 0x06000177 RID: 375
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanChatMemberCount")]
		private static extern int _GetClanChatMemberCount(IntPtr self, SteamId steamIDClan);

		// Token: 0x06000178 RID: 376 RVA: 0x00005084 File Offset: 0x00003284
		internal int GetClanChatMemberCount(SteamId steamIDClan)
		{
			return ISteamFriends._GetClanChatMemberCount(this.Self, steamIDClan);
		}

		// Token: 0x06000179 RID: 377
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetChatMemberByIndex")]
		private static extern SteamId _GetChatMemberByIndex(IntPtr self, SteamId steamIDClan, int iUser);

		// Token: 0x0600017A RID: 378 RVA: 0x000050A4 File Offset: 0x000032A4
		internal SteamId GetChatMemberByIndex(SteamId steamIDClan, int iUser)
		{
			return ISteamFriends._GetChatMemberByIndex(this.Self, steamIDClan, iUser);
		}

		// Token: 0x0600017B RID: 379
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_SendClanChatMessage")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SendClanChatMessage(IntPtr self, SteamId steamIDClanChat, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchText);

		// Token: 0x0600017C RID: 380 RVA: 0x000050C8 File Offset: 0x000032C8
		internal bool SendClanChatMessage(SteamId steamIDClanChat, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchText)
		{
			return ISteamFriends._SendClanChatMessage(this.Self, steamIDClanChat, pchText);
		}

		// Token: 0x0600017D RID: 381
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanChatMessage")]
		private static extern int _GetClanChatMessage(IntPtr self, SteamId steamIDClanChat, int iMessage, IntPtr prgchText, int cchTextMax, ref ChatEntryType peChatEntryType, ref SteamId psteamidChatter);

		// Token: 0x0600017E RID: 382 RVA: 0x000050EC File Offset: 0x000032EC
		internal int GetClanChatMessage(SteamId steamIDClanChat, int iMessage, IntPtr prgchText, int cchTextMax, ref ChatEntryType peChatEntryType, ref SteamId psteamidChatter)
		{
			return ISteamFriends._GetClanChatMessage(this.Self, steamIDClanChat, iMessage, prgchText, cchTextMax, ref peChatEntryType, ref psteamidChatter);
		}

		// Token: 0x0600017F RID: 383
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_IsClanChatAdmin")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsClanChatAdmin(IntPtr self, SteamId steamIDClanChat, SteamId steamIDUser);

		// Token: 0x06000180 RID: 384 RVA: 0x00005114 File Offset: 0x00003314
		internal bool IsClanChatAdmin(SteamId steamIDClanChat, SteamId steamIDUser)
		{
			return ISteamFriends._IsClanChatAdmin(this.Self, steamIDClanChat, steamIDUser);
		}

		// Token: 0x06000181 RID: 385
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_IsClanChatWindowOpenInSteam")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsClanChatWindowOpenInSteam(IntPtr self, SteamId steamIDClanChat);

		// Token: 0x06000182 RID: 386 RVA: 0x00005138 File Offset: 0x00003338
		internal bool IsClanChatWindowOpenInSteam(SteamId steamIDClanChat)
		{
			return ISteamFriends._IsClanChatWindowOpenInSteam(this.Self, steamIDClanChat);
		}

		// Token: 0x06000183 RID: 387
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_OpenClanChatWindowInSteam")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _OpenClanChatWindowInSteam(IntPtr self, SteamId steamIDClanChat);

		// Token: 0x06000184 RID: 388 RVA: 0x00005158 File Offset: 0x00003358
		internal bool OpenClanChatWindowInSteam(SteamId steamIDClanChat)
		{
			return ISteamFriends._OpenClanChatWindowInSteam(this.Self, steamIDClanChat);
		}

		// Token: 0x06000185 RID: 389
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_CloseClanChatWindowInSteam")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _CloseClanChatWindowInSteam(IntPtr self, SteamId steamIDClanChat);

		// Token: 0x06000186 RID: 390 RVA: 0x00005178 File Offset: 0x00003378
		internal bool CloseClanChatWindowInSteam(SteamId steamIDClanChat)
		{
			return ISteamFriends._CloseClanChatWindowInSteam(this.Self, steamIDClanChat);
		}

		// Token: 0x06000187 RID: 391
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_SetListenForFriendsMessages")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetListenForFriendsMessages(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bInterceptEnabled);

		// Token: 0x06000188 RID: 392 RVA: 0x00005198 File Offset: 0x00003398
		internal bool SetListenForFriendsMessages([MarshalAs(UnmanagedType.U1)] bool bInterceptEnabled)
		{
			return ISteamFriends._SetListenForFriendsMessages(this.Self, bInterceptEnabled);
		}

		// Token: 0x06000189 RID: 393
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ReplyToFriendMessage")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ReplyToFriendMessage(IntPtr self, SteamId steamIDFriend, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchMsgToSend);

		// Token: 0x0600018A RID: 394 RVA: 0x000051B8 File Offset: 0x000033B8
		internal bool ReplyToFriendMessage(SteamId steamIDFriend, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchMsgToSend)
		{
			return ISteamFriends._ReplyToFriendMessage(this.Self, steamIDFriend, pchMsgToSend);
		}

		// Token: 0x0600018B RID: 395
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendMessage")]
		private static extern int _GetFriendMessage(IntPtr self, SteamId steamIDFriend, int iMessageID, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType);

		// Token: 0x0600018C RID: 396 RVA: 0x000051DC File Offset: 0x000033DC
		internal int GetFriendMessage(SteamId steamIDFriend, int iMessageID, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType)
		{
			return ISteamFriends._GetFriendMessage(this.Self, steamIDFriend, iMessageID, pvData, cubData, ref peChatEntryType);
		}

		// Token: 0x0600018D RID: 397
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFollowerCount")]
		private static extern SteamAPICall_t _GetFollowerCount(IntPtr self, SteamId steamID);

		// Token: 0x0600018E RID: 398 RVA: 0x00005204 File Offset: 0x00003404
		internal CallResult<FriendsGetFollowerCount_t> GetFollowerCount(SteamId steamID)
		{
			SteamAPICall_t call = ISteamFriends._GetFollowerCount(this.Self, steamID);
			return new CallResult<FriendsGetFollowerCount_t>(call, base.IsServer);
		}

		// Token: 0x0600018F RID: 399
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_IsFollowing")]
		private static extern SteamAPICall_t _IsFollowing(IntPtr self, SteamId steamID);

		// Token: 0x06000190 RID: 400 RVA: 0x00005230 File Offset: 0x00003430
		internal CallResult<FriendsIsFollowing_t> IsFollowing(SteamId steamID)
		{
			SteamAPICall_t call = ISteamFriends._IsFollowing(this.Self, steamID);
			return new CallResult<FriendsIsFollowing_t>(call, base.IsServer);
		}

		// Token: 0x06000191 RID: 401
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_EnumerateFollowingList")]
		private static extern SteamAPICall_t _EnumerateFollowingList(IntPtr self, uint unStartIndex);

		// Token: 0x06000192 RID: 402 RVA: 0x0000525C File Offset: 0x0000345C
		internal CallResult<FriendsEnumerateFollowingList_t> EnumerateFollowingList(uint unStartIndex)
		{
			SteamAPICall_t call = ISteamFriends._EnumerateFollowingList(this.Self, unStartIndex);
			return new CallResult<FriendsEnumerateFollowingList_t>(call, base.IsServer);
		}

		// Token: 0x06000193 RID: 403
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_IsClanPublic")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsClanPublic(IntPtr self, SteamId steamIDClan);

		// Token: 0x06000194 RID: 404 RVA: 0x00005288 File Offset: 0x00003488
		internal bool IsClanPublic(SteamId steamIDClan)
		{
			return ISteamFriends._IsClanPublic(this.Self, steamIDClan);
		}

		// Token: 0x06000195 RID: 405
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_IsClanOfficialGameGroup")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsClanOfficialGameGroup(IntPtr self, SteamId steamIDClan);

		// Token: 0x06000196 RID: 406 RVA: 0x000052A8 File Offset: 0x000034A8
		internal bool IsClanOfficialGameGroup(SteamId steamIDClan)
		{
			return ISteamFriends._IsClanOfficialGameGroup(this.Self, steamIDClan);
		}

		// Token: 0x06000197 RID: 407
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetNumChatsWithUnreadPriorityMessages")]
		private static extern int _GetNumChatsWithUnreadPriorityMessages(IntPtr self);

		// Token: 0x06000198 RID: 408 RVA: 0x000052C8 File Offset: 0x000034C8
		internal int GetNumChatsWithUnreadPriorityMessages()
		{
			return ISteamFriends._GetNumChatsWithUnreadPriorityMessages(this.Self);
		}

		// Token: 0x06000199 RID: 409
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayRemotePlayTogetherInviteDialog")]
		private static extern void _ActivateGameOverlayRemotePlayTogetherInviteDialog(IntPtr self, SteamId steamIDLobby);

		// Token: 0x0600019A RID: 410 RVA: 0x000052E7 File Offset: 0x000034E7
		internal void ActivateGameOverlayRemotePlayTogetherInviteDialog(SteamId steamIDLobby)
		{
			ISteamFriends._ActivateGameOverlayRemotePlayTogetherInviteDialog(this.Self, steamIDLobby);
		}
	}
}
