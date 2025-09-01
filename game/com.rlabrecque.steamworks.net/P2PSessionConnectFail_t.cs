using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A1 RID: 161
	[CallbackIdentity(1203)]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct P2PSessionConnectFail_t
	{
		// Token: 0x040001B6 RID: 438
		public const int k_iCallback = 1203;

		// Token: 0x040001B7 RID: 439
		public CSteamID m_steamIDRemote;

		// Token: 0x040001B8 RID: 440
		public byte m_eP2PSessionError;
	}
}
