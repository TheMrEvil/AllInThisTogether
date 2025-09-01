using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000141 RID: 321
	internal sealed class DecimalSumAggregationOperator : InlinedAggregationOperator<decimal, decimal, decimal>
	{
		// Token: 0x0600097B RID: 2427 RVA: 0x00021D21 File Offset: 0x0001FF21
		internal DecimalSumAggregationOperator(IEnumerable<decimal> child) : base(child)
		{
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00021D2C File Offset: 0x0001FF2C
		protected override decimal InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal result;
			using (IEnumerator<decimal> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				decimal num = 0.0m;
				while (enumerator.MoveNext())
				{
					decimal d = enumerator.Current;
					num += d;
				}
				result = num;
			}
			return result;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00021D8C File Offset: 0x0001FF8C
		protected override QueryOperatorEnumerator<decimal, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DecimalSumAggregationOperator.DecimalSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000142 RID: 322
		private class DecimalSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<decimal>
		{
			// Token: 0x0600097E RID: 2430 RVA: 0x00021D97 File Offset: 0x0001FF97
			internal DecimalSumAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x0600097F RID: 2431 RVA: 0x00021DA8 File Offset: 0x0001FFA8
			protected override bool MoveNextCore(ref decimal currentElement)
			{
				decimal d = 0m;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<decimal, TKey> source = this._source;
				if (source.MoveNext(ref d, ref tkey))
				{
					decimal num = 0.0m;
					int num2 = 0;
					do
					{
						if ((num2++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						num += d;
					}
					while (source.MoveNext(ref d, ref tkey));
					currentElement = num;
					return true;
				}
				return false;
			}

			// Token: 0x06000980 RID: 2432 RVA: 0x00021E1C File Offset: 0x0002001C
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040006F4 RID: 1780
			private QueryOperatorEnumerator<decimal, TKey> _source;
		}
	}
}
