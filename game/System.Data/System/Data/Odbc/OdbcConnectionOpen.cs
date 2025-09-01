using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Transactions;

namespace System.Data.Odbc
{
	// Token: 0x02000300 RID: 768
	internal sealed class OdbcConnectionOpen : DbConnectionInternal
	{
		// Token: 0x06002216 RID: 8726 RVA: 0x0009EC9C File Offset: 0x0009CE9C
		internal OdbcConnectionOpen(OdbcConnection outerConnection, OdbcConnectionString connectionOptions)
		{
			OdbcEnvironmentHandle globalEnvironmentHandle = OdbcEnvironment.GetGlobalEnvironmentHandle();
			outerConnection.ConnectionHandle = new OdbcConnectionHandle(outerConnection, connectionOptions, globalEnvironmentHandle);
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06002217 RID: 8727 RVA: 0x0009ECC4 File Offset: 0x0009CEC4
		internal OdbcConnection OuterConnection
		{
			get
			{
				OdbcConnection odbcConnection = (OdbcConnection)base.Owner;
				if (odbcConnection == null)
				{
					throw ODBC.OpenConnectionNoOwner();
				}
				return odbcConnection;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x0009ECE7 File Offset: 0x0009CEE7
		public override string ServerVersion
		{
			get
			{
				return this.OuterConnection.Open_GetServerVersion();
			}
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x00007EED File Offset: 0x000060ED
		protected override void Activate(Transaction transaction)
		{
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x0009ECF4 File Offset: 0x0009CEF4
		public override DbTransaction BeginTransaction(IsolationLevel isolevel)
		{
			return this.BeginOdbcTransaction(isolevel);
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x0009ECFD File Offset: 0x0009CEFD
		internal OdbcTransaction BeginOdbcTransaction(IsolationLevel isolevel)
		{
			return this.OuterConnection.Open_BeginTransaction(isolevel);
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x0009ED0B File Offset: 0x0009CF0B
		public override void ChangeDatabase(string value)
		{
			this.OuterConnection.Open_ChangeDatabase(value);
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x0009ED19 File Offset: 0x0009CF19
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			return new OdbcReferenceCollection();
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x0009ED20 File Offset: 0x0009CF20
		protected override void Deactivate()
		{
			base.NotifyWeakReference(0);
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x00007EED File Offset: 0x000060ED
		public override void EnlistTransaction(Transaction transaction)
		{
		}
	}
}
