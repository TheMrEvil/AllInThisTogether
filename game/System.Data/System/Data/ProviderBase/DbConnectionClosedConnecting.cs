using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace System.Data.ProviderBase
{
	// Token: 0x02000349 RID: 841
	internal sealed class DbConnectionClosedConnecting : DbConnectionBusy
	{
		// Token: 0x06002665 RID: 9829 RVA: 0x000AA803 File Offset: 0x000A8A03
		private DbConnectionClosedConnecting() : base(ConnectionState.Connecting)
		{
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x000AA80C File Offset: 0x000A8A0C
		internal override void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
			connectionFactory.SetInnerConnectionTo(owningObject, DbConnectionClosedPreviouslyOpened.SingletonInstance);
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x000AA81A File Offset: 0x000A8A1A
		internal override bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return this.TryOpenConnection(outerConnection, connectionFactory, retry, userOptions);
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x000AA828 File Offset: 0x000A8A28
		internal override bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			if (retry == null || !retry.Task.IsCompleted)
			{
				throw ADP.ConnectionAlreadyOpen(base.State);
			}
			DbConnectionInternal result = retry.Task.Result;
			if (result == null)
			{
				connectionFactory.SetInnerConnectionTo(outerConnection, this);
				throw ADP.InternalConnectionError(ADP.ConnectionError.GetConnectionReturnsNull);
			}
			connectionFactory.SetInnerConnectionEvent(outerConnection, result);
			return true;
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x000AA878 File Offset: 0x000A8A78
		// Note: this type is marked as 'beforefieldinit'.
		static DbConnectionClosedConnecting()
		{
		}

		// Token: 0x0400192F RID: 6447
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedConnecting();
	}
}
