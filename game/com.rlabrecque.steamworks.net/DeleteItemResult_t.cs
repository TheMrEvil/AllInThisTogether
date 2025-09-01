using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D7 RID: 215
	[CallbackIdentity(3417)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DeleteItemResult_t
	{
		// Token: 0x04000296 RID: 662
		public const int k_iCallback = 3417;

		// Token: 0x04000297 RID: 663
		public EResult m_eResult;

		// Token: 0x04000298 RID: 664
		public PublishedFileId_t m_nPublishedFileId;
	}
}
