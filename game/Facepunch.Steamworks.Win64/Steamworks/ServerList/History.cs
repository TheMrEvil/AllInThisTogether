using System;
using Steamworks.Data;

namespace Steamworks.ServerList
{
	// Token: 0x020000CE RID: 206
	public class History : Base
	{
		// Token: 0x06000ADF RID: 2783 RVA: 0x00014FE0 File Offset: 0x000131E0
		internal override void LaunchQuery()
		{
			MatchMakingKeyValuePair[] filters = this.GetFilters();
			this.request = Base.Internal.RequestHistoryServerList(base.AppId.Value, ref filters, (uint)filters.Length, IntPtr.Zero);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0001501F File Offset: 0x0001321F
		public History()
		{
		}
	}
}
