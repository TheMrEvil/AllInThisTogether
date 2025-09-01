using System;

namespace Steamworks.ServerList
{
	// Token: 0x020000D1 RID: 209
	public class LocalNetwork : Base
	{
		// Token: 0x06000AE7 RID: 2791 RVA: 0x0001510C File Offset: 0x0001330C
		internal override void LaunchQuery()
		{
			this.request = Base.Internal.RequestLANServerList(base.AppId.Value, IntPtr.Zero);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00015134 File Offset: 0x00013334
		public LocalNetwork()
		{
		}
	}
}
