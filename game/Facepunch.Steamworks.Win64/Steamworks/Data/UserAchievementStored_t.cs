using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x02000133 RID: 307
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserAchievementStored_t : ICallbackData
	{
		// Token: 0x06000C11 RID: 3089 RVA: 0x00016015 File Offset: 0x00014215
		internal string AchievementNameUTF8()
		{
			return Encoding.UTF8.GetString(this.AchievementName, 0, Array.IndexOf<byte>(this.AchievementName, 0));
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x00016034 File Offset: 0x00014234
		public int DataSize
		{
			get
			{
				return UserAchievementStored_t._datasize;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0001603B File Offset: 0x0001423B
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.UserAchievementStored;
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00016042 File Offset: 0x00014242
		// Note: this type is marked as 'beforefieldinit'.
		static UserAchievementStored_t()
		{
		}

		// Token: 0x04000958 RID: 2392
		internal ulong GameID;

		// Token: 0x04000959 RID: 2393
		[MarshalAs(UnmanagedType.I1)]
		internal bool GroupAchievement;

		// Token: 0x0400095A RID: 2394
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		internal byte[] AchievementName;

		// Token: 0x0400095B RID: 2395
		internal uint CurProgress;

		// Token: 0x0400095C RID: 2396
		internal uint MaxProgress;

		// Token: 0x0400095D RID: 2397
		public static int _datasize = Marshal.SizeOf(typeof(UserAchievementStored_t));
	}
}
