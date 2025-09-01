using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000CC RID: 204
	[CallbackIdentity(3406)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DownloadItemResult_t
	{
		// Token: 0x0400026A RID: 618
		public const int k_iCallback = 3406;

		// Token: 0x0400026B RID: 619
		public AppId_t m_unAppID;

		// Token: 0x0400026C RID: 620
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x0400026D RID: 621
		public EResult m_eResult;
	}
}
