using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D9 RID: 217
	[CallbackIdentity(3420)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct WorkshopEULAStatus_t
	{
		// Token: 0x0400029B RID: 667
		public const int k_iCallback = 3420;

		// Token: 0x0400029C RID: 668
		public EResult m_eResult;

		// Token: 0x0400029D RID: 669
		public AppId_t m_nAppID;

		// Token: 0x0400029E RID: 670
		public uint m_unVersion;

		// Token: 0x0400029F RID: 671
		public RTime32 m_rtAction;

		// Token: 0x040002A0 RID: 672
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAccepted;

		// Token: 0x040002A1 RID: 673
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bNeedsAction;
	}
}
