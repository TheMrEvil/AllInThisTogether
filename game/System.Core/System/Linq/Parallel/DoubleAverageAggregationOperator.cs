using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000143 RID: 323
	internal sealed class DoubleAverageAggregationOperator : InlinedAggregationOperator<double, Pair<double, long>, double>
	{
		// Token: 0x06000981 RID: 2433 RVA: 0x00021E29 File Offset: 0x00020029
		internal DoubleAverageAggregationOperator(IEnumerable<double> child) : base(child)
		{
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00021E34 File Offset: 0x00020034
		protected override double InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double result;
			using (IEnumerator<Pair<double, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
					result = 0.0;
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
					result = pair.First / (double)pair.Second;
				}
			}
			return result;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00021EE8 File Offset: 0x000200E8
		protected override QueryOperatorEnumerator<Pair<double, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DoubleAverageAggregationOperator.DoubleAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000144 RID: 324
		private class DoubleAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<double, long>>
		{
			// Token: 0x06000984 RID: 2436 RVA: 0x00021EF3 File Offset: 0x000200F3
			internal DoubleAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<double, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000985 RID: 2437 RVA: 0x00021F04 File Offset: 0x00020104
			protected override bool MoveNextCore(ref Pair<double, long> currentElement)
			{
				double num = 0.0;
				long num2 = 0L;
				QueryOperatorEnumerator<double, TKey> source = this._source;
				double num3 = 0.0;
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
						num += num3;
						checked
						{
							num2 += 1L;
						}
					}
					while (source.MoveNext(ref num3, ref tkey));
					currentElement = new Pair<double, long>(num, num2);
					return true;
				}
				return false;
			}

			// Token: 0x06000986 RID: 2438 RVA: 0x00021F82 File Offset: 0x00020182
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040006F5 RID: 1781
			private QueryOperatorEnumerator<double, TKey> _source;
		}
	}
}
