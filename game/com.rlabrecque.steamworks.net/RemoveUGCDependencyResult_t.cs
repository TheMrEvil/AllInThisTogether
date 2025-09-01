using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D3 RID: 211
	[CallbackIdentity(3413)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoveUGCDependencyResult_t
	{
		// Token: 0x04000284 RID: 644
		public const int k_iCallback = 3413;

		// Token: 0x04000285 RID: 645
		public EResult m_eResult;

		// Token: 0x04000286 RID: 646
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000287 RID: 647
		public PublishedFileId_t m_nChildPublishedFileId;
	}
}
