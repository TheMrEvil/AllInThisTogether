using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B2 RID: 178
	[CallbackIdentity(1315)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageUnsubscribePublishedFileResult_t
	{
		// Token: 0x040001EF RID: 495
		public const int k_iCallback = 1315;

		// Token: 0x040001F0 RID: 496
		public EResult m_eResult;

		// Token: 0x040001F1 RID: 497
		public PublishedFileId_t m_nPublishedFileId;
	}
}
