using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000190 RID: 400
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamNetConnectionStatusChangedCallback_t : ICallbackData
	{
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00016DE3 File Offset: 0x00014FE3
		public int DataSize
		{
			get
			{
				return SteamNetConnectionStatusChangedCallback_t._datasize;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x00016DEA File Offset: 0x00014FEA
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamNetConnectionStatusChangedCallback;
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00016DF1 File Offset: 0x00014FF1
		// Note: this type is marked as 'beforefieldinit'.
		static SteamNetConnectionStatusChangedCallback_t()
		{
		}

		// Token: 0x04000A8F RID: 2703
		internal Connection Conn;

		// Token: 0x04000A90 RID: 2704
		internal ConnectionInfo Nfo;

		// Token: 0x04000A91 RID: 2705
		internal ConnectionState OldState;

		// Token: 0x04000A92 RID: 2706
		public static int _datasize = Marshal.SizeOf(typeof(SteamNetConnectionStatusChangedCallback_t));
	}
}
