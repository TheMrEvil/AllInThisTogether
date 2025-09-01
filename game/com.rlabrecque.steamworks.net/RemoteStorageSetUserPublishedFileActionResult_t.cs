using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BE RID: 190
	[CallbackIdentity(1327)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageSetUserPublishedFileActionResult_t
	{
		// Token: 0x04000237 RID: 567
		public const int k_iCallback = 1327;

		// Token: 0x04000238 RID: 568
		public EResult m_eResult;

		// Token: 0x04000239 RID: 569
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x0400023A RID: 570
		public EWorkshopFileAction m_eAction;
	}
}
