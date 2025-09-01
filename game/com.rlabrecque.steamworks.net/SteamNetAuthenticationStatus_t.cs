using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A6 RID: 166
	[CallbackIdentity(1222)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetAuthenticationStatus_t
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x0000BF87 File Offset: 0x0000A187
		// (set) Token: 0x0600086D RID: 2157 RVA: 0x0000BF94 File Offset: 0x0000A194
		public string m_debugMsg
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_debugMsg_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_debugMsg_, 256);
			}
		}

		// Token: 0x040001C6 RID: 454
		public const int k_iCallback = 1222;

		// Token: 0x040001C7 RID: 455
		public ESteamNetworkingAvailability m_eAvail;

		// Token: 0x040001C8 RID: 456
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_debugMsg_;
	}
}
