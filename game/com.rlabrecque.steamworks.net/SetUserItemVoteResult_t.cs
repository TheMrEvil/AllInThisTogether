using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000CE RID: 206
	[CallbackIdentity(3408)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SetUserItemVoteResult_t
	{
		// Token: 0x04000272 RID: 626
		public const int k_iCallback = 3408;

		// Token: 0x04000273 RID: 627
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000274 RID: 628
		public EResult m_eResult;

		// Token: 0x04000275 RID: 629
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVoteUp;
	}
}
