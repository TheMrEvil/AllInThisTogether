using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AF RID: 175
	[CallbackIdentity(1312)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumerateUserPublishedFilesResult_t
	{
		// Token: 0x040001E1 RID: 481
		public const int k_iCallback = 1312;

		// Token: 0x040001E2 RID: 482
		public EResult m_eResult;

		// Token: 0x040001E3 RID: 483
		public int m_nResultsReturned;

		// Token: 0x040001E4 RID: 484
		public int m_nTotalResultCount;

		// Token: 0x040001E5 RID: 485
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;
	}
}
