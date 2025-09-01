using System;
using Steamworks.Data;

namespace Steamworks.ServerList
{
	// Token: 0x020000CF RID: 207
	public class Internet : Base
	{
		// Token: 0x06000AE1 RID: 2785 RVA: 0x00015028 File Offset: 0x00013228
		internal override void LaunchQuery()
		{
			MatchMakingKeyValuePair[] filters = this.GetFilters();
			this.request = Base.Internal.RequestInternetServerList(base.AppId.Value, ref filters, (uint)filters.Length, IntPtr.Zero);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00015067 File Offset: 0x00013267
		public Internet()
		{
		}
	}
}
