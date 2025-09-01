using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Data.OleDb
{
	/// <summary>Represents a set of data commands and a database connection that are used to fill the <see cref="T:System.Data.DataSet" /> and update the data source.</summary>
	// Token: 0x02000161 RID: 353
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbDataAdapter : DbDataAdapter, IDataAdapter, IDbDataAdapter, ICloneable
	{
		/// <summary>Gets or sets an SQL statement or stored procedure for deleting records from the data set.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source that correspond to deleted rows in the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x00007EED File Offset: 0x000060ED
		public new OleDbCommand DeleteCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to insert new records into the data source.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source that correspond to new rows in the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x0600130F RID: 4879 RVA: 0x00007EED File Offset: 0x000060ED
		public new OleDbCommand InsertCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to select records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Fill(System.Data.DataSet)" /> to select records from data source for placement in the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x00007EED File Offset: 0x000060ED
		public new OleDbCommand SelectCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataAdapter.DeleteCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during an update to delete records in the data source for deleted rows in the data set.</returns>
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x00007EED File Offset: 0x000060ED
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataAdapter.InsertCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during an update to insert records from a data source for placement in the data set.</returns>
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x00007EED File Offset: 0x000060ED
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataAdapter.SelectCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during an update to select records from a data source for placement in the data set.</returns>
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x06001317 RID: 4887 RVA: 0x00007EED File Offset: 0x000060ED
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataAdapter.UpdateCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during an update to update records in the data source for modified rows in the data set.</returns>
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x06001319 RID: 4889 RVA: 0x00007EED File Offset: 0x000060ED
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to update records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source that correspond to modified rows in the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x0600131B RID: 4891 RVA: 0x00007EED File Offset: 0x000060ED
		public new OleDbCommand UpdateCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> class.</summary>
		// Token: 0x0600131C RID: 4892 RVA: 0x0005AC92 File Offset: 0x00058E92
		public OleDbDataAdapter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> class with the specified <see cref="T:System.Data.OleDb.OleDbCommand" /> as the <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" /> property.</summary>
		/// <param name="selectCommand">An <see cref="T:System.Data.OleDb.OleDbCommand" /> that is a SELECT statement or stored procedure, and is set as the <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" />.</param>
		// Token: 0x0600131D RID: 4893 RVA: 0x0005AC9A File Offset: 0x00058E9A
		public OleDbDataAdapter(OleDbCommand selectCommand)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> class with a <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" />.</summary>
		/// <param name="selectCommandText">A string that is an SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" />.</param>
		/// <param name="selectConnection">An <see cref="T:System.Data.OleDb.OleDbConnection" /> that represents the connection.</param>
		// Token: 0x0600131E RID: 4894 RVA: 0x0005AC9A File Offset: 0x00058E9A
		public OleDbDataAdapter(string selectCommandText, OleDbConnection selectConnection)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> class with a <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" />.</summary>
		/// <param name="selectCommandText">A string that is an SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" />.</param>
		/// <param name="selectConnectionString">The connection string.</param>
		// Token: 0x0600131F RID: 4895 RVA: 0x0005AC9A File Offset: 0x00058E9A
		public OleDbDataAdapter(string selectCommandText, string selectConnectionString)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataSet" /> to match those in an ADO <see langword="Recordset" /> or <see langword="Record" /> object using the specified <see cref="T:System.Data.DataSet" />, ADO object, and source table name.</summary>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if it is required, schema.</param>
		/// <param name="ADODBRecordSet">An ADO <see langword="Recordset" /> or <see langword="Record" /> object.</param>
		/// <param name="srcTable">The source table used for the table mappings.</param>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <exception cref="T:System.SystemException">The source table is invalid.</exception>
		// Token: 0x06001322 RID: 4898 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public int Fill(DataSet dataSet, object ADODBRecordSet, string srcTable)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds or refreshes rows in a <see cref="T:System.Data.DataTable" /> to match those in an ADO <see langword="Recordset" /> or <see langword="Record" /> object using the specified <see cref="T:System.Data.DataTable" /> and ADO objects.</summary>
		/// <param name="dataTable">A <see cref="T:System.Data.DataTable" /> to fill with records and, if it is required, schema.</param>
		/// <param name="ADODBRecordSet">An ADO <see langword="Recordset" /> or <see langword="Record" /> object.</param>
		/// <returns>The number of rows successfully refreshed to the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
		// Token: 0x06001323 RID: 4899 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public int Fill(DataTable dataTable, object ADODBRecordSet)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> after a command is executed against the data source. The attempt to update is made. Therefore, the event occurs.</summary>
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06001326 RID: 4902 RVA: 0x0005ACA8 File Offset: 0x00058EA8
		// (remove) Token: 0x06001327 RID: 4903 RVA: 0x0005ACE0 File Offset: 0x00058EE0
		public event OleDbRowUpdatedEventHandler RowUpdated
		{
			[CompilerGenerated]
			add
			{
				OleDbRowUpdatedEventHandler oleDbRowUpdatedEventHandler = this.RowUpdated;
				OleDbRowUpdatedEventHandler oleDbRowUpdatedEventHandler2;
				do
				{
					oleDbRowUpdatedEventHandler2 = oleDbRowUpdatedEventHandler;
					OleDbRowUpdatedEventHandler value2 = (OleDbRowUpdatedEventHandler)Delegate.Combine(oleDbRowUpdatedEventHandler2, value);
					oleDbRowUpdatedEventHandler = Interlocked.CompareExchange<OleDbRowUpdatedEventHandler>(ref this.RowUpdated, value2, oleDbRowUpdatedEventHandler2);
				}
				while (oleDbRowUpdatedEventHandler != oleDbRowUpdatedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				OleDbRowUpdatedEventHandler oleDbRowUpdatedEventHandler = this.RowUpdated;
				OleDbRowUpdatedEventHandler oleDbRowUpdatedEventHandler2;
				do
				{
					oleDbRowUpdatedEventHandler2 = oleDbRowUpdatedEventHandler;
					OleDbRowUpdatedEventHandler value2 = (OleDbRowUpdatedEventHandler)Delegate.Remove(oleDbRowUpdatedEventHandler2, value);
					oleDbRowUpdatedEventHandler = Interlocked.CompareExchange<OleDbRowUpdatedEventHandler>(ref this.RowUpdated, value2, oleDbRowUpdatedEventHandler2);
				}
				while (oleDbRowUpdatedEventHandler != oleDbRowUpdatedEventHandler2);
			}
		}

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> before a command is executed against the data source. The attempt to update is made. Therefore, the event occurs.</summary>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06001328 RID: 4904 RVA: 0x0005AD18 File Offset: 0x00058F18
		// (remove) Token: 0x06001329 RID: 4905 RVA: 0x0005AD50 File Offset: 0x00058F50
		public event OleDbRowUpdatingEventHandler RowUpdating
		{
			[CompilerGenerated]
			add
			{
				OleDbRowUpdatingEventHandler oleDbRowUpdatingEventHandler = this.RowUpdating;
				OleDbRowUpdatingEventHandler oleDbRowUpdatingEventHandler2;
				do
				{
					oleDbRowUpdatingEventHandler2 = oleDbRowUpdatingEventHandler;
					OleDbRowUpdatingEventHandler value2 = (OleDbRowUpdatingEventHandler)Delegate.Combine(oleDbRowUpdatingEventHandler2, value);
					oleDbRowUpdatingEventHandler = Interlocked.CompareExchange<OleDbRowUpdatingEventHandler>(ref this.RowUpdating, value2, oleDbRowUpdatingEventHandler2);
				}
				while (oleDbRowUpdatingEventHandler != oleDbRowUpdatingEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				OleDbRowUpdatingEventHandler oleDbRowUpdatingEventHandler = this.RowUpdating;
				OleDbRowUpdatingEventHandler oleDbRowUpdatingEventHandler2;
				do
				{
					oleDbRowUpdatingEventHandler2 = oleDbRowUpdatingEventHandler;
					OleDbRowUpdatingEventHandler value2 = (OleDbRowUpdatingEventHandler)Delegate.Remove(oleDbRowUpdatingEventHandler2, value);
					oleDbRowUpdatingEventHandler = Interlocked.CompareExchange<OleDbRowUpdatingEventHandler>(ref this.RowUpdating, value2, oleDbRowUpdatingEventHandler2);
				}
				while (oleDbRowUpdatingEventHandler != oleDbRowUpdatingEventHandler2);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x0600132A RID: 4906 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		object ICloneable.Clone()
		{
			throw ADP.OleDb();
		}

		// Token: 0x04000BD2 RID: 3026
		[CompilerGenerated]
		private OleDbRowUpdatedEventHandler RowUpdated;

		// Token: 0x04000BD3 RID: 3027
		[CompilerGenerated]
		private OleDbRowUpdatingEventHandler RowUpdating;
	}
}
