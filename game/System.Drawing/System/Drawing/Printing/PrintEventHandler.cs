using System;

namespace System.Drawing.Printing
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Drawing.Printing.PrintDocument.BeginPrint" /> or <see cref="E:System.Drawing.Printing.PrintDocument.EndPrint" /> event of a <see cref="T:System.Drawing.Printing.PrintDocument" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data.</param>
	// Token: 0x020000C0 RID: 192
	// (Invoke) Token: 0x06000AA5 RID: 2725
	public delegate void PrintEventHandler(object sender, PrintEventArgs e);
}
