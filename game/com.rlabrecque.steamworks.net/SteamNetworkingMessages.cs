using System;

namespace Steamworks
{
	// Token: 0x0200001B RID: 27
	public static class SteamNetworkingMessages
	{
		// Token: 0x0600032F RID: 815 RVA: 0x00008BCA File Offset: 0x00006DCA
		public static EResult SendMessageToUser(ref SteamNetworkingIdentity identityRemote, IntPtr pubData, uint cubData, int nSendFlags, int nRemoteChannel)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_SendMessageToUser(CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote, pubData, cubData, nSendFlags, nRemoteChannel);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00008BE1 File Offset: 0x00006DE1
		public static int ReceiveMessagesOnChannel(int nLocalChannel, IntPtr[] ppOutMessages, int nMaxMessages)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_ReceiveMessagesOnChannel(CSteamAPIContext.GetSteamNetworkingMessages(), nLocalChannel, ppOutMessages, nMaxMessages);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00008BF5 File Offset: 0x00006DF5
		public static bool AcceptSessionWithUser(ref SteamNetworkingIdentity identityRemote)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_AcceptSessionWithUser(CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00008C07 File Offset: 0x00006E07
		public static bool CloseSessionWithUser(ref SteamNetworkingIdentity identityRemote)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_CloseSessionWithUser(CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00008C19 File Offset: 0x00006E19
		public static bool CloseChannelWithUser(ref SteamNetworkingIdentity identityRemote, int nLocalChannel)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_CloseChannelWithUser(CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote, nLocalChannel);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00008C2C File Offset: 0x00006E2C
		public static ESteamNetworkingConnectionState GetSessionConnectionInfo(ref SteamNetworkingIdentity identityRemote, out SteamNetConnectionInfo_t pConnectionInfo, out SteamNetConnectionRealTimeStatus_t pQuickStatus)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_GetSessionConnectionInfo(CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote, out pConnectionInfo, out pQuickStatus);
		}
	}
}
