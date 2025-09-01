using System;

namespace System.Data.SqlClient
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.SqlClient.SqlDataAdapter.RowUpdated" /> event of a <see cref="T:System.Data.SqlClient.SqlDataAdapter" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="T:System.Data.SqlClient.SqlRowUpdatedEventArgs" /> that contains the event data.</param>
	// Token: 0x02000222 RID: 546
	// (Invoke) Token: 0x06001A79 RID: 6777
	public delegate void SqlRowUpdatedEventHandler(object sender, SqlRowUpdatedEventArgs e);
}
