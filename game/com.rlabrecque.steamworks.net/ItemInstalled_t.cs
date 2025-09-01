using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000CB RID: 203
	[CallbackIdentity(3405)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ItemInstalled_t
	{
		// Token: 0x04000267 RID: 615
		public const int k_iCallback = 3405;

		// Token: 0x04000268 RID: 616
		public AppId_t m_unAppID;

		// Token: 0x04000269 RID: 617
		public PublishedFileId_t m_nPublishedFileId;
	}
}
