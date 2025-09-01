using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies which GDI+ objects use color adjustment information.</summary>
	// Token: 0x020000F6 RID: 246
	public enum ColorAdjustType
	{
		/// <summary>Color adjustment information that is used by all GDI+ objects that do not have their own color adjustment information.</summary>
		// Token: 0x0400083B RID: 2107
		Default,
		/// <summary>Color adjustment information for <see cref="T:System.Drawing.Bitmap" /> objects.</summary>
		// Token: 0x0400083C RID: 2108
		Bitmap,
		/// <summary>Color adjustment information for <see cref="T:System.Drawing.Brush" /> objects.</summary>
		// Token: 0x0400083D RID: 2109
		Brush,
		/// <summary>Color adjustment information for <see cref="T:System.Drawing.Pen" /> objects.</summary>
		// Token: 0x0400083E RID: 2110
		Pen,
		/// <summary>Color adjustment information for text.</summary>
		// Token: 0x0400083F RID: 2111
		Text,
		/// <summary>The number of types specified.</summary>
		// Token: 0x04000840 RID: 2112
		Count,
		/// <summary>The number of types specified.</summary>
		// Token: 0x04000841 RID: 2113
		Any
	}
}
