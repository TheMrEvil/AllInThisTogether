using System;

namespace System.ComponentModel.Design
{
	/// <summary>Represents the method that handles the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosed" /> and <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosing" /> events of a designer.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.Design.DesignerTransactionCloseEventArgs" /> that contains the event data.</param>
	// Token: 0x0200044A RID: 1098
	// (Invoke) Token: 0x060023BE RID: 9150
	public delegate void DesignerTransactionCloseEventHandler(object sender, DesignerTransactionCloseEventArgs e);
}
