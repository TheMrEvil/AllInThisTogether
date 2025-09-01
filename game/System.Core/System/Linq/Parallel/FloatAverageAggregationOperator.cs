using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000149 RID: 329
	internal sealed class FloatAverageAggregationOperator : InlinedAggregationOperator<float, Pair<double, long>, float>
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x00022235 File Offset: 0x00020435
		internal FloatAverageAggregationOperator(IEnumerable<float> child) : base(child)
		{
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00022240 File Offset: 0x00020440
		protected override float InternalAggregate(ref Exception singularExceptionToThrow)
		{
			float result;
			using (IEnumerator<Pair<double, long>> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
					result = 0f;
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
					result = (float)(pair.First / (double)pair.Second);
				}
			}
			return result;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x000222F0 File Offset: 0x000204F0
		protected override QueryOperatorEnumerator<Pair<double, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<float, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new FloatAverageAggregationOperator.FloatAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x0200014A RID: 330
		private class FloatAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<double, long>>
		{
			// Token: 0x06000996 RID: 2454 RVA: 0x000222FB File Offset: 0x000204FB
			internal FloatAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<float, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000997 RID: 2455 RVA: 0x0002230C File Offset: 0x0002050C
			protected override bool MoveNextCore(ref Pair<double, long> currentElement)
			{
				double num = 0.0;
				long num2 = 0L;
				QueryOperatorEnumerator<float, TKey> source = this._source;
				float num3 = 0f;
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
						num += (double)num3;
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

			// Token: 0x06000998 RID: 2456 RVA: 0x00022387 File Offset: 0x00020587
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040006FA RID: 1786
			private QueryOperatorEnumerator<float, TKey> _source;
		}
	}
}
