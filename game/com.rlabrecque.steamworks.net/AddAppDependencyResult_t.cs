using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D4 RID: 212
	[CallbackIdentity(3414)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AddAppDependencyResult_t
	{
		// Token: 0x04000288 RID: 648
		public const int k_iCallback = 3414;

		// Token: 0x04000289 RID: 649
		public EResult m_eResult;

		// Token: 0x0400028A RID: 650
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x0400028B RID: 651
		public AppId_t m_nAppID;
	}
}
