using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A0 RID: 160
	[CallbackIdentity(1202)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct P2PSessionRequest_t
	{
		// Token: 0x040001B4 RID: 436
		public const int k_iCallback = 1202;

		// Token: 0x040001B5 RID: 437
		public CSteamID m_steamIDRemote;
	}
}
