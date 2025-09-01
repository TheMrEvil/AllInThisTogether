using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x0200011D RID: 285
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	internal struct EnhMetafileHeader
	{
		// Token: 0x04000A7C RID: 2684
		public int type;

		// Token: 0x04000A7D RID: 2685
		public int size;

		// Token: 0x04000A7E RID: 2686
		public Rectangle bounds;

		// Token: 0x04000A7F RID: 2687
		public Rectangle frame;

		// Token: 0x04000A80 RID: 2688
		public int signature;

		// Token: 0x04000A81 RID: 2689
		public int version;

		// Token: 0x04000A82 RID: 2690
		public int bytes;

		// Token: 0x04000A83 RID: 2691
		public int records;

		// Token: 0x04000A84 RID: 2692
		public short handles;

		// Token: 0x04000A85 RID: 2693
		public short reserved;

		// Token: 0x04000A86 RID: 2694
		public int description;

		// Token: 0x04000A87 RID: 2695
		public int off_description;

		// Token: 0x04000A88 RID: 2696
		public int palette_entires;

		// Token: 0x04000A89 RID: 2697
		public Size device;

		// Token: 0x04000A8A RID: 2698
		public Size millimeters;
	}
}
