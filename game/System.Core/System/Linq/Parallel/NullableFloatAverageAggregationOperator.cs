using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200016B RID: 363
	internal sealed class NullableFloatAverageAggregationOperator : InlinedAggregationOperator<float?, Pair<double, long>, float?>
	{
		// Token: 0x060009FE RID: 2558 RVA: 0x00023996 File Offset: 0x00021B96
		internal NullableFloatAverageAggregationOperator(IEnumerable<float?> child) : base(child)
		{
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x000239A0 File Offset: 0x00021BA0
		protected override float? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			float? num;
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
					num = new float?((float)(pair.First / (double)pair.Second));
				}
			}
			return num;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00023A4C File Offset: 0x00021C4C
		protected override QueryOperatorEnumerator<Pair<double, long>, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<float?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableFloatAverageAggregationOperator.NullableFloatAverageAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x0200016C RID: 364
		private class NullableFloatAverageAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<Pair<double, long>>
		{
			// Token: 0x06000A01 RID: 2561 RVA: 0x00023A57 File Offset: 0x00021C57
			internal NullableFloatAverageAggregationOperatorEnumerator(QueryOperatorEnumerator<float?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A02 RID: 2562 RVA: 0x00023A68 File Offset: 0x00021C68
			protected override bool MoveNextCore(ref Pair<double, long> currentElement)
			{
				double num = 0.0;
				long num2 = 0L;
				QueryOperatorEnumerator<float?, TKey> source = this._source;
				float? num3 = null;
				TKey tkey = default(TKey);
				int num4 = 0;
				while (source.MoveNext(ref num3, ref tkey))
				{
					if ((num4++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					if (num3 != null)
					{
						num += (double)num3.GetValueOrDefault();
						checked
						{
							num2 += 1L;
						}
					}
				}
				currentElement = new Pair<double, long>(num, num2);
				return num2 > 0L;
			}

			// Token: 0x06000A03 RID: 2563 RVA: 0x00023AEC File Offset: 0x00021CEC
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000717 RID: 1815
			private QueryOperatorEnumerator<float?, TKey> _source;
		}
	}
}
