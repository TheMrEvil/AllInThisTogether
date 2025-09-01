using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000CF RID: 207
	[CallbackIdentity(3409)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetUserItemVoteResult_t
	{
		// Token: 0x04000276 RID: 630
		public const int k_iCallback = 3409;

		// Token: 0x04000277 RID: 631
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000278 RID: 632
		public EResult m_eResult;

		// Token: 0x04000279 RID: 633
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVotedUp;

		// Token: 0x0400027A RID: 634
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVotedDown;

		// Token: 0x0400027B RID: 635
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVoteSkipped;
	}
}
