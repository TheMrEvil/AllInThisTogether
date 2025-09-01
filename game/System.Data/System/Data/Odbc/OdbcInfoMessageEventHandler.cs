using System;

namespace System.Data.Odbc
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.Odbc.OdbcConnection.InfoMessage" /> event of an <see cref="T:System.Data.Odbc.OdbcConnection" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.Data.Odbc.OdbcInfoMessageEventArgs" /> object that contains the event data.</param>
	// Token: 0x020002ED RID: 749
	// (Invoke) Token: 0x06002128 RID: 8488
	public delegate void OdbcInfoMessageEventHandler(object sender, OdbcInfoMessageEventArgs e);
}
