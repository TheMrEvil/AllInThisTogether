using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B1 RID: 177
	[CallbackIdentity(1314)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumerateUserSubscribedFilesResult_t
	{
		// Token: 0x040001E9 RID: 489
		public const int k_iCallback = 1314;

		// Token: 0x040001EA RID: 490
		public EResult m_eResult;

		// Token: 0x040001EB RID: 491
		public int m_nResultsReturned;

		// Token: 0x040001EC RID: 492
		public int m_nTotalResultCount;

		// Token: 0x040001ED RID: 493
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;

		// Token: 0x040001EE RID: 494
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public uint[] m_rgRTimeSubscribed;
	}
}
