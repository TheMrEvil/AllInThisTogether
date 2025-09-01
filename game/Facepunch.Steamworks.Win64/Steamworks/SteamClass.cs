using System;

namespace Steamworks
{
	// Token: 0x020000BD RID: 189
	public abstract class SteamClass
	{
		// Token: 0x060009F1 RID: 2545
		internal abstract void InitializeInterface(bool server);

		// Token: 0x060009F2 RID: 2546
		internal abstract void DestroyInterface(bool server);

		// Token: 0x060009F3 RID: 2547 RVA: 0x00012664 File Offset: 0x00010864
		protected SteamClass()
		{
		}
	}
}
