using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200002D RID: 45
	internal class ISteamUser : SteamInterface
	{
		// Token: 0x06000624 RID: 1572 RVA: 0x000099AF File Offset: 0x00007BAF
		internal ISteamUser(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x06000625 RID: 1573
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamUser_v020();

		// Token: 0x06000626 RID: 1574 RVA: 0x000099C1 File Offset: 0x00007BC1
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamUser.SteamAPI_SteamUser_v020();
		}

		// Token: 0x06000627 RID: 1575
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetHSteamUser")]
		private static extern HSteamUser _GetHSteamUser(IntPtr self);

		// Token: 0x06000628 RID: 1576 RVA: 0x000099C8 File Offset: 0x00007BC8
		internal HSteamUser GetHSteamUser()
		{
			return ISteamUser._GetHSteamUser(this.Self);
		}

		// Token: 0x06000629 RID: 1577
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BLoggedOn")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BLoggedOn(IntPtr self);

		// Token: 0x0600062A RID: 1578 RVA: 0x000099E8 File Offset: 0x00007BE8
		internal bool BLoggedOn()
		{
			return ISteamUser._BLoggedOn(this.Self);
		}

		// Token: 0x0600062B RID: 1579
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetSteamID")]
		private static extern SteamId _GetSteamID(IntPtr self);

		// Token: 0x0600062C RID: 1580 RVA: 0x00009A08 File Offset: 0x00007C08
		internal SteamId GetSteamID()
		{
			return ISteamUser._GetSteamID(this.Self);
		}

		// Token: 0x0600062D RID: 1581
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_InitiateGameConnection")]
		private static extern int _InitiateGameConnection(IntPtr self, IntPtr pAuthBlob, int cbMaxAuthBlob, SteamId steamIDGameServer, uint unIPServer, ushort usPortServer, [MarshalAs(UnmanagedType.U1)] bool bSecure);

		// Token: 0x0600062E RID: 1582 RVA: 0x00009A28 File Offset: 0x00007C28
		internal int InitiateGameConnection(IntPtr pAuthBlob, int cbMaxAuthBlob, SteamId steamIDGameServer, uint unIPServer, ushort usPortServer, [MarshalAs(UnmanagedType.U1)] bool bSecure)
		{
			return ISteamUser._InitiateGameConnection(this.Self, pAuthBlob, cbMaxAuthBlob, steamIDGameServer, unIPServer, usPortServer, bSecure);
		}

		// Token: 0x0600062F RID: 1583
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_TerminateGameConnection")]
		private static extern void _TerminateGameConnection(IntPtr self, uint unIPServer, ushort usPortServer);

		// Token: 0x06000630 RID: 1584 RVA: 0x00009A50 File Offset: 0x00007C50
		internal void TerminateGameConnection(uint unIPServer, ushort usPortServer)
		{
			ISteamUser._TerminateGameConnection(this.Self, unIPServer, usPortServer);
		}

		// Token: 0x06000631 RID: 1585
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_TrackAppUsageEvent")]
		private static extern void _TrackAppUsageEvent(IntPtr self, GameId gameID, int eAppUsageEvent, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchExtraInfo);

		// Token: 0x06000632 RID: 1586 RVA: 0x00009A61 File Offset: 0x00007C61
		internal void TrackAppUsageEvent(GameId gameID, int eAppUsageEvent, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchExtraInfo)
		{
			ISteamUser._TrackAppUsageEvent(this.Self, gameID, eAppUsageEvent, pchExtraInfo);
		}

		// Token: 0x06000633 RID: 1587
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetUserDataFolder")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetUserDataFolder(IntPtr self, IntPtr pchBuffer, int cubBuffer);

		// Token: 0x06000634 RID: 1588 RVA: 0x00009A74 File Offset: 0x00007C74
		internal bool GetUserDataFolder(out string pchBuffer)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			bool result = ISteamUser._GetUserDataFolder(this.Self, intPtr, 32768);
			pchBuffer = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x06000635 RID: 1589
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_StartVoiceRecording")]
		private static extern void _StartVoiceRecording(IntPtr self);

		// Token: 0x06000636 RID: 1590 RVA: 0x00009AA7 File Offset: 0x00007CA7
		internal void StartVoiceRecording()
		{
			ISteamUser._StartVoiceRecording(this.Self);
		}

		// Token: 0x06000637 RID: 1591
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_StopVoiceRecording")]
		private static extern void _StopVoiceRecording(IntPtr self);

		// Token: 0x06000638 RID: 1592 RVA: 0x00009AB6 File Offset: 0x00007CB6
		internal void StopVoiceRecording()
		{
			ISteamUser._StopVoiceRecording(this.Self);
		}

		// Token: 0x06000639 RID: 1593
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetAvailableVoice")]
		private static extern VoiceResult _GetAvailableVoice(IntPtr self, ref uint pcbCompressed, ref uint pcbUncompressed_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated);

		// Token: 0x0600063A RID: 1594 RVA: 0x00009AC8 File Offset: 0x00007CC8
		internal VoiceResult GetAvailableVoice(ref uint pcbCompressed, ref uint pcbUncompressed_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated)
		{
			return ISteamUser._GetAvailableVoice(this.Self, ref pcbCompressed, ref pcbUncompressed_Deprecated, nUncompressedVoiceDesiredSampleRate_Deprecated);
		}

		// Token: 0x0600063B RID: 1595
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetVoice")]
		private static extern VoiceResult _GetVoice(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bWantCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, [MarshalAs(UnmanagedType.U1)] bool bWantUncompressed_Deprecated, IntPtr pUncompressedDestBuffer_Deprecated, uint cbUncompressedDestBufferSize_Deprecated, ref uint nUncompressBytesWritten_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated);

		// Token: 0x0600063C RID: 1596 RVA: 0x00009AEC File Offset: 0x00007CEC
		internal VoiceResult GetVoice([MarshalAs(UnmanagedType.U1)] bool bWantCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, [MarshalAs(UnmanagedType.U1)] bool bWantUncompressed_Deprecated, IntPtr pUncompressedDestBuffer_Deprecated, uint cbUncompressedDestBufferSize_Deprecated, ref uint nUncompressBytesWritten_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated)
		{
			return ISteamUser._GetVoice(this.Self, bWantCompressed, pDestBuffer, cbDestBufferSize, ref nBytesWritten, bWantUncompressed_Deprecated, pUncompressedDestBuffer_Deprecated, cbUncompressedDestBufferSize_Deprecated, ref nUncompressBytesWritten_Deprecated, nUncompressedVoiceDesiredSampleRate_Deprecated);
		}

		// Token: 0x0600063D RID: 1597
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_DecompressVoice")]
		private static extern VoiceResult _DecompressVoice(IntPtr self, IntPtr pCompressed, uint cbCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, uint nDesiredSampleRate);

		// Token: 0x0600063E RID: 1598 RVA: 0x00009B1C File Offset: 0x00007D1C
		internal VoiceResult DecompressVoice(IntPtr pCompressed, uint cbCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, uint nDesiredSampleRate)
		{
			return ISteamUser._DecompressVoice(this.Self, pCompressed, cbCompressed, pDestBuffer, cbDestBufferSize, ref nBytesWritten, nDesiredSampleRate);
		}

		// Token: 0x0600063F RID: 1599
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetVoiceOptimalSampleRate")]
		private static extern uint _GetVoiceOptimalSampleRate(IntPtr self);

		// Token: 0x06000640 RID: 1600 RVA: 0x00009B44 File Offset: 0x00007D44
		internal uint GetVoiceOptimalSampleRate()
		{
			return ISteamUser._GetVoiceOptimalSampleRate(this.Self);
		}

		// Token: 0x06000641 RID: 1601
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetAuthSessionTicket")]
		private static extern HAuthTicket _GetAuthSessionTicket(IntPtr self, IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket);

		// Token: 0x06000642 RID: 1602 RVA: 0x00009B64 File Offset: 0x00007D64
		internal HAuthTicket GetAuthSessionTicket(IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket)
		{
			return ISteamUser._GetAuthSessionTicket(this.Self, pTicket, cbMaxTicket, ref pcbTicket);
		}

		// Token: 0x06000643 RID: 1603
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BeginAuthSession")]
		private static extern BeginAuthResult _BeginAuthSession(IntPtr self, IntPtr pAuthTicket, int cbAuthTicket, SteamId steamID);

		// Token: 0x06000644 RID: 1604 RVA: 0x00009B88 File Offset: 0x00007D88
		internal BeginAuthResult BeginAuthSession(IntPtr pAuthTicket, int cbAuthTicket, SteamId steamID)
		{
			return ISteamUser._BeginAuthSession(this.Self, pAuthTicket, cbAuthTicket, steamID);
		}

		// Token: 0x06000645 RID: 1605
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_EndAuthSession")]
		private static extern void _EndAuthSession(IntPtr self, SteamId steamID);

		// Token: 0x06000646 RID: 1606 RVA: 0x00009BAA File Offset: 0x00007DAA
		internal void EndAuthSession(SteamId steamID)
		{
			ISteamUser._EndAuthSession(this.Self, steamID);
		}

		// Token: 0x06000647 RID: 1607
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_CancelAuthTicket")]
		private static extern void _CancelAuthTicket(IntPtr self, HAuthTicket hAuthTicket);

		// Token: 0x06000648 RID: 1608 RVA: 0x00009BBA File Offset: 0x00007DBA
		internal void CancelAuthTicket(HAuthTicket hAuthTicket)
		{
			ISteamUser._CancelAuthTicket(this.Self, hAuthTicket);
		}

		// Token: 0x06000649 RID: 1609
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_UserHasLicenseForApp")]
		private static extern UserHasLicenseForAppResult _UserHasLicenseForApp(IntPtr self, SteamId steamID, AppId appID);

		// Token: 0x0600064A RID: 1610 RVA: 0x00009BCC File Offset: 0x00007DCC
		internal UserHasLicenseForAppResult UserHasLicenseForApp(SteamId steamID, AppId appID)
		{
			return ISteamUser._UserHasLicenseForApp(this.Self, steamID, appID);
		}

		// Token: 0x0600064B RID: 1611
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BIsBehindNAT")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsBehindNAT(IntPtr self);

		// Token: 0x0600064C RID: 1612 RVA: 0x00009BF0 File Offset: 0x00007DF0
		internal bool BIsBehindNAT()
		{
			return ISteamUser._BIsBehindNAT(this.Self);
		}

		// Token: 0x0600064D RID: 1613
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_AdvertiseGame")]
		private static extern void _AdvertiseGame(IntPtr self, SteamId steamIDGameServer, uint unIPServer, ushort usPortServer);

		// Token: 0x0600064E RID: 1614 RVA: 0x00009C0F File Offset: 0x00007E0F
		internal void AdvertiseGame(SteamId steamIDGameServer, uint unIPServer, ushort usPortServer)
		{
			ISteamUser._AdvertiseGame(this.Self, steamIDGameServer, unIPServer, usPortServer);
		}

		// Token: 0x0600064F RID: 1615
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_RequestEncryptedAppTicket")]
		private static extern SteamAPICall_t _RequestEncryptedAppTicket(IntPtr self, IntPtr pDataToInclude, int cbDataToInclude);

		// Token: 0x06000650 RID: 1616 RVA: 0x00009C24 File Offset: 0x00007E24
		internal CallResult<EncryptedAppTicketResponse_t> RequestEncryptedAppTicket(IntPtr pDataToInclude, int cbDataToInclude)
		{
			SteamAPICall_t call = ISteamUser._RequestEncryptedAppTicket(this.Self, pDataToInclude, cbDataToInclude);
			return new CallResult<EncryptedAppTicketResponse_t>(call, base.IsServer);
		}

		// Token: 0x06000651 RID: 1617
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetEncryptedAppTicket")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetEncryptedAppTicket(IntPtr self, IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket);

		// Token: 0x06000652 RID: 1618 RVA: 0x00009C50 File Offset: 0x00007E50
		internal bool GetEncryptedAppTicket(IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket)
		{
			return ISteamUser._GetEncryptedAppTicket(this.Self, pTicket, cbMaxTicket, ref pcbTicket);
		}

		// Token: 0x06000653 RID: 1619
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetGameBadgeLevel")]
		private static extern int _GetGameBadgeLevel(IntPtr self, int nSeries, [MarshalAs(UnmanagedType.U1)] bool bFoil);

		// Token: 0x06000654 RID: 1620 RVA: 0x00009C74 File Offset: 0x00007E74
		internal int GetGameBadgeLevel(int nSeries, [MarshalAs(UnmanagedType.U1)] bool bFoil)
		{
			return ISteamUser._GetGameBadgeLevel(this.Self, nSeries, bFoil);
		}

		// Token: 0x06000655 RID: 1621
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetPlayerSteamLevel")]
		private static extern int _GetPlayerSteamLevel(IntPtr self);

		// Token: 0x06000656 RID: 1622 RVA: 0x00009C98 File Offset: 0x00007E98
		internal int GetPlayerSteamLevel()
		{
			return ISteamUser._GetPlayerSteamLevel(this.Self);
		}

		// Token: 0x06000657 RID: 1623
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_RequestStoreAuthURL")]
		private static extern SteamAPICall_t _RequestStoreAuthURL(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchRedirectURL);

		// Token: 0x06000658 RID: 1624 RVA: 0x00009CB8 File Offset: 0x00007EB8
		internal CallResult<StoreAuthURLResponse_t> RequestStoreAuthURL([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchRedirectURL)
		{
			SteamAPICall_t call = ISteamUser._RequestStoreAuthURL(this.Self, pchRedirectURL);
			return new CallResult<StoreAuthURLResponse_t>(call, base.IsServer);
		}

		// Token: 0x06000659 RID: 1625
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BIsPhoneVerified")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsPhoneVerified(IntPtr self);

		// Token: 0x0600065A RID: 1626 RVA: 0x00009CE4 File Offset: 0x00007EE4
		internal bool BIsPhoneVerified()
		{
			return ISteamUser._BIsPhoneVerified(this.Self);
		}

		// Token: 0x0600065B RID: 1627
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BIsTwoFactorEnabled")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsTwoFactorEnabled(IntPtr self);

		// Token: 0x0600065C RID: 1628 RVA: 0x00009D04 File Offset: 0x00007F04
		internal bool BIsTwoFactorEnabled()
		{
			return ISteamUser._BIsTwoFactorEnabled(this.Self);
		}

		// Token: 0x0600065D RID: 1629
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BIsPhoneIdentifying")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsPhoneIdentifying(IntPtr self);

		// Token: 0x0600065E RID: 1630 RVA: 0x00009D24 File Offset: 0x00007F24
		internal bool BIsPhoneIdentifying()
		{
			return ISteamUser._BIsPhoneIdentifying(this.Self);
		}

		// Token: 0x0600065F RID: 1631
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BIsPhoneRequiringVerification")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsPhoneRequiringVerification(IntPtr self);

		// Token: 0x06000660 RID: 1632 RVA: 0x00009D44 File Offset: 0x00007F44
		internal bool BIsPhoneRequiringVerification()
		{
			return ISteamUser._BIsPhoneRequiringVerification(this.Self);
		}

		// Token: 0x06000661 RID: 1633
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetMarketEligibility")]
		private static extern SteamAPICall_t _GetMarketEligibility(IntPtr self);

		// Token: 0x06000662 RID: 1634 RVA: 0x00009D64 File Offset: 0x00007F64
		internal CallResult<MarketEligibilityResponse_t> GetMarketEligibility()
		{
			SteamAPICall_t call = ISteamUser._GetMarketEligibility(this.Self);
			return new CallResult<MarketEligibilityResponse_t>(call, base.IsServer);
		}

		// Token: 0x06000663 RID: 1635
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetDurationControl")]
		private static extern SteamAPICall_t _GetDurationControl(IntPtr self);

		// Token: 0x06000664 RID: 1636 RVA: 0x00009D90 File Offset: 0x00007F90
		internal CallResult<DurationControl_t> GetDurationControl()
		{
			SteamAPICall_t call = ISteamUser._GetDurationControl(this.Self);
			return new CallResult<DurationControl_t>(call, base.IsServer);
		}
	}
}
