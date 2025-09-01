using System;
using System.Data.Common;
using Unity;

namespace System.Data.Odbc
{
	/// <summary>Represents an SQL transaction to be made at a data source. This class cannot be inherited.</summary>
	// Token: 0x020002FC RID: 764
	public sealed class OdbcTransaction : DbTransaction
	{
		// Token: 0x06002200 RID: 8704 RVA: 0x0009E213 File Offset: 0x0009C413
		internal OdbcTransaction(OdbcConnection connection, IsolationLevel isolevel, OdbcConnectionHandle handle)
		{
			this._isolevel = IsolationLevel.Unspecified;
			base..ctor();
			this._connection = connection;
			this._isolevel = isolevel;
			this._handle = handle;
		}

		/// <summary>Gets the <see cref="T:System.Data.Odbc.OdbcConnection" /> object associated with the transaction, or <see langword="null" /> if the transaction is no longer valid.</summary>
		/// <returns>The <see cref="T:System.Data.Odbc.OdbcConnection" /> object associated with the transaction.</returns>
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x0009E237 File Offset: 0x0009C437
		public new OdbcConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x0009E23F File Offset: 0x0009C43F
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
		}

		/// <summary>Specifies the <see cref="T:System.Data.IsolationLevel" /> for this transaction.</summary>
		/// <returns>The <see cref="T:System.Data.IsolationLevel" /> for this transaction. The default depends on the underlying ODBC driver.</returns>
		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x0009E248 File Offset: 0x0009C448
		public override IsolationLevel IsolationLevel
		{
			get
			{
				OdbcConnection connection = this._connection;
				if (connection == null)
				{
					throw ADP.TransactionZombied(this);
				}
				if (IsolationLevel.Unspecified == this._isolevel)
				{
					int connectAttr = connection.GetConnectAttr(ODBC32.SQL_ATTR.TXN_ISOLATION, ODBC32.HANDLER.THROW);
					ODBC32.SQL_TRANSACTION sql_TRANSACTION = (ODBC32.SQL_TRANSACTION)connectAttr;
					switch (sql_TRANSACTION)
					{
					case ODBC32.SQL_TRANSACTION.READ_UNCOMMITTED:
						this._isolevel = IsolationLevel.ReadUncommitted;
						goto IL_91;
					case ODBC32.SQL_TRANSACTION.READ_COMMITTED:
						this._isolevel = IsolationLevel.ReadCommitted;
						goto IL_91;
					case (ODBC32.SQL_TRANSACTION)3:
						break;
					case ODBC32.SQL_TRANSACTION.REPEATABLE_READ:
						this._isolevel = IsolationLevel.RepeatableRead;
						goto IL_91;
					default:
						if (sql_TRANSACTION == ODBC32.SQL_TRANSACTION.SERIALIZABLE)
						{
							this._isolevel = IsolationLevel.Serializable;
							goto IL_91;
						}
						if (sql_TRANSACTION == ODBC32.SQL_TRANSACTION.SNAPSHOT)
						{
							this._isolevel = IsolationLevel.Snapshot;
							goto IL_91;
						}
						break;
					}
					throw ODBC.NoMappingForSqlTransactionLevel(connectAttr);
				}
				IL_91:
				return this._isolevel;
			}
		}

		/// <summary>Commits the database transaction.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction.</exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.  
		///  -or-  
		///  The connection is broken.</exception>
		// Token: 0x06002204 RID: 8708 RVA: 0x0009E2EC File Offset: 0x0009C4EC
		public override void Commit()
		{
			OdbcConnection connection = this._connection;
			if (connection == null)
			{
				throw ADP.TransactionZombied(this);
			}
			connection.CheckState("CommitTransaction");
			if (this._handle == null)
			{
				throw ODBC.NotInTransaction();
			}
			ODBC32.RetCode retCode = this._handle.CompleteTransaction(0);
			if (retCode == ODBC32.RetCode.ERROR)
			{
				connection.HandleError(this._handle, retCode);
			}
			connection.LocalTransaction = null;
			this._connection = null;
			this._handle = null;
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x0009E358 File Offset: 0x0009C558
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				OdbcConnectionHandle handle = this._handle;
				this._handle = null;
				if (handle != null)
				{
					try
					{
						ODBC32.RetCode retCode = handle.CompleteTransaction(1);
						if (retCode == ODBC32.RetCode.ERROR && this._connection != null)
						{
							ADP.TraceExceptionWithoutRethrow(this._connection.HandleErrorNoThrow(handle, retCode));
						}
					}
					catch (Exception e)
					{
						if (!ADP.IsCatchableExceptionType(e))
						{
							throw;
						}
					}
				}
				if (this._connection != null && this._connection.IsOpen)
				{
					this._connection.LocalTransaction = null;
				}
				this._connection = null;
				this._isolevel = IsolationLevel.Unspecified;
			}
			base.Dispose(disposing);
		}

		/// <summary>Rolls back a transaction from a pending state.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction.</exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.  
		///  -or-  
		///  The connection is broken.</exception>
		// Token: 0x06002206 RID: 8710 RVA: 0x0009E3F4 File Offset: 0x0009C5F4
		public override void Rollback()
		{
			OdbcConnection connection = this._connection;
			if (connection == null)
			{
				throw ADP.TransactionZombied(this);
			}
			connection.CheckState("RollbackTransaction");
			if (this._handle == null)
			{
				throw ODBC.NotInTransaction();
			}
			ODBC32.RetCode retCode = this._handle.CompleteTransaction(1);
			if (retCode == ODBC32.RetCode.ERROR)
			{
				connection.HandleError(this._handle, retCode);
			}
			connection.LocalTransaction = null;
			this._connection = null;
			this._handle = null;
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000108A6 File Offset: 0x0000EAA6
		internal OdbcTransaction()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001800 RID: 6144
		private OdbcConnection _connection;

		// Token: 0x04001801 RID: 6145
		private IsolationLevel _isolevel;

		// Token: 0x04001802 RID: 6146
		private OdbcConnectionHandle _handle;
	}
}
