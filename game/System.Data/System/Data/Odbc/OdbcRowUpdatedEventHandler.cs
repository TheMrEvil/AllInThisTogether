using System;

namespace System.Data.Odbc
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.Odbc.OdbcDataAdapter.RowUpdated" /> event of an <see cref="T:System.Data.Odbc.OdbcDataAdapter" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="T:System.Data.Odbc.OdbcRowUpdatedEventArgs" /> that contains the event data.</param>
	// Token: 0x020002F7 RID: 759
	// (Invoke) Token: 0x060021D3 RID: 8659
	public delegate void OdbcRowUpdatedEventHandler(object sender, OdbcRowUpdatedEventArgs e);
}
