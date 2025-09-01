using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001EB RID: 491
	internal static class ExchangeUtilities
	{
		// Token: 0x06000C13 RID: 3091 RVA: 0x0002A66C File Offset: 0x0002886C
		internal static PartitionedStream<T, int> PartitionDataSource<T>(IEnumerable<T> source, int partitionCount, bool useStriping)
		{
			IParallelPartitionable<T> parallelPartitionable = source as IParallelPartitionable<T>;
			PartitionedStream<T, int> result;
			if (parallelPartitionable != null)
			{
				QueryOperatorEnumerator<T, int>[] partitions = parallelPartitionable.GetPartitions(partitionCount);
				if (partitions == null)
				{
					throw new InvalidOperationException("The return value must not be null.");
				}
				if (partitions.Length != partitionCount)
				{
					throw new InvalidOperationException("The returned array's length must equal the number of partitions requested.");
				}
				PartitionedStream<T, int> partitionedStream = new PartitionedStream<T, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
				for (int i = 0; i < partitionCount; i++)
				{
					QueryOperatorEnumerator<T, int> queryOperatorEnumerator = partitions[i];
					if (queryOperatorEnumerator == null)
					{
						throw new InvalidOperationException("Elements returned must not be null.");
					}
					partitionedStream[i] = queryOperatorEnumerator;
				}
				result = partitionedStream;
			}
			else
			{
				result = new PartitionedDataSource<T>(source, partitionCount, useStriping);
			}
			return result;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002A6F4 File Offset: 0x000288F4
		internal static PartitionedStream<Pair<TElement, THashKey>, int> HashRepartition<TElement, THashKey, TIgnoreKey>(PartitionedStream<TElement, TIgnoreKey> source, Func<TElement, THashKey> keySelector, IEqualityComparer<THashKey> keyComparer, IEqualityComparer<TElement> elementComparer, CancellationToken cancellationToken)
		{
			return new UnorderedHashRepartitionStream<TElement, THashKey, TIgnoreKey>(source, keySelector, keyComparer, elementComparer, cancellationToken);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002A701 File Offset: 0x00028901
		internal static PartitionedStream<Pair<TElement, THashKey>, TOrderKey> HashRepartitionOrdered<TElement, THashKey, TOrderKey>(PartitionedStream<TElement, TOrderKey> source, Func<TElement, THashKey> keySelector, IEqualityComparer<THashKey> keyComparer, IEqualityComparer<TElement> elementComparer, CancellationToken cancellationToken)
		{
			return new OrderedHashRepartitionStream<TElement, THashKey, TOrderKey>(source, keySelector, keyComparer, elementComparer, cancellationToken);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002A70E File Offset: 0x0002890E
		internal static OrdinalIndexState Worse(this OrdinalIndexState state1, OrdinalIndexState state2)
		{
			if (state1 <= state2)
			{
				return state2;
			}
			return state1;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0002A717 File Offset: 0x00028917
		internal static bool IsWorseThan(this OrdinalIndexState state1, OrdinalIndexState state2)
		{
			return state1 > state2;
		}
	}
}
