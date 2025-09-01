using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BD RID: 189
	[CallbackIdentity(1326)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t
	{
		// Token: 0x04000232 RID: 562
		public const int k_iCallback = 1326;

		// Token: 0x04000233 RID: 563
		public EResult m_eResult;

		// Token: 0x04000234 RID: 564
		public int m_nResultsReturned;

		// Token: 0x04000235 RID: 565
		public int m_nTotalResultCount;

		// Token: 0x04000236 RID: 566
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;
	}
}
