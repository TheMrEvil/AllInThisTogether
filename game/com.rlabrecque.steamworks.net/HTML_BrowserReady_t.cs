using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000054 RID: 84
	[CallbackIdentity(4501)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_BrowserReady_t
	{
		// Token: 0x0400009E RID: 158
		public const int k_iCallback = 4501;

		// Token: 0x0400009F RID: 159
		public HHTMLBrowser unBrowserHandle;
	}
}
