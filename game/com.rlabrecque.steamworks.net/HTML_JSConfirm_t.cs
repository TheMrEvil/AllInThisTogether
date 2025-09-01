using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000062 RID: 98
	[CallbackIdentity(4515)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_JSConfirm_t
	{
		// Token: 0x040000E6 RID: 230
		public const int k_iCallback = 4515;

		// Token: 0x040000E7 RID: 231
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000E8 RID: 232
		public string pchMessage;
	}
}
