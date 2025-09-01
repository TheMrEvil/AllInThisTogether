using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AB RID: 171
	[CallbackIdentity(5703)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamRemotePlayTogetherGuestInvite_t
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x0000BFC7 File Offset: 0x0000A1C7
		// (set) Token: 0x06000871 RID: 2161 RVA: 0x0000BFD4 File Offset: 0x0000A1D4
		public string m_szConnectURL
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_szConnectURL_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_szConnectURL_, 1024);
			}
		}

		// Token: 0x040001D4 RID: 468
		public const int k_iCallback = 5703;

		// Token: 0x040001D5 RID: 469
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
		private byte[] m_szConnectURL_;
	}
}
