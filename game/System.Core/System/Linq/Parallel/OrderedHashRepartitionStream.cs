using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000114 RID: 276
	internal class OrderedHashRepartitionStream<TInputOutput, THashKey, TOrderKey> : HashRepartitionStream<TInputOutput, THashKey, TOrderKey>
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x0001EC64 File Offset: 0x0001CE64
		internal OrderedHashRepartitionStream(PartitionedStream<TInputOutput, TOrderKey> inputStream, Func<TInputOutput, THashKey> hashKeySelector, IEqualityComparer<THashKey> hashKeyComparer, IEqualityComparer<TInputOutput> elementComparer, CancellationToken cancellationToken) : base(inputStream.PartitionCount, inputStream.KeyComparer, hashKeyComparer, elementComparer)
		{
			QueryOperatorEnumerator<Pair<TInputOutput, THashKey>, TOrderKey>[] partitions = new OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey>[inputStream.PartitionCount];
			this._partitions = partitions;
			CountdownEvent barrier = new CountdownEvent(inputStream.PartitionCount);
			ListChunk<Pair<TInputOutput, THashKey>>[][] valueExchangeMatrix = JaggedArray<ListChunk<Pair<TInputOutput, THashKey>>>.Allocate(inputStream.PartitionCount, inputStream.PartitionCount);
			ListChunk<TOrderKey>[][] keyExchangeMatrix = JaggedArray<ListChunk<TOrderKey>>.Allocate(inputStream.PartitionCount, inputStream.PartitionCount);
			for (int i = 0; i < inputStream.PartitionCount; i++)
			{
				this._partitions[i] = new OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey>(inputStream[i], inputStream.PartitionCount, i, hashKeySelector, this, barrier, valueExchangeMatrix, keyExchangeMatrix, cancellationToken);
			}
		}
	}
}
