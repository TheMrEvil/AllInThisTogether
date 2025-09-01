using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x020002EB RID: 747
	internal abstract class OdbcHandle : SafeHandle
	{
		// Token: 0x0600211C RID: 8476 RVA: 0x0009A7D4 File Offset: 0x000989D4
		protected OdbcHandle(ODBC32.SQL_HANDLE handleType, OdbcHandle parentHandle) : base(IntPtr.Zero, true)
		{
			this._handleType = handleType;
			bool flag = false;
			ODBC32.RetCode retCode = ODBC32.RetCode.SUCCESS;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (handleType != ODBC32.SQL_HANDLE.ENV)
				{
					if (handleType - ODBC32.SQL_HANDLE.DBC <= 1)
					{
						parentHandle.DangerousAddRef(ref flag);
						retCode = Interop.Odbc.SQLAllocHandle(handleType, parentHandle, out this.handle);
					}
				}
				else
				{
					retCode = Interop.Odbc.SQLAllocHandle(handleType, IntPtr.Zero, out this.handle);
				}
			}
			finally
			{
				if (flag && handleType - ODBC32.SQL_HANDLE.DBC <= 1)
				{
					if (IntPtr.Zero != this.handle)
					{
						this._parentHandle = parentHandle;
					}
					else
					{
						parentHandle.DangerousRelease();
					}
				}
			}
			if (ADP.PtrZero == this.handle || retCode != ODBC32.RetCode.SUCCESS)
			{
				throw ODBC.CantAllocateEnvironmentHandle(retCode);
			}
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x0009A88C File Offset: 0x00098A8C
		internal OdbcHandle(OdbcStatementHandle parentHandle, ODBC32.SQL_ATTR attribute) : base(IntPtr.Zero, true)
		{
			this._handleType = ODBC32.SQL_HANDLE.DESC;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			ODBC32.RetCode statementAttribute;
			try
			{
				parentHandle.DangerousAddRef(ref flag);
				int num;
				statementAttribute = parentHandle.GetStatementAttribute(attribute, out this.handle, out num);
			}
			finally
			{
				if (flag)
				{
					if (IntPtr.Zero != this.handle)
					{
						this._parentHandle = parentHandle;
					}
					else
					{
						parentHandle.DangerousRelease();
					}
				}
			}
			if (ADP.PtrZero == this.handle)
			{
				throw ODBC.FailedToGetDescriptorHandle(statementAttribute);
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x0009A91C File Offset: 0x00098B1C
		internal ODBC32.SQL_HANDLE HandleType
		{
			get
			{
				return this._handleType;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x00089910 File Offset: 0x00087B10
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x0009A924 File Offset: 0x00098B24
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				ODBC32.SQL_HANDLE handleType = this.HandleType;
				if (handleType - ODBC32.SQL_HANDLE.ENV > 2)
				{
					if (handleType != ODBC32.SQL_HANDLE.DESC)
					{
					}
				}
				else
				{
					Interop.Odbc.SQLFreeHandle(handleType, handle);
				}
			}
			OdbcHandle parentHandle = this._parentHandle;
			this._parentHandle = null;
			if (parentHandle != null)
			{
				parentHandle.DangerousRelease();
			}
			return true;
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0009A988 File Offset: 0x00098B88
		internal ODBC32.RetCode GetDiagnosticField(out string sqlState)
		{
			StringBuilder stringBuilder = new StringBuilder(6);
			short num;
			ODBC32.RetCode retCode = Interop.Odbc.SQLGetDiagFieldW(this.HandleType, this, 1, 4, stringBuilder, checked((short)(2 * stringBuilder.Capacity)), out num);
			ODBC.TraceODBC(3, "SQLGetDiagFieldW", retCode);
			if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				sqlState = stringBuilder.ToString();
			}
			else
			{
				sqlState = ADP.StrEmpty;
			}
			return retCode;
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x0009A9DC File Offset: 0x00098BDC
		internal ODBC32.RetCode GetDiagnosticRecord(short record, out string sqlState, StringBuilder message, out int nativeError, out short cchActual)
		{
			StringBuilder stringBuilder = new StringBuilder(5);
			ODBC32.RetCode retCode = Interop.Odbc.SQLGetDiagRecW(this.HandleType, this, record, stringBuilder, out nativeError, message, checked((short)message.Capacity), out cchActual);
			ODBC.TraceODBC(3, "SQLGetDiagRecW", retCode);
			if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				sqlState = stringBuilder.ToString();
			}
			else
			{
				sqlState = ADP.StrEmpty;
			}
			return retCode;
		}

		// Token: 0x040017BD RID: 6077
		private ODBC32.SQL_HANDLE _handleType;

		// Token: 0x040017BE RID: 6078
		private OdbcHandle _parentHandle;
	}
}
