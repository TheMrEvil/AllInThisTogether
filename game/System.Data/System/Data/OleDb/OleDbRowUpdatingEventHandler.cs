using System;

namespace System.Data.OleDb
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.OleDb.OleDbDataAdapter.RowUpdating" /> event of an <see cref="T:System.Data.OleDb.OleDbDataAdapter" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="T:System.Data.OleDb.OleDbRowUpdatingEventArgs" /> that contains the event data.</param>
	// Token: 0x02000172 RID: 370
	// (Invoke) Token: 0x060013D1 RID: 5073
	public delegate void OleDbRowUpdatingEventHandler(object sender, OleDbRowUpdatingEventArgs e);
}
