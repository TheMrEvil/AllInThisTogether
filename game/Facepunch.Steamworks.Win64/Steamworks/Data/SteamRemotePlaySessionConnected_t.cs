using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200018E RID: 398
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamRemotePlaySessionConnected_t : ICallbackData
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00016D9B File Offset: 0x00014F9B
		public int DataSize
		{
			get
			{
				return SteamRemotePlaySessionConnected_t._datasize;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x00016DA2 File Offset: 0x00014FA2
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamRemotePlaySessionConnected;
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00016DA9 File Offset: 0x00014FA9
		// Note: this type is marked as 'beforefieldinit'.
		static SteamRemotePlaySessionConnected_t()
		{
		}

		// Token: 0x04000A8B RID: 2699
		internal uint SessionID;

		// Token: 0x04000A8C RID: 2700
		public static int _datasize = Marshal.SizeOf(typeof(SteamRemotePlaySessionConnected_t));
	}
}
