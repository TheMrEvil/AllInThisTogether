using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.Common
{
	/// <summary>Represents a connection to a database.</summary>
	// Token: 0x0200038D RID: 909
	public abstract class DbConnection : Component, IDbConnection, IDisposable, IAsyncDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbConnection" /> class.</summary>
		// Token: 0x06002B87 RID: 11143 RVA: 0x000BB8F6 File Offset: 0x000B9AF6
		protected DbConnection()
		{
		}

		/// <summary>Gets or sets the string used to open the connection.</summary>
		/// <returns>The connection string used to establish the initial connection. The exact contents of the connection string depend on the specific data source for this connection. The default value is an empty string.</returns>
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002B88 RID: 11144
		// (set) Token: 0x06002B89 RID: 11145
		[DefaultValue("")]
		[SettingsBindable(true)]
		[RefreshProperties(RefreshProperties.All)]
		[RecommendedAsConfigurable(true)]
		public abstract string ConnectionString { get; set; }

		/// <summary>Gets the time to wait while establishing a connection before terminating the attempt and generating an error.</summary>
		/// <returns>The time (in seconds) to wait for a connection to open. The default value is determined by the specific type of connection that you are using.</returns>
		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002B8A RID: 11146 RVA: 0x000BBC0D File Offset: 0x000B9E0D
		public virtual int ConnectionTimeout
		{
			get
			{
				return 15;
			}
		}

		/// <summary>Gets the name of the current database after a connection is opened, or the database name specified in the connection string before the connection is opened.</summary>
		/// <returns>The name of the current database or the name of the database to be used after a connection is opened. The default value is an empty string.</returns>
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002B8B RID: 11147
		public abstract string Database { get; }

		/// <summary>Gets the name of the database server to which to connect.</summary>
		/// <returns>The name of the database server to which to connect. The default value is an empty string.</returns>
		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002B8C RID: 11148
		public abstract string DataSource { get; }

		/// <summary>Gets the <see cref="T:System.Data.Common.DbProviderFactory" /> for this <see cref="T:System.Data.Common.DbConnection" />.</summary>
		/// <returns>A set of methods for creating instances of a provider's implementation of the data source classes.</returns>
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x00003E32 File Offset: 0x00002032
		protected virtual DbProviderFactory DbProviderFactory
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002B8E RID: 11150 RVA: 0x000BBC11 File Offset: 0x000B9E11
		internal DbProviderFactory ProviderFactory
		{
			get
			{
				return this.DbProviderFactory;
			}
		}

		/// <summary>Gets a string that represents the version of the server to which the object is connected.</summary>
		/// <returns>The version of the database. The format of the string returned depends on the specific type of connection you are using.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Data.Common.DbConnection.ServerVersion" /> was called while the returned Task was not completed and the connection was not opened after a call to <see cref="Overload:System.Data.Common.DbConnection.OpenAsync" />.</exception>
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002B8F RID: 11151
		[Browsable(false)]
		public abstract string ServerVersion { get; }

		/// <summary>Gets a string that describes the state of the connection.</summary>
		/// <returns>The state of the connection. The format of the string returned depends on the specific type of connection you are using.</returns>
		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002B90 RID: 11152
		[Browsable(false)]
		public abstract ConnectionState State { get; }

		/// <summary>Occurs when the state of the event changes.</summary>
		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06002B91 RID: 11153 RVA: 0x000BBC1C File Offset: 0x000B9E1C
		// (remove) Token: 0x06002B92 RID: 11154 RVA: 0x000BBC54 File Offset: 0x000B9E54
		public virtual event StateChangeEventHandler StateChange
		{
			[CompilerGenerated]
			add
			{
				StateChangeEventHandler stateChangeEventHandler = this.StateChange;
				StateChangeEventHandler stateChangeEventHandler2;
				do
				{
					stateChangeEventHandler2 = stateChangeEventHandler;
					StateChangeEventHandler value2 = (StateChangeEventHandler)Delegate.Combine(stateChangeEventHandler2, value);
					stateChangeEventHandler = Interlocked.CompareExchange<StateChangeEventHandler>(ref this.StateChange, value2, stateChangeEventHandler2);
				}
				while (stateChangeEventHandler != stateChangeEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				StateChangeEventHandler stateChangeEventHandler = this.StateChange;
				StateChangeEventHandler stateChangeEventHandler2;
				do
				{
					stateChangeEventHandler2 = stateChangeEventHandler;
					StateChangeEventHandler value2 = (StateChangeEventHandler)Delegate.Remove(stateChangeEventHandler2, value);
					stateChangeEventHandler = Interlocked.CompareExchange<StateChangeEventHandler>(ref this.StateChange, value2, stateChangeEventHandler2);
				}
				while (stateChangeEventHandler != stateChangeEventHandler2);
			}
		}

		/// <summary>Starts a database transaction.</summary>
		/// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
		/// <returns>An object representing the new transaction.</returns>
		// Token: 0x06002B93 RID: 11155
		protected abstract DbTransaction BeginDbTransaction(IsolationLevel isolationLevel);

		/// <summary>Starts a database transaction.</summary>
		/// <returns>An object representing the new transaction.</returns>
		// Token: 0x06002B94 RID: 11156 RVA: 0x000BBC89 File Offset: 0x000B9E89
		public DbTransaction BeginTransaction()
		{
			return this.BeginDbTransaction(IsolationLevel.Unspecified);
		}

		/// <summary>Starts a database transaction with the specified isolation level.</summary>
		/// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
		/// <returns>An object representing the new transaction.</returns>
		// Token: 0x06002B95 RID: 11157 RVA: 0x000BBC92 File Offset: 0x000B9E92
		public DbTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			return this.BeginDbTransaction(isolationLevel);
		}

		/// <summary>Begins a database transaction.</summary>
		/// <returns>An object that represents the new transaction.</returns>
		// Token: 0x06002B96 RID: 11158 RVA: 0x000BBC89 File Offset: 0x000B9E89
		IDbTransaction IDbConnection.BeginTransaction()
		{
			return this.BeginDbTransaction(IsolationLevel.Unspecified);
		}

		/// <summary>Begins a database transaction with the specified <see cref="T:System.Data.IsolationLevel" /> value.</summary>
		/// <param name="isolationLevel">One of the <see cref="T:System.Data.IsolationLevel" /> values.</param>
		/// <returns>An object that represents the new transaction.</returns>
		// Token: 0x06002B97 RID: 11159 RVA: 0x000BBC92 File Offset: 0x000B9E92
		IDbTransaction IDbConnection.BeginTransaction(IsolationLevel isolationLevel)
		{
			return this.BeginDbTransaction(isolationLevel);
		}

		/// <summary>Closes the connection to the database. This is the preferred method of closing any open connection.</summary>
		/// <exception cref="T:System.Data.Common.DbException">The connection-level error that occurred while opening the connection.</exception>
		// Token: 0x06002B98 RID: 11160
		public abstract void Close();

		/// <summary>Changes the current database for an open connection.</summary>
		/// <param name="databaseName">Specifies the name of the database for the connection to use.</param>
		// Token: 0x06002B99 RID: 11161
		public abstract void ChangeDatabase(string databaseName);

		/// <summary>Creates and returns a <see cref="T:System.Data.Common.DbCommand" /> object associated with the current connection.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbCommand" /> object.</returns>
		// Token: 0x06002B9A RID: 11162 RVA: 0x000BBC9B File Offset: 0x000B9E9B
		public DbCommand CreateCommand()
		{
			return this.CreateDbCommand();
		}

		/// <summary>Creates and returns a <see cref="T:System.Data.Common.DbCommand" /> object that is associated with the current connection.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbCommand" /> object that is associated with the connection.</returns>
		// Token: 0x06002B9B RID: 11163 RVA: 0x000BBC9B File Offset: 0x000B9E9B
		IDbCommand IDbConnection.CreateCommand()
		{
			return this.CreateDbCommand();
		}

		/// <summary>Creates and returns a <see cref="T:System.Data.Common.DbCommand" /> object associated with the current connection.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbCommand" /> object.</returns>
		// Token: 0x06002B9C RID: 11164
		protected abstract DbCommand CreateDbCommand();

		/// <summary>Enlists in the specified transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.Transactions.Transaction" /> in which to enlist.</param>
		// Token: 0x06002B9D RID: 11165 RVA: 0x00008E4B File Offset: 0x0000704B
		public virtual void EnlistTransaction(Transaction transaction)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		// Token: 0x06002B9E RID: 11166 RVA: 0x00008E4B File Offset: 0x0000704B
		public virtual DataTable GetSchema()
		{
			throw ADP.NotSupported();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection" /> using the specified string for the schema name.</summary>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collectionName" /> is specified as null.</exception>
		// Token: 0x06002B9F RID: 11167 RVA: 0x00008E4B File Offset: 0x0000704B
		public virtual DataTable GetSchema(string collectionName)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection" /> using the specified string for the schema name and the specified string array for the restriction values.</summary>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <param name="restrictionValues">Specifies a set of restriction values for the requested schema.</param>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collectionName" /> is specified as null.</exception>
		// Token: 0x06002BA0 RID: 11168 RVA: 0x00008E4B File Offset: 0x0000704B
		public virtual DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Raises the <see cref="E:System.Data.Common.DbConnection.StateChange" /> event.</summary>
		/// <param name="stateChange">A <see cref="T:System.Data.StateChangeEventArgs" /> that contains the event data.</param>
		// Token: 0x06002BA1 RID: 11169 RVA: 0x000BBCA3 File Offset: 0x000B9EA3
		protected virtual void OnStateChange(StateChangeEventArgs stateChange)
		{
			if (this._suppressStateChangeForReconnection)
			{
				return;
			}
			StateChangeEventHandler stateChange2 = this.StateChange;
			if (stateChange2 == null)
			{
				return;
			}
			stateChange2(this, stateChange);
		}

		/// <summary>Opens a database connection with the settings specified by the <see cref="P:System.Data.Common.DbConnection.ConnectionString" />.</summary>
		// Token: 0x06002BA2 RID: 11170
		public abstract void Open();

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbConnection.Open" />, which opens a database connection with the settings specified by the <see cref="P:System.Data.Common.DbConnection.ConnectionString" />. This method invokes the virtual method <see cref="M:System.Data.Common.DbConnection.OpenAsync(System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		// Token: 0x06002BA3 RID: 11171 RVA: 0x000BBCC0 File Offset: 0x000B9EC0
		public Task OpenAsync()
		{
			return this.OpenAsync(CancellationToken.None);
		}

		/// <summary>This is the asynchronous version of <see cref="M:System.Data.Common.DbConnection.Open" />. Providers should override with an appropriate implementation. The cancellation token can optionally be honored.  
		///  The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbConnection.Open" /> call and returns a completed task. The default implementation will return a cancelled task if passed an already cancelled cancellationToken. Exceptions thrown by Open will be communicated via the returned Task Exception property.  
		///  Do not invoke other methods and properties of the <see langword="DbConnection" /> object until the returned Task is complete.</summary>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		// Token: 0x06002BA4 RID: 11172 RVA: 0x000BBCD0 File Offset: 0x000B9ED0
		public virtual Task OpenAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task result;
			try
			{
				this.Open();
				result = Task.CompletedTask;
			}
			catch (Exception exception)
			{
				result = Task.FromException(exception);
			}
			return result;
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000BBD18 File Offset: 0x000B9F18
		protected virtual ValueTask<DbTransaction> BeginDbTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<DbTransaction>(Task.FromCanceled<DbTransaction>(cancellationToken));
			}
			ValueTask<DbTransaction> result;
			try
			{
				result = new ValueTask<DbTransaction>(this.BeginDbTransaction(isolationLevel));
			}
			catch (Exception exception)
			{
				result = new ValueTask<DbTransaction>(Task.FromException<DbTransaction>(exception));
			}
			return result;
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000BBD68 File Offset: 0x000B9F68
		public ValueTask<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.BeginDbTransactionAsync(IsolationLevel.Unspecified, cancellationToken);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000BBD72 File Offset: 0x000B9F72
		public ValueTask<DbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.BeginDbTransactionAsync(isolationLevel, cancellationToken);
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000BBD7C File Offset: 0x000B9F7C
		public virtual Task CloseAsync()
		{
			Task result;
			try
			{
				this.Close();
				result = Task.CompletedTask;
			}
			catch (Exception exception)
			{
				result = Task.FromException(exception);
			}
			return result;
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000BBDB0 File Offset: 0x000B9FB0
		public virtual ValueTask DisposeAsync()
		{
			base.Dispose();
			return default(ValueTask);
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x000BBDCC File Offset: 0x000B9FCC
		public virtual Task ChangeDatabaseAsync(string databaseName, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task result;
			try
			{
				this.ChangeDatabase(databaseName);
				result = Task.CompletedTask;
			}
			catch (Exception exception)
			{
				result = Task.FromException(exception);
			}
			return result;
		}

		// Token: 0x04001B35 RID: 6965
		internal bool _suppressStateChangeForReconnection;

		// Token: 0x04001B36 RID: 6966
		[CompilerGenerated]
		private StateChangeEventHandler StateChange;
	}
}
