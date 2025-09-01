using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000124 RID: 292
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageGetPublishedItemVoteDetailsResult_t : ICallbackData
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x00015DF9 File Offset: 0x00013FF9
		public int DataSize
		{
			get
			{
				return RemoteStorageGetPublishedItemVoteDetailsResult_t._datasize;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00015E00 File Offset: 0x00014000
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageGetPublishedItemVoteDetailsResult;
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00015E07 File Offset: 0x00014007
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageGetPublishedItemVoteDetailsResult_t()
		{
		}

		// Token: 0x0400091C RID: 2332
		internal Result Result;

		// Token: 0x0400091D RID: 2333
		internal PublishedFileId PublishedFileId;

		// Token: 0x0400091E RID: 2334
		internal int VotesFor;

		// Token: 0x0400091F RID: 2335
		internal int VotesAgainst;

		// Token: 0x04000920 RID: 2336
		internal int Reports;

		// Token: 0x04000921 RID: 2337
		internal float FScore;

		// Token: 0x04000922 RID: 2338
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t));
	}
}
