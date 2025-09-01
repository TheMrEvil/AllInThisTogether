using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000F9 RID: 249
	internal struct SteamShutdown_t : ICallbackData
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x000156D6 File Offset: 0x000138D6
		public int DataSize
		{
			get
			{
				return SteamShutdown_t._datasize;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x000156DD File Offset: 0x000138DD
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamShutdown;
			}
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x000156E4 File Offset: 0x000138E4
		// Note: this type is marked as 'beforefieldinit'.
		static SteamShutdown_t()
		{
		}

		// Token: 0x04000858 RID: 2136
		public static int _datasize = Marshal.SizeOf(typeof(SteamShutdown_t));
	}
}
