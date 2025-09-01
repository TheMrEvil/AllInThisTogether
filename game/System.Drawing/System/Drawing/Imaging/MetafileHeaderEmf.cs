using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x0200010E RID: 270
	[StructLayout(LayoutKind.Sequential)]
	internal class MetafileHeaderEmf
	{
		// Token: 0x06000CC8 RID: 3272 RVA: 0x00002050 File Offset: 0x00000250
		public MetafileHeaderEmf()
		{
		}

		// Token: 0x040009E8 RID: 2536
		public MetafileType type;

		// Token: 0x040009E9 RID: 2537
		public int size;

		// Token: 0x040009EA RID: 2538
		public int version;

		// Token: 0x040009EB RID: 2539
		public EmfPlusFlags emfPlusFlags;

		// Token: 0x040009EC RID: 2540
		public float dpiX;

		// Token: 0x040009ED RID: 2541
		public float dpiY;

		// Token: 0x040009EE RID: 2542
		public int X;

		// Token: 0x040009EF RID: 2543
		public int Y;

		// Token: 0x040009F0 RID: 2544
		public int Width;

		// Token: 0x040009F1 RID: 2545
		public int Height;

		// Token: 0x040009F2 RID: 2546
		public SafeNativeMethods.ENHMETAHEADER EmfHeader;

		// Token: 0x040009F3 RID: 2547
		public int EmfPlusHeaderSize;

		// Token: 0x040009F4 RID: 2548
		public int LogicalDpiX;

		// Token: 0x040009F5 RID: 2549
		public int LogicalDpiY;
	}
}
