using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000161 RID: 353
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetUserItemVoteResult_t : ICallbackData
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x00016709 File Offset: 0x00014909
		public int DataSize
		{
			get
			{
				return GetUserItemVoteResult_t._datasize;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x00016710 File Offset: 0x00014910
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GetUserItemVoteResult;
			}
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00016717 File Offset: 0x00014917
		// Note: this type is marked as 'beforefieldinit'.
		static GetUserItemVoteResult_t()
		{
		}

		// Token: 0x040009DE RID: 2526
		internal PublishedFileId PublishedFileId;

		// Token: 0x040009DF RID: 2527
		internal Result Result;

		// Token: 0x040009E0 RID: 2528
		[MarshalAs(UnmanagedType.I1)]
		internal bool VotedUp;

		// Token: 0x040009E1 RID: 2529
		[MarshalAs(UnmanagedType.I1)]
		internal bool VotedDown;

		// Token: 0x040009E2 RID: 2530
		[MarshalAs(UnmanagedType.I1)]
		internal bool VoteSkipped;

		// Token: 0x040009E3 RID: 2531
		public static int _datasize = Marshal.SizeOf(typeof(GetUserItemVoteResult_t));
	}
}
