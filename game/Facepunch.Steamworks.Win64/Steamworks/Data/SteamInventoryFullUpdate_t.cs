using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000184 RID: 388
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamInventoryFullUpdate_t : ICallbackData
	{
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00016BF5 File Offset: 0x00014DF5
		public int DataSize
		{
			get
			{
				return SteamInventoryFullUpdate_t._datasize;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x00016BFC File Offset: 0x00014DFC
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamInventoryFullUpdate;
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00016C03 File Offset: 0x00014E03
		// Note: this type is marked as 'beforefieldinit'.
		static SteamInventoryFullUpdate_t()
		{
		}

		// Token: 0x04000A70 RID: 2672
		internal int Handle;

		// Token: 0x04000A71 RID: 2673
		public static int _datasize = Marshal.SizeOf(typeof(SteamInventoryFullUpdate_t));
	}
}
