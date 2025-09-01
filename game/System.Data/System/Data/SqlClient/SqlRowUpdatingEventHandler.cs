using System;

namespace System.Data.SqlClient
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.SqlClient.SqlDataAdapter.RowUpdating" /> event of a <see cref="T:System.Data.SqlClient.SqlDataAdapter" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="T:System.Data.SqlClient.SqlRowUpdatingEventArgs" /> that contains the event data.</param>
	// Token: 0x02000224 RID: 548
	// (Invoke) Token: 0x06001A82 RID: 6786
	public delegate void SqlRowUpdatingEventHandler(object sender, SqlRowUpdatingEventArgs e);
}
