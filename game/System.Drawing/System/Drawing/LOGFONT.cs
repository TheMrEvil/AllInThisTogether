using System;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x0200009F RID: 159
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct LOGFONT
	{
		// Token: 0x040005FA RID: 1530
		internal int lfHeight;

		// Token: 0x040005FB RID: 1531
		internal uint lfWidth;

		// Token: 0x040005FC RID: 1532
		internal uint lfEscapement;

		// Token: 0x040005FD RID: 1533
		internal uint lfOrientation;

		// Token: 0x040005FE RID: 1534
		internal uint lfWeight;

		// Token: 0x040005FF RID: 1535
		internal byte lfItalic;

		// Token: 0x04000600 RID: 1536
		internal byte lfUnderline;

		// Token: 0x04000601 RID: 1537
		internal byte lfStrikeOut;

		// Token: 0x04000602 RID: 1538
		internal byte lfCharSet;

		// Token: 0x04000603 RID: 1539
		internal byte lfOutPrecision;

		// Token: 0x04000604 RID: 1540
		internal byte lfClipPrecision;

		// Token: 0x04000605 RID: 1541
		internal byte lfQuality;

		// Token: 0x04000606 RID: 1542
		internal byte lfPitchAndFamily;

		// Token: 0x04000607 RID: 1543
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		internal string lfFaceName;
	}
}
