using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the style of dashed lines drawn with a <see cref="T:System.Drawing.Pen" /> object.</summary>
	// Token: 0x0200013D RID: 317
	public enum DashStyle
	{
		/// <summary>Specifies a solid line.</summary>
		// Token: 0x04000AD5 RID: 2773
		Solid,
		/// <summary>Specifies a line consisting of dashes.</summary>
		// Token: 0x04000AD6 RID: 2774
		Dash,
		/// <summary>Specifies a line consisting of dots.</summary>
		// Token: 0x04000AD7 RID: 2775
		Dot,
		/// <summary>Specifies a line consisting of a repeating pattern of dash-dot.</summary>
		// Token: 0x04000AD8 RID: 2776
		DashDot,
		/// <summary>Specifies a line consisting of a repeating pattern of dash-dot-dot.</summary>
		// Token: 0x04000AD9 RID: 2777
		DashDotDot,
		/// <summary>Specifies a user-defined custom dash style.</summary>
		// Token: 0x04000ADA RID: 2778
		Custom
	}
}
