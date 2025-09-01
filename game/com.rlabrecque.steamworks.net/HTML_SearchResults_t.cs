using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200005C RID: 92
	[CallbackIdentity(4509)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_SearchResults_t
	{
		// Token: 0x040000C6 RID: 198
		public const int k_iCallback = 4509;

		// Token: 0x040000C7 RID: 199
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000C8 RID: 200
		public uint unResults;

		// Token: 0x040000C9 RID: 201
		public uint unCurrentMatch;
	}
}
