using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x0200011E RID: 286
	[StructLayout(LayoutKind.Explicit)]
	internal struct MonoMetafileHeader
	{
		// Token: 0x04000A8B RID: 2699
		[FieldOffset(0)]
		public MetafileType type;

		// Token: 0x04000A8C RID: 2700
		[FieldOffset(4)]
		public int size;

		// Token: 0x04000A8D RID: 2701
		[FieldOffset(8)]
		public int version;

		// Token: 0x04000A8E RID: 2702
		[FieldOffset(12)]
		public int emf_plus_flags;

		// Token: 0x04000A8F RID: 2703
		[FieldOffset(16)]
		public float dpi_x;

		// Token: 0x04000A90 RID: 2704
		[FieldOffset(20)]
		public float dpi_y;

		// Token: 0x04000A91 RID: 2705
		[FieldOffset(24)]
		public int x;

		// Token: 0x04000A92 RID: 2706
		[FieldOffset(28)]
		public int y;

		// Token: 0x04000A93 RID: 2707
		[FieldOffset(32)]
		public int width;

		// Token: 0x04000A94 RID: 2708
		[FieldOffset(36)]
		public int height;

		// Token: 0x04000A95 RID: 2709
		[FieldOffset(40)]
		public WmfMetaHeader wmf_header;

		// Token: 0x04000A96 RID: 2710
		[FieldOffset(40)]
		public EnhMetafileHeader emf_header;

		// Token: 0x04000A97 RID: 2711
		[FieldOffset(128)]
		public int emfplus_header_size;

		// Token: 0x04000A98 RID: 2712
		[FieldOffset(132)]
		public int logical_dpi_x;

		// Token: 0x04000A99 RID: 2713
		[FieldOffset(136)]
		public int logical_dpi_y;
	}
}
