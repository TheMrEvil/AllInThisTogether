using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000031 RID: 49
	[CallbackIdentity(332)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameServerChangeRequested_t
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x0000BE91 File Offset: 0x0000A091
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public string m_rgchServer
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchServer_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchServer_, 64);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0000BEAE File Offset: 0x0000A0AE
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x0000BEBB File Offset: 0x0000A0BB
		public string m_rgchPassword
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchPassword_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchPassword_, 64);
			}
		}

		// Token: 0x04000022 RID: 34
		public const int k_iCallback = 332;

		// Token: 0x04000023 RID: 35
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_rgchServer_;

		// Token: 0x04000024 RID: 36
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_rgchPassword_;
	}
}
