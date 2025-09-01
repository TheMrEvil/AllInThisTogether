using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000135 RID: 309
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaderboardScoresDownloaded_t : ICallbackData
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0001607C File Offset: 0x0001427C
		public int DataSize
		{
			get
			{
				return LeaderboardScoresDownloaded_t._datasize;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x00016083 File Offset: 0x00014283
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.LeaderboardScoresDownloaded;
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0001608A File Offset: 0x0001428A
		// Note: this type is marked as 'beforefieldinit'.
		static LeaderboardScoresDownloaded_t()
		{
		}

		// Token: 0x04000961 RID: 2401
		internal ulong SteamLeaderboard;

		// Token: 0x04000962 RID: 2402
		internal ulong SteamLeaderboardEntries;

		// Token: 0x04000963 RID: 2403
		internal int CEntryCount;

		// Token: 0x04000964 RID: 2404
		public static int _datasize = Marshal.SizeOf(typeof(LeaderboardScoresDownloaded_t));
	}
}
