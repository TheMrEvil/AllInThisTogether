using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000066 RID: 102
	[CallbackIdentity(4523)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_StatusText_t
	{
		// Token: 0x040000F8 RID: 248
		public const int k_iCallback = 4523;

		// Token: 0x040000F9 RID: 249
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000FA RID: 250
		public string pchMsg;
	}
}
