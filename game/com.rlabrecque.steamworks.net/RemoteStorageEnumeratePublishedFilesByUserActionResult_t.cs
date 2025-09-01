using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BF RID: 191
	[CallbackIdentity(1328)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumeratePublishedFilesByUserActionResult_t
	{
		// Token: 0x0400023B RID: 571
		public const int k_iCallback = 1328;

		// Token: 0x0400023C RID: 572
		public EResult m_eResult;

		// Token: 0x0400023D RID: 573
		public EWorkshopFileAction m_eAction;

		// Token: 0x0400023E RID: 574
		public int m_nResultsReturned;

		// Token: 0x0400023F RID: 575
		public int m_nTotalResultCount;

		// Token: 0x04000240 RID: 576
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;

		// Token: 0x04000241 RID: 577
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public uint[] m_rgRTimeUpdated;
	}
}
