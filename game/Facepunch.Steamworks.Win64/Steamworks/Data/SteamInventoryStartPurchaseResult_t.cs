using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000187 RID: 391
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamInventoryStartPurchaseResult_t : ICallbackData
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00016C61 File Offset: 0x00014E61
		public int DataSize
		{
			get
			{
				return SteamInventoryStartPurchaseResult_t._datasize;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00016C68 File Offset: 0x00014E68
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamInventoryStartPurchaseResult;
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00016C6F File Offset: 0x00014E6F
		// Note: this type is marked as 'beforefieldinit'.
		static SteamInventoryStartPurchaseResult_t()
		{
		}

		// Token: 0x04000A78 RID: 2680
		internal Result Result;

		// Token: 0x04000A79 RID: 2681
		internal ulong OrderID;

		// Token: 0x04000A7A RID: 2682
		internal ulong TransID;

		// Token: 0x04000A7B RID: 2683
		public static int _datasize = Marshal.SizeOf(typeof(SteamInventoryStartPurchaseResult_t));
	}
}
