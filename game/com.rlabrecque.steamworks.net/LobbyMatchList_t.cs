using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200007F RID: 127
	[CallbackIdentity(510)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyMatchList_t
	{
		// Token: 0x0400015C RID: 348
		public const int k_iCallback = 510;

		// Token: 0x0400015D RID: 349
		public uint m_nLobbiesMatching;
	}
}
