using System;

namespace Unity.Collections
{
	// Token: 0x020000C9 RID: 201
	internal struct NativeQueueBlockHeader
	{
		// Token: 0x0400029B RID: 667
		public unsafe NativeQueueBlockHeader* m_NextBlock;

		// Token: 0x0400029C RID: 668
		public int m_NumItems;
	}
}
