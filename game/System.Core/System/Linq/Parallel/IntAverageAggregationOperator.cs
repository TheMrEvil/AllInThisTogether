using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000151 RID: 337
	internal sealed class IntAverageAggregationOperator : InlinedAggregationOperator<int, Pair<long, long>, double>
	{
		// Token: 0x060009B0 RID: 2480 RVA: 0x0002275A File Offset: 0x0002095A
		internal IntAverageAggregationOperator(IEnumerable<int> child) : base(child)
		{
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00022764 File Offset: 0x00020964
		protected override double InternalAggregate(ref Exception singularExceptionToThrow)
		{
			checked
			{
				double result;
				using (IEnumerator<Pair<long, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
				{
					if (!enumerator.MoveNext())
					{
						singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
						result = 0.0;
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
						result = (double)pair.First / (double)pair.Second;
					}
				}
				return result;
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00022818 File Offset: 0x00020A18
		protected override QueryOperatorEnumerator<Pair<long, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new IntAverageAggregationOperator.IntAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000152 RID: 338
		private class IntAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<long, long>>
		{
			// Token: 0x060009B3 RID: 2483 RVA: 0x00022823 File Offset: 0x00020A23
			internal IntAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<int, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x060009B4 RID: 2484 RVA: 0x00022834 File Offset: 0x00020A34
			protected override bool MoveNextCore(ref Pair<long, long> currentElement)
			{
				long num = 0L;
				long num2 = 0L;
				QueryOperatorEnumerator<int, TKey> source = this._source;
				int num3 = 0;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref num3, ref tkey))
				{
					int num4 = 0;
					do
					{
						if ((num4++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						checked
						{
							num += unchecked((long)num3);
							num2 += 1L;
						}
					}
					while (source.MoveNext(ref num3, ref tkey));
					currentElement = new Pair<long, long>(num, num2);
					return true;
				}
				return false;
			}

			// Token: 0x060009B5 RID: 2485 RVA: 0x000228A4 File Offset: 0x00020AA4
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000702 RID: 1794
			private QueryOperatorEnumerator<int, TKey> _source;
		}
	}
}
