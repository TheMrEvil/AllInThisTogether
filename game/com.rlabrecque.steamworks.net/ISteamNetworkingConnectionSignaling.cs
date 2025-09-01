using System;

namespace Steamworks
{
	// Token: 0x020001AD RID: 429
	[Serializable]
	public struct ISteamNetworkingConnectionSignaling
	{
		// Token: 0x06000A54 RID: 2644 RVA: 0x0000F539 File Offset: 0x0000D739
		public bool SendSignal(HSteamNetConnection hConn, ref SteamNetConnectionInfo_t info, IntPtr pMsg, int cbMsg)
		{
			return NativeMethods.SteamAPI_ISteamNetworkingConnectionSignaling_SendSignal(ref this, hConn, ref info, pMsg, cbMsg);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0000F546 File Offset: 0x0000D746
		public void Release()
		{
			NativeMethods.SteamAPI_ISteamNetworkingConnectionSignaling_Release(ref this);
		}
	}
}
