using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C8 RID: 200
	[CallbackIdentity(3402)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamUGCRequestUGCDetailsResult_t
	{
		// Token: 0x0400025C RID: 604
		public const int k_iCallback = 3402;

		// Token: 0x0400025D RID: 605
		public SteamUGCDetails_t m_details;

		// Token: 0x0400025E RID: 606
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bCachedData;
	}
}
