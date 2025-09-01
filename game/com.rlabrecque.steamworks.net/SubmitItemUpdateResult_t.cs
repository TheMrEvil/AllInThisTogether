using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000CA RID: 202
	[CallbackIdentity(3404)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SubmitItemUpdateResult_t
	{
		// Token: 0x04000263 RID: 611
		public const int k_iCallback = 3404;

		// Token: 0x04000264 RID: 612
		public EResult m_eResult;

		// Token: 0x04000265 RID: 613
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;

		// Token: 0x04000266 RID: 614
		public PublishedFileId_t m_nPublishedFileId;
	}
}
