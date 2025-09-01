using System;

namespace Steamworks.Data
{
	// Token: 0x020001EE RID: 494
	public struct Socket
	{
		// Token: 0x06000FA8 RID: 4008 RVA: 0x000197A3 File Offset: 0x000179A3
		public override string ToString()
		{
			return this.Id.ToString();
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x000197B0 File Offset: 0x000179B0
		public static implicit operator Socket(uint value)
		{
			return new Socket
			{
				Id = value
			};
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x000197CE File Offset: 0x000179CE
		public static implicit operator uint(Socket value)
		{
			return value.Id;
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x000197D8 File Offset: 0x000179D8
		public bool Close()
		{
			return SteamNetworkingSockets.Internal.CloseListenSocket(this.Id);
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x000197FF File Offset: 0x000179FF
		// (set) Token: 0x06000FAD RID: 4013 RVA: 0x0001980C File Offset: 0x00017A0C
		public SocketManager Manager
		{
			get
			{
				return SteamNetworkingSockets.GetSocketManager(this.Id);
			}
			set
			{
				SteamNetworkingSockets.SetSocketManager(this.Id, value);
			}
		}

		// Token: 0x04000BEA RID: 3050
		internal uint Id;
	}
}
