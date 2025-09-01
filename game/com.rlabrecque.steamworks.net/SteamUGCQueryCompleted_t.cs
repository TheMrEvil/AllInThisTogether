using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C7 RID: 199
	[CallbackIdentity(3401)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamUGCQueryCompleted_t
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x0000C0C7 File Offset: 0x0000A2C7
		// (set) Token: 0x06000881 RID: 2177 RVA: 0x0000C0D4 File Offset: 0x0000A2D4
		public string m_rgchNextCursor
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchNextCursor_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchNextCursor_, 256);
			}
		}

		// Token: 0x04000255 RID: 597
		public const int k_iCallback = 3401;

		// Token: 0x04000256 RID: 598
		public UGCQueryHandle_t m_handle;

		// Token: 0x04000257 RID: 599
		public EResult m_eResult;

		// Token: 0x04000258 RID: 600
		public uint m_unNumResultsReturned;

		// Token: 0x04000259 RID: 601
		public uint m_unTotalMatchingResults;

		// Token: 0x0400025A RID: 602
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bCachedData;

		// Token: 0x0400025B RID: 603
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchNextCursor_;
	}
}
