using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C1 RID: 193
	[CallbackIdentity(1330)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishedFileUpdated_t
	{
		// Token: 0x04000245 RID: 581
		public const int k_iCallback = 1330;

		// Token: 0x04000246 RID: 582
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000247 RID: 583
		public AppId_t m_nAppID;

		// Token: 0x04000248 RID: 584
		public ulong m_ulUnused;
	}
}
