using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000036 RID: 54
	[CallbackIdentity(337)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameRichPresenceJoinRequested_t
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0000BECB File Offset: 0x0000A0CB
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x0000BED8 File Offset: 0x0000A0D8
		public string m_rgchConnect
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchConnect_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchConnect_, 256);
			}
		}

		// Token: 0x04000034 RID: 52
		public const int k_iCallback = 337;

		// Token: 0x04000035 RID: 53
		public CSteamID m_steamIDFriend;

		// Token: 0x04000036 RID: 54
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchConnect_;
	}
}
