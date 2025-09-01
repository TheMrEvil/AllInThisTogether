using System;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x02000032 RID: 50
	internal class NativeMethods
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00002050 File Offset: 0x00000250
		public NativeMethods()
		{
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005494 File Offset: 0x00003694
		// Note: this type is marked as 'beforefieldinit'.
		static NativeMethods()
		{
		}

		// Token: 0x04000308 RID: 776
		internal static HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);

		// Token: 0x04000309 RID: 777
		public const int MAX_PATH = 260;

		// Token: 0x0400030A RID: 778
		internal const int SM_REMOTESESSION = 4096;

		// Token: 0x0400030B RID: 779
		internal const int OBJ_DC = 3;

		// Token: 0x0400030C RID: 780
		internal const int OBJ_METADC = 4;

		// Token: 0x0400030D RID: 781
		internal const int OBJ_MEMDC = 10;

		// Token: 0x0400030E RID: 782
		internal const int OBJ_ENHMETADC = 12;

		// Token: 0x0400030F RID: 783
		internal const int DIB_RGB_COLORS = 0;

		// Token: 0x04000310 RID: 784
		internal const int BI_BITFIELDS = 3;

		// Token: 0x04000311 RID: 785
		internal const int BI_RGB = 0;

		// Token: 0x04000312 RID: 786
		internal const int BITMAPINFO_MAX_COLORSIZE = 256;

		// Token: 0x04000313 RID: 787
		internal const int SPI_GETICONTITLELOGFONT = 31;

		// Token: 0x04000314 RID: 788
		internal const int SPI_GETNONCLIENTMETRICS = 41;

		// Token: 0x04000315 RID: 789
		internal const int DEFAULT_GUI_FONT = 17;

		// Token: 0x02000033 RID: 51
		internal struct BITMAPINFO_FLAT
		{
			// Token: 0x04000316 RID: 790
			public int bmiHeader_biSize;

			// Token: 0x04000317 RID: 791
			public int bmiHeader_biWidth;

			// Token: 0x04000318 RID: 792
			public int bmiHeader_biHeight;

			// Token: 0x04000319 RID: 793
			public short bmiHeader_biPlanes;

			// Token: 0x0400031A RID: 794
			public short bmiHeader_biBitCount;

			// Token: 0x0400031B RID: 795
			public int bmiHeader_biCompression;

			// Token: 0x0400031C RID: 796
			public int bmiHeader_biSizeImage;

			// Token: 0x0400031D RID: 797
			public int bmiHeader_biXPelsPerMeter;

			// Token: 0x0400031E RID: 798
			public int bmiHeader_biYPelsPerMeter;

			// Token: 0x0400031F RID: 799
			public int bmiHeader_biClrUsed;

			// Token: 0x04000320 RID: 800
			public int bmiHeader_biClrImportant;

			// Token: 0x04000321 RID: 801
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
			public byte[] bmiColors;
		}

		// Token: 0x02000034 RID: 52
		[StructLayout(LayoutKind.Sequential)]
		internal class BITMAPINFOHEADER
		{
			// Token: 0x060000EB RID: 235 RVA: 0x000054A6 File Offset: 0x000036A6
			public BITMAPINFOHEADER()
			{
			}

			// Token: 0x04000322 RID: 802
			public int biSize = 40;

			// Token: 0x04000323 RID: 803
			public int biWidth;

			// Token: 0x04000324 RID: 804
			public int biHeight;

			// Token: 0x04000325 RID: 805
			public short biPlanes;

			// Token: 0x04000326 RID: 806
			public short biBitCount;

			// Token: 0x04000327 RID: 807
			public int biCompression;

			// Token: 0x04000328 RID: 808
			public int biSizeImage;

			// Token: 0x04000329 RID: 809
			public int biXPelsPerMeter;

			// Token: 0x0400032A RID: 810
			public int biYPelsPerMeter;

			// Token: 0x0400032B RID: 811
			public int biClrUsed;

			// Token: 0x0400032C RID: 812
			public int biClrImportant;
		}

		// Token: 0x02000035 RID: 53
		internal struct PALETTEENTRY
		{
			// Token: 0x0400032D RID: 813
			public byte peRed;

			// Token: 0x0400032E RID: 814
			public byte peGreen;

			// Token: 0x0400032F RID: 815
			public byte peBlue;

			// Token: 0x04000330 RID: 816
			public byte peFlags;
		}

		// Token: 0x02000036 RID: 54
		internal struct RGBQUAD
		{
			// Token: 0x04000331 RID: 817
			public byte rgbBlue;

			// Token: 0x04000332 RID: 818
			public byte rgbGreen;

			// Token: 0x04000333 RID: 819
			public byte rgbRed;

			// Token: 0x04000334 RID: 820
			public byte rgbReserved;
		}

		// Token: 0x02000037 RID: 55
		[StructLayout(LayoutKind.Sequential)]
		internal class NONCLIENTMETRICS
		{
			// Token: 0x060000EC RID: 236 RVA: 0x000054B6 File Offset: 0x000036B6
			public NONCLIENTMETRICS()
			{
			}

			// Token: 0x04000335 RID: 821
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.NONCLIENTMETRICS));

			// Token: 0x04000336 RID: 822
			public int iBorderWidth;

			// Token: 0x04000337 RID: 823
			public int iScrollWidth;

			// Token: 0x04000338 RID: 824
			public int iScrollHeight;

			// Token: 0x04000339 RID: 825
			public int iCaptionWidth;

			// Token: 0x0400033A RID: 826
			public int iCaptionHeight;

			// Token: 0x0400033B RID: 827
			[MarshalAs(UnmanagedType.Struct)]
			public SafeNativeMethods.LOGFONT lfCaptionFont;

			// Token: 0x0400033C RID: 828
			public int iSmCaptionWidth;

			// Token: 0x0400033D RID: 829
			public int iSmCaptionHeight;

			// Token: 0x0400033E RID: 830
			[MarshalAs(UnmanagedType.Struct)]
			public SafeNativeMethods.LOGFONT lfSmCaptionFont;

			// Token: 0x0400033F RID: 831
			public int iMenuWidth;

			// Token: 0x04000340 RID: 832
			public int iMenuHeight;

			// Token: 0x04000341 RID: 833
			[MarshalAs(UnmanagedType.Struct)]
			public SafeNativeMethods.LOGFONT lfMenuFont;

			// Token: 0x04000342 RID: 834
			[MarshalAs(UnmanagedType.Struct)]
			public SafeNativeMethods.LOGFONT lfStatusFont;

			// Token: 0x04000343 RID: 835
			[MarshalAs(UnmanagedType.Struct)]
			public SafeNativeMethods.LOGFONT lfMessageFont;
		}
	}
}
