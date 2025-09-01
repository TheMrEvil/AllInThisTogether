using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x02000210 RID: 528
	internal sealed class SqlInternalTransaction
	{
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x00076811 File Offset: 0x00074A11
		// (set) Token: 0x06001993 RID: 6547 RVA: 0x00076819 File Offset: 0x00074A19
		internal bool RestoreBrokenConnection
		{
			[CompilerGenerated]
			get
			{
				return this.<RestoreBrokenConnection>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RestoreBrokenConnection>k__BackingField = value;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x00076822 File Offset: 0x00074A22
		// (set) Token: 0x06001995 RID: 6549 RVA: 0x0007682A File Offset: 0x00074A2A
		internal bool ConnectionHasBeenRestored
		{
			[CompilerGenerated]
			get
			{
				return this.<ConnectionHasBeenRestored>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ConnectionHasBeenRestored>k__BackingField = value;
			}
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x00076833 File Offset: 0x00074A33
		internal SqlInternalTransaction(SqlInternalConnection innerConnection, TransactionType type, SqlTransaction outerTransaction) : this(innerConnection, type, outerTransaction, 0L)
		{
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x00076840 File Offset: 0x00074A40
		internal SqlInternalTransaction(SqlInternalConnection innerConnection, TransactionType type, SqlTransaction outerTransaction, long transactionId)
		{
			this._innerConnection = innerConnection;
			this._transactionType = type;
			if (outerTransaction != null)
			{
				this._parent = new WeakReference(outerTransaction);
			}
			this._transactionId = transactionId;
			this.RestoreBrokenConnection = false;
			this.ConnectionHasBeenRestored = false;
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x0007687B File Offset: 0x00074A7B
		internal bool HasParentTransaction
		{
			get
			{
				return TransactionType.LocalFromAPI == this._transactionType || (TransactionType.LocalFromTSQL == this._transactionType && this._parent != null);
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001999 RID: 6553 RVA: 0x0007689C File Offset: 0x00074A9C
		internal bool IsAborted
		{
			get
			{
				return TransactionState.Aborted == this._transactionState;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x000768A7 File Offset: 0x00074AA7
		internal bool IsActive
		{
			get
			{
				return TransactionState.Active == this._transactionState;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x000768B2 File Offset: 0x00074AB2
		internal bool IsCommitted
		{
			get
			{
				return TransactionState.Committed == this._transactionState;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x000768BD File Offset: 0x00074ABD
		internal bool IsCompleted
		{
			get
			{
				return TransactionState.Aborted == this._transactionState || TransactionState.Committed == this._transactionState || TransactionState.Unknown == this._transactionState;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x000768DC File Offset: 0x00074ADC
		internal bool IsDelegated
		{
			get
			{
				return TransactionType.Delegated == this._transactionType;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x000768E7 File Offset: 0x00074AE7
		internal bool IsDistributed
		{
			get
			{
				return TransactionType.Distributed == this._transactionType;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x000768F2 File Offset: 0x00074AF2
		internal bool IsLocal
		{
			get
			{
				return TransactionType.LocalFromTSQL == this._transactionType || TransactionType.LocalFromAPI == this._transactionType;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x00076908 File Offset: 0x00074B08
		internal bool IsOrphaned
		{
			get
			{
				return this._parent != null && this._parent.Target == null;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x00076935 File Offset: 0x00074B35
		internal bool IsZombied
		{
			get
			{
				return this._innerConnection == null;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x00076940 File Offset: 0x00074B40
		internal int OpenResultsCount
		{
			get
			{
				return this._openResultCount;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x00076948 File Offset: 0x00074B48
		internal SqlTransaction Parent
		{
			get
			{
				SqlTransaction result = null;
				if (this._parent != null)
				{
					result = (SqlTransaction)this._parent.Target;
				}
				return result;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x00076971 File Offset: 0x00074B71
		// (set) Token: 0x060019A5 RID: 6565 RVA: 0x00076979 File Offset: 0x00074B79
		internal long TransactionId
		{
			get
			{
				return this._transactionId;
			}
			set
			{
				this._transactionId = value;
			}
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00076982 File Offset: 0x00074B82
		internal void Activate()
		{
			this._transactionState = TransactionState.Active;
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0007698C File Offset: 0x00074B8C
		private void CheckTransactionLevelAndZombie()
		{
			try
			{
				if (!this.IsZombied && this.GetServerTransactionLevel() == 0)
				{
					this.Zombie();
				}
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				this.Zombie();
			}
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x000769D4 File Offset: 0x00074BD4
		internal void CloseFromConnection()
		{
			SqlInternalConnection innerConnection = this._innerConnection;
			bool flag = true;
			try
			{
				innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.IfRollback, null, IsolationLevel.Unspecified, null, false);
			}
			catch (Exception e)
			{
				flag = ADP.IsCatchableExceptionType(e);
				throw;
			}
			finally
			{
				if (flag)
				{
					this.Zombie();
				}
			}
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00076A28 File Offset: 0x00074C28
		internal void Commit()
		{
			if (this._innerConnection.IsLockedForBulkCopy)
			{
				throw SQL.ConnectionLockedForBcpEvent();
			}
			this._innerConnection.ValidateConnectionForExecute(null);
			try
			{
				this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Commit, null, IsolationLevel.Unspecified, null, false);
				this.ZombieParent();
			}
			catch (Exception e)
			{
				if (ADP.IsCatchableExceptionType(e))
				{
					this.CheckTransactionLevelAndZombie();
				}
				throw;
			}
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00076A8C File Offset: 0x00074C8C
		internal void Completed(TransactionState transactionState)
		{
			this._transactionState = transactionState;
			this.Zombie();
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x00076A9B File Offset: 0x00074C9B
		internal int DecrementAndObtainOpenResultCount()
		{
			int num = Interlocked.Decrement(ref this._openResultCount);
			if (num < 0)
			{
				throw SQL.OpenResultCountExceeded();
			}
			return num;
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00076AB2 File Offset: 0x00074CB2
		internal void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x00076AC1 File Offset: 0x00074CC1
		private void Dispose(bool disposing)
		{
			if (disposing && this._innerConnection != null)
			{
				this._disposing = true;
				this.Rollback();
			}
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x00076ADC File Offset: 0x00074CDC
		private int GetServerTransactionLevel()
		{
			int result;
			using (SqlCommand sqlCommand = new SqlCommand("set @out = @@trancount", (SqlConnection)this._innerConnection.Owner))
			{
				sqlCommand.Transaction = this.Parent;
				SqlParameter sqlParameter = new SqlParameter("@out", SqlDbType.Int);
				sqlParameter.Direction = ParameterDirection.Output;
				sqlCommand.Parameters.Add(sqlParameter);
				sqlCommand.RunExecuteReader(CommandBehavior.Default, RunBehavior.UntilDone, false, "GetServerTransactionLevel");
				result = (int)sqlParameter.Value;
			}
			return result;
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x00076B68 File Offset: 0x00074D68
		internal int IncrementAndObtainOpenResultCount()
		{
			int num = Interlocked.Increment(ref this._openResultCount);
			if (num < 0)
			{
				throw SQL.OpenResultCountExceeded();
			}
			return num;
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x00076B7F File Offset: 0x00074D7F
		internal void InitParent(SqlTransaction transaction)
		{
			this._parent = new WeakReference(transaction);
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x00076B90 File Offset: 0x00074D90
		internal void Rollback()
		{
			if (this._innerConnection.IsLockedForBulkCopy)
			{
				throw SQL.ConnectionLockedForBcpEvent();
			}
			this._innerConnection.ValidateConnectionForExecute(null);
			try
			{
				this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.IfRollback, null, IsolationLevel.Unspecified, null, false);
				this.Zombie();
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				this.CheckTransactionLevelAndZombie();
				if (!this._disposing)
				{
					throw;
				}
			}
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x00076C00 File Offset: 0x00074E00
		internal void Rollback(string transactionName)
		{
			if (this._innerConnection.IsLockedForBulkCopy)
			{
				throw SQL.ConnectionLockedForBcpEvent();
			}
			this._innerConnection.ValidateConnectionForExecute(null);
			if (string.IsNullOrEmpty(transactionName))
			{
				throw SQL.NullEmptyTransactionName();
			}
			try
			{
				this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Rollback, transactionName, IsolationLevel.Unspecified, null, false);
			}
			catch (Exception e)
			{
				if (ADP.IsCatchableExceptionType(e))
				{
					this.CheckTransactionLevelAndZombie();
				}
				throw;
			}
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x00076C6C File Offset: 0x00074E6C
		internal void Save(string savePointName)
		{
			this._innerConnection.ValidateConnectionForExecute(null);
			if (string.IsNullOrEmpty(savePointName))
			{
				throw SQL.NullEmptyTransactionName();
			}
			try
			{
				this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Save, savePointName, IsolationLevel.Unspecified, null, false);
			}
			catch (Exception e)
			{
				if (ADP.IsCatchableExceptionType(e))
				{
					this.CheckTransactionLevelAndZombie();
				}
				throw;
			}
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x00076CC8 File Offset: 0x00074EC8
		internal void Zombie()
		{
			this.ZombieParent();
			SqlInternalConnection innerConnection = this._innerConnection;
			this._innerConnection = null;
			if (innerConnection != null)
			{
				innerConnection.DisconnectTransaction(this);
			}
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x00076CF4 File Offset: 0x00074EF4
		private void ZombieParent()
		{
			if (this._parent != null)
			{
				SqlTransaction sqlTransaction = (SqlTransaction)this._parent.Target;
				if (sqlTransaction != null)
				{
					sqlTransaction.Zombie();
				}
				this._parent = null;
			}
		}

		// Token: 0x04001099 RID: 4249
		internal const long NullTransactionId = 0L;

		// Token: 0x0400109A RID: 4250
		private TransactionState _transactionState;

		// Token: 0x0400109B RID: 4251
		private TransactionType _transactionType;

		// Token: 0x0400109C RID: 4252
		private long _transactionId;

		// Token: 0x0400109D RID: 4253
		private int _openResultCount;

		// Token: 0x0400109E RID: 4254
		private SqlInternalConnection _innerConnection;

		// Token: 0x0400109F RID: 4255
		private bool _disposing;

		// Token: 0x040010A0 RID: 4256
		private WeakReference _parent;

		// Token: 0x040010A1 RID: 4257
		[CompilerGenerated]
		private bool <RestoreBrokenConnection>k__BackingField;

		// Token: 0x040010A2 RID: 4258
		[CompilerGenerated]
		private bool <ConnectionHasBeenRestored>k__BackingField;
	}
}
