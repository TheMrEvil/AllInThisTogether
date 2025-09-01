using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing.Internal;
using System.Internal;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Drawing
{
	// Token: 0x0200001F RID: 31
	internal class SafeNativeMethods
	{
		// Token: 0x0600007F RID: 127
		[DllImport("gdi32", CharSet = CharSet.Auto, EntryPoint = "CreateCompatibleBitmap", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr IntCreateCompatibleBitmap(HandleRef hDC, int width, int height);

		// Token: 0x06000080 RID: 128 RVA: 0x000046FC File Offset: 0x000028FC
		public static IntPtr CreateCompatibleBitmap(HandleRef hDC, int width, int height)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateCompatibleBitmap(hDC, width, height), SafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06000081 RID: 129
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int BitBlt(HandleRef hDC, int x, int y, int nWidth, int nHeight, HandleRef hSrcDC, int xSrc, int ySrc, int dwRop);

		// Token: 0x06000082 RID: 130
		[DllImport("gdi32")]
		public static extern int GetDIBits(HandleRef hdc, HandleRef hbm, int arg1, int arg2, IntPtr arg3, ref NativeMethods.BITMAPINFO_FLAT bmi, int arg5);

		// Token: 0x06000083 RID: 131
		[DllImport("gdi32")]
		public static extern uint GetPaletteEntries(HandleRef hpal, int iStartIndex, int nEntries, byte[] lppe);

		// Token: 0x06000084 RID: 132
		[DllImport("gdi32", CharSet = CharSet.Auto, EntryPoint = "CreateDIBSection", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr IntCreateDIBSection(HandleRef hdc, ref NativeMethods.BITMAPINFO_FLAT bmi, int iUsage, ref IntPtr ppvBits, IntPtr hSection, int dwOffset);

		// Token: 0x06000085 RID: 133 RVA: 0x00004710 File Offset: 0x00002910
		public static IntPtr CreateDIBSection(HandleRef hdc, ref NativeMethods.BITMAPINFO_FLAT bmi, int iUsage, ref IntPtr ppvBits, IntPtr hSection, int dwOffset)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateDIBSection(hdc, ref bmi, iUsage, ref ppvBits, hSection, dwOffset), SafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06000086 RID: 134
		[DllImport("kernel32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GlobalFree(HandleRef handle);

		// Token: 0x06000087 RID: 135
		[DllImport("gdi32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int StartDoc(HandleRef hDC, SafeNativeMethods.DOCINFO lpDocInfo);

		// Token: 0x06000088 RID: 136
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int StartPage(HandleRef hDC);

		// Token: 0x06000089 RID: 137
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int EndPage(HandleRef hDC);

		// Token: 0x0600008A RID: 138
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int AbortDoc(HandleRef hDC);

		// Token: 0x0600008B RID: 139
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int EndDoc(HandleRef hDC);

		// Token: 0x0600008C RID: 140
		[DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool PrintDlg([In] [Out] SafeNativeMethods.PRINTDLG lppd);

		// Token: 0x0600008D RID: 141
		[DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool PrintDlg([In] [Out] SafeNativeMethods.PRINTDLGX86 lppd);

		// Token: 0x0600008E RID: 142
		[DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int DeviceCapabilities(string pDevice, string pPort, short fwCapabilities, IntPtr pOutput, IntPtr pDevMode);

		// Token: 0x0600008F RID: 143
		[DllImport("winspool.drv", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int DocumentProperties(HandleRef hwnd, HandleRef hPrinter, string pDeviceName, IntPtr pDevModeOutput, HandleRef pDevModeInput, int fMode);

		// Token: 0x06000090 RID: 144
		[DllImport("winspool.drv", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int DocumentProperties(HandleRef hwnd, HandleRef hPrinter, string pDeviceName, IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);

		// Token: 0x06000091 RID: 145
		[DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int EnumPrinters(int flags, string name, int level, IntPtr pPrinterEnum, int cbBuf, out int pcbNeeded, out int pcReturned);

		// Token: 0x06000092 RID: 146
		[DllImport("kernel32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GlobalLock(HandleRef handle);

		// Token: 0x06000093 RID: 147
		[DllImport("gdi32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr ResetDC(HandleRef hDC, HandleRef lpDevMode);

		// Token: 0x06000094 RID: 148
		[DllImport("kernel32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool GlobalUnlock(HandleRef handle);

		// Token: 0x06000095 RID: 149
		[DllImport("gdi32", CharSet = CharSet.Auto, EntryPoint = "CreateRectRgn", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreateRectRgn(int x1, int y1, int x2, int y2);

		// Token: 0x06000096 RID: 150 RVA: 0x00004729 File Offset: 0x00002929
		public static IntPtr CreateRectRgn(int x1, int y1, int x2, int y2)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateRectRgn(x1, y1, x2, y2), SafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06000097 RID: 151
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetClipRgn(HandleRef hDC, HandleRef hRgn);

		// Token: 0x06000098 RID: 152
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int SelectClipRgn(HandleRef hDC, HandleRef hRgn);

		// Token: 0x06000099 RID: 153
		[DllImport("gdi32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int AddFontResourceEx(string lpszFilename, int fl, IntPtr pdv);

		// Token: 0x0600009A RID: 154 RVA: 0x0000473E File Offset: 0x0000293E
		public static int AddFontFile(string fileName)
		{
			return SafeNativeMethods.AddFontResourceEx(fileName, 16, IntPtr.Zero);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004750 File Offset: 0x00002950
		internal static IntPtr SaveClipRgn(IntPtr hDC)
		{
			IntPtr intPtr = SafeNativeMethods.CreateRectRgn(0, 0, 0, 0);
			IntPtr result = IntPtr.Zero;
			try
			{
				if (SafeNativeMethods.GetClipRgn(new HandleRef(null, hDC), new HandleRef(null, intPtr)) > 0)
				{
					result = intPtr;
					intPtr = IntPtr.Zero;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
				}
			}
			return result;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000047BC File Offset: 0x000029BC
		internal static void RestoreClipRgn(IntPtr hDC, IntPtr hRgn)
		{
			try
			{
				SafeNativeMethods.SelectClipRgn(new HandleRef(null, hDC), new HandleRef(null, hRgn));
			}
			finally
			{
				if (hRgn != IntPtr.Zero)
				{
					SafeNativeMethods.DeleteObject(new HandleRef(null, hRgn));
				}
			}
		}

		// Token: 0x0600009D RID: 157
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int ExtEscape(HandleRef hDC, int nEscape, int cbInput, ref int inData, int cbOutput, out int outData);

		// Token: 0x0600009E RID: 158
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int ExtEscape(HandleRef hDC, int nEscape, int cbInput, byte[] inData, int cbOutput, out int outData);

		// Token: 0x0600009F RID: 159
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int IntersectClipRect(HandleRef hDC, int x1, int y1, int x2, int y2);

		// Token: 0x060000A0 RID: 160
		[DllImport("kernel32", CharSet = CharSet.Auto, EntryPoint = "GlobalAlloc", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr IntGlobalAlloc(int uFlags, UIntPtr dwBytes);

		// Token: 0x060000A1 RID: 161 RVA: 0x0000480C File Offset: 0x00002A0C
		public static IntPtr GlobalAlloc(int uFlags, uint dwBytes)
		{
			return SafeNativeMethods.IntGlobalAlloc(uFlags, new UIntPtr(dwBytes));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000481C File Offset: 0x00002A1C
		internal unsafe static void ZeroMemory(byte* ptr, ulong length)
		{
			byte* ptr2 = ptr + length;
			while (ptr != ptr2)
			{
				*(ptr++) = 0;
			}
		}

		// Token: 0x060000A3 RID: 163
		[DllImport("gdi32", CharSet = CharSet.Auto, EntryPoint = "DeleteObject", ExactSpelling = true, SetLastError = true)]
		internal static extern int IntDeleteObject(HandleRef hObject);

		// Token: 0x060000A4 RID: 164 RVA: 0x0000483C File Offset: 0x00002A3C
		public static int DeleteObject(HandleRef hObject)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hObject, SafeNativeMethods.CommonHandles.GDI);
			return SafeNativeMethods.IntDeleteObject(hObject);
		}

		// Token: 0x060000A5 RID: 165
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr SelectObject(HandleRef hdc, HandleRef obj);

		// Token: 0x060000A6 RID: 166
		[DllImport("user32", EntryPoint = "CreateIconFromResourceEx", SetLastError = true)]
		private unsafe static extern IntPtr IntCreateIconFromResourceEx(byte* pbIconBits, int cbIconBits, bool fIcon, int dwVersion, int csDesired, int cyDesired, int flags);

		// Token: 0x060000A7 RID: 167 RVA: 0x00004855 File Offset: 0x00002A55
		public unsafe static IntPtr CreateIconFromResourceEx(byte* pbIconBits, int cbIconBits, bool fIcon, int dwVersion, int csDesired, int cyDesired, int flags)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateIconFromResourceEx(pbIconBits, cbIconBits, fIcon, dwVersion, csDesired, cyDesired, flags), SafeNativeMethods.CommonHandles.Icon);
		}

		// Token: 0x060000A8 RID: 168
		[DllImport("shell32.dll", BestFitMapping = false, CharSet = CharSet.Auto, EntryPoint = "ExtractAssociatedIcon")]
		public static extern IntPtr IntExtractAssociatedIcon(HandleRef hInst, StringBuilder iconPath, ref int index);

		// Token: 0x060000A9 RID: 169 RVA: 0x00004870 File Offset: 0x00002A70
		public static IntPtr ExtractAssociatedIcon(HandleRef hInst, StringBuilder iconPath, ref int index)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntExtractAssociatedIcon(hInst, iconPath, ref index), SafeNativeMethods.CommonHandles.Icon);
		}

		// Token: 0x060000AA RID: 170
		[DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "LoadIcon", SetLastError = true)]
		private static extern IntPtr IntLoadIcon(HandleRef hInst, IntPtr iconId);

		// Token: 0x060000AB RID: 171 RVA: 0x00004884 File Offset: 0x00002A84
		public static IntPtr LoadIcon(HandleRef hInst, int iconId)
		{
			return SafeNativeMethods.IntLoadIcon(hInst, new IntPtr(iconId));
		}

		// Token: 0x060000AC RID: 172
		[DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "DestroyIcon", ExactSpelling = true, SetLastError = true)]
		private static extern bool IntDestroyIcon(HandleRef hIcon);

		// Token: 0x060000AD RID: 173 RVA: 0x00004892 File Offset: 0x00002A92
		public static bool DestroyIcon(HandleRef hIcon)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hIcon, SafeNativeMethods.CommonHandles.Icon);
			return SafeNativeMethods.IntDestroyIcon(hIcon);
		}

		// Token: 0x060000AE RID: 174
		[DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "CopyImage", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCopyImage(HandleRef hImage, int uType, int cxDesired, int cyDesired, int fuFlags);

		// Token: 0x060000AF RID: 175 RVA: 0x000048AC File Offset: 0x00002AAC
		public static IntPtr CopyImage(HandleRef hImage, int uType, int cxDesired, int cyDesired, int fuFlags)
		{
			int type;
			if (uType == 1)
			{
				type = SafeNativeMethods.CommonHandles.Icon;
			}
			else
			{
				type = SafeNativeMethods.CommonHandles.GDI;
			}
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCopyImage(hImage, uType, cxDesired, cyDesired, fuFlags), type);
		}

		// Token: 0x060000B0 RID: 176
		[DllImport("gdi32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetObject(HandleRef hObject, int nSize, [In] [Out] SafeNativeMethods.BITMAP bm);

		// Token: 0x060000B1 RID: 177
		[DllImport("gdi32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetObject(HandleRef hObject, int nSize, [In] [Out] SafeNativeMethods.LOGFONT lf);

		// Token: 0x060000B2 RID: 178 RVA: 0x000048DC File Offset: 0x00002ADC
		public static int GetObject(HandleRef hObject, SafeNativeMethods.LOGFONT lp)
		{
			return SafeNativeMethods.GetObject(hObject, Marshal.SizeOf(typeof(SafeNativeMethods.LOGFONT)), lp);
		}

		// Token: 0x060000B3 RID: 179
		[DllImport("user32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool GetIconInfo(HandleRef hIcon, [In] [Out] SafeNativeMethods.ICONINFO info);

		// Token: 0x060000B4 RID: 180
		[DllImport("user32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool DrawIconEx(HandleRef hDC, int x, int y, HandleRef hIcon, int width, int height, int iStepIfAniCursor, HandleRef hBrushFlickerFree, int diFlags);

		// Token: 0x060000B5 RID: 181
		[DllImport("oleaut32.dll", PreserveSig = false)]
		public static extern SafeNativeMethods.IPicture OleCreatePictureIndirect(SafeNativeMethods.PICTDESC pictdesc, [In] ref Guid refiid, bool fOwn);

		// Token: 0x060000B6 RID: 182 RVA: 0x00002050 File Offset: 0x00000250
		public SafeNativeMethods()
		{
		}

		// Token: 0x04000161 RID: 353
		public const int ERROR_CANCELLED = 1223;

		// Token: 0x04000162 RID: 354
		public const int E_UNEXPECTED = -2147418113;

		// Token: 0x04000163 RID: 355
		public const int E_NOTIMPL = -2147467263;

		// Token: 0x04000164 RID: 356
		public const int E_ABORT = -2147467260;

		// Token: 0x04000165 RID: 357
		public const int E_FAIL = -2147467259;

		// Token: 0x04000166 RID: 358
		public const int E_ACCESSDENIED = -2147024891;

		// Token: 0x04000167 RID: 359
		public const int GMEM_MOVEABLE = 2;

		// Token: 0x04000168 RID: 360
		public const int GMEM_ZEROINIT = 64;

		// Token: 0x04000169 RID: 361
		public const int DM_IN_BUFFER = 8;

		// Token: 0x0400016A RID: 362
		public const int DM_OUT_BUFFER = 2;

		// Token: 0x0400016B RID: 363
		public const int DT_PLOTTER = 0;

		// Token: 0x0400016C RID: 364
		public const int DT_RASPRINTER = 2;

		// Token: 0x0400016D RID: 365
		public const int TECHNOLOGY = 2;

		// Token: 0x0400016E RID: 366
		public const int DC_PAPERS = 2;

		// Token: 0x0400016F RID: 367
		public const int DC_PAPERSIZE = 3;

		// Token: 0x04000170 RID: 368
		public const int DC_BINS = 6;

		// Token: 0x04000171 RID: 369
		public const int DC_DUPLEX = 7;

		// Token: 0x04000172 RID: 370
		public const int DC_BINNAMES = 12;

		// Token: 0x04000173 RID: 371
		public const int DC_ENUMRESOLUTIONS = 13;

		// Token: 0x04000174 RID: 372
		public const int DC_PAPERNAMES = 16;

		// Token: 0x04000175 RID: 373
		public const int DC_ORIENTATION = 17;

		// Token: 0x04000176 RID: 374
		public const int DC_COPIES = 18;

		// Token: 0x04000177 RID: 375
		public const int PD_ALLPAGES = 0;

		// Token: 0x04000178 RID: 376
		public const int PD_SELECTION = 1;

		// Token: 0x04000179 RID: 377
		public const int PD_PAGENUMS = 2;

		// Token: 0x0400017A RID: 378
		public const int PD_CURRENTPAGE = 4194304;

		// Token: 0x0400017B RID: 379
		public const int PD_RETURNDEFAULT = 1024;

		// Token: 0x0400017C RID: 380
		public const int DI_NORMAL = 3;

		// Token: 0x0400017D RID: 381
		public const int IMAGE_ICON = 1;

		// Token: 0x0400017E RID: 382
		public const int IDI_APPLICATION = 32512;

		// Token: 0x0400017F RID: 383
		public const int IDI_HAND = 32513;

		// Token: 0x04000180 RID: 384
		public const int IDI_QUESTION = 32514;

		// Token: 0x04000181 RID: 385
		public const int IDI_EXCLAMATION = 32515;

		// Token: 0x04000182 RID: 386
		public const int IDI_ASTERISK = 32516;

		// Token: 0x04000183 RID: 387
		public const int IDI_WINLOGO = 32517;

		// Token: 0x04000184 RID: 388
		public const int IDI_WARNING = 32515;

		// Token: 0x04000185 RID: 389
		public const int IDI_ERROR = 32513;

		// Token: 0x04000186 RID: 390
		public const int IDI_INFORMATION = 32516;

		// Token: 0x04000187 RID: 391
		public const int SRCCOPY = 13369376;

		// Token: 0x04000188 RID: 392
		public const int PLANES = 14;

		// Token: 0x04000189 RID: 393
		public const int BITSPIXEL = 12;

		// Token: 0x0400018A RID: 394
		public const int LOGPIXELSX = 88;

		// Token: 0x0400018B RID: 395
		public const int LOGPIXELSY = 90;

		// Token: 0x0400018C RID: 396
		public const int PHYSICALWIDTH = 110;

		// Token: 0x0400018D RID: 397
		public const int PHYSICALHEIGHT = 111;

		// Token: 0x0400018E RID: 398
		public const int PHYSICALOFFSETX = 112;

		// Token: 0x0400018F RID: 399
		public const int PHYSICALOFFSETY = 113;

		// Token: 0x04000190 RID: 400
		public const int VERTRES = 10;

		// Token: 0x04000191 RID: 401
		public const int HORZRES = 8;

		// Token: 0x04000192 RID: 402
		public const int DM_ORIENTATION = 1;

		// Token: 0x04000193 RID: 403
		public const int DM_PAPERSIZE = 2;

		// Token: 0x04000194 RID: 404
		public const int DM_PAPERLENGTH = 4;

		// Token: 0x04000195 RID: 405
		public const int DM_PAPERWIDTH = 8;

		// Token: 0x04000196 RID: 406
		public const int DM_COPIES = 256;

		// Token: 0x04000197 RID: 407
		public const int DM_DEFAULTSOURCE = 512;

		// Token: 0x04000198 RID: 408
		public const int DM_PRINTQUALITY = 1024;

		// Token: 0x04000199 RID: 409
		public const int DM_COLOR = 2048;

		// Token: 0x0400019A RID: 410
		public const int DM_DUPLEX = 4096;

		// Token: 0x0400019B RID: 411
		public const int DM_YRESOLUTION = 8192;

		// Token: 0x0400019C RID: 412
		public const int DM_COLLATE = 32768;

		// Token: 0x0400019D RID: 413
		public const int DMORIENT_PORTRAIT = 1;

		// Token: 0x0400019E RID: 414
		public const int DMORIENT_LANDSCAPE = 2;

		// Token: 0x0400019F RID: 415
		public const int DMPAPER_LETTER = 1;

		// Token: 0x040001A0 RID: 416
		public const int DMPAPER_LETTERSMALL = 2;

		// Token: 0x040001A1 RID: 417
		public const int DMPAPER_TABLOID = 3;

		// Token: 0x040001A2 RID: 418
		public const int DMPAPER_LEDGER = 4;

		// Token: 0x040001A3 RID: 419
		public const int DMPAPER_LEGAL = 5;

		// Token: 0x040001A4 RID: 420
		public const int DMPAPER_STATEMENT = 6;

		// Token: 0x040001A5 RID: 421
		public const int DMPAPER_EXECUTIVE = 7;

		// Token: 0x040001A6 RID: 422
		public const int DMPAPER_A3 = 8;

		// Token: 0x040001A7 RID: 423
		public const int DMPAPER_A4 = 9;

		// Token: 0x040001A8 RID: 424
		public const int DMPAPER_A4SMALL = 10;

		// Token: 0x040001A9 RID: 425
		public const int DMPAPER_A5 = 11;

		// Token: 0x040001AA RID: 426
		public const int DMPAPER_B4 = 12;

		// Token: 0x040001AB RID: 427
		public const int DMPAPER_B5 = 13;

		// Token: 0x040001AC RID: 428
		public const int DMPAPER_FOLIO = 14;

		// Token: 0x040001AD RID: 429
		public const int DMPAPER_QUARTO = 15;

		// Token: 0x040001AE RID: 430
		public const int DMPAPER_10X14 = 16;

		// Token: 0x040001AF RID: 431
		public const int DMPAPER_11X17 = 17;

		// Token: 0x040001B0 RID: 432
		public const int DMPAPER_NOTE = 18;

		// Token: 0x040001B1 RID: 433
		public const int DMPAPER_ENV_9 = 19;

		// Token: 0x040001B2 RID: 434
		public const int DMPAPER_ENV_10 = 20;

		// Token: 0x040001B3 RID: 435
		public const int DMPAPER_ENV_11 = 21;

		// Token: 0x040001B4 RID: 436
		public const int DMPAPER_ENV_12 = 22;

		// Token: 0x040001B5 RID: 437
		public const int DMPAPER_ENV_14 = 23;

		// Token: 0x040001B6 RID: 438
		public const int DMPAPER_CSHEET = 24;

		// Token: 0x040001B7 RID: 439
		public const int DMPAPER_DSHEET = 25;

		// Token: 0x040001B8 RID: 440
		public const int DMPAPER_ESHEET = 26;

		// Token: 0x040001B9 RID: 441
		public const int DMPAPER_ENV_DL = 27;

		// Token: 0x040001BA RID: 442
		public const int DMPAPER_ENV_C5 = 28;

		// Token: 0x040001BB RID: 443
		public const int DMPAPER_ENV_C3 = 29;

		// Token: 0x040001BC RID: 444
		public const int DMPAPER_ENV_C4 = 30;

		// Token: 0x040001BD RID: 445
		public const int DMPAPER_ENV_C6 = 31;

		// Token: 0x040001BE RID: 446
		public const int DMPAPER_ENV_C65 = 32;

		// Token: 0x040001BF RID: 447
		public const int DMPAPER_ENV_B4 = 33;

		// Token: 0x040001C0 RID: 448
		public const int DMPAPER_ENV_B5 = 34;

		// Token: 0x040001C1 RID: 449
		public const int DMPAPER_ENV_B6 = 35;

		// Token: 0x040001C2 RID: 450
		public const int DMPAPER_ENV_ITALY = 36;

		// Token: 0x040001C3 RID: 451
		public const int DMPAPER_ENV_MONARCH = 37;

		// Token: 0x040001C4 RID: 452
		public const int DMPAPER_ENV_PERSONAL = 38;

		// Token: 0x040001C5 RID: 453
		public const int DMPAPER_FANFOLD_US = 39;

		// Token: 0x040001C6 RID: 454
		public const int DMPAPER_FANFOLD_STD_GERMAN = 40;

		// Token: 0x040001C7 RID: 455
		public const int DMPAPER_FANFOLD_LGL_GERMAN = 41;

		// Token: 0x040001C8 RID: 456
		public const int DMPAPER_ISO_B4 = 42;

		// Token: 0x040001C9 RID: 457
		public const int DMPAPER_JAPANESE_POSTCARD = 43;

		// Token: 0x040001CA RID: 458
		public const int DMPAPER_9X11 = 44;

		// Token: 0x040001CB RID: 459
		public const int DMPAPER_10X11 = 45;

		// Token: 0x040001CC RID: 460
		public const int DMPAPER_15X11 = 46;

		// Token: 0x040001CD RID: 461
		public const int DMPAPER_ENV_INVITE = 47;

		// Token: 0x040001CE RID: 462
		public const int DMPAPER_RESERVED_48 = 48;

		// Token: 0x040001CF RID: 463
		public const int DMPAPER_RESERVED_49 = 49;

		// Token: 0x040001D0 RID: 464
		public const int DMPAPER_LETTER_EXTRA = 50;

		// Token: 0x040001D1 RID: 465
		public const int DMPAPER_LEGAL_EXTRA = 51;

		// Token: 0x040001D2 RID: 466
		public const int DMPAPER_TABLOID_EXTRA = 52;

		// Token: 0x040001D3 RID: 467
		public const int DMPAPER_A4_EXTRA = 53;

		// Token: 0x040001D4 RID: 468
		public const int DMPAPER_LETTER_TRANSVERSE = 54;

		// Token: 0x040001D5 RID: 469
		public const int DMPAPER_A4_TRANSVERSE = 55;

		// Token: 0x040001D6 RID: 470
		public const int DMPAPER_LETTER_EXTRA_TRANSVERSE = 56;

		// Token: 0x040001D7 RID: 471
		public const int DMPAPER_A_PLUS = 57;

		// Token: 0x040001D8 RID: 472
		public const int DMPAPER_B_PLUS = 58;

		// Token: 0x040001D9 RID: 473
		public const int DMPAPER_LETTER_PLUS = 59;

		// Token: 0x040001DA RID: 474
		public const int DMPAPER_A4_PLUS = 60;

		// Token: 0x040001DB RID: 475
		public const int DMPAPER_A5_TRANSVERSE = 61;

		// Token: 0x040001DC RID: 476
		public const int DMPAPER_B5_TRANSVERSE = 62;

		// Token: 0x040001DD RID: 477
		public const int DMPAPER_A3_EXTRA = 63;

		// Token: 0x040001DE RID: 478
		public const int DMPAPER_A5_EXTRA = 64;

		// Token: 0x040001DF RID: 479
		public const int DMPAPER_B5_EXTRA = 65;

		// Token: 0x040001E0 RID: 480
		public const int DMPAPER_A2 = 66;

		// Token: 0x040001E1 RID: 481
		public const int DMPAPER_A3_TRANSVERSE = 67;

		// Token: 0x040001E2 RID: 482
		public const int DMPAPER_A3_EXTRA_TRANSVERSE = 68;

		// Token: 0x040001E3 RID: 483
		public const int DMPAPER_DBL_JAPANESE_POSTCARD = 69;

		// Token: 0x040001E4 RID: 484
		public const int DMPAPER_A6 = 70;

		// Token: 0x040001E5 RID: 485
		public const int DMPAPER_JENV_KAKU2 = 71;

		// Token: 0x040001E6 RID: 486
		public const int DMPAPER_JENV_KAKU3 = 72;

		// Token: 0x040001E7 RID: 487
		public const int DMPAPER_JENV_CHOU3 = 73;

		// Token: 0x040001E8 RID: 488
		public const int DMPAPER_JENV_CHOU4 = 74;

		// Token: 0x040001E9 RID: 489
		public const int DMPAPER_LETTER_ROTATED = 75;

		// Token: 0x040001EA RID: 490
		public const int DMPAPER_A3_ROTATED = 76;

		// Token: 0x040001EB RID: 491
		public const int DMPAPER_A4_ROTATED = 77;

		// Token: 0x040001EC RID: 492
		public const int DMPAPER_A5_ROTATED = 78;

		// Token: 0x040001ED RID: 493
		public const int DMPAPER_B4_JIS_ROTATED = 79;

		// Token: 0x040001EE RID: 494
		public const int DMPAPER_B5_JIS_ROTATED = 80;

		// Token: 0x040001EF RID: 495
		public const int DMPAPER_JAPANESE_POSTCARD_ROTATED = 81;

		// Token: 0x040001F0 RID: 496
		public const int DMPAPER_DBL_JAPANESE_POSTCARD_ROTATED = 82;

		// Token: 0x040001F1 RID: 497
		public const int DMPAPER_A6_ROTATED = 83;

		// Token: 0x040001F2 RID: 498
		public const int DMPAPER_JENV_KAKU2_ROTATED = 84;

		// Token: 0x040001F3 RID: 499
		public const int DMPAPER_JENV_KAKU3_ROTATED = 85;

		// Token: 0x040001F4 RID: 500
		public const int DMPAPER_JENV_CHOU3_ROTATED = 86;

		// Token: 0x040001F5 RID: 501
		public const int DMPAPER_JENV_CHOU4_ROTATED = 87;

		// Token: 0x040001F6 RID: 502
		public const int DMPAPER_B6_JIS = 88;

		// Token: 0x040001F7 RID: 503
		public const int DMPAPER_B6_JIS_ROTATED = 89;

		// Token: 0x040001F8 RID: 504
		public const int DMPAPER_12X11 = 90;

		// Token: 0x040001F9 RID: 505
		public const int DMPAPER_JENV_YOU4 = 91;

		// Token: 0x040001FA RID: 506
		public const int DMPAPER_JENV_YOU4_ROTATED = 92;

		// Token: 0x040001FB RID: 507
		public const int DMPAPER_P16K = 93;

		// Token: 0x040001FC RID: 508
		public const int DMPAPER_P32K = 94;

		// Token: 0x040001FD RID: 509
		public const int DMPAPER_P32KBIG = 95;

		// Token: 0x040001FE RID: 510
		public const int DMPAPER_PENV_1 = 96;

		// Token: 0x040001FF RID: 511
		public const int DMPAPER_PENV_2 = 97;

		// Token: 0x04000200 RID: 512
		public const int DMPAPER_PENV_3 = 98;

		// Token: 0x04000201 RID: 513
		public const int DMPAPER_PENV_4 = 99;

		// Token: 0x04000202 RID: 514
		public const int DMPAPER_PENV_5 = 100;

		// Token: 0x04000203 RID: 515
		public const int DMPAPER_PENV_6 = 101;

		// Token: 0x04000204 RID: 516
		public const int DMPAPER_PENV_7 = 102;

		// Token: 0x04000205 RID: 517
		public const int DMPAPER_PENV_8 = 103;

		// Token: 0x04000206 RID: 518
		public const int DMPAPER_PENV_9 = 104;

		// Token: 0x04000207 RID: 519
		public const int DMPAPER_PENV_10 = 105;

		// Token: 0x04000208 RID: 520
		public const int DMPAPER_P16K_ROTATED = 106;

		// Token: 0x04000209 RID: 521
		public const int DMPAPER_P32K_ROTATED = 107;

		// Token: 0x0400020A RID: 522
		public const int DMPAPER_P32KBIG_ROTATED = 108;

		// Token: 0x0400020B RID: 523
		public const int DMPAPER_PENV_1_ROTATED = 109;

		// Token: 0x0400020C RID: 524
		public const int DMPAPER_PENV_2_ROTATED = 110;

		// Token: 0x0400020D RID: 525
		public const int DMPAPER_PENV_3_ROTATED = 111;

		// Token: 0x0400020E RID: 526
		public const int DMPAPER_PENV_4_ROTATED = 112;

		// Token: 0x0400020F RID: 527
		public const int DMPAPER_PENV_5_ROTATED = 113;

		// Token: 0x04000210 RID: 528
		public const int DMPAPER_PENV_6_ROTATED = 114;

		// Token: 0x04000211 RID: 529
		public const int DMPAPER_PENV_7_ROTATED = 115;

		// Token: 0x04000212 RID: 530
		public const int DMPAPER_PENV_8_ROTATED = 116;

		// Token: 0x04000213 RID: 531
		public const int DMPAPER_PENV_9_ROTATED = 117;

		// Token: 0x04000214 RID: 532
		public const int DMPAPER_PENV_10_ROTATED = 118;

		// Token: 0x04000215 RID: 533
		public const int DMPAPER_LAST = 118;

		// Token: 0x04000216 RID: 534
		public const int DMBIN_UPPER = 1;

		// Token: 0x04000217 RID: 535
		public const int DMBIN_LOWER = 2;

		// Token: 0x04000218 RID: 536
		public const int DMBIN_MIDDLE = 3;

		// Token: 0x04000219 RID: 537
		public const int DMBIN_MANUAL = 4;

		// Token: 0x0400021A RID: 538
		public const int DMBIN_ENVELOPE = 5;

		// Token: 0x0400021B RID: 539
		public const int DMBIN_ENVMANUAL = 6;

		// Token: 0x0400021C RID: 540
		public const int DMBIN_AUTO = 7;

		// Token: 0x0400021D RID: 541
		public const int DMBIN_TRACTOR = 8;

		// Token: 0x0400021E RID: 542
		public const int DMBIN_SMALLFMT = 9;

		// Token: 0x0400021F RID: 543
		public const int DMBIN_LARGEFMT = 10;

		// Token: 0x04000220 RID: 544
		public const int DMBIN_LARGECAPACITY = 11;

		// Token: 0x04000221 RID: 545
		public const int DMBIN_CASSETTE = 14;

		// Token: 0x04000222 RID: 546
		public const int DMBIN_FORMSOURCE = 15;

		// Token: 0x04000223 RID: 547
		public const int DMBIN_LAST = 15;

		// Token: 0x04000224 RID: 548
		public const int DMBIN_USER = 256;

		// Token: 0x04000225 RID: 549
		public const int DMRES_DRAFT = -1;

		// Token: 0x04000226 RID: 550
		public const int DMRES_LOW = -2;

		// Token: 0x04000227 RID: 551
		public const int DMRES_MEDIUM = -3;

		// Token: 0x04000228 RID: 552
		public const int DMRES_HIGH = -4;

		// Token: 0x04000229 RID: 553
		public const int DMCOLOR_MONOCHROME = 1;

		// Token: 0x0400022A RID: 554
		public const int DMCOLOR_COLOR = 2;

		// Token: 0x0400022B RID: 555
		public const int DMDUP_SIMPLEX = 1;

		// Token: 0x0400022C RID: 556
		public const int DMDUP_VERTICAL = 2;

		// Token: 0x0400022D RID: 557
		public const int DMDUP_HORIZONTAL = 3;

		// Token: 0x0400022E RID: 558
		public const int DMCOLLATE_FALSE = 0;

		// Token: 0x0400022F RID: 559
		public const int DMCOLLATE_TRUE = 1;

		// Token: 0x04000230 RID: 560
		public const int PRINTER_ENUM_LOCAL = 2;

		// Token: 0x04000231 RID: 561
		public const int PRINTER_ENUM_CONNECTIONS = 4;

		// Token: 0x04000232 RID: 562
		public const int SRCPAINT = 15597702;

		// Token: 0x04000233 RID: 563
		public const int SRCAND = 8913094;

		// Token: 0x04000234 RID: 564
		public const int SRCINVERT = 6684742;

		// Token: 0x04000235 RID: 565
		public const int SRCERASE = 4457256;

		// Token: 0x04000236 RID: 566
		public const int NOTSRCCOPY = 3342344;

		// Token: 0x04000237 RID: 567
		public const int NOTSRCERASE = 1114278;

		// Token: 0x04000238 RID: 568
		public const int MERGECOPY = 12583114;

		// Token: 0x04000239 RID: 569
		public const int MERGEPAINT = 12255782;

		// Token: 0x0400023A RID: 570
		public const int PATCOPY = 15728673;

		// Token: 0x0400023B RID: 571
		public const int PATPAINT = 16452105;

		// Token: 0x0400023C RID: 572
		public const int PATINVERT = 5898313;

		// Token: 0x0400023D RID: 573
		public const int DSTINVERT = 5570569;

		// Token: 0x0400023E RID: 574
		public const int BLACKNESS = 66;

		// Token: 0x0400023F RID: 575
		public const int WHITENESS = 16711778;

		// Token: 0x04000240 RID: 576
		public const int CAPTUREBLT = 1073741824;

		// Token: 0x04000241 RID: 577
		public const int SM_CXICON = 11;

		// Token: 0x04000242 RID: 578
		public const int SM_CYICON = 12;

		// Token: 0x04000243 RID: 579
		public const int DEFAULT_CHARSET = 1;

		// Token: 0x04000244 RID: 580
		public const int NOMIRRORBITMAP = -2147483648;

		// Token: 0x04000245 RID: 581
		public const int QUERYESCSUPPORT = 8;

		// Token: 0x04000246 RID: 582
		public const int CHECKJPEGFORMAT = 4119;

		// Token: 0x04000247 RID: 583
		public const int CHECKPNGFORMAT = 4120;

		// Token: 0x04000248 RID: 584
		public const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x04000249 RID: 585
		public const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x0400024A RID: 586
		public const int ERROR_PROC_NOT_FOUND = 127;

		// Token: 0x02000020 RID: 32
		internal class Gdip : GDIPlus
		{
			// Token: 0x060000B7 RID: 183 RVA: 0x000048F4 File Offset: 0x00002AF4
			static Gdip()
			{
				AppDomain currentDomain = AppDomain.CurrentDomain;
				currentDomain.ProcessExit += SafeNativeMethods.Gdip.OnProcessExit;
				if (!currentDomain.IsDefaultAppDomain())
				{
					currentDomain.DomainUnload += SafeNativeMethods.Gdip.OnProcessExit;
				}
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004952 File Offset: 0x00002B52
			private static bool Initialized
			{
				get
				{
					return SafeNativeMethods.Gdip.s_initToken != IntPtr.Zero;
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004964 File Offset: 0x00002B64
			internal static IDictionary ThreadData
			{
				get
				{
					LocalDataStoreSlot namedDataSlot = Thread.GetNamedDataSlot("system.drawing.threaddata");
					IDictionary dictionary = (IDictionary)Thread.GetData(namedDataSlot);
					if (dictionary == null)
					{
						dictionary = new Hashtable();
						Thread.SetData(namedDataSlot, dictionary);
					}
					return dictionary;
				}
			}

			// Token: 0x060000BA RID: 186 RVA: 0x00004999 File Offset: 0x00002B99
			[MethodImpl(MethodImplOptions.NoInlining)]
			private static void ClearThreadData()
			{
				Thread.SetData(Thread.GetNamedDataSlot("system.drawing.threaddata"), null);
			}

			// Token: 0x060000BB RID: 187 RVA: 0x000049AC File Offset: 0x00002BAC
			private static void Shutdown()
			{
				if (SafeNativeMethods.Gdip.Initialized)
				{
					SafeNativeMethods.Gdip.ClearThreadData();
					AppDomain currentDomain = AppDomain.CurrentDomain;
					currentDomain.ProcessExit -= SafeNativeMethods.Gdip.OnProcessExit;
					if (!currentDomain.IsDefaultAppDomain())
					{
						currentDomain.DomainUnload -= SafeNativeMethods.Gdip.OnProcessExit;
					}
				}
			}

			// Token: 0x060000BC RID: 188 RVA: 0x000049F7 File Offset: 0x00002BF7
			[PrePrepareMethod]
			private static void OnProcessExit(object sender, EventArgs e)
			{
				SafeNativeMethods.Gdip.Shutdown();
			}

			// Token: 0x060000BD RID: 189 RVA: 0x000049FE File Offset: 0x00002BFE
			internal static void DummyFunction()
			{
			}

			// Token: 0x060000BE RID: 190 RVA: 0x00004A00 File Offset: 0x00002C00
			internal static void CheckStatus(int status)
			{
				if (status != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(status);
				}
			}

			// Token: 0x060000BF RID: 191 RVA: 0x00004A0C File Offset: 0x00002C0C
			internal static Exception StatusException(int status)
			{
				switch (status)
				{
				case 1:
					return new ExternalException(SR.Format("A generic error occurred in GDI+.", Array.Empty<object>()), -2147467259);
				case 2:
					return new ArgumentException(SR.Format("Parameter is not valid.", Array.Empty<object>()));
				case 3:
					return new OutOfMemoryException(SR.Format("Out of memory.", Array.Empty<object>()));
				case 4:
					return new InvalidOperationException(SR.Format("Object is currently in use elsewhere.", Array.Empty<object>()));
				case 5:
					return new OutOfMemoryException(SR.Format("Buffer is too small (internal GDI+ error).", Array.Empty<object>()));
				case 6:
					return new NotImplementedException(SR.Format("Not implemented.", Array.Empty<object>()));
				case 7:
					return new ExternalException(SR.Format("A generic error occurred in GDI+.", Array.Empty<object>()), -2147467259);
				case 8:
					return new InvalidOperationException(SR.Format("Bitmap region is already locked.", Array.Empty<object>()));
				case 9:
					return new ExternalException(SR.Format("Function was ended.", Array.Empty<object>()), -2147467260);
				case 10:
					return new FileNotFoundException(SR.Format("File not found.", Array.Empty<object>()));
				case 11:
					return new OverflowException(SR.Format("Overflow error.", Array.Empty<object>()));
				case 12:
					return new ExternalException(SR.Format("File access is denied.", Array.Empty<object>()), -2147024891);
				case 13:
					return new ArgumentException(SR.Format("Image format is unknown.", Array.Empty<object>()));
				case 14:
					return new ArgumentException(SR.Format("Font '{0}' cannot be found.", new object[]
					{
						"?"
					}));
				case 15:
					return new ArgumentException(SR.Format("Font '{0}' does not support style '{1}'.", new object[]
					{
						"?",
						"?"
					}));
				case 16:
					return new ArgumentException(SR.Format("Only TrueType fonts are supported. This is not a TrueType font.", Array.Empty<object>()));
				case 17:
					return new ExternalException(SR.Format("Current version of GDI+ does not support this feature.", Array.Empty<object>()), -2147467259);
				case 18:
					return new ExternalException(SR.Format("GDI+ is not properly initialized (internal GDI+ error).", Array.Empty<object>()), -2147467259);
				case 19:
					return new ArgumentException(SR.Format("Property cannot be found.", Array.Empty<object>()));
				case 20:
					return new ArgumentException(SR.Format("Property is not supported.", Array.Empty<object>()));
				default:
					return new ExternalException(SR.Format("Unknown GDI+ error occurred.", Array.Empty<object>()), -2147418113);
				}
			}

			// Token: 0x060000C0 RID: 192 RVA: 0x00004C6C File Offset: 0x00002E6C
			internal static PointF[] ConvertGPPOINTFArrayF(IntPtr memory, int count)
			{
				if (memory == IntPtr.Zero)
				{
					throw new ArgumentNullException("memory");
				}
				PointF[] array = new PointF[count];
				Type typeFromHandle = typeof(GPPOINTF);
				int num = Marshal.SizeOf(typeFromHandle);
				for (int i = 0; i < count; i++)
				{
					GPPOINTF gppointf = (GPPOINTF)Marshal.PtrToStructure((IntPtr)((long)memory + (long)(i * num)), typeFromHandle);
					array[i] = new PointF(gppointf.X, gppointf.Y);
				}
				return array;
			}

			// Token: 0x060000C1 RID: 193 RVA: 0x00004CF0 File Offset: 0x00002EF0
			internal static Point[] ConvertGPPOINTArray(IntPtr memory, int count)
			{
				if (memory == IntPtr.Zero)
				{
					throw new ArgumentNullException("memory");
				}
				Point[] array = new Point[count];
				Type typeFromHandle = typeof(GPPOINT);
				int num = Marshal.SizeOf(typeFromHandle);
				for (int i = 0; i < count; i++)
				{
					GPPOINT gppoint = (GPPOINT)Marshal.PtrToStructure((IntPtr)((long)memory + (long)(i * num)), typeFromHandle);
					array[i] = new Point(gppoint.X, gppoint.Y);
				}
				return array;
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x00004D74 File Offset: 0x00002F74
			internal static IntPtr ConvertPointToMemory(PointF[] points)
			{
				if (points == null)
				{
					throw new ArgumentNullException("points");
				}
				int num = Marshal.SizeOf(typeof(GPPOINTF));
				int num2 = points.Length;
				IntPtr intPtr = Marshal.AllocHGlobal(checked(num2 * num));
				for (int i = 0; i < num2; i++)
				{
					Marshal.StructureToPtr<GPPOINTF>(new GPPOINTF(points[i]), (IntPtr)(checked((long)intPtr + unchecked((long)(checked(i * num))))), false);
				}
				return intPtr;
			}

			// Token: 0x060000C3 RID: 195 RVA: 0x00004DDC File Offset: 0x00002FDC
			internal static IntPtr ConvertPointToMemory(Point[] points)
			{
				if (points == null)
				{
					throw new ArgumentNullException("points");
				}
				int num = Marshal.SizeOf(typeof(GPPOINT));
				int num2 = points.Length;
				IntPtr intPtr = Marshal.AllocHGlobal(checked(num2 * num));
				for (int i = 0; i < num2; i++)
				{
					Marshal.StructureToPtr<GPPOINT>(new GPPOINT(points[i]), (IntPtr)(checked((long)intPtr + unchecked((long)(checked(i * num))))), false);
				}
				return intPtr;
			}

			// Token: 0x060000C4 RID: 196 RVA: 0x00004E44 File Offset: 0x00003044
			internal static IntPtr ConvertRectangleToMemory(RectangleF[] rect)
			{
				if (rect == null)
				{
					throw new ArgumentNullException("rect");
				}
				int num = Marshal.SizeOf(typeof(GPRECTF));
				int num2 = rect.Length;
				IntPtr intPtr = Marshal.AllocHGlobal(checked(num2 * num));
				for (int i = 0; i < num2; i++)
				{
					Marshal.StructureToPtr<GPRECTF>(new GPRECTF(rect[i]), (IntPtr)(checked((long)intPtr + unchecked((long)(checked(i * num))))), false);
				}
				return intPtr;
			}

			// Token: 0x060000C5 RID: 197 RVA: 0x00004EAC File Offset: 0x000030AC
			internal static IntPtr ConvertRectangleToMemory(Rectangle[] rect)
			{
				if (rect == null)
				{
					throw new ArgumentNullException("rect");
				}
				int num = Marshal.SizeOf(typeof(GPRECT));
				int num2 = rect.Length;
				IntPtr intPtr = Marshal.AllocHGlobal(checked(num2 * num));
				for (int i = 0; i < num2; i++)
				{
					Marshal.StructureToPtr<GPRECT>(new GPRECT(rect[i]), (IntPtr)(checked((long)intPtr + unchecked((long)(checked(i * num))))), false);
				}
				return intPtr;
			}

			// Token: 0x060000C6 RID: 198 RVA: 0x00004F13 File Offset: 0x00003113
			public Gdip()
			{
			}

			// Token: 0x0400024B RID: 587
			private static readonly TraceSwitch s_gdiPlusInitialization = new TraceSwitch("GdiPlusInitialization", "Tracks GDI+ initialization and teardown");

			// Token: 0x0400024C RID: 588
			private static IntPtr s_initToken = (IntPtr)1;

			// Token: 0x0400024D RID: 589
			private const string ThreadDataSlotName = "system.drawing.threaddata";

			// Token: 0x0400024E RID: 590
			internal const int Ok = 0;

			// Token: 0x0400024F RID: 591
			internal const int GenericError = 1;

			// Token: 0x04000250 RID: 592
			internal const int InvalidParameter = 2;

			// Token: 0x04000251 RID: 593
			internal const int OutOfMemory = 3;

			// Token: 0x04000252 RID: 594
			internal const int ObjectBusy = 4;

			// Token: 0x04000253 RID: 595
			internal const int InsufficientBuffer = 5;

			// Token: 0x04000254 RID: 596
			internal const int NotImplemented = 6;

			// Token: 0x04000255 RID: 597
			internal const int Win32Error = 7;

			// Token: 0x04000256 RID: 598
			internal const int WrongState = 8;

			// Token: 0x04000257 RID: 599
			internal const int Aborted = 9;

			// Token: 0x04000258 RID: 600
			internal const int FileNotFound = 10;

			// Token: 0x04000259 RID: 601
			internal const int ValueOverflow = 11;

			// Token: 0x0400025A RID: 602
			internal const int AccessDenied = 12;

			// Token: 0x0400025B RID: 603
			internal const int UnknownImageFormat = 13;

			// Token: 0x0400025C RID: 604
			internal const int FontFamilyNotFound = 14;

			// Token: 0x0400025D RID: 605
			internal const int FontStyleNotFound = 15;

			// Token: 0x0400025E RID: 606
			internal const int NotTrueTypeFont = 16;

			// Token: 0x0400025F RID: 607
			internal const int UnsupportedGdiplusVersion = 17;

			// Token: 0x04000260 RID: 608
			internal const int GdiplusNotInitialized = 18;

			// Token: 0x04000261 RID: 609
			internal const int PropertyNotFound = 19;

			// Token: 0x04000262 RID: 610
			internal const int PropertyNotSupported = 20;
		}

		// Token: 0x02000021 RID: 33
		[StructLayout(LayoutKind.Sequential)]
		public class ENHMETAHEADER
		{
			// Token: 0x060000C7 RID: 199 RVA: 0x00004F1B File Offset: 0x0000311B
			public ENHMETAHEADER()
			{
			}

			// Token: 0x04000263 RID: 611
			public int iType;

			// Token: 0x04000264 RID: 612
			public int nSize = 40;

			// Token: 0x04000265 RID: 613
			public int rclBounds_left;

			// Token: 0x04000266 RID: 614
			public int rclBounds_top;

			// Token: 0x04000267 RID: 615
			public int rclBounds_right;

			// Token: 0x04000268 RID: 616
			public int rclBounds_bottom;

			// Token: 0x04000269 RID: 617
			public int rclFrame_left;

			// Token: 0x0400026A RID: 618
			public int rclFrame_top;

			// Token: 0x0400026B RID: 619
			public int rclFrame_right;

			// Token: 0x0400026C RID: 620
			public int rclFrame_bottom;

			// Token: 0x0400026D RID: 621
			public int dSignature;

			// Token: 0x0400026E RID: 622
			public int nVersion;

			// Token: 0x0400026F RID: 623
			public int nBytes;

			// Token: 0x04000270 RID: 624
			public int nRecords;

			// Token: 0x04000271 RID: 625
			public short nHandles;

			// Token: 0x04000272 RID: 626
			public short sReserved;

			// Token: 0x04000273 RID: 627
			public int nDescription;

			// Token: 0x04000274 RID: 628
			public int offDescription;

			// Token: 0x04000275 RID: 629
			public int nPalEntries;

			// Token: 0x04000276 RID: 630
			public int szlDevice_cx;

			// Token: 0x04000277 RID: 631
			public int szlDevice_cy;

			// Token: 0x04000278 RID: 632
			public int szlMillimeters_cx;

			// Token: 0x04000279 RID: 633
			public int szlMillimeters_cy;

			// Token: 0x0400027A RID: 634
			public int cbPixelFormat;

			// Token: 0x0400027B RID: 635
			public int offPixelFormat;

			// Token: 0x0400027C RID: 636
			public int bOpenGL;
		}

		// Token: 0x02000022 RID: 34
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class DOCINFO
		{
			// Token: 0x060000C8 RID: 200 RVA: 0x00004F2B File Offset: 0x0000312B
			public DOCINFO()
			{
			}

			// Token: 0x0400027D RID: 637
			public int cbSize = 20;

			// Token: 0x0400027E RID: 638
			public string lpszDocName;

			// Token: 0x0400027F RID: 639
			public string lpszOutput;

			// Token: 0x04000280 RID: 640
			public string lpszDatatype;

			// Token: 0x04000281 RID: 641
			public int fwType;
		}

		// Token: 0x02000023 RID: 35
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class PRINTDLG
		{
			// Token: 0x060000C9 RID: 201 RVA: 0x00002050 File Offset: 0x00000250
			public PRINTDLG()
			{
			}

			// Token: 0x04000282 RID: 642
			public int lStructSize;

			// Token: 0x04000283 RID: 643
			public IntPtr hwndOwner;

			// Token: 0x04000284 RID: 644
			public IntPtr hDevMode;

			// Token: 0x04000285 RID: 645
			public IntPtr hDevNames;

			// Token: 0x04000286 RID: 646
			public IntPtr hDC;

			// Token: 0x04000287 RID: 647
			public int Flags;

			// Token: 0x04000288 RID: 648
			public short nFromPage;

			// Token: 0x04000289 RID: 649
			public short nToPage;

			// Token: 0x0400028A RID: 650
			public short nMinPage;

			// Token: 0x0400028B RID: 651
			public short nMaxPage;

			// Token: 0x0400028C RID: 652
			public short nCopies;

			// Token: 0x0400028D RID: 653
			public IntPtr hInstance;

			// Token: 0x0400028E RID: 654
			public IntPtr lCustData;

			// Token: 0x0400028F RID: 655
			public IntPtr lpfnPrintHook;

			// Token: 0x04000290 RID: 656
			public IntPtr lpfnSetupHook;

			// Token: 0x04000291 RID: 657
			public string lpPrintTemplateName;

			// Token: 0x04000292 RID: 658
			public string lpSetupTemplateName;

			// Token: 0x04000293 RID: 659
			public IntPtr hPrintTemplate;

			// Token: 0x04000294 RID: 660
			public IntPtr hSetupTemplate;
		}

		// Token: 0x02000024 RID: 36
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
		public class PRINTDLGX86
		{
			// Token: 0x060000CA RID: 202 RVA: 0x00002050 File Offset: 0x00000250
			public PRINTDLGX86()
			{
			}

			// Token: 0x04000295 RID: 661
			public int lStructSize;

			// Token: 0x04000296 RID: 662
			public IntPtr hwndOwner;

			// Token: 0x04000297 RID: 663
			public IntPtr hDevMode;

			// Token: 0x04000298 RID: 664
			public IntPtr hDevNames;

			// Token: 0x04000299 RID: 665
			public IntPtr hDC;

			// Token: 0x0400029A RID: 666
			public int Flags;

			// Token: 0x0400029B RID: 667
			public short nFromPage;

			// Token: 0x0400029C RID: 668
			public short nToPage;

			// Token: 0x0400029D RID: 669
			public short nMinPage;

			// Token: 0x0400029E RID: 670
			public short nMaxPage;

			// Token: 0x0400029F RID: 671
			public short nCopies;

			// Token: 0x040002A0 RID: 672
			public IntPtr hInstance;

			// Token: 0x040002A1 RID: 673
			public IntPtr lCustData;

			// Token: 0x040002A2 RID: 674
			public IntPtr lpfnPrintHook;

			// Token: 0x040002A3 RID: 675
			public IntPtr lpfnSetupHook;

			// Token: 0x040002A4 RID: 676
			public string lpPrintTemplateName;

			// Token: 0x040002A5 RID: 677
			public string lpSetupTemplateName;

			// Token: 0x040002A6 RID: 678
			public IntPtr hPrintTemplate;

			// Token: 0x040002A7 RID: 679
			public IntPtr hSetupTemplate;
		}

		// Token: 0x02000025 RID: 37
		[StructLayout(LayoutKind.Sequential)]
		public class ICONINFO
		{
			// Token: 0x060000CB RID: 203 RVA: 0x00004F3B File Offset: 0x0000313B
			public ICONINFO()
			{
			}

			// Token: 0x040002A8 RID: 680
			public int fIcon;

			// Token: 0x040002A9 RID: 681
			public int xHotspot;

			// Token: 0x040002AA RID: 682
			public int yHotspot;

			// Token: 0x040002AB RID: 683
			public IntPtr hbmMask = IntPtr.Zero;

			// Token: 0x040002AC RID: 684
			public IntPtr hbmColor = IntPtr.Zero;
		}

		// Token: 0x02000026 RID: 38
		[StructLayout(LayoutKind.Sequential)]
		public class BITMAP
		{
			// Token: 0x060000CC RID: 204 RVA: 0x00004F59 File Offset: 0x00003159
			public BITMAP()
			{
			}

			// Token: 0x040002AD RID: 685
			public int bmType;

			// Token: 0x040002AE RID: 686
			public int bmWidth;

			// Token: 0x040002AF RID: 687
			public int bmHeight;

			// Token: 0x040002B0 RID: 688
			public int bmWidthBytes;

			// Token: 0x040002B1 RID: 689
			public short bmPlanes;

			// Token: 0x040002B2 RID: 690
			public short bmBitsPixel;

			// Token: 0x040002B3 RID: 691
			public IntPtr bmBits = IntPtr.Zero;
		}

		// Token: 0x02000027 RID: 39
		[StructLayout(LayoutKind.Sequential)]
		public class BITMAPINFOHEADER
		{
			// Token: 0x060000CD RID: 205 RVA: 0x00004F6C File Offset: 0x0000316C
			public BITMAPINFOHEADER()
			{
			}

			// Token: 0x040002B4 RID: 692
			public int biSize = 40;

			// Token: 0x040002B5 RID: 693
			public int biWidth;

			// Token: 0x040002B6 RID: 694
			public int biHeight;

			// Token: 0x040002B7 RID: 695
			public short biPlanes;

			// Token: 0x040002B8 RID: 696
			public short biBitCount;

			// Token: 0x040002B9 RID: 697
			public int biCompression;

			// Token: 0x040002BA RID: 698
			public int biSizeImage;

			// Token: 0x040002BB RID: 699
			public int biXPelsPerMeter;

			// Token: 0x040002BC RID: 700
			public int biYPelsPerMeter;

			// Token: 0x040002BD RID: 701
			public int biClrUsed;

			// Token: 0x040002BE RID: 702
			public int biClrImportant;
		}

		// Token: 0x02000028 RID: 40
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class LOGFONT
		{
			// Token: 0x060000CE RID: 206 RVA: 0x00004F7C File Offset: 0x0000317C
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"lfHeight=",
					this.lfHeight.ToString(),
					", lfWidth=",
					this.lfWidth.ToString(),
					", lfEscapement=",
					this.lfEscapement.ToString(),
					", lfOrientation=",
					this.lfOrientation.ToString(),
					", lfWeight=",
					this.lfWeight.ToString(),
					", lfItalic=",
					this.lfItalic.ToString(),
					", lfUnderline=",
					this.lfUnderline.ToString(),
					", lfStrikeOut=",
					this.lfStrikeOut.ToString(),
					", lfCharSet=",
					this.lfCharSet.ToString(),
					", lfOutPrecision=",
					this.lfOutPrecision.ToString(),
					", lfClipPrecision=",
					this.lfClipPrecision.ToString(),
					", lfQuality=",
					this.lfQuality.ToString(),
					", lfPitchAndFamily=",
					this.lfPitchAndFamily.ToString(),
					", lfFaceName=",
					this.lfFaceName
				});
			}

			// Token: 0x060000CF RID: 207 RVA: 0x00002050 File Offset: 0x00000250
			public LOGFONT()
			{
			}

			// Token: 0x040002BF RID: 703
			public int lfHeight;

			// Token: 0x040002C0 RID: 704
			public int lfWidth;

			// Token: 0x040002C1 RID: 705
			public int lfEscapement;

			// Token: 0x040002C2 RID: 706
			public int lfOrientation;

			// Token: 0x040002C3 RID: 707
			public int lfWeight;

			// Token: 0x040002C4 RID: 708
			public byte lfItalic;

			// Token: 0x040002C5 RID: 709
			public byte lfUnderline;

			// Token: 0x040002C6 RID: 710
			public byte lfStrikeOut;

			// Token: 0x040002C7 RID: 711
			public byte lfCharSet;

			// Token: 0x040002C8 RID: 712
			public byte lfOutPrecision;

			// Token: 0x040002C9 RID: 713
			public byte lfClipPrecision;

			// Token: 0x040002CA RID: 714
			public byte lfQuality;

			// Token: 0x040002CB RID: 715
			public byte lfPitchAndFamily;

			// Token: 0x040002CC RID: 716
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string lfFaceName;
		}

		// Token: 0x02000029 RID: 41
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct ICONDIR
		{
			// Token: 0x040002CD RID: 717
			public short idReserved;

			// Token: 0x040002CE RID: 718
			public short idType;

			// Token: 0x040002CF RID: 719
			public short idCount;

			// Token: 0x040002D0 RID: 720
			public SafeNativeMethods.ICONDIRENTRY idEntries;
		}

		// Token: 0x0200002A RID: 42
		public struct ICONDIRENTRY
		{
			// Token: 0x040002D1 RID: 721
			public byte bWidth;

			// Token: 0x040002D2 RID: 722
			public byte bHeight;

			// Token: 0x040002D3 RID: 723
			public byte bColorCount;

			// Token: 0x040002D4 RID: 724
			public byte bReserved;

			// Token: 0x040002D5 RID: 725
			public short wPlanes;

			// Token: 0x040002D6 RID: 726
			public short wBitCount;

			// Token: 0x040002D7 RID: 727
			public int dwBytesInRes;

			// Token: 0x040002D8 RID: 728
			public int dwImageOffset;
		}

		// Token: 0x0200002B RID: 43
		public class Ole
		{
			// Token: 0x060000D0 RID: 208 RVA: 0x00002050 File Offset: 0x00000250
			public Ole()
			{
			}

			// Token: 0x040002D9 RID: 729
			public const int PICTYPE_ICON = 3;
		}

		// Token: 0x0200002C RID: 44
		[StructLayout(LayoutKind.Sequential)]
		public class PICTDESC
		{
			// Token: 0x060000D1 RID: 209 RVA: 0x000050D7 File Offset: 0x000032D7
			public static SafeNativeMethods.PICTDESC CreateIconPICTDESC(IntPtr hicon)
			{
				return new SafeNativeMethods.PICTDESC
				{
					cbSizeOfStruct = 12,
					picType = 3,
					union1 = hicon
				};
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x00002050 File Offset: 0x00000250
			public PICTDESC()
			{
			}

			// Token: 0x040002DA RID: 730
			internal int cbSizeOfStruct;

			// Token: 0x040002DB RID: 731
			public int picType;

			// Token: 0x040002DC RID: 732
			internal IntPtr union1;

			// Token: 0x040002DD RID: 733
			internal int union2;

			// Token: 0x040002DE RID: 734
			internal int union3;
		}

		// Token: 0x0200002D RID: 45
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class DEVMODE
		{
			// Token: 0x060000D3 RID: 211 RVA: 0x000050F4 File Offset: 0x000032F4
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"[DEVMODE: dmDeviceName=",
					this.dmDeviceName,
					", dmSpecVersion=",
					this.dmSpecVersion.ToString(),
					", dmDriverVersion=",
					this.dmDriverVersion.ToString(),
					", dmSize=",
					this.dmSize.ToString(),
					", dmDriverExtra=",
					this.dmDriverExtra.ToString(),
					", dmFields=",
					this.dmFields.ToString(),
					", dmOrientation=",
					this.dmOrientation.ToString(),
					", dmPaperSize=",
					this.dmPaperSize.ToString(),
					", dmPaperLength=",
					this.dmPaperLength.ToString(),
					", dmPaperWidth=",
					this.dmPaperWidth.ToString(),
					", dmScale=",
					this.dmScale.ToString(),
					", dmCopies=",
					this.dmCopies.ToString(),
					", dmDefaultSource=",
					this.dmDefaultSource.ToString(),
					", dmPrintQuality=",
					this.dmPrintQuality.ToString(),
					", dmColor=",
					this.dmColor.ToString(),
					", dmDuplex=",
					this.dmDuplex.ToString(),
					", dmYResolution=",
					this.dmYResolution.ToString(),
					", dmTTOption=",
					this.dmTTOption.ToString(),
					", dmCollate=",
					this.dmCollate.ToString(),
					", dmFormName=",
					this.dmFormName,
					", dmLogPixels=",
					this.dmLogPixels.ToString(),
					", dmBitsPerPel=",
					this.dmBitsPerPel.ToString(),
					", dmPelsWidth=",
					this.dmPelsWidth.ToString(),
					", dmPelsHeight=",
					this.dmPelsHeight.ToString(),
					", dmDisplayFlags=",
					this.dmDisplayFlags.ToString(),
					", dmDisplayFrequency=",
					this.dmDisplayFrequency.ToString(),
					", dmICMMethod=",
					this.dmICMMethod.ToString(),
					", dmICMIntent=",
					this.dmICMIntent.ToString(),
					", dmMediaType=",
					this.dmMediaType.ToString(),
					", dmDitherType=",
					this.dmDitherType.ToString(),
					", dmICCManufacturer=",
					this.dmICCManufacturer.ToString(),
					", dmICCModel=",
					this.dmICCModel.ToString(),
					", dmPanningWidth=",
					this.dmPanningWidth.ToString(),
					", dmPanningHeight=",
					this.dmPanningHeight.ToString(),
					"]"
				});
			}

			// Token: 0x060000D4 RID: 212 RVA: 0x00002050 File Offset: 0x00000250
			public DEVMODE()
			{
			}

			// Token: 0x040002DF RID: 735
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmDeviceName;

			// Token: 0x040002E0 RID: 736
			public short dmSpecVersion;

			// Token: 0x040002E1 RID: 737
			public short dmDriverVersion;

			// Token: 0x040002E2 RID: 738
			public short dmSize;

			// Token: 0x040002E3 RID: 739
			public short dmDriverExtra;

			// Token: 0x040002E4 RID: 740
			public int dmFields;

			// Token: 0x040002E5 RID: 741
			public short dmOrientation;

			// Token: 0x040002E6 RID: 742
			public short dmPaperSize;

			// Token: 0x040002E7 RID: 743
			public short dmPaperLength;

			// Token: 0x040002E8 RID: 744
			public short dmPaperWidth;

			// Token: 0x040002E9 RID: 745
			public short dmScale;

			// Token: 0x040002EA RID: 746
			public short dmCopies;

			// Token: 0x040002EB RID: 747
			public short dmDefaultSource;

			// Token: 0x040002EC RID: 748
			public short dmPrintQuality;

			// Token: 0x040002ED RID: 749
			public short dmColor;

			// Token: 0x040002EE RID: 750
			public short dmDuplex;

			// Token: 0x040002EF RID: 751
			public short dmYResolution;

			// Token: 0x040002F0 RID: 752
			public short dmTTOption;

			// Token: 0x040002F1 RID: 753
			public short dmCollate;

			// Token: 0x040002F2 RID: 754
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmFormName;

			// Token: 0x040002F3 RID: 755
			public short dmLogPixels;

			// Token: 0x040002F4 RID: 756
			public int dmBitsPerPel;

			// Token: 0x040002F5 RID: 757
			public int dmPelsWidth;

			// Token: 0x040002F6 RID: 758
			public int dmPelsHeight;

			// Token: 0x040002F7 RID: 759
			public int dmDisplayFlags;

			// Token: 0x040002F8 RID: 760
			public int dmDisplayFrequency;

			// Token: 0x040002F9 RID: 761
			public int dmICMMethod;

			// Token: 0x040002FA RID: 762
			public int dmICMIntent;

			// Token: 0x040002FB RID: 763
			public int dmMediaType;

			// Token: 0x040002FC RID: 764
			public int dmDitherType;

			// Token: 0x040002FD RID: 765
			public int dmICCManufacturer;

			// Token: 0x040002FE RID: 766
			public int dmICCModel;

			// Token: 0x040002FF RID: 767
			public int dmPanningWidth;

			// Token: 0x04000300 RID: 768
			public int dmPanningHeight;
		}

		// Token: 0x0200002E RID: 46
		public sealed class CommonHandles
		{
			// Token: 0x060000D5 RID: 213 RVA: 0x00005434 File Offset: 0x00003634
			static CommonHandles()
			{
			}

			// Token: 0x060000D6 RID: 214 RVA: 0x00002050 File Offset: 0x00000250
			public CommonHandles()
			{
			}

			// Token: 0x04000301 RID: 769
			public static readonly int GDI = System.Internal.HandleCollector.RegisterType("GDI", 50, 500);

			// Token: 0x04000302 RID: 770
			public static readonly int HDC = System.Internal.HandleCollector.RegisterType("HDC", 100, 2);

			// Token: 0x04000303 RID: 771
			public static readonly int Icon = System.Internal.HandleCollector.RegisterType("Icon", 20, 500);

			// Token: 0x04000304 RID: 772
			public static readonly int Kernel = System.Internal.HandleCollector.RegisterType("Kernel", 0, 1000);
		}

		// Token: 0x0200002F RID: 47
		public class StreamConsts
		{
			// Token: 0x060000D7 RID: 215 RVA: 0x00002050 File Offset: 0x00000250
			public StreamConsts()
			{
			}

			// Token: 0x04000305 RID: 773
			public const int STREAM_SEEK_SET = 0;

			// Token: 0x04000306 RID: 774
			public const int STREAM_SEEK_CUR = 1;

			// Token: 0x04000307 RID: 775
			public const int STREAM_SEEK_END = 2;
		}

		// Token: 0x02000030 RID: 48
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("7BF80980-BF32-101A-8BBB-00AA00300CAB")]
		[ComImport]
		public interface IPicture
		{
			// Token: 0x060000D8 RID: 216
			IntPtr GetHandle();

			// Token: 0x060000D9 RID: 217
			IntPtr GetHPal();

			// Token: 0x060000DA RID: 218
			[return: MarshalAs(UnmanagedType.I2)]
			short GetPictureType();

			// Token: 0x060000DB RID: 219
			int GetWidth();

			// Token: 0x060000DC RID: 220
			int GetHeight();

			// Token: 0x060000DD RID: 221
			void Render();

			// Token: 0x060000DE RID: 222
			void SetHPal([In] IntPtr phpal);

			// Token: 0x060000DF RID: 223
			IntPtr GetCurDC();

			// Token: 0x060000E0 RID: 224
			void SelectPicture([In] IntPtr hdcIn, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] phdcOut, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] phbmpOut);

			// Token: 0x060000E1 RID: 225
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetKeepOriginalFormat();

			// Token: 0x060000E2 RID: 226
			void SetKeepOriginalFormat([MarshalAs(UnmanagedType.Bool)] [In] bool pfkeep);

			// Token: 0x060000E3 RID: 227
			void PictureChanged();

			// Token: 0x060000E4 RID: 228
			[PreserveSig]
			int SaveAsFile([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStream pstm, [In] int fSaveMemCopy, out int pcbSize);

			// Token: 0x060000E5 RID: 229
			int GetAttributes();

			// Token: 0x060000E6 RID: 230
			void SetHdc([In] IntPtr hdc);
		}
	}
}
