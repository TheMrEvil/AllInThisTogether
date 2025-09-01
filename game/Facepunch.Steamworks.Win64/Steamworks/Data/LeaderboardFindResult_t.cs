using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000134 RID: 308
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaderboardFindResult_t : ICallbackData
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00016058 File Offset: 0x00014258
		public int DataSize
		{
			get
			{
				return LeaderboardFindResult_t._datasize;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0001605F File Offset: 0x0001425F
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.LeaderboardFindResult;
			}
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00016066 File Offset: 0x00014266
		// Note: this type is marked as 'beforefieldinit'.
		static LeaderboardFindResult_t()
		{
		}

		// Token: 0x0400095E RID: 2398
		internal ulong SteamLeaderboard;

		// Token: 0x0400095F RID: 2399
		internal byte LeaderboardFound;

		// Token: 0x04000960 RID: 2400
		public static int _datasize = Marshal.SizeOf(typeof(LeaderboardFindResult_t));
	}
}
