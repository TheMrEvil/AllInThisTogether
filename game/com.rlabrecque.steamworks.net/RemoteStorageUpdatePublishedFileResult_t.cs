using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B3 RID: 179
	[CallbackIdentity(1316)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageUpdatePublishedFileResult_t
	{
		// Token: 0x040001F2 RID: 498
		public const int k_iCallback = 1316;

		// Token: 0x040001F3 RID: 499
		public EResult m_eResult;

		// Token: 0x040001F4 RID: 500
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040001F5 RID: 501
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	}
}
