using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000F2 RID: 242
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct FriendsIsFollowing_t : ICallbackData
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x000155DA File Offset: 0x000137DA
		public int DataSize
		{
			get
			{
				return FriendsIsFollowing_t._datasize;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x000155E1 File Offset: 0x000137E1
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.FriendsIsFollowing;
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x000155E8 File Offset: 0x000137E8
		// Note: this type is marked as 'beforefieldinit'.
		static FriendsIsFollowing_t()
		{
		}

		// Token: 0x04000843 RID: 2115
		internal Result Result;

		// Token: 0x04000844 RID: 2116
		internal ulong SteamID;

		// Token: 0x04000845 RID: 2117
		[MarshalAs(UnmanagedType.I1)]
		internal bool IsFollowing;

		// Token: 0x04000846 RID: 2118
		public static int _datasize = Marshal.SizeOf(typeof(FriendsIsFollowing_t));
	}
}
