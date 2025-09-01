using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000157 RID: 343
	internal sealed class LongAverageAggregationOperator : InlinedAggregationOperator<long, Pair<long, long>, double>
	{
		// Token: 0x060009C2 RID: 2498 RVA: 0x00022B05 File Offset: 0x00020D05
		internal LongAverageAggregationOperator(IEnumerable<long> child) : base(child)
		{
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00022B10 File Offset: 0x00020D10
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

		// Token: 0x060009C4 RID: 2500 RVA: 0x00022BC4 File Offset: 0x00020DC4
		protected override QueryOperatorEnumerator<Pair<long, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new LongAverageAggregationOperator.LongAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000158 RID: 344
		private class LongAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<long, long>>
		{
			// Token: 0x060009C5 RID: 2501 RVA: 0x00022BCF File Offset: 0x00020DCF
			internal LongAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<long, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x060009C6 RID: 2502 RVA: 0x00022BE0 File Offset: 0x00020DE0
			protected override bool MoveNextCore(ref Pair<long, long> currentElement)
			{
				long num = 0L;
				long num2 = 0L;
				QueryOperatorEnumerator<long, TKey> source = this._source;
				long num3 = 0L;
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
							num += num3;
							num2 += 1L;
						}
					}
					while (source.MoveNext(ref num3, ref tkey));
					currentElement = new Pair<long, long>(num, num2);
					return true;
				}
				return false;
			}

			// Token: 0x060009C7 RID: 2503 RVA: 0x00022C50 File Offset: 0x00020E50
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000707 RID: 1799
			private QueryOperatorEnumerator<long, TKey> _source;
		}
	}
}
