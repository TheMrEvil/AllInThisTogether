using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200013D RID: 317
	internal sealed class DecimalAverageAggregationOperator : InlinedAggregationOperator<decimal, Pair<decimal, long>, decimal>
	{
		// Token: 0x0600096F RID: 2415 RVA: 0x00021A07 File Offset: 0x0001FC07
		internal DecimalAverageAggregationOperator(IEnumerable<decimal> child) : base(child)
		{
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00021A10 File Offset: 0x0001FC10
		protected override decimal InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal result;
			using (IEnumerator<Pair<decimal, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
					result = 0m;
				}
				else
				{
					Pair<decimal, long> pair = enumerator.Current;
					while (enumerator.MoveNext())
					{
						decimal first = pair.First;
						Pair<decimal, long> pair2 = enumerator.Current;
						pair.First = first + pair2.First;
						long second = pair.Second;
						pair2 = enumerator.Current;
						pair.Second = checked(second + pair2.Second);
					}
					result = pair.First / pair.Second;
				}
			}
			return result;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00021ACC File Offset: 0x0001FCCC
		protected override QueryOperatorEnumerator<Pair<decimal, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DecimalAverageAggregationOperator.DecimalAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x0200013E RID: 318
		private class DecimalAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<decimal, long>>
		{
			// Token: 0x06000972 RID: 2418 RVA: 0x00021AD7 File Offset: 0x0001FCD7
			internal DecimalAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000973 RID: 2419 RVA: 0x00021AE8 File Offset: 0x0001FCE8
			protected override bool MoveNextCore(ref Pair<decimal, long> currentElement)
			{
				decimal num = 0.0m;
				long num2 = 0L;
				QueryOperatorEnumerator<decimal, TKey> source = this._source;
				decimal d = 0m;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref d, ref tkey))
				{
					int num3 = 0;
					do
					{
						if ((num3++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						num += d;
						checked
						{
							num2 += 1L;
						}
					}
					while (source.MoveNext(ref d, ref tkey));
					currentElement = new Pair<decimal, long>(num, num2);
					return true;
				}
				return false;
			}

			// Token: 0x06000974 RID: 2420 RVA: 0x00021B6A File Offset: 0x0001FD6A
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040006F0 RID: 1776
			private QueryOperatorEnumerator<decimal, TKey> _source;
		}
	}
}
