using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000175 RID: 373
	internal sealed class NullableIntSumAggregationOperator : InlinedAggregationOperator<int?, int?, int?>
	{
		// Token: 0x06000A1C RID: 2588 RVA: 0x00024253 File Offset: 0x00022453
		internal NullableIntSumAggregationOperator(IEnumerable<int?> child) : base(child)
		{
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002425C File Offset: 0x0002245C
		protected override int? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			int? result;
			using (IEnumerator<int?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				int num = 0;
				while (enumerator.MoveNext())
				{
					int num2 = num;
					result = enumerator.Current;
					num = checked(num2 + result.GetValueOrDefault());
				}
				result = new int?(num);
			}
			return result;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x000242B8 File Offset: 0x000224B8
		protected override QueryOperatorEnumerator<int?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableIntSumAggregationOperator.NullableIntSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000176 RID: 374
		private class NullableIntSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<int?>
		{
			// Token: 0x06000A1F RID: 2591 RVA: 0x000242C3 File Offset: 0x000224C3
			internal NullableIntSumAggregationOperatorEnumerator(QueryOperatorEnumerator<int?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A20 RID: 2592 RVA: 0x000242D4 File Offset: 0x000224D4
			protected override bool MoveNextCore(ref int? currentElement)
			{
				int? num = null;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<int?, TKey> source = this._source;
				if (source.MoveNext(ref num, ref tkey))
				{
					int num2 = 0;
					int num3 = 0;
					do
					{
						if ((num3++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						checked
						{
							num2 += num.GetValueOrDefault();
						}
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = new int?(num2);
					return true;
				}
				return false;
			}

			// Token: 0x06000A21 RID: 2593 RVA: 0x00024345 File Offset: 0x00022545
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000720 RID: 1824
			private QueryOperatorEnumerator<int?, TKey> _source;
		}
	}
}
