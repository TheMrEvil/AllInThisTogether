using System;
using Steamworks.Data;

namespace Steamworks.ServerList
{
	// Token: 0x020000CC RID: 204
	public class Favourites : Base
	{
		// Token: 0x06000ADB RID: 2779 RVA: 0x00014F50 File Offset: 0x00013150
		internal override void LaunchQuery()
		{
			MatchMakingKeyValuePair[] filters = this.GetFilters();
			this.request = Base.Internal.RequestFavoritesServerList(base.AppId.Value, ref filters, (uint)filters.Length, IntPtr.Zero);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00014F8F File Offset: 0x0001318F
		public Favourites()
		{
		}
	}
}
