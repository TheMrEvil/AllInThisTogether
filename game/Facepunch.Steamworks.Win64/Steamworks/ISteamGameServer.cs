using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000013 RID: 19
	internal class ISteamGameServer : SteamInterface
	{
		// Token: 0x060001BA RID: 442 RVA: 0x00005500 File Offset: 0x00003700
		internal ISteamGameServer(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x060001BB RID: 443
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamGameServer_v013();

		// Token: 0x060001BC RID: 444 RVA: 0x00005512 File Offset: 0x00003712
		public override IntPtr GetServerInterfacePointer()
		{
			return ISteamGameServer.SteamAPI_SteamGameServer_v013();
		}

		// Token: 0x060001BD RID: 445
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetProduct")]
		private static extern void _SetProduct(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszProduct);

		// Token: 0x060001BE RID: 446 RVA: 0x00005519 File Offset: 0x00003719
		internal void SetProduct([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszProduct)
		{
			ISteamGameServer._SetProduct(this.Self, pszProduct);
		}

		// Token: 0x060001BF RID: 447
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetGameDescription")]
		private static extern void _SetGameDescription(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszGameDescription);

		// Token: 0x060001C0 RID: 448 RVA: 0x00005529 File Offset: 0x00003729
		internal void SetGameDescription([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszGameDescription)
		{
			ISteamGameServer._SetGameDescription(this.Self, pszGameDescription);
		}

		// Token: 0x060001C1 RID: 449
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetModDir")]
		private static extern void _SetModDir(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszModDir);

		// Token: 0x060001C2 RID: 450 RVA: 0x00005539 File Offset: 0x00003739
		internal void SetModDir([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszModDir)
		{
			ISteamGameServer._SetModDir(this.Self, pszModDir);
		}

		// Token: 0x060001C3 RID: 451
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetDedicatedServer")]
		private static extern void _SetDedicatedServer(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bDedicated);

		// Token: 0x060001C4 RID: 452 RVA: 0x00005549 File Offset: 0x00003749
		internal void SetDedicatedServer([MarshalAs(UnmanagedType.U1)] bool bDedicated)
		{
			ISteamGameServer._SetDedicatedServer(this.Self, bDedicated);
		}

		// Token: 0x060001C5 RID: 453
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_LogOn")]
		private static extern void _LogOn(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszToken);

		// Token: 0x060001C6 RID: 454 RVA: 0x00005559 File Offset: 0x00003759
		internal void LogOn([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszToken)
		{
			ISteamGameServer._LogOn(this.Self, pszToken);
		}

		// Token: 0x060001C7 RID: 455
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_LogOnAnonymous")]
		private static extern void _LogOnAnonymous(IntPtr self);

		// Token: 0x060001C8 RID: 456 RVA: 0x00005569 File Offset: 0x00003769
		internal void LogOnAnonymous()
		{
			ISteamGameServer._LogOnAnonymous(this.Self);
		}

		// Token: 0x060001C9 RID: 457
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_LogOff")]
		private static extern void _LogOff(IntPtr self);

		// Token: 0x060001CA RID: 458 RVA: 0x00005578 File Offset: 0x00003778
		internal void LogOff()
		{
			ISteamGameServer._LogOff(this.Self);
		}

		// Token: 0x060001CB RID: 459
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_BLoggedOn")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BLoggedOn(IntPtr self);

		// Token: 0x060001CC RID: 460 RVA: 0x00005588 File Offset: 0x00003788
		internal bool BLoggedOn()
		{
			return ISteamGameServer._BLoggedOn(this.Self);
		}

		// Token: 0x060001CD RID: 461
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_BSecure")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BSecure(IntPtr self);

		// Token: 0x060001CE RID: 462 RVA: 0x000055A8 File Offset: 0x000037A8
		internal bool BSecure()
		{
			return ISteamGameServer._BSecure(this.Self);
		}

		// Token: 0x060001CF RID: 463
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetSteamID")]
		private static extern SteamId _GetSteamID(IntPtr self);

		// Token: 0x060001D0 RID: 464 RVA: 0x000055C8 File Offset: 0x000037C8
		internal SteamId GetSteamID()
		{
			return ISteamGameServer._GetSteamID(this.Self);
		}

		// Token: 0x060001D1 RID: 465
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_WasRestartRequested")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _WasRestartRequested(IntPtr self);

		// Token: 0x060001D2 RID: 466 RVA: 0x000055E8 File Offset: 0x000037E8
		internal bool WasRestartRequested()
		{
			return ISteamGameServer._WasRestartRequested(this.Self);
		}

		// Token: 0x060001D3 RID: 467
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetMaxPlayerCount")]
		private static extern void _SetMaxPlayerCount(IntPtr self, int cPlayersMax);

		// Token: 0x060001D4 RID: 468 RVA: 0x00005607 File Offset: 0x00003807
		internal void SetMaxPlayerCount(int cPlayersMax)
		{
			ISteamGameServer._SetMaxPlayerCount(this.Self, cPlayersMax);
		}

		// Token: 0x060001D5 RID: 469
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetBotPlayerCount")]
		private static extern void _SetBotPlayerCount(IntPtr self, int cBotplayers);

		// Token: 0x060001D6 RID: 470 RVA: 0x00005617 File Offset: 0x00003817
		internal void SetBotPlayerCount(int cBotplayers)
		{
			ISteamGameServer._SetBotPlayerCount(this.Self, cBotplayers);
		}

		// Token: 0x060001D7 RID: 471
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetServerName")]
		private static extern void _SetServerName(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszServerName);

		// Token: 0x060001D8 RID: 472 RVA: 0x00005627 File Offset: 0x00003827
		internal void SetServerName([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszServerName)
		{
			ISteamGameServer._SetServerName(this.Self, pszServerName);
		}

		// Token: 0x060001D9 RID: 473
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetMapName")]
		private static extern void _SetMapName(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszMapName);

		// Token: 0x060001DA RID: 474 RVA: 0x00005637 File Offset: 0x00003837
		internal void SetMapName([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszMapName)
		{
			ISteamGameServer._SetMapName(this.Self, pszMapName);
		}

		// Token: 0x060001DB RID: 475
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetPasswordProtected")]
		private static extern void _SetPasswordProtected(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bPasswordProtected);

		// Token: 0x060001DC RID: 476 RVA: 0x00005647 File Offset: 0x00003847
		internal void SetPasswordProtected([MarshalAs(UnmanagedType.U1)] bool bPasswordProtected)
		{
			ISteamGameServer._SetPasswordProtected(this.Self, bPasswordProtected);
		}

		// Token: 0x060001DD RID: 477
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetSpectatorPort")]
		private static extern void _SetSpectatorPort(IntPtr self, ushort unSpectatorPort);

		// Token: 0x060001DE RID: 478 RVA: 0x00005657 File Offset: 0x00003857
		internal void SetSpectatorPort(ushort unSpectatorPort)
		{
			ISteamGameServer._SetSpectatorPort(this.Self, unSpectatorPort);
		}

		// Token: 0x060001DF RID: 479
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetSpectatorServerName")]
		private static extern void _SetSpectatorServerName(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszSpectatorServerName);

		// Token: 0x060001E0 RID: 480 RVA: 0x00005667 File Offset: 0x00003867
		internal void SetSpectatorServerName([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszSpectatorServerName)
		{
			ISteamGameServer._SetSpectatorServerName(this.Self, pszSpectatorServerName);
		}

		// Token: 0x060001E1 RID: 481
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_ClearAllKeyValues")]
		private static extern void _ClearAllKeyValues(IntPtr self);

		// Token: 0x060001E2 RID: 482 RVA: 0x00005677 File Offset: 0x00003877
		internal void ClearAllKeyValues()
		{
			ISteamGameServer._ClearAllKeyValues(this.Self);
		}

		// Token: 0x060001E3 RID: 483
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetKeyValue")]
		private static extern void _SetKeyValue(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pValue);

		// Token: 0x060001E4 RID: 484 RVA: 0x00005686 File Offset: 0x00003886
		internal void SetKeyValue([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pValue)
		{
			ISteamGameServer._SetKeyValue(this.Self, pKey, pValue);
		}

		// Token: 0x060001E5 RID: 485
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetGameTags")]
		private static extern void _SetGameTags(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchGameTags);

		// Token: 0x060001E6 RID: 486 RVA: 0x00005697 File Offset: 0x00003897
		internal void SetGameTags([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchGameTags)
		{
			ISteamGameServer._SetGameTags(this.Self, pchGameTags);
		}

		// Token: 0x060001E7 RID: 487
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetGameData")]
		private static extern void _SetGameData(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchGameData);

		// Token: 0x060001E8 RID: 488 RVA: 0x000056A7 File Offset: 0x000038A7
		internal void SetGameData([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchGameData)
		{
			ISteamGameServer._SetGameData(this.Self, pchGameData);
		}

		// Token: 0x060001E9 RID: 489
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetRegion")]
		private static extern void _SetRegion(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszRegion);

		// Token: 0x060001EA RID: 490 RVA: 0x000056B7 File Offset: 0x000038B7
		internal void SetRegion([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszRegion)
		{
			ISteamGameServer._SetRegion(this.Self, pszRegion);
		}

		// Token: 0x060001EB RID: 491
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SendUserConnectAndAuthenticate")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SendUserConnectAndAuthenticate(IntPtr self, uint unIPClient, IntPtr pvAuthBlob, uint cubAuthBlobSize, ref SteamId pSteamIDUser);

		// Token: 0x060001EC RID: 492 RVA: 0x000056C8 File Offset: 0x000038C8
		internal bool SendUserConnectAndAuthenticate(uint unIPClient, IntPtr pvAuthBlob, uint cubAuthBlobSize, ref SteamId pSteamIDUser)
		{
			return ISteamGameServer._SendUserConnectAndAuthenticate(this.Self, unIPClient, pvAuthBlob, cubAuthBlobSize, ref pSteamIDUser);
		}

		// Token: 0x060001ED RID: 493
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_CreateUnauthenticatedUserConnection")]
		private static extern SteamId _CreateUnauthenticatedUserConnection(IntPtr self);

		// Token: 0x060001EE RID: 494 RVA: 0x000056EC File Offset: 0x000038EC
		internal SteamId CreateUnauthenticatedUserConnection()
		{
			return ISteamGameServer._CreateUnauthenticatedUserConnection(this.Self);
		}

		// Token: 0x060001EF RID: 495
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SendUserDisconnect")]
		private static extern void _SendUserDisconnect(IntPtr self, SteamId steamIDUser);

		// Token: 0x060001F0 RID: 496 RVA: 0x0000570B File Offset: 0x0000390B
		internal void SendUserDisconnect(SteamId steamIDUser)
		{
			ISteamGameServer._SendUserDisconnect(this.Self, steamIDUser);
		}

		// Token: 0x060001F1 RID: 497
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_BUpdateUserData")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BUpdateUserData(IntPtr self, SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPlayerName, uint uScore);

		// Token: 0x060001F2 RID: 498 RVA: 0x0000571C File Offset: 0x0000391C
		internal bool BUpdateUserData(SteamId steamIDUser, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPlayerName, uint uScore)
		{
			return ISteamGameServer._BUpdateUserData(this.Self, steamIDUser, pchPlayerName, uScore);
		}

		// Token: 0x060001F3 RID: 499
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetAuthSessionTicket")]
		private static extern HAuthTicket _GetAuthSessionTicket(IntPtr self, IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket);

		// Token: 0x060001F4 RID: 500 RVA: 0x00005740 File Offset: 0x00003940
		internal HAuthTicket GetAuthSessionTicket(IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket)
		{
			return ISteamGameServer._GetAuthSessionTicket(this.Self, pTicket, cbMaxTicket, ref pcbTicket);
		}

		// Token: 0x060001F5 RID: 501
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_BeginAuthSession")]
		private static extern BeginAuthResult _BeginAuthSession(IntPtr self, IntPtr pAuthTicket, int cbAuthTicket, SteamId steamID);

		// Token: 0x060001F6 RID: 502 RVA: 0x00005764 File Offset: 0x00003964
		internal BeginAuthResult BeginAuthSession(IntPtr pAuthTicket, int cbAuthTicket, SteamId steamID)
		{
			return ISteamGameServer._BeginAuthSession(this.Self, pAuthTicket, cbAuthTicket, steamID);
		}

		// Token: 0x060001F7 RID: 503
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_EndAuthSession")]
		private static extern void _EndAuthSession(IntPtr self, SteamId steamID);

		// Token: 0x060001F8 RID: 504 RVA: 0x00005786 File Offset: 0x00003986
		internal void EndAuthSession(SteamId steamID)
		{
			ISteamGameServer._EndAuthSession(this.Self, steamID);
		}

		// Token: 0x060001F9 RID: 505
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_CancelAuthTicket")]
		private static extern void _CancelAuthTicket(IntPtr self, HAuthTicket hAuthTicket);

		// Token: 0x060001FA RID: 506 RVA: 0x00005796 File Offset: 0x00003996
		internal void CancelAuthTicket(HAuthTicket hAuthTicket)
		{
			ISteamGameServer._CancelAuthTicket(this.Self, hAuthTicket);
		}

		// Token: 0x060001FB RID: 507
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_UserHasLicenseForApp")]
		private static extern UserHasLicenseForAppResult _UserHasLicenseForApp(IntPtr self, SteamId steamID, AppId appID);

		// Token: 0x060001FC RID: 508 RVA: 0x000057A8 File Offset: 0x000039A8
		internal UserHasLicenseForAppResult UserHasLicenseForApp(SteamId steamID, AppId appID)
		{
			return ISteamGameServer._UserHasLicenseForApp(this.Self, steamID, appID);
		}

		// Token: 0x060001FD RID: 509
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_RequestUserGroupStatus")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _RequestUserGroupStatus(IntPtr self, SteamId steamIDUser, SteamId steamIDGroup);

		// Token: 0x060001FE RID: 510 RVA: 0x000057CC File Offset: 0x000039CC
		internal bool RequestUserGroupStatus(SteamId steamIDUser, SteamId steamIDGroup)
		{
			return ISteamGameServer._RequestUserGroupStatus(this.Self, steamIDUser, steamIDGroup);
		}

		// Token: 0x060001FF RID: 511
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetGameplayStats")]
		private static extern void _GetGameplayStats(IntPtr self);

		// Token: 0x06000200 RID: 512 RVA: 0x000057ED File Offset: 0x000039ED
		internal void GetGameplayStats()
		{
			ISteamGameServer._GetGameplayStats(this.Self);
		}

		// Token: 0x06000201 RID: 513
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetServerReputation")]
		private static extern SteamAPICall_t _GetServerReputation(IntPtr self);

		// Token: 0x06000202 RID: 514 RVA: 0x000057FC File Offset: 0x000039FC
		internal CallResult<GSReputation_t> GetServerReputation()
		{
			SteamAPICall_t call = ISteamGameServer._GetServerReputation(this.Self);
			return new CallResult<GSReputation_t>(call, base.IsServer);
		}

		// Token: 0x06000203 RID: 515
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetPublicIP")]
		private static extern SteamIPAddress _GetPublicIP(IntPtr self);

		// Token: 0x06000204 RID: 516 RVA: 0x00005828 File Offset: 0x00003A28
		internal SteamIPAddress GetPublicIP()
		{
			return ISteamGameServer._GetPublicIP(this.Self);
		}

		// Token: 0x06000205 RID: 517
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_HandleIncomingPacket")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _HandleIncomingPacket(IntPtr self, IntPtr pData, int cbData, uint srcIP, ushort srcPort);

		// Token: 0x06000206 RID: 518 RVA: 0x00005848 File Offset: 0x00003A48
		internal bool HandleIncomingPacket(IntPtr pData, int cbData, uint srcIP, ushort srcPort)
		{
			return ISteamGameServer._HandleIncomingPacket(this.Self, pData, cbData, srcIP, srcPort);
		}

		// Token: 0x06000207 RID: 519
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetNextOutgoingPacket")]
		private static extern int _GetNextOutgoingPacket(IntPtr self, IntPtr pOut, int cbMaxOut, ref uint pNetAdr, ref ushort pPort);

		// Token: 0x06000208 RID: 520 RVA: 0x0000586C File Offset: 0x00003A6C
		internal int GetNextOutgoingPacket(IntPtr pOut, int cbMaxOut, ref uint pNetAdr, ref ushort pPort)
		{
			return ISteamGameServer._GetNextOutgoingPacket(this.Self, pOut, cbMaxOut, ref pNetAdr, ref pPort);
		}

		// Token: 0x06000209 RID: 521
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_EnableHeartbeats")]
		private static extern void _EnableHeartbeats(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bActive);

		// Token: 0x0600020A RID: 522 RVA: 0x00005890 File Offset: 0x00003A90
		internal void EnableHeartbeats([MarshalAs(UnmanagedType.U1)] bool bActive)
		{
			ISteamGameServer._EnableHeartbeats(this.Self, bActive);
		}

		// Token: 0x0600020B RID: 523
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetHeartbeatInterval")]
		private static extern void _SetHeartbeatInterval(IntPtr self, int iHeartbeatInterval);

		// Token: 0x0600020C RID: 524 RVA: 0x000058A0 File Offset: 0x00003AA0
		internal void SetHeartbeatInterval(int iHeartbeatInterval)
		{
			ISteamGameServer._SetHeartbeatInterval(this.Self, iHeartbeatInterval);
		}

		// Token: 0x0600020D RID: 525
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_ForceHeartbeat")]
		private static extern void _ForceHeartbeat(IntPtr self);

		// Token: 0x0600020E RID: 526 RVA: 0x000058B0 File Offset: 0x00003AB0
		internal void ForceHeartbeat()
		{
			ISteamGameServer._ForceHeartbeat(this.Self);
		}

		// Token: 0x0600020F RID: 527
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_AssociateWithClan")]
		private static extern SteamAPICall_t _AssociateWithClan(IntPtr self, SteamId steamIDClan);

		// Token: 0x06000210 RID: 528 RVA: 0x000058C0 File Offset: 0x00003AC0
		internal CallResult<AssociateWithClanResult_t> AssociateWithClan(SteamId steamIDClan)
		{
			SteamAPICall_t call = ISteamGameServer._AssociateWithClan(this.Self, steamIDClan);
			return new CallResult<AssociateWithClanResult_t>(call, base.IsServer);
		}

		// Token: 0x06000211 RID: 529
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_ComputeNewPlayerCompatibility")]
		private static extern SteamAPICall_t _ComputeNewPlayerCompatibility(IntPtr self, SteamId steamIDNewPlayer);

		// Token: 0x06000212 RID: 530 RVA: 0x000058EC File Offset: 0x00003AEC
		internal CallResult<ComputeNewPlayerCompatibilityResult_t> ComputeNewPlayerCompatibility(SteamId steamIDNewPlayer)
		{
			SteamAPICall_t call = ISteamGameServer._ComputeNewPlayerCompatibility(this.Self, steamIDNewPlayer);
			return new CallResult<ComputeNewPlayerCompatibilityResult_t>(call, base.IsServer);
		}
	}
}
