using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200017B RID: 379
	internal sealed class NullableLongSumAggregationOperator : InlinedAggregationOperator<long?, long?, long?>
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x000246F3 File Offset: 0x000228F3
		internal NullableLongSumAggregationOperator(IEnumerable<long?> child) : base(child)
		{
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x000246FC File Offset: 0x000228FC
		protected override long? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			long? result;
			using (IEnumerator<long?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				long num = 0L;
				while (enumerator.MoveNext())
				{
					long num2 = num;
					result = enumerator.Current;
					num = checked(num2 + result.GetValueOrDefault());
				}
				result = new long?(num);
			}
			return result;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002475C File Offset: 0x0002295C
		protected override QueryOperatorEnumerator<long?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableLongSumAggregationOperator.NullableLongSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x0200017C RID: 380
		private class NullableLongSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<long?>
		{
			// Token: 0x06000A31 RID: 2609 RVA: 0x00024767 File Offset: 0x00022967
			internal NullableLongSumAggregationOperatorEnumerator(QueryOperatorEnumerator<long?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A32 RID: 2610 RVA: 0x00024778 File Offset: 0x00022978
			protected override bool MoveNextCore(ref long? currentElement)
			{
				long? num = null;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<long?, TKey> source = this._source;
				if (source.MoveNext(ref num, ref tkey))
				{
					long num2 = 0L;
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
					currentElement = new long?(num2);
					return true;
				}
				return false;
			}

			// Token: 0x06000A33 RID: 2611 RVA: 0x000247EA File Offset: 0x000229EA
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000725 RID: 1829
			private readonly QueryOperatorEnumerator<long?, TKey> _source;
		}
	}
}
