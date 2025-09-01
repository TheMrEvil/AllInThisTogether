using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000084 RID: 132
	[CallbackIdentity(5202)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SearchForGameResultCallback_t
	{
		// Token: 0x0400016E RID: 366
		public const int k_iCallback = 5202;

		// Token: 0x0400016F RID: 367
		public ulong m_ullSearchID;

		// Token: 0x04000170 RID: 368
		public EResult m_eResult;

		// Token: 0x04000171 RID: 369
		public int m_nCountPlayersInGame;

		// Token: 0x04000172 RID: 370
		public int m_nCountAcceptedGame;

		// Token: 0x04000173 RID: 371
		public CSteamID m_steamIDHost;

		// Token: 0x04000174 RID: 372
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bFinalCallback;
	}
}
