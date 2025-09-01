using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000068 RID: 104
	[CallbackIdentity(4525)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_UpdateToolTip_t
	{
		// Token: 0x040000FE RID: 254
		public const int k_iCallback = 4525;

		// Token: 0x040000FF RID: 255
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000100 RID: 256
		public string pchMsg;
	}
}
