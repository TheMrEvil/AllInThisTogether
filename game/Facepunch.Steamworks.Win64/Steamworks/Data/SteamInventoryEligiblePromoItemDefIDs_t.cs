using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000186 RID: 390
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct SteamInventoryEligiblePromoItemDefIDs_t : ICallbackData
	{
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00016C3D File Offset: 0x00014E3D
		public int DataSize
		{
			get
			{
				return SteamInventoryEligiblePromoItemDefIDs_t._datasize;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00016C44 File Offset: 0x00014E44
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamInventoryEligiblePromoItemDefIDs;
			}
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00016C4B File Offset: 0x00014E4B
		// Note: this type is marked as 'beforefieldinit'.
		static SteamInventoryEligiblePromoItemDefIDs_t()
		{
		}

		// Token: 0x04000A73 RID: 2675
		internal Result Result;

		// Token: 0x04000A74 RID: 2676
		internal ulong SteamID;

		// Token: 0x04000A75 RID: 2677
		internal int UmEligiblePromoItemDefs;

		// Token: 0x04000A76 RID: 2678
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData;

		// Token: 0x04000A77 RID: 2679
		public static int _datasize = Marshal.SizeOf(typeof(SteamInventoryEligiblePromoItemDefIDs_t));
	}
}
