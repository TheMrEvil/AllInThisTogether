using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000177 RID: 375
	internal sealed class NullableLongAverageAggregationOperator : InlinedAggregationOperator<long?, Pair<long, long>, double?>
	{
		// Token: 0x06000A22 RID: 2594 RVA: 0x00024352 File Offset: 0x00022552
		internal NullableLongAverageAggregationOperator(IEnumerable<long?> child) : base(child)
		{
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0002435C File Offset: 0x0002255C
		protected override double? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			checked
			{
				double? num;
				using (IEnumerator<Pair<long, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
				{
					if (!enumerator.MoveNext())
					{
						num = null;
						num = num;
					}
					else
					{
						Pair<long, long> pair = enumerator.Current;
						while (enumerator.MoveNext())
						{
							long first = pair.First;
							Pair<long, long> pair2 = enumerator.Current;
							pair.First = first + pair2.First;
							long second = pair.Second;
							pair2 = enumerator.Current;
							pair.Second = second + pair2.Second;
						}
						num = new double?((double)pair.First / (double)pair.Second);
					}
				}
				return num;
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00024408 File Offset: 0x00022608
		protected override QueryOperatorEnumerator<Pair<long, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableLongAverageAggregationOperator.NullableLongAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000178 RID: 376
		private class NullableLongAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<long, long>>
		{
			// Token: 0x06000A25 RID: 2597 RVA: 0x00024413 File Offset: 0x00022613
			internal NullableLongAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<long?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A26 RID: 2598 RVA: 0x00024424 File Offset: 0x00022624
			protected override bool MoveNextCore(ref Pair<long, long> currentElement)
			{
				long num = 0L;
				long num2 = 0L;
				QueryOperatorEnumerator<long?, TKey> source = this._source;
				long? num3 = null;
				TKey tkey = default(TKey);
				int num4 = 0;
				while (source.MoveNext(ref num3, ref tkey))
				{
					if ((num4++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					checked
					{
						if (num3 != null)
						{
							num += num3.GetValueOrDefault();
							num2 += 1L;
						}
					}
				}
				currentElement = new Pair<long, long>(num, num2);
				return num2 > 0L;
			}

			// Token: 0x06000A27 RID: 2599 RVA: 0x000244A0 File Offset: 0x000226A0
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000721 RID: 1825
			private QueryOperatorEnumerator<long?, TKey> _source;
		}
	}
}
