using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x0200010F RID: 271
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal class MetafileHeaderWmf
	{
		// Token: 0x06000CC9 RID: 3273 RVA: 0x0001DCB4 File Offset: 0x0001BEB4
		public MetafileHeaderWmf()
		{
		}

		// Token: 0x040009F6 RID: 2550
		public MetafileType type;

		// Token: 0x040009F7 RID: 2551
		public int size = Marshal.SizeOf(typeof(MetafileHeaderWmf));

		// Token: 0x040009F8 RID: 2552
		public int version;

		// Token: 0x040009F9 RID: 2553
		public EmfPlusFlags emfPlusFlags;

		// Token: 0x040009FA RID: 2554
		public float dpiX;

		// Token: 0x040009FB RID: 2555
		public float dpiY;

		// Token: 0x040009FC RID: 2556
		public int X;

		// Token: 0x040009FD RID: 2557
		public int Y;

		// Token: 0x040009FE RID: 2558
		public int Width;

		// Token: 0x040009FF RID: 2559
		public int Height;

		// Token: 0x04000A00 RID: 2560
		[MarshalAs(UnmanagedType.Struct)]
		public MetaHeader WmfHeader = new MetaHeader();

		// Token: 0x04000A01 RID: 2561
		public int dummy1;

		// Token: 0x04000A02 RID: 2562
		public int dummy2;

		// Token: 0x04000A03 RID: 2563
		public int dummy3;

		// Token: 0x04000A04 RID: 2564
		public int dummy4;

		// Token: 0x04000A05 RID: 2565
		public int dummy5;

		// Token: 0x04000A06 RID: 2566
		public int dummy6;

		// Token: 0x04000A07 RID: 2567
		public int dummy7;

		// Token: 0x04000A08 RID: 2568
		public int dummy8;

		// Token: 0x04000A09 RID: 2569
		public int dummy9;

		// Token: 0x04000A0A RID: 2570
		public int dummy10;

		// Token: 0x04000A0B RID: 2571
		public int dummy11;

		// Token: 0x04000A0C RID: 2572
		public int dummy12;

		// Token: 0x04000A0D RID: 2573
		public int dummy13;

		// Token: 0x04000A0E RID: 2574
		public int dummy14;

		// Token: 0x04000A0F RID: 2575
		public int dummy15;

		// Token: 0x04000A10 RID: 2576
		public int dummy16;

		// Token: 0x04000A11 RID: 2577
		public int EmfPlusHeaderSize;

		// Token: 0x04000A12 RID: 2578
		public int LogicalDpiX;

		// Token: 0x04000A13 RID: 2579
		public int LogicalDpiY;
	}
}
