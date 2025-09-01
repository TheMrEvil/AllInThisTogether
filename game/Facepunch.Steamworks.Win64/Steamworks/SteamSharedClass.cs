using System;

namespace Steamworks
{
	// Token: 0x020000BE RID: 190
	public class SteamSharedClass<T> : SteamClass
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0001266D File Offset: 0x0001086D
		internal static SteamInterface Interface
		{
			get
			{
				return SteamSharedClass<T>.InterfaceClient ?? SteamSharedClass<T>.InterfaceServer;
			}
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0001267D File Offset: 0x0001087D
		internal override void InitializeInterface(bool server)
		{
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00012680 File Offset: 0x00010880
		internal virtual void SetInterface(bool server, SteamInterface iface)
		{
			if (server)
			{
				SteamSharedClass<T>.InterfaceServer = iface;
			}
			bool flag = !server;
			if (flag)
			{
				SteamSharedClass<T>.InterfaceClient = iface;
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x000126AC File Offset: 0x000108AC
		internal override void DestroyInterface(bool server)
		{
			bool flag = !server;
			if (flag)
			{
				SteamSharedClass<T>.InterfaceClient = null;
			}
			if (server)
			{
				SteamSharedClass<T>.InterfaceServer = null;
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x000126D7 File Offset: 0x000108D7
		public SteamSharedClass()
		{
		}

		// Token: 0x0400077F RID: 1919
		internal static SteamInterface InterfaceClient;

		// Token: 0x04000780 RID: 1920
		internal static SteamInterface InterfaceServer;
	}
}
