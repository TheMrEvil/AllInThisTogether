using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies the type of printing that code is allowed to do.</summary>
	// Token: 0x020000CB RID: 203
	public enum PrintingPermissionLevel
	{
		/// <summary>Provides full access to all printers.</summary>
		// Token: 0x04000715 RID: 1813
		AllPrinting = 3,
		/// <summary>Provides printing programmatically to the default printer, along with safe printing through semirestricted dialog box. <see cref="F:System.Drawing.Printing.PrintingPermissionLevel.DefaultPrinting" /> is a subset of <see cref="F:System.Drawing.Printing.PrintingPermissionLevel.AllPrinting" />.</summary>
		// Token: 0x04000716 RID: 1814
		DefaultPrinting = 2,
		/// <summary>Prevents access to printers. <see cref="F:System.Drawing.Printing.PrintingPermissionLevel.NoPrinting" /> is a subset of <see cref="F:System.Drawing.Printing.PrintingPermissionLevel.SafePrinting" />.</summary>
		// Token: 0x04000717 RID: 1815
		NoPrinting = 0,
		/// <summary>Provides printing only from a restricted dialog box. <see cref="F:System.Drawing.Printing.PrintingPermissionLevel.SafePrinting" /> is a subset of <see cref="F:System.Drawing.Printing.PrintingPermissionLevel.DefaultPrinting" />.</summary>
		// Token: 0x04000718 RID: 1816
		SafePrinting
	}
}
