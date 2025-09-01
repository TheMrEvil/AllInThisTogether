using System;
using System.Data.Common;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x02000345 RID: 837
	internal abstract class DbConnectionClosed : DbConnectionInternal
	{
		// Token: 0x06002654 RID: 9812 RVA: 0x000AA7A7 File Offset: 0x000A89A7
		protected DbConnectionClosed(ConnectionState state, bool hidePassword, bool allowSetConnectionString) : base(state, hidePassword, allowSetConnectionString)
		{
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06002655 RID: 9813 RVA: 0x000AA7B2 File Offset: 0x000A89B2
		public override string ServerVersion
		{
			get
			{
				throw ADP.ClosedConnectionError();
			}
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x000AA7B2 File Offset: 0x000A89B2
		public override DbTransaction BeginTransaction(IsolationLevel il)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000AA7B2 File Offset: 0x000A89B2
		public override void ChangeDatabase(string database)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x00007EED File Offset: 0x000060ED
		internal override void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x000AA7B9 File Offset: 0x000A89B9
		protected override void Deactivate()
		{
			ADP.ClosedConnectionError();
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x000AA7B2 File Offset: 0x000A89B2
		protected internal override DataTable GetSchema(DbConnectionFactory factory, DbConnectionPoolGroup poolGroup, DbConnection outerConnection, string collectionName, string[] restrictions)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x000AA7B2 File Offset: 0x000A89B2
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x000764C6 File Offset: 0x000746C6
		internal override bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return base.TryOpenConnectionInternal(outerConnection, connectionFactory, retry, userOptions);
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x000AA7B2 File Offset: 0x000A89B2
		protected override void Activate(Transaction transaction)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x000AA7B2 File Offset: 0x000A89B2
		public override void EnlistTransaction(Transaction transaction)
		{
			throw ADP.ClosedConnectionError();
		}
	}
}
