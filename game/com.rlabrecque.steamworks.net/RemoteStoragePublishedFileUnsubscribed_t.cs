using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B9 RID: 185
	[CallbackIdentity(1322)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishedFileUnsubscribed_t
	{
		// Token: 0x04000225 RID: 549
		public const int k_iCallback = 1322;

		// Token: 0x04000226 RID: 550
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000227 RID: 551
		public AppId_t m_nAppID;
	}
}
