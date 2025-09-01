using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C3 RID: 195
	[CallbackIdentity(1332)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageFileReadAsyncComplete_t
	{
		// Token: 0x0400024B RID: 587
		public const int k_iCallback = 1332;

		// Token: 0x0400024C RID: 588
		public SteamAPICall_t m_hFileReadAsync;

		// Token: 0x0400024D RID: 589
		public EResult m_eResult;

		// Token: 0x0400024E RID: 590
		public uint m_nOffset;

		// Token: 0x0400024F RID: 591
		public uint m_cubRead;
	}
}
