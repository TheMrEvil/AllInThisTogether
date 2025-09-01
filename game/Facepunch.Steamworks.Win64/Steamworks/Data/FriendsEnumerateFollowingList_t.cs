using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000F3 RID: 243
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct FriendsEnumerateFollowingList_t : ICallbackData
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x000155FE File Offset: 0x000137FE
		public int DataSize
		{
			get
			{
				return FriendsEnumerateFollowingList_t._datasize;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00015605 File Offset: 0x00013805
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.FriendsEnumerateFollowingList;
			}
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0001560C File Offset: 0x0001380C
		// Note: this type is marked as 'beforefieldinit'.
		static FriendsEnumerateFollowingList_t()
		{
		}

		// Token: 0x04000847 RID: 2119
		internal Result Result;

		// Token: 0x04000848 RID: 2120
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal ulong[] GSteamID;

		// Token: 0x04000849 RID: 2121
		internal int ResultsReturned;

		// Token: 0x0400084A RID: 2122
		internal int TotalResultCount;

		// Token: 0x0400084B RID: 2123
		public static int _datasize = Marshal.SizeOf(typeof(FriendsEnumerateFollowingList_t));
	}
}
