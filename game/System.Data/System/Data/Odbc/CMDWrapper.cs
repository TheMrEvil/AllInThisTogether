using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x020002D6 RID: 726
	internal sealed class CMDWrapper
	{
		// Token: 0x06001FA0 RID: 8096 RVA: 0x00094FB1 File Offset: 0x000931B1
		internal CMDWrapper(OdbcConnection connection)
		{
			this._connection = connection;
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001FA1 RID: 8097 RVA: 0x00094FC0 File Offset: 0x000931C0
		// (set) Token: 0x06001FA2 RID: 8098 RVA: 0x00094FC8 File Offset: 0x000931C8
		internal bool Canceling
		{
			get
			{
				return this._canceling;
			}
			set
			{
				this._canceling = value;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x00094FD1 File Offset: 0x000931D1
		internal OdbcConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (set) Token: 0x06001FA4 RID: 8100 RVA: 0x00094FD9 File Offset: 0x000931D9
		internal bool HasBoundColumns
		{
			set
			{
				this._hasBoundColumns = value;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x00094FE2 File Offset: 0x000931E2
		internal OdbcStatementHandle StatementHandle
		{
			get
			{
				return this._stmt;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001FA6 RID: 8102 RVA: 0x00094FEA File Offset: 0x000931EA
		internal OdbcStatementHandle KeyInfoStatement
		{
			get
			{
				return this._keyinfostmt;
			}
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x00094FF2 File Offset: 0x000931F2
		internal void CreateKeyInfoStatementHandle()
		{
			this.DisposeKeyInfoStatementHandle();
			this._keyinfostmt = this._connection.CreateStatementHandle();
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x0009500B File Offset: 0x0009320B
		internal void CreateStatementHandle()
		{
			this.DisposeStatementHandle();
			this._stmt = this._connection.CreateStatementHandle();
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x00095024 File Offset: 0x00093224
		internal void Dispose()
		{
			if (this._dataReaderBuf != null)
			{
				this._dataReaderBuf.Dispose();
				this._dataReaderBuf = null;
			}
			this.DisposeStatementHandle();
			CNativeBuffer nativeParameterBuffer = this._nativeParameterBuffer;
			this._nativeParameterBuffer = null;
			if (nativeParameterBuffer != null)
			{
				nativeParameterBuffer.Dispose();
			}
			this._ssKeyInfoModeOn = false;
			this._ssKeyInfoModeOff = false;
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x00095078 File Offset: 0x00093278
		private void DisposeDescriptorHandle()
		{
			OdbcDescriptorHandle hdesc = this._hdesc;
			if (hdesc != null)
			{
				this._hdesc = null;
				hdesc.Dispose();
			}
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x0009509C File Offset: 0x0009329C
		internal void DisposeStatementHandle()
		{
			this.DisposeKeyInfoStatementHandle();
			this.DisposeDescriptorHandle();
			OdbcStatementHandle stmt = this._stmt;
			if (stmt != null)
			{
				this._stmt = null;
				stmt.Dispose();
			}
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x000950CC File Offset: 0x000932CC
		internal void DisposeKeyInfoStatementHandle()
		{
			OdbcStatementHandle keyinfostmt = this._keyinfostmt;
			if (keyinfostmt != null)
			{
				this._keyinfostmt = null;
				keyinfostmt.Dispose();
			}
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x000950F0 File Offset: 0x000932F0
		internal void FreeStatementHandle(ODBC32.STMT stmt)
		{
			this.DisposeDescriptorHandle();
			OdbcStatementHandle stmt2 = this._stmt;
			if (stmt2 != null)
			{
				try
				{
					ODBC32.RetCode retcode = stmt2.FreeStatement(stmt);
					this.StatementErrorHandler(retcode);
				}
				catch (Exception e)
				{
					if (ADP.IsCatchableExceptionType(e))
					{
						this._stmt = null;
						stmt2.Dispose();
					}
					throw;
				}
			}
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x00095144 File Offset: 0x00093344
		internal void FreeKeyInfoStatementHandle(ODBC32.STMT stmt)
		{
			OdbcStatementHandle keyinfostmt = this._keyinfostmt;
			if (keyinfostmt != null)
			{
				try
				{
					keyinfostmt.FreeStatement(stmt);
				}
				catch (Exception e)
				{
					if (ADP.IsCatchableExceptionType(e))
					{
						this._keyinfostmt = null;
						keyinfostmt.Dispose();
					}
					throw;
				}
			}
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x0009518C File Offset: 0x0009338C
		internal OdbcDescriptorHandle GetDescriptorHandle(ODBC32.SQL_ATTR attribute)
		{
			OdbcDescriptorHandle result = this._hdesc;
			if (this._hdesc == null)
			{
				result = (this._hdesc = new OdbcDescriptorHandle(this._stmt, attribute));
			}
			return result;
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x000951C0 File Offset: 0x000933C0
		internal string GetDiagSqlState()
		{
			string result;
			this._stmt.GetDiagnosticField(out result);
			return result;
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x000951DC File Offset: 0x000933DC
		internal void StatementErrorHandler(ODBC32.RetCode retcode)
		{
			if (retcode <= ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				this._connection.HandleErrorNoThrow(this._stmt, retcode);
				return;
			}
			throw this._connection.HandleErrorNoThrow(this._stmt, retcode);
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x00095208 File Offset: 0x00093408
		internal void UnbindStmtColumns()
		{
			if (this._hasBoundColumns)
			{
				this.FreeStatementHandle(ODBC32.STMT.UNBIND);
				this._hasBoundColumns = false;
			}
		}

		// Token: 0x0400174A RID: 5962
		private OdbcStatementHandle _stmt;

		// Token: 0x0400174B RID: 5963
		private OdbcStatementHandle _keyinfostmt;

		// Token: 0x0400174C RID: 5964
		internal OdbcDescriptorHandle _hdesc;

		// Token: 0x0400174D RID: 5965
		internal CNativeBuffer _nativeParameterBuffer;

		// Token: 0x0400174E RID: 5966
		internal CNativeBuffer _dataReaderBuf;

		// Token: 0x0400174F RID: 5967
		private readonly OdbcConnection _connection;

		// Token: 0x04001750 RID: 5968
		private bool _canceling;

		// Token: 0x04001751 RID: 5969
		internal bool _hasBoundColumns;

		// Token: 0x04001752 RID: 5970
		internal bool _ssKeyInfoModeOn;

		// Token: 0x04001753 RID: 5971
		internal bool _ssKeyInfoModeOff;
	}
}
