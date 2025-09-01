using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000FE RID: 254
	[CallbackIdentity(4611)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetVideoURLResult_t
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0000C167 File Offset: 0x0000A367
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x0000C174 File Offset: 0x0000A374
		public string m_rgchURL
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchURL_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchURL_, 256);
			}
		}

		// Token: 0x04000316 RID: 790
		public const int k_iCallback = 4611;

		// Token: 0x04000317 RID: 791
		public EResult m_eResult;

		// Token: 0x04000318 RID: 792
		public AppId_t m_unVideoAppID;

		// Token: 0x04000319 RID: 793
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchURL_;
	}
}
