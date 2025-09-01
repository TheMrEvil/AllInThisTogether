using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000087 RID: 135
	[CallbackIdentity(5213)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RequestPlayersForGameFinalResultCallback_t
	{
		// Token: 0x04000183 RID: 387
		public const int k_iCallback = 5213;

		// Token: 0x04000184 RID: 388
		public EResult m_eResult;

		// Token: 0x04000185 RID: 389
		public ulong m_ullSearchID;

		// Token: 0x04000186 RID: 390
		public ulong m_ullUniqueGameID;
	}
}
