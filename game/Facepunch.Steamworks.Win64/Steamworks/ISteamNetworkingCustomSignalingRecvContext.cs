using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000023 RID: 35
	internal class ISteamNetworkingCustomSignalingRecvContext : SteamInterface
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x00007957 File Offset: 0x00005B57
		internal ISteamNetworkingCustomSignalingRecvContext(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x06000449 RID: 1097
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingCustomSignalingRecvContext_OnConnectRequest")]
		private static extern IntPtr _OnConnectRequest(IntPtr self, Connection hConn, ref NetIdentity identityPeer);

		// Token: 0x0600044A RID: 1098 RVA: 0x0000796C File Offset: 0x00005B6C
		internal IntPtr OnConnectRequest(Connection hConn, ref NetIdentity identityPeer)
		{
			return ISteamNetworkingCustomSignalingRecvContext._OnConnectRequest(this.Self, hConn, ref identityPeer);
		}

		// Token: 0x0600044B RID: 1099
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingCustomSignalingRecvContext_SendRejectionSignal")]
		private static extern void _SendRejectionSignal(IntPtr self, ref NetIdentity identityPeer, IntPtr pMsg, int cbMsg);

		// Token: 0x0600044C RID: 1100 RVA: 0x0000798D File Offset: 0x00005B8D
		internal void SendRejectionSignal(ref NetIdentity identityPeer, IntPtr pMsg, int cbMsg)
		{
			ISteamNetworkingCustomSignalingRecvContext._SendRejectionSignal(this.Self, ref identityPeer, pMsg, cbMsg);
		}
	}
}
