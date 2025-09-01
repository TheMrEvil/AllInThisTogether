using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B7 RID: 183
	[CallbackIdentity(1320)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageGetPublishedItemVoteDetailsResult_t
	{
		// Token: 0x0400021B RID: 539
		public const int k_iCallback = 1320;

		// Token: 0x0400021C RID: 540
		public EResult m_eResult;

		// Token: 0x0400021D RID: 541
		public PublishedFileId_t m_unPublishedFileId;

		// Token: 0x0400021E RID: 542
		public int m_nVotesFor;

		// Token: 0x0400021F RID: 543
		public int m_nVotesAgainst;

		// Token: 0x04000220 RID: 544
		public int m_nReports;

		// Token: 0x04000221 RID: 545
		public float m_fScore;
	}
}
