using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200006A RID: 106
	[CallbackIdentity(4527)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_BrowserRestarted_t
	{
		// Token: 0x04000103 RID: 259
		public const int k_iCallback = 4527;

		// Token: 0x04000104 RID: 260
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000105 RID: 261
		public HHTMLBrowser unOldBrowserHandle;
	}
}
