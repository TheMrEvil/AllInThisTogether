using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000F1 RID: 241
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct FriendsGetFollowerCount_t : ICallbackData
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x000155B6 File Offset: 0x000137B6
		public int DataSize
		{
			get
			{
				return FriendsGetFollowerCount_t._datasize;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x000155BD File Offset: 0x000137BD
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.FriendsGetFollowerCount;
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x000155C4 File Offset: 0x000137C4
		// Note: this type is marked as 'beforefieldinit'.
		static FriendsGetFollowerCount_t()
		{
		}

		// Token: 0x0400083F RID: 2111
		internal Result Result;

		// Token: 0x04000840 RID: 2112
		internal ulong SteamID;

		// Token: 0x04000841 RID: 2113
		internal int Count;

		// Token: 0x04000842 RID: 2114
		public static int _datasize = Marshal.SizeOf(typeof(FriendsGetFollowerCount_t));
	}
}
