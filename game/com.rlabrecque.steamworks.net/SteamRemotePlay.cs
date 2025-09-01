using System;

namespace Steamworks
{
	// Token: 0x0200001F RID: 31
	public static class SteamRemotePlay
	{
		// Token: 0x06000385 RID: 901 RVA: 0x0000942A File Offset: 0x0000762A
		public static uint GetSessionCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemotePlay_GetSessionCount(CSteamAPIContext.GetSteamRemotePlay());
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000943B File Offset: 0x0000763B
		public static RemotePlaySessionID_t GetSessionID(int iSessionIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (RemotePlaySessionID_t)NativeMethods.ISteamRemotePlay_GetSessionID(CSteamAPIContext.GetSteamRemotePlay(), iSessionIndex);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00009452 File Offset: 0x00007652
		public static CSteamID GetSessionSteamID(RemotePlaySessionID_t unSessionID)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamRemotePlay_GetSessionSteamID(CSteamAPIContext.GetSteamRemotePlay(), unSessionID);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00009469 File Offset: 0x00007669
		public static string GetSessionClientName(RemotePlaySessionID_t unSessionID)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamRemotePlay_GetSessionClientName(CSteamAPIContext.GetSteamRemotePlay(), unSessionID));
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00009480 File Offset: 0x00007680
		public static ESteamDeviceFormFactor GetSessionClientFormFactor(RemotePlaySessionID_t unSessionID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemotePlay_GetSessionClientFormFactor(CSteamAPIContext.GetSteamRemotePlay(), unSessionID);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00009492 File Offset: 0x00007692
		public static bool BGetSessionClientResolution(RemotePlaySessionID_t unSessionID, out int pnResolutionX, out int pnResolutionY)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemotePlay_BGetSessionClientResolution(CSteamAPIContext.GetSteamRemotePlay(), unSessionID, out pnResolutionX, out pnResolutionY);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000094A6 File Offset: 0x000076A6
		public static bool BSendRemotePlayTogetherInvite(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemotePlay_BSendRemotePlayTogetherInvite(CSteamAPIContext.GetSteamRemotePlay(), steamIDFriend);
		}
	}
}
