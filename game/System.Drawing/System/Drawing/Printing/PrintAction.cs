using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies the type of print operation occurring.</summary>
	// Token: 0x020000BF RID: 191
	public enum PrintAction
	{
		/// <summary>The print operation is printing to a file.</summary>
		// Token: 0x040006FA RID: 1786
		PrintToFile,
		/// <summary>The print operation is a print preview.</summary>
		// Token: 0x040006FB RID: 1787
		PrintToPreview,
		/// <summary>The print operation is printing to a printer.</summary>
		// Token: 0x040006FC RID: 1788
		PrintToPrinter
	}
}
