using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000059 RID: 89
	[CallbackIdentity(4506)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_FinishedRequest_t
	{
		// Token: 0x040000BC RID: 188
		public const int k_iCallback = 4506;

		// Token: 0x040000BD RID: 189
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000BE RID: 190
		public string pchURL;

		// Token: 0x040000BF RID: 191
		public string pchPageTitle;
	}
}
