using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200012E RID: 302
	[BurstCompatible]
	public struct UnsafeHashMapBucketData
	{
		// Token: 0x06000B16 RID: 2838 RVA: 0x00020A42 File Offset: 0x0001EC42
		internal unsafe UnsafeHashMapBucketData(byte* v, byte* k, byte* n, byte* b, int bcm)
		{
			this.values = v;
			this.keys = k;
			this.next = n;
			this.buckets = b;
			this.bucketCapacityMask = bcm;
		}

		// Token: 0x04000398 RID: 920
		public unsafe readonly byte* values;

		// Token: 0x04000399 RID: 921
		public unsafe readonly byte* keys;

		// Token: 0x0400039A RID: 922
		public unsafe readonly byte* next;

		// Token: 0x0400039B RID: 923
		public unsafe readonly byte* buckets;

		// Token: 0x0400039C RID: 924
		public readonly int bucketCapacityMask;
	}
}
