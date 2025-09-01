using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BB RID: 187
	[CallbackIdentity(1324)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageUpdateUserPublishedItemVoteResult_t
	{
		// Token: 0x0400022B RID: 555
		public const int k_iCallback = 1324;

		// Token: 0x0400022C RID: 556
		public EResult m_eResult;

		// Token: 0x0400022D RID: 557
		public PublishedFileId_t m_nPublishedFileId;
	}
}
