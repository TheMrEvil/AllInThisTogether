using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AC RID: 172
	[CallbackIdentity(1307)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageFileShareResult_t
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0000BFE7 File Offset: 0x0000A1E7
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x0000BFF4 File Offset: 0x0000A1F4
		public string m_rgchFilename
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchFilename_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchFilename_, 260);
			}
		}

		// Token: 0x040001D6 RID: 470
		public const int k_iCallback = 1307;

		// Token: 0x040001D7 RID: 471
		public EResult m_eResult;

		// Token: 0x040001D8 RID: 472
		public UGCHandle_t m_hFile;

		// Token: 0x040001D9 RID: 473
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
		private byte[] m_rgchFilename_;
	}
}
