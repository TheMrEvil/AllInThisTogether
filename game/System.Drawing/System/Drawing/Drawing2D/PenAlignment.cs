using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the alignment of a <see cref="T:System.Drawing.Pen" /> object in relation to the theoretical, zero-width line.</summary>
	// Token: 0x0200014B RID: 331
	public enum PenAlignment
	{
		/// <summary>Specifies that the <see cref="T:System.Drawing.Pen" /> object is centered over the theoretical line.</summary>
		// Token: 0x04000B4B RID: 2891
		Center,
		/// <summary>Specifies that the <see cref="T:System.Drawing.Pen" /> is positioned on the inside of the theoretical line.</summary>
		// Token: 0x04000B4C RID: 2892
		Inset,
		/// <summary>Specifies the <see cref="T:System.Drawing.Pen" /> is positioned on the outside of the theoretical line.</summary>
		// Token: 0x04000B4D RID: 2893
		Outset,
		/// <summary>Specifies the <see cref="T:System.Drawing.Pen" /> is positioned to the left of the theoretical line.</summary>
		// Token: 0x04000B4E RID: 2894
		Left,
		/// <summary>Specifies the <see cref="T:System.Drawing.Pen" /> is positioned to the right of the theoretical line.</summary>
		// Token: 0x04000B4F RID: 2895
		Right
	}
}
