using System;

namespace Unity.Collections
{
	// Token: 0x02000047 RID: 71
	[Obsolete("NativeMultiHashMapIterator is renamed to NativeParallelMultiHashMapIterator. (UnityUpgradable) -> NativeParallelMultiHashMapIterator<TKey>", false)]
	public struct NativeMultiHashMapIterator<TKey> where TKey : struct
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00004893 File Offset: 0x00002A93
		public int GetEntryIndex()
		{
			return this.EntryIndex;
		}

		// Token: 0x0400009E RID: 158
		internal TKey key;

		// Token: 0x0400009F RID: 159
		internal int NextEntryIndex;

		// Token: 0x040000A0 RID: 160
		internal int EntryIndex;
	}
}
