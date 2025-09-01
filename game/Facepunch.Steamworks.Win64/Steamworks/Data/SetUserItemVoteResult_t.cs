using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000160 RID: 352
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetUserItemVoteResult_t : ICallbackData
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x000166E5 File Offset: 0x000148E5
		public int DataSize
		{
			get
			{
				return SetUserItemVoteResult_t._datasize;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x000166EC File Offset: 0x000148EC
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SetUserItemVoteResult;
			}
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x000166F3 File Offset: 0x000148F3
		// Note: this type is marked as 'beforefieldinit'.
		static SetUserItemVoteResult_t()
		{
		}

		// Token: 0x040009DA RID: 2522
		internal PublishedFileId PublishedFileId;

		// Token: 0x040009DB RID: 2523
		internal Result Result;

		// Token: 0x040009DC RID: 2524
		[MarshalAs(UnmanagedType.I1)]
		internal bool VoteUp;

		// Token: 0x040009DD RID: 2525
		public static int _datasize = Marshal.SizeOf(typeof(SetUserItemVoteResult_t));
	}
}
