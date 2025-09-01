using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200016B RID: 363
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamAppUninstalled_t : ICallbackData
	{
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00016871 File Offset: 0x00014A71
		public int DataSize
		{
			get
			{
				return SteamAppUninstalled_t._datasize;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00016878 File Offset: 0x00014A78
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamAppUninstalled;
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0001687F File Offset: 0x00014A7F
		// Note: this type is marked as 'beforefieldinit'.
		static SteamAppUninstalled_t()
		{
		}

		// Token: 0x04000A03 RID: 2563
		internal AppId AppID;

		// Token: 0x04000A04 RID: 2564
		public static int _datasize = Marshal.SizeOf(typeof(SteamAppUninstalled_t));
	}
}
