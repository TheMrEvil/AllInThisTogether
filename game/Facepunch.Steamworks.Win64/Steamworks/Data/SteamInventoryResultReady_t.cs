using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000183 RID: 387
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamInventoryResultReady_t : ICallbackData
	{
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00016BD1 File Offset: 0x00014DD1
		public int DataSize
		{
			get
			{
				return SteamInventoryResultReady_t._datasize;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00016BD8 File Offset: 0x00014DD8
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamInventoryResultReady;
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00016BDF File Offset: 0x00014DDF
		// Note: this type is marked as 'beforefieldinit'.
		static SteamInventoryResultReady_t()
		{
		}

		// Token: 0x04000A6D RID: 2669
		internal int Handle;

		// Token: 0x04000A6E RID: 2670
		internal Result Result;

		// Token: 0x04000A6F RID: 2671
		public static int _datasize = Marshal.SizeOf(typeof(SteamInventoryResultReady_t));
	}
}
