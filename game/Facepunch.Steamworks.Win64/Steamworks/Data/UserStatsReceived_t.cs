using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000131 RID: 305
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct UserStatsReceived_t : ICallbackData
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x00015FCD File Offset: 0x000141CD
		public int DataSize
		{
			get
			{
				return UserStatsReceived_t._datasize;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x00015FD4 File Offset: 0x000141D4
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.UserStatsReceived;
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00015FDB File Offset: 0x000141DB
		// Note: this type is marked as 'beforefieldinit'.
		static UserStatsReceived_t()
		{
		}

		// Token: 0x04000951 RID: 2385
		internal ulong GameID;

		// Token: 0x04000952 RID: 2386
		internal Result Result;

		// Token: 0x04000953 RID: 2387
		internal ulong SteamIDUser;

		// Token: 0x04000954 RID: 2388
		public static int _datasize = Marshal.SizeOf(typeof(UserStatsReceived_t));
	}
}
