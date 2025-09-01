using System;

namespace Steamworks
{
	// Token: 0x020000C0 RID: 192
	public class SteamServerClass<T> : SteamClass
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x00012715 File Offset: 0x00010915
		internal override void InitializeInterface(bool server)
		{
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00012718 File Offset: 0x00010918
		internal virtual void SetInterface(bool server, SteamInterface iface)
		{
			bool flag = !server;
			if (flag)
			{
				throw new NotSupportedException();
			}
			SteamServerClass<T>.Interface = iface;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0001273A File Offset: 0x0001093A
		internal override void DestroyInterface(bool server)
		{
			SteamServerClass<T>.Interface = null;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00012743 File Offset: 0x00010943
		public SteamServerClass()
		{
		}

		// Token: 0x04000782 RID: 1922
		internal static SteamInterface Interface;
	}
}
