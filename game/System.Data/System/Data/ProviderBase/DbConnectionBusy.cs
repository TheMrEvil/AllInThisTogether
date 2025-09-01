using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace System.Data.ProviderBase
{
	// Token: 0x02000346 RID: 838
	internal abstract class DbConnectionBusy : DbConnectionClosed
	{
		// Token: 0x0600265F RID: 9823 RVA: 0x000AA7C1 File Offset: 0x000A89C1
		protected DbConnectionBusy(ConnectionState state) : base(state, true, false)
		{
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x000AA7CC File Offset: 0x000A89CC
		internal override bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			throw ADP.ConnectionAlreadyOpen(base.State);
		}
	}
}
