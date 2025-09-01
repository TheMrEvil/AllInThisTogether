using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000061 RID: 97
	[CallbackIdentity(4514)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_JSAlert_t
	{
		// Token: 0x040000E3 RID: 227
		public const int k_iCallback = 4514;

		// Token: 0x040000E4 RID: 228
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000E5 RID: 229
		public string pchMessage;
	}
}
