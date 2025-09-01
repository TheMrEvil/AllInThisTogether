using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies how to join consecutive line or curve segments in a figure (subpath) contained in a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object.</summary>
	// Token: 0x02000146 RID: 326
	public enum LineJoin
	{
		/// <summary>Specifies a mitered join. This produces a sharp corner or a clipped corner, depending on whether the length of the miter exceeds the miter limit.</summary>
		// Token: 0x04000B33 RID: 2867
		Miter,
		/// <summary>Specifies a beveled join. This produces a diagonal corner.</summary>
		// Token: 0x04000B34 RID: 2868
		Bevel,
		/// <summary>Specifies a circular join. This produces a smooth, circular arc between the lines.</summary>
		// Token: 0x04000B35 RID: 2869
		Round,
		/// <summary>Specifies a mitered join. This produces a sharp corner or a beveled corner, depending on whether the length of the miter exceeds the miter limit.</summary>
		// Token: 0x04000B36 RID: 2870
		MiterClipped
	}
}
