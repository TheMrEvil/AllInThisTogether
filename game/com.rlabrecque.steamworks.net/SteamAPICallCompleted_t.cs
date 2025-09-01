using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F7 RID: 247
	[CallbackIdentity(703)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamAPICallCompleted_t
	{
		// Token: 0x04000307 RID: 775
		public const int k_iCallback = 703;

		// Token: 0x04000308 RID: 776
		public SteamAPICall_t m_hAsyncCall;

		// Token: 0x04000309 RID: 777
		public int m_iCallback;

		// Token: 0x0400030A RID: 778
		public uint m_cubParam;
	}
}
