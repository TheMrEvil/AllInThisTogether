using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200016A RID: 362
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamAppInstalled_t : ICallbackData
	{
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0001684D File Offset: 0x00014A4D
		public int DataSize
		{
			get
			{
				return SteamAppInstalled_t._datasize;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x00016854 File Offset: 0x00014A54
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamAppInstalled;
			}
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0001685B File Offset: 0x00014A5B
		// Note: this type is marked as 'beforefieldinit'.
		static SteamAppInstalled_t()
		{
		}

		// Token: 0x04000A01 RID: 2561
		internal AppId AppID;

		// Token: 0x04000A02 RID: 2562
		public static int _datasize = Marshal.SizeOf(typeof(SteamAppInstalled_t));
	}
}
