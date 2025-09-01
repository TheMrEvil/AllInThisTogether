using System;

namespace System.Linq.Parallel
{
	// Token: 0x02000111 RID: 273
	internal interface IPartitionedStreamRecipient<TElement>
	{
		// Token: 0x060008D3 RID: 2259
		void Receive<TKey>(PartitionedStream<TElement, TKey> partitionedStream);
	}
}
