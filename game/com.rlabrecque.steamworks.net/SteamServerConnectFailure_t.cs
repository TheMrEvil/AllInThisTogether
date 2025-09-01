using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000DB RID: 219
	[CallbackIdentity(102)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamServerConnectFailure_t
	{
		// Token: 0x040002A3 RID: 675
		public const int k_iCallback = 102;

		// Token: 0x040002A4 RID: 676
		public EResult m_eResult;

		// Token: 0x040002A5 RID: 677
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bStillRetrying;
	}
}
