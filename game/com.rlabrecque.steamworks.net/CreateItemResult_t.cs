using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C9 RID: 201
	[CallbackIdentity(3403)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CreateItemResult_t
	{
		// Token: 0x0400025F RID: 607
		public const int k_iCallback = 3403;

		// Token: 0x04000260 RID: 608
		public EResult m_eResult;

		// Token: 0x04000261 RID: 609
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000262 RID: 610
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	}
}
