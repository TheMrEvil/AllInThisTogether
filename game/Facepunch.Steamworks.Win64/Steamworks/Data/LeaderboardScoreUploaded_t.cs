using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000136 RID: 310
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaderboardScoreUploaded_t : ICallbackData
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x000160A0 File Offset: 0x000142A0
		public int DataSize
		{
			get
			{
				return LeaderboardScoreUploaded_t._datasize;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x000160A7 File Offset: 0x000142A7
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.LeaderboardScoreUploaded;
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x000160AE File Offset: 0x000142AE
		// Note: this type is marked as 'beforefieldinit'.
		static LeaderboardScoreUploaded_t()
		{
		}

		// Token: 0x04000965 RID: 2405
		internal byte Success;

		// Token: 0x04000966 RID: 2406
		internal ulong SteamLeaderboard;

		// Token: 0x04000967 RID: 2407
		internal int Score;

		// Token: 0x04000968 RID: 2408
		internal byte ScoreChanged;

		// Token: 0x04000969 RID: 2409
		internal int GlobalRankNew;

		// Token: 0x0400096A RID: 2410
		internal int GlobalRankPrevious;

		// Token: 0x0400096B RID: 2411
		public static int _datasize = Marshal.SizeOf(typeof(LeaderboardScoreUploaded_t));
	}
}
