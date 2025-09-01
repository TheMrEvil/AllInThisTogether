using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000132 RID: 306
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserStatsStored_t : ICallbackData
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00015FF1 File Offset: 0x000141F1
		public int DataSize
		{
			get
			{
				return UserStatsStored_t._datasize;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00015FF8 File Offset: 0x000141F8
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.UserStatsStored;
			}
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00015FFF File Offset: 0x000141FF
		// Note: this type is marked as 'beforefieldinit'.
		static UserStatsStored_t()
		{
		}

		// Token: 0x04000955 RID: 2389
		internal ulong GameID;

		// Token: 0x04000956 RID: 2390
		internal Result Result;

		// Token: 0x04000957 RID: 2391
		public static int _datasize = Marshal.SizeOf(typeof(UserStatsStored_t));
	}
}
