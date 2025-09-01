using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BC RID: 188
	[CallbackIdentity(1325)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageUserVoteDetails_t
	{
		// Token: 0x0400022E RID: 558
		public const int k_iCallback = 1325;

		// Token: 0x0400022F RID: 559
		public EResult m_eResult;

		// Token: 0x04000230 RID: 560
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000231 RID: 561
		public EWorkshopVote m_eVote;
	}
}
