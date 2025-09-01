using System;

namespace System.Drawing
{
	/// <summary>Specifies style information applied to text.</summary>
	// Token: 0x0200001E RID: 30
	[Flags]
	public enum FontStyle
	{
		/// <summary>Normal text.</summary>
		// Token: 0x0400015C RID: 348
		Regular = 0,
		/// <summary>Bold text.</summary>
		// Token: 0x0400015D RID: 349
		Bold = 1,
		/// <summary>Italic text.</summary>
		// Token: 0x0400015E RID: 350
		Italic = 2,
		/// <summary>Underlined text.</summary>
		// Token: 0x0400015F RID: 351
		Underline = 4,
		/// <summary>Text with a line through the middle.</summary>
		// Token: 0x04000160 RID: 352
		Strikeout = 8
	}
}
