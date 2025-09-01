using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace System.Data.ProviderBase
{
	// Token: 0x0200034B RID: 843
	internal sealed class DbConnectionClosedPreviouslyOpened : DbConnectionClosed
	{
		// Token: 0x0600266C RID: 9836 RVA: 0x000AA89B File Offset: 0x000A8A9B
		private DbConnectionClosedPreviouslyOpened() : base(ConnectionState.Closed, true, true)
		{
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000AA81A File Offset: 0x000A8A1A
		internal override bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return this.TryOpenConnection(outerConnection, connectionFactory, retry, userOptions);
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000AA8A6 File Offset: 0x000A8AA6
		// Note: this type is marked as 'beforefieldinit'.
		static DbConnectionClosedPreviouslyOpened()
		{
		}

		// Token: 0x04001931 RID: 6449
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedPreviouslyOpened();
	}
}
