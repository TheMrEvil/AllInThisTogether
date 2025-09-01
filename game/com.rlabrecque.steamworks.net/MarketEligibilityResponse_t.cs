using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E6 RID: 230
	[CallbackIdentity(166)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MarketEligibilityResponse_t
	{
		// Token: 0x040002C2 RID: 706
		public const int k_iCallback = 166;

		// Token: 0x040002C3 RID: 707
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAllowed;

		// Token: 0x040002C4 RID: 708
		public EMarketNotAllowedReasonFlags m_eNotAllowedReason;

		// Token: 0x040002C5 RID: 709
		public RTime32 m_rtAllowedAtTime;

		// Token: 0x040002C6 RID: 710
		public int m_cdaySteamGuardRequiredDays;

		// Token: 0x040002C7 RID: 711
		public int m_cdayNewDeviceCooldown;
	}
}
