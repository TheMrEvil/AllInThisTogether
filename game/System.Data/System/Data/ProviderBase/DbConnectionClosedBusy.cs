using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000347 RID: 839
	internal sealed class DbConnectionClosedBusy : DbConnectionBusy
	{
		// Token: 0x06002661 RID: 9825 RVA: 0x000AA7D9 File Offset: 0x000A89D9
		private DbConnectionClosedBusy() : base(ConnectionState.Closed)
		{
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000AA7E2 File Offset: 0x000A89E2
		// Note: this type is marked as 'beforefieldinit'.
		static DbConnectionClosedBusy()
		{
		}

		// Token: 0x0400192D RID: 6445
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedBusy();
	}
}
