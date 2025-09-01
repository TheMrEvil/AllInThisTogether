using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200011F RID: 287
	internal class UnorderedHashRepartitionStream<TInputOutput, THashKey, TIgnoreKey> : HashRepartitionStream<TInputOutput, THashKey, int>
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x0001F57C File Offset: 0x0001D77C
		internal UnorderedHashRepartitionStream(PartitionedStream<TInputOutput, TIgnoreKey> inputStream, Func<TInputOutput, THashKey> keySelector, IEqualityComparer<THashKey> keyComparer, IEqualityComparer<TInputOutput> elementComparer, CancellationToken cancellationToken) : base(inputStream.PartitionCount, Util.GetDefaultComparer<int>(), keyComparer, elementComparer)
		{
			QueryOperatorEnumerator<Pair<TInputOutput, THashKey>, int>[] partitions = new HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey>[inputStream.PartitionCount];
			this._partitions = partitions;
			CountdownEvent barrier = new CountdownEvent(inputStream.PartitionCount);
			ListChunk<Pair<TInputOutput, THashKey>>[][] valueExchangeMatrix = JaggedArray<ListChunk<Pair<TInputOutput, THashKey>>>.Allocate(inputStream.PartitionCount, inputStream.PartitionCount);
			for (int i = 0; i < inputStream.PartitionCount; i++)
			{
				this._partitions[i] = new HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey>(inputStream[i], inputStream.PartitionCount, i, keySelector, this, barrier, valueExchangeMatrix, cancellationToken);
			}
		}
	}
}
