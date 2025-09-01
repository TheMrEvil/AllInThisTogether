using System;

namespace Unity.Collections
{
	// Token: 0x020000C2 RID: 194
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int)
	})]
	public struct NativeParallelMultiHashMapIterator<TKey> where TKey : struct
	{
		// Token: 0x0600076C RID: 1900 RVA: 0x00017014 File Offset: 0x00015214
		public int GetEntryIndex()
		{
			return this.EntryIndex;
		}

		// Token: 0x0400028F RID: 655
		internal TKey key;

		// Token: 0x04000290 RID: 656
		internal int NextEntryIndex;

		// Token: 0x04000291 RID: 657
		internal int EntryIndex;
	}
}
