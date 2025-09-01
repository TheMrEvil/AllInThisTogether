using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200014D RID: 333
	internal sealed class FloatSumAggregationOperator : InlinedAggregationOperator<float, double, float>
	{
		// Token: 0x0600099F RID: 2463 RVA: 0x00022535 File Offset: 0x00020735
		internal FloatSumAggregationOperator(IEnumerable<float> child) : base(child)
		{
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00022540 File Offset: 0x00020740
		protected override float InternalAggregate(ref Exception singularExceptionToThrow)
		{
			float result;
			using (IEnumerator<double> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				double num = 0.0;
				while (enumerator.MoveNext())
				{
					double num2 = enumerator.Current;
					num += num2;
				}
				result = (float)num;
			}
			return result;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00022598 File Offset: 0x00020798
		protected override QueryOperatorEnumerator<double, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<float, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new FloatSumAggregationOperator.FloatSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x0200014E RID: 334
		private class FloatSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double>
		{
			// Token: 0x060009A2 RID: 2466 RVA: 0x000225A3 File Offset: 0x000207A3
			internal FloatSumAggregationOperatorEnumerator(QueryOperatorEnumerator<float, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x060009A3 RID: 2467 RVA: 0x000225B4 File Offset: 0x000207B4
			protected override bool MoveNextCore(ref double currentElement)
			{
				float num = 0f;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<float, TKey> source = this._source;
				if (source.MoveNext(ref num, ref tkey))
				{
					double num2 = 0.0;
					int num3 = 0;
					do
					{
						if ((num3++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						num2 += (double)num;
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = num2;
					return true;
				}
				return false;
			}

			// Token: 0x060009A4 RID: 2468 RVA: 0x0002261D File Offset: 0x0002081D
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040006FE RID: 1790
			private readonly QueryOperatorEnumerator<float, TKey> _source;
		}
	}
}
