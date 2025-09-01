using System;

namespace System.Data
{
	/// <summary>Specifies the type of SQL query to be used by the <see cref="T:System.Data.OleDb.OleDbRowUpdatedEventArgs" />, <see cref="T:System.Data.OleDb.OleDbRowUpdatingEventArgs" />, <see cref="T:System.Data.SqlClient.SqlRowUpdatedEventArgs" />, or <see cref="T:System.Data.SqlClient.SqlRowUpdatingEventArgs" /> class.</summary>
	// Token: 0x02000135 RID: 309
	public enum StatementType
	{
		/// <summary>An SQL query that is a SELECT statement.</summary>
		// Token: 0x04000A46 RID: 2630
		Select,
		/// <summary>An SQL query that is an INSERT statement.</summary>
		// Token: 0x04000A47 RID: 2631
		Insert,
		/// <summary>An SQL query that is an UPDATE statement.</summary>
		// Token: 0x04000A48 RID: 2632
		Update,
		/// <summary>An SQL query that is a DELETE statement.</summary>
		// Token: 0x04000A49 RID: 2633
		Delete,
		/// <summary>A SQL query that is a batch statement.</summary>
		// Token: 0x04000A4A RID: 2634
		Batch
	}
}
