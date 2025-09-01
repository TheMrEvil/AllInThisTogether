using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200005F RID: 95
	[CallbackIdentity(4512)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_VerticalScroll_t
	{
		// Token: 0x040000D5 RID: 213
		public const int k_iCallback = 4512;

		// Token: 0x040000D6 RID: 214
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000D7 RID: 215
		public uint unScrollMax;

		// Token: 0x040000D8 RID: 216
		public uint unScrollCurrent;

		// Token: 0x040000D9 RID: 217
		public float flPageScale;

		// Token: 0x040000DA RID: 218
		[MarshalAs(UnmanagedType.I1)]
		public bool bVisible;

		// Token: 0x040000DB RID: 219
		public uint unPageSize;
	}
}
