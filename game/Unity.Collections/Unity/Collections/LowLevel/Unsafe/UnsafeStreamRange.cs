using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200014C RID: 332
	[BurstCompatible]
	internal struct UnsafeStreamRange
	{
		// Token: 0x040003D8 RID: 984
		internal unsafe UnsafeStreamBlock* Block;

		// Token: 0x040003D9 RID: 985
		internal int OffsetInFirstBlock;

		// Token: 0x040003DA RID: 986
		internal int ElementCount;

		// Token: 0x040003DB RID: 987
		internal int LastOffset;

		// Token: 0x040003DC RID: 988
		internal int NumberOfBlocks;
	}
}
