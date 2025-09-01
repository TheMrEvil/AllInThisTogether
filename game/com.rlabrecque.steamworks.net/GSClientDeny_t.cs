using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000048 RID: 72
	[CallbackIdentity(202)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GSClientDeny_t
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0000BF0B File Offset: 0x0000A10B
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x0000BF18 File Offset: 0x0000A118
		public string m_rgchOptionalText
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchOptionalText_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchOptionalText_, 128);
			}
		}

		// Token: 0x0400006F RID: 111
		public const int k_iCallback = 202;

		// Token: 0x04000070 RID: 112
		public CSteamID m_SteamID;

		// Token: 0x04000071 RID: 113
		public EDenyReason m_eDenyReason;

		// Token: 0x04000072 RID: 114
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_rgchOptionalText_;
	}
}
