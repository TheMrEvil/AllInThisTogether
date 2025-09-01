using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000128 RID: 296
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageUpdateUserPublishedItemVoteResult_t : ICallbackData
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00015E89 File Offset: 0x00014089
		public int DataSize
		{
			get
			{
				return RemoteStorageUpdateUserPublishedItemVoteResult_t._datasize;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00015E90 File Offset: 0x00014090
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageUpdateUserPublishedItemVoteResult;
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00015E97 File Offset: 0x00014097
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageUpdateUserPublishedItemVoteResult_t()
		{
		}

		// Token: 0x0400092C RID: 2348
		internal Result Result;

		// Token: 0x0400092D RID: 2349
		internal PublishedFileId PublishedFileId;

		// Token: 0x0400092E RID: 2350
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t));
	}
}
