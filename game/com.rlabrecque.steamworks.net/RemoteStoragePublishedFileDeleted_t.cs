using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BA RID: 186
	[CallbackIdentity(1323)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishedFileDeleted_t
	{
		// Token: 0x04000228 RID: 552
		public const int k_iCallback = 1323;

		// Token: 0x04000229 RID: 553
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x0400022A RID: 554
		public AppId_t m_nAppID;
	}
}
