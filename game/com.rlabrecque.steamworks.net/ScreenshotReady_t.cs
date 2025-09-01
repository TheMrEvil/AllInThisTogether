using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C5 RID: 197
	[CallbackIdentity(2301)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ScreenshotReady_t
	{
		// Token: 0x04000251 RID: 593
		public const int k_iCallback = 2301;

		// Token: 0x04000252 RID: 594
		public ScreenshotHandle m_hLocal;

		// Token: 0x04000253 RID: 595
		public EResult m_eResult;
	}
}
