using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200013B RID: 315
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaderboardUGCSet_t : ICallbackData
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x00016173 File Offset: 0x00014373
		public int DataSize
		{
			get
			{
				return LeaderboardUGCSet_t._datasize;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0001617A File Offset: 0x0001437A
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.LeaderboardUGCSet;
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00016181 File Offset: 0x00014381
		// Note: this type is marked as 'beforefieldinit'.
		static LeaderboardUGCSet_t()
		{
		}

		// Token: 0x04000979 RID: 2425
		internal Result Result;

		// Token: 0x0400097A RID: 2426
		internal ulong SteamLeaderboard;

		// Token: 0x0400097B RID: 2427
		public static int _datasize = Marshal.SizeOf(typeof(LeaderboardUGCSet_t));
	}
}
