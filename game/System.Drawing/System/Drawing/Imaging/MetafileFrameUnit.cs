using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the unit of measurement for the rectangle used to size and position a metafile. This is specified during the creation of the <see cref="T:System.Drawing.Imaging.Metafile" /> object.</summary>
	// Token: 0x0200010D RID: 269
	public enum MetafileFrameUnit
	{
		/// <summary>The unit of measurement is 1 pixel.</summary>
		// Token: 0x040009E2 RID: 2530
		Pixel = 2,
		/// <summary>The unit of measurement is 1 printer's point.</summary>
		// Token: 0x040009E3 RID: 2531
		Point,
		/// <summary>The unit of measurement is 1 inch.</summary>
		// Token: 0x040009E4 RID: 2532
		Inch,
		/// <summary>The unit of measurement is 1/300 of an inch.</summary>
		// Token: 0x040009E5 RID: 2533
		Document,
		/// <summary>The unit of measurement is 1 millimeter.</summary>
		// Token: 0x040009E6 RID: 2534
		Millimeter,
		/// <summary>The unit of measurement is 0.01 millimeter. Provided for compatibility with GDI.</summary>
		// Token: 0x040009E7 RID: 2535
		GdiCompatible
	}
}
