using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	/// <summary>Represents a set of data commands and a connection to a data source that are used to fill the <see cref="T:System.Data.DataSet" /> and update the data source. This class cannot be inherited.</summary>
	// Token: 0x020002E0 RID: 736
	public sealed class OdbcDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> class.</summary>
		// Token: 0x06002071 RID: 8305 RVA: 0x000970DE File Offset: 0x000952DE
		public OdbcDataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> class with the specified SQL SELECT statement.</summary>
		/// <param name="selectCommand">An <see cref="T:System.Data.Odbc.OdbcCommand" /> that is an SQL SELECT statement or stored procedure, and is set as the <see cref="P:System.Data.Odbc.OdbcDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" />.</param>
		// Token: 0x06002072 RID: 8306 RVA: 0x000970EC File Offset: 0x000952EC
		public OdbcDataAdapter(OdbcCommand selectCommand) : this()
		{
			this.SelectCommand = selectCommand;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> class with an SQL SELECT statement and an <see cref="T:System.Data.Odbc.OdbcConnection" />.</summary>
		/// <param name="selectCommandText">A string that is a SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.Odbc.OdbcDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" />.</param>
		/// <param name="selectConnection">An <see cref="T:System.Data.Odbc.OdbcConnection" /> that represents the connection.</param>
		// Token: 0x06002073 RID: 8307 RVA: 0x000970FB File Offset: 0x000952FB
		public OdbcDataAdapter(string selectCommandText, OdbcConnection selectConnection) : this()
		{
			this.SelectCommand = new OdbcCommand(selectCommandText, selectConnection);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> class with an SQL SELECT statement and a connection string.</summary>
		/// <param name="selectCommandText">A string that is a SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.Odbc.OdbcDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" />.</param>
		/// <param name="selectConnectionString">The connection string.</param>
		// Token: 0x06002074 RID: 8308 RVA: 0x00097110 File Offset: 0x00095310
		public OdbcDataAdapter(string selectCommandText, string selectConnectionString) : this()
		{
			OdbcConnection connection = new OdbcConnection(selectConnectionString);
			this.SelectCommand = new OdbcCommand(selectCommandText, connection);
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x00097137 File Offset: 0x00095337
		private OdbcDataAdapter(OdbcDataAdapter from) : base(from)
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to delete records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> used during an update operation to delete records in the data source that correspond to deleted rows in the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x00097146 File Offset: 0x00095346
		// (set) Token: 0x06002077 RID: 8311 RVA: 0x0009714E File Offset: 0x0009534E
		public new OdbcCommand DeleteCommand
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
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during an update to delete records in the data source for deleted rows in the data set.</returns>
		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x00097146 File Offset: 0x00095346
		// (set) Token: 0x06002079 RID: 8313 RVA: 0x00097157 File Offset: 0x00095357
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = (OdbcCommand)value;
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to insert new records into the data source.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> used during an update operation to insert records in the data source that correspond to new rows in the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x00097165 File Offset: 0x00095365
		// (set) Token: 0x0600207B RID: 8315 RVA: 0x0009716D File Offset: 0x0009536D
		public new OdbcCommand InsertCommand
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
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during an update to insert records from a data source for placement in the data set.</returns>
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x00097165 File Offset: 0x00095365
		// (set) Token: 0x0600207D RID: 8317 RVA: 0x00097176 File Offset: 0x00095376
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = (OdbcCommand)value;
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to select records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> that is used during a fill operation to select records from data source for placement in the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x00097184 File Offset: 0x00095384
		// (set) Token: 0x0600207F RID: 8319 RVA: 0x0009718C File Offset: 0x0009538C
		public new OdbcCommand SelectCommand
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
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during an update to select records from a data source for placement in the data set.</returns>
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06002080 RID: 8320 RVA: 0x00097184 File Offset: 0x00095384
		// (set) Token: 0x06002081 RID: 8321 RVA: 0x00097195 File Offset: 0x00095395
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this._selectCommand;
			}
			set
			{
				this._selectCommand = (OdbcCommand)value;
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to update records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> used during an update operation to update records in the data source that correspond to modified rows in the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06002082 RID: 8322 RVA: 0x000971A3 File Offset: 0x000953A3
		// (set) Token: 0x06002083 RID: 8323 RVA: 0x000971AB File Offset: 0x000953AB
		public new OdbcCommand UpdateCommand
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
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during an update to update records in the data source for modified rows in the data set.</returns>
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06002084 RID: 8324 RVA: 0x000971A3 File Offset: 0x000953A3
		// (set) Token: 0x06002085 RID: 8325 RVA: 0x000971B4 File Offset: 0x000953B4
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = (OdbcCommand)value;
			}
		}

		/// <summary>Occurs during an update operation after a command is executed against the data source.</summary>
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06002086 RID: 8326 RVA: 0x000971C2 File Offset: 0x000953C2
		// (remove) Token: 0x06002087 RID: 8327 RVA: 0x000971D5 File Offset: 0x000953D5
		public event OdbcRowUpdatedEventHandler RowUpdated
		{
			add
			{
				base.Events.AddHandler(OdbcDataAdapter.s_eventRowUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(OdbcDataAdapter.s_eventRowUpdated, value);
			}
		}

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> before a command is executed against the data source.</summary>
		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06002088 RID: 8328 RVA: 0x000971E8 File Offset: 0x000953E8
		// (remove) Token: 0x06002089 RID: 8329 RVA: 0x0009724C File Offset: 0x0009544C
		public event OdbcRowUpdatingEventHandler RowUpdating
		{
			add
			{
				OdbcRowUpdatingEventHandler odbcRowUpdatingEventHandler = (OdbcRowUpdatingEventHandler)base.Events[OdbcDataAdapter.s_eventRowUpdating];
				if (odbcRowUpdatingEventHandler != null && value.Target is OdbcCommandBuilder)
				{
					OdbcRowUpdatingEventHandler odbcRowUpdatingEventHandler2 = (OdbcRowUpdatingEventHandler)ADP.FindBuilder(odbcRowUpdatingEventHandler);
					if (odbcRowUpdatingEventHandler2 != null)
					{
						base.Events.RemoveHandler(OdbcDataAdapter.s_eventRowUpdating, odbcRowUpdatingEventHandler2);
					}
				}
				base.Events.AddHandler(OdbcDataAdapter.s_eventRowUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(OdbcDataAdapter.s_eventRowUpdating, value);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x0600208A RID: 8330 RVA: 0x0009725F File Offset: 0x0009545F
		object ICloneable.Clone()
		{
			return new OdbcDataAdapter(this);
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00097267 File Offset: 0x00095467
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OdbcRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x00097273 File Offset: 0x00095473
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OdbcRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00097280 File Offset: 0x00095480
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			OdbcRowUpdatedEventHandler odbcRowUpdatedEventHandler = (OdbcRowUpdatedEventHandler)base.Events[OdbcDataAdapter.s_eventRowUpdated];
			if (odbcRowUpdatedEventHandler != null && value is OdbcRowUpdatedEventArgs)
			{
				odbcRowUpdatedEventHandler(this, (OdbcRowUpdatedEventArgs)value);
			}
			base.OnRowUpdated(value);
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x000972C4 File Offset: 0x000954C4
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			OdbcRowUpdatingEventHandler odbcRowUpdatingEventHandler = (OdbcRowUpdatingEventHandler)base.Events[OdbcDataAdapter.s_eventRowUpdating];
			if (odbcRowUpdatingEventHandler != null && value is OdbcRowUpdatingEventArgs)
			{
				odbcRowUpdatingEventHandler(this, (OdbcRowUpdatingEventArgs)value);
			}
			base.OnRowUpdating(value);
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x00097306 File Offset: 0x00095506
		// Note: this type is marked as 'beforefieldinit'.
		static OdbcDataAdapter()
		{
		}

		// Token: 0x0400177F RID: 6015
		private static readonly object s_eventRowUpdated = new object();

		// Token: 0x04001780 RID: 6016
		private static readonly object s_eventRowUpdating = new object();

		// Token: 0x04001781 RID: 6017
		private OdbcCommand _deleteCommand;

		// Token: 0x04001782 RID: 6018
		private OdbcCommand _insertCommand;

		// Token: 0x04001783 RID: 6019
		private OdbcCommand _selectCommand;

		// Token: 0x04001784 RID: 6020
		private OdbcCommand _updateCommand;
	}
}
