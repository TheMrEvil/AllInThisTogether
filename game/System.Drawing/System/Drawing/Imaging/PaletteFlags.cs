using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the type of color data in the system palette. The data can be color data with alpha, grayscale data only, or halftone data.</summary>
	// Token: 0x02000111 RID: 273
	[Flags]
	public enum PaletteFlags
	{
		/// <summary>Alpha data.</summary>
		// Token: 0x04000A1C RID: 2588
		HasAlpha = 1,
		/// <summary>Grayscale data.</summary>
		// Token: 0x04000A1D RID: 2589
		GrayScale = 2,
		/// <summary>Halftone data.</summary>
		// Token: 0x04000A1E RID: 2590
		Halftone = 4
	}
}
