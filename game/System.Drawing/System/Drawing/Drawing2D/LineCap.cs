using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the available cap styles with which a <see cref="T:System.Drawing.Pen" /> object can end a line.</summary>
	// Token: 0x02000145 RID: 325
	public enum LineCap
	{
		/// <summary>Specifies a flat line cap.</summary>
		// Token: 0x04000B27 RID: 2855
		Flat,
		/// <summary>Specifies a square line cap.</summary>
		// Token: 0x04000B28 RID: 2856
		Square,
		/// <summary>Specifies a round line cap.</summary>
		// Token: 0x04000B29 RID: 2857
		Round,
		/// <summary>Specifies a triangular line cap.</summary>
		// Token: 0x04000B2A RID: 2858
		Triangle,
		/// <summary>Specifies no anchor.</summary>
		// Token: 0x04000B2B RID: 2859
		NoAnchor = 16,
		/// <summary>Specifies a square anchor line cap.</summary>
		// Token: 0x04000B2C RID: 2860
		SquareAnchor,
		/// <summary>Specifies a round anchor cap.</summary>
		// Token: 0x04000B2D RID: 2861
		RoundAnchor,
		/// <summary>Specifies a diamond anchor cap.</summary>
		// Token: 0x04000B2E RID: 2862
		DiamondAnchor,
		/// <summary>Specifies an arrow-shaped anchor cap.</summary>
		// Token: 0x04000B2F RID: 2863
		ArrowAnchor,
		/// <summary>Specifies a custom line cap.</summary>
		// Token: 0x04000B30 RID: 2864
		Custom = 255,
		/// <summary>Specifies a mask used to check whether a line cap is an anchor cap.</summary>
		// Token: 0x04000B31 RID: 2865
		AnchorMask = 240
	}
}
