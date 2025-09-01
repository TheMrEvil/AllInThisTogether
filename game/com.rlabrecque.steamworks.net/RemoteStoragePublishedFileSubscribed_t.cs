using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B8 RID: 184
	[CallbackIdentity(1321)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishedFileSubscribed_t
	{
		// Token: 0x04000222 RID: 546
		public const int k_iCallback = 1321;

		// Token: 0x04000223 RID: 547
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000224 RID: 548
		public AppId_t m_nAppID;
	}
}
