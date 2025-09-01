using System;

namespace Steamworks
{
	// Token: 0x02000098 RID: 152
	public class SteamMatchmakingServers : SteamClientClass<SteamMatchmakingServers>
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0000CDEA File Offset: 0x0000AFEA
		internal static ISteamMatchmakingServers Internal
		{
			get
			{
				return SteamClientClass<SteamMatchmakingServers>.Interface as ISteamMatchmakingServers;
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0000CDF6 File Offset: 0x0000AFF6
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamMatchmakingServers(server));
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0000CE07 File Offset: 0x0000B007
		public SteamMatchmakingServers()
		{
		}
	}
}
