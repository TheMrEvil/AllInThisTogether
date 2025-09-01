using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	/// <summary>Represents a set of data commands and a database connection that are used to fill the <see cref="T:System.Data.DataSet" /> and update a SQL Server database. This class cannot be inherited.</summary>
	// Token: 0x020001E4 RID: 484
	public sealed class SqlDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> class.</summary>
		// Token: 0x06001786 RID: 6022 RVA: 0x0006A6B1 File Offset: 0x000688B1
		public SqlDataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> class with the specified <see cref="T:System.Data.SqlClient.SqlCommand" /> as the <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> property.</summary>
		/// <param name="selectCommand">A <see cref="T:System.Data.SqlClient.SqlCommand" /> that is a Transact-SQL SELECT statement or stored procedure and is set as the <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" />.</param>
		// Token: 0x06001787 RID: 6023 RVA: 0x0006A6C6 File Offset: 0x000688C6
		public SqlDataAdapter(SqlCommand selectCommand) : this()
		{
			this.SelectCommand = selectCommand;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> class with a <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> and a connection string.</summary>
		/// <param name="selectCommandText">A <see cref="T:System.String" /> that is a Transact-SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" />.</param>
		/// <param name="selectConnectionString">The connection string. If your connection string does not use <see langword="Integrated Security = true" />, you can use <see cref="M:System.Data.SqlClient.SqlDataAdapter.#ctor(System.String,System.Data.SqlClient.SqlConnection)" /> and <see cref="T:System.Data.SqlClient.SqlCredential" /> to pass the user ID and password more securely than by specifying the user ID and password as text in the connection string.</param>
		// Token: 0x06001788 RID: 6024 RVA: 0x0006A6D8 File Offset: 0x000688D8
		public SqlDataAdapter(string selectCommandText, string selectConnectionString) : this()
		{
			SqlConnection connection = new SqlConnection(selectConnectionString);
			this.SelectCommand = new SqlCommand(selectCommandText, connection);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> class with a <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> and a <see cref="T:System.Data.SqlClient.SqlConnection" /> object.</summary>
		/// <param name="selectCommandText">A <see cref="T:System.String" /> that is a Transact-SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" />.</param>
		/// <param name="selectConnection">A <see cref="T:System.Data.SqlClient.SqlConnection" /> that represents the connection. If your connection string does not use <see langword="Integrated Security = true" />, you can use <see cref="T:System.Data.SqlClient.SqlCredential" /> to pass the user ID and password more securely than by specifying the user ID and password as text in the connection string.</param>
		// Token: 0x06001789 RID: 6025 RVA: 0x0006A6FF File Offset: 0x000688FF
		public SqlDataAdapter(string selectCommandText, SqlConnection selectConnection) : this()
		{
			this.SelectCommand = new SqlCommand(selectCommandText, selectConnection);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0006A714 File Offset: 0x00068914
		private SqlDataAdapter(SqlDataAdapter from) : base(from)
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets or sets a Transact-SQL statement or stored procedure to delete records from the data set.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the database that correspond to deleted rows in the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x0006A72A File Offset: 0x0006892A
		// (set) Token: 0x0600178C RID: 6028 RVA: 0x0006A732 File Offset: 0x00068932
		public new SqlCommand DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataAdapter.DeleteCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source for deleted rows in the data set.</returns>
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x0006A72A File Offset: 0x0006892A
		// (set) Token: 0x0600178E RID: 6030 RVA: 0x0006A73B File Offset: 0x0006893B
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = (SqlCommand)value;
			}
		}

		/// <summary>Gets or sets a Transact-SQL statement or stored procedure to insert new records into the data source.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records into the database that correspond to new rows in the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x0006A749 File Offset: 0x00068949
		// (set) Token: 0x06001790 RID: 6032 RVA: 0x0006A751 File Offset: 0x00068951
		public new SqlCommand InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataAdapter.InsertCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source for new rows in the data set.</returns>
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x0006A749 File Offset: 0x00068949
		// (set) Token: 0x06001792 RID: 6034 RVA: 0x0006A75A File Offset: 0x0006895A
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = (SqlCommand)value;
			}
		}

		/// <summary>Gets or sets a Transact-SQL statement or stored procedure used to select records in the data source.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Fill(System.Data.DataSet)" /> to select records from the database for placement in the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x0006A768 File Offset: 0x00068968
		// (set) Token: 0x06001794 RID: 6036 RVA: 0x0006A770 File Offset: 0x00068970
		public new SqlCommand SelectCommand
		{
			get
			{
				return this._selectCommand;
			}
			set
			{
				this._selectCommand = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataAdapter.SelectCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to select records from data source for placement in the data set.</returns>
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x0006A768 File Offset: 0x00068968
		// (set) Token: 0x06001796 RID: 6038 RVA: 0x0006A779 File Offset: 0x00068979
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this._selectCommand;
			}
			set
			{
				this._selectCommand = (SqlCommand)value;
			}
		}

		/// <summary>Gets or sets a Transact-SQL statement or stored procedure used to update records in the data source.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the database that correspond to modified rows in the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x0006A787 File Offset: 0x00068987
		// (set) Token: 0x06001798 RID: 6040 RVA: 0x0006A78F File Offset: 0x0006898F
		public new SqlCommand UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataAdapter.UpdateCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source for modified rows in the data set.</returns>
		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x0006A787 File Offset: 0x00068987
		// (set) Token: 0x0600179A RID: 6042 RVA: 0x0006A798 File Offset: 0x00068998
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = (SqlCommand)value;
			}
		}

		/// <summary>Gets or sets the number of rows that are processed in each round-trip to the server.</summary>
		/// <returns>The number of rows to process per-batch.  
		///   Value is  
		///
		///   Effect  
		///
		///   0  
		///
		///   There is no limit on the batch size. 
		///
		///   1  
		///
		///   Disables batch updating.  
		///
		///   &gt;1  
		///
		///   Changes are sent using batches of <see cref="P:System.Data.SqlClient.SqlDataAdapter.UpdateBatchSize" /> operations at a time.  
		///
		///
		///
		///  When setting this to a value other than 1, all the commands associated with the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> have to have their UpdatedRowSource property set to <see langword="None" /> or <see langword="OutputParameters" />. An exception is thrown otherwise.</returns>
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x0006A7A6 File Offset: 0x000689A6
		// (set) Token: 0x0600179C RID: 6044 RVA: 0x0006A7AE File Offset: 0x000689AE
		public override int UpdateBatchSize
		{
			get
			{
				return this._updateBatchSize;
			}
			set
			{
				if (0 > value)
				{
					throw ADP.ArgumentOutOfRange("UpdateBatchSize");
				}
				this._updateBatchSize = value;
			}
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0006A7C6 File Offset: 0x000689C6
		protected override int AddToBatch(IDbCommand command)
		{
			int commandCount = this._commandSet.CommandCount;
			this._commandSet.Append((SqlCommand)command);
			return commandCount;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0006A7E4 File Offset: 0x000689E4
		protected override void ClearBatch()
		{
			this._commandSet.Clear();
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0006A7F1 File Offset: 0x000689F1
		protected override int ExecuteBatch()
		{
			return this._commandSet.ExecuteNonQuery();
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0006A7FE File Offset: 0x000689FE
		protected override IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex)
		{
			return this._commandSet.GetParameter(commandIdentifier, parameterIndex);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0006A80D File Offset: 0x00068A0D
		protected override bool GetBatchedRecordsAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			return this._commandSet.GetBatchedAffected(commandIdentifier, out recordsAffected, out error);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0006A820 File Offset: 0x00068A20
		protected override void InitializeBatching()
		{
			this._commandSet = new SqlCommandSet();
			SqlCommand sqlCommand = this.SelectCommand;
			if (sqlCommand == null)
			{
				sqlCommand = this.InsertCommand;
				if (sqlCommand == null)
				{
					sqlCommand = this.UpdateCommand;
					if (sqlCommand == null)
					{
						sqlCommand = this.DeleteCommand;
					}
				}
			}
			if (sqlCommand != null)
			{
				this._commandSet.Connection = sqlCommand.Connection;
				this._commandSet.Transaction = sqlCommand.Transaction;
				this._commandSet.CommandTimeout = sqlCommand.CommandTimeout;
			}
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0006A893 File Offset: 0x00068A93
		protected override void TerminateBatching()
		{
			if (this._commandSet != null)
			{
				this._commandSet.Dispose();
				this._commandSet = null;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new object that is a copy of the current instance.</returns>
		// Token: 0x060017A4 RID: 6052 RVA: 0x0006A8AF File Offset: 0x00068AAF
		object ICloneable.Clone()
		{
			return new SqlDataAdapter(this);
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0006A8B7 File Offset: 0x00068AB7
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new SqlRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0006A8C3 File Offset: 0x00068AC3
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new SqlRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> after a command is executed against the data source. The attempt to update is made, so the event fires.</summary>
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060017A7 RID: 6055 RVA: 0x0006A8CF File Offset: 0x00068ACF
		// (remove) Token: 0x060017A8 RID: 6056 RVA: 0x0006A8E2 File Offset: 0x00068AE2
		public event SqlRowUpdatedEventHandler RowUpdated
		{
			add
			{
				base.Events.AddHandler(SqlDataAdapter.EventRowUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataAdapter.EventRowUpdated, value);
			}
		}

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> before a command is executed against the data source. The attempt to update is made, so the event fires.</summary>
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060017A9 RID: 6057 RVA: 0x0006A8F8 File Offset: 0x00068AF8
		// (remove) Token: 0x060017AA RID: 6058 RVA: 0x0006A95C File Offset: 0x00068B5C
		public event SqlRowUpdatingEventHandler RowUpdating
		{
			add
			{
				SqlRowUpdatingEventHandler sqlRowUpdatingEventHandler = (SqlRowUpdatingEventHandler)base.Events[SqlDataAdapter.EventRowUpdating];
				if (sqlRowUpdatingEventHandler != null && value.Target is DbCommandBuilder)
				{
					SqlRowUpdatingEventHandler sqlRowUpdatingEventHandler2 = (SqlRowUpdatingEventHandler)ADP.FindBuilder(sqlRowUpdatingEventHandler);
					if (sqlRowUpdatingEventHandler2 != null)
					{
						base.Events.RemoveHandler(SqlDataAdapter.EventRowUpdating, sqlRowUpdatingEventHandler2);
					}
				}
				base.Events.AddHandler(SqlDataAdapter.EventRowUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataAdapter.EventRowUpdating, value);
			}
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0006A970 File Offset: 0x00068B70
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			SqlRowUpdatedEventHandler sqlRowUpdatedEventHandler = (SqlRowUpdatedEventHandler)base.Events[SqlDataAdapter.EventRowUpdated];
			if (sqlRowUpdatedEventHandler != null && value is SqlRowUpdatedEventArgs)
			{
				sqlRowUpdatedEventHandler(this, (SqlRowUpdatedEventArgs)value);
			}
			base.OnRowUpdated(value);
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0006A9B4 File Offset: 0x00068BB4
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			SqlRowUpdatingEventHandler sqlRowUpdatingEventHandler = (SqlRowUpdatingEventHandler)base.Events[SqlDataAdapter.EventRowUpdating];
			if (sqlRowUpdatingEventHandler != null && value is SqlRowUpdatingEventArgs)
			{
				sqlRowUpdatingEventHandler(this, (SqlRowUpdatingEventArgs)value);
			}
			base.OnRowUpdating(value);
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0006A9F6 File Offset: 0x00068BF6
		// Note: this type is marked as 'beforefieldinit'.
		static SqlDataAdapter()
		{
		}

		// Token: 0x04000F2C RID: 3884
		private static readonly object EventRowUpdated = new object();

		// Token: 0x04000F2D RID: 3885
		private static readonly object EventRowUpdating = new object();

		// Token: 0x04000F2E RID: 3886
		private SqlCommand _deleteCommand;

		// Token: 0x04000F2F RID: 3887
		private SqlCommand _insertCommand;

		// Token: 0x04000F30 RID: 3888
		private SqlCommand _selectCommand;

		// Token: 0x04000F31 RID: 3889
		private SqlCommand _updateCommand;

		// Token: 0x04000F32 RID: 3890
		private SqlCommandSet _commandSet;

		// Token: 0x04000F33 RID: 3891
		private int _updateBatchSize = 1;
	}
}
