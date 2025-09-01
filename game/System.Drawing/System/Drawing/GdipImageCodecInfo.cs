using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x020000A0 RID: 160
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct GdipImageCodecInfo
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x0001732C File Offset: 0x0001552C
		internal static void MarshalTo(GdipImageCodecInfo gdipcodec, ImageCodecInfo codec)
		{
			codec.CodecName = Marshal.PtrToStringUni(gdipcodec.CodecName);
			codec.DllName = Marshal.PtrToStringUni(gdipcodec.DllName);
			codec.FormatDescription = Marshal.PtrToStringUni(gdipcodec.FormatDescription);
			codec.FilenameExtension = Marshal.PtrToStringUni(gdipcodec.FilenameExtension);
			codec.MimeType = Marshal.PtrToStringUni(gdipcodec.MimeType);
			codec.Clsid = gdipcodec.Clsid;
			codec.FormatID = gdipcodec.FormatID;
			codec.Flags = gdipcodec.Flags;
			codec.Version = gdipcodec.Version;
			codec.SignatureMasks = new byte[gdipcodec.SigCount][];
			codec.SignaturePatterns = new byte[gdipcodec.SigCount][];
			IntPtr sigPattern = gdipcodec.SigPattern;
			IntPtr sigMask = gdipcodec.SigMask;
			for (int i = 0; i < gdipcodec.SigCount; i++)
			{
				codec.SignatureMasks[i] = new byte[gdipcodec.SigSize];
				Marshal.Copy(sigMask, codec.SignatureMasks[i], 0, gdipcodec.SigSize);
				sigMask = new IntPtr(sigMask.ToInt64() + (long)gdipcodec.SigSize);
				codec.SignaturePatterns[i] = new byte[gdipcodec.SigSize];
				Marshal.Copy(sigPattern, codec.SignaturePatterns[i], 0, gdipcodec.SigSize);
				sigPattern = new IntPtr(sigPattern.ToInt64() + (long)gdipcodec.SigSize);
			}
		}

		// Token: 0x04000608 RID: 1544
		internal Guid Clsid;

		// Token: 0x04000609 RID: 1545
		internal Guid FormatID;

		// Token: 0x0400060A RID: 1546
		internal IntPtr CodecName;

		// Token: 0x0400060B RID: 1547
		internal IntPtr DllName;

		// Token: 0x0400060C RID: 1548
		internal IntPtr FormatDescription;

		// Token: 0x0400060D RID: 1549
		internal IntPtr FilenameExtension;

		// Token: 0x0400060E RID: 1550
		internal IntPtr MimeType;

		// Token: 0x0400060F RID: 1551
		internal ImageCodecFlags Flags;

		// Token: 0x04000610 RID: 1552
		internal int Version;

		// Token: 0x04000611 RID: 1553
		internal int SigCount;

		// Token: 0x04000612 RID: 1554
		internal int SigSize;

		// Token: 0x04000613 RID: 1555
		private IntPtr SigPattern;

		// Token: 0x04000614 RID: 1556
		private IntPtr SigMask;
	}
}
