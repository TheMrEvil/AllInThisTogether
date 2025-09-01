using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>The <see cref="T:System.Drawing.Drawing2D.InterpolationMode" /> enumeration specifies the algorithm that is used when images are scaled or rotated.</summary>
	// Token: 0x02000144 RID: 324
	public enum InterpolationMode
	{
		/// <summary>Equivalent to the <see cref="F:System.Drawing.Drawing2D.QualityMode.Invalid" /> element of the <see cref="T:System.Drawing.Drawing2D.QualityMode" /> enumeration.</summary>
		// Token: 0x04000B1D RID: 2845
		Invalid = -1,
		/// <summary>Specifies default mode.</summary>
		// Token: 0x04000B1E RID: 2846
		Default,
		/// <summary>Specifies low quality interpolation.</summary>
		// Token: 0x04000B1F RID: 2847
		Low,
		/// <summary>Specifies high quality interpolation.</summary>
		// Token: 0x04000B20 RID: 2848
		High,
		/// <summary>Specifies bilinear interpolation. No prefiltering is done. This mode is not suitable for shrinking an image below 50 percent of its original size.</summary>
		// Token: 0x04000B21 RID: 2849
		Bilinear,
		/// <summary>Specifies bicubic interpolation. No prefiltering is done. This mode is not suitable for shrinking an image below 25 percent of its original size.</summary>
		// Token: 0x04000B22 RID: 2850
		Bicubic,
		/// <summary>Specifies nearest-neighbor interpolation.</summary>
		// Token: 0x04000B23 RID: 2851
		NearestNeighbor,
		/// <summary>Specifies high-quality, bilinear interpolation. Prefiltering is performed to ensure high-quality shrinking.</summary>
		// Token: 0x04000B24 RID: 2852
		HighQualityBilinear,
		/// <summary>Specifies high-quality, bicubic interpolation. Prefiltering is performed to ensure high-quality shrinking. This mode produces the highest quality transformed images.</summary>
		// Token: 0x04000B25 RID: 2853
		HighQualityBicubic
	}
}
