using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x0200010A RID: 266
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal class ImageCodecInfoPrivate
	{
		// Token: 0x06000CC7 RID: 3271 RVA: 0x0001DC54 File Offset: 0x0001BE54
		public ImageCodecInfoPrivate()
		{
		}

		// Token: 0x040009C0 RID: 2496
		[MarshalAs(UnmanagedType.Struct)]
		public Guid Clsid;

		// Token: 0x040009C1 RID: 2497
		[MarshalAs(UnmanagedType.Struct)]
		public Guid FormatID;

		// Token: 0x040009C2 RID: 2498
		public IntPtr CodecName = IntPtr.Zero;

		// Token: 0x040009C3 RID: 2499
		public IntPtr DllName = IntPtr.Zero;

		// Token: 0x040009C4 RID: 2500
		public IntPtr FormatDescription = IntPtr.Zero;

		// Token: 0x040009C5 RID: 2501
		public IntPtr FilenameExtension = IntPtr.Zero;

		// Token: 0x040009C6 RID: 2502
		public IntPtr MimeType = IntPtr.Zero;

		// Token: 0x040009C7 RID: 2503
		public int Flags;

		// Token: 0x040009C8 RID: 2504
		public int Version;

		// Token: 0x040009C9 RID: 2505
		public int SigCount;

		// Token: 0x040009CA RID: 2506
		public int SigSize;

		// Token: 0x040009CB RID: 2507
		public IntPtr SigPattern = IntPtr.Zero;

		// Token: 0x040009CC RID: 2508
		public IntPtr SigMask = IntPtr.Zero;
	}
}
