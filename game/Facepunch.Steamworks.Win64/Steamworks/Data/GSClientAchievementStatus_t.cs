using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x02000196 RID: 406
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GSClientAchievementStatus_t : ICallbackData
	{
		// Token: 0x06000D43 RID: 3395 RVA: 0x00016F18 File Offset: 0x00015118
		internal string PchAchievementUTF8()
		{
			return Encoding.UTF8.GetString(this.PchAchievement, 0, Array.IndexOf<byte>(this.PchAchievement, 0));
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00016F37 File Offset: 0x00015137
		public int DataSize
		{
			get
			{
				return GSClientAchievementStatus_t._datasize;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x00016F3E File Offset: 0x0001513E
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GSClientAchievementStatus;
			}
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00016F45 File Offset: 0x00015145
		// Note: this type is marked as 'beforefieldinit'.
		static GSClientAchievementStatus_t()
		{
		}

		// Token: 0x04000AA6 RID: 2726
		internal ulong SteamID;

		// Token: 0x04000AA7 RID: 2727
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		internal byte[] PchAchievement;

		// Token: 0x04000AA8 RID: 2728
		[MarshalAs(UnmanagedType.I1)]
		internal bool Unlocked;

		// Token: 0x04000AA9 RID: 2729
		public static int _datasize = Marshal.SizeOf(typeof(GSClientAchievementStatus_t));
	}
}
