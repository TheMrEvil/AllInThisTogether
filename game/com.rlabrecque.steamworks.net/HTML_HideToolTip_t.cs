using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000069 RID: 105
	[CallbackIdentity(4526)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_HideToolTip_t
	{
		// Token: 0x04000101 RID: 257
		public const int k_iCallback = 4526;

		// Token: 0x04000102 RID: 258
		public HHTMLBrowser unBrowserHandle;
	}
}
