using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B6 RID: 182
	[CallbackIdentity(1319)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumerateWorkshopFilesResult_t
	{
		// Token: 0x04000213 RID: 531
		public const int k_iCallback = 1319;

		// Token: 0x04000214 RID: 532
		public EResult m_eResult;

		// Token: 0x04000215 RID: 533
		public int m_nResultsReturned;

		// Token: 0x04000216 RID: 534
		public int m_nTotalResultCount;

		// Token: 0x04000217 RID: 535
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;

		// Token: 0x04000218 RID: 536
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public float[] m_rgScore;

		// Token: 0x04000219 RID: 537
		public AppId_t m_nAppId;

		// Token: 0x0400021A RID: 538
		public uint m_unStartIndex;
	}
}
