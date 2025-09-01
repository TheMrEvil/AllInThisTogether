using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000348 RID: 840
	internal sealed class DbConnectionOpenBusy : DbConnectionBusy
	{
		// Token: 0x06002663 RID: 9827 RVA: 0x000AA7EE File Offset: 0x000A89EE
		private DbConnectionOpenBusy() : base(ConnectionState.Open)
		{
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000AA7F7 File Offset: 0x000A89F7
		// Note: this type is marked as 'beforefieldinit'.
		static DbConnectionOpenBusy()
		{
		}

		// Token: 0x0400192E RID: 6446
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionOpenBusy();
	}
}
