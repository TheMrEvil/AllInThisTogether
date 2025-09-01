using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000163 RID: 355
	internal sealed class NullableDecimalSumAggregationOperator : InlinedAggregationOperator<decimal?, decimal?, decimal?>
	{
		// Token: 0x060009E6 RID: 2534 RVA: 0x00023361 File Offset: 0x00021561
		internal NullableDecimalSumAggregationOperator(IEnumerable<decimal?> child) : base(child)
		{
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0002336C File Offset: 0x0002156C
		protected override decimal? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal? result;
			using (IEnumerator<decimal?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				decimal num = 0.0m;
				while (enumerator.MoveNext())
				{
					decimal d = num;
					result = enumerator.Current;
					num = d + result.GetValueOrDefault();
				}
				result = new decimal?(num);
			}
			return result;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x000233D8 File Offset: 0x000215D8
		protected override QueryOperatorEnumerator<decimal?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDecimalSumAggregationOperator.NullableDecimalSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000164 RID: 356
		private class NullableDecimalSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<decimal?>
		{
			// Token: 0x060009E9 RID: 2537 RVA: 0x000233E3 File Offset: 0x000215E3
			internal NullableDecimalSumAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x060009EA RID: 2538 RVA: 0x000233F4 File Offset: 0x000215F4
			protected override bool MoveNextCore(ref decimal? currentElement)
			{
				decimal? num = null;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<decimal?, TKey> source = this._source;
				if (source.MoveNext(ref num, ref tkey))
				{
					decimal num2 = 0.0m;
					int num3 = 0;
					do
					{
						if ((num3++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						num2 += num.GetValueOrDefault();
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = new decimal?(num2);
					return true;
				}
				return false;
			}

			// Token: 0x060009EB RID: 2539 RVA: 0x00023473 File Offset: 0x00021673
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000711 RID: 1809
			private readonly QueryOperatorEnumerator<decimal?, TKey> _source;
		}
	}
}
