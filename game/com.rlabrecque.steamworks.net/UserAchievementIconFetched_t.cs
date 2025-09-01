using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F1 RID: 241
	[CallbackIdentity(1109)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserAchievementIconFetched_t
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x0000C147 File Offset: 0x0000A347
		// (set) Token: 0x06000889 RID: 2185 RVA: 0x0000C154 File Offset: 0x0000A354
		public string m_rgchAchievementName
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchAchievementName_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchAchievementName_, 128);
			}
		}

		// Token: 0x040002F6 RID: 758
		public const int k_iCallback = 1109;

		// Token: 0x040002F7 RID: 759
		public CGameID m_nGameID;

		// Token: 0x040002F8 RID: 760
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_rgchAchievementName_;

		// Token: 0x040002F9 RID: 761
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAchieved;

		// Token: 0x040002FA RID: 762
		public int m_nIconHandle;
	}
}
