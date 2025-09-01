using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	/// <summary>Provides data for the <see cref="E:System.Data.SqlClient.SqlDataAdapter.RowUpdated" /> event.</summary>
	// Token: 0x02000221 RID: 545
	public sealed class SqlRowUpdatedEventArgs : RowUpdatedEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlRowUpdatedEventArgs" /> class.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> sent through an <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</param>
		/// <param name="statementType">One of the <see cref="T:System.Data.StatementType" /> values that specifies the type of query executed.</param>
		/// <param name="tableMapping">The <see cref="T:System.Data.Common.DataTableMapping" /> sent through an <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</param>
		// Token: 0x06001A76 RID: 6774 RVA: 0x0007A394 File Offset: 0x00078594
		public SqlRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(row, command, statementType, tableMapping)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlClient.SqlCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</returns>
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x0007A3A1 File Offset: 0x000785A1
		public new SqlCommand Command
		{
			get
			{
				return (SqlCommand)base.Command;
			}
		}
	}
}
