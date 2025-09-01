using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001B0 RID: 432
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamItemDetails_t
	{
		// Token: 0x04000B87 RID: 2951
		internal InventoryItemId ItemId;

		// Token: 0x04000B88 RID: 2952
		internal InventoryDefId Definition;

		// Token: 0x04000B89 RID: 2953
		internal ushort Quantity;

		// Token: 0x04000B8A RID: 2954
		internal ushort Flags;
	}
}
