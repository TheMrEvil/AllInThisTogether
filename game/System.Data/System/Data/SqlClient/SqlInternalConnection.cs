using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x02000205 RID: 517
	internal abstract class SqlInternalConnection : DbConnectionInternal
	{
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x0007469D File Offset: 0x0007289D
		// (set) Token: 0x0600190A RID: 6410 RVA: 0x000746A5 File Offset: 0x000728A5
		internal string CurrentDatabase
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentDatabase>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CurrentDatabase>k__BackingField = value;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x000746AE File Offset: 0x000728AE
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x000746B6 File Offset: 0x000728B6
		internal string CurrentDataSource
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentDataSource>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CurrentDataSource>k__BackingField = value;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x000746BF File Offset: 0x000728BF
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x000746C7 File Offset: 0x000728C7
		internal SqlDelegatedTransaction DelegatedTransaction
		{
			[CompilerGenerated]
			get
			{
				return this.<DelegatedTransaction>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DelegatedTransaction>k__BackingField = value;
			}
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000746D0 File Offset: 0x000728D0
		internal SqlInternalConnection(SqlConnectionString connectionOptions)
		{
			this._connectionOptions = connectionOptions;
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x000746DF File Offset: 0x000728DF
		internal SqlConnection Connection
		{
			get
			{
				return (SqlConnection)base.Owner;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x000746EC File Offset: 0x000728EC
		internal SqlConnectionString ConnectionOptions
		{
			get
			{
				return this._connectionOptions;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001912 RID: 6418
		internal abstract SqlInternalTransaction CurrentTransaction { get; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x000746F4 File Offset: 0x000728F4
		internal virtual SqlInternalTransaction AvailableInternalTransaction
		{
			get
			{
				return this.CurrentTransaction;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001914 RID: 6420
		internal abstract SqlInternalTransaction PendingTransaction { get; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x000746FC File Offset: 0x000728FC
		protected internal override bool IsNonPoolableTransactionRoot
		{
			get
			{
				return this.IsTransactionRoot;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x00074704 File Offset: 0x00072904
		internal override bool IsTransactionRoot
		{
			get
			{
				SqlDelegatedTransaction delegatedTransaction = this.DelegatedTransaction;
				return delegatedTransaction != null && delegatedTransaction.IsActive;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x00074724 File Offset: 0x00072924
		internal bool HasLocalTransaction
		{
			get
			{
				SqlInternalTransaction currentTransaction = this.CurrentTransaction;
				return currentTransaction != null && currentTransaction.IsLocal;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x00074744 File Offset: 0x00072944
		internal bool HasLocalTransactionFromAPI
		{
			get
			{
				SqlInternalTransaction currentTransaction = this.CurrentTransaction;
				return currentTransaction != null && currentTransaction.HasParentTransaction;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x00074763 File Offset: 0x00072963
		internal bool IsEnlistedInTransaction
		{
			get
			{
				return this._isEnlistedInTransaction;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600191A RID: 6426
		internal abstract bool IsLockedForBulkCopy { get; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600191B RID: 6427
		internal abstract bool IsKatmaiOrNewer { get; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600191C RID: 6428 RVA: 0x0007476B File Offset: 0x0007296B
		// (set) Token: 0x0600191D RID: 6429 RVA: 0x00074773 File Offset: 0x00072973
		internal byte[] PromotedDTCToken
		{
			get
			{
				return this._promotedDTCToken;
			}
			set
			{
				this._promotedDTCToken = value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x0007477C File Offset: 0x0007297C
		// (set) Token: 0x0600191F RID: 6431 RVA: 0x00074784 File Offset: 0x00072984
		internal bool IsGlobalTransaction
		{
			get
			{
				return this._isGlobalTransaction;
			}
			set
			{
				this._isGlobalTransaction = value;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x0007478D File Offset: 0x0007298D
		// (set) Token: 0x06001921 RID: 6433 RVA: 0x00074795 File Offset: 0x00072995
		internal bool IsGlobalTransactionsEnabledForServer
		{
			get
			{
				return this._isGlobalTransactionEnabledForServer;
			}
			set
			{
				this._isGlobalTransactionEnabledForServer = value;
			}
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x0007479E File Offset: 0x0007299E
		public override DbTransaction BeginTransaction(IsolationLevel iso)
		{
			return this.BeginSqlTransaction(iso, null, false);
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x000747AC File Offset: 0x000729AC
		internal virtual SqlTransaction BeginSqlTransaction(IsolationLevel iso, string transactionName, bool shouldReconnect)
		{
			SqlStatistics statistics = null;
			SqlTransaction result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Connection.Statistics);
				this.ValidateConnectionForExecute(null);
				if (this.HasLocalTransactionFromAPI)
				{
					throw ADP.ParallelTransactionsNotSupported(this.Connection);
				}
				if (iso == IsolationLevel.Unspecified)
				{
					iso = IsolationLevel.ReadCommitted;
				}
				SqlTransaction sqlTransaction = new SqlTransaction(this, this.Connection, iso, this.AvailableInternalTransaction);
				sqlTransaction.InternalTransaction.RestoreBrokenConnection = shouldReconnect;
				this.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Begin, transactionName, iso, sqlTransaction.InternalTransaction, false);
				sqlTransaction.InternalTransaction.RestoreBrokenConnection = false;
				result = sqlTransaction;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x0007484C File Offset: 0x00072A4C
		public override void ChangeDatabase(string database)
		{
			if (string.IsNullOrEmpty(database))
			{
				throw ADP.EmptyDatabaseName();
			}
			this.ValidateConnectionForExecute(null);
			this.ChangeDatabaseInternal(database);
		}

		// Token: 0x06001925 RID: 6437
		protected abstract void ChangeDatabaseInternal(string database);

		// Token: 0x06001926 RID: 6438 RVA: 0x0007486C File Offset: 0x00072A6C
		protected override void CleanupTransactionOnCompletion(Transaction transaction)
		{
			SqlDelegatedTransaction delegatedTransaction = this.DelegatedTransaction;
			if (delegatedTransaction != null)
			{
				delegatedTransaction.TransactionEnded(transaction);
			}
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x0007488A File Offset: 0x00072A8A
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			return new SqlReferenceCollection();
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00074894 File Offset: 0x00072A94
		protected override void Deactivate()
		{
			try
			{
				SqlReferenceCollection sqlReferenceCollection = (SqlReferenceCollection)base.ReferenceCollection;
				if (sqlReferenceCollection != null)
				{
					sqlReferenceCollection.Deactivate();
				}
				this.InternalDeactivate();
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				base.DoomThisConnection();
			}
		}

		// Token: 0x06001929 RID: 6441
		internal abstract void DisconnectTransaction(SqlInternalTransaction internalTransaction);

		// Token: 0x0600192A RID: 6442 RVA: 0x000748E0 File Offset: 0x00072AE0
		public override void Dispose()
		{
			this._whereAbouts = null;
			base.Dispose();
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x000748F0 File Offset: 0x00072AF0
		protected void Enlist(Transaction tx)
		{
			if (null == tx)
			{
				if (this.IsEnlistedInTransaction)
				{
					this.EnlistNull();
					return;
				}
				Transaction enlistedTransaction = base.EnlistedTransaction;
				if (enlistedTransaction != null && enlistedTransaction.TransactionInformation.Status != TransactionStatus.Active)
				{
					this.EnlistNull();
					return;
				}
			}
			else if (!tx.Equals(base.EnlistedTransaction))
			{
				this.EnlistNonNull(tx);
			}
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00074950 File Offset: 0x00072B50
		private void EnlistNonNull(Transaction tx)
		{
			bool flag = false;
			SqlDelegatedTransaction sqlDelegatedTransaction = new SqlDelegatedTransaction(this, tx);
			try
			{
				if (this._isGlobalTransaction)
				{
					if (SysTxForGlobalTransactions.EnlistPromotableSinglePhase == null)
					{
						flag = tx.EnlistPromotableSinglePhase(sqlDelegatedTransaction);
					}
					else
					{
						flag = (bool)SysTxForGlobalTransactions.EnlistPromotableSinglePhase.Invoke(tx, new object[]
						{
							sqlDelegatedTransaction,
							SqlInternalConnection._globalTransactionTMID
						});
					}
				}
				else
				{
					flag = tx.EnlistPromotableSinglePhase(sqlDelegatedTransaction);
				}
				if (flag)
				{
					this.DelegatedTransaction = sqlDelegatedTransaction;
				}
			}
			catch (SqlException ex)
			{
				if (ex.Class >= 20)
				{
					throw;
				}
				SqlInternalConnectionTds sqlInternalConnectionTds = this as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds != null)
				{
					TdsParser parser = sqlInternalConnectionTds.Parser;
					if (parser == null || parser.State != TdsParserState.OpenLoggedIn)
					{
						throw;
					}
				}
			}
			if (!flag)
			{
				byte[] transactionCookie;
				if (this._isGlobalTransaction)
				{
					if (SysTxForGlobalTransactions.GetPromotedToken == null)
					{
						throw SQL.UnsupportedSysTxForGlobalTransactions();
					}
					transactionCookie = (byte[])SysTxForGlobalTransactions.GetPromotedToken.Invoke(tx, null);
				}
				else
				{
					if (this._whereAbouts == null)
					{
						byte[] dtcaddress = this.GetDTCAddress();
						if (dtcaddress == null)
						{
							throw SQL.CannotGetDTCAddress();
						}
						this._whereAbouts = dtcaddress;
					}
					transactionCookie = SqlInternalConnection.GetTransactionCookie(tx, this._whereAbouts);
				}
				this.PropagateTransactionCookie(transactionCookie);
				this._isEnlistedInTransaction = true;
			}
			base.EnlistedTransaction = tx;
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00074A7C File Offset: 0x00072C7C
		internal void EnlistNull()
		{
			this.PropagateTransactionCookie(null);
			this._isEnlistedInTransaction = false;
			base.EnlistedTransaction = null;
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00074A94 File Offset: 0x00072C94
		public override void EnlistTransaction(Transaction transaction)
		{
			this.ValidateConnectionForExecute(null);
			if (this.HasLocalTransaction)
			{
				throw ADP.LocalTransactionPresent();
			}
			if (null != transaction && transaction.Equals(base.EnlistedTransaction))
			{
				return;
			}
			try
			{
				this.Enlist(transaction);
			}
			catch (OutOfMemoryException e)
			{
				this.Connection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this.Connection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this.Connection.Abort(e3);
				throw;
			}
		}

		// Token: 0x0600192F RID: 6447
		internal abstract void ExecuteTransaction(SqlInternalConnection.TransactionRequest transactionRequest, string name, IsolationLevel iso, SqlInternalTransaction internalTransaction, bool isDelegateControlRequest);

		// Token: 0x06001930 RID: 6448 RVA: 0x00074B30 File Offset: 0x00072D30
		internal SqlDataReader FindLiveReader(SqlCommand command)
		{
			SqlDataReader result = null;
			SqlReferenceCollection sqlReferenceCollection = (SqlReferenceCollection)base.ReferenceCollection;
			if (sqlReferenceCollection != null)
			{
				result = sqlReferenceCollection.FindLiveReader(command);
			}
			return result;
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00074B58 File Offset: 0x00072D58
		internal SqlCommand FindLiveCommand(TdsParserStateObject stateObj)
		{
			SqlCommand result = null;
			SqlReferenceCollection sqlReferenceCollection = (SqlReferenceCollection)base.ReferenceCollection;
			if (sqlReferenceCollection != null)
			{
				result = sqlReferenceCollection.FindLiveCommand(stateObj);
			}
			return result;
		}

		// Token: 0x06001932 RID: 6450
		protected abstract byte[] GetDTCAddress();

		// Token: 0x06001933 RID: 6451 RVA: 0x00074B80 File Offset: 0x00072D80
		private static byte[] GetTransactionCookie(Transaction transaction, byte[] whereAbouts)
		{
			byte[] result = null;
			if (null != transaction)
			{
				result = TransactionInterop.GetExportCookie(transaction, whereAbouts);
			}
			return result;
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00007EED File Offset: 0x000060ED
		protected virtual void InternalDeactivate()
		{
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00074BA4 File Offset: 0x00072DA4
		internal void OnError(SqlException exception, bool breakConnection, Action<Action> wrapCloseInAction = null)
		{
			if (breakConnection)
			{
				base.DoomThisConnection();
			}
			SqlConnection connection = this.Connection;
			if (connection != null)
			{
				connection.OnError(exception, breakConnection, wrapCloseInAction);
				return;
			}
			if (exception.Class >= 11)
			{
				throw exception;
			}
		}

		// Token: 0x06001936 RID: 6454
		protected abstract void PropagateTransactionCookie(byte[] transactionCookie);

		// Token: 0x06001937 RID: 6455
		internal abstract void ValidateConnectionForExecute(SqlCommand command);

		// Token: 0x06001938 RID: 6456 RVA: 0x00074BDA File Offset: 0x00072DDA
		// Note: this type is marked as 'beforefieldinit'.
		static SqlInternalConnection()
		{
		}

		// Token: 0x0400103F RID: 4159
		private readonly SqlConnectionString _connectionOptions;

		// Token: 0x04001040 RID: 4160
		private bool _isEnlistedInTransaction;

		// Token: 0x04001041 RID: 4161
		private byte[] _promotedDTCToken;

		// Token: 0x04001042 RID: 4162
		private byte[] _whereAbouts;

		// Token: 0x04001043 RID: 4163
		private bool _isGlobalTransaction;

		// Token: 0x04001044 RID: 4164
		private bool _isGlobalTransactionEnabledForServer;

		// Token: 0x04001045 RID: 4165
		private static readonly Guid _globalTransactionTMID = new Guid("1c742caf-6680-40ea-9c26-6b6846079764");

		// Token: 0x04001046 RID: 4166
		[CompilerGenerated]
		private string <CurrentDatabase>k__BackingField;

		// Token: 0x04001047 RID: 4167
		[CompilerGenerated]
		private string <CurrentDataSource>k__BackingField;

		// Token: 0x04001048 RID: 4168
		[CompilerGenerated]
		private SqlDelegatedTransaction <DelegatedTransaction>k__BackingField;

		// Token: 0x02000206 RID: 518
		internal enum TransactionRequest
		{
			// Token: 0x0400104A RID: 4170
			Begin,
			// Token: 0x0400104B RID: 4171
			Promote,
			// Token: 0x0400104C RID: 4172
			Commit,
			// Token: 0x0400104D RID: 4173
			Rollback,
			// Token: 0x0400104E RID: 4174
			IfRollback,
			// Token: 0x0400104F RID: 4175
			Save
		}
	}
}
