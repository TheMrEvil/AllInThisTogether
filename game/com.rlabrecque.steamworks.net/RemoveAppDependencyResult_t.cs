using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D5 RID: 213
	[CallbackIdentity(3415)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoveAppDependencyResult_t
	{
		// Token: 0x0400028C RID: 652
		public const int k_iCallback = 3415;

		// Token: 0x0400028D RID: 653
		public EResult m_eResult;

		// Token: 0x0400028E RID: 654
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x0400028F RID: 655
		public AppId_t m_nAppID;
	}
}
