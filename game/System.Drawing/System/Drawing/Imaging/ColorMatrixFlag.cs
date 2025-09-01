using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the types of images and colors that will be affected by the color and grayscale adjustment settings of an <see cref="T:System.Drawing.Imaging.ImageAttributes" />.</summary>
	// Token: 0x020000FB RID: 251
	public enum ColorMatrixFlag
	{
		/// <summary>All color values, including gray shades, are adjusted by the same color-adjustment matrix.</summary>
		// Token: 0x04000867 RID: 2151
		Default,
		/// <summary>All colors are adjusted, but gray shades are not adjusted. A gray shade is any color that has the same value for its red, green, and blue components.</summary>
		// Token: 0x04000868 RID: 2152
		SkipGrays,
		/// <summary>Only gray shades are adjusted.</summary>
		// Token: 0x04000869 RID: 2153
		AltGrays
	}
}
