using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000129 RID: 297
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageUserVoteDetails_t : ICallbackData
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00015EAD File Offset: 0x000140AD
		public int DataSize
		{
			get
			{
				return RemoteStorageUserVoteDetails_t._datasize;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00015EB4 File Offset: 0x000140B4
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageUserVoteDetails;
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00015EBB File Offset: 0x000140BB
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageUserVoteDetails_t()
		{
		}

		// Token: 0x0400092F RID: 2351
		internal Result Result;

		// Token: 0x04000930 RID: 2352
		internal PublishedFileId PublishedFileId;

		// Token: 0x04000931 RID: 2353
		internal WorkshopVote Vote;

		// Token: 0x04000932 RID: 2354
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageUserVoteDetails_t));
	}
}
