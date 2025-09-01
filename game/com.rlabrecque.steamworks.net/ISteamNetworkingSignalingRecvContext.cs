using System;

namespace Steamworks
{
	// Token: 0x020001AE RID: 430
	[Serializable]
	public struct ISteamNetworkingSignalingRecvContext
	{
		// Token: 0x06000A56 RID: 2646 RVA: 0x0000F54E File Offset: 0x0000D74E
		public IntPtr OnConnectRequest(HSteamNetConnection hConn, ref SteamNetworkingIdentity identityPeer, int nLocalVirtualPort)
		{
			return NativeMethods.SteamAPI_ISteamNetworkingSignalingRecvContext_OnConnectRequest(ref this, hConn, ref identityPeer, nLocalVirtualPort);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0000F559 File Offset: 0x0000D759
		public void SendRejectionSignal(ref SteamNetworkingIdentity identityPeer, IntPtr pMsg, int cbMsg)
		{
			NativeMethods.SteamAPI_ISteamNetworkingSignalingRecvContext_SendRejectionSignal(ref this, ref identityPeer, pMsg, cbMsg);
		}
	}
}
