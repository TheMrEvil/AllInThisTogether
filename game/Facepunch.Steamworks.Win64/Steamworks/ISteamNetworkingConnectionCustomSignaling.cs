using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000022 RID: 34
	internal class ISteamNetworkingConnectionCustomSignaling : SteamInterface
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x00007910 File Offset: 0x00005B10
		internal ISteamNetworkingConnectionCustomSignaling(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x06000444 RID: 1092
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingConnectionCustomSignaling_SendSignal")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SendSignal(IntPtr self, Connection hConn, ref ConnectionInfo info, IntPtr pMsg, int cbMsg);

		// Token: 0x06000445 RID: 1093 RVA: 0x00007924 File Offset: 0x00005B24
		internal bool SendSignal(Connection hConn, ref ConnectionInfo info, IntPtr pMsg, int cbMsg)
		{
			return ISteamNetworkingConnectionCustomSignaling._SendSignal(this.Self, hConn, ref info, pMsg, cbMsg);
		}

		// Token: 0x06000446 RID: 1094
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingConnectionCustomSignaling_Release")]
		private static extern void _Release(IntPtr self);

		// Token: 0x06000447 RID: 1095 RVA: 0x00007948 File Offset: 0x00005B48
		internal void Release()
		{
			ISteamNetworkingConnectionCustomSignaling._Release(this.Self);
		}
	}
}
