using System;

namespace System.Drawing.Text
{
	/// <summary>Specifies the quality of text rendering.</summary>
	// Token: 0x020000B5 RID: 181
	public enum TextRenderingHint
	{
		/// <summary>Each character is drawn using its glyph bitmap, with the system default rendering hint. The text will be drawn using whatever font-smoothing settings the user has selected for the system.</summary>
		// Token: 0x04000657 RID: 1623
		SystemDefault,
		/// <summary>Each character is drawn using its glyph bitmap. Hinting is used to improve character appearance on stems and curvature.</summary>
		// Token: 0x04000658 RID: 1624
		SingleBitPerPixelGridFit,
		/// <summary>Each character is drawn using its glyph bitmap. Hinting is not used.</summary>
		// Token: 0x04000659 RID: 1625
		SingleBitPerPixel,
		/// <summary>Each character is drawn using its antialiased glyph bitmap with hinting. Much better quality due to antialiasing, but at a higher performance cost.</summary>
		// Token: 0x0400065A RID: 1626
		AntiAliasGridFit,
		/// <summary>Each character is drawn using its antialiased glyph bitmap without hinting. Better quality due to antialiasing. Stem width differences may be noticeable because hinting is turned off.</summary>
		// Token: 0x0400065B RID: 1627
		AntiAlias,
		/// <summary>Each character is drawn using its glyph ClearType bitmap with hinting. The highest quality setting. Used to take advantage of ClearType font features.</summary>
		// Token: 0x0400065C RID: 1628
		ClearTypeGridFit
	}
}
