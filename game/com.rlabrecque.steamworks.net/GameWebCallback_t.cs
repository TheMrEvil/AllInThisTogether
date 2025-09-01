using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E4 RID: 228
	[CallbackIdentity(164)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameWebCallback_t
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x0000C0E7 File Offset: 0x0000A2E7
		// (set) Token: 0x06000883 RID: 2179 RVA: 0x0000C0F4 File Offset: 0x0000A2F4
		public string m_szURL
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_szURL_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_szURL_, 256);
			}
		}

		// Token: 0x040002BE RID: 702
		public const int k_iCallback = 164;

		// Token: 0x040002BF RID: 703
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_szURL_;
	}
}
