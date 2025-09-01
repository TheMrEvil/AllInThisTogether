using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000155 RID: 341
	internal sealed class IntSumAggregationOperator : InlinedAggregationOperator<int, int, int>
	{
		// Token: 0x060009BC RID: 2492 RVA: 0x00022A25 File Offset: 0x00020C25
		internal IntSumAggregationOperator(IEnumerable<int> child) : base(child)
		{
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00022A30 File Offset: 0x00020C30
		protected override int InternalAggregate(ref Exception singularExceptionToThrow)
		{
			checked
			{
				int result;
				using (IEnumerator<int> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
				{
					int num = 0;
					while (enumerator.MoveNext())
					{
						int num2 = enumerator.Current;
						num += num2;
					}
					result = num;
				}
				return result;
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00022A80 File Offset: 0x00020C80
		protected override QueryOperatorEnumerator<int, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new IntSumAggregationOperator.IntSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000156 RID: 342
		private class IntSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<int>
		{
			// Token: 0x060009BF RID: 2495 RVA: 0x00022A8B File Offset: 0x00020C8B
			internal IntSumAggregationOperatorEnumerator(QueryOperatorEnumerator<int, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x060009C0 RID: 2496 RVA: 0x00022A9C File Offset: 0x00020C9C
			protected override bool MoveNextCore(ref int currentElement)
			{
				int num = 0;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<int, TKey> source = this._source;
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
							num2 += num;
						}
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = num2;
					return true;
				}
				return false;
			}

			// Token: 0x060009C1 RID: 2497 RVA: 0x00022AF8 File Offset: 0x00020CF8
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000706 RID: 1798
			private readonly QueryOperatorEnumerator<int, TKey> _source;
		}
	}
}
