using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D2 RID: 210
	[CallbackIdentity(3412)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AddUGCDependencyResult_t
	{
		// Token: 0x04000280 RID: 640
		public const int k_iCallback = 3412;

		// Token: 0x04000281 RID: 641
		public EResult m_eResult;

		// Token: 0x04000282 RID: 642
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000283 RID: 643
		public PublishedFileId_t m_nChildPublishedFileId;
	}
}
