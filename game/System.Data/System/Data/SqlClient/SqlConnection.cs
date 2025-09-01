using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.SqlServer.Server;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Represents a connection to a SQL Server database. This class cannot be inherited.</summary>
	// Token: 0x020001C8 RID: 456
	public sealed class SqlConnection : DbConnection, ICloneable, IDbConnection, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlConnection" /> class when given a string that contains the connection string.</summary>
		/// <param name="connectionString">The connection used to open the SQL Server database.</param>
		// Token: 0x06001635 RID: 5685 RVA: 0x00065E1D File Offset: 0x0006401D
		public SqlConnection(string connectionString) : this()
		{
			this.ConnectionString = connectionString;
			this.CacheConnectionStringProperties();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlConnection" /> class given a connection string, that does not use <see langword="Integrated Security = true" /> and a <see cref="T:System.Data.SqlClient.SqlCredential" /> object that contains the user ID and password.</summary>
		/// <param name="connectionString">A connection string that does not use any of the following connection string keywords: <see langword="Integrated Security = true" />, <see langword="UserId" />, or <see langword="Password" />; or that does not use <see langword="ContextConnection = true" />.</param>
		/// <param name="credential">A <see cref="T:System.Data.SqlClient.SqlCredential" /> object. If <paramref name="credential" /> is null, <see cref="M:System.Data.SqlClient.SqlConnection.#ctor(System.String,System.Data.SqlClient.SqlCredential)" /> is functionally equivalent to <see cref="M:System.Data.SqlClient.SqlConnection.#ctor(System.String)" />.</param>
		// Token: 0x06001636 RID: 5686 RVA: 0x00065E34 File Offset: 0x00064034
		public SqlConnection(string connectionString, SqlCredential credential) : this()
		{
			this.ConnectionString = connectionString;
			if (credential != null)
			{
				SqlConnectionString opt = (SqlConnectionString)this.ConnectionOptions;
				if (this.UsesClearUserIdOrPassword(opt))
				{
					throw ADP.InvalidMixedArgumentOfSecureAndClearCredential();
				}
				if (this.UsesIntegratedSecurity(opt))
				{
					throw ADP.InvalidMixedArgumentOfSecureCredentialAndIntegratedSecurity();
				}
				this.Credential = credential;
			}
			this.CacheConnectionStringProperties();
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x00065E88 File Offset: 0x00064088
		private SqlConnection(SqlConnection connection)
		{
			this._reconnectLock = new object();
			this._originalConnectionId = Guid.Empty;
			base..ctor();
			GC.SuppressFinalize(this);
			this.CopyFrom(connection);
			this._connectionString = connection._connectionString;
			if (connection._credential != null)
			{
				SecureString secureString = connection._credential.Password.Copy();
				secureString.MakeReadOnly();
				this._credential = new SqlCredential(connection._credential.UserId, secureString);
			}
			this._accessToken = connection._accessToken;
			this.CacheConnectionStringProperties();
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00065F14 File Offset: 0x00064114
		private void CacheConnectionStringProperties()
		{
			SqlConnectionString sqlConnectionString = this.ConnectionOptions as SqlConnectionString;
			if (sqlConnectionString != null)
			{
				this._connectRetryCount = sqlConnectionString.ConnectRetryCount;
			}
		}

		/// <summary>When set to <see langword="true" />, enables statistics gathering for the current connection.</summary>
		/// <returns>Returns <see langword="true" /> if statistics gathering is enabled; otherwise <see langword="false" />. <see langword="false" /> is the default.</returns>
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00065F3C File Offset: 0x0006413C
		// (set) Token: 0x0600163A RID: 5690 RVA: 0x00065F44 File Offset: 0x00064144
		public bool StatisticsEnabled
		{
			get
			{
				return this._collectstats;
			}
			set
			{
				if (value)
				{
					if (ConnectionState.Open == this.State)
					{
						if (this._statistics == null)
						{
							this._statistics = new SqlStatistics();
							ADP.TimerCurrent(out this._statistics._openTimestamp);
						}
						this.Parser.Statistics = this._statistics;
					}
				}
				else if (this._statistics != null && ConnectionState.Open == this.State)
				{
					this.Parser.Statistics = null;
					ADP.TimerCurrent(out this._statistics._closeTimestamp);
				}
				this._collectstats = value;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x00065FC7 File Offset: 0x000641C7
		// (set) Token: 0x0600163C RID: 5692 RVA: 0x00065FCF File Offset: 0x000641CF
		internal bool AsyncCommandInProgress
		{
			get
			{
				return this._AsyncCommandInProgress;
			}
			set
			{
				this._AsyncCommandInProgress = value;
			}
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00065FD8 File Offset: 0x000641D8
		private bool UsesIntegratedSecurity(SqlConnectionString opt)
		{
			return opt != null && opt.IntegratedSecurity;
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00065FE8 File Offset: 0x000641E8
		private bool UsesClearUserIdOrPassword(SqlConnectionString opt)
		{
			bool result = false;
			if (opt != null)
			{
				result = (!string.IsNullOrEmpty(opt.UserID) || !string.IsNullOrEmpty(opt.Password));
			}
			return result;
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0006601A File Offset: 0x0006421A
		internal SqlConnectionString.TransactionBindingEnum TransactionBinding
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).TransactionBinding;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x0006602C File Offset: 0x0006422C
		internal SqlConnectionString.TypeSystem TypeSystem
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).TypeSystemVersion;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x0006603E File Offset: 0x0006423E
		internal Version TypeSystemAssemblyVersion
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).TypeSystemAssemblyVersion;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x00066050 File Offset: 0x00064250
		internal int ConnectRetryInterval
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).ConnectRetryInterval;
			}
		}

		/// <summary>Gets or sets the string used to open a SQL Server database.</summary>
		/// <returns>The connection string that includes the source database name, and other parameters needed to establish the initial connection. The default value is an empty string.</returns>
		/// <exception cref="T:System.ArgumentException">An invalid connection string argument has been supplied, or a required connection string argument has not been supplied.</exception>
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x00066062 File Offset: 0x00064262
		// (set) Token: 0x06001644 RID: 5700 RVA: 0x0006606C File Offset: 0x0006426C
		public override string ConnectionString
		{
			get
			{
				return this.ConnectionString_Get();
			}
			set
			{
				if (this._credential != null || this._accessToken != null)
				{
					SqlConnectionString connectionOptions = new SqlConnectionString(value);
					if (this._credential != null)
					{
						this.CheckAndThrowOnInvalidCombinationOfConnectionStringAndSqlCredential(connectionOptions);
					}
					else
					{
						this.CheckAndThrowOnInvalidCombinationOfConnectionOptionAndAccessToken(connectionOptions);
					}
				}
				this.ConnectionString_Set(new SqlConnectionPoolKey(value, this._credential, this._accessToken));
				this._connectionString = value;
				this.CacheConnectionStringProperties();
			}
		}

		/// <summary>Gets the time to wait while trying to establish a connection before terminating the attempt and generating an error.</summary>
		/// <returns>The time (in seconds) to wait for a connection to open. The default value is 15 seconds.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is less than 0.</exception>
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x000660D0 File Offset: 0x000642D0
		public override int ConnectionTimeout
		{
			get
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				if (sqlConnectionString == null)
				{
					return 15;
				}
				return sqlConnectionString.ConnectTimeout;
			}
		}

		/// <summary>Gets or sets the access token for the connection.</summary>
		/// <returns>The access token for the connection.</returns>
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x000660F8 File Offset: 0x000642F8
		// (set) Token: 0x06001647 RID: 5703 RVA: 0x00066138 File Offset: 0x00064338
		public string AccessToken
		{
			get
			{
				string accessToken = this._accessToken;
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.UserConnectionOptions;
				if (!this.InnerConnection.ShouldHidePassword || sqlConnectionString == null || sqlConnectionString.PersistSecurityInfo)
				{
					return this._accessToken;
				}
				return null;
			}
			set
			{
				if (!this.InnerConnection.AllowSetConnectionString)
				{
					throw ADP.OpenConnectionPropertySet("AccessToken", this.InnerConnection.State);
				}
				if (value != null)
				{
					this.CheckAndThrowOnInvalidCombinationOfConnectionOptionAndAccessToken((SqlConnectionString)this.ConnectionOptions);
				}
				this.ConnectionString_Set(new SqlConnectionPoolKey(this._connectionString, this._credential, value));
				this._accessToken = value;
			}
		}

		/// <summary>Gets the name of the current database or the database to be used after a connection is opened.</summary>
		/// <returns>The name of the current database or the name of the database to be used after a connection is opened. The default value is an empty string.</returns>
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0006619C File Offset: 0x0006439C
		public override string Database
		{
			get
			{
				SqlInternalConnection sqlInternalConnection = this.InnerConnection as SqlInternalConnection;
				string result;
				if (sqlInternalConnection != null)
				{
					result = sqlInternalConnection.CurrentDatabase;
				}
				else
				{
					SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
					result = ((sqlConnectionString != null) ? sqlConnectionString.InitialCatalog : "");
				}
				return result;
			}
		}

		/// <summary>Gets the name of the instance of SQL Server to which to connect.</summary>
		/// <returns>The name of the instance of SQL Server to which to connect. The default value is an empty string.</returns>
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x000661E0 File Offset: 0x000643E0
		public override string DataSource
		{
			get
			{
				SqlInternalConnection sqlInternalConnection = this.InnerConnection as SqlInternalConnection;
				string result;
				if (sqlInternalConnection != null)
				{
					result = sqlInternalConnection.CurrentDataSource;
				}
				else
				{
					SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
					result = ((sqlConnectionString != null) ? sqlConnectionString.DataSource : "");
				}
				return result;
			}
		}

		/// <summary>Gets the size (in bytes) of network packets used to communicate with an instance of SQL Server.</summary>
		/// <returns>The size (in bytes) of network packets. The default value is 8000.</returns>
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x00066224 File Offset: 0x00064424
		public int PacketSize
		{
			get
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
				int result;
				if (sqlInternalConnectionTds != null)
				{
					result = sqlInternalConnectionTds.PacketSize;
				}
				else
				{
					SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
					result = ((sqlConnectionString != null) ? sqlConnectionString.PacketSize : 8000);
				}
				return result;
			}
		}

		/// <summary>The connection ID of the most recent connection attempt, regardless of whether the attempt succeeded or failed.</summary>
		/// <returns>The connection ID of the most recent connection attempt.</returns>
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x00066268 File Offset: 0x00064468
		public Guid ClientConnectionId
		{
			get
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds != null)
				{
					return sqlInternalConnectionTds.ClientConnectionId;
				}
				Task currentReconnectionTask = this._currentReconnectionTask;
				if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
				{
					return this._originalConnectionId;
				}
				return Guid.Empty;
			}
		}

		/// <summary>Gets a string that contains the version of the instance of SQL Server to which the client is connected.</summary>
		/// <returns>The version of the instance of SQL Server.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection is closed.  
		///  <see cref="P:System.Data.SqlClient.SqlConnection.ServerVersion" /> was called while the returned Task was not completed and the connection was not opened after a call to <see cref="M:System.Data.SqlClient.SqlConnection.OpenAsync(System.Threading.CancellationToken)" />.</exception>
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x000662A9 File Offset: 0x000644A9
		public override string ServerVersion
		{
			get
			{
				return this.GetOpenTdsConnection().ServerVersion;
			}
		}

		/// <summary>Indicates the state of the <see cref="T:System.Data.SqlClient.SqlConnection" /> during the most recent network operation performed on the connection.</summary>
		/// <returns>An <see cref="T:System.Data.ConnectionState" /> enumeration.</returns>
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x000662B8 File Offset: 0x000644B8
		public override ConnectionState State
		{
			get
			{
				Task currentReconnectionTask = this._currentReconnectionTask;
				if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
				{
					return ConnectionState.Open;
				}
				return this.InnerConnection.State;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x000662E4 File Offset: 0x000644E4
		internal SqlStatistics Statistics
		{
			get
			{
				return this._statistics;
			}
		}

		/// <summary>Gets a string that identifies the database client.</summary>
		/// <returns>A string that identifies the database client. If not specified, the name of the client computer. If neither is specified, the value is an empty string.</returns>
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x000662EC File Offset: 0x000644EC
		public string WorkstationId
		{
			get
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				return ((sqlConnectionString != null) ? sqlConnectionString.WorkstationId : null) ?? Environment.MachineName;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlClient.SqlCredential" /> object for this connection.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlCredential" /> object for this connection.</returns>
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x00066310 File Offset: 0x00064510
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x0006634C File Offset: 0x0006454C
		public SqlCredential Credential
		{
			get
			{
				SqlCredential result = this._credential;
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.UserConnectionOptions;
				if (this.InnerConnection.ShouldHidePassword && sqlConnectionString != null && !sqlConnectionString.PersistSecurityInfo)
				{
					result = null;
				}
				return result;
			}
			set
			{
				if (!this.InnerConnection.AllowSetConnectionString)
				{
					throw ADP.OpenConnectionPropertySet("Credential", this.InnerConnection.State);
				}
				if (value != null)
				{
					this.CheckAndThrowOnInvalidCombinationOfConnectionStringAndSqlCredential((SqlConnectionString)this.ConnectionOptions);
					if (this._accessToken != null)
					{
						throw ADP.InvalidMixedUsageOfCredentialAndAccessToken();
					}
				}
				this._credential = value;
				this.ConnectionString_Set(new SqlConnectionPoolKey(this._connectionString, this._credential, this._accessToken));
			}
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x000663C2 File Offset: 0x000645C2
		private void CheckAndThrowOnInvalidCombinationOfConnectionStringAndSqlCredential(SqlConnectionString connectionOptions)
		{
			if (this.UsesClearUserIdOrPassword(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfSecureAndClearCredential();
			}
			if (this.UsesIntegratedSecurity(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity();
			}
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x000663E2 File Offset: 0x000645E2
		private void CheckAndThrowOnInvalidCombinationOfConnectionOptionAndAccessToken(SqlConnectionString connectionOptions)
		{
			if (this.UsesClearUserIdOrPassword(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfAccessTokenAndUserIDPassword();
			}
			if (this.UsesIntegratedSecurity(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfAccessTokenAndIntegratedSecurity();
			}
			if (this._credential != null)
			{
				throw ADP.InvalidMixedUsageOfCredentialAndAccessToken();
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x00066410 File Offset: 0x00064610
		protected override DbProviderFactory DbProviderFactory
		{
			get
			{
				return SqlClientFactory.Instance;
			}
		}

		/// <summary>Occurs when SQL Server returns a warning or informational message.</summary>
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001655 RID: 5717 RVA: 0x00066418 File Offset: 0x00064618
		// (remove) Token: 0x06001656 RID: 5718 RVA: 0x00066450 File Offset: 0x00064650
		public event SqlInfoMessageEventHandler InfoMessage
		{
			[CompilerGenerated]
			add
			{
				SqlInfoMessageEventHandler sqlInfoMessageEventHandler = this.InfoMessage;
				SqlInfoMessageEventHandler sqlInfoMessageEventHandler2;
				do
				{
					sqlInfoMessageEventHandler2 = sqlInfoMessageEventHandler;
					SqlInfoMessageEventHandler value2 = (SqlInfoMessageEventHandler)Delegate.Combine(sqlInfoMessageEventHandler2, value);
					sqlInfoMessageEventHandler = Interlocked.CompareExchange<SqlInfoMessageEventHandler>(ref this.InfoMessage, value2, sqlInfoMessageEventHandler2);
				}
				while (sqlInfoMessageEventHandler != sqlInfoMessageEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SqlInfoMessageEventHandler sqlInfoMessageEventHandler = this.InfoMessage;
				SqlInfoMessageEventHandler sqlInfoMessageEventHandler2;
				do
				{
					sqlInfoMessageEventHandler2 = sqlInfoMessageEventHandler;
					SqlInfoMessageEventHandler value2 = (SqlInfoMessageEventHandler)Delegate.Remove(sqlInfoMessageEventHandler2, value);
					sqlInfoMessageEventHandler = Interlocked.CompareExchange<SqlInfoMessageEventHandler>(ref this.InfoMessage, value2, sqlInfoMessageEventHandler2);
				}
				while (sqlInfoMessageEventHandler != sqlInfoMessageEventHandler2);
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Data.SqlClient.SqlConnection.FireInfoMessageEventOnUserErrors" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Data.SqlClient.SqlConnection.FireInfoMessageEventOnUserErrors" /> property has been set; otherwise <see langword="false" />.</returns>
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x00066485 File Offset: 0x00064685
		// (set) Token: 0x06001658 RID: 5720 RVA: 0x0006648D File Offset: 0x0006468D
		public bool FireInfoMessageEventOnUserErrors
		{
			get
			{
				return this._fireInfoMessageEventOnUserErrors;
			}
			set
			{
				this._fireInfoMessageEventOnUserErrors = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x00066496 File Offset: 0x00064696
		internal int ReconnectCount
		{
			get
			{
				return this._reconnectCount;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0006649E File Offset: 0x0006469E
		// (set) Token: 0x0600165B RID: 5723 RVA: 0x000664A6 File Offset: 0x000646A6
		internal bool ForceNewConnection
		{
			[CompilerGenerated]
			get
			{
				return this.<ForceNewConnection>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ForceNewConnection>k__BackingField = value;
			}
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x000664AF File Offset: 0x000646AF
		protected override void OnStateChange(StateChangeEventArgs stateChange)
		{
			if (!this._suppressStateChangeForReconnection)
			{
				base.OnStateChange(stateChange);
			}
		}

		/// <summary>Starts a database transaction.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Parallel transactions are not allowed when using Multiple Active Result Sets (MARS).</exception>
		/// <exception cref="T:System.InvalidOperationException">Parallel transactions are not supported.</exception>
		// Token: 0x0600165D RID: 5725 RVA: 0x000664C0 File Offset: 0x000646C0
		public new SqlTransaction BeginTransaction()
		{
			return this.BeginTransaction(IsolationLevel.Unspecified, null);
		}

		/// <summary>Starts a database transaction with the specified isolation level.</summary>
		/// <param name="iso">The isolation level under which the transaction should run.</param>
		/// <returns>An object representing the new transaction.</returns>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Parallel transactions are not allowed when using Multiple Active Result Sets (MARS).</exception>
		/// <exception cref="T:System.InvalidOperationException">Parallel transactions are not supported.</exception>
		// Token: 0x0600165E RID: 5726 RVA: 0x000664CA File Offset: 0x000646CA
		public new SqlTransaction BeginTransaction(IsolationLevel iso)
		{
			return this.BeginTransaction(iso, null);
		}

		/// <summary>Starts a database transaction with the specified transaction name.</summary>
		/// <param name="transactionName">The name of the transaction.</param>
		/// <returns>An object representing the new transaction.</returns>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Parallel transactions are not allowed when using Multiple Active Result Sets (MARS).</exception>
		/// <exception cref="T:System.InvalidOperationException">Parallel transactions are not supported.</exception>
		// Token: 0x0600165F RID: 5727 RVA: 0x000664D4 File Offset: 0x000646D4
		public SqlTransaction BeginTransaction(string transactionName)
		{
			return this.BeginTransaction(IsolationLevel.Unspecified, transactionName);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x000664DE File Offset: 0x000646DE
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			DbTransaction result = this.BeginTransaction(isolationLevel);
			GC.KeepAlive(this);
			return result;
		}

		/// <summary>Starts a database transaction with the specified isolation level and transaction name.</summary>
		/// <param name="iso">The isolation level under which the transaction should run.</param>
		/// <param name="transactionName">The name of the transaction.</param>
		/// <returns>An object representing the new transaction.</returns>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Parallel transactions are not allowed when using Multiple Active Result Sets (MARS).</exception>
		/// <exception cref="T:System.InvalidOperationException">Parallel transactions are not supported.</exception>
		// Token: 0x06001661 RID: 5729 RVA: 0x000664F0 File Offset: 0x000646F0
		public SqlTransaction BeginTransaction(IsolationLevel iso, string transactionName)
		{
			this.WaitForPendingReconnection();
			SqlStatistics statistics = null;
			SqlTransaction result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				bool shouldReconnect = true;
				SqlTransaction sqlTransaction;
				do
				{
					sqlTransaction = this.GetOpenTdsConnection().BeginSqlTransaction(iso, transactionName, shouldReconnect);
					shouldReconnect = false;
				}
				while (sqlTransaction.InternalTransaction.ConnectionHasBeenRestored);
				GC.KeepAlive(this);
				result = sqlTransaction;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		/// <summary>Changes the current database for an open <see cref="T:System.Data.SqlClient.SqlConnection" />.</summary>
		/// <param name="database">The name of the database to use instead of the current database.</param>
		/// <exception cref="T:System.ArgumentException">The database name is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The connection is not open.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Cannot change the database.</exception>
		// Token: 0x06001662 RID: 5730 RVA: 0x00066554 File Offset: 0x00064754
		public override void ChangeDatabase(string database)
		{
			SqlStatistics statistics = null;
			this.RepairInnerConnection();
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.InnerConnection.ChangeDatabase(database);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
		}

		/// <summary>Empties the connection pool.</summary>
		// Token: 0x06001663 RID: 5731 RVA: 0x0006659C File Offset: 0x0006479C
		public static void ClearAllPools()
		{
			SqlConnectionFactory.SingletonInstance.ClearAllPools();
		}

		/// <summary>Empties the connection pool associated with the specified connection.</summary>
		/// <param name="connection">The <see cref="T:System.Data.SqlClient.SqlConnection" /> to be cleared from the pool.</param>
		// Token: 0x06001664 RID: 5732 RVA: 0x000665A8 File Offset: 0x000647A8
		public static void ClearPool(SqlConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			DbConnectionOptions userConnectionOptions = connection.UserConnectionOptions;
			if (userConnectionOptions != null)
			{
				SqlConnectionFactory.SingletonInstance.ClearPool(connection);
			}
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x000665D5 File Offset: 0x000647D5
		private void CloseInnerConnection()
		{
			this.InnerConnection.CloseConnection(this, this.ConnectionFactory);
		}

		/// <summary>Closes the connection to the database. This is the preferred method of closing any open connection.</summary>
		/// <exception cref="T:System.Data.SqlClient.SqlException">The connection-level error that occurred while opening the connection.</exception>
		// Token: 0x06001666 RID: 5734 RVA: 0x000665EC File Offset: 0x000647EC
		public override void Close()
		{
			ConnectionState state = this.State;
			Guid operationId = default(Guid);
			Guid clientConnectionId = default(Guid);
			if (state != ConnectionState.Closed)
			{
				operationId = SqlConnection.s_diagnosticListener.WriteConnectionCloseBefore(this, "Close");
				clientConnectionId = this.ClientConnectionId;
			}
			SqlStatistics statistics = null;
			Exception ex = null;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				Task currentReconnectionTask = this._currentReconnectionTask;
				if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
				{
					CancellationTokenSource reconnectionCancellationSource = this._reconnectionCancellationSource;
					if (reconnectionCancellationSource != null)
					{
						reconnectionCancellationSource.Cancel();
					}
					AsyncHelper.WaitForCompletion(currentReconnectionTask, 0, null, false);
					if (this.State != ConnectionState.Open)
					{
						this.OnStateChange(DbConnectionInternal.StateChangeClosed);
					}
				}
				this.CancelOpenAndWait();
				this.CloseInnerConnection();
				GC.SuppressFinalize(this);
				if (this.Statistics != null)
				{
					ADP.TimerCurrent(out this._statistics._closeTimestamp);
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
				if (state != ConnectionState.Closed)
				{
					if (ex != null)
					{
						SqlConnection.s_diagnosticListener.WriteConnectionCloseError(operationId, clientConnectionId, this, ex, "Close");
					}
					else
					{
						SqlConnection.s_diagnosticListener.WriteConnectionCloseAfter(operationId, clientConnectionId, this, "Close");
					}
				}
			}
		}

		/// <summary>Creates and returns a <see cref="T:System.Data.SqlClient.SqlCommand" /> object associated with the <see cref="T:System.Data.SqlClient.SqlConnection" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> object.</returns>
		// Token: 0x06001667 RID: 5735 RVA: 0x00066704 File Offset: 0x00064904
		public new SqlCommand CreateCommand()
		{
			return new SqlCommand(null, this);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00066710 File Offset: 0x00064910
		private void DisposeMe(bool disposing)
		{
			this._credential = null;
			this._accessToken = null;
			if (!disposing)
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds != null && !sqlInternalConnectionTds.ConnectionOptions.Pooling)
				{
					TdsParser parser = sqlInternalConnectionTds.Parser;
					if (parser != null && parser._physicalStateObj != null)
					{
						parser._physicalStateObj.DecrementPendingCallbacks(false);
					}
				}
			}
		}

		/// <summary>Opens a database connection with the property settings specified by the <see cref="P:System.Data.SqlClient.SqlConnection.ConnectionString" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">Cannot open a connection without specifying a data source or server.  
		///  or  
		///  The connection is already open.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">A connection-level error occurred while opening the connection. If the <see cref="P:System.Data.SqlClient.SqlException.Number" /> property contains the value 18487 or 18488, this indicates that the specified password has expired or must be reset. See the <see cref="M:System.Data.SqlClient.SqlConnection.ChangePassword(System.String,System.String)" /> method for more information.  
		///  The <see langword="&lt;system.data.localdb&gt;" /> tag in the app.config file has invalid or unknown elements.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">There are two entries with the same name in the <see langword="&lt;localdbinstances&gt;" /> section.</exception>
		// Token: 0x06001669 RID: 5737 RVA: 0x0006676C File Offset: 0x0006496C
		public override void Open()
		{
			Guid operationId = SqlConnection.s_diagnosticListener.WriteConnectionOpenBefore(this, "Open");
			this.PrepareStatisticsForNewConnection();
			SqlStatistics statistics = null;
			Exception ex = null;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (!this.TryOpen(null))
				{
					throw ADP.InternalError(ADP.InternalErrorCode.SynchronousConnectReturnedPending);
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
				if (ex != null)
				{
					SqlConnection.s_diagnosticListener.WriteConnectionOpenError(operationId, this, ex, "Open");
				}
				else
				{
					SqlConnection.s_diagnosticListener.WriteConnectionOpenAfter(operationId, this, "Open");
				}
			}
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00066804 File Offset: 0x00064A04
		internal void RegisterWaitingForReconnect(Task waitingTask)
		{
			if (((SqlConnectionString)this.ConnectionOptions).MARS)
			{
				return;
			}
			Interlocked.CompareExchange<Task>(ref this._asyncWaitingForReconnection, waitingTask, null);
			if (this._asyncWaitingForReconnection != waitingTask)
			{
				throw SQL.MARSUnspportedOnConnection();
			}
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00066838 File Offset: 0x00064A38
		private Task ReconnectAsync(int timeout)
		{
			SqlConnection.<ReconnectAsync>d__97 <ReconnectAsync>d__;
			<ReconnectAsync>d__.<>4__this = this;
			<ReconnectAsync>d__.timeout = timeout;
			<ReconnectAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ReconnectAsync>d__.<>1__state = -1;
			<ReconnectAsync>d__.<>t__builder.Start<SqlConnection.<ReconnectAsync>d__97>(ref <ReconnectAsync>d__);
			return <ReconnectAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00066884 File Offset: 0x00064A84
		internal Task ValidateAndReconnect(Action beforeDisconnect, int timeout)
		{
			Task task = this._currentReconnectionTask;
			while (task != null && task.IsCompleted)
			{
				Interlocked.CompareExchange<Task>(ref this._currentReconnectionTask, null, task);
				task = this._currentReconnectionTask;
			}
			if (task == null)
			{
				if (this._connectRetryCount > 0)
				{
					SqlInternalConnectionTds openTdsConnection = this.GetOpenTdsConnection();
					if (openTdsConnection._sessionRecoveryAcknowledged && !openTdsConnection.Parser._physicalStateObj.ValidateSNIConnection())
					{
						if (openTdsConnection.Parser._sessionPool != null && openTdsConnection.Parser._sessionPool.ActiveSessionsCount > 0)
						{
							if (beforeDisconnect != null)
							{
								beforeDisconnect();
							}
							this.OnError(SQL.CR_UnrecoverableClient(this.ClientConnectionId), true, null);
						}
						SessionData currentSessionData = openTdsConnection.CurrentSessionData;
						if (currentSessionData._unrecoverableStatesCount == 0)
						{
							bool flag = false;
							object reconnectLock = this._reconnectLock;
							lock (reconnectLock)
							{
								openTdsConnection.CheckEnlistedTransactionBinding();
								task = this._currentReconnectionTask;
								if (task == null)
								{
									if (currentSessionData._unrecoverableStatesCount == 0)
									{
										this._originalConnectionId = this.ClientConnectionId;
										this._recoverySessionData = currentSessionData;
										if (beforeDisconnect != null)
										{
											beforeDisconnect();
										}
										try
										{
											this._suppressStateChangeForReconnection = true;
											openTdsConnection.DoomThisConnection();
										}
										catch (SqlException)
										{
										}
										task = Task.Run(() => this.ReconnectAsync(timeout));
										this._currentReconnectionTask = task;
									}
								}
								else
								{
									flag = true;
								}
							}
							if (flag && beforeDisconnect != null)
							{
								beforeDisconnect();
							}
						}
						else
						{
							if (beforeDisconnect != null)
							{
								beforeDisconnect();
							}
							this.OnError(SQL.CR_UnrecoverableServer(this.ClientConnectionId), true, null);
						}
					}
				}
			}
			else if (beforeDisconnect != null)
			{
				beforeDisconnect();
			}
			return task;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00066A34 File Offset: 0x00064C34
		private void WaitForPendingReconnection()
		{
			Task currentReconnectionTask = this._currentReconnectionTask;
			if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
			{
				AsyncHelper.WaitForCompletion(currentReconnectionTask, 0, null, false);
			}
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00066A5C File Offset: 0x00064C5C
		private void CancelOpenAndWait()
		{
			Tuple<TaskCompletionSource<DbConnectionInternal>, Task> currentCompletion = this._currentCompletion;
			if (currentCompletion != null)
			{
				currentCompletion.Item1.TrySetCanceled();
				((IAsyncResult)currentCompletion.Item2).AsyncWaitHandle.WaitOne();
			}
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlConnection.Open" />, which opens a database connection with the property settings specified by the <see cref="P:System.Data.SqlClient.SqlConnection.ConnectionString" />. The cancellation token can be used to request that the operation be abandoned before the connection timeout elapses.  Exceptions will be propagated via the returned Task. If the connection timeout time elapses without successfully connecting, the returned Task will be marked as faulted with an Exception. The implementation returns a Task without blocking the calling thread for both pooled and non-pooled connections.</summary>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlConnection.OpenAsync(System.Threading.CancellationToken)" /> more than once for the same instance before task completion.  
		///  <see langword="Context Connection=true" /> is specified in the connection string.  
		///  A connection was not available from the connection pool before the connection time out elapsed.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x0600166F RID: 5743 RVA: 0x00066A90 File Offset: 0x00064C90
		public override Task OpenAsync(CancellationToken cancellationToken)
		{
			Guid operationId = SqlConnection.s_diagnosticListener.WriteConnectionOpenBefore(this, "OpenAsync");
			this.PrepareStatisticsForNewConnection();
			SqlStatistics statistics = null;
			Task task;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				TaskCompletionSource<DbConnectionInternal> taskCompletionSource = new TaskCompletionSource<DbConnectionInternal>(ADP.GetCurrentTransaction());
				TaskCompletionSource<object> taskCompletionSource2 = new TaskCompletionSource<object>();
				if (SqlConnection.s_diagnosticListener.IsEnabled("System.Data.SqlClient.WriteConnectionOpenAfter") || SqlConnection.s_diagnosticListener.IsEnabled("System.Data.SqlClient.WriteConnectionOpenError"))
				{
					taskCompletionSource2.Task.ContinueWith(delegate(Task<object> t)
					{
						if (t.Exception != null)
						{
							SqlConnection.s_diagnosticListener.WriteConnectionOpenError(operationId, this, t.Exception, "OpenAsync");
							return;
						}
						SqlConnection.s_diagnosticListener.WriteConnectionOpenAfter(operationId, this, "OpenAsync");
					}, TaskScheduler.Default);
				}
				if (cancellationToken.IsCancellationRequested)
				{
					taskCompletionSource2.SetCanceled();
					task = taskCompletionSource2.Task;
				}
				else
				{
					bool flag;
					try
					{
						flag = this.TryOpen(taskCompletionSource);
					}
					catch (Exception ex)
					{
						SqlConnection.s_diagnosticListener.WriteConnectionOpenError(operationId, this, ex, "OpenAsync");
						taskCompletionSource2.SetException(ex);
						return taskCompletionSource2.Task;
					}
					if (flag)
					{
						taskCompletionSource2.SetResult(null);
						task = taskCompletionSource2.Task;
					}
					else
					{
						CancellationTokenRegistration registration = default(CancellationTokenRegistration);
						if (cancellationToken.CanBeCanceled)
						{
							registration = cancellationToken.Register(delegate(object s)
							{
								((TaskCompletionSource<DbConnectionInternal>)s).TrySetCanceled();
							}, taskCompletionSource);
						}
						SqlConnection.OpenAsyncRetry @object = new SqlConnection.OpenAsyncRetry(this, taskCompletionSource, taskCompletionSource2, registration);
						this._currentCompletion = new Tuple<TaskCompletionSource<DbConnectionInternal>, Task>(taskCompletionSource, taskCompletionSource2.Task);
						taskCompletionSource.Task.ContinueWith(new Action<Task<DbConnectionInternal>>(@object.Retry), TaskScheduler.Default);
						task = taskCompletionSource2.Task;
					}
				}
			}
			catch (Exception ex2)
			{
				SqlConnection.s_diagnosticListener.WriteConnectionOpenError(operationId, this, ex2, "OpenAsync");
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return task;
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.SqlClient.SqlConnection" />. For more information about scheme, see SQL Server Schema Collections.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		// Token: 0x06001670 RID: 5744 RVA: 0x00066C88 File Offset: 0x00064E88
		public override DataTable GetSchema()
		{
			return this.GetSchema(DbMetaDataCollectionNames.MetaDataCollections, null);
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.SqlClient.SqlConnection" /> using the specified string for the schema name.</summary>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collectionName" /> is specified as null.</exception>
		// Token: 0x06001671 RID: 5745 RVA: 0x00066C96 File Offset: 0x00064E96
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, null);
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.SqlClient.SqlConnection" /> using the specified string for the schema name and the specified string array for the restriction values.</summary>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <param name="restrictionValues">A set of restriction values for the requested schema.</param>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collectionName" /> is specified as null.</exception>
		// Token: 0x06001672 RID: 5746 RVA: 0x00066CA0 File Offset: 0x00064EA0
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			return this.InnerConnection.GetSchema(this.ConnectionFactory, this.PoolGroup, this, collectionName, restrictionValues);
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x00066CBC File Offset: 0x00064EBC
		private void PrepareStatisticsForNewConnection()
		{
			if (this.StatisticsEnabled || SqlConnection.s_diagnosticListener.IsEnabled("System.Data.SqlClient.WriteCommandAfter") || SqlConnection.s_diagnosticListener.IsEnabled("System.Data.SqlClient.WriteConnectionOpenAfter"))
			{
				if (this._statistics == null)
				{
					this._statistics = new SqlStatistics();
					return;
				}
				this._statistics.ContinueOnNewConnection();
			}
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x00066D14 File Offset: 0x00064F14
		private bool TryOpen(TaskCompletionSource<DbConnectionInternal> retry)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
			this._applyTransientFaultHandling = (retry == null && sqlConnectionString != null && sqlConnectionString.ConnectRetryCount > 0);
			if (this.ForceNewConnection)
			{
				if (!this.InnerConnection.TryReplaceConnection(this, this.ConnectionFactory, retry, this.UserConnectionOptions))
				{
					return false;
				}
			}
			else if (!this.InnerConnection.TryOpenConnection(this, this.ConnectionFactory, retry, this.UserConnectionOptions))
			{
				return false;
			}
			SqlInternalConnectionTds sqlInternalConnectionTds = (SqlInternalConnectionTds)this.InnerConnection;
			if (!sqlInternalConnectionTds.ConnectionOptions.Pooling)
			{
				GC.ReRegisterForFinalize(this);
			}
			SqlStatistics statistics = this._statistics;
			if (this.StatisticsEnabled || (SqlConnection.s_diagnosticListener.IsEnabled("System.Data.SqlClient.WriteCommandAfter") && statistics != null))
			{
				ADP.TimerCurrent(out this._statistics._openTimestamp);
				sqlInternalConnectionTds.Parser.Statistics = this._statistics;
			}
			else
			{
				sqlInternalConnectionTds.Parser.Statistics = null;
				this._statistics = null;
			}
			return true;
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x00066E00 File Offset: 0x00065000
		internal bool HasLocalTransaction
		{
			get
			{
				return this.GetOpenTdsConnection().HasLocalTransaction;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x00066E10 File Offset: 0x00065010
		internal bool HasLocalTransactionFromAPI
		{
			get
			{
				Task currentReconnectionTask = this._currentReconnectionTask;
				return (currentReconnectionTask == null || currentReconnectionTask.IsCompleted) && this.GetOpenTdsConnection().HasLocalTransactionFromAPI;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x00066E3C File Offset: 0x0006503C
		internal bool IsKatmaiOrNewer
		{
			get
			{
				return this._currentReconnectionTask != null || this.GetOpenTdsConnection().IsKatmaiOrNewer;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x00066E53 File Offset: 0x00065053
		internal TdsParser Parser
		{
			get
			{
				return this.GetOpenTdsConnection().Parser;
			}
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x00066E60 File Offset: 0x00065060
		internal void ValidateConnectionForExecute(string method, SqlCommand command)
		{
			Task asyncWaitingForReconnection = this._asyncWaitingForReconnection;
			if (asyncWaitingForReconnection != null)
			{
				if (!asyncWaitingForReconnection.IsCompleted)
				{
					throw SQL.MARSUnspportedOnConnection();
				}
				Interlocked.CompareExchange<Task>(ref this._asyncWaitingForReconnection, null, asyncWaitingForReconnection);
			}
			if (this._currentReconnectionTask != null)
			{
				Task currentReconnectionTask = this._currentReconnectionTask;
				if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
				{
					return;
				}
			}
			this.GetOpenTdsConnection(method).ValidateConnectionForExecute(command);
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x00066EBB File Offset: 0x000650BB
		internal static string FixupDatabaseTransactionName(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				return SqlServerEscapeHelper.EscapeIdentifier(name);
			}
			return name;
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x00066ED0 File Offset: 0x000650D0
		internal void OnError(SqlException exception, bool breakConnection, Action<Action> wrapCloseInAction)
		{
			if (breakConnection && ConnectionState.Open == this.State)
			{
				if (wrapCloseInAction != null)
				{
					int capturedCloseCount = this._closeCount;
					Action obj = delegate()
					{
						if (capturedCloseCount == this._closeCount)
						{
							this.Close();
						}
					};
					wrapCloseInAction(obj);
				}
				else
				{
					this.Close();
				}
			}
			if (exception.Class >= 11)
			{
				throw exception;
			}
			this.OnInfoMessage(new SqlInfoMessageEventArgs(exception));
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00066F3C File Offset: 0x0006513C
		internal SqlInternalConnectionTds GetOpenTdsConnection()
		{
			SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
			if (sqlInternalConnectionTds == null)
			{
				throw ADP.ClosedConnectionError();
			}
			return sqlInternalConnectionTds;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00066F60 File Offset: 0x00065160
		internal SqlInternalConnectionTds GetOpenTdsConnection(string method)
		{
			SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
			if (sqlInternalConnectionTds == null)
			{
				throw ADP.OpenConnectionRequired(method, this.InnerConnection.State);
			}
			return sqlInternalConnectionTds;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00066F90 File Offset: 0x00065190
		internal void OnInfoMessage(SqlInfoMessageEventArgs imevent)
		{
			bool flag;
			this.OnInfoMessage(imevent, out flag);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00066FA8 File Offset: 0x000651A8
		internal void OnInfoMessage(SqlInfoMessageEventArgs imevent, out bool notified)
		{
			SqlInfoMessageEventHandler infoMessage = this.InfoMessage;
			if (infoMessage != null)
			{
				notified = true;
				try
				{
					infoMessage(this, imevent);
					return;
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableOrSecurityExceptionType(e))
					{
						throw;
					}
					return;
				}
			}
			notified = false;
		}

		/// <summary>Changes the SQL Server password for the user indicated in the connection string to the supplied new password.</summary>
		/// <param name="connectionString">The connection string that contains enough information to connect to the server that you want. The connection string must contain the user ID and the current password.</param>
		/// <param name="newPassword">The new password to set. This password must comply with any password security policy set on the server, including minimum length, requirements for specific characters, and so on.</param>
		/// <exception cref="T:System.ArgumentException">The connection string includes the option to use integrated security.  
		///  Or  
		///  The <paramref name="newPassword" /> exceeds 128 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">Either the <paramref name="connectionString" /> or the <paramref name="newPassword" /> parameter is null.</exception>
		// Token: 0x06001680 RID: 5760 RVA: 0x00066FEC File Offset: 0x000651EC
		public static void ChangePassword(string connectionString, string newPassword)
		{
			if (string.IsNullOrEmpty(connectionString))
			{
				throw SQL.ChangePasswordArgumentMissing("newPassword");
			}
			if (string.IsNullOrEmpty(newPassword))
			{
				throw SQL.ChangePasswordArgumentMissing("newPassword");
			}
			if (128 < newPassword.Length)
			{
				throw ADP.InvalidArgumentLength("newPassword", 128);
			}
			SqlConnectionString sqlConnectionString = SqlConnectionFactory.FindSqlConnectionOptions(new SqlConnectionPoolKey(connectionString, null, null));
			if (sqlConnectionString.IntegratedSecurity)
			{
				throw SQL.ChangePasswordConflictsWithSSPI();
			}
			if (!string.IsNullOrEmpty(sqlConnectionString.AttachDBFilename))
			{
				throw SQL.ChangePasswordUseOfUnallowedKey("attachdbfilename");
			}
			SqlConnection.ChangePassword(connectionString, sqlConnectionString, null, newPassword, null);
		}

		/// <summary>Changes the SQL Server password for the user indicated in the <see cref="T:System.Data.SqlClient.SqlCredential" /> object.</summary>
		/// <param name="connectionString">The connection string that contains enough information to connect to a server. The connection string should not use any of the following connection string keywords: <see langword="Integrated Security = true" />, <see langword="UserId" />, or <see langword="Password" />; or <see langword="ContextConnection = true" />.</param>
		/// <param name="credential">A <see cref="T:System.Data.SqlClient.SqlCredential" /> object.</param>
		/// <param name="newSecurePassword">The new password. <paramref name="newSecurePassword" /> must be read only. The password must also comply with any password security policy set on the server (for example, minimum length and requirements for specific characters).</param>
		/// <exception cref="T:System.ArgumentException">The connection string contains any combination of <see langword="UserId" />, <see langword="Password" />, or <see langword="Integrated Security=true" />.
		/// -or-
		/// The connection string contains <see langword="Context Connection=true" />.  
		/// -or-
		/// <paramref name="newSecurePassword" /> (or <paramref name="newPassword" />) is greater than 128 characters.
		/// -or-
		/// <paramref name="newSecurePassword" /> (or <paramref name="newPassword" />) is not read only.
		/// -or-
		/// <paramref name="newSecurePassword" /> (or <paramref name="newPassword" />) is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">One of the parameters (<paramref name="connectionString" />, <paramref name="credential" />, or <paramref name="newSecurePassword" />) is null.</exception>
		// Token: 0x06001681 RID: 5761 RVA: 0x0006707C File Offset: 0x0006527C
		public static void ChangePassword(string connectionString, SqlCredential credential, SecureString newSecurePassword)
		{
			if (string.IsNullOrEmpty(connectionString))
			{
				throw SQL.ChangePasswordArgumentMissing("connectionString");
			}
			if (credential == null)
			{
				throw SQL.ChangePasswordArgumentMissing("credential");
			}
			if (newSecurePassword == null || newSecurePassword.Length == 0)
			{
				throw SQL.ChangePasswordArgumentMissing("newSecurePassword");
			}
			if (!newSecurePassword.IsReadOnly())
			{
				throw ADP.MustBeReadOnly("newSecurePassword");
			}
			if (128 < newSecurePassword.Length)
			{
				throw ADP.InvalidArgumentLength("newSecurePassword", 128);
			}
			SqlConnectionString sqlConnectionString = SqlConnectionFactory.FindSqlConnectionOptions(new SqlConnectionPoolKey(connectionString, null, null));
			if (!string.IsNullOrEmpty(sqlConnectionString.UserID) || !string.IsNullOrEmpty(sqlConnectionString.Password))
			{
				throw ADP.InvalidMixedArgumentOfSecureAndClearCredential();
			}
			if (sqlConnectionString.IntegratedSecurity)
			{
				throw SQL.ChangePasswordConflictsWithSSPI();
			}
			if (!string.IsNullOrEmpty(sqlConnectionString.AttachDBFilename))
			{
				throw SQL.ChangePasswordUseOfUnallowedKey("attachdbfilename");
			}
			SqlConnection.ChangePassword(connectionString, sqlConnectionString, credential, null, newSecurePassword);
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00067150 File Offset: 0x00065350
		private static void ChangePassword(string connectionString, SqlConnectionString connectionOptions, SqlCredential credential, string newPassword, SecureString newSecurePassword)
		{
			SqlInternalConnectionTds sqlInternalConnectionTds = null;
			try
			{
				sqlInternalConnectionTds = new SqlInternalConnectionTds(null, connectionOptions, credential, null, newPassword, newSecurePassword, false, null, null, false, null);
			}
			finally
			{
				if (sqlInternalConnectionTds != null)
				{
					sqlInternalConnectionTds.Dispose();
				}
			}
			SqlConnectionPoolKey key = new SqlConnectionPoolKey(connectionString, null, null);
			SqlConnectionFactory.SingletonInstance.ClearPool(key);
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x000671A4 File Offset: 0x000653A4
		internal void RegisterForConnectionCloseNotification<T>(ref Task<T> outerTask, object value, int tag)
		{
			outerTask = outerTask.ContinueWith<Task<T>>(delegate(Task<T> task)
			{
				this.RemoveWeakReference(value);
				return task;
			}, TaskScheduler.Default).Unwrap<T>();
		}

		/// <summary>If statistics gathering is enabled, all values are reset to zero.</summary>
		// Token: 0x06001684 RID: 5764 RVA: 0x000671E4 File Offset: 0x000653E4
		public void ResetStatistics()
		{
			if (this.Statistics != null)
			{
				this.Statistics.Reset();
				if (ConnectionState.Open == this.State)
				{
					ADP.TimerCurrent(out this._statistics._openTimestamp);
				}
			}
		}

		/// <summary>Returns a name value pair collection of statistics at the point in time the method is called.</summary>
		/// <returns>Returns a reference of type <see cref="T:System.Collections.IDictionary" /> of <see cref="T:System.Collections.DictionaryEntry" /> items.</returns>
		// Token: 0x06001685 RID: 5765 RVA: 0x00067212 File Offset: 0x00065412
		public IDictionary RetrieveStatistics()
		{
			if (this.Statistics != null)
			{
				this.UpdateStatistics();
				return this.Statistics.GetDictionary();
			}
			return new SqlStatistics().GetDictionary();
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00067238 File Offset: 0x00065438
		private void UpdateStatistics()
		{
			if (ConnectionState.Open == this.State)
			{
				ADP.TimerCurrent(out this._statistics._closeTimestamp);
			}
			this.Statistics.UpdateStatistics();
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		// Token: 0x06001687 RID: 5767 RVA: 0x0006725E File Offset: 0x0006545E
		object ICloneable.Clone()
		{
			return new SqlConnection(this);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00067268 File Offset: 0x00065468
		private void CopyFrom(SqlConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			this._userConnectionOptions = connection.UserConnectionOptions;
			this._poolGroup = connection.PoolGroup;
			if (DbConnectionClosedNeverOpened.SingletonInstance == connection._innerConnection)
			{
				this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
				return;
			}
			this._innerConnection = DbConnectionClosedPreviouslyOpened.SingletonInstance;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x000672BC File Offset: 0x000654BC
		private Assembly ResolveTypeAssembly(AssemblyName asmRef, bool throwOnError)
		{
			if (string.Compare(asmRef.Name, "Microsoft.SqlServer.Types", StringComparison.OrdinalIgnoreCase) == 0)
			{
				asmRef.Version = this.TypeSystemAssemblyVersion;
			}
			Assembly result;
			try
			{
				result = Assembly.Load(asmRef);
			}
			catch (Exception e)
			{
				if (throwOnError || !ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00067318 File Offset: 0x00065518
		internal void CheckGetExtendedUDTInfo(SqlMetaDataPriv metaData, bool fThrow)
		{
			if (metaData.udtType == null)
			{
				metaData.udtType = Type.GetType(metaData.udtAssemblyQualifiedName, (AssemblyName asmRef) => this.ResolveTypeAssembly(asmRef, fThrow), null, fThrow);
				if (fThrow && metaData.udtType == null)
				{
					throw SQL.UDTUnexpectedResult(metaData.udtAssemblyQualifiedName);
				}
			}
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00067390 File Offset: 0x00065590
		internal object GetUdtValue(object value, SqlMetaDataPriv metaData, bool returnDBNull)
		{
			if (returnDBNull && ADP.IsNull(value))
			{
				return DBNull.Value;
			}
			if (ADP.IsNull(value))
			{
				return metaData.udtType.InvokeMember("Null", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, new object[0], CultureInfo.InvariantCulture);
			}
			return SerializationHelperSql9.Deserialize(new MemoryStream((byte[])value), metaData.udtType);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x000673F0 File Offset: 0x000655F0
		internal byte[] GetBytes(object o)
		{
			Format format = Format.Native;
			int num;
			return this.GetBytes(o, out format, out num);
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0006740C File Offset: 0x0006560C
		internal byte[] GetBytes(object o, out Format format, out int maxSize)
		{
			SqlUdtInfo infoFromType = this.GetInfoFromType(o.GetType());
			maxSize = infoFromType.MaxByteSize;
			format = infoFromType.SerializationFormat;
			if (maxSize < -1 || maxSize >= 65535)
			{
				Type type = o.GetType();
				throw new InvalidOperationException(((type != null) ? type.ToString() : null) + ": invalid Size");
			}
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream((maxSize < 0) ? 0 : maxSize))
			{
				SerializationHelperSql9.Serialize(memoryStream, o);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x000674A4 File Offset: 0x000656A4
		private SqlUdtInfo GetInfoFromType(Type t)
		{
			Type type = t;
			SqlUdtInfo sqlUdtInfo;
			for (;;)
			{
				sqlUdtInfo = SqlUdtInfo.TryGetFromType(t);
				if (sqlUdtInfo != null)
				{
					break;
				}
				t = t.BaseType;
				if (!(t != null))
				{
					goto Block_2;
				}
			}
			return sqlUdtInfo;
			Block_2:
			throw SQL.UDTInvalidSqlType(type.AssemblyQualifiedName);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlConnection" /> class.</summary>
		// Token: 0x0600168F RID: 5775 RVA: 0x000674DB File Offset: 0x000656DB
		public SqlConnection()
		{
			this._reconnectLock = new object();
			this._originalConnectionId = Guid.Empty;
			base..ctor();
			GC.SuppressFinalize(this);
			this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x0006750A File Offset: 0x0006570A
		internal int CloseCount
		{
			get
			{
				return this._closeCount;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x00067512 File Offset: 0x00065712
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return SqlConnection.s_connectionFactory;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x0006751C File Offset: 0x0006571C
		internal DbConnectionOptions ConnectionOptions
		{
			get
			{
				DbConnectionPoolGroup poolGroup = this.PoolGroup;
				if (poolGroup == null)
				{
					return null;
				}
				return poolGroup.ConnectionOptions;
			}
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0006753C File Offset: 0x0006573C
		private string ConnectionString_Get()
		{
			bool shouldHidePassword = this.InnerConnection.ShouldHidePassword;
			DbConnectionOptions userConnectionOptions = this.UserConnectionOptions;
			if (userConnectionOptions == null)
			{
				return "";
			}
			return userConnectionOptions.UsersConnectionString(shouldHidePassword);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0006756C File Offset: 0x0006576C
		private void ConnectionString_Set(DbConnectionPoolKey key)
		{
			DbConnectionOptions userConnectionOptions = null;
			DbConnectionPoolGroup connectionPoolGroup = this.ConnectionFactory.GetConnectionPoolGroup(key, null, ref userConnectionOptions);
			DbConnectionInternal innerConnection = this.InnerConnection;
			bool flag = innerConnection.AllowSetConnectionString;
			if (flag)
			{
				flag = this.SetInnerConnectionFrom(DbConnectionClosedBusy.SingletonInstance, innerConnection);
				if (flag)
				{
					this._userConnectionOptions = userConnectionOptions;
					this._poolGroup = connectionPoolGroup;
					this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
				}
			}
			if (!flag)
			{
				throw ADP.OpenConnectionPropertySet("ConnectionString", innerConnection.State);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x000675D9 File Offset: 0x000657D9
		internal DbConnectionInternal InnerConnection
		{
			get
			{
				return this._innerConnection;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x000675E1 File Offset: 0x000657E1
		// (set) Token: 0x06001697 RID: 5783 RVA: 0x000675E9 File Offset: 0x000657E9
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._poolGroup;
			}
			set
			{
				this._poolGroup = value;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x000675F2 File Offset: 0x000657F2
		internal DbConnectionOptions UserConnectionOptions
		{
			get
			{
				return this._userConnectionOptions;
			}
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x000675FC File Offset: 0x000657FC
		internal void Abort(Exception e)
		{
			DbConnectionInternal innerConnection = this._innerConnection;
			if (ConnectionState.Open == innerConnection.State)
			{
				Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, DbConnectionClosedPreviouslyOpened.SingletonInstance, innerConnection);
				innerConnection.DoomThisConnection();
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00067631 File Offset: 0x00065831
		internal void AddWeakReference(object value, int tag)
		{
			this.InnerConnection.AddWeakReference(value, tag);
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00067640 File Offset: 0x00065840
		protected override DbCommand CreateDbCommand()
		{
			DbCommand dbCommand = this.ConnectionFactory.ProviderFactory.CreateCommand();
			dbCommand.Connection = this;
			return dbCommand;
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00067659 File Offset: 0x00065859
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._userConnectionOptions = null;
				this._poolGroup = null;
				this.Close();
			}
			this.DisposeMe(disposing);
			base.Dispose(disposing);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00067680 File Offset: 0x00065880
		private void RepairInnerConnection()
		{
			this.WaitForPendingReconnection();
			if (this._connectRetryCount == 0)
			{
				return;
			}
			SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
			if (sqlInternalConnectionTds != null)
			{
				sqlInternalConnectionTds.ValidateConnectionForExecute(null);
				sqlInternalConnectionTds.GetSessionAndReconnectIfNeeded(this, 0);
			}
		}

		/// <summary>Enlists in the specified transaction as a distributed transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.Transactions.Transaction" /> in which to enlist.</param>
		// Token: 0x0600169E RID: 5790 RVA: 0x000676BC File Offset: 0x000658BC
		public override void EnlistTransaction(Transaction transaction)
		{
			Transaction enlistedTransaction = this.InnerConnection.EnlistedTransaction;
			if (enlistedTransaction != null)
			{
				if (enlistedTransaction.Equals(transaction))
				{
					return;
				}
				if (enlistedTransaction.TransactionInformation.Status == System.Transactions.TransactionStatus.Active)
				{
					throw ADP.TransactionPresent();
				}
			}
			this.RepairInnerConnection();
			this.InnerConnection.EnlistTransaction(transaction);
			GC.KeepAlive(this);
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00067713 File Offset: 0x00065913
		internal void NotifyWeakReference(int message)
		{
			this.InnerConnection.NotifyWeakReference(message);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00067724 File Offset: 0x00065924
		internal void PermissionDemand()
		{
			DbConnectionPoolGroup poolGroup = this.PoolGroup;
			DbConnectionOptions dbConnectionOptions = (poolGroup != null) ? poolGroup.ConnectionOptions : null;
			if (dbConnectionOptions == null || dbConnectionOptions.IsEmpty)
			{
				throw ADP.NoConnectionString();
			}
			DbConnectionOptions userConnectionOptions = this.UserConnectionOptions;
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0006775D File Offset: 0x0006595D
		internal void RemoveWeakReference(object value)
		{
			this.InnerConnection.RemoveWeakReference(value);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0006776C File Offset: 0x0006596C
		internal void SetInnerConnectionEvent(DbConnectionInternal to)
		{
			ConnectionState connectionState = this._innerConnection.State & ConnectionState.Open;
			ConnectionState connectionState2 = to.State & ConnectionState.Open;
			if (connectionState != connectionState2 && connectionState2 == ConnectionState.Closed)
			{
				this._closeCount++;
			}
			this._innerConnection = to;
			if (connectionState == ConnectionState.Closed && ConnectionState.Open == connectionState2)
			{
				this.OnStateChange(DbConnectionInternal.StateChangeOpen);
				return;
			}
			if (ConnectionState.Open == connectionState && connectionState2 == ConnectionState.Closed)
			{
				this.OnStateChange(DbConnectionInternal.StateChangeClosed);
				return;
			}
			if (connectionState != connectionState2)
			{
				this.OnStateChange(new StateChangeEventArgs(connectionState, connectionState2));
			}
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x000677E3 File Offset: 0x000659E3
		internal bool SetInnerConnectionFrom(DbConnectionInternal to, DbConnectionInternal from)
		{
			return from == Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, to, from);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x000677F5 File Offset: 0x000659F5
		internal void SetInnerConnectionTo(DbConnectionInternal to)
		{
			this._innerConnection = to;
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x00010C60 File Offset: 0x0000EE60
		// (set) Token: 0x060016A6 RID: 5798 RVA: 0x00010C60 File Offset: 0x0000EE60
		[MonoTODO]
		public SqlCredential Credentials
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Enlists in the specified transaction as a distributed transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.EnterpriseServices.ITransaction" /> in which to enlist.</param>
		// Token: 0x060016A7 RID: 5799 RVA: 0x00010C60 File Offset: 0x0000EE60
		[MonoTODO]
		public void EnlistDistributedTransaction(ITransaction transaction)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x000677FE File Offset: 0x000659FE
		// Note: this type is marked as 'beforefieldinit'.
		static SqlConnection()
		{
		}

		/// <summary>Gets or sets the time-to-live for column encryption key entries in the column encryption key cache for the Always Encrypted feature. The default value is 2 hours. 0 means no caching at all.</summary>
		/// <returns>The time interval.</returns>
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x0006781C File Offset: 0x00065A1C
		// (set) Token: 0x060016AA RID: 5802 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public static TimeSpan ColumnEncryptionKeyCacheTtl
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(TimeSpan);
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether query metadata caching is enabled (true) or not (false) for parameterized queries running against Always Encrypted enabled databases. The default value is true.</summary>
		/// <returns>Returns true if query metadata caching is enabled; otherwise false. true is the default.</returns>
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x00067838 File Offset: 0x00065A38
		// (set) Token: 0x060016AC RID: 5804 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public static bool ColumnEncryptionQueryMetadataCacheEnabled
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Allows you to set a list of trusted key paths for a database server. If while processing an application query the driver receives a key path that is not on the list, the query will fail. This property provides additional protection against security attacks that involve a compromised SQL Server providing fake key paths, which may lead to leaking key store credentials.</summary>
		/// <returns>The list of trusted master key paths for the column encryption.</returns>
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x00067853 File Offset: 0x00065A53
		public static IDictionary<string, IList<string>> ColumnEncryptionTrustedMasterKeyPaths
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Registers the column encryption key store providers.</summary>
		/// <param name="customProviders">The custom providers</param>
		// Token: 0x060016AE RID: 5806 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public static void RegisterColumnEncryptionKeyStoreProviders(IDictionary<string, SqlColumnEncryptionKeyStoreProvider> customProviders)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000E12 RID: 3602
		private bool _AsyncCommandInProgress;

		// Token: 0x04000E13 RID: 3603
		internal SqlStatistics _statistics;

		// Token: 0x04000E14 RID: 3604
		private bool _collectstats;

		// Token: 0x04000E15 RID: 3605
		private bool _fireInfoMessageEventOnUserErrors;

		// Token: 0x04000E16 RID: 3606
		private Tuple<TaskCompletionSource<DbConnectionInternal>, Task> _currentCompletion;

		// Token: 0x04000E17 RID: 3607
		private SqlCredential _credential;

		// Token: 0x04000E18 RID: 3608
		private string _connectionString;

		// Token: 0x04000E19 RID: 3609
		private int _connectRetryCount;

		// Token: 0x04000E1A RID: 3610
		private string _accessToken;

		// Token: 0x04000E1B RID: 3611
		private object _reconnectLock;

		// Token: 0x04000E1C RID: 3612
		internal Task _currentReconnectionTask;

		// Token: 0x04000E1D RID: 3613
		private Task _asyncWaitingForReconnection;

		// Token: 0x04000E1E RID: 3614
		private Guid _originalConnectionId;

		// Token: 0x04000E1F RID: 3615
		private CancellationTokenSource _reconnectionCancellationSource;

		// Token: 0x04000E20 RID: 3616
		internal SessionData _recoverySessionData;

		// Token: 0x04000E21 RID: 3617
		internal new bool _suppressStateChangeForReconnection;

		// Token: 0x04000E22 RID: 3618
		private int _reconnectCount;

		// Token: 0x04000E23 RID: 3619
		private static readonly DiagnosticListener s_diagnosticListener = new DiagnosticListener("SqlClientDiagnosticListener");

		// Token: 0x04000E24 RID: 3620
		internal bool _applyTransientFaultHandling;

		// Token: 0x04000E25 RID: 3621
		[CompilerGenerated]
		private SqlInfoMessageEventHandler InfoMessage;

		// Token: 0x04000E26 RID: 3622
		[CompilerGenerated]
		private bool <ForceNewConnection>k__BackingField;

		// Token: 0x04000E27 RID: 3623
		private static readonly DbConnectionFactory s_connectionFactory = SqlConnectionFactory.SingletonInstance;

		// Token: 0x04000E28 RID: 3624
		private DbConnectionOptions _userConnectionOptions;

		// Token: 0x04000E29 RID: 3625
		private DbConnectionPoolGroup _poolGroup;

		// Token: 0x04000E2A RID: 3626
		private DbConnectionInternal _innerConnection;

		// Token: 0x04000E2B RID: 3627
		private int _closeCount;

		// Token: 0x020001C9 RID: 457
		private class OpenAsyncRetry
		{
			// Token: 0x060016AF RID: 5807 RVA: 0x0006785B File Offset: 0x00065A5B
			public OpenAsyncRetry(SqlConnection parent, TaskCompletionSource<DbConnectionInternal> retry, TaskCompletionSource<object> result, CancellationTokenRegistration registration)
			{
				this._parent = parent;
				this._retry = retry;
				this._result = result;
				this._registration = registration;
			}

			// Token: 0x060016B0 RID: 5808 RVA: 0x00067880 File Offset: 0x00065A80
			internal void Retry(Task<DbConnectionInternal> retryTask)
			{
				this._registration.Dispose();
				try
				{
					SqlStatistics statistics = null;
					try
					{
						statistics = SqlStatistics.StartTimer(this._parent.Statistics);
						if (retryTask.IsFaulted)
						{
							Exception innerException = retryTask.Exception.InnerException;
							this._parent.CloseInnerConnection();
							this._parent._currentCompletion = null;
							this._result.SetException(retryTask.Exception.InnerException);
						}
						else if (retryTask.IsCanceled)
						{
							this._parent.CloseInnerConnection();
							this._parent._currentCompletion = null;
							this._result.SetCanceled();
						}
						else
						{
							DbConnectionInternal innerConnection = this._parent.InnerConnection;
							bool flag2;
							lock (innerConnection)
							{
								flag2 = this._parent.TryOpen(this._retry);
							}
							if (flag2)
							{
								this._parent._currentCompletion = null;
								this._result.SetResult(null);
							}
							else
							{
								this._parent.CloseInnerConnection();
								this._parent._currentCompletion = null;
								this._result.SetException(ADP.ExceptionWithStackTrace(ADP.InternalError(ADP.InternalErrorCode.CompletedConnectReturnedPending)));
							}
						}
					}
					finally
					{
						SqlStatistics.StopTimer(statistics);
					}
				}
				catch (Exception exception)
				{
					this._parent.CloseInnerConnection();
					this._parent._currentCompletion = null;
					this._result.SetException(exception);
				}
			}

			// Token: 0x04000E2C RID: 3628
			private SqlConnection _parent;

			// Token: 0x04000E2D RID: 3629
			private TaskCompletionSource<DbConnectionInternal> _retry;

			// Token: 0x04000E2E RID: 3630
			private TaskCompletionSource<object> _result;

			// Token: 0x04000E2F RID: 3631
			private CancellationTokenRegistration _registration;
		}

		// Token: 0x020001CA RID: 458
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReconnectAsync>d__97 : IAsyncStateMachine
		{
			// Token: 0x060016B1 RID: 5809 RVA: 0x00067A1C File Offset: 0x00065C1C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				SqlConnection sqlConnection = this.<>4__this;
				try
				{
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							if (num != 1)
							{
								this.<commandTimeoutExpiration>5__2 = 0L;
								if (this.timeout > 0)
								{
									this.<commandTimeoutExpiration>5__2 = ADP.TimerCurrent() + ADP.TimerFromSeconds(this.timeout);
								}
								CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
								sqlConnection._reconnectionCancellationSource = cancellationTokenSource;
								this.<ctoken>5__3 = cancellationTokenSource.Token;
								this.<retryCount>5__4 = sqlConnection._connectRetryCount;
								this.<attempt>5__5 = 0;
								goto IL_1FB;
							}
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_1E2;
						}
						IL_88:
						try
						{
							try
							{
								if (num != 0)
								{
									sqlConnection.ForceNewConnection = true;
									awaiter = sqlConnection.OpenAsync(this.<ctoken>5__3).ConfigureAwait(false).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										num = (this.<>1__state = 0);
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, SqlConnection.<ReconnectAsync>d__97>(ref awaiter, ref this);
										return;
									}
								}
								else
								{
									awaiter = this.<>u__1;
									this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
									num = (this.<>1__state = -1);
								}
								awaiter.GetResult();
								sqlConnection._reconnectCount++;
							}
							finally
							{
								if (num < 0)
								{
									sqlConnection.ForceNewConnection = false;
								}
							}
							goto IL_248;
						}
						catch (SqlException innerException)
						{
							if (this.<attempt>5__5 == this.<retryCount>5__4 - 1)
							{
								throw SQL.CR_AllAttemptsFailed(innerException, sqlConnection._originalConnectionId);
							}
							if (this.timeout > 0 && ADP.TimerRemaining(this.<commandTimeoutExpiration>5__2) < ADP.TimerFromSeconds(sqlConnection.ConnectRetryInterval))
							{
								throw SQL.CR_NextAttemptWillExceedQueryTimeout(innerException, sqlConnection._originalConnectionId);
							}
						}
						awaiter = Task.Delay(1000 * sqlConnection.ConnectRetryInterval, this.<ctoken>5__3).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 1);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, SqlConnection.<ReconnectAsync>d__97>(ref awaiter, ref this);
							return;
						}
						IL_1E2:
						awaiter.GetResult();
						int num2 = this.<attempt>5__5;
						this.<attempt>5__5 = num2 + 1;
						IL_1FB:
						if (this.<attempt>5__5 >= this.<retryCount>5__4)
						{
							this.<ctoken>5__3 = default(CancellationToken);
						}
						else if (!this.<ctoken>5__3.IsCancellationRequested)
						{
							goto IL_88;
						}
					}
					finally
					{
						if (num < 0)
						{
							sqlConnection._recoverySessionData = null;
							sqlConnection._suppressStateChangeForReconnection = false;
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_248:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060016B2 RID: 5810 RVA: 0x00067CE8 File Offset: 0x00065EE8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000E30 RID: 3632
			public int <>1__state;

			// Token: 0x04000E31 RID: 3633
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E32 RID: 3634
			public int timeout;

			// Token: 0x04000E33 RID: 3635
			public SqlConnection <>4__this;

			// Token: 0x04000E34 RID: 3636
			private long <commandTimeoutExpiration>5__2;

			// Token: 0x04000E35 RID: 3637
			private CancellationToken <ctoken>5__3;

			// Token: 0x04000E36 RID: 3638
			private int <retryCount>5__4;

			// Token: 0x04000E37 RID: 3639
			private int <attempt>5__5;

			// Token: 0x04000E38 RID: 3640
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020001CB RID: 459
		[CompilerGenerated]
		private sealed class <>c__DisplayClass98_0
		{
			// Token: 0x060016B3 RID: 5811 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass98_0()
			{
			}

			// Token: 0x060016B4 RID: 5812 RVA: 0x00067CF6 File Offset: 0x00065EF6
			internal Task <ValidateAndReconnect>b__0()
			{
				return this.<>4__this.ReconnectAsync(this.timeout);
			}

			// Token: 0x04000E39 RID: 3641
			public SqlConnection <>4__this;

			// Token: 0x04000E3A RID: 3642
			public int timeout;
		}

		// Token: 0x020001CC RID: 460
		[CompilerGenerated]
		private sealed class <>c__DisplayClass101_0
		{
			// Token: 0x060016B5 RID: 5813 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass101_0()
			{
			}

			// Token: 0x060016B6 RID: 5814 RVA: 0x00067D0C File Offset: 0x00065F0C
			internal void <OpenAsync>b__0(Task<object> t)
			{
				if (t.Exception != null)
				{
					SqlConnection.s_diagnosticListener.WriteConnectionOpenError(this.operationId, this.<>4__this, t.Exception, "OpenAsync");
					return;
				}
				SqlConnection.s_diagnosticListener.WriteConnectionOpenAfter(this.operationId, this.<>4__this, "OpenAsync");
			}

			// Token: 0x04000E3B RID: 3643
			public Guid operationId;

			// Token: 0x04000E3C RID: 3644
			public SqlConnection <>4__this;
		}

		// Token: 0x020001CD RID: 461
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060016B7 RID: 5815 RVA: 0x00067D5E File Offset: 0x00065F5E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060016B8 RID: 5816 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c()
			{
			}

			// Token: 0x060016B9 RID: 5817 RVA: 0x00067D6A File Offset: 0x00065F6A
			internal void <OpenAsync>b__101_1(object s)
			{
				((TaskCompletionSource<DbConnectionInternal>)s).TrySetCanceled();
			}

			// Token: 0x04000E3D RID: 3645
			public static readonly SqlConnection.<>c <>9 = new SqlConnection.<>c();

			// Token: 0x04000E3E RID: 3646
			public static Action<object> <>9__101_1;
		}

		// Token: 0x020001CE RID: 462
		[CompilerGenerated]
		private sealed class <>c__DisplayClass118_0
		{
			// Token: 0x060016BA RID: 5818 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass118_0()
			{
			}

			// Token: 0x060016BB RID: 5819 RVA: 0x00067D78 File Offset: 0x00065F78
			internal void <OnError>b__0()
			{
				if (this.capturedCloseCount == this.<>4__this._closeCount)
				{
					this.<>4__this.Close();
				}
			}

			// Token: 0x04000E3F RID: 3647
			public SqlConnection <>4__this;

			// Token: 0x04000E40 RID: 3648
			public int capturedCloseCount;
		}

		// Token: 0x020001CF RID: 463
		[CompilerGenerated]
		private sealed class <>c__DisplayClass126_0<T>
		{
			// Token: 0x060016BC RID: 5820 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass126_0()
			{
			}

			// Token: 0x060016BD RID: 5821 RVA: 0x00067D98 File Offset: 0x00065F98
			internal Task<T> <RegisterForConnectionCloseNotification>b__0(Task<T> task)
			{
				this.<>4__this.RemoveWeakReference(this.value);
				return task;
			}

			// Token: 0x04000E41 RID: 3649
			public SqlConnection <>4__this;

			// Token: 0x04000E42 RID: 3650
			public object value;
		}

		// Token: 0x020001D0 RID: 464
		[CompilerGenerated]
		private sealed class <>c__DisplayClass133_0
		{
			// Token: 0x060016BE RID: 5822 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass133_0()
			{
			}

			// Token: 0x060016BF RID: 5823 RVA: 0x00067DAC File Offset: 0x00065FAC
			internal Assembly <CheckGetExtendedUDTInfo>b__0(AssemblyName asmRef)
			{
				return this.<>4__this.ResolveTypeAssembly(asmRef, this.fThrow);
			}

			// Token: 0x04000E43 RID: 3651
			public SqlConnection <>4__this;

			// Token: 0x04000E44 RID: 3652
			public bool fThrow;
		}
	}
}
