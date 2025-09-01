using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000067 RID: 103
	[CallbackIdentity(4524)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_ShowToolTip_t
	{
		// Token: 0x040000FB RID: 251
		public const int k_iCallback = 4524;

		// Token: 0x040000FC RID: 252
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000FD RID: 253
		public string pchMsg;
	}
}
