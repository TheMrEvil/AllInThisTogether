using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B4 RID: 180
	[CallbackIdentity(1317)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageDownloadUGCResult_t
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0000C007 File Offset: 0x0000A207
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x0000C014 File Offset: 0x0000A214
		public string m_pchFileName
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_pchFileName_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_pchFileName_, 260);
			}
		}

		// Token: 0x040001F6 RID: 502
		public const int k_iCallback = 1317;

		// Token: 0x040001F7 RID: 503
		public EResult m_eResult;

		// Token: 0x040001F8 RID: 504
		public UGCHandle_t m_hFile;

		// Token: 0x040001F9 RID: 505
		public AppId_t m_nAppID;

		// Token: 0x040001FA RID: 506
		public int m_nSizeInBytes;

		// Token: 0x040001FB RID: 507
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
		private byte[] m_pchFileName_;

		// Token: 0x040001FC RID: 508
		public ulong m_ulSteamIDOwner;
	}
}
