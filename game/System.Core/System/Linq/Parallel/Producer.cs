using System;

namespace System.Linq.Parallel
{
	// Token: 0x0200010B RID: 267
	internal readonly struct Producer<TKey>
	{
		// Token: 0x060008C5 RID: 2245 RVA: 0x0001E39C File Offset: 0x0001C59C
		internal Producer(TKey maxKey, int producerIndex)
		{
			this.MaxKey = maxKey;
			this.ProducerIndex = producerIndex;
		}

		// Token: 0x04000620 RID: 1568
		internal readonly TKey MaxKey;

		// Token: 0x04000621 RID: 1569
		internal readonly int ProducerIndex;
	}
}
