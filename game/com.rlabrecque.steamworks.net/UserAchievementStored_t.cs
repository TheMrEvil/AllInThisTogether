using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000EB RID: 235
	[CallbackIdentity(1103)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserAchievementStored_t
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x0000C127 File Offset: 0x0000A327
		// (set) Token: 0x06000887 RID: 2183 RVA: 0x0000C134 File Offset: 0x0000A334
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

		// Token: 0x040002DD RID: 733
		public const int k_iCallback = 1103;

		// Token: 0x040002DE RID: 734
		public ulong m_nGameID;

		// Token: 0x040002DF RID: 735
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bGroupAchievement;

		// Token: 0x040002E0 RID: 736
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_rgchAchievementName_;

		// Token: 0x040002E1 RID: 737
		public uint m_nCurProgress;

		// Token: 0x040002E2 RID: 738
		public uint m_nMaxProgress;
	}
}
