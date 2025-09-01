using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000064 RID: 100
	[CallbackIdentity(4521)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_NewWindow_t
	{
		// Token: 0x040000ED RID: 237
		public const int k_iCallback = 4521;

		// Token: 0x040000EE RID: 238
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000EF RID: 239
		public string pchURL;

		// Token: 0x040000F0 RID: 240
		public uint unX;

		// Token: 0x040000F1 RID: 241
		public uint unY;

		// Token: 0x040000F2 RID: 242
		public uint unWide;

		// Token: 0x040000F3 RID: 243
		public uint unTall;

		// Token: 0x040000F4 RID: 244
		public HHTMLBrowser unNewWindow_BrowserHandle_IGNORE;
	}
}
