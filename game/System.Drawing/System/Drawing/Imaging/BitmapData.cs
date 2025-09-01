using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the attributes of a bitmap image. The <see cref="T:System.Drawing.Imaging.BitmapData" /> class is used by the <see cref="Overload:System.Drawing.Bitmap.LockBits" /> and <see cref="M:System.Drawing.Bitmap.UnlockBits(System.Drawing.Imaging.BitmapData)" /> methods of the <see cref="T:System.Drawing.Bitmap" /> class. Not inheritable.</summary>
	// Token: 0x02000117 RID: 279
	[StructLayout(LayoutKind.Sequential)]
	public sealed class BitmapData
	{
		/// <summary>Gets or sets the pixel height of the <see cref="T:System.Drawing.Bitmap" /> object. Also sometimes referred to as the number of scan lines.</summary>
		/// <returns>The pixel height of the <see cref="T:System.Drawing.Bitmap" /> object.</returns>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x0001DFAB File Offset: 0x0001C1AB
		// (set) Token: 0x06000CF2 RID: 3314 RVA: 0x0001DFB3 File Offset: 0x0001C1B3
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		/// <summary>Gets or sets the pixel width of the <see cref="T:System.Drawing.Bitmap" /> object. This can also be thought of as the number of pixels in one scan line.</summary>
		/// <returns>The pixel width of the <see cref="T:System.Drawing.Bitmap" /> object.</returns>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x0001DFBC File Offset: 0x0001C1BC
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x0001DFC4 File Offset: 0x0001C1C4
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		/// <summary>Gets or sets the format of the pixel information in the <see cref="T:System.Drawing.Bitmap" /> object that returned this <see cref="T:System.Drawing.Imaging.BitmapData" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Imaging.PixelFormat" /> that specifies the format of the pixel information in the associated <see cref="T:System.Drawing.Bitmap" /> object.</returns>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0001DFCD File Offset: 0x0001C1CD
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x0001DFD5 File Offset: 0x0001C1D5
		public PixelFormat PixelFormat
		{
			get
			{
				return this.pixel_format;
			}
			set
			{
				this.pixel_format = value;
			}
		}

		/// <summary>Reserved. Do not use.</summary>
		/// <returns>Reserved. Do not use.</returns>
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x0001DFDE File Offset: 0x0001C1DE
		// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x0001DFE6 File Offset: 0x0001C1E6
		public int Reserved
		{
			get
			{
				return this.reserved;
			}
			set
			{
				this.reserved = value;
			}
		}

		/// <summary>Gets or sets the address of the first pixel data in the bitmap. This can also be thought of as the first scan line in the bitmap.</summary>
		/// <returns>The address of the first pixel data in the bitmap.</returns>
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0001DFEF File Offset: 0x0001C1EF
		// (set) Token: 0x06000CFA RID: 3322 RVA: 0x0001DFF7 File Offset: 0x0001C1F7
		public IntPtr Scan0
		{
			get
			{
				return this.scan0;
			}
			set
			{
				this.scan0 = value;
			}
		}

		/// <summary>Gets or sets the stride width (also called scan width) of the <see cref="T:System.Drawing.Bitmap" /> object.</summary>
		/// <returns>The stride width, in bytes, of the <see cref="T:System.Drawing.Bitmap" /> object.</returns>
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x0001E000 File Offset: 0x0001C200
		// (set) Token: 0x06000CFC RID: 3324 RVA: 0x0001E008 File Offset: 0x0001C208
		public int Stride
		{
			get
			{
				return this.stride;
			}
			set
			{
				this.stride = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.BitmapData" /> class.</summary>
		// Token: 0x06000CFD RID: 3325 RVA: 0x00002050 File Offset: 0x00000250
		public BitmapData()
		{
		}

		// Token: 0x04000A48 RID: 2632
		private int width;

		// Token: 0x04000A49 RID: 2633
		private int height;

		// Token: 0x04000A4A RID: 2634
		private int stride;

		// Token: 0x04000A4B RID: 2635
		private PixelFormat pixel_format;

		// Token: 0x04000A4C RID: 2636
		private IntPtr scan0;

		// Token: 0x04000A4D RID: 2637
		private int reserved;

		// Token: 0x04000A4E RID: 2638
		private IntPtr palette;

		// Token: 0x04000A4F RID: 2639
		private int property_count;

		// Token: 0x04000A50 RID: 2640
		private IntPtr property;

		// Token: 0x04000A51 RID: 2641
		private float dpi_horz;

		// Token: 0x04000A52 RID: 2642
		private float dpi_vert;

		// Token: 0x04000A53 RID: 2643
		private int image_flags;

		// Token: 0x04000A54 RID: 2644
		private int left;

		// Token: 0x04000A55 RID: 2645
		private int top;

		// Token: 0x04000A56 RID: 2646
		private int x;

		// Token: 0x04000A57 RID: 2647
		private int y;

		// Token: 0x04000A58 RID: 2648
		private int transparent;
	}
}
