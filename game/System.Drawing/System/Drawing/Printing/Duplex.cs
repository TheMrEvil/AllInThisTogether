using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies the printer's duplex setting.</summary>
	// Token: 0x020000B7 RID: 183
	public enum Duplex
	{
		/// <summary>The printer's default duplex setting.</summary>
		// Token: 0x0400065E RID: 1630
		Default = -1,
		/// <summary>Single-sided printing.</summary>
		// Token: 0x0400065F RID: 1631
		Simplex = 1,
		/// <summary>Double-sided, horizontal printing.</summary>
		// Token: 0x04000660 RID: 1632
		Horizontal = 3,
		/// <summary>Double-sided, vertical printing.</summary>
		// Token: 0x04000661 RID: 1633
		Vertical = 2
	}
}
