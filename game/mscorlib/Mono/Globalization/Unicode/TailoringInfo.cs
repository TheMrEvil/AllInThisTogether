using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200006C RID: 108
	internal class TailoringInfo
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00005CC0 File Offset: 0x00003EC0
		public TailoringInfo(int lcid, int tailoringIndex, int tailoringCount, bool frenchSort)
		{
			this.LCID = lcid;
			this.TailoringIndex = tailoringIndex;
			this.TailoringCount = tailoringCount;
			this.FrenchSort = frenchSort;
		}

		// Token: 0x04000E31 RID: 3633
		public readonly int LCID;

		// Token: 0x04000E32 RID: 3634
		public readonly int TailoringIndex;

		// Token: 0x04000E33 RID: 3635
		public readonly int TailoringCount;

		// Token: 0x04000E34 RID: 3636
		public readonly bool FrenchSort;
	}
}
