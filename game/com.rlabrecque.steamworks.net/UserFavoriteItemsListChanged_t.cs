using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000CD RID: 205
	[CallbackIdentity(3407)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserFavoriteItemsListChanged_t
	{
		// Token: 0x0400026E RID: 622
		public const int k_iCallback = 3407;

		// Token: 0x0400026F RID: 623
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000270 RID: 624
		public EResult m_eResult;

		// Token: 0x04000271 RID: 625
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bWasAddRequest;
	}
}
