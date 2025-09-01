using System;

namespace System.Data.SqlClient
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.SqlClient.SqlConnection.InfoMessage" /> event of a <see cref="T:System.Data.SqlClient.SqlConnection" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Data.SqlClient.SqlInfoMessageEventArgs" /> object that contains the event data.</param>
	// Token: 0x02000204 RID: 516
	// (Invoke) Token: 0x06001906 RID: 6406
	public delegate void SqlInfoMessageEventHandler(object sender, SqlInfoMessageEventArgs e);
}
