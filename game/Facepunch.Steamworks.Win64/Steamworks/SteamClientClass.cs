using System;

namespace Steamworks
{
	// Token: 0x020000BF RID: 191
	public class SteamClientClass<T> : SteamClass
	{
		// Token: 0x060009F9 RID: 2553 RVA: 0x000126E0 File Offset: 0x000108E0
		internal override void InitializeInterface(bool server)
		{
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x000126E4 File Offset: 0x000108E4
		internal virtual void SetInterface(bool server, SteamInterface iface)
		{
			if (server)
			{
				throw new NotSupportedException();
			}
			SteamClientClass<T>.Interface = iface;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00012703 File Offset: 0x00010903
		internal override void DestroyInterface(bool server)
		{
			SteamClientClass<T>.Interface = null;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0001270C File Offset: 0x0001090C
		public SteamClientClass()
		{
		}

		// Token: 0x04000781 RID: 1921
		internal static SteamInterface Interface;
	}
}
