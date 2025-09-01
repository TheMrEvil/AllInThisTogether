using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200005A RID: 90
	[CallbackIdentity(4507)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_OpenLinkInNewTab_t
	{
		// Token: 0x040000C0 RID: 192
		public const int k_iCallback = 4507;

		// Token: 0x040000C1 RID: 193
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000C2 RID: 194
		public string pchURL;
	}
}
