using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x02000139 RID: 313
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserAchievementIconFetched_t : ICallbackData
	{
		// Token: 0x06000C24 RID: 3108 RVA: 0x0001610C File Offset: 0x0001430C
		internal string AchievementNameUTF8()
		{
			return Encoding.UTF8.GetString(this.AchievementName, 0, Array.IndexOf<byte>(this.AchievementName, 0));
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0001612B File Offset: 0x0001432B
		public int DataSize
		{
			get
			{
				return UserAchievementIconFetched_t._datasize;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x00016132 File Offset: 0x00014332
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.UserAchievementIconFetched;
			}
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00016139 File Offset: 0x00014339
		// Note: this type is marked as 'beforefieldinit'.
		static UserAchievementIconFetched_t()
		{
		}

		// Token: 0x04000971 RID: 2417
		internal GameId GameID;

		// Token: 0x04000972 RID: 2418
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		internal byte[] AchievementName;

		// Token: 0x04000973 RID: 2419
		[MarshalAs(UnmanagedType.I1)]
		internal bool Achieved;

		// Token: 0x04000974 RID: 2420
		internal int IconHandle;

		// Token: 0x04000975 RID: 2421
		public static int _datasize = Marshal.SizeOf(typeof(UserAchievementIconFetched_t));
	}
}
