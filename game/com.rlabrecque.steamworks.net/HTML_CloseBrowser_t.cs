using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000057 RID: 87
	[CallbackIdentity(4504)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_CloseBrowser_t
	{
		// Token: 0x040000B3 RID: 179
		public const int k_iCallback = 4504;

		// Token: 0x040000B4 RID: 180
		public HHTMLBrowser unBrowserHandle;
	}
}
