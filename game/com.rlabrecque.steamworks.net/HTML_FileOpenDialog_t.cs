using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000063 RID: 99
	[CallbackIdentity(4516)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_FileOpenDialog_t
	{
		// Token: 0x040000E9 RID: 233
		public const int k_iCallback = 4516;

		// Token: 0x040000EA RID: 234
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000EB RID: 235
		public string pchTitle;

		// Token: 0x040000EC RID: 236
		public string pchInitialFile;
	}
}
