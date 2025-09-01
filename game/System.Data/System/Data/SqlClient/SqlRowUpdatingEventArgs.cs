using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	/// <summary>Provides data for the <see cref="E:System.Data.SqlClient.SqlDataAdapter.RowUpdating" /> event.</summary>
	// Token: 0x02000223 RID: 547
	public sealed class SqlRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlRowUpdatingEventArgs" /> class.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> to execute during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</param>
		/// <param name="statementType">One of the <see cref="T:System.Data.StatementType" /> values that specifies the type of query executed.</param>
		/// <param name="tableMapping">The <see cref="T:System.Data.Common.DataTableMapping" /> sent through an <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</param>
		// Token: 0x06001A7C RID: 6780 RVA: 0x0007A3AE File Offset: 0x000785AE
		public SqlRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(row, command, statementType, tableMapping)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlClient.SqlCommand" /> to execute when performing the <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlCommand" /> to execute when performing the <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</returns>
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x0007A3BB File Offset: 0x000785BB
		// (set) Token: 0x06001A7E RID: 6782 RVA: 0x0007A3C8 File Offset: 0x000785C8
		public new SqlCommand Command
		{
			get
			{
				return base.Command as SqlCommand;
			}
			set
			{
				base.Command = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x0007A3D1 File Offset: 0x000785D1
		// (set) Token: 0x06001A80 RID: 6784 RVA: 0x0007A3D9 File Offset: 0x000785D9
		protected override IDbCommand BaseCommand
		{
			get
			{
				return base.BaseCommand;
			}
			set
			{
				base.BaseCommand = (value as SqlCommand);
			}
		}
	}
}
