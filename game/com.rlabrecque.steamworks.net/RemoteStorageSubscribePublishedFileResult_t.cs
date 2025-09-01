using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B0 RID: 176
	[CallbackIdentity(1313)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageSubscribePublishedFileResult_t
	{
		// Token: 0x040001E6 RID: 486
		public const int k_iCallback = 1313;

		// Token: 0x040001E7 RID: 487
		public EResult m_eResult;

		// Token: 0x040001E8 RID: 488
		public PublishedFileId_t m_nPublishedFileId;
	}
}
