using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200019F RID: 415
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GSStatsUnloaded_t : ICallbackData
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00017078 File Offset: 0x00015278
		public int DataSize
		{
			get
			{
				return GSStatsUnloaded_t._datasize;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0001707F File Offset: 0x0001527F
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.UserStatsUnloaded;
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00017086 File Offset: 0x00015286
		// Note: this type is marked as 'beforefieldinit'.
		static GSStatsUnloaded_t()
		{
		}

		// Token: 0x04000ACC RID: 2764
		internal ulong SteamIDUser;

		// Token: 0x04000ACD RID: 2765
		public static int _datasize = Marshal.SizeOf(typeof(GSStatsUnloaded_t));
	}
}
