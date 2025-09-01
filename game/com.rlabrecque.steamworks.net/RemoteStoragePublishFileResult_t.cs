using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AD RID: 173
	[CallbackIdentity(1309)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishFileResult_t
	{
		// Token: 0x040001DA RID: 474
		public const int k_iCallback = 1309;

		// Token: 0x040001DB RID: 475
		public EResult m_eResult;

		// Token: 0x040001DC RID: 476
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040001DD RID: 477
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	}
}
