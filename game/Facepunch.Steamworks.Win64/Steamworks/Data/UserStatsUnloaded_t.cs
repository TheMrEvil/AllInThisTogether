using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000138 RID: 312
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserStatsUnloaded_t : ICallbackData
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x000160E8 File Offset: 0x000142E8
		public int DataSize
		{
			get
			{
				return UserStatsUnloaded_t._datasize;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x000160EF File Offset: 0x000142EF
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.UserStatsUnloaded;
			}
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x000160F6 File Offset: 0x000142F6
		// Note: this type is marked as 'beforefieldinit'.
		static UserStatsUnloaded_t()
		{
		}

		// Token: 0x0400096F RID: 2415
		internal ulong SteamIDUser;

		// Token: 0x04000970 RID: 2416
		public static int _datasize = Marshal.SizeOf(typeof(UserStatsUnloaded_t));
	}
}
