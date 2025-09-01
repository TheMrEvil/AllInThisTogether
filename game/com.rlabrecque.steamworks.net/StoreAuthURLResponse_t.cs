using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E5 RID: 229
	[CallbackIdentity(165)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct StoreAuthURLResponse_t
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0000C107 File Offset: 0x0000A307
		// (set) Token: 0x06000885 RID: 2181 RVA: 0x0000C114 File Offset: 0x0000A314
		public string m_szURL
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_szURL_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_szURL_, 512);
			}
		}

		// Token: 0x040002C0 RID: 704
		public const int k_iCallback = 165;

		// Token: 0x040002C1 RID: 705
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
		private byte[] m_szURL_;
	}
}
