using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200019D RID: 413
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GSStatsReceived_t : ICallbackData
	{
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x00017030 File Offset: 0x00015230
		public int DataSize
		{
			get
			{
				return GSStatsReceived_t._datasize;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x00017037 File Offset: 0x00015237
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GSStatsReceived;
			}
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0001703E File Offset: 0x0001523E
		// Note: this type is marked as 'beforefieldinit'.
		static GSStatsReceived_t()
		{
		}

		// Token: 0x04000AC6 RID: 2758
		internal Result Result;

		// Token: 0x04000AC7 RID: 2759
		internal ulong SteamIDUser;

		// Token: 0x04000AC8 RID: 2760
		public static int _datasize = Marshal.SizeOf(typeof(GSStatsReceived_t));
	}
}
