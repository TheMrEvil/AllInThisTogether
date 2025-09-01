using System;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Drawing
{
	// Token: 0x02000093 RID: 147
	internal class GDIPlus
	{
		// Token: 0x060007B7 RID: 1975
		[DllImport("gdiplus")]
		internal static extern Status GdiplusStartup(ref ulong token, ref GdiplusStartupInput input, ref GdiplusStartupOutput output);

		// Token: 0x060007B8 RID: 1976
		[DllImport("gdiplus")]
		internal static extern void GdiplusShutdown(ref ulong token);

		// Token: 0x060007B9 RID: 1977 RVA: 0x000168F4 File Offset: 0x00014AF4
		private static void ProcessExit(object sender, EventArgs e)
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00016900 File Offset: 0x00014B00
		static GDIPlus()
		{
			int platform = (int)Environment.OSVersion.Platform;
			if (platform == 4 || platform == 6 || platform == 128)
			{
				if (Environment.GetEnvironmentVariable("not_supported_MONO_MWF_USE_NEW_X11_BACKEND") != null || Environment.GetEnvironmentVariable("MONO_MWF_MAC_FORCE_X11") != null)
				{
					GDIPlus.UseX11Drawable = true;
				}
				else
				{
					IntPtr intPtr = Marshal.AllocHGlobal(8192);
					if (GDIPlus.uname(intPtr) != 0)
					{
						GDIPlus.UseX11Drawable = true;
					}
					else if (Marshal.PtrToStringAnsi(intPtr) == "Darwin")
					{
						GDIPlus.UseCarbonDrawable = true;
					}
					else
					{
						GDIPlus.UseX11Drawable = true;
					}
					Marshal.FreeHGlobal(intPtr);
				}
			}
			GdiplusStartupInput gdiplusStartupInput = GdiplusStartupInput.MakeGdiplusStartupInput();
			GdiplusStartupOutput gdiplusStartupOutput = GdiplusStartupOutput.MakeGdiplusStartupOutput();
			try
			{
				GDIPlus.GdiplusStartup(ref GDIPlus.GdiPlusToken, ref gdiplusStartupInput, ref gdiplusStartupOutput);
			}
			catch (TypeInitializationException)
			{
				Console.Error.WriteLine("* ERROR: Can not initialize GDI+ library{0}{0}Please check http://www.mono-project.com/Problem:GDIPlusInit for details", Environment.NewLine);
			}
			AppDomain.CurrentDomain.ProcessExit += GDIPlus.ProcessExit;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00016A0C File Offset: 0x00014C0C
		public static bool RunningOnWindows()
		{
			return !GDIPlus.UseX11Drawable && !GDIPlus.UseCarbonDrawable && !GDIPlus.UseCocoaDrawable;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00016A26 File Offset: 0x00014C26
		public static bool RunningOnUnix()
		{
			return GDIPlus.UseX11Drawable || GDIPlus.UseCarbonDrawable || GDIPlus.UseCocoaDrawable;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00016A40 File Offset: 0x00014C40
		public static void FromUnManagedMemoryToPointI(IntPtr prt, Point[] pts)
		{
			int num = Marshal.SizeOf<Point>(pts[0]);
			IntPtr ptr = prt;
			int i = 0;
			while (i < pts.Length)
			{
				pts[i] = (Point)Marshal.PtrToStructure(ptr, typeof(Point));
				i++;
				ptr = new IntPtr(ptr.ToInt64() + (long)num);
			}
			Marshal.FreeHGlobal(prt);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00016AA0 File Offset: 0x00014CA0
		public static void FromUnManagedMemoryToPoint(IntPtr prt, PointF[] pts)
		{
			int num = Marshal.SizeOf<PointF>(pts[0]);
			IntPtr ptr = prt;
			int i = 0;
			while (i < pts.Length)
			{
				pts[i] = (PointF)Marshal.PtrToStructure(ptr, typeof(PointF));
				i++;
				ptr = new IntPtr(ptr.ToInt64() + (long)num);
			}
			Marshal.FreeHGlobal(prt);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00016B00 File Offset: 0x00014D00
		public static IntPtr FromPointToUnManagedMemoryI(Point[] pts)
		{
			int num = Marshal.SizeOf<Point>(pts[0]);
			IntPtr intPtr = Marshal.AllocHGlobal(num * pts.Length);
			IntPtr ptr = intPtr;
			int i = 0;
			while (i < pts.Length)
			{
				Marshal.StructureToPtr<Point>(pts[i], ptr, false);
				i++;
				ptr = new IntPtr(ptr.ToInt64() + (long)num);
			}
			return intPtr;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00016B58 File Offset: 0x00014D58
		public static void FromUnManagedMemoryToRectangles(IntPtr prt, RectangleF[] pts)
		{
			int num = Marshal.SizeOf<RectangleF>(pts[0]);
			IntPtr ptr = prt;
			int i = 0;
			while (i < pts.Length)
			{
				pts[i] = (RectangleF)Marshal.PtrToStructure(ptr, typeof(RectangleF));
				i++;
				ptr = new IntPtr(ptr.ToInt64() + (long)num);
			}
			Marshal.FreeHGlobal(prt);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00016BB8 File Offset: 0x00014DB8
		public static IntPtr FromPointToUnManagedMemory(PointF[] pts)
		{
			int num = Marshal.SizeOf<PointF>(pts[0]);
			IntPtr intPtr = Marshal.AllocHGlobal(num * pts.Length);
			IntPtr ptr = intPtr;
			int i = 0;
			while (i < pts.Length)
			{
				Marshal.StructureToPtr<PointF>(pts[i], ptr, false);
				i++;
				ptr = new IntPtr(ptr.ToInt64() + (long)num);
			}
			return intPtr;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00016C10 File Offset: 0x00014E10
		internal static void CheckStatus(Status status)
		{
			switch (status)
			{
			case Status.Ok:
				return;
			case Status.GenericError:
				throw new Exception(Locale.GetText("Generic Error [GDI+ status: {0}]", new object[]
				{
					status
				}));
			case Status.InvalidParameter:
				throw new ArgumentException(Locale.GetText("A null reference or invalid value was found [GDI+ status: {0}]", new object[]
				{
					status
				}));
			case Status.OutOfMemory:
				throw new OutOfMemoryException(Locale.GetText("Not enough memory to complete operation [GDI+ status: {0}]", new object[]
				{
					status
				}));
			case Status.ObjectBusy:
				throw new MemberAccessException(Locale.GetText("Object is busy and cannot state allow this operation [GDI+ status: {0}]", new object[]
				{
					status
				}));
			case Status.InsufficientBuffer:
				throw new InternalBufferOverflowException(Locale.GetText("Insufficient buffer provided to complete operation [GDI+ status: {0}]", new object[]
				{
					status
				}));
			case Status.NotImplemented:
				throw new NotImplementedException(Locale.GetText("The requested feature is not implemented [GDI+ status: {0}]", new object[]
				{
					status
				}));
			case Status.Win32Error:
				throw new InvalidOperationException(Locale.GetText("The operation is invalid [GDI+ status: {0}]", new object[]
				{
					status
				}));
			case Status.WrongState:
				throw new InvalidOperationException(Locale.GetText("Object is not in a state that can allow this operation [GDI+ status: {0}]", new object[]
				{
					status
				}));
			case Status.FileNotFound:
				throw new FileNotFoundException(Locale.GetText("Requested file was not found [GDI+ status: {0}]", new object[]
				{
					status
				}));
			case Status.ValueOverflow:
				throw new OverflowException(Locale.GetText("Argument is out of range [GDI+ status: {0}]", new object[]
				{
					status
				}));
			case Status.AccessDenied:
				throw new UnauthorizedAccessException(Locale.GetText("Access to resource was denied [GDI+ status: {0}]", new object[]
				{
					status
				}));
			case Status.UnknownImageFormat:
				throw new NotSupportedException(Locale.GetText("Either the image format is unknown or you don't have the required libraries to decode this format [GDI+ status: {0}]", new object[]
				{
					status
				}));
			case Status.FontFamilyNotFound:
				throw new ArgumentException(Locale.GetText("The requested FontFamily could not be found [GDI+ status: {0}]", new object[]
				{
					status
				}));
			case Status.PropertyNotSupported:
				throw new NotSupportedException(Locale.GetText("Property not supported [GDI+ status: {0}]", new object[]
				{
					status
				}));
			}
			throw new Exception(Locale.GetText("Unknown Error [GDI+ status: {0}]", new object[]
			{
				status
			}));
		}

		// Token: 0x060007C3 RID: 1987
		[DllImport("gdiplus")]
		internal static extern IntPtr GdipAlloc(int size);

		// Token: 0x060007C4 RID: 1988
		[DllImport("gdiplus")]
		internal static extern void GdipFree(IntPtr ptr);

		// Token: 0x060007C5 RID: 1989
		[DllImport("gdiplus")]
		internal static extern int GdipCloneBrush(HandleRef brush, out IntPtr clonedBrush);

		// Token: 0x060007C6 RID: 1990
		[DllImport("gdiplus")]
		internal static extern int GdipDeleteBrush(HandleRef brush);

		// Token: 0x060007C7 RID: 1991
		[DllImport("gdiplus")]
		internal static extern int GdipGetBrushType(HandleRef brush, out BrushType type);

		// Token: 0x060007C8 RID: 1992
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateRegion(out IntPtr region);

		// Token: 0x060007C9 RID: 1993
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateRegionRgnData(byte[] data, int size, out IntPtr region);

		// Token: 0x060007CA RID: 1994
		[DllImport("gdiplus")]
		internal static extern Status GdipDeleteRegion(IntPtr region);

		// Token: 0x060007CB RID: 1995
		[DllImport("gdiplus")]
		internal static extern Status GdipCloneRegion(IntPtr region, out IntPtr cloned);

		// Token: 0x060007CC RID: 1996
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateRegionRect(ref RectangleF rect, out IntPtr region);

		// Token: 0x060007CD RID: 1997
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateRegionRectI(ref Rectangle rect, out IntPtr region);

		// Token: 0x060007CE RID: 1998
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateRegionPath(IntPtr path, out IntPtr region);

		// Token: 0x060007CF RID: 1999
		[DllImport("gdiplus")]
		internal static extern Status GdipTranslateRegion(IntPtr region, float dx, float dy);

		// Token: 0x060007D0 RID: 2000
		[DllImport("gdiplus")]
		internal static extern Status GdipTranslateRegionI(IntPtr region, int dx, int dy);

		// Token: 0x060007D1 RID: 2001
		[DllImport("gdiplus")]
		internal static extern Status GdipIsVisibleRegionPoint(IntPtr region, float x, float y, IntPtr graphics, out bool result);

		// Token: 0x060007D2 RID: 2002
		[DllImport("gdiplus")]
		internal static extern Status GdipIsVisibleRegionPointI(IntPtr region, int x, int y, IntPtr graphics, out bool result);

		// Token: 0x060007D3 RID: 2003
		[DllImport("gdiplus")]
		internal static extern Status GdipIsVisibleRegionRect(IntPtr region, float x, float y, float width, float height, IntPtr graphics, out bool result);

		// Token: 0x060007D4 RID: 2004
		[DllImport("gdiplus")]
		internal static extern Status GdipIsVisibleRegionRectI(IntPtr region, int x, int y, int width, int height, IntPtr graphics, out bool result);

		// Token: 0x060007D5 RID: 2005
		[DllImport("gdiplus")]
		internal static extern Status GdipCombineRegionRect(IntPtr region, ref RectangleF rect, CombineMode combineMode);

		// Token: 0x060007D6 RID: 2006
		[DllImport("gdiplus")]
		internal static extern Status GdipCombineRegionRectI(IntPtr region, ref Rectangle rect, CombineMode combineMode);

		// Token: 0x060007D7 RID: 2007
		[DllImport("gdiplus")]
		internal static extern Status GdipCombineRegionPath(IntPtr region, IntPtr path, CombineMode combineMode);

		// Token: 0x060007D8 RID: 2008
		[DllImport("gdiplus")]
		internal static extern Status GdipGetRegionBounds(IntPtr region, IntPtr graphics, ref RectangleF rect);

		// Token: 0x060007D9 RID: 2009
		[DllImport("gdiplus")]
		internal static extern Status GdipSetInfinite(IntPtr region);

		// Token: 0x060007DA RID: 2010
		[DllImport("gdiplus")]
		internal static extern Status GdipSetEmpty(IntPtr region);

		// Token: 0x060007DB RID: 2011
		[DllImport("gdiplus")]
		internal static extern Status GdipIsEmptyRegion(IntPtr region, IntPtr graphics, out bool result);

		// Token: 0x060007DC RID: 2012
		[DllImport("gdiplus")]
		internal static extern Status GdipIsInfiniteRegion(IntPtr region, IntPtr graphics, out bool result);

		// Token: 0x060007DD RID: 2013
		[DllImport("gdiplus")]
		internal static extern Status GdipCombineRegionRegion(IntPtr region, IntPtr region2, CombineMode combineMode);

		// Token: 0x060007DE RID: 2014
		[DllImport("gdiplus")]
		internal static extern Status GdipIsEqualRegion(IntPtr region, IntPtr region2, IntPtr graphics, out bool result);

		// Token: 0x060007DF RID: 2015
		[DllImport("gdiplus")]
		internal static extern Status GdipGetRegionDataSize(IntPtr region, out int bufferSize);

		// Token: 0x060007E0 RID: 2016
		[DllImport("gdiplus")]
		internal static extern Status GdipGetRegionData(IntPtr region, byte[] buffer, int bufferSize, out int sizeFilled);

		// Token: 0x060007E1 RID: 2017
		[DllImport("gdiplus")]
		internal static extern Status GdipGetRegionScansCount(IntPtr region, out int count, IntPtr matrix);

		// Token: 0x060007E2 RID: 2018
		[DllImport("gdiplus")]
		internal static extern Status GdipGetRegionScans(IntPtr region, IntPtr rects, out int count, IntPtr matrix);

		// Token: 0x060007E3 RID: 2019
		[DllImport("gdiplus")]
		internal static extern Status GdipTransformRegion(IntPtr region, IntPtr matrix);

		// Token: 0x060007E4 RID: 2020
		[DllImport("gdiplus")]
		internal static extern Status GdipFillRegion(IntPtr graphics, IntPtr brush, IntPtr region);

		// Token: 0x060007E5 RID: 2021
		[DllImport("gdiplus")]
		internal static extern Status GdipGetRegionHRgn(IntPtr region, IntPtr graphics, ref IntPtr hRgn);

		// Token: 0x060007E6 RID: 2022
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateRegionHrgn(IntPtr hRgn, out IntPtr region);

		// Token: 0x060007E7 RID: 2023
		[DllImport("gdiplus")]
		internal static extern int GdipCreateSolidFill(int color, out IntPtr brush);

		// Token: 0x060007E8 RID: 2024
		[DllImport("gdiplus")]
		internal static extern int GdipGetSolidFillColor(HandleRef brush, out int color);

		// Token: 0x060007E9 RID: 2025
		[DllImport("gdiplus")]
		internal static extern int GdipSetSolidFillColor(HandleRef brush, int color);

		// Token: 0x060007EA RID: 2026
		[DllImport("gdiplus")]
		internal static extern int GdipCreateHatchBrush(int hatchstyle, int foreColor, int backColor, out IntPtr brush);

		// Token: 0x060007EB RID: 2027
		[DllImport("gdiplus")]
		internal static extern int GdipGetHatchStyle(HandleRef brush, out int hatchstyle);

		// Token: 0x060007EC RID: 2028
		[DllImport("gdiplus")]
		internal static extern int GdipGetHatchForegroundColor(HandleRef brush, out int foreColor);

		// Token: 0x060007ED RID: 2029
		[DllImport("gdiplus")]
		internal static extern int GdipGetHatchBackgroundColor(HandleRef brush, out int backColor);

		// Token: 0x060007EE RID: 2030
		[DllImport("gdiplus")]
		internal static extern int GdipGetTextureImage(HandleRef texture, out IntPtr image);

		// Token: 0x060007EF RID: 2031
		[DllImport("gdiplus")]
		internal static extern int GdipCreateTexture(HandleRef image, int wrapMode, out IntPtr texture);

		// Token: 0x060007F0 RID: 2032
		[DllImport("gdiplus")]
		internal static extern int GdipCreateTextureIAI(HandleRef image, HandleRef imageAttributes, int x, int y, int width, int height, out IntPtr texture);

		// Token: 0x060007F1 RID: 2033
		[DllImport("gdiplus")]
		internal static extern int GdipCreateTextureIA(HandleRef image, HandleRef imageAttributes, float x, float y, float width, float height, out IntPtr texture);

		// Token: 0x060007F2 RID: 2034
		[DllImport("gdiplus")]
		internal static extern int GdipCreateTexture2I(HandleRef image, int wrapMode, int x, int y, int width, int height, out IntPtr texture);

		// Token: 0x060007F3 RID: 2035
		[DllImport("gdiplus")]
		internal static extern int GdipCreateTexture2(HandleRef image, int wrapMode, float x, float y, float width, float height, out IntPtr texture);

		// Token: 0x060007F4 RID: 2036
		[DllImport("gdiplus")]
		internal static extern int GdipGetTextureTransform(HandleRef texture, HandleRef matrix);

		// Token: 0x060007F5 RID: 2037
		[DllImport("gdiplus")]
		internal static extern int GdipSetTextureTransform(HandleRef texture, HandleRef matrix);

		// Token: 0x060007F6 RID: 2038
		[DllImport("gdiplus")]
		internal static extern int GdipGetTextureWrapMode(HandleRef texture, out int wrapMode);

		// Token: 0x060007F7 RID: 2039
		[DllImport("gdiplus")]
		internal static extern int GdipSetTextureWrapMode(HandleRef texture, int wrapMode);

		// Token: 0x060007F8 RID: 2040
		[DllImport("gdiplus")]
		internal static extern int GdipMultiplyTextureTransform(HandleRef texture, HandleRef matrix, MatrixOrder order);

		// Token: 0x060007F9 RID: 2041
		[DllImport("gdiplus")]
		internal static extern int GdipResetTextureTransform(HandleRef texture);

		// Token: 0x060007FA RID: 2042
		[DllImport("gdiplus")]
		internal static extern int GdipRotateTextureTransform(HandleRef texture, float angle, MatrixOrder order);

		// Token: 0x060007FB RID: 2043
		[DllImport("gdiplus")]
		internal static extern int GdipScaleTextureTransform(HandleRef texture, float sx, float sy, MatrixOrder order);

		// Token: 0x060007FC RID: 2044
		[DllImport("gdiplus")]
		internal static extern int GdipTranslateTextureTransform(HandleRef texture, float dx, float dy, MatrixOrder order);

		// Token: 0x060007FD RID: 2045
		[DllImport("gdiplus")]
		internal static extern Status GdipCreatePathGradientFromPath(IntPtr path, out IntPtr brush);

		// Token: 0x060007FE RID: 2046
		[DllImport("gdiplus")]
		internal static extern Status GdipCreatePathGradientI(Point[] points, int count, WrapMode wrapMode, out IntPtr brush);

		// Token: 0x060007FF RID: 2047
		[DllImport("gdiplus")]
		internal static extern Status GdipCreatePathGradient(PointF[] points, int count, WrapMode wrapMode, out IntPtr brush);

		// Token: 0x06000800 RID: 2048
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathGradientBlendCount(IntPtr brush, out int count);

		// Token: 0x06000801 RID: 2049
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathGradientBlend(IntPtr brush, float[] blend, float[] positions, int count);

		// Token: 0x06000802 RID: 2050
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPathGradientBlend(IntPtr brush, float[] blend, float[] positions, int count);

		// Token: 0x06000803 RID: 2051
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathGradientCenterColor(IntPtr brush, out int color);

		// Token: 0x06000804 RID: 2052
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPathGradientCenterColor(IntPtr brush, int color);

		// Token: 0x06000805 RID: 2053
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathGradientCenterPoint(IntPtr brush, out PointF point);

		// Token: 0x06000806 RID: 2054
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPathGradientCenterPoint(IntPtr brush, ref PointF point);

		// Token: 0x06000807 RID: 2055
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathGradientFocusScales(IntPtr brush, out float xScale, out float yScale);

		// Token: 0x06000808 RID: 2056
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPathGradientFocusScales(IntPtr brush, float xScale, float yScale);

		// Token: 0x06000809 RID: 2057
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathGradientPresetBlendCount(IntPtr brush, out int count);

		// Token: 0x0600080A RID: 2058
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathGradientPresetBlend(IntPtr brush, int[] blend, float[] positions, int count);

		// Token: 0x0600080B RID: 2059
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPathGradientPresetBlend(IntPtr brush, int[] blend, float[] positions, int count);

		// Token: 0x0600080C RID: 2060
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathGradientRect(IntPtr brush, out RectangleF rect);

		// Token: 0x0600080D RID: 2061
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathGradientSurroundColorCount(IntPtr brush, out int count);

		// Token: 0x0600080E RID: 2062
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathGradientSurroundColorsWithCount(IntPtr brush, int[] color, ref int count);

		// Token: 0x0600080F RID: 2063
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPathGradientSurroundColorsWithCount(IntPtr brush, int[] color, ref int count);

		// Token: 0x06000810 RID: 2064
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathGradientTransform(IntPtr brush, IntPtr matrix);

		// Token: 0x06000811 RID: 2065
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPathGradientTransform(IntPtr brush, IntPtr matrix);

		// Token: 0x06000812 RID: 2066
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathGradientWrapMode(IntPtr brush, out WrapMode wrapMode);

		// Token: 0x06000813 RID: 2067
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPathGradientWrapMode(IntPtr brush, WrapMode wrapMode);

		// Token: 0x06000814 RID: 2068
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPathGradientLinearBlend(IntPtr brush, float focus, float scale);

		// Token: 0x06000815 RID: 2069
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPathGradientSigmaBlend(IntPtr brush, float focus, float scale);

		// Token: 0x06000816 RID: 2070
		[DllImport("gdiplus")]
		internal static extern Status GdipMultiplyPathGradientTransform(IntPtr texture, IntPtr matrix, MatrixOrder order);

		// Token: 0x06000817 RID: 2071
		[DllImport("gdiplus")]
		internal static extern Status GdipResetPathGradientTransform(IntPtr brush);

		// Token: 0x06000818 RID: 2072
		[DllImport("gdiplus")]
		internal static extern Status GdipRotatePathGradientTransform(IntPtr brush, float angle, MatrixOrder order);

		// Token: 0x06000819 RID: 2073
		[DllImport("gdiplus")]
		internal static extern Status GdipScalePathGradientTransform(IntPtr brush, float sx, float sy, MatrixOrder order);

		// Token: 0x0600081A RID: 2074
		[DllImport("gdiplus")]
		internal static extern Status GdipTranslatePathGradientTransform(IntPtr brush, float dx, float dy, MatrixOrder order);

		// Token: 0x0600081B RID: 2075
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateLineBrushI(ref Point point1, ref Point point2, int color1, int color2, WrapMode wrapMode, out IntPtr brush);

		// Token: 0x0600081C RID: 2076
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateLineBrush(ref PointF point1, ref PointF point2, int color1, int color2, WrapMode wrapMode, out IntPtr brush);

		// Token: 0x0600081D RID: 2077
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateLineBrushFromRectI(ref Rectangle rect, int color1, int color2, LinearGradientMode linearGradientMode, WrapMode wrapMode, out IntPtr brush);

		// Token: 0x0600081E RID: 2078
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateLineBrushFromRect(ref RectangleF rect, int color1, int color2, LinearGradientMode linearGradientMode, WrapMode wrapMode, out IntPtr brush);

		// Token: 0x0600081F RID: 2079
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateLineBrushFromRectWithAngleI(ref Rectangle rect, int color1, int color2, float angle, bool isAngleScaleable, WrapMode wrapMode, out IntPtr brush);

		// Token: 0x06000820 RID: 2080
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateLineBrushFromRectWithAngle(ref RectangleF rect, int color1, int color2, float angle, bool isAngleScaleable, WrapMode wrapMode, out IntPtr brush);

		// Token: 0x06000821 RID: 2081
		[DllImport("gdiplus")]
		internal static extern Status GdipGetLineBlendCount(IntPtr brush, out int count);

		// Token: 0x06000822 RID: 2082
		[DllImport("gdiplus")]
		internal static extern Status GdipSetLineBlend(IntPtr brush, float[] blend, float[] positions, int count);

		// Token: 0x06000823 RID: 2083
		[DllImport("gdiplus")]
		internal static extern Status GdipGetLineBlend(IntPtr brush, float[] blend, float[] positions, int count);

		// Token: 0x06000824 RID: 2084
		[DllImport("gdiplus")]
		internal static extern Status GdipSetLineGammaCorrection(IntPtr brush, bool useGammaCorrection);

		// Token: 0x06000825 RID: 2085
		[DllImport("gdiplus")]
		internal static extern Status GdipGetLineGammaCorrection(IntPtr brush, out bool useGammaCorrection);

		// Token: 0x06000826 RID: 2086
		[DllImport("gdiplus")]
		internal static extern Status GdipGetLinePresetBlendCount(IntPtr brush, out int count);

		// Token: 0x06000827 RID: 2087
		[DllImport("gdiplus")]
		internal static extern Status GdipSetLinePresetBlend(IntPtr brush, int[] blend, float[] positions, int count);

		// Token: 0x06000828 RID: 2088
		[DllImport("gdiplus")]
		internal static extern Status GdipGetLinePresetBlend(IntPtr brush, int[] blend, float[] positions, int count);

		// Token: 0x06000829 RID: 2089
		[DllImport("gdiplus")]
		internal static extern Status GdipSetLineColors(IntPtr brush, int color1, int color2);

		// Token: 0x0600082A RID: 2090
		[DllImport("gdiplus")]
		internal static extern Status GdipGetLineColors(IntPtr brush, int[] colors);

		// Token: 0x0600082B RID: 2091
		[DllImport("gdiplus")]
		internal static extern Status GdipGetLineRectI(IntPtr brush, out Rectangle rect);

		// Token: 0x0600082C RID: 2092
		[DllImport("gdiplus")]
		internal static extern Status GdipGetLineRect(IntPtr brush, out RectangleF rect);

		// Token: 0x0600082D RID: 2093
		[DllImport("gdiplus")]
		internal static extern Status GdipSetLineTransform(IntPtr brush, IntPtr matrix);

		// Token: 0x0600082E RID: 2094
		[DllImport("gdiplus")]
		internal static extern Status GdipGetLineTransform(IntPtr brush, IntPtr matrix);

		// Token: 0x0600082F RID: 2095
		[DllImport("gdiplus")]
		internal static extern Status GdipSetLineWrapMode(IntPtr brush, WrapMode wrapMode);

		// Token: 0x06000830 RID: 2096
		[DllImport("gdiplus")]
		internal static extern Status GdipGetLineWrapMode(IntPtr brush, out WrapMode wrapMode);

		// Token: 0x06000831 RID: 2097
		[DllImport("gdiplus")]
		internal static extern Status GdipSetLineLinearBlend(IntPtr brush, float focus, float scale);

		// Token: 0x06000832 RID: 2098
		[DllImport("gdiplus")]
		internal static extern Status GdipSetLineSigmaBlend(IntPtr brush, float focus, float scale);

		// Token: 0x06000833 RID: 2099
		[DllImport("gdiplus")]
		internal static extern Status GdipMultiplyLineTransform(IntPtr brush, IntPtr matrix, MatrixOrder order);

		// Token: 0x06000834 RID: 2100
		[DllImport("gdiplus")]
		internal static extern Status GdipResetLineTransform(IntPtr brush);

		// Token: 0x06000835 RID: 2101
		[DllImport("gdiplus")]
		internal static extern Status GdipRotateLineTransform(IntPtr brush, float angle, MatrixOrder order);

		// Token: 0x06000836 RID: 2102
		[DllImport("gdiplus")]
		internal static extern Status GdipScaleLineTransform(IntPtr brush, float sx, float sy, MatrixOrder order);

		// Token: 0x06000837 RID: 2103
		[DllImport("gdiplus")]
		internal static extern Status GdipTranslateLineTransform(IntPtr brush, float dx, float dy, MatrixOrder order);

		// Token: 0x06000838 RID: 2104
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateFromHDC(IntPtr hDC, out IntPtr graphics);

		// Token: 0x06000839 RID: 2105
		[DllImport("gdiplus")]
		internal static extern Status GdipDeleteGraphics(IntPtr graphics);

		// Token: 0x0600083A RID: 2106
		[DllImport("gdiplus")]
		internal static extern Status GdipRestoreGraphics(IntPtr graphics, uint graphicsState);

		// Token: 0x0600083B RID: 2107
		[DllImport("gdiplus")]
		internal static extern Status GdipSaveGraphics(IntPtr graphics, out uint state);

		// Token: 0x0600083C RID: 2108
		[DllImport("gdiplus")]
		internal static extern Status GdipMultiplyWorldTransform(IntPtr graphics, IntPtr matrix, MatrixOrder order);

		// Token: 0x0600083D RID: 2109
		[DllImport("gdiplus")]
		internal static extern Status GdipRotateWorldTransform(IntPtr graphics, float angle, MatrixOrder order);

		// Token: 0x0600083E RID: 2110
		[DllImport("gdiplus")]
		internal static extern Status GdipTranslateWorldTransform(IntPtr graphics, float dx, float dy, MatrixOrder order);

		// Token: 0x0600083F RID: 2111
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawArc(IntPtr graphics, IntPtr pen, float x, float y, float width, float height, float startAngle, float sweepAngle);

		// Token: 0x06000840 RID: 2112
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawArcI(IntPtr graphics, IntPtr pen, int x, int y, int width, int height, float startAngle, float sweepAngle);

		// Token: 0x06000841 RID: 2113
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawBezier(IntPtr graphics, IntPtr pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4);

		// Token: 0x06000842 RID: 2114
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawBezierI(IntPtr graphics, IntPtr pen, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4);

		// Token: 0x06000843 RID: 2115
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawEllipseI(IntPtr graphics, IntPtr pen, int x, int y, int width, int height);

		// Token: 0x06000844 RID: 2116
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawEllipse(IntPtr graphics, IntPtr pen, float x, float y, float width, float height);

		// Token: 0x06000845 RID: 2117
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawLine(IntPtr graphics, IntPtr pen, float x1, float y1, float x2, float y2);

		// Token: 0x06000846 RID: 2118
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawLineI(IntPtr graphics, IntPtr pen, int x1, int y1, int x2, int y2);

		// Token: 0x06000847 RID: 2119
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawLines(IntPtr graphics, IntPtr pen, PointF[] points, int count);

		// Token: 0x06000848 RID: 2120
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawLinesI(IntPtr graphics, IntPtr pen, Point[] points, int count);

		// Token: 0x06000849 RID: 2121
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawPath(IntPtr graphics, IntPtr pen, IntPtr path);

		// Token: 0x0600084A RID: 2122
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawPie(IntPtr graphics, IntPtr pen, float x, float y, float width, float height, float startAngle, float sweepAngle);

		// Token: 0x0600084B RID: 2123
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawPieI(IntPtr graphics, IntPtr pen, int x, int y, int width, int height, float startAngle, float sweepAngle);

		// Token: 0x0600084C RID: 2124
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawPolygon(IntPtr graphics, IntPtr pen, PointF[] points, int count);

		// Token: 0x0600084D RID: 2125
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawPolygonI(IntPtr graphics, IntPtr pen, Point[] points, int count);

		// Token: 0x0600084E RID: 2126
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawRectangle(IntPtr graphics, IntPtr pen, float x, float y, float width, float height);

		// Token: 0x0600084F RID: 2127
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawRectangleI(IntPtr graphics, IntPtr pen, int x, int y, int width, int height);

		// Token: 0x06000850 RID: 2128
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawRectangles(IntPtr graphics, IntPtr pen, RectangleF[] rects, int count);

		// Token: 0x06000851 RID: 2129
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawRectanglesI(IntPtr graphics, IntPtr pen, Rectangle[] rects, int count);

		// Token: 0x06000852 RID: 2130
		[DllImport("gdiplus")]
		internal static extern Status GdipFillEllipseI(IntPtr graphics, IntPtr pen, int x, int y, int width, int height);

		// Token: 0x06000853 RID: 2131
		[DllImport("gdiplus")]
		internal static extern Status GdipFillEllipse(IntPtr graphics, IntPtr pen, float x, float y, float width, float height);

		// Token: 0x06000854 RID: 2132
		[DllImport("gdiplus")]
		internal static extern Status GdipFillPolygon(IntPtr graphics, IntPtr brush, PointF[] points, int count, FillMode fillMode);

		// Token: 0x06000855 RID: 2133
		[DllImport("gdiplus")]
		internal static extern Status GdipFillPolygonI(IntPtr graphics, IntPtr brush, Point[] points, int count, FillMode fillMode);

		// Token: 0x06000856 RID: 2134
		[DllImport("gdiplus")]
		internal static extern Status GdipFillPolygon2(IntPtr graphics, IntPtr brush, PointF[] points, int count);

		// Token: 0x06000857 RID: 2135
		[DllImport("gdiplus")]
		internal static extern Status GdipFillPolygon2I(IntPtr graphics, IntPtr brush, Point[] points, int count);

		// Token: 0x06000858 RID: 2136
		[DllImport("gdiplus")]
		internal static extern Status GdipFillRectangle(IntPtr graphics, IntPtr brush, float x1, float y1, float x2, float y2);

		// Token: 0x06000859 RID: 2137
		[DllImport("gdiplus")]
		internal static extern Status GdipFillRectangleI(IntPtr graphics, IntPtr brush, int x1, int y1, int x2, int y2);

		// Token: 0x0600085A RID: 2138
		[DllImport("gdiplus")]
		internal static extern Status GdipFillRectangles(IntPtr graphics, IntPtr brush, RectangleF[] rects, int count);

		// Token: 0x0600085B RID: 2139
		[DllImport("gdiplus")]
		internal static extern Status GdipFillRectanglesI(IntPtr graphics, IntPtr brush, Rectangle[] rects, int count);

		// Token: 0x0600085C RID: 2140
		[DllImport("gdiplus", CharSet = CharSet.Unicode)]
		internal static extern Status GdipDrawString(IntPtr graphics, string text, int len, IntPtr font, ref RectangleF rc, IntPtr format, IntPtr brush);

		// Token: 0x0600085D RID: 2141
		[DllImport("gdiplus")]
		internal static extern Status GdipGetDC(IntPtr graphics, out IntPtr hdc);

		// Token: 0x0600085E RID: 2142
		[DllImport("gdiplus")]
		internal static extern Status GdipReleaseDC(IntPtr graphics, IntPtr hdc);

		// Token: 0x0600085F RID: 2143
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawImageRectI(IntPtr graphics, IntPtr image, int x, int y, int width, int height);

		// Token: 0x06000860 RID: 2144
		[DllImport("gdiplus")]
		internal static extern Status GdipGetRenderingOrigin(IntPtr graphics, out int x, out int y);

		// Token: 0x06000861 RID: 2145
		[DllImport("gdiplus")]
		internal static extern Status GdipSetRenderingOrigin(IntPtr graphics, int x, int y);

		// Token: 0x06000862 RID: 2146
		[DllImport("gdiplus")]
		internal static extern Status GdipCloneBitmapArea(float x, float y, float width, float height, PixelFormat format, IntPtr original, out IntPtr bitmap);

		// Token: 0x06000863 RID: 2147
		[DllImport("gdiplus")]
		internal static extern Status GdipCloneBitmapAreaI(int x, int y, int width, int height, PixelFormat format, IntPtr original, out IntPtr bitmap);

		// Token: 0x06000864 RID: 2148
		[DllImport("gdiplus")]
		internal static extern Status GdipResetWorldTransform(IntPtr graphics);

		// Token: 0x06000865 RID: 2149
		[DllImport("gdiplus")]
		internal static extern Status GdipSetWorldTransform(IntPtr graphics, IntPtr matrix);

		// Token: 0x06000866 RID: 2150
		[DllImport("gdiplus")]
		internal static extern Status GdipGetWorldTransform(IntPtr graphics, IntPtr matrix);

		// Token: 0x06000867 RID: 2151
		[DllImport("gdiplus")]
		internal static extern Status GdipScaleWorldTransform(IntPtr graphics, float sx, float sy, MatrixOrder order);

		// Token: 0x06000868 RID: 2152
		[DllImport("gdiplus")]
		internal static extern Status GdipGraphicsClear(IntPtr graphics, int argb);

		// Token: 0x06000869 RID: 2153
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawClosedCurve(IntPtr graphics, IntPtr pen, PointF[] points, int count);

		// Token: 0x0600086A RID: 2154
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawClosedCurveI(IntPtr graphics, IntPtr pen, Point[] points, int count);

		// Token: 0x0600086B RID: 2155
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawClosedCurve2(IntPtr graphics, IntPtr pen, PointF[] points, int count, float tension);

		// Token: 0x0600086C RID: 2156
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawClosedCurve2I(IntPtr graphics, IntPtr pen, Point[] points, int count, float tension);

		// Token: 0x0600086D RID: 2157
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawCurve(IntPtr graphics, IntPtr pen, PointF[] points, int count);

		// Token: 0x0600086E RID: 2158
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawCurveI(IntPtr graphics, IntPtr pen, Point[] points, int count);

		// Token: 0x0600086F RID: 2159
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawCurve2(IntPtr graphics, IntPtr pen, PointF[] points, int count, float tension);

		// Token: 0x06000870 RID: 2160
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawCurve2I(IntPtr graphics, IntPtr pen, Point[] points, int count, float tension);

		// Token: 0x06000871 RID: 2161
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawCurve3(IntPtr graphics, IntPtr pen, PointF[] points, int count, int offset, int numberOfSegments, float tension);

		// Token: 0x06000872 RID: 2162
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawCurve3I(IntPtr graphics, IntPtr pen, Point[] points, int count, int offset, int numberOfSegments, float tension);

		// Token: 0x06000873 RID: 2163
		[DllImport("gdiplus")]
		internal static extern Status GdipSetClipRect(IntPtr graphics, float x, float y, float width, float height, CombineMode combineMode);

		// Token: 0x06000874 RID: 2164
		[DllImport("gdiplus")]
		internal static extern Status GdipSetClipRectI(IntPtr graphics, int x, int y, int width, int height, CombineMode combineMode);

		// Token: 0x06000875 RID: 2165
		[DllImport("gdiplus")]
		internal static extern Status GdipSetClipPath(IntPtr graphics, IntPtr path, CombineMode combineMode);

		// Token: 0x06000876 RID: 2166
		[DllImport("gdiplus")]
		internal static extern Status GdipSetClipRegion(IntPtr graphics, IntPtr region, CombineMode combineMode);

		// Token: 0x06000877 RID: 2167
		[DllImport("gdiplus")]
		internal static extern Status GdipSetClipGraphics(IntPtr graphics, IntPtr srcgraphics, CombineMode combineMode);

		// Token: 0x06000878 RID: 2168
		[DllImport("gdiplus")]
		internal static extern Status GdipResetClip(IntPtr graphics);

		// Token: 0x06000879 RID: 2169
		[DllImport("gdiplus")]
		internal static extern Status GdipEndContainer(IntPtr graphics, uint state);

		// Token: 0x0600087A RID: 2170
		[DllImport("gdiplus")]
		internal static extern Status GdipGetClip(IntPtr graphics, IntPtr region);

		// Token: 0x0600087B RID: 2171
		[DllImport("gdiplus")]
		internal static extern Status GdipFillClosedCurve(IntPtr graphics, IntPtr brush, PointF[] points, int count);

		// Token: 0x0600087C RID: 2172
		[DllImport("gdiplus")]
		internal static extern Status GdipFillClosedCurveI(IntPtr graphics, IntPtr brush, Point[] points, int count);

		// Token: 0x0600087D RID: 2173
		[DllImport("gdiplus")]
		internal static extern Status GdipFillClosedCurve2(IntPtr graphics, IntPtr brush, PointF[] points, int count, float tension, FillMode fillMode);

		// Token: 0x0600087E RID: 2174
		[DllImport("gdiplus")]
		internal static extern Status GdipFillClosedCurve2I(IntPtr graphics, IntPtr brush, Point[] points, int count, float tension, FillMode fillMode);

		// Token: 0x0600087F RID: 2175
		[DllImport("gdiplus")]
		internal static extern Status GdipFillPie(IntPtr graphics, IntPtr brush, float x, float y, float width, float height, float startAngle, float sweepAngle);

		// Token: 0x06000880 RID: 2176
		[DllImport("gdiplus")]
		internal static extern Status GdipFillPieI(IntPtr graphics, IntPtr brush, int x, int y, int width, int height, float startAngle, float sweepAngle);

		// Token: 0x06000881 RID: 2177
		[DllImport("gdiplus")]
		internal static extern Status GdipFillPath(IntPtr graphics, IntPtr brush, IntPtr path);

		// Token: 0x06000882 RID: 2178
		[DllImport("gdiplus")]
		internal static extern Status GdipGetNearestColor(IntPtr graphics, out int argb);

		// Token: 0x06000883 RID: 2179
		[DllImport("gdiplus")]
		internal static extern Status GdipIsVisiblePoint(IntPtr graphics, float x, float y, out bool result);

		// Token: 0x06000884 RID: 2180
		[DllImport("gdiplus")]
		internal static extern Status GdipIsVisiblePointI(IntPtr graphics, int x, int y, out bool result);

		// Token: 0x06000885 RID: 2181
		[DllImport("gdiplus")]
		internal static extern Status GdipIsVisibleRect(IntPtr graphics, float x, float y, float width, float height, out bool result);

		// Token: 0x06000886 RID: 2182
		[DllImport("gdiplus")]
		internal static extern Status GdipIsVisibleRectI(IntPtr graphics, int x, int y, int width, int height, out bool result);

		// Token: 0x06000887 RID: 2183
		[DllImport("gdiplus")]
		internal static extern Status GdipTransformPoints(IntPtr graphics, CoordinateSpace destSpace, CoordinateSpace srcSpace, IntPtr points, int count);

		// Token: 0x06000888 RID: 2184
		[DllImport("gdiplus")]
		internal static extern Status GdipTransformPointsI(IntPtr graphics, CoordinateSpace destSpace, CoordinateSpace srcSpace, IntPtr points, int count);

		// Token: 0x06000889 RID: 2185
		[DllImport("gdiplus")]
		internal static extern Status GdipTranslateClip(IntPtr graphics, float dx, float dy);

		// Token: 0x0600088A RID: 2186
		[DllImport("gdiplus")]
		internal static extern Status GdipTranslateClipI(IntPtr graphics, int dx, int dy);

		// Token: 0x0600088B RID: 2187
		[DllImport("gdiplus")]
		internal static extern Status GdipGetClipBounds(IntPtr graphics, out RectangleF rect);

		// Token: 0x0600088C RID: 2188
		[DllImport("gdiplus")]
		internal static extern Status GdipSetCompositingMode(IntPtr graphics, CompositingMode compositingMode);

		// Token: 0x0600088D RID: 2189
		[DllImport("gdiplus")]
		internal static extern Status GdipGetCompositingMode(IntPtr graphics, out CompositingMode compositingMode);

		// Token: 0x0600088E RID: 2190
		[DllImport("gdiplus")]
		internal static extern Status GdipSetCompositingQuality(IntPtr graphics, CompositingQuality compositingQuality);

		// Token: 0x0600088F RID: 2191
		[DllImport("gdiplus")]
		internal static extern Status GdipGetCompositingQuality(IntPtr graphics, out CompositingQuality compositingQuality);

		// Token: 0x06000890 RID: 2192
		[DllImport("gdiplus")]
		internal static extern Status GdipSetInterpolationMode(IntPtr graphics, InterpolationMode interpolationMode);

		// Token: 0x06000891 RID: 2193
		[DllImport("gdiplus")]
		internal static extern Status GdipGetInterpolationMode(IntPtr graphics, out InterpolationMode interpolationMode);

		// Token: 0x06000892 RID: 2194
		[DllImport("gdiplus")]
		internal static extern Status GdipGetDpiX(IntPtr graphics, out float dpi);

		// Token: 0x06000893 RID: 2195
		[DllImport("gdiplus")]
		internal static extern Status GdipGetDpiY(IntPtr graphics, out float dpi);

		// Token: 0x06000894 RID: 2196
		[DllImport("gdiplus")]
		internal static extern Status GdipIsClipEmpty(IntPtr graphics, out bool result);

		// Token: 0x06000895 RID: 2197
		[DllImport("gdiplus")]
		internal static extern Status GdipIsVisibleClipEmpty(IntPtr graphics, out bool result);

		// Token: 0x06000896 RID: 2198
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPageUnit(IntPtr graphics, out GraphicsUnit unit);

		// Token: 0x06000897 RID: 2199
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPageScale(IntPtr graphics, out float scale);

		// Token: 0x06000898 RID: 2200
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPageUnit(IntPtr graphics, GraphicsUnit unit);

		// Token: 0x06000899 RID: 2201
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPageScale(IntPtr graphics, float scale);

		// Token: 0x0600089A RID: 2202
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPixelOffsetMode(IntPtr graphics, PixelOffsetMode pixelOffsetMode);

		// Token: 0x0600089B RID: 2203
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPixelOffsetMode(IntPtr graphics, out PixelOffsetMode pixelOffsetMode);

		// Token: 0x0600089C RID: 2204
		[DllImport("gdiplus")]
		internal static extern Status GdipSetSmoothingMode(IntPtr graphics, SmoothingMode smoothingMode);

		// Token: 0x0600089D RID: 2205
		[DllImport("gdiplus")]
		internal static extern Status GdipGetSmoothingMode(IntPtr graphics, out SmoothingMode smoothingMode);

		// Token: 0x0600089E RID: 2206
		[DllImport("gdiplus")]
		internal static extern Status GdipSetTextContrast(IntPtr graphics, int contrast);

		// Token: 0x0600089F RID: 2207
		[DllImport("gdiplus")]
		internal static extern Status GdipGetTextContrast(IntPtr graphics, out int contrast);

		// Token: 0x060008A0 RID: 2208
		[DllImport("gdiplus")]
		internal static extern Status GdipSetTextRenderingHint(IntPtr graphics, TextRenderingHint mode);

		// Token: 0x060008A1 RID: 2209
		[DllImport("gdiplus")]
		internal static extern Status GdipGetTextRenderingHint(IntPtr graphics, out TextRenderingHint mode);

		// Token: 0x060008A2 RID: 2210
		[DllImport("gdiplus")]
		internal static extern Status GdipGetVisibleClipBounds(IntPtr graphics, out RectangleF rect);

		// Token: 0x060008A3 RID: 2211
		[DllImport("gdiplus")]
		internal static extern Status GdipFlush(IntPtr graphics, FlushIntention intention);

		// Token: 0x060008A4 RID: 2212
		[DllImport("gdiplus", CharSet = CharSet.Unicode)]
		internal static extern Status GdipAddPathString(IntPtr path, string s, int lenght, IntPtr family, int style, float emSize, ref RectangleF layoutRect, IntPtr format);

		// Token: 0x060008A5 RID: 2213
		[DllImport("gdiplus", CharSet = CharSet.Unicode)]
		internal static extern Status GdipAddPathStringI(IntPtr path, string s, int lenght, IntPtr family, int style, float emSize, ref Rectangle layoutRect, IntPtr format);

		// Token: 0x060008A6 RID: 2214
		[DllImport("gdiplus")]
		internal static extern Status GdipCreatePen1(int argb, float width, GraphicsUnit unit, out IntPtr pen);

		// Token: 0x060008A7 RID: 2215
		[DllImport("gdiplus")]
		internal static extern Status GdipCreatePen2(IntPtr brush, float width, GraphicsUnit unit, out IntPtr pen);

		// Token: 0x060008A8 RID: 2216
		[DllImport("gdiplus")]
		internal static extern Status GdipClonePen(IntPtr pen, out IntPtr clonepen);

		// Token: 0x060008A9 RID: 2217
		[DllImport("gdiplus")]
		internal static extern Status GdipDeletePen(IntPtr pen);

		// Token: 0x060008AA RID: 2218
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenBrushFill(IntPtr pen, IntPtr brush);

		// Token: 0x060008AB RID: 2219
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenBrushFill(IntPtr pen, out IntPtr brush);

		// Token: 0x060008AC RID: 2220
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenFillType(IntPtr pen, out PenType type);

		// Token: 0x060008AD RID: 2221
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenColor(IntPtr pen, int color);

		// Token: 0x060008AE RID: 2222
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenColor(IntPtr pen, out int color);

		// Token: 0x060008AF RID: 2223
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenCompoundArray(IntPtr pen, float[] dash, int count);

		// Token: 0x060008B0 RID: 2224
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenCompoundArray(IntPtr pen, float[] dash, int count);

		// Token: 0x060008B1 RID: 2225
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenCompoundCount(IntPtr pen, out int count);

		// Token: 0x060008B2 RID: 2226
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenDashCap197819(IntPtr pen, DashCap dashCap);

		// Token: 0x060008B3 RID: 2227
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenDashCap197819(IntPtr pen, out DashCap dashCap);

		// Token: 0x060008B4 RID: 2228
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenDashStyle(IntPtr pen, DashStyle dashStyle);

		// Token: 0x060008B5 RID: 2229
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenDashStyle(IntPtr pen, out DashStyle dashStyle);

		// Token: 0x060008B6 RID: 2230
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenDashOffset(IntPtr pen, float offset);

		// Token: 0x060008B7 RID: 2231
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenDashOffset(IntPtr pen, out float offset);

		// Token: 0x060008B8 RID: 2232
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenDashCount(IntPtr pen, out int count);

		// Token: 0x060008B9 RID: 2233
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenDashArray(IntPtr pen, float[] dash, int count);

		// Token: 0x060008BA RID: 2234
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenDashArray(IntPtr pen, float[] dash, int count);

		// Token: 0x060008BB RID: 2235
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenMiterLimit(IntPtr pen, float miterLimit);

		// Token: 0x060008BC RID: 2236
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenMiterLimit(IntPtr pen, out float miterLimit);

		// Token: 0x060008BD RID: 2237
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenLineJoin(IntPtr pen, LineJoin lineJoin);

		// Token: 0x060008BE RID: 2238
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenLineJoin(IntPtr pen, out LineJoin lineJoin);

		// Token: 0x060008BF RID: 2239
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenLineCap197819(IntPtr pen, LineCap startCap, LineCap endCap, DashCap dashCap);

		// Token: 0x060008C0 RID: 2240
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenMode(IntPtr pen, PenAlignment alignment);

		// Token: 0x060008C1 RID: 2241
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenMode(IntPtr pen, out PenAlignment alignment);

		// Token: 0x060008C2 RID: 2242
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenStartCap(IntPtr pen, LineCap startCap);

		// Token: 0x060008C3 RID: 2243
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenStartCap(IntPtr pen, out LineCap startCap);

		// Token: 0x060008C4 RID: 2244
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenEndCap(IntPtr pen, LineCap endCap);

		// Token: 0x060008C5 RID: 2245
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenEndCap(IntPtr pen, out LineCap endCap);

		// Token: 0x060008C6 RID: 2246
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenCustomStartCap(IntPtr pen, IntPtr customCap);

		// Token: 0x060008C7 RID: 2247
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenCustomStartCap(IntPtr pen, out IntPtr customCap);

		// Token: 0x060008C8 RID: 2248
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenCustomEndCap(IntPtr pen, IntPtr customCap);

		// Token: 0x060008C9 RID: 2249
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenCustomEndCap(IntPtr pen, out IntPtr customCap);

		// Token: 0x060008CA RID: 2250
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenTransform(IntPtr pen, IntPtr matrix);

		// Token: 0x060008CB RID: 2251
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenTransform(IntPtr pen, IntPtr matrix);

		// Token: 0x060008CC RID: 2252
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenWidth(IntPtr pen, float width);

		// Token: 0x060008CD RID: 2253
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenWidth(IntPtr pen, out float width);

		// Token: 0x060008CE RID: 2254
		[DllImport("gdiplus")]
		internal static extern Status GdipResetPenTransform(IntPtr pen);

		// Token: 0x060008CF RID: 2255
		[DllImport("gdiplus")]
		internal static extern Status GdipMultiplyPenTransform(IntPtr pen, IntPtr matrix, MatrixOrder order);

		// Token: 0x060008D0 RID: 2256
		[DllImport("gdiplus")]
		internal static extern Status GdipRotatePenTransform(IntPtr pen, float angle, MatrixOrder order);

		// Token: 0x060008D1 RID: 2257
		[DllImport("gdiplus")]
		internal static extern Status GdipScalePenTransform(IntPtr pen, float sx, float sy, MatrixOrder order);

		// Token: 0x060008D2 RID: 2258
		[DllImport("gdiplus")]
		internal static extern Status GdipTranslatePenTransform(IntPtr pen, float dx, float dy, MatrixOrder order);

		// Token: 0x060008D3 RID: 2259
		[DllImport("gdiplus")]
		internal static extern int GdipCreateCustomLineCap(HandleRef fillPath, HandleRef strokePath, LineCap baseCap, float baseInset, out IntPtr customCap);

		// Token: 0x060008D4 RID: 2260
		[DllImport("gdiplus")]
		internal static extern int GdipDeleteCustomLineCap(HandleRef customCap);

		// Token: 0x060008D5 RID: 2261
		[DllImport("gdiplus")]
		internal static extern int GdipCloneCustomLineCap(HandleRef customCap, out IntPtr clonedCap);

		// Token: 0x060008D6 RID: 2262
		[DllImport("gdiplus")]
		internal static extern int GdipSetCustomLineCapStrokeCaps(HandleRef customCap, LineCap startCap, LineCap endCap);

		// Token: 0x060008D7 RID: 2263
		[DllImport("gdiplus")]
		internal static extern int GdipGetCustomLineCapStrokeCaps(HandleRef customCap, out LineCap startCap, out LineCap endCap);

		// Token: 0x060008D8 RID: 2264
		[DllImport("gdiplus")]
		internal static extern int GdipSetCustomLineCapStrokeJoin(HandleRef customCap, LineJoin lineJoin);

		// Token: 0x060008D9 RID: 2265
		[DllImport("gdiplus")]
		internal static extern int GdipGetCustomLineCapStrokeJoin(HandleRef customCap, out LineJoin lineJoin);

		// Token: 0x060008DA RID: 2266
		[DllImport("gdiplus")]
		internal static extern int GdipSetCustomLineCapBaseCap(HandleRef customCap, LineCap baseCap);

		// Token: 0x060008DB RID: 2267
		[DllImport("gdiplus")]
		internal static extern int GdipGetCustomLineCapBaseCap(HandleRef customCap, out LineCap baseCap);

		// Token: 0x060008DC RID: 2268
		[DllImport("gdiplus")]
		internal static extern int GdipSetCustomLineCapBaseInset(HandleRef customCap, float inset);

		// Token: 0x060008DD RID: 2269
		[DllImport("gdiplus")]
		internal static extern int GdipGetCustomLineCapBaseInset(HandleRef customCap, out float inset);

		// Token: 0x060008DE RID: 2270
		[DllImport("gdiplus")]
		internal static extern int GdipSetCustomLineCapWidthScale(HandleRef customCap, float widthScale);

		// Token: 0x060008DF RID: 2271
		[DllImport("gdiplus")]
		internal static extern int GdipGetCustomLineCapWidthScale(HandleRef customCap, out float widthScale);

		// Token: 0x060008E0 RID: 2272
		[DllImport("gdiplus")]
		internal static extern int GdipCreateAdjustableArrowCap(float height, float width, bool isFilled, out IntPtr arrowCap);

		// Token: 0x060008E1 RID: 2273
		[DllImport("gdiplus")]
		internal static extern int GdipSetAdjustableArrowCapHeight(HandleRef arrowCap, float height);

		// Token: 0x060008E2 RID: 2274
		[DllImport("gdiplus")]
		internal static extern int GdipGetAdjustableArrowCapHeight(HandleRef arrowCap, out float height);

		// Token: 0x060008E3 RID: 2275
		[DllImport("gdiplus")]
		internal static extern int GdipSetAdjustableArrowCapWidth(HandleRef arrowCap, float width);

		// Token: 0x060008E4 RID: 2276
		[DllImport("gdiplus")]
		internal static extern int GdipGetAdjustableArrowCapWidth(HandleRef arrowCap, out float width);

		// Token: 0x060008E5 RID: 2277
		[DllImport("gdiplus")]
		internal static extern int GdipSetAdjustableArrowCapMiddleInset(HandleRef arrowCap, float middleInset);

		// Token: 0x060008E6 RID: 2278
		[DllImport("gdiplus")]
		internal static extern int GdipGetAdjustableArrowCapMiddleInset(HandleRef arrowCap, out float middleInset);

		// Token: 0x060008E7 RID: 2279
		[DllImport("gdiplus")]
		internal static extern int GdipSetAdjustableArrowCapFillState(HandleRef arrowCap, bool isFilled);

		// Token: 0x060008E8 RID: 2280
		[DllImport("gdiplus")]
		internal static extern int GdipGetAdjustableArrowCapFillState(HandleRef arrowCap, out bool isFilled);

		// Token: 0x060008E9 RID: 2281
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateFromHWND(IntPtr hwnd, out IntPtr graphics);

		// Token: 0x060008EA RID: 2282
		[DllImport("gdiplus", CharSet = CharSet.Unicode)]
		internal unsafe static extern Status GdipMeasureString(IntPtr graphics, string str, int length, IntPtr font, ref RectangleF layoutRect, IntPtr stringFormat, out RectangleF boundingBox, int* codepointsFitted, int* linesFilled);

		// Token: 0x060008EB RID: 2283
		[DllImport("gdiplus", CharSet = CharSet.Unicode)]
		internal static extern Status GdipMeasureCharacterRanges(IntPtr graphics, string str, int length, IntPtr font, ref RectangleF layoutRect, IntPtr stringFormat, int regcount, out IntPtr regions);

		// Token: 0x060008EC RID: 2284
		[DllImport("gdiplus")]
		internal static extern Status GdipSetStringFormatMeasurableCharacterRanges(IntPtr native, int cnt, CharacterRange[] range);

		// Token: 0x060008ED RID: 2285
		[DllImport("gdiplus")]
		internal static extern Status GdipGetStringFormatMeasurableCharacterRangeCount(IntPtr native, out int cnt);

		// Token: 0x060008EE RID: 2286
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateBitmapFromScan0(int width, int height, int stride, PixelFormat format, IntPtr scan0, out IntPtr bmp);

		// Token: 0x060008EF RID: 2287
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateBitmapFromGraphics(int width, int height, IntPtr target, out IntPtr bitmap);

		// Token: 0x060008F0 RID: 2288
		[DllImport("gdiplus")]
		internal static extern Status GdipBitmapLockBits(IntPtr bmp, ref Rectangle rc, ImageLockMode flags, PixelFormat format, [In] [Out] BitmapData bmpData);

		// Token: 0x060008F1 RID: 2289
		[DllImport("gdiplus")]
		internal static extern Status GdipBitmapSetResolution(IntPtr bmp, float xdpi, float ydpi);

		// Token: 0x060008F2 RID: 2290
		[DllImport("gdiplus")]
		internal static extern Status GdipBitmapUnlockBits(IntPtr bmp, [In] [Out] BitmapData bmpData);

		// Token: 0x060008F3 RID: 2291
		[DllImport("gdiplus")]
		internal static extern Status GdipBitmapGetPixel(IntPtr bmp, int x, int y, out int argb);

		// Token: 0x060008F4 RID: 2292
		[DllImport("gdiplus")]
		internal static extern Status GdipBitmapSetPixel(IntPtr bmp, int x, int y, int argb);

		// Token: 0x060008F5 RID: 2293
		[DllImport("gdiplus", CharSet = CharSet.Auto)]
		internal static extern Status GdipLoadImageFromFile([MarshalAs(UnmanagedType.LPWStr)] string filename, out IntPtr image);

		// Token: 0x060008F6 RID: 2294
		[DllImport("gdiplus", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern Status GdipLoadImageFromStream([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Drawing.ComIStreamMarshaler)] IStream stream, out IntPtr image);

		// Token: 0x060008F7 RID: 2295
		[DllImport("gdiplus", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern Status GdipSaveImageToStream(HandleRef image, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Drawing.ComIStreamMarshaler)] IStream stream, [In] ref Guid clsidEncoder, HandleRef encoderParams);

		// Token: 0x060008F8 RID: 2296
		[DllImport("gdiplus")]
		internal static extern Status GdipCloneImage(IntPtr image, out IntPtr imageclone);

		// Token: 0x060008F9 RID: 2297
		[DllImport("gdiplus", CharSet = CharSet.Auto)]
		internal static extern Status GdipLoadImageFromFileICM([MarshalAs(UnmanagedType.LPWStr)] string filename, out IntPtr image);

		// Token: 0x060008FA RID: 2298
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateBitmapFromHBITMAP(IntPtr hBitMap, IntPtr gdiPalette, out IntPtr image);

		// Token: 0x060008FB RID: 2299
		[DllImport("gdiplus")]
		internal static extern Status GdipDisposeImage(IntPtr image);

		// Token: 0x060008FC RID: 2300
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImageFlags(IntPtr image, out int flag);

		// Token: 0x060008FD RID: 2301
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImageType(IntPtr image, out ImageType type);

		// Token: 0x060008FE RID: 2302
		[DllImport("gdiplus")]
		internal static extern Status GdipImageGetFrameDimensionsCount(IntPtr image, out uint count);

		// Token: 0x060008FF RID: 2303
		[DllImport("gdiplus")]
		internal static extern Status GdipImageGetFrameDimensionsList(IntPtr image, [Out] Guid[] dimensionIDs, uint count);

		// Token: 0x06000900 RID: 2304
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImageHeight(IntPtr image, out uint height);

		// Token: 0x06000901 RID: 2305
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImageHorizontalResolution(IntPtr image, out float resolution);

		// Token: 0x06000902 RID: 2306
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImagePaletteSize(IntPtr image, out int size);

		// Token: 0x06000903 RID: 2307
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImagePalette(IntPtr image, IntPtr palette, int size);

		// Token: 0x06000904 RID: 2308
		[DllImport("gdiplus")]
		internal static extern Status GdipSetImagePalette(IntPtr image, IntPtr palette);

		// Token: 0x06000905 RID: 2309
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImageDimension(IntPtr image, out float width, out float height);

		// Token: 0x06000906 RID: 2310
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImagePixelFormat(IntPtr image, out PixelFormat format);

		// Token: 0x06000907 RID: 2311
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPropertyCount(IntPtr image, out uint propNumbers);

		// Token: 0x06000908 RID: 2312
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPropertyIdList(IntPtr image, uint propNumbers, [Out] int[] list);

		// Token: 0x06000909 RID: 2313
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPropertySize(IntPtr image, out int bufferSize, out int propNumbers);

		// Token: 0x0600090A RID: 2314
		[DllImport("gdiplus")]
		internal static extern Status GdipGetAllPropertyItems(IntPtr image, int bufferSize, int propNumbers, IntPtr items);

		// Token: 0x0600090B RID: 2315
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImageRawFormat(IntPtr image, out Guid format);

		// Token: 0x0600090C RID: 2316
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImageVerticalResolution(IntPtr image, out float resolution);

		// Token: 0x0600090D RID: 2317
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImageWidth(IntPtr image, out uint width);

		// Token: 0x0600090E RID: 2318
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImageBounds(IntPtr image, out RectangleF source, ref GraphicsUnit unit);

		// Token: 0x0600090F RID: 2319
		[DllImport("gdiplus")]
		internal static extern Status GdipGetEncoderParameterListSize(IntPtr image, ref Guid encoder, out uint size);

		// Token: 0x06000910 RID: 2320
		[DllImport("gdiplus")]
		internal static extern Status GdipGetEncoderParameterList(IntPtr image, ref Guid encoder, uint size, IntPtr buffer);

		// Token: 0x06000911 RID: 2321
		[DllImport("gdiplus")]
		internal static extern Status GdipImageGetFrameCount(IntPtr image, ref Guid guidDimension, out uint count);

		// Token: 0x06000912 RID: 2322
		[DllImport("gdiplus")]
		internal static extern Status GdipImageSelectActiveFrame(IntPtr image, ref Guid guidDimension, int frameIndex);

		// Token: 0x06000913 RID: 2323
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPropertyItemSize(IntPtr image, int propertyID, out int propertySize);

		// Token: 0x06000914 RID: 2324
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPropertyItem(IntPtr image, int propertyID, int propertySize, IntPtr buffer);

		// Token: 0x06000915 RID: 2325
		[DllImport("gdiplus")]
		internal static extern Status GdipRemovePropertyItem(IntPtr image, int propertyId);

		// Token: 0x06000916 RID: 2326
		[DllImport("gdiplus")]
		internal unsafe static extern Status GdipSetPropertyItem(IntPtr image, GdipPropertyItem* propertyItem);

		// Token: 0x06000917 RID: 2327
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImageThumbnail(IntPtr image, uint width, uint height, out IntPtr thumbImage, IntPtr callback, IntPtr callBackData);

		// Token: 0x06000918 RID: 2328
		[DllImport("gdiplus")]
		internal static extern Status GdipImageRotateFlip(IntPtr image, RotateFlipType rotateFlipType);

		// Token: 0x06000919 RID: 2329
		[DllImport("gdiplus", CharSet = CharSet.Unicode)]
		internal static extern Status GdipSaveImageToFile(IntPtr image, string filename, ref Guid encoderClsID, IntPtr encoderParameters);

		// Token: 0x0600091A RID: 2330
		[DllImport("gdiplus")]
		internal static extern Status GdipSaveAdd(IntPtr image, IntPtr encoderParameters);

		// Token: 0x0600091B RID: 2331
		[DllImport("gdiplus")]
		internal static extern Status GdipSaveAddImage(IntPtr image, IntPtr imagenew, IntPtr encoderParameters);

		// Token: 0x0600091C RID: 2332
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawImageI(IntPtr graphics, IntPtr image, int x, int y);

		// Token: 0x0600091D RID: 2333
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImageGraphicsContext(IntPtr image, out IntPtr graphics);

		// Token: 0x0600091E RID: 2334
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawImage(IntPtr graphics, IntPtr image, float x, float y);

		// Token: 0x0600091F RID: 2335
		[DllImport("gdiplus")]
		internal static extern Status GdipBeginContainer(IntPtr graphics, ref RectangleF dstrect, ref RectangleF srcrect, GraphicsUnit unit, out uint state);

		// Token: 0x06000920 RID: 2336
		[DllImport("gdiplus")]
		internal static extern Status GdipBeginContainerI(IntPtr graphics, ref Rectangle dstrect, ref Rectangle srcrect, GraphicsUnit unit, out uint state);

		// Token: 0x06000921 RID: 2337
		[DllImport("gdiplus")]
		internal static extern Status GdipBeginContainer2(IntPtr graphics, out uint state);

		// Token: 0x06000922 RID: 2338
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawImagePoints(IntPtr graphics, IntPtr image, PointF[] destPoints, int count);

		// Token: 0x06000923 RID: 2339
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawImagePointsI(IntPtr graphics, IntPtr image, Point[] destPoints, int count);

		// Token: 0x06000924 RID: 2340
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawImageRectRectI(IntPtr graphics, IntPtr image, int dstx, int dsty, int dstwidth, int dstheight, int srcx, int srcy, int srcwidth, int srcheight, GraphicsUnit srcUnit, IntPtr imageattr, Graphics.DrawImageAbort callback, IntPtr callbackData);

		// Token: 0x06000925 RID: 2341
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawImageRectRect(IntPtr graphics, IntPtr image, float dstx, float dsty, float dstwidth, float dstheight, float srcx, float srcy, float srcwidth, float srcheight, GraphicsUnit srcUnit, IntPtr imageattr, Graphics.DrawImageAbort callback, IntPtr callbackData);

		// Token: 0x06000926 RID: 2342
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawImagePointsRectI(IntPtr graphics, IntPtr image, Point[] destPoints, int count, int srcx, int srcy, int srcwidth, int srcheight, GraphicsUnit srcUnit, IntPtr imageattr, Graphics.DrawImageAbort callback, IntPtr callbackData);

		// Token: 0x06000927 RID: 2343
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawImagePointsRect(IntPtr graphics, IntPtr image, PointF[] destPoints, int count, float srcx, float srcy, float srcwidth, float srcheight, GraphicsUnit srcUnit, IntPtr imageattr, Graphics.DrawImageAbort callback, IntPtr callbackData);

		// Token: 0x06000928 RID: 2344
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawImageRect(IntPtr graphics, IntPtr image, float x, float y, float width, float height);

		// Token: 0x06000929 RID: 2345
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawImagePointRect(IntPtr graphics, IntPtr image, float x, float y, float srcx, float srcy, float srcwidth, float srcheight, GraphicsUnit srcUnit);

		// Token: 0x0600092A RID: 2346
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawImagePointRectI(IntPtr graphics, IntPtr image, int x, int y, int srcx, int srcy, int srcwidth, int srcheight, GraphicsUnit srcUnit);

		// Token: 0x0600092B RID: 2347
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateStringFormat(StringFormatFlags formatAttributes, int language, out IntPtr native);

		// Token: 0x0600092C RID: 2348
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateHBITMAPFromBitmap(IntPtr bmp, out IntPtr HandleBmp, int clrbackground);

		// Token: 0x0600092D RID: 2349
		[DllImport("gdiplus", CharSet = CharSet.Auto)]
		internal static extern Status GdipCreateBitmapFromFile([MarshalAs(UnmanagedType.LPWStr)] string filename, out IntPtr bitmap);

		// Token: 0x0600092E RID: 2350
		[DllImport("gdiplus", CharSet = CharSet.Auto)]
		internal static extern Status GdipCreateBitmapFromFileICM([MarshalAs(UnmanagedType.LPWStr)] string filename, out IntPtr bitmap);

		// Token: 0x0600092F RID: 2351
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateHICONFromBitmap(IntPtr bmp, out IntPtr HandleIcon);

		// Token: 0x06000930 RID: 2352
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateBitmapFromHICON(IntPtr hicon, out IntPtr bitmap);

		// Token: 0x06000931 RID: 2353
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateBitmapFromResource(IntPtr hInstance, string lpBitmapName, out IntPtr bitmap);

		// Token: 0x06000932 RID: 2354
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateMatrix(out IntPtr matrix);

		// Token: 0x06000933 RID: 2355
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateMatrix2(float m11, float m12, float m21, float m22, float dx, float dy, out IntPtr matrix);

		// Token: 0x06000934 RID: 2356
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateMatrix3(ref RectangleF rect, PointF[] dstplg, out IntPtr matrix);

		// Token: 0x06000935 RID: 2357
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateMatrix3I(ref Rectangle rect, Point[] dstplg, out IntPtr matrix);

		// Token: 0x06000936 RID: 2358
		[DllImport("gdiplus")]
		internal static extern Status GdipDeleteMatrix(IntPtr matrix);

		// Token: 0x06000937 RID: 2359
		[DllImport("gdiplus")]
		internal static extern Status GdipCloneMatrix(IntPtr matrix, out IntPtr cloneMatrix);

		// Token: 0x06000938 RID: 2360
		[DllImport("gdiplus")]
		internal static extern Status GdipSetMatrixElements(IntPtr matrix, float m11, float m12, float m21, float m22, float dx, float dy);

		// Token: 0x06000939 RID: 2361
		[DllImport("gdiplus")]
		internal static extern Status GdipGetMatrixElements(IntPtr matrix, IntPtr matrixOut);

		// Token: 0x0600093A RID: 2362
		[DllImport("gdiplus")]
		internal static extern Status GdipMultiplyMatrix(IntPtr matrix, IntPtr matrix2, MatrixOrder order);

		// Token: 0x0600093B RID: 2363
		[DllImport("gdiplus")]
		internal static extern Status GdipTranslateMatrix(IntPtr matrix, float offsetX, float offsetY, MatrixOrder order);

		// Token: 0x0600093C RID: 2364
		[DllImport("gdiplus")]
		internal static extern Status GdipScaleMatrix(IntPtr matrix, float scaleX, float scaleY, MatrixOrder order);

		// Token: 0x0600093D RID: 2365
		[DllImport("gdiplus")]
		internal static extern Status GdipRotateMatrix(IntPtr matrix, float angle, MatrixOrder order);

		// Token: 0x0600093E RID: 2366
		[DllImport("gdiplus")]
		internal static extern Status GdipShearMatrix(IntPtr matrix, float shearX, float shearY, MatrixOrder order);

		// Token: 0x0600093F RID: 2367
		[DllImport("gdiplus")]
		internal static extern Status GdipInvertMatrix(IntPtr matrix);

		// Token: 0x06000940 RID: 2368
		[DllImport("gdiplus")]
		internal static extern Status GdipTransformMatrixPoints(IntPtr matrix, [In] [Out] PointF[] pts, int count);

		// Token: 0x06000941 RID: 2369
		[DllImport("gdiplus")]
		internal static extern Status GdipTransformMatrixPointsI(IntPtr matrix, [In] [Out] Point[] pts, int count);

		// Token: 0x06000942 RID: 2370
		[DllImport("gdiplus")]
		internal static extern Status GdipVectorTransformMatrixPoints(IntPtr matrix, [In] [Out] PointF[] pts, int count);

		// Token: 0x06000943 RID: 2371
		[DllImport("gdiplus")]
		internal static extern Status GdipVectorTransformMatrixPointsI(IntPtr matrix, [In] [Out] Point[] pts, int count);

		// Token: 0x06000944 RID: 2372
		[DllImport("gdiplus")]
		internal static extern Status GdipIsMatrixInvertible(IntPtr matrix, out bool result);

		// Token: 0x06000945 RID: 2373
		[DllImport("gdiplus")]
		internal static extern Status GdipIsMatrixIdentity(IntPtr matrix, out bool result);

		// Token: 0x06000946 RID: 2374
		[DllImport("gdiplus")]
		internal static extern Status GdipIsMatrixEqual(IntPtr matrix, IntPtr matrix2, out bool result);

		// Token: 0x06000947 RID: 2375
		[DllImport("gdiplus")]
		internal static extern Status GdipCreatePath(FillMode brushMode, out IntPtr path);

		// Token: 0x06000948 RID: 2376
		[DllImport("gdiplus")]
		internal static extern Status GdipCreatePath2(PointF[] points, byte[] types, int count, FillMode brushMode, out IntPtr path);

		// Token: 0x06000949 RID: 2377
		[DllImport("gdiplus")]
		internal static extern Status GdipCreatePath2I(Point[] points, byte[] types, int count, FillMode brushMode, out IntPtr path);

		// Token: 0x0600094A RID: 2378
		[DllImport("gdiplus")]
		internal static extern Status GdipClonePath(IntPtr path, out IntPtr clonePath);

		// Token: 0x0600094B RID: 2379
		[DllImport("gdiplus")]
		internal static extern Status GdipDeletePath(IntPtr path);

		// Token: 0x0600094C RID: 2380
		[DllImport("gdiplus")]
		internal static extern Status GdipResetPath(IntPtr path);

		// Token: 0x0600094D RID: 2381
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPointCount(IntPtr path, out int count);

		// Token: 0x0600094E RID: 2382
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathTypes(IntPtr path, [Out] byte[] types, int count);

		// Token: 0x0600094F RID: 2383
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathPoints(IntPtr path, [Out] PointF[] points, int count);

		// Token: 0x06000950 RID: 2384
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathPointsI(IntPtr path, [Out] Point[] points, int count);

		// Token: 0x06000951 RID: 2385
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathFillMode(IntPtr path, out FillMode fillMode);

		// Token: 0x06000952 RID: 2386
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPathFillMode(IntPtr path, FillMode fillMode);

		// Token: 0x06000953 RID: 2387
		[DllImport("gdiplus")]
		internal static extern Status GdipStartPathFigure(IntPtr path);

		// Token: 0x06000954 RID: 2388
		[DllImport("gdiplus")]
		internal static extern Status GdipClosePathFigure(IntPtr path);

		// Token: 0x06000955 RID: 2389
		[DllImport("gdiplus")]
		internal static extern Status GdipClosePathFigures(IntPtr path);

		// Token: 0x06000956 RID: 2390
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPathMarker(IntPtr path);

		// Token: 0x06000957 RID: 2391
		[DllImport("gdiplus")]
		internal static extern Status GdipClearPathMarkers(IntPtr path);

		// Token: 0x06000958 RID: 2392
		[DllImport("gdiplus")]
		internal static extern Status GdipReversePath(IntPtr path);

		// Token: 0x06000959 RID: 2393
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathLastPoint(IntPtr path, out PointF lastPoint);

		// Token: 0x0600095A RID: 2394
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathLine(IntPtr path, float x1, float y1, float x2, float y2);

		// Token: 0x0600095B RID: 2395
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathLine2(IntPtr path, PointF[] points, int count);

		// Token: 0x0600095C RID: 2396
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathLine2I(IntPtr path, Point[] points, int count);

		// Token: 0x0600095D RID: 2397
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathArc(IntPtr path, float x, float y, float width, float height, float startAngle, float sweepAngle);

		// Token: 0x0600095E RID: 2398
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathBezier(IntPtr path, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4);

		// Token: 0x0600095F RID: 2399
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathBeziers(IntPtr path, PointF[] points, int count);

		// Token: 0x06000960 RID: 2400
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathCurve(IntPtr path, PointF[] points, int count);

		// Token: 0x06000961 RID: 2401
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathCurveI(IntPtr path, Point[] points, int count);

		// Token: 0x06000962 RID: 2402
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathCurve2(IntPtr path, PointF[] points, int count, float tension);

		// Token: 0x06000963 RID: 2403
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathCurve2I(IntPtr path, Point[] points, int count, float tension);

		// Token: 0x06000964 RID: 2404
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathCurve3(IntPtr path, PointF[] points, int count, int offset, int numberOfSegments, float tension);

		// Token: 0x06000965 RID: 2405
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathCurve3I(IntPtr path, Point[] points, int count, int offset, int numberOfSegments, float tension);

		// Token: 0x06000966 RID: 2406
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathClosedCurve(IntPtr path, PointF[] points, int count);

		// Token: 0x06000967 RID: 2407
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathClosedCurveI(IntPtr path, Point[] points, int count);

		// Token: 0x06000968 RID: 2408
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathClosedCurve2(IntPtr path, PointF[] points, int count, float tension);

		// Token: 0x06000969 RID: 2409
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathClosedCurve2I(IntPtr path, Point[] points, int count, float tension);

		// Token: 0x0600096A RID: 2410
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathRectangle(IntPtr path, float x, float y, float width, float height);

		// Token: 0x0600096B RID: 2411
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathRectangles(IntPtr path, RectangleF[] rects, int count);

		// Token: 0x0600096C RID: 2412
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathEllipse(IntPtr path, float x, float y, float width, float height);

		// Token: 0x0600096D RID: 2413
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathEllipseI(IntPtr path, int x, int y, int width, int height);

		// Token: 0x0600096E RID: 2414
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathPie(IntPtr path, float x, float y, float width, float height, float startAngle, float sweepAngle);

		// Token: 0x0600096F RID: 2415
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathPieI(IntPtr path, int x, int y, int width, int height, float startAngle, float sweepAngle);

		// Token: 0x06000970 RID: 2416
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathPolygon(IntPtr path, PointF[] points, int count);

		// Token: 0x06000971 RID: 2417
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathPath(IntPtr path, IntPtr addingPath, bool connect);

		// Token: 0x06000972 RID: 2418
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathLineI(IntPtr path, int x1, int y1, int x2, int y2);

		// Token: 0x06000973 RID: 2419
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathArcI(IntPtr path, int x, int y, int width, int height, float startAngle, float sweepAngle);

		// Token: 0x06000974 RID: 2420
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathBezierI(IntPtr path, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4);

		// Token: 0x06000975 RID: 2421
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathBeziersI(IntPtr path, Point[] points, int count);

		// Token: 0x06000976 RID: 2422
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathPolygonI(IntPtr path, Point[] points, int count);

		// Token: 0x06000977 RID: 2423
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathRectangleI(IntPtr path, int x, int y, int width, int height);

		// Token: 0x06000978 RID: 2424
		[DllImport("gdiplus")]
		internal static extern Status GdipAddPathRectanglesI(IntPtr path, Rectangle[] rects, int count);

		// Token: 0x06000979 RID: 2425
		[DllImport("gdiplus")]
		internal static extern Status GdipFlattenPath(IntPtr path, IntPtr matrix, float floatness);

		// Token: 0x0600097A RID: 2426
		[DllImport("gdiplus")]
		internal static extern Status GdipTransformPath(IntPtr path, IntPtr matrix);

		// Token: 0x0600097B RID: 2427
		[DllImport("gdiplus")]
		internal static extern Status GdipWarpPath(IntPtr path, IntPtr matrix, PointF[] points, int count, float srcx, float srcy, float srcwidth, float srcheight, WarpMode mode, float flatness);

		// Token: 0x0600097C RID: 2428
		[DllImport("gdiplus")]
		internal static extern Status GdipWidenPath(IntPtr path, IntPtr pen, IntPtr matrix, float flatness);

		// Token: 0x0600097D RID: 2429
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathWorldBounds(IntPtr path, out RectangleF bounds, IntPtr matrix, IntPtr pen);

		// Token: 0x0600097E RID: 2430
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPathWorldBoundsI(IntPtr path, out Rectangle bounds, IntPtr matrix, IntPtr pen);

		// Token: 0x0600097F RID: 2431
		[DllImport("gdiplus")]
		internal static extern Status GdipIsVisiblePathPoint(IntPtr path, float x, float y, IntPtr graphics, out bool result);

		// Token: 0x06000980 RID: 2432
		[DllImport("gdiplus")]
		internal static extern Status GdipIsVisiblePathPointI(IntPtr path, int x, int y, IntPtr graphics, out bool result);

		// Token: 0x06000981 RID: 2433
		[DllImport("gdiplus")]
		internal static extern Status GdipIsOutlineVisiblePathPoint(IntPtr path, float x, float y, IntPtr pen, IntPtr graphics, out bool result);

		// Token: 0x06000982 RID: 2434
		[DllImport("gdiplus")]
		internal static extern Status GdipIsOutlineVisiblePathPointI(IntPtr path, int x, int y, IntPtr pen, IntPtr graphics, out bool result);

		// Token: 0x06000983 RID: 2435
		[DllImport("gdiplus")]
		internal static extern int GdipCreatePathIter(out IntPtr iterator, HandleRef path);

		// Token: 0x06000984 RID: 2436
		[DllImport("gdiplus")]
		internal static extern int GdipPathIterGetCount(HandleRef iterator, out int count);

		// Token: 0x06000985 RID: 2437
		[DllImport("gdiplus")]
		internal static extern int GdipPathIterGetSubpathCount(HandleRef iterator, out int count);

		// Token: 0x06000986 RID: 2438
		[DllImport("gdiplus")]
		internal static extern int GdipDeletePathIter(HandleRef iterator);

		// Token: 0x06000987 RID: 2439
		[DllImport("gdiplus")]
		internal static extern int GdipPathIterCopyData(HandleRef iterator, out int resultCount, IntPtr points, byte[] types, int startIndex, int endIndex);

		// Token: 0x06000988 RID: 2440
		[DllImport("gdiplus")]
		internal static extern int GdipPathIterEnumerate(HandleRef iterator, out int resultCount, IntPtr points, byte[] types, int count);

		// Token: 0x06000989 RID: 2441
		[DllImport("gdiplus")]
		internal static extern int GdipPathIterHasCurve(HandleRef iterator, out bool curve);

		// Token: 0x0600098A RID: 2442
		[DllImport("gdiplus")]
		internal static extern int GdipPathIterNextMarkerPath(HandleRef iterator, out int resultCount, HandleRef path);

		// Token: 0x0600098B RID: 2443
		[DllImport("gdiplus")]
		internal static extern int GdipPathIterNextMarker(HandleRef iterator, out int resultCount, out int startIndex, out int endIndex);

		// Token: 0x0600098C RID: 2444
		[DllImport("gdiplus")]
		internal static extern int GdipPathIterNextPathType(HandleRef iterator, out int resultCount, out byte pathType, out int startIndex, out int endIndex);

		// Token: 0x0600098D RID: 2445
		[DllImport("gdiplus")]
		internal static extern int GdipPathIterNextSubpathPath(HandleRef iterator, out int resultCount, HandleRef path, out bool isClosed);

		// Token: 0x0600098E RID: 2446
		[DllImport("gdiplus")]
		internal static extern int GdipPathIterNextSubpath(HandleRef iterator, out int resultCount, out int startIndex, out int endIndex, out bool isClosed);

		// Token: 0x0600098F RID: 2447
		[DllImport("gdiplus")]
		internal static extern int GdipPathIterRewind(HandleRef iterator);

		// Token: 0x06000990 RID: 2448
		[DllImport("gdiplus")]
		internal static extern int GdipCreateImageAttributes(out IntPtr imageattr);

		// Token: 0x06000991 RID: 2449
		[DllImport("gdiplus")]
		internal static extern int GdipSetImageAttributesColorKeys(HandleRef imageattr, ColorAdjustType type, bool enableFlag, int colorLow, int colorHigh);

		// Token: 0x06000992 RID: 2450
		[DllImport("gdiplus")]
		internal static extern int GdipDisposeImageAttributes(HandleRef imageattr);

		// Token: 0x06000993 RID: 2451
		[DllImport("gdiplus")]
		internal static extern int GdipSetImageAttributesColorMatrix(HandleRef imageattr, ColorAdjustType type, bool enableFlag, ColorMatrix colorMatrix, ColorMatrix grayMatrix, ColorMatrixFlag flags);

		// Token: 0x06000994 RID: 2452
		[DllImport("gdiplus")]
		internal static extern int GdipSetImageAttributesGamma(HandleRef imageattr, ColorAdjustType type, bool enableFlag, float gamma);

		// Token: 0x06000995 RID: 2453
		[DllImport("gdiplus")]
		internal static extern int GdipSetImageAttributesNoOp(HandleRef imageattr, ColorAdjustType type, bool enableFlag);

		// Token: 0x06000996 RID: 2454
		[DllImport("gdiplus")]
		internal static extern int GdipSetImageAttributesOutputChannel(HandleRef imageattr, ColorAdjustType type, bool enableFlag, ColorChannelFlag channelFlags);

		// Token: 0x06000997 RID: 2455
		[DllImport("gdiplus", CharSet = CharSet.Auto)]
		internal static extern int GdipSetImageAttributesOutputChannelColorProfile(HandleRef imageattr, ColorAdjustType type, bool enableFlag, [MarshalAs(UnmanagedType.LPWStr)] string profileName);

		// Token: 0x06000998 RID: 2456
		[DllImport("gdiplus")]
		internal static extern int GdipSetImageAttributesRemapTable(HandleRef imageattr, ColorAdjustType type, bool enableFlag, int mapSize, HandleRef colorMap);

		// Token: 0x06000999 RID: 2457
		[DllImport("gdiplus")]
		internal static extern int GdipSetImageAttributesThreshold(HandleRef imageattr, ColorAdjustType type, bool enableFlag, float thresHold);

		// Token: 0x0600099A RID: 2458
		[DllImport("gdiplus")]
		internal static extern int GdipCloneImageAttributes(HandleRef imageattr, out IntPtr cloneImageattr);

		// Token: 0x0600099B RID: 2459
		[DllImport("gdiplus")]
		internal static extern int GdipGetImageAttributesAdjustedPalette(HandleRef imageattr, HandleRef colorPalette, ColorAdjustType colorAdjustType);

		// Token: 0x0600099C RID: 2460
		[DllImport("gdiplus")]
		internal static extern int GdipSetImageAttributesWrapMode(HandleRef imageattr, int wrap, int argb, bool clamp);

		// Token: 0x0600099D RID: 2461
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateFont(IntPtr fontFamily, float emSize, FontStyle style, GraphicsUnit unit, out IntPtr font);

		// Token: 0x0600099E RID: 2462
		[DllImport("gdiplus")]
		internal static extern Status GdipDeleteFont(IntPtr font);

		// Token: 0x0600099F RID: 2463
		[DllImport("gdiplus", CharSet = CharSet.Auto)]
		internal static extern Status GdipGetLogFont(IntPtr font, IntPtr graphics, [MarshalAs(UnmanagedType.AsAny)] [Out] object logfontA);

		// Token: 0x060009A0 RID: 2464
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateFontFromDC(IntPtr hdc, out IntPtr font);

		// Token: 0x060009A1 RID: 2465
		[DllImport("gdiplus", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern Status GdipCreateFontFromLogfont(IntPtr hdc, ref LOGFONT lf, out IntPtr ptr);

		// Token: 0x060009A2 RID: 2466
		[DllImport("gdiplus", CharSet = CharSet.Ansi)]
		internal static extern Status GdipCreateFontFromHfont(IntPtr hdc, out IntPtr font, ref LOGFONT lf);

		// Token: 0x060009A3 RID: 2467
		[DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
		internal static extern IntPtr CreateFontIndirect(ref LOGFONT logfont);

		// Token: 0x060009A4 RID: 2468
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		internal static extern IntPtr GetDC(IntPtr hwnd);

		// Token: 0x060009A5 RID: 2469
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		// Token: 0x060009A6 RID: 2470
		[DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);

		// Token: 0x060009A7 RID: 2471
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool GetIconInfo(IntPtr hIcon, out IconInfo iconinfo);

		// Token: 0x060009A8 RID: 2472
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		internal static extern IntPtr CreateIconIndirect([In] ref IconInfo piconinfo);

		// Token: 0x060009A9 RID: 2473
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		internal static extern bool DestroyIcon(IntPtr hIcon);

		// Token: 0x060009AA RID: 2474
		[DllImport("gdi32.dll")]
		internal static extern bool DeleteObject(IntPtr hObject);

		// Token: 0x060009AB RID: 2475
		[DllImport("user32.dll")]
		internal static extern IntPtr GetDesktopWindow();

		// Token: 0x060009AC RID: 2476
		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

		// Token: 0x060009AD RID: 2477
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetSysColor")]
		public static extern uint Win32GetSysColor(GetSysColorIndex index);

		// Token: 0x060009AE RID: 2478
		[DllImport("libX11")]
		internal static extern IntPtr XOpenDisplay(IntPtr display);

		// Token: 0x060009AF RID: 2479
		[DllImport("libX11")]
		internal static extern int XCloseDisplay(IntPtr display);

		// Token: 0x060009B0 RID: 2480
		[DllImport("libX11")]
		internal static extern IntPtr XRootWindow(IntPtr display, int screen);

		// Token: 0x060009B1 RID: 2481
		[DllImport("libX11")]
		internal static extern int XDefaultScreen(IntPtr display);

		// Token: 0x060009B2 RID: 2482
		[DllImport("libX11")]
		internal static extern uint XDefaultDepth(IntPtr display, int screen);

		// Token: 0x060009B3 RID: 2483
		[DllImport("libX11")]
		internal static extern IntPtr XGetImage(IntPtr display, IntPtr drawable, int src_x, int src_y, int width, int height, int pane, int format);

		// Token: 0x060009B4 RID: 2484
		[DllImport("libX11")]
		internal static extern int XGetPixel(IntPtr image, int x, int y);

		// Token: 0x060009B5 RID: 2485
		[DllImport("libX11")]
		internal static extern int XDestroyImage(IntPtr image);

		// Token: 0x060009B6 RID: 2486
		[DllImport("libX11")]
		internal static extern IntPtr XDefaultVisual(IntPtr display, int screen);

		// Token: 0x060009B7 RID: 2487
		[DllImport("libX11")]
		internal static extern IntPtr XGetVisualInfo(IntPtr display, int vinfo_mask, ref XVisualInfo vinfo_template, ref int nitems);

		// Token: 0x060009B8 RID: 2488
		[DllImport("libX11")]
		internal static extern IntPtr XVisualIDFromVisual(IntPtr visual);

		// Token: 0x060009B9 RID: 2489
		[DllImport("libX11")]
		internal static extern void XFree(IntPtr data);

		// Token: 0x060009BA RID: 2490
		[DllImport("gdiplus")]
		internal static extern int GdipGetFontCollectionFamilyCount(HandleRef collection, out int found);

		// Token: 0x060009BB RID: 2491
		[DllImport("gdiplus")]
		internal static extern int GdipGetFontCollectionFamilyList(HandleRef collection, int getCount, IntPtr[] dest, out int retCount);

		// Token: 0x060009BC RID: 2492
		[DllImport("gdiplus")]
		internal static extern int GdipNewInstalledFontCollection(out IntPtr collection);

		// Token: 0x060009BD RID: 2493
		[DllImport("gdiplus")]
		internal static extern Status GdipNewPrivateFontCollection(out IntPtr collection);

		// Token: 0x060009BE RID: 2494
		[DllImport("gdiplus")]
		internal static extern Status GdipDeletePrivateFontCollection(ref IntPtr collection);

		// Token: 0x060009BF RID: 2495
		[DllImport("gdiplus", CharSet = CharSet.Auto)]
		internal static extern Status GdipPrivateAddFontFile(IntPtr collection, [MarshalAs(UnmanagedType.LPWStr)] string fileName);

		// Token: 0x060009C0 RID: 2496
		[DllImport("gdiplus")]
		internal static extern Status GdipPrivateAddMemoryFont(IntPtr collection, IntPtr mem, int length);

		// Token: 0x060009C1 RID: 2497
		[DllImport("gdiplus", CharSet = CharSet.Auto)]
		internal static extern Status GdipCreateFontFamilyFromName([MarshalAs(UnmanagedType.LPWStr)] string fName, IntPtr collection, out IntPtr fontFamily);

		// Token: 0x060009C2 RID: 2498
		[DllImport("gdiplus", CharSet = CharSet.Unicode)]
		internal static extern Status GdipGetFamilyName(IntPtr family, IntPtr name, int language);

		// Token: 0x060009C3 RID: 2499
		[DllImport("gdiplus")]
		internal static extern Status GdipGetGenericFontFamilySansSerif(out IntPtr fontFamily);

		// Token: 0x060009C4 RID: 2500
		[DllImport("gdiplus")]
		internal static extern Status GdipGetGenericFontFamilySerif(out IntPtr fontFamily);

		// Token: 0x060009C5 RID: 2501
		[DllImport("gdiplus")]
		internal static extern Status GdipGetGenericFontFamilyMonospace(out IntPtr fontFamily);

		// Token: 0x060009C6 RID: 2502
		[DllImport("gdiplus")]
		internal static extern Status GdipGetCellAscent(IntPtr fontFamily, int style, out short ascent);

		// Token: 0x060009C7 RID: 2503
		[DllImport("gdiplus")]
		internal static extern Status GdipGetCellDescent(IntPtr fontFamily, int style, out short descent);

		// Token: 0x060009C8 RID: 2504
		[DllImport("gdiplus")]
		internal static extern Status GdipGetLineSpacing(IntPtr fontFamily, int style, out short spacing);

		// Token: 0x060009C9 RID: 2505
		[DllImport("gdiplus")]
		internal static extern Status GdipGetEmHeight(IntPtr fontFamily, int style, out short emHeight);

		// Token: 0x060009CA RID: 2506
		[DllImport("gdiplus")]
		internal static extern Status GdipIsStyleAvailable(IntPtr fontFamily, int style, out bool styleAvailable);

		// Token: 0x060009CB RID: 2507
		[DllImport("gdiplus")]
		internal static extern Status GdipDeleteFontFamily(IntPtr fontFamily);

		// Token: 0x060009CC RID: 2508
		[DllImport("gdiplus")]
		internal static extern Status GdipGetFontSize(IntPtr font, out float size);

		// Token: 0x060009CD RID: 2509
		[DllImport("gdiplus")]
		internal static extern Status GdipGetFontHeight(IntPtr font, IntPtr graphics, out float height);

		// Token: 0x060009CE RID: 2510
		[DllImport("gdiplus")]
		internal static extern Status GdipGetFontHeightGivenDPI(IntPtr font, float dpi, out float height);

		// Token: 0x060009CF RID: 2511
		[DllImport("gdiplus")]
		internal static extern int GdipCloneFontFamily(HandleRef fontFamily, out IntPtr clone);

		// Token: 0x060009D0 RID: 2512
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateStringFormat(int formatAttributes, int language, out IntPtr format);

		// Token: 0x060009D1 RID: 2513
		[DllImport("gdiplus")]
		internal static extern Status GdipStringFormatGetGenericDefault(out IntPtr format);

		// Token: 0x060009D2 RID: 2514
		[DllImport("gdiplus")]
		internal static extern Status GdipStringFormatGetGenericTypographic(out IntPtr format);

		// Token: 0x060009D3 RID: 2515
		[DllImport("gdiplus")]
		internal static extern Status GdipDeleteStringFormat(IntPtr format);

		// Token: 0x060009D4 RID: 2516
		[DllImport("gdiplus")]
		internal static extern Status GdipCloneStringFormat(IntPtr srcformat, out IntPtr format);

		// Token: 0x060009D5 RID: 2517
		[DllImport("gdiplus")]
		internal static extern Status GdipSetStringFormatFlags(IntPtr format, StringFormatFlags flags);

		// Token: 0x060009D6 RID: 2518
		[DllImport("gdiplus")]
		internal static extern Status GdipGetStringFormatFlags(IntPtr format, out StringFormatFlags flags);

		// Token: 0x060009D7 RID: 2519
		[DllImport("gdiplus")]
		internal static extern Status GdipSetStringFormatAlign(IntPtr format, StringAlignment align);

		// Token: 0x060009D8 RID: 2520
		[DllImport("gdiplus")]
		internal static extern Status GdipGetStringFormatAlign(IntPtr format, out StringAlignment align);

		// Token: 0x060009D9 RID: 2521
		[DllImport("gdiplus")]
		internal static extern Status GdipSetStringFormatLineAlign(IntPtr format, StringAlignment align);

		// Token: 0x060009DA RID: 2522
		[DllImport("gdiplus")]
		internal static extern Status GdipGetStringFormatLineAlign(IntPtr format, out StringAlignment align);

		// Token: 0x060009DB RID: 2523
		[DllImport("gdiplus")]
		internal static extern Status GdipSetStringFormatTrimming(IntPtr format, StringTrimming trimming);

		// Token: 0x060009DC RID: 2524
		[DllImport("gdiplus")]
		internal static extern Status GdipGetStringFormatTrimming(IntPtr format, out StringTrimming trimming);

		// Token: 0x060009DD RID: 2525
		[DllImport("gdiplus")]
		internal static extern Status GdipSetStringFormatHotkeyPrefix(IntPtr format, HotkeyPrefix hotkeyPrefix);

		// Token: 0x060009DE RID: 2526
		[DllImport("gdiplus")]
		internal static extern Status GdipGetStringFormatHotkeyPrefix(IntPtr format, out HotkeyPrefix hotkeyPrefix);

		// Token: 0x060009DF RID: 2527
		[DllImport("gdiplus")]
		internal static extern Status GdipSetStringFormatTabStops(IntPtr format, float firstTabOffset, int count, float[] tabStops);

		// Token: 0x060009E0 RID: 2528
		[DllImport("gdiplus")]
		internal static extern Status GdipGetStringFormatDigitSubstitution(IntPtr format, int language, out StringDigitSubstitute substitute);

		// Token: 0x060009E1 RID: 2529
		[DllImport("gdiplus")]
		internal static extern Status GdipSetStringFormatDigitSubstitution(IntPtr format, int language, StringDigitSubstitute substitute);

		// Token: 0x060009E2 RID: 2530
		[DllImport("gdiplus")]
		internal static extern Status GdipGetStringFormatTabStopCount(IntPtr format, out int count);

		// Token: 0x060009E3 RID: 2531
		[DllImport("gdiplus")]
		internal static extern Status GdipGetStringFormatTabStops(IntPtr format, int count, out float firstTabOffset, [In] [Out] float[] tabStops);

		// Token: 0x060009E4 RID: 2532
		[DllImport("gdiplus", CharSet = CharSet.Auto)]
		internal static extern Status GdipCreateMetafileFromFile([MarshalAs(UnmanagedType.LPWStr)] string filename, out IntPtr metafile);

		// Token: 0x060009E5 RID: 2533
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateMetafileFromEmf(IntPtr hEmf, bool deleteEmf, out IntPtr metafile);

		// Token: 0x060009E6 RID: 2534
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateMetafileFromWmf(IntPtr hWmf, bool deleteWmf, WmfPlaceableFileHeader wmfPlaceableFileHeader, out IntPtr metafile);

		// Token: 0x060009E7 RID: 2535
		[DllImport("gdiplus", CharSet = CharSet.Auto)]
		internal static extern Status GdipGetMetafileHeaderFromFile([MarshalAs(UnmanagedType.LPWStr)] string filename, IntPtr header);

		// Token: 0x060009E8 RID: 2536
		[DllImport("gdiplus")]
		internal static extern Status GdipGetMetafileHeaderFromMetafile(IntPtr metafile, IntPtr header);

		// Token: 0x060009E9 RID: 2537
		[DllImport("gdiplus")]
		internal static extern Status GdipGetMetafileHeaderFromEmf(IntPtr hEmf, IntPtr header);

		// Token: 0x060009EA RID: 2538
		[DllImport("gdiplus")]
		internal static extern Status GdipGetMetafileHeaderFromWmf(IntPtr hWmf, IntPtr wmfPlaceableFileHeader, IntPtr header);

		// Token: 0x060009EB RID: 2539
		[DllImport("gdiplus")]
		internal static extern Status GdipGetHemfFromMetafile(IntPtr metafile, out IntPtr hEmf);

		// Token: 0x060009EC RID: 2540
		[DllImport("gdiplus")]
		internal static extern Status GdipGetMetafileDownLevelRasterizationLimit(IntPtr metafile, ref uint metafileRasterizationLimitDpi);

		// Token: 0x060009ED RID: 2541
		[DllImport("gdiplus")]
		internal static extern Status GdipSetMetafileDownLevelRasterizationLimit(IntPtr metafile, uint metafileRasterizationLimitDpi);

		// Token: 0x060009EE RID: 2542
		[DllImport("gdiplus")]
		internal static extern Status GdipPlayMetafileRecord(IntPtr metafile, EmfPlusRecordType recordType, int flags, int dataSize, byte[] data);

		// Token: 0x060009EF RID: 2543
		[DllImport("gdiplus")]
		internal static extern Status GdipRecordMetafile(IntPtr hdc, EmfType type, ref RectangleF frameRect, MetafileFrameUnit frameUnit, [MarshalAs(UnmanagedType.LPWStr)] string description, out IntPtr metafile);

		// Token: 0x060009F0 RID: 2544
		[DllImport("gdiplus")]
		internal static extern Status GdipRecordMetafileI(IntPtr hdc, EmfType type, ref Rectangle frameRect, MetafileFrameUnit frameUnit, [MarshalAs(UnmanagedType.LPWStr)] string description, out IntPtr metafile);

		// Token: 0x060009F1 RID: 2545
		[DllImport("gdiplus")]
		internal static extern Status GdipRecordMetafileFileName([MarshalAs(UnmanagedType.LPWStr)] string filename, IntPtr hdc, EmfType type, ref RectangleF frameRect, MetafileFrameUnit frameUnit, [MarshalAs(UnmanagedType.LPWStr)] string description, out IntPtr metafile);

		// Token: 0x060009F2 RID: 2546
		[DllImport("gdiplus")]
		internal static extern Status GdipRecordMetafileFileNameI([MarshalAs(UnmanagedType.LPWStr)] string filename, IntPtr hdc, EmfType type, ref Rectangle frameRect, MetafileFrameUnit frameUnit, [MarshalAs(UnmanagedType.LPWStr)] string description, out IntPtr metafile);

		// Token: 0x060009F3 RID: 2547
		[DllImport("gdiplus", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern Status GdipCreateMetafileFromStream([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Drawing.ComIStreamMarshaler)] IStream stream, out IntPtr metafile);

		// Token: 0x060009F4 RID: 2548
		[DllImport("gdiplus", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern Status GdipGetMetafileHeaderFromStream([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Drawing.ComIStreamMarshaler)] IStream stream, IntPtr header);

		// Token: 0x060009F5 RID: 2549
		[DllImport("gdiplus")]
		internal static extern Status GdipRecordMetafileStream([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Drawing.ComIStreamMarshaler)] IStream stream, IntPtr hdc, EmfType type, ref RectangleF frameRect, MetafileFrameUnit frameUnit, [MarshalAs(UnmanagedType.LPWStr)] string description, out IntPtr metafile);

		// Token: 0x060009F6 RID: 2550
		[DllImport("gdiplus")]
		internal static extern Status GdipRecordMetafileStreamI([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Drawing.ComIStreamMarshaler)] IStream stream, IntPtr hdc, EmfType type, ref Rectangle frameRect, MetafileFrameUnit frameUnit, [MarshalAs(UnmanagedType.LPWStr)] string description, out IntPtr metafile);

		// Token: 0x060009F7 RID: 2551
		[DllImport("gdiplus")]
		internal static extern int GdipGetImageDecodersSize(out int decoderNums, out int arraySize);

		// Token: 0x060009F8 RID: 2552
		[DllImport("gdiplus")]
		internal static extern int GdipGetImageDecoders(int decoderNums, int arraySize, IntPtr decoders);

		// Token: 0x060009F9 RID: 2553
		[DllImport("gdiplus")]
		internal static extern int GdipGetImageEncodersSize(out int encoderNums, out int arraySize);

		// Token: 0x060009FA RID: 2554
		[DllImport("gdiplus")]
		internal static extern int GdipGetImageEncoders(int encoderNums, int arraySize, IntPtr encoders);

		// Token: 0x060009FB RID: 2555
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateFromContext_macosx(IntPtr cgref, int width, int height, out IntPtr graphics);

		// Token: 0x060009FC RID: 2556
		[DllImport("gdiplus")]
		internal static extern Status GdipSetVisibleClip_linux(IntPtr graphics, ref Rectangle rect);

		// Token: 0x060009FD RID: 2557
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateFromXDrawable_linux(IntPtr drawable, IntPtr display, out IntPtr graphics);

		// Token: 0x060009FE RID: 2558
		[DllImport("gdiplus")]
		internal static extern Status GdipLoadImageFromDelegate_linux(GDIPlus.StreamGetHeaderDelegate getHeader, GDIPlus.StreamGetBytesDelegate getBytes, GDIPlus.StreamPutBytesDelegate putBytes, GDIPlus.StreamSeekDelegate doSeek, GDIPlus.StreamCloseDelegate close, GDIPlus.StreamSizeDelegate size, out IntPtr image);

		// Token: 0x060009FF RID: 2559
		[DllImport("gdiplus")]
		internal static extern Status GdipSaveImageToDelegate_linux(IntPtr image, GDIPlus.StreamGetBytesDelegate getBytes, GDIPlus.StreamPutBytesDelegate putBytes, GDIPlus.StreamSeekDelegate doSeek, GDIPlus.StreamCloseDelegate close, GDIPlus.StreamSizeDelegate size, ref Guid encoderClsID, IntPtr encoderParameters);

		// Token: 0x06000A00 RID: 2560
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateMetafileFromDelegate_linux(GDIPlus.StreamGetHeaderDelegate getHeader, GDIPlus.StreamGetBytesDelegate getBytes, GDIPlus.StreamPutBytesDelegate putBytes, GDIPlus.StreamSeekDelegate doSeek, GDIPlus.StreamCloseDelegate close, GDIPlus.StreamSizeDelegate size, out IntPtr metafile);

		// Token: 0x06000A01 RID: 2561
		[DllImport("gdiplus")]
		internal static extern Status GdipGetMetafileHeaderFromDelegate_linux(GDIPlus.StreamGetHeaderDelegate getHeader, GDIPlus.StreamGetBytesDelegate getBytes, GDIPlus.StreamPutBytesDelegate putBytes, GDIPlus.StreamSeekDelegate doSeek, GDIPlus.StreamCloseDelegate close, GDIPlus.StreamSizeDelegate size, IntPtr header);

		// Token: 0x06000A02 RID: 2562
		[DllImport("gdiplus")]
		internal static extern Status GdipRecordMetafileFromDelegate_linux(GDIPlus.StreamGetHeaderDelegate getHeader, GDIPlus.StreamGetBytesDelegate getBytes, GDIPlus.StreamPutBytesDelegate putBytes, GDIPlus.StreamSeekDelegate doSeek, GDIPlus.StreamCloseDelegate close, GDIPlus.StreamSizeDelegate size, IntPtr hdc, EmfType type, ref RectangleF frameRect, MetafileFrameUnit frameUnit, [MarshalAs(UnmanagedType.LPWStr)] string description, out IntPtr metafile);

		// Token: 0x06000A03 RID: 2563
		[DllImport("gdiplus")]
		internal static extern Status GdipRecordMetafileFromDelegateI_linux(GDIPlus.StreamGetHeaderDelegate getHeader, GDIPlus.StreamGetBytesDelegate getBytes, GDIPlus.StreamPutBytesDelegate putBytes, GDIPlus.StreamSeekDelegate doSeek, GDIPlus.StreamCloseDelegate close, GDIPlus.StreamSizeDelegate size, IntPtr hdc, EmfType type, ref Rectangle frameRect, MetafileFrameUnit frameUnit, [MarshalAs(UnmanagedType.LPWStr)] string description, out IntPtr metafile);

		// Token: 0x06000A04 RID: 2564
		[DllImport("libc")]
		private static extern int uname(IntPtr buf);

		// Token: 0x06000A05 RID: 2565 RVA: 0x00002050 File Offset: 0x00000250
		public GDIPlus()
		{
		}

		// Token: 0x040005DC RID: 1500
		public const int FACESIZE = 32;

		// Token: 0x040005DD RID: 1501
		public const int LANG_NEUTRAL = 0;

		// Token: 0x040005DE RID: 1502
		public static IntPtr Display = IntPtr.Zero;

		// Token: 0x040005DF RID: 1503
		public static bool UseX11Drawable = false;

		// Token: 0x040005E0 RID: 1504
		public static bool UseCarbonDrawable = false;

		// Token: 0x040005E1 RID: 1505
		public static bool UseCocoaDrawable = false;

		// Token: 0x040005E2 RID: 1506
		private const string GdiPlus = "gdiplus";

		// Token: 0x040005E3 RID: 1507
		internal static ulong GdiPlusToken = 0UL;

		// Token: 0x02000094 RID: 148
		// (Invoke) Token: 0x06000A07 RID: 2567
		public delegate int StreamGetHeaderDelegate(IntPtr buf, int bufsz);

		// Token: 0x02000095 RID: 149
		// (Invoke) Token: 0x06000A0B RID: 2571
		public delegate int StreamGetBytesDelegate(IntPtr buf, int bufsz, bool peek);

		// Token: 0x02000096 RID: 150
		// (Invoke) Token: 0x06000A0F RID: 2575
		public delegate long StreamSeekDelegate(int offset, int whence);

		// Token: 0x02000097 RID: 151
		// (Invoke) Token: 0x06000A13 RID: 2579
		public delegate int StreamPutBytesDelegate(IntPtr buf, int bufsz);

		// Token: 0x02000098 RID: 152
		// (Invoke) Token: 0x06000A17 RID: 2583
		public delegate void StreamCloseDelegate();

		// Token: 0x02000099 RID: 153
		// (Invoke) Token: 0x06000A1B RID: 2587
		public delegate long StreamSizeDelegate();

		// Token: 0x0200009A RID: 154
		internal sealed class GdiPlusStreamHelper
		{
			// Token: 0x06000A1E RID: 2590 RVA: 0x00016E50 File Offset: 0x00015050
			public GdiPlusStreamHelper(Stream s, bool seekToOrigin)
			{
				this.managedBuf = new byte[4096];
				this.stream = s;
				if (this.stream != null && this.stream.CanSeek && seekToOrigin)
				{
					this.stream.Seek(0L, SeekOrigin.Begin);
				}
			}

			// Token: 0x06000A1F RID: 2591 RVA: 0x00016EA4 File Offset: 0x000150A4
			public int StreamGetHeaderImpl(IntPtr buf, int bufsz)
			{
				this.start_buf = new byte[bufsz];
				int num;
				try
				{
					num = this.stream.Read(this.start_buf, 0, bufsz);
				}
				catch (IOException)
				{
					return -1;
				}
				if (num > 0 && buf != IntPtr.Zero)
				{
					Marshal.Copy(this.start_buf, 0, (IntPtr)buf.ToInt64(), num);
				}
				this.start_buf_pos = 0;
				this.start_buf_len = num;
				return num;
			}

			// Token: 0x170002C6 RID: 710
			// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00016F24 File Offset: 0x00015124
			public GDIPlus.StreamGetHeaderDelegate GetHeaderDelegate
			{
				get
				{
					if (this.stream != null && this.stream.CanRead)
					{
						if (this.sghd == null)
						{
							this.sghd = new GDIPlus.StreamGetHeaderDelegate(this.StreamGetHeaderImpl);
						}
						return this.sghd;
					}
					return null;
				}
			}

			// Token: 0x06000A21 RID: 2593 RVA: 0x00016F60 File Offset: 0x00015160
			public int StreamGetBytesImpl(IntPtr buf, int bufsz, bool peek)
			{
				if (buf == IntPtr.Zero && peek)
				{
					return -1;
				}
				if (bufsz > this.managedBuf.Length)
				{
					this.managedBuf = new byte[bufsz];
				}
				int num = 0;
				long offset = 0L;
				if (bufsz > 0)
				{
					if (this.stream.CanSeek)
					{
						offset = this.stream.Position;
					}
					if (this.start_buf_len > 0)
					{
						if (this.start_buf_len > bufsz)
						{
							Array.Copy(this.start_buf, this.start_buf_pos, this.managedBuf, 0, bufsz);
							this.start_buf_pos += bufsz;
							this.start_buf_len -= bufsz;
							num = bufsz;
							bufsz = 0;
						}
						else
						{
							Array.Copy(this.start_buf, this.start_buf_pos, this.managedBuf, 0, this.start_buf_len);
							bufsz -= this.start_buf_len;
							num = this.start_buf_len;
							this.start_buf_len = 0;
						}
					}
					if (bufsz > 0)
					{
						try
						{
							num += this.stream.Read(this.managedBuf, num, bufsz);
						}
						catch (IOException)
						{
							return -1;
						}
					}
					if (num > 0 && buf != IntPtr.Zero)
					{
						Marshal.Copy(this.managedBuf, 0, (IntPtr)buf.ToInt64(), num);
					}
					bool flag = !this.stream.CanSeek && bufsz == 10 && peek;
					if (peek)
					{
						if (!this.stream.CanSeek)
						{
							throw new NotSupportedException();
						}
						this.stream.Seek(offset, SeekOrigin.Begin);
					}
				}
				return num;
			}

			// Token: 0x170002C7 RID: 711
			// (get) Token: 0x06000A22 RID: 2594 RVA: 0x000170DC File Offset: 0x000152DC
			public GDIPlus.StreamGetBytesDelegate GetBytesDelegate
			{
				get
				{
					if (this.stream != null && this.stream.CanRead)
					{
						if (this.sgbd == null)
						{
							this.sgbd = new GDIPlus.StreamGetBytesDelegate(this.StreamGetBytesImpl);
						}
						return this.sgbd;
					}
					return null;
				}
			}

			// Token: 0x06000A23 RID: 2595 RVA: 0x00017118 File Offset: 0x00015318
			public long StreamSeekImpl(int offset, int whence)
			{
				if (whence < 0 || whence > 2)
				{
					return -1L;
				}
				this.start_buf_pos += this.start_buf_len;
				this.start_buf_len = 0;
				SeekOrigin origin;
				switch (whence)
				{
				case 0:
					origin = SeekOrigin.Begin;
					break;
				case 1:
					origin = SeekOrigin.Current;
					break;
				case 2:
					origin = SeekOrigin.End;
					break;
				default:
					return -1L;
				}
				return this.stream.Seek((long)offset, origin);
			}

			// Token: 0x170002C8 RID: 712
			// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0001717B File Offset: 0x0001537B
			public GDIPlus.StreamSeekDelegate SeekDelegate
			{
				get
				{
					if (this.stream != null && this.stream.CanSeek)
					{
						if (this.skd == null)
						{
							this.skd = new GDIPlus.StreamSeekDelegate(this.StreamSeekImpl);
						}
						return this.skd;
					}
					return null;
				}
			}

			// Token: 0x06000A25 RID: 2597 RVA: 0x000171B4 File Offset: 0x000153B4
			public int StreamPutBytesImpl(IntPtr buf, int bufsz)
			{
				if (bufsz > this.managedBuf.Length)
				{
					this.managedBuf = new byte[bufsz];
				}
				Marshal.Copy(buf, this.managedBuf, 0, bufsz);
				this.stream.Write(this.managedBuf, 0, bufsz);
				return bufsz;
			}

			// Token: 0x170002C9 RID: 713
			// (get) Token: 0x06000A26 RID: 2598 RVA: 0x000171EF File Offset: 0x000153EF
			public GDIPlus.StreamPutBytesDelegate PutBytesDelegate
			{
				get
				{
					if (this.stream != null && this.stream.CanWrite)
					{
						if (this.spbd == null)
						{
							this.spbd = new GDIPlus.StreamPutBytesDelegate(this.StreamPutBytesImpl);
						}
						return this.spbd;
					}
					return null;
				}
			}

			// Token: 0x06000A27 RID: 2599 RVA: 0x00017228 File Offset: 0x00015428
			public void StreamCloseImpl()
			{
				this.stream.Dispose();
			}

			// Token: 0x170002CA RID: 714
			// (get) Token: 0x06000A28 RID: 2600 RVA: 0x00017235 File Offset: 0x00015435
			public GDIPlus.StreamCloseDelegate CloseDelegate
			{
				get
				{
					if (this.stream != null)
					{
						if (this.scd == null)
						{
							this.scd = new GDIPlus.StreamCloseDelegate(this.StreamCloseImpl);
						}
						return this.scd;
					}
					return null;
				}
			}

			// Token: 0x06000A29 RID: 2601 RVA: 0x00017264 File Offset: 0x00015464
			public long StreamSizeImpl()
			{
				long result;
				try
				{
					result = this.stream.Length;
				}
				catch
				{
					result = -1L;
				}
				return result;
			}

			// Token: 0x170002CB RID: 715
			// (get) Token: 0x06000A2A RID: 2602 RVA: 0x00017298 File Offset: 0x00015498
			public GDIPlus.StreamSizeDelegate SizeDelegate
			{
				get
				{
					if (this.stream != null)
					{
						if (this.ssd == null)
						{
							this.ssd = new GDIPlus.StreamSizeDelegate(this.StreamSizeImpl);
						}
						return this.ssd;
					}
					return null;
				}
			}

			// Token: 0x040005E4 RID: 1508
			public Stream stream;

			// Token: 0x040005E5 RID: 1509
			private GDIPlus.StreamGetHeaderDelegate sghd;

			// Token: 0x040005E6 RID: 1510
			private GDIPlus.StreamGetBytesDelegate sgbd;

			// Token: 0x040005E7 RID: 1511
			private GDIPlus.StreamSeekDelegate skd;

			// Token: 0x040005E8 RID: 1512
			private GDIPlus.StreamPutBytesDelegate spbd;

			// Token: 0x040005E9 RID: 1513
			private GDIPlus.StreamCloseDelegate scd;

			// Token: 0x040005EA RID: 1514
			private GDIPlus.StreamSizeDelegate ssd;

			// Token: 0x040005EB RID: 1515
			private byte[] start_buf;

			// Token: 0x040005EC RID: 1516
			private int start_buf_pos;

			// Token: 0x040005ED RID: 1517
			private int start_buf_len;

			// Token: 0x040005EE RID: 1518
			private byte[] managedBuf;

			// Token: 0x040005EF RID: 1519
			private const int default_bufsize = 4096;
		}
	}
}
