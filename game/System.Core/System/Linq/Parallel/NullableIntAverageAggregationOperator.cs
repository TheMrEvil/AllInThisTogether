using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000171 RID: 369
	internal sealed class NullableIntAverageAggregationOperator : InlinedAggregationOperator<int?, Pair<long, long>, double?>
	{
		// Token: 0x06000A10 RID: 2576 RVA: 0x00023EB3 File Offset: 0x000220B3
		internal NullableIntAverageAggregationOperator(IEnumerable<int?> child) : base(child)
		{
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00023EBC File Offset: 0x000220BC
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

		// Token: 0x06000A12 RID: 2578 RVA: 0x00023F68 File Offset: 0x00022168
		protected override QueryOperatorEnumerator<Pair<long, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableIntAverageAggregationOperator.NullableIntAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000172 RID: 370
		private class NullableIntAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<long, long>>
		{
			// Token: 0x06000A13 RID: 2579 RVA: 0x00023F73 File Offset: 0x00022173
			internal NullableIntAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<int?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A14 RID: 2580 RVA: 0x00023F84 File Offset: 0x00022184
			protected override bool MoveNextCore(ref Pair<long, long> currentElement)
			{
				long num = 0L;
				long num2 = 0L;
				QueryOperatorEnumerator<int?, TKey> source = this._source;
				int? num3 = null;
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
							num += unchecked((long)num3.GetValueOrDefault());
							num2 += 1L;
						}
					}
				}
				currentElement = new Pair<long, long>(num, num2);
				return num2 > 0L;
			}

			// Token: 0x06000A15 RID: 2581 RVA: 0x00024001 File Offset: 0x00022201
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400071C RID: 1820
			private QueryOperatorEnumerator<int?, TKey> _source;
		}
	}
}
