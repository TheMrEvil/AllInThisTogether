using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the attributes of the pixel data contained in an <see cref="T:System.Drawing.Image" /> object. The <see cref="P:System.Drawing.Image.Flags" /> property returns a member of this enumeration.</summary>
	// Token: 0x0200010B RID: 267
	[Flags]
	public enum ImageFlags
	{
		/// <summary>There is no format information.</summary>
		// Token: 0x040009CE RID: 2510
		None = 0,
		/// <summary>The pixel data is scalable.</summary>
		// Token: 0x040009CF RID: 2511
		Scalable = 1,
		/// <summary>The pixel data contains alpha information.</summary>
		// Token: 0x040009D0 RID: 2512
		HasAlpha = 2,
		/// <summary>Specifies that the pixel data has alpha values other than 0 (transparent) and 255 (opaque).</summary>
		// Token: 0x040009D1 RID: 2513
		HasTranslucent = 4,
		/// <summary>The pixel data is partially scalable, but there are some limitations.</summary>
		// Token: 0x040009D2 RID: 2514
		PartiallyScalable = 8,
		/// <summary>The pixel data uses an RGB color space.</summary>
		// Token: 0x040009D3 RID: 2515
		ColorSpaceRgb = 16,
		/// <summary>The pixel data uses a CMYK color space.</summary>
		// Token: 0x040009D4 RID: 2516
		ColorSpaceCmyk = 32,
		/// <summary>The pixel data is grayscale.</summary>
		// Token: 0x040009D5 RID: 2517
		ColorSpaceGray = 64,
		/// <summary>Specifies that the image is stored using a YCBCR color space.</summary>
		// Token: 0x040009D6 RID: 2518
		ColorSpaceYcbcr = 128,
		/// <summary>Specifies that the image is stored using a YCCK color space.</summary>
		// Token: 0x040009D7 RID: 2519
		ColorSpaceYcck = 256,
		/// <summary>Specifies that dots per inch information is stored in the image.</summary>
		// Token: 0x040009D8 RID: 2520
		HasRealDpi = 4096,
		/// <summary>Specifies that the pixel size is stored in the image.</summary>
		// Token: 0x040009D9 RID: 2521
		HasRealPixelSize = 8192,
		/// <summary>The pixel data is read-only.</summary>
		// Token: 0x040009DA RID: 2522
		ReadOnly = 65536,
		/// <summary>The pixel data can be cached for faster access.</summary>
		// Token: 0x040009DB RID: 2523
		Caching = 131072
	}
}
