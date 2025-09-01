using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000D5 RID: 213
	internal struct SteamServersConnected_t : ICallbackData
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0001513D File Offset: 0x0001333D
		public int DataSize
		{
			get
			{
				return SteamServersConnected_t._datasize;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00015144 File Offset: 0x00013344
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamServersConnected;
			}
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00015148 File Offset: 0x00013348
		// Note: this type is marked as 'beforefieldinit'.
		static SteamServersConnected_t()
		{
		}

		// Token: 0x040007E2 RID: 2018
		public static int _datasize = Marshal.SizeOf(typeof(SteamServersConnected_t));
	}
}
