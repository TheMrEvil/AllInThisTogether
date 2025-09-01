using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000023 RID: 35
	public static class SteamUser
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x0000AE0D File Offset: 0x0000900D
		public static HSteamUser GetHSteamUser()
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamUser)NativeMethods.ISteamUser_GetHSteamUser(CSteamAPIContext.GetSteamUser());
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000AE23 File Offset: 0x00009023
		public static bool BLoggedOn()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_BLoggedOn(CSteamAPIContext.GetSteamUser());
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000AE34 File Offset: 0x00009034
		public static CSteamID GetSteamID()
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamUser_GetSteamID(CSteamAPIContext.GetSteamUser());
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000AE4A File Offset: 0x0000904A
		public static int InitiateGameConnection_DEPRECATED(byte[] pAuthBlob, int cbMaxAuthBlob, CSteamID steamIDGameServer, uint unIPServer, ushort usPortServer, bool bSecure)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_InitiateGameConnection_DEPRECATED(CSteamAPIContext.GetSteamUser(), pAuthBlob, cbMaxAuthBlob, steamIDGameServer, unIPServer, usPortServer, bSecure);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000AE63 File Offset: 0x00009063
		public static void TerminateGameConnection_DEPRECATED(uint unIPServer, ushort usPortServer)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUser_TerminateGameConnection_DEPRECATED(CSteamAPIContext.GetSteamUser(), unIPServer, usPortServer);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000AE78 File Offset: 0x00009078
		public static void TrackAppUsageEvent(CGameID gameID, int eAppUsageEvent, string pchExtraInfo = "")
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchExtraInfo))
			{
				NativeMethods.ISteamUser_TrackAppUsageEvent(CSteamAPIContext.GetSteamUser(), gameID, eAppUsageEvent, utf8StringHandle);
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000AEBC File Offset: 0x000090BC
		public static bool GetUserDataFolder(out string pchBuffer, int cubBuffer)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cubBuffer);
			bool flag = NativeMethods.ISteamUser_GetUserDataFolder(CSteamAPIContext.GetSteamUser(), intPtr, cubBuffer);
			pchBuffer = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000AEF7 File Offset: 0x000090F7
		public static void StartVoiceRecording()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUser_StartVoiceRecording(CSteamAPIContext.GetSteamUser());
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000AF08 File Offset: 0x00009108
		public static void StopVoiceRecording()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUser_StopVoiceRecording(CSteamAPIContext.GetSteamUser());
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000AF19 File Offset: 0x00009119
		public static EVoiceResult GetAvailableVoice(out uint pcbCompressed)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_GetAvailableVoice(CSteamAPIContext.GetSteamUser(), out pcbCompressed, IntPtr.Zero, 0U);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000AF34 File Offset: 0x00009134
		public static EVoiceResult GetVoice(bool bWantCompressed, byte[] pDestBuffer, uint cbDestBufferSize, out uint nBytesWritten)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_GetVoice(CSteamAPIContext.GetSteamUser(), bWantCompressed, pDestBuffer, cbDestBufferSize, out nBytesWritten, false, IntPtr.Zero, 0U, IntPtr.Zero, 0U);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000AF61 File Offset: 0x00009161
		public static EVoiceResult DecompressVoice(byte[] pCompressed, uint cbCompressed, byte[] pDestBuffer, uint cbDestBufferSize, out uint nBytesWritten, uint nDesiredSampleRate)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_DecompressVoice(CSteamAPIContext.GetSteamUser(), pCompressed, cbCompressed, pDestBuffer, cbDestBufferSize, out nBytesWritten, nDesiredSampleRate);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000AF7A File Offset: 0x0000917A
		public static uint GetVoiceOptimalSampleRate()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_GetVoiceOptimalSampleRate(CSteamAPIContext.GetSteamUser());
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000AF8B File Offset: 0x0000918B
		public static HAuthTicket GetAuthSessionTicket(byte[] pTicket, int cbMaxTicket, out uint pcbTicket, ref SteamNetworkingIdentity pSteamNetworkingIdentity)
		{
			InteropHelp.TestIfAvailableClient();
			return (HAuthTicket)NativeMethods.ISteamUser_GetAuthSessionTicket(CSteamAPIContext.GetSteamUser(), pTicket, cbMaxTicket, out pcbTicket, ref pSteamNetworkingIdentity);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000AFA8 File Offset: 0x000091A8
		public static HAuthTicket GetAuthTicketForWebApi(string pchIdentity)
		{
			InteropHelp.TestIfAvailableClient();
			HAuthTicket result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchIdentity))
			{
				result = (HAuthTicket)NativeMethods.ISteamUser_GetAuthTicketForWebApi(CSteamAPIContext.GetSteamUser(), utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000AFF0 File Offset: 0x000091F0
		public static EBeginAuthSessionResult BeginAuthSession(byte[] pAuthTicket, int cbAuthTicket, CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_BeginAuthSession(CSteamAPIContext.GetSteamUser(), pAuthTicket, cbAuthTicket, steamID);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000B004 File Offset: 0x00009204
		public static void EndAuthSession(CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUser_EndAuthSession(CSteamAPIContext.GetSteamUser(), steamID);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000B016 File Offset: 0x00009216
		public static void CancelAuthTicket(HAuthTicket hAuthTicket)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUser_CancelAuthTicket(CSteamAPIContext.GetSteamUser(), hAuthTicket);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000B028 File Offset: 0x00009228
		public static EUserHasLicenseForAppResult UserHasLicenseForApp(CSteamID steamID, AppId_t appID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_UserHasLicenseForApp(CSteamAPIContext.GetSteamUser(), steamID, appID);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000B03B File Offset: 0x0000923B
		public static bool BIsBehindNAT()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_BIsBehindNAT(CSteamAPIContext.GetSteamUser());
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000B04C File Offset: 0x0000924C
		public static void AdvertiseGame(CSteamID steamIDGameServer, uint unIPServer, ushort usPortServer)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUser_AdvertiseGame(CSteamAPIContext.GetSteamUser(), steamIDGameServer, unIPServer, usPortServer);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000B060 File Offset: 0x00009260
		public static SteamAPICall_t RequestEncryptedAppTicket(byte[] pDataToInclude, int cbDataToInclude)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUser_RequestEncryptedAppTicket(CSteamAPIContext.GetSteamUser(), pDataToInclude, cbDataToInclude);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000B078 File Offset: 0x00009278
		public static bool GetEncryptedAppTicket(byte[] pTicket, int cbMaxTicket, out uint pcbTicket)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_GetEncryptedAppTicket(CSteamAPIContext.GetSteamUser(), pTicket, cbMaxTicket, out pcbTicket);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000B08C File Offset: 0x0000928C
		public static int GetGameBadgeLevel(int nSeries, bool bFoil)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_GetGameBadgeLevel(CSteamAPIContext.GetSteamUser(), nSeries, bFoil);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000B09F File Offset: 0x0000929F
		public static int GetPlayerSteamLevel()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_GetPlayerSteamLevel(CSteamAPIContext.GetSteamUser());
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000B0B0 File Offset: 0x000092B0
		public static SteamAPICall_t RequestStoreAuthURL(string pchRedirectURL)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchRedirectURL))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamUser_RequestStoreAuthURL(CSteamAPIContext.GetSteamUser(), utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000B0F8 File Offset: 0x000092F8
		public static bool BIsPhoneVerified()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_BIsPhoneVerified(CSteamAPIContext.GetSteamUser());
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000B109 File Offset: 0x00009309
		public static bool BIsTwoFactorEnabled()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_BIsTwoFactorEnabled(CSteamAPIContext.GetSteamUser());
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000B11A File Offset: 0x0000931A
		public static bool BIsPhoneIdentifying()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_BIsPhoneIdentifying(CSteamAPIContext.GetSteamUser());
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000B12B File Offset: 0x0000932B
		public static bool BIsPhoneRequiringVerification()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_BIsPhoneRequiringVerification(CSteamAPIContext.GetSteamUser());
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000B13C File Offset: 0x0000933C
		public static SteamAPICall_t GetMarketEligibility()
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUser_GetMarketEligibility(CSteamAPIContext.GetSteamUser());
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000B152 File Offset: 0x00009352
		public static SteamAPICall_t GetDurationControl()
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUser_GetDurationControl(CSteamAPIContext.GetSteamUser());
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000B168 File Offset: 0x00009368
		public static bool BSetDurationControlOnlineState(EDurationControlOnlineState eNewState)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUser_BSetDurationControlOnlineState(CSteamAPIContext.GetSteamUser(), eNewState);
		}
	}
}
