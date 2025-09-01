using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	/// <summary>Provides data for the <see cref="E:System.Data.Odbc.OdbcDataAdapter.RowUpdated" /> event.</summary>
	// Token: 0x020002F9 RID: 761
	public sealed class OdbcRowUpdatedEventArgs : RowUpdatedEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcRowUpdatedEventArgs" /> class.</summary>
		/// <param name="row">The <see langword="DataRow" /> sent through an update operation.</param>
		/// <param name="command">The <see cref="T:System.Data.Odbc.OdbcCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</param>
		/// <param name="statementType">One of the <see cref="T:System.Data.StatementType" /> values that specifies the type of query executed.</param>
		/// <param name="tableMapping">The <see cref="T:System.Data.Common.DataTableMapping" /> sent through <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</param>
		// Token: 0x060021DB RID: 8667 RVA: 0x0007A394 File Offset: 0x00078594
		public OdbcRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(row, command, statementType, tableMapping)
		{
		}

		/// <summary>Gets the <see cref="T:System.Data.Odbc.OdbcCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</summary>
		/// <returns>The <see cref="T:System.Data.Odbc.OdbcCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</returns>
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060021DC RID: 8668 RVA: 0x0009DCEE File Offset: 0x0009BEEE
		public new OdbcCommand Command
		{
			get
			{
				return (OdbcCommand)base.Command;
			}
		}
	}
}
