using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200015F RID: 351
	internal sealed class NullableDecimalAverageAggregationOperator : InlinedAggregationOperator<decimal?, Pair<decimal, long>, decimal?>
	{
		// Token: 0x060009DA RID: 2522 RVA: 0x00022F9B File Offset: 0x0002119B
		internal NullableDecimalAverageAggregationOperator(IEnumerable<decimal?> child) : base(child)
		{
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00022FA4 File Offset: 0x000211A4
		protected override decimal? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal? num;
			using (IEnumerator<Pair<decimal, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
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
					num = new decimal?(pair.First / pair.Second);
				}
			}
			return num;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002305C File Offset: 0x0002125C
		protected override QueryOperatorEnumerator<Pair<decimal, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDecimalAverageAggregationOperator.NullableDecimalAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000160 RID: 352
		private class NullableDecimalAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<decimal, long>>
		{
			// Token: 0x060009DD RID: 2525 RVA: 0x00023067 File Offset: 0x00021267
			internal NullableDecimalAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x060009DE RID: 2526 RVA: 0x00023078 File Offset: 0x00021278
			protected override bool MoveNextCore(ref Pair<decimal, long> currentElement)
			{
				decimal num = 0.0m;
				long num2 = 0L;
				QueryOperatorEnumerator<decimal?, TKey> source = this._source;
				decimal? num3 = null;
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
				currentElement = new Pair<decimal, long>(num, num2);
				return num2 > 0L;
			}

			// Token: 0x060009DF RID: 2527 RVA: 0x00023101 File Offset: 0x00021301
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400070D RID: 1805
			private QueryOperatorEnumerator<decimal?, TKey> _source;
		}
	}
}
