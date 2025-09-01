using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200004A RID: 74
	[CallbackIdentity(206)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSClientAchievementStatus_t
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x0000BF2B File Offset: 0x0000A12B
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x0000BF38 File Offset: 0x0000A138
		public string m_pchAchievement
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_pchAchievement_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_pchAchievement_, 128);
			}
		}

		// Token: 0x04000076 RID: 118
		public const int k_iCallback = 206;

		// Token: 0x04000077 RID: 119
		public ulong m_SteamID;

		// Token: 0x04000078 RID: 120
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_pchAchievement_;

		// Token: 0x04000079 RID: 121
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUnlocked;
	}
}
