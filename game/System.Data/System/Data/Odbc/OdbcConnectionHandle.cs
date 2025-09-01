using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Transactions;

namespace System.Data.Odbc
{
	// Token: 0x020002DA RID: 730
	internal sealed class OdbcConnectionHandle : OdbcHandle
	{
		// Token: 0x0600202F RID: 8239 RVA: 0x00096818 File Offset: 0x00094A18
		internal OdbcConnectionHandle(OdbcConnection connection, OdbcConnectionString constr, OdbcEnvironmentHandle environmentHandle) : base(ODBC32.SQL_HANDLE.DBC, environmentHandle)
		{
			if (connection == null)
			{
				throw ADP.ArgumentNull("connection");
			}
			if (constr == null)
			{
				throw ADP.ArgumentNull("constr");
			}
			int connectionTimeout = connection.ConnectionTimeout;
			ODBC32.RetCode retcode = this.SetConnectionAttribute2(ODBC32.SQL_ATTR.LOGIN_TIMEOUT, (IntPtr)connectionTimeout, -5);
			string connectionString = constr.UsersConnectionString(false);
			retcode = this.Connect(connectionString);
			connection.HandleError(this, retcode);
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x0009687C File Offset: 0x00094A7C
		private ODBC32.RetCode AutoCommitOff()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			ODBC32.RetCode retCode;
			try
			{
			}
			finally
			{
				retCode = Interop.Odbc.SQLSetConnectAttrW(this, ODBC32.SQL_ATTR.AUTOCOMMIT, ODBC32.SQL_AUTOCOMMIT_OFF, -5);
				if (retCode <= ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					this._handleState = OdbcConnectionHandle.HandleState.Transacted;
				}
			}
			ODBC.TraceODBC(3, "SQLSetConnectAttrW", retCode);
			return retCode;
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x000968CC File Offset: 0x00094ACC
		internal ODBC32.RetCode BeginTransaction(ref IsolationLevel isolevel)
		{
			ODBC32.RetCode retCode = ODBC32.RetCode.SUCCESS;
			if (IsolationLevel.Unspecified != isolevel)
			{
				IsolationLevel isolationLevel = isolevel;
				ODBC32.SQL_TRANSACTION value;
				ODBC32.SQL_ATTR attribute;
				if (isolationLevel <= IsolationLevel.ReadCommitted)
				{
					if (isolationLevel == IsolationLevel.Chaos)
					{
						throw ODBC.NotSupportedIsolationLevel(isolevel);
					}
					if (isolationLevel == IsolationLevel.ReadUncommitted)
					{
						value = ODBC32.SQL_TRANSACTION.READ_UNCOMMITTED;
						attribute = ODBC32.SQL_ATTR.TXN_ISOLATION;
						goto IL_7D;
					}
					if (isolationLevel == IsolationLevel.ReadCommitted)
					{
						value = ODBC32.SQL_TRANSACTION.READ_COMMITTED;
						attribute = ODBC32.SQL_ATTR.TXN_ISOLATION;
						goto IL_7D;
					}
				}
				else
				{
					if (isolationLevel == IsolationLevel.RepeatableRead)
					{
						value = ODBC32.SQL_TRANSACTION.REPEATABLE_READ;
						attribute = ODBC32.SQL_ATTR.TXN_ISOLATION;
						goto IL_7D;
					}
					if (isolationLevel == IsolationLevel.Serializable)
					{
						value = ODBC32.SQL_TRANSACTION.SERIALIZABLE;
						attribute = ODBC32.SQL_ATTR.TXN_ISOLATION;
						goto IL_7D;
					}
					if (isolationLevel == IsolationLevel.Snapshot)
					{
						value = ODBC32.SQL_TRANSACTION.SNAPSHOT;
						attribute = ODBC32.SQL_ATTR.SQL_COPT_SS_TXN_ISOLATION;
						goto IL_7D;
					}
				}
				throw ADP.InvalidIsolationLevel(isolevel);
				IL_7D:
				retCode = this.SetConnectionAttribute2(attribute, (IntPtr)((int)value), -6);
				if (ODBC32.RetCode.SUCCESS_WITH_INFO == retCode)
				{
					isolevel = IsolationLevel.Unspecified;
				}
			}
			if (retCode <= ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				retCode = this.AutoCommitOff();
				this._handleState = OdbcConnectionHandle.HandleState.TransactionInProgress;
			}
			return retCode;
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x00096980 File Offset: 0x00094B80
		internal ODBC32.RetCode CompleteTransaction(short transactionOperation)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			ODBC32.RetCode result;
			try
			{
				base.DangerousAddRef(ref flag);
				result = this.CompleteTransaction(transactionOperation, this.handle);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x000969C8 File Offset: 0x00094BC8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private ODBC32.RetCode CompleteTransaction(short transactionOperation, IntPtr handle)
		{
			ODBC32.RetCode retCode = ODBC32.RetCode.SUCCESS;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				if (OdbcConnectionHandle.HandleState.TransactionInProgress == this._handleState)
				{
					retCode = Interop.Odbc.SQLEndTran(base.HandleType, handle, transactionOperation);
					if (retCode == ODBC32.RetCode.SUCCESS || ODBC32.RetCode.SUCCESS_WITH_INFO == retCode)
					{
						this._handleState = OdbcConnectionHandle.HandleState.Transacted;
					}
				}
				if (OdbcConnectionHandle.HandleState.Transacted == this._handleState)
				{
					retCode = Interop.Odbc.SQLSetConnectAttrW(handle, ODBC32.SQL_ATTR.AUTOCOMMIT, ODBC32.SQL_AUTOCOMMIT_ON, -5);
					this._handleState = OdbcConnectionHandle.HandleState.Connected;
				}
			}
			return retCode;
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x00096A38 File Offset: 0x00094C38
		private ODBC32.RetCode Connect(string connectionString)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			ODBC32.RetCode retCode;
			try
			{
			}
			finally
			{
				short num;
				retCode = Interop.Odbc.SQLDriverConnectW(this, ADP.PtrZero, connectionString, -3, ADP.PtrZero, 0, out num, 0);
				if (retCode <= ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					this._handleState = OdbcConnectionHandle.HandleState.Connected;
				}
			}
			ODBC.TraceODBC(3, "SQLDriverConnectW", retCode);
			return retCode;
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x00096A90 File Offset: 0x00094C90
		protected override bool ReleaseHandle()
		{
			this.CompleteTransaction(1, this.handle);
			if (OdbcConnectionHandle.HandleState.Connected == this._handleState || OdbcConnectionHandle.HandleState.TransactionInProgress == this._handleState)
			{
				Interop.Odbc.SQLDisconnect(this.handle);
				this._handleState = OdbcConnectionHandle.HandleState.Allocated;
			}
			return base.ReleaseHandle();
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x00096ACB File Offset: 0x00094CCB
		internal ODBC32.RetCode GetConnectionAttribute(ODBC32.SQL_ATTR attribute, byte[] buffer, out int cbActual)
		{
			return Interop.Odbc.SQLGetConnectAttrW(this, attribute, buffer, buffer.Length, out cbActual);
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x00096ADC File Offset: 0x00094CDC
		internal ODBC32.RetCode GetFunctions(ODBC32.SQL_API fFunction, out short fExists)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLGetFunctions(this, fFunction, out fExists);
			ODBC.TraceODBC(3, "SQLGetFunctions", retCode);
			return retCode;
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x00096AFF File Offset: 0x00094CFF
		internal ODBC32.RetCode GetInfo2(ODBC32.SQL_INFO info, byte[] buffer, out short cbActual)
		{
			return Interop.Odbc.SQLGetInfoW(this, info, buffer, checked((short)buffer.Length), out cbActual);
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x00096B0E File Offset: 0x00094D0E
		internal ODBC32.RetCode GetInfo1(ODBC32.SQL_INFO info, byte[] buffer)
		{
			return Interop.Odbc.SQLGetInfoW(this, info, buffer, checked((short)buffer.Length), ADP.PtrZero);
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x00096B24 File Offset: 0x00094D24
		internal ODBC32.RetCode SetConnectionAttribute2(ODBC32.SQL_ATTR attribute, IntPtr value, int length)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLSetConnectAttrW(this, attribute, value, length);
			ODBC.TraceODBC(3, "SQLSetConnectAttrW", retCode);
			return retCode;
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x00096B48 File Offset: 0x00094D48
		internal ODBC32.RetCode SetConnectionAttribute3(ODBC32.SQL_ATTR attribute, string buffer, int length)
		{
			return Interop.Odbc.SQLSetConnectAttrW(this, attribute, buffer, length);
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x00096B54 File Offset: 0x00094D54
		internal ODBC32.RetCode SetConnectionAttribute4(ODBC32.SQL_ATTR attribute, IDtcTransaction transaction, int length)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLSetConnectAttrW(this, attribute, transaction, length);
			ODBC.TraceODBC(3, "SQLSetConnectAttrW", retCode);
			return retCode;
		}

		// Token: 0x0400175F RID: 5983
		private OdbcConnectionHandle.HandleState _handleState;

		// Token: 0x020002DB RID: 731
		private enum HandleState
		{
			// Token: 0x04001761 RID: 5985
			Allocated,
			// Token: 0x04001762 RID: 5986
			Connected,
			// Token: 0x04001763 RID: 5987
			Transacted,
			// Token: 0x04001764 RID: 5988
			TransactionInProgress
		}
	}
}
