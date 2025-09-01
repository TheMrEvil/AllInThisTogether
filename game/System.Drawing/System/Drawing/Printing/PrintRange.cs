using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies the part of the document to print.</summary>
	// Token: 0x020000C2 RID: 194
	public enum PrintRange
	{
		/// <summary>All pages are printed.</summary>
		// Token: 0x040006FE RID: 1790
		AllPages,
		/// <summary>The pages between <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> and <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> are printed.</summary>
		// Token: 0x040006FF RID: 1791
		SomePages = 2,
		/// <summary>The selected pages are printed.</summary>
		// Token: 0x04000700 RID: 1792
		Selection = 1,
		/// <summary>The currently displayed page is printed</summary>
		// Token: 0x04000701 RID: 1793
		CurrentPage = 4194304
	}
}
