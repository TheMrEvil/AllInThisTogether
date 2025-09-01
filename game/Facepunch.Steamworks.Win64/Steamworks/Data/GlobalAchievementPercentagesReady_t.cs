using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200013A RID: 314
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GlobalAchievementPercentagesReady_t : ICallbackData
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0001614F File Offset: 0x0001434F
		public int DataSize
		{
			get
			{
				return GlobalAchievementPercentagesReady_t._datasize;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00016156 File Offset: 0x00014356
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GlobalAchievementPercentagesReady;
			}
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0001615D File Offset: 0x0001435D
		// Note: this type is marked as 'beforefieldinit'.
		static GlobalAchievementPercentagesReady_t()
		{
		}

		// Token: 0x04000976 RID: 2422
		internal ulong GameID;

		// Token: 0x04000977 RID: 2423
		internal Result Result;

		// Token: 0x04000978 RID: 2424
		public static int _datasize = Marshal.SizeOf(typeof(GlobalAchievementPercentagesReady_t));
	}
}
