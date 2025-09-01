using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000DC RID: 220
	[CallbackIdentity(103)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamServersDisconnected_t
	{
		// Token: 0x040002A6 RID: 678
		public const int k_iCallback = 103;

		// Token: 0x040002A7 RID: 679
		public EResult m_eResult;
	}
}
