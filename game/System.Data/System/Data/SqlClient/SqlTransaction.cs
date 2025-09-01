using System;
using System.Data.Common;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Represents a Transact-SQL transaction to be made in a SQL Server database. This class cannot be inherited.</summary>
	// Token: 0x02000231 RID: 561
	public sealed class SqlTransaction : DbTransaction
	{
		// Token: 0x06001AF9 RID: 6905 RVA: 0x0007C164 File Offset: 0x0007A364
		internal SqlTransaction(SqlInternalConnection internalConnection, SqlConnection con, IsolationLevel iso, SqlInternalTransaction internalTransaction)
		{
			this._isolationLevel = IsolationLevel.ReadCommitted;
			base..ctor();
			this._isolationLevel = iso;
			this._connection = con;
			if (internalTransaction == null)
			{
				this._internalTransaction = new SqlInternalTransaction(internalConnection, TransactionType.LocalFromAPI, this);
				return;
			}
			this._internalTransaction = internalTransaction;
			this._internalTransaction.InitParent(this);
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlConnection" /> object associated with the transaction, or <see langword="null" /> if the transaction is no longer valid.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlConnection" /> object associated with the transaction.</returns>
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x0007C1B7 File Offset: 0x0007A3B7
		public new SqlConnection Connection
		{
			get
			{
				if (this.IsZombied)
				{
					return null;
				}
				return this._connection;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x0007C1C9 File Offset: 0x0007A3C9
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x0007C1D1 File Offset: 0x0007A3D1
		internal SqlInternalTransaction InternalTransaction
		{
			get
			{
				return this._internalTransaction;
			}
		}

		/// <summary>Specifies the <see cref="T:System.Data.IsolationLevel" /> for this transaction.</summary>
		/// <returns>The <see cref="T:System.Data.IsolationLevel" /> for this transaction. The default is <see langword="ReadCommitted" />.</returns>
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x0007C1D9 File Offset: 0x0007A3D9
		public override IsolationLevel IsolationLevel
		{
			get
			{
				this.ZombieCheck();
				return this._isolationLevel;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001AFE RID: 6910 RVA: 0x0007C1E7 File Offset: 0x0007A3E7
		private bool IsYukonPartialZombie
		{
			get
			{
				return this._internalTransaction != null && this._internalTransaction.IsCompleted;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x0007C1FE File Offset: 0x0007A3FE
		internal bool IsZombied
		{
			get
			{
				return this._internalTransaction == null || this._internalTransaction.IsCompleted;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x0007C215 File Offset: 0x0007A415
		internal SqlStatistics Statistics
		{
			get
			{
				if (this._connection != null && this._connection.StatisticsEnabled)
				{
					return this._connection.Statistics;
				}
				return null;
			}
		}

		/// <summary>Commits the database transaction.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction.</exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.  
		///  -or-  
		///  The connection is broken.</exception>
		// Token: 0x06001B01 RID: 6913 RVA: 0x0007C23C File Offset: 0x0007A43C
		public override void Commit()
		{
			Exception ex = null;
			Guid operationId = SqlTransaction.s_diagnosticListener.WriteTransactionCommitBefore(this._isolationLevel, this._connection, "Commit");
			this.ZombieCheck();
			SqlStatistics statistics = null;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._isFromAPI = true;
				this._internalTransaction.Commit();
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (ex != null)
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionCommitError(operationId, this._isolationLevel, this._connection, ex, "Commit");
				}
				else
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionCommitAfter(operationId, this._isolationLevel, this._connection, "Commit");
				}
				this._isFromAPI = false;
				SqlStatistics.StopTimer(statistics);
			}
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0007C2FC File Offset: 0x0007A4FC
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.IsZombied && !this.IsYukonPartialZombie)
			{
				this._internalTransaction.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>Rolls back a transaction from a pending state.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction.</exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.  
		///  -or-  
		///  The connection is broken.</exception>
		// Token: 0x06001B03 RID: 6915 RVA: 0x0007C324 File Offset: 0x0007A524
		public override void Rollback()
		{
			Exception ex = null;
			Guid operationId = SqlTransaction.s_diagnosticListener.WriteTransactionRollbackBefore(this._isolationLevel, this._connection, null, "Rollback");
			if (this.IsYukonPartialZombie)
			{
				this._internalTransaction = null;
				return;
			}
			this.ZombieCheck();
			SqlStatistics statistics = null;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._isFromAPI = true;
				this._internalTransaction.Rollback();
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (ex != null)
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionRollbackError(operationId, this._isolationLevel, this._connection, null, ex, "Rollback");
				}
				else
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionRollbackAfter(operationId, this._isolationLevel, this._connection, null, "Rollback");
				}
				this._isFromAPI = false;
				SqlStatistics.StopTimer(statistics);
			}
		}

		/// <summary>Rolls back a transaction from a pending state, and specifies the transaction or savepoint name.</summary>
		/// <param name="transactionName">The name of the transaction to roll back, or the savepoint to which to roll back.</param>
		/// <exception cref="T:System.ArgumentException">No transaction name was specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.  
		///  -or-  
		///  The connection is broken.</exception>
		// Token: 0x06001B04 RID: 6916 RVA: 0x0007C3F8 File Offset: 0x0007A5F8
		public void Rollback(string transactionName)
		{
			Exception ex = null;
			Guid operationId = SqlTransaction.s_diagnosticListener.WriteTransactionRollbackBefore(this._isolationLevel, this._connection, transactionName, "Rollback");
			this.ZombieCheck();
			SqlStatistics statistics = null;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._isFromAPI = true;
				this._internalTransaction.Rollback(transactionName);
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (ex != null)
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionRollbackError(operationId, this._isolationLevel, this._connection, transactionName, ex, "Rollback");
				}
				else
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionRollbackAfter(operationId, this._isolationLevel, this._connection, transactionName, "Rollback");
				}
				this._isFromAPI = false;
				SqlStatistics.StopTimer(statistics);
			}
		}

		/// <summary>Creates a savepoint in the transaction that can be used to roll back a part of the transaction, and specifies the savepoint name.</summary>
		/// <param name="savePointName">The name of the savepoint.</param>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction.</exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.  
		///  -or-  
		///  The connection is broken.</exception>
		// Token: 0x06001B05 RID: 6917 RVA: 0x0007C4BC File Offset: 0x0007A6BC
		public void Save(string savePointName)
		{
			this.ZombieCheck();
			SqlStatistics statistics = null;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._internalTransaction.Save(savePointName);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0007C504 File Offset: 0x0007A704
		internal void Zombie()
		{
			if (!(this._connection.InnerConnection is SqlInternalConnection) || this._isFromAPI)
			{
				this._internalTransaction = null;
			}
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0007C527 File Offset: 0x0007A727
		private void ZombieCheck()
		{
			if (this.IsZombied)
			{
				if (this.IsYukonPartialZombie)
				{
					this._internalTransaction = null;
				}
				throw ADP.TransactionZombied(this);
			}
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0007C547 File Offset: 0x0007A747
		// Note: this type is marked as 'beforefieldinit'.
		static SqlTransaction()
		{
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x000108A6 File Offset: 0x0000EAA6
		internal SqlTransaction()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001142 RID: 4418
		private static readonly DiagnosticListener s_diagnosticListener = new DiagnosticListener("SqlClientDiagnosticListener");

		// Token: 0x04001143 RID: 4419
		internal readonly IsolationLevel _isolationLevel;

		// Token: 0x04001144 RID: 4420
		private SqlInternalTransaction _internalTransaction;

		// Token: 0x04001145 RID: 4421
		private SqlConnection _connection;

		// Token: 0x04001146 RID: 4422
		private bool _isFromAPI;
	}
}
