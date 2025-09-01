using System;

namespace System.Data.ProviderBase
{
	// Token: 0x0200034A RID: 842
	internal sealed class DbConnectionClosedNeverOpened : DbConnectionClosed
	{
		// Token: 0x0600266A RID: 9834 RVA: 0x000AA884 File Offset: 0x000A8A84
		private DbConnectionClosedNeverOpened() : base(ConnectionState.Closed, false, true)
		{
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x000AA88F File Offset: 0x000A8A8F
		// Note: this type is marked as 'beforefieldinit'.
		static DbConnectionClosedNeverOpened()
		{
		}

		// Token: 0x04001930 RID: 6448
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedNeverOpened();
	}
}
