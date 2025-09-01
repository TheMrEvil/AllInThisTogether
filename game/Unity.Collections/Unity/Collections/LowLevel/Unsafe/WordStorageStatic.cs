using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000106 RID: 262
	[Obsolete("This storage will no longer be used. (RemovedAfter 2021-06-01)")]
	internal sealed class WordStorageStatic
	{
		// Token: 0x060009D6 RID: 2518 RVA: 0x000020EA File Offset: 0x000002EA
		private WordStorageStatic()
		{
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00002C2B File Offset: 0x00000E2B
		// Note: this type is marked as 'beforefieldinit'.
		static WordStorageStatic()
		{
		}

		// Token: 0x0400033C RID: 828
		public static WordStorageStatic.Thing Ref;

		// Token: 0x02000107 RID: 263
		public struct Thing
		{
			// Token: 0x0400033D RID: 829
			public WordStorage Data;
		}
	}
}
