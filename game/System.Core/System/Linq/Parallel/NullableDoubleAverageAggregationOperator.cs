using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000165 RID: 357
	internal sealed class NullableDoubleAverageAggregationOperator : InlinedAggregationOperator<double?, Pair<double, long>, double?>
	{
		// Token: 0x060009EC RID: 2540 RVA: 0x00023480 File Offset: 0x00021680
		internal NullableDoubleAverageAggregationOperator(IEnumerable<double?> child) : base(child)
		{
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0002348C File Offset: 0x0002168C
		protected override double? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double? num;
			using (IEnumerator<Pair<double, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					Pair<double, long> pair = enumerator.Current;
					while (enumerator.MoveNext())
					{
						double first = pair.First;
						Pair<double, long> pair2 = enumerator.Current;
						pair.First = first + pair2.First;
						long second = pair.Second;
						pair2 = enumerator.Current;
						pair.Second = checked(second + pair2.Second);
					}
					num = new double?(pair.First / (double)pair.Second);
				}
			}
			return num;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00023538 File Offset: 0x00021738
		protected override QueryOperatorEnumerator<Pair<double, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDoubleAverageAggregationOperator.NullableDoubleAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000166 RID: 358
		private class NullableDoubleAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<double, long>>
		{
			// Token: 0x060009EF RID: 2543 RVA: 0x00023543 File Offset: 0x00021743
			internal NullableDoubleAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<double?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x060009F0 RID: 2544 RVA: 0x00023554 File Offset: 0x00021754
			protected override bool MoveNextCore(ref Pair<double, long> currentElement)
			{
				double num = 0.0;
				long num2 = 0L;
				QueryOperatorEnumerator<double?, TKey> source = this._source;
				double? num3 = null;
				TKey tkey = default(TKey);
				int num4 = 0;
				while (source.MoveNext(ref num3, ref tkey))
				{
					if (num3 != null)
					{
						if ((num4++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						num += num3.GetValueOrDefault();
						checked
						{
							num2 += 1L;
						}
					}
				}
				currentElement = new Pair<double, long>(num, num2);
				return num2 > 0L;
			}

			// Token: 0x060009F1 RID: 2545 RVA: 0x000235D7 File Offset: 0x000217D7
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000712 RID: 1810
			private QueryOperatorEnumerator<double?, TKey> _source;
		}
	}
}
