using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x020001F5 RID: 501
	internal sealed class SqlDelegatedTransaction : IPromotableSinglePhaseNotification, ITransactionPromoter
	{
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x0007066A File Offset: 0x0006E86A
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00070674 File Offset: 0x0006E874
		internal SqlDelegatedTransaction(SqlInternalConnection connection, Transaction tx)
		{
			this._connection = connection;
			this._atomicTransaction = tx;
			this._active = false;
			IsolationLevel isolationLevel = tx.IsolationLevel;
			switch (isolationLevel)
			{
			case IsolationLevel.Serializable:
				this._isolationLevel = IsolationLevel.Serializable;
				return;
			case IsolationLevel.RepeatableRead:
				this._isolationLevel = IsolationLevel.RepeatableRead;
				return;
			case IsolationLevel.ReadCommitted:
				this._isolationLevel = IsolationLevel.ReadCommitted;
				return;
			case IsolationLevel.ReadUncommitted:
				this._isolationLevel = IsolationLevel.ReadUncommitted;
				return;
			case IsolationLevel.Snapshot:
				this._isolationLevel = IsolationLevel.Snapshot;
				return;
			default:
				throw SQL.UnknownSysTxIsolationLevel(isolationLevel);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x00070711 File Offset: 0x0006E911
		internal Transaction Transaction
		{
			get
			{
				return this._atomicTransaction;
			}
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0007071C File Offset: 0x0006E91C
		public void Initialize()
		{
			SqlInternalConnection connection = this._connection;
			SqlConnection connection2 = connection.Connection;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (connection.IsEnlistedInTransaction)
				{
					connection.EnlistNull();
				}
				this._internalTransaction = new SqlInternalTransaction(connection, TransactionType.Delegated, null);
				connection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Begin, null, this._isolationLevel, this._internalTransaction, true);
				if (connection.CurrentTransaction == null)
				{
					connection.DoomThisConnection();
					throw ADP.InternalError(ADP.InternalErrorCode.UnknownTransactionFailure);
				}
				this._active = true;
			}
			catch (OutOfMemoryException e)
			{
				connection2.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				connection2.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				connection2.Abort(e3);
				throw;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x000707D4 File Offset: 0x0006E9D4
		internal bool IsActive
		{
			get
			{
				return this._active;
			}
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x000707DC File Offset: 0x0006E9DC
		public byte[] Promote()
		{
			SqlInternalConnection validConnection = this.GetValidConnection();
			byte[] result = null;
			SqlConnection connection = validConnection.Connection;
			RuntimeHelpers.PrepareConstrainedRegions();
			Exception ex;
			try
			{
				SqlInternalConnection obj = validConnection;
				lock (obj)
				{
					try
					{
						this.ValidateActiveOnConnection(validConnection);
						validConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Promote, null, IsolationLevel.Unspecified, this._internalTransaction, true);
						result = this._connection.PromotedDTCToken;
						if (this._connection.IsGlobalTransaction)
						{
							if (SysTxForGlobalTransactions.SetDistributedTransactionIdentifier == null)
							{
								throw SQL.UnsupportedSysTxForGlobalTransactions();
							}
							if (!this._connection.IsGlobalTransactionsEnabledForServer)
							{
								throw SQL.GlobalTransactionsNotEnabled();
							}
							SysTxForGlobalTransactions.SetDistributedTransactionIdentifier.Invoke(this._atomicTransaction, new object[]
							{
								this,
								this.GetGlobalTxnIdentifierFromToken()
							});
						}
						ex = null;
					}
					catch (SqlException ex)
					{
						validConnection.DoomThisConnection();
					}
					catch (InvalidOperationException ex)
					{
						validConnection.DoomThisConnection();
					}
				}
			}
			catch (OutOfMemoryException e)
			{
				connection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				connection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				connection.Abort(e3);
				throw;
			}
			if (ex != null)
			{
				throw SQL.PromotionFailed(ex);
			}
			return result;
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x00070930 File Offset: 0x0006EB30
		public void Rollback(SinglePhaseEnlistment enlistment)
		{
			SqlInternalConnection validConnection = this.GetValidConnection();
			SqlConnection connection = validConnection.Connection;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				SqlInternalConnection obj = validConnection;
				lock (obj)
				{
					try
					{
						this.ValidateActiveOnConnection(validConnection);
						this._active = false;
						this._connection = null;
						if (!this._internalTransaction.IsAborted)
						{
							validConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Rollback, null, IsolationLevel.Unspecified, this._internalTransaction, true);
						}
					}
					catch (SqlException)
					{
						validConnection.DoomThisConnection();
					}
					catch (InvalidOperationException)
					{
						validConnection.DoomThisConnection();
					}
				}
				validConnection.CleanupConnectionOnTransactionCompletion(this._atomicTransaction);
				enlistment.Aborted();
			}
			catch (OutOfMemoryException e)
			{
				connection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				connection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				connection.Abort(e3);
				throw;
			}
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00070A34 File Offset: 0x0006EC34
		public void SinglePhaseCommit(SinglePhaseEnlistment enlistment)
		{
			SqlInternalConnection validConnection = this.GetValidConnection();
			SqlConnection connection = validConnection.Connection;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (validConnection.IsConnectionDoomed)
				{
					SqlInternalConnection obj = validConnection;
					lock (obj)
					{
						this._active = false;
						this._connection = null;
					}
					enlistment.Aborted(SQL.ConnectionDoomed());
				}
				else
				{
					SqlInternalConnection obj = validConnection;
					Exception ex;
					lock (obj)
					{
						try
						{
							this.ValidateActiveOnConnection(validConnection);
							this._active = false;
							this._connection = null;
							validConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Commit, null, IsolationLevel.Unspecified, this._internalTransaction, true);
							ex = null;
						}
						catch (SqlException ex)
						{
							validConnection.DoomThisConnection();
						}
						catch (InvalidOperationException ex)
						{
							validConnection.DoomThisConnection();
						}
					}
					if (ex != null)
					{
						if (this._internalTransaction.IsCommitted)
						{
							enlistment.Committed();
						}
						else if (this._internalTransaction.IsAborted)
						{
							enlistment.Aborted(ex);
						}
						else
						{
							enlistment.InDoubt(ex);
						}
					}
					validConnection.CleanupConnectionOnTransactionCompletion(this._atomicTransaction);
					if (ex == null)
					{
						enlistment.Committed();
					}
				}
			}
			catch (OutOfMemoryException e)
			{
				connection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				connection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				connection.Abort(e3);
				throw;
			}
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x00070BB4 File Offset: 0x0006EDB4
		internal void TransactionEnded(Transaction transaction)
		{
			SqlInternalConnection connection = this._connection;
			if (connection != null)
			{
				SqlInternalConnection obj = connection;
				lock (obj)
				{
					if (this._atomicTransaction.Equals(transaction))
					{
						this._active = false;
						this._connection = null;
					}
				}
			}
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x00070C10 File Offset: 0x0006EE10
		private SqlInternalConnection GetValidConnection()
		{
			SqlInternalConnection connection = this._connection;
			if (connection == null)
			{
				throw ADP.ObjectDisposed(this);
			}
			return connection;
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x00070C30 File Offset: 0x0006EE30
		private void ValidateActiveOnConnection(SqlInternalConnection connection)
		{
			if (!this._active || connection != this._connection || connection.DelegatedTransaction != this)
			{
				if (connection != null)
				{
					connection.DoomThisConnection();
				}
				if (connection != this._connection && this._connection != null)
				{
					this._connection.DoomThisConnection();
				}
				throw ADP.InternalError(ADP.InternalErrorCode.UnpooledObjectHasWrongOwner);
			}
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x00070C88 File Offset: 0x0006EE88
		private Guid GetGlobalTxnIdentifierFromToken()
		{
			byte[] array = new byte[16];
			Array.Copy(this._connection.PromotedDTCToken, 4, array, 0, array.Length);
			return new Guid(array);
		}

		// Token: 0x04000FA1 RID: 4001
		private static int _objectTypeCount;

		// Token: 0x04000FA2 RID: 4002
		private readonly int _objectID = Interlocked.Increment(ref SqlDelegatedTransaction._objectTypeCount);

		// Token: 0x04000FA3 RID: 4003
		private const int _globalTransactionsTokenVersionSizeInBytes = 4;

		// Token: 0x04000FA4 RID: 4004
		private SqlInternalConnection _connection;

		// Token: 0x04000FA5 RID: 4005
		private IsolationLevel _isolationLevel;

		// Token: 0x04000FA6 RID: 4006
		private SqlInternalTransaction _internalTransaction;

		// Token: 0x04000FA7 RID: 4007
		private Transaction _atomicTransaction;

		// Token: 0x04000FA8 RID: 4008
		private bool _active;
	}
}
