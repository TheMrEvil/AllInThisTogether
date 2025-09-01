using System;
using Steamworks.Data;

namespace Steamworks.ServerList
{
	// Token: 0x020000CD RID: 205
	public class Friends : Base
	{
		// Token: 0x06000ADD RID: 2781 RVA: 0x00014F98 File Offset: 0x00013198
		internal override void LaunchQuery()
		{
			MatchMakingKeyValuePair[] filters = this.GetFilters();
			this.request = Base.Internal.RequestFriendsServerList(base.AppId.Value, ref filters, (uint)filters.Length, IntPtr.Zero);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00014FD7 File Offset: 0x000131D7
		public Friends()
		{
		}
	}
}
