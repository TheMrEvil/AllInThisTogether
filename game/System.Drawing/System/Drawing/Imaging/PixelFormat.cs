using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the format of the color data for each pixel in the image.</summary>
	// Token: 0x02000112 RID: 274
	public enum PixelFormat
	{
		/// <summary>The pixel data contains color-indexed values, which means the values are an index to colors in the system color table, as opposed to individual color values.</summary>
		// Token: 0x04000A20 RID: 2592
		Indexed = 65536,
		/// <summary>The pixel data contains GDI colors.</summary>
		// Token: 0x04000A21 RID: 2593
		Gdi = 131072,
		/// <summary>The pixel data contains alpha values that are not premultiplied.</summary>
		// Token: 0x04000A22 RID: 2594
		Alpha = 262144,
		/// <summary>The pixel format contains premultiplied alpha values.</summary>
		// Token: 0x04000A23 RID: 2595
		PAlpha = 524288,
		/// <summary>Reserved.</summary>
		// Token: 0x04000A24 RID: 2596
		Extended = 1048576,
		/// <summary>The default pixel format of 32 bits per pixel. The format specifies 24-bit color depth and an 8-bit alpha channel.</summary>
		// Token: 0x04000A25 RID: 2597
		Canonical = 2097152,
		/// <summary>The pixel format is undefined.</summary>
		// Token: 0x04000A26 RID: 2598
		Undefined = 0,
		/// <summary>No pixel format is specified.</summary>
		// Token: 0x04000A27 RID: 2599
		DontCare = 0,
		/// <summary>Specifies that the pixel format is 1 bit per pixel and that it uses indexed color. The color table therefore has two colors in it.</summary>
		// Token: 0x04000A28 RID: 2600
		Format1bppIndexed = 196865,
		/// <summary>Specifies that the format is 4 bits per pixel, indexed.</summary>
		// Token: 0x04000A29 RID: 2601
		Format4bppIndexed = 197634,
		/// <summary>Specifies that the format is 8 bits per pixel, indexed. The color table therefore has 256 colors in it.</summary>
		// Token: 0x04000A2A RID: 2602
		Format8bppIndexed = 198659,
		/// <summary>The pixel format is 16 bits per pixel. The color information specifies 65536 shades of gray.</summary>
		// Token: 0x04000A2B RID: 2603
		Format16bppGrayScale = 1052676,
		/// <summary>Specifies that the format is 16 bits per pixel; 5 bits each are used for the red, green, and blue components. The remaining bit is not used.</summary>
		// Token: 0x04000A2C RID: 2604
		Format16bppRgb555 = 135173,
		/// <summary>Specifies that the format is 16 bits per pixel; 5 bits are used for the red component, 6 bits are used for the green component, and 5 bits are used for the blue component.</summary>
		// Token: 0x04000A2D RID: 2605
		Format16bppRgb565,
		/// <summary>The pixel format is 16 bits per pixel. The color information specifies 32,768 shades of color, of which 5 bits are red, 5 bits are green, 5 bits are blue, and 1 bit is alpha.</summary>
		// Token: 0x04000A2E RID: 2606
		Format16bppArgb1555 = 397319,
		/// <summary>Specifies that the format is 24 bits per pixel; 8 bits each are used for the red, green, and blue components.</summary>
		// Token: 0x04000A2F RID: 2607
		Format24bppRgb = 137224,
		/// <summary>Specifies that the format is 32 bits per pixel; 8 bits each are used for the red, green, and blue components. The remaining 8 bits are not used.</summary>
		// Token: 0x04000A30 RID: 2608
		Format32bppRgb = 139273,
		/// <summary>Specifies that the format is 32 bits per pixel; 8 bits each are used for the alpha, red, green, and blue components.</summary>
		// Token: 0x04000A31 RID: 2609
		Format32bppArgb = 2498570,
		/// <summary>Specifies that the format is 32 bits per pixel; 8 bits each are used for the alpha, red, green, and blue components. The red, green, and blue components are premultiplied, according to the alpha component.</summary>
		// Token: 0x04000A32 RID: 2610
		Format32bppPArgb = 925707,
		/// <summary>Specifies that the format is 48 bits per pixel; 16 bits each are used for the red, green, and blue components.</summary>
		// Token: 0x04000A33 RID: 2611
		Format48bppRgb = 1060876,
		/// <summary>Specifies that the format is 64 bits per pixel; 16 bits each are used for the alpha, red, green, and blue components.</summary>
		// Token: 0x04000A34 RID: 2612
		Format64bppArgb = 3424269,
		/// <summary>Specifies that the format is 64 bits per pixel; 16 bits each are used for the alpha, red, green, and blue components. The red, green, and blue components are premultiplied according to the alpha component.</summary>
		// Token: 0x04000A35 RID: 2613
		Format64bppPArgb = 1851406,
		/// <summary>The maximum value for this enumeration.</summary>
		// Token: 0x04000A36 RID: 2614
		Max = 15
	}
}
