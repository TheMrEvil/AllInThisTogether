using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Common
{
	/// <summary>Represents an SQL statement or stored procedure to execute against a data source. Provides a base class for database-specific classes that represent commands. <see cref="Overload:System.Data.Common.DbCommand.ExecuteNonQueryAsync" /></summary>
	// Token: 0x0200038B RID: 907
	public abstract class DbCommand : Component, IDbCommand, IDisposable, IAsyncDisposable
	{
		/// <summary>Constructs an instance of the <see cref="T:System.Data.Common.DbCommand" /> object.</summary>
		// Token: 0x06002B50 RID: 11088 RVA: 0x000BB8F6 File Offset: 0x000B9AF6
		protected DbCommand()
		{
		}

		/// <summary>Gets or sets the text command to run against the data source.</summary>
		/// <returns>The text command to execute. The default value is an empty string ("").</returns>
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002B51 RID: 11089
		// (set) Token: 0x06002B52 RID: 11090
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.All)]
		public abstract string CommandText { get; set; }

		/// <summary>Gets or sets the wait time before terminating the attempt to execute a command and generating an error.</summary>
		/// <returns>The time in seconds to wait for the command to execute.</returns>
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002B53 RID: 11091
		// (set) Token: 0x06002B54 RID: 11092
		public abstract int CommandTimeout { get; set; }

		/// <summary>Indicates or specifies how the <see cref="P:System.Data.Common.DbCommand.CommandText" /> property is interpreted.</summary>
		/// <returns>One of the <see cref="T:System.Data.CommandType" /> values. The default is <see langword="Text" />.</returns>
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06002B55 RID: 11093
		// (set) Token: 0x06002B56 RID: 11094
		[DefaultValue(CommandType.Text)]
		[RefreshProperties(RefreshProperties.All)]
		public abstract CommandType CommandType { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DbConnection" /> used by this <see cref="T:System.Data.Common.DbCommand" />.</summary>
		/// <returns>The connection to the data source.</returns>
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002B57 RID: 11095 RVA: 0x000BB8FE File Offset: 0x000B9AFE
		// (set) Token: 0x06002B58 RID: 11096 RVA: 0x000BB906 File Offset: 0x000B9B06
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbConnection Connection
		{
			get
			{
				return this.DbConnection;
			}
			set
			{
				this.DbConnection = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.IDbConnection" /> used by this instance of the <see cref="T:System.Data.IDbCommand" />.</summary>
		/// <returns>The connection to the data source.</returns>
		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002B59 RID: 11097 RVA: 0x000BB8FE File Offset: 0x000B9AFE
		// (set) Token: 0x06002B5A RID: 11098 RVA: 0x000BB90F File Offset: 0x000B9B0F
		IDbConnection IDbCommand.Connection
		{
			get
			{
				return this.DbConnection;
			}
			set
			{
				this.DbConnection = (DbConnection)value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DbConnection" /> used by this <see cref="T:System.Data.Common.DbCommand" />.</summary>
		/// <returns>The connection to the data source.</returns>
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06002B5B RID: 11099
		// (set) Token: 0x06002B5C RID: 11100
		protected abstract DbConnection DbConnection { get; set; }

		/// <summary>Gets the collection of <see cref="T:System.Data.Common.DbParameter" /> objects.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure.</returns>
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002B5D RID: 11101
		protected abstract DbParameterCollection DbParameterCollection { get; }

		/// <summary>Gets or sets the <see cref="P:System.Data.Common.DbCommand.DbTransaction" /> within which this <see cref="T:System.Data.Common.DbCommand" /> object executes.</summary>
		/// <returns>The transaction within which a Command object of a .NET Framework data provider executes. The default value is a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002B5E RID: 11102
		// (set) Token: 0x06002B5F RID: 11103
		protected abstract DbTransaction DbTransaction { get; set; }

		/// <summary>Gets or sets a value indicating whether the command object should be visible in a customized interface control.</summary>
		/// <returns>
		///   <see langword="true" />, if the command object should be visible in a control; otherwise <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002B60 RID: 11104
		// (set) Token: 0x06002B61 RID: 11105
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(true)]
		[DesignOnly(true)]
		[Browsable(false)]
		public abstract bool DesignTimeVisible { get; set; }

		/// <summary>Gets the collection of <see cref="T:System.Data.Common.DbParameter" /> objects. For more information on parameters, see Configuring Parameters and Parameter Data Types.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure.</returns>
		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x000BB91D File Offset: 0x000B9B1D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbParameterCollection Parameters
		{
			get
			{
				return this.DbParameterCollection;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.IDataParameterCollection" />.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure.</returns>
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x000BB91D File Offset: 0x000B9B1D
		IDataParameterCollection IDbCommand.Parameters
		{
			get
			{
				return this.DbParameterCollection;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DbTransaction" /> within which this <see cref="T:System.Data.Common.DbCommand" /> object executes.</summary>
		/// <returns>The transaction within which a <see langword="Command" /> object of a .NET Framework data provider executes. The default value is a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x000BB925 File Offset: 0x000B9B25
		// (set) Token: 0x06002B65 RID: 11109 RVA: 0x000BB92D File Offset: 0x000B9B2D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		public DbTransaction Transaction
		{
			get
			{
				return this.DbTransaction;
			}
			set
			{
				this.DbTransaction = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Data.Common.DbCommand.DbTransaction" /> within which this <see cref="T:System.Data.Common.DbCommand" /> object executes.</summary>
		/// <returns>The transaction within which a <see langword="Command" /> object of a .NET Framework data provider executes. The default value is a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002B66 RID: 11110 RVA: 0x000BB925 File Offset: 0x000B9B25
		// (set) Token: 0x06002B67 RID: 11111 RVA: 0x000BB936 File Offset: 0x000B9B36
		IDbTransaction IDbCommand.Transaction
		{
			get
			{
				return this.DbTransaction;
			}
			set
			{
				this.DbTransaction = (DbTransaction)value;
			}
		}

		/// <summary>Gets or sets how command results are applied to the <see cref="T:System.Data.DataRow" /> when used by the Update method of a <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.UpdateRowSource" /> values. The default is <see langword="Both" /> unless the command is automatically generated. Then the default is <see langword="None" />.</returns>
		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06002B68 RID: 11112
		// (set) Token: 0x06002B69 RID: 11113
		[DefaultValue(UpdateRowSource.Both)]
		public abstract UpdateRowSource UpdatedRowSource { get; set; }

		// Token: 0x06002B6A RID: 11114 RVA: 0x000BB944 File Offset: 0x000B9B44
		internal void CancelIgnoreFailure()
		{
			try
			{
				this.Cancel();
			}
			catch (Exception)
			{
			}
		}

		/// <summary>Attempts to cancels the execution of a <see cref="T:System.Data.Common.DbCommand" />.</summary>
		// Token: 0x06002B6B RID: 11115
		public abstract void Cancel();

		/// <summary>Creates a new instance of a <see cref="T:System.Data.Common.DbParameter" /> object.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbParameter" /> object.</returns>
		// Token: 0x06002B6C RID: 11116 RVA: 0x000BB96C File Offset: 0x000B9B6C
		public DbParameter CreateParameter()
		{
			return this.CreateDbParameter();
		}

		/// <summary>Creates a new instance of an <see cref="T:System.Data.IDbDataParameter" /> object.</summary>
		/// <returns>An <see langword="IDbDataParameter" /> object.</returns>
		// Token: 0x06002B6D RID: 11117 RVA: 0x000BB96C File Offset: 0x000B9B6C
		IDbDataParameter IDbCommand.CreateParameter()
		{
			return this.CreateDbParameter();
		}

		/// <summary>Creates a new instance of a <see cref="T:System.Data.Common.DbParameter" /> object.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbParameter" /> object.</returns>
		// Token: 0x06002B6E RID: 11118
		protected abstract DbParameter CreateDbParameter();

		/// <summary>Executes the command text against the connection.</summary>
		/// <param name="behavior">An instance of <see cref="T:System.Data.CommandBehavior" />.</param>
		/// <returns>A task representing the operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x06002B6F RID: 11119
		protected abstract DbDataReader ExecuteDbDataReader(CommandBehavior behavior);

		/// <summary>Executes a SQL statement against a connection object.</summary>
		/// <returns>The number of rows affected.</returns>
		// Token: 0x06002B70 RID: 11120
		public abstract int ExecuteNonQuery();

		/// <summary>Executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" />, and returns an <see cref="T:System.Data.Common.DbDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		// Token: 0x06002B71 RID: 11121 RVA: 0x000BB974 File Offset: 0x000B9B74
		public DbDataReader ExecuteReader()
		{
			return this.ExecuteDbDataReader(CommandBehavior.Default);
		}

		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" /> and builds an <see cref="T:System.Data.IDataReader" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		// Token: 0x06002B72 RID: 11122 RVA: 0x000BB974 File Offset: 0x000B9B74
		IDataReader IDbCommand.ExecuteReader()
		{
			return this.ExecuteDbDataReader(CommandBehavior.Default);
		}

		/// <summary>Executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" />, and returns an <see cref="T:System.Data.Common.DbDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <returns>An <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		// Token: 0x06002B73 RID: 11123 RVA: 0x000BB97D File Offset: 0x000B9B7D
		public DbDataReader ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteDbDataReader(behavior);
		}

		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" />, and builds an <see cref="T:System.Data.IDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		// Token: 0x06002B74 RID: 11124 RVA: 0x000BB97D File Offset: 0x000B9B7D
		IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteDbDataReader(behavior);
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbCommand.ExecuteNonQuery" />, which executes a SQL statement against a connection object.  
		///  Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteNonQueryAsync(System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x06002B75 RID: 11125 RVA: 0x000BB986 File Offset: 0x000B9B86
		public Task<int> ExecuteNonQueryAsync()
		{
			return this.ExecuteNonQueryAsync(CancellationToken.None);
		}

		/// <summary>This is the asynchronous version of <see cref="M:System.Data.Common.DbCommand.ExecuteNonQuery" />. Providers should override with an appropriate implementation. The cancellation token may optionally be ignored.  
		///  The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbCommand.ExecuteNonQuery" /> method and returns a completed task, blocking the calling thread. The default implementation will return a cancelled task if passed an already cancelled cancellation token.  Exceptions thrown by <see cref="M:System.Data.Common.DbCommand.ExecuteNonQuery" /> will be communicated via the returned Task Exception property.  
		///  Do not invoke other methods and properties of the <see langword="DbCommand" /> object until the returned Task is complete.</summary>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x06002B76 RID: 11126 RVA: 0x000BB994 File Offset: 0x000B9B94
		public virtual Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<int>();
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = cancellationToken.Register(delegate(object s)
				{
					((DbCommand)s).CancelIgnoreFailure();
				}, this);
			}
			Task<int> result;
			try
			{
				result = Task.FromResult<int>(this.ExecuteNonQuery());
			}
			catch (Exception exception)
			{
				result = Task.FromException<int>(exception);
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
			}
			return result;
		}

		/// <summary>An asynchronous version of <see cref="Overload:System.Data.Common.DbCommand.ExecuteReader" />, which executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" /> and returns a <see cref="T:System.Data.Common.DbDataReader" />.  
		///  Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteDbDataReaderAsync(System.Data.CommandBehavior,System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x06002B77 RID: 11127 RVA: 0x000BBA28 File Offset: 0x000B9C28
		public Task<DbDataReader> ExecuteReaderAsync()
		{
			return this.ExecuteReaderAsync(CommandBehavior.Default, CancellationToken.None);
		}

		/// <summary>An asynchronous version of <see cref="Overload:System.Data.Common.DbCommand.ExecuteReader" />, which executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" /> and returns a <see cref="T:System.Data.Common.DbDataReader" />. This method propagates a notification that operations should be canceled.  
		///  Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteDbDataReaderAsync(System.Data.CommandBehavior,System.Threading.CancellationToken)" />.</summary>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x06002B78 RID: 11128 RVA: 0x000BBA36 File Offset: 0x000B9C36
		public Task<DbDataReader> ExecuteReaderAsync(CancellationToken cancellationToken)
		{
			return this.ExecuteReaderAsync(CommandBehavior.Default, cancellationToken);
		}

		/// <summary>An asynchronous version of <see cref="Overload:System.Data.Common.DbCommand.ExecuteReader" />, which executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" /> and returns a <see cref="T:System.Data.Common.DbDataReader" />.  
		///  Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteDbDataReaderAsync(System.Data.CommandBehavior,System.Threading.CancellationToken)" />.</summary>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x06002B79 RID: 11129 RVA: 0x000BBA40 File Offset: 0x000B9C40
		public Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior)
		{
			return this.ExecuteReaderAsync(behavior, CancellationToken.None);
		}

		/// <summary>Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteDbDataReaderAsync(System.Data.CommandBehavior,System.Threading.CancellationToken)" />.</summary>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x06002B7A RID: 11130 RVA: 0x000BBA4E File Offset: 0x000B9C4E
		public Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			return this.ExecuteDbDataReaderAsync(behavior, cancellationToken);
		}

		/// <summary>Providers should implement this method to provide a non-default implementation for <see cref="Overload:System.Data.Common.DbCommand.ExecuteReader" /> overloads.  
		///  The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbCommand.ExecuteReader" /> method and returns a completed task, blocking the calling thread. The default implementation will return a cancelled task if passed an already cancelled cancellation token. Exceptions thrown by ExecuteReader will be communicated via the returned Task Exception property.  
		///  This method accepts a cancellation token that can be used to request the operation to be cancelled early. Implementations may ignore this request.</summary>
		/// <param name="behavior">Options for statement execution and data retrieval.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x06002B7B RID: 11131 RVA: 0x000BBA58 File Offset: 0x000B9C58
		protected virtual Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<DbDataReader>();
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = cancellationToken.Register(delegate(object s)
				{
					((DbCommand)s).CancelIgnoreFailure();
				}, this);
			}
			Task<DbDataReader> result;
			try
			{
				result = Task.FromResult<DbDataReader>(this.ExecuteReader(behavior));
			}
			catch (Exception exception)
			{
				result = Task.FromException<DbDataReader>(exception);
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
			}
			return result;
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbCommand.ExecuteScalar" />, which executes the query and returns the first column of the first row in the result set returned by the query. All other columns and rows are ignored.  
		///  Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteScalarAsync(System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x06002B7C RID: 11132 RVA: 0x000BBAEC File Offset: 0x000B9CEC
		public Task<object> ExecuteScalarAsync()
		{
			return this.ExecuteScalarAsync(CancellationToken.None);
		}

		/// <summary>This is the asynchronous version of <see cref="M:System.Data.Common.DbCommand.ExecuteScalar" />. Providers should override with an appropriate implementation. The cancellation token may optionally be ignored.  
		///  The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbCommand.ExecuteScalar" /> method and returns a completed task, blocking the calling thread. The default implementation will return a cancelled task if passed an already cancelled cancellation token. Exceptions thrown by ExecuteScalar will be communicated via the returned Task Exception property.  
		///  Do not invoke other methods and properties of the <see langword="DbCommand" /> object until the returned Task is complete.</summary>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x06002B7D RID: 11133 RVA: 0x000BBAFC File Offset: 0x000B9CFC
		public virtual Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<object>();
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = cancellationToken.Register(delegate(object s)
				{
					((DbCommand)s).CancelIgnoreFailure();
				}, this);
			}
			Task<object> result;
			try
			{
				result = Task.FromResult<object>(this.ExecuteScalar());
			}
			catch (Exception exception)
			{
				result = Task.FromException<object>(exception);
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
			}
			return result;
		}

		/// <summary>Executes the query and returns the first column of the first row in the result set returned by the query. All other columns and rows are ignored.</summary>
		/// <returns>The first column of the first row in the result set.</returns>
		// Token: 0x06002B7E RID: 11134
		public abstract object ExecuteScalar();

		/// <summary>Creates a prepared (or compiled) version of the command on the data source.</summary>
		// Token: 0x06002B7F RID: 11135
		public abstract void Prepare();

		// Token: 0x06002B80 RID: 11136 RVA: 0x000BBB90 File Offset: 0x000B9D90
		public virtual Task PrepareAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task result;
			try
			{
				this.Prepare();
				result = Task.CompletedTask;
			}
			catch (Exception exception)
			{
				result = Task.FromException(exception);
			}
			return result;
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000BBBD8 File Offset: 0x000B9DD8
		public virtual ValueTask DisposeAsync()
		{
			base.Dispose();
			return default(ValueTask);
		}

		// Token: 0x0200038C RID: 908
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002B82 RID: 11138 RVA: 0x000BBBF4 File Offset: 0x000B9DF4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002B83 RID: 11139 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c()
			{
			}

			// Token: 0x06002B84 RID: 11140 RVA: 0x000BBC00 File Offset: 0x000B9E00
			internal void <ExecuteNonQueryAsync>b__52_0(object s)
			{
				((DbCommand)s).CancelIgnoreFailure();
			}

			// Token: 0x06002B85 RID: 11141 RVA: 0x000BBC00 File Offset: 0x000B9E00
			internal void <ExecuteDbDataReaderAsync>b__57_0(object s)
			{
				((DbCommand)s).CancelIgnoreFailure();
			}

			// Token: 0x06002B86 RID: 11142 RVA: 0x000BBC00 File Offset: 0x000B9E00
			internal void <ExecuteScalarAsync>b__59_0(object s)
			{
				((DbCommand)s).CancelIgnoreFailure();
			}

			// Token: 0x04001B31 RID: 6961
			public static readonly DbCommand.<>c <>9 = new DbCommand.<>c();

			// Token: 0x04001B32 RID: 6962
			public static Action<object> <>9__52_0;

			// Token: 0x04001B33 RID: 6963
			public static Action<object> <>9__57_0;

			// Token: 0x04001B34 RID: 6964
			public static Action<object> <>9__59_0;
		}
	}
}
