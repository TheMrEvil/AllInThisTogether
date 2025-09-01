using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200015D RID: 349
	internal sealed class LongSumAggregationOperator : InlinedAggregationOperator<long, long, long>
	{
		// Token: 0x060009D4 RID: 2516 RVA: 0x00022EBB File Offset: 0x000210BB
		internal LongSumAggregationOperator(IEnumerable<long> child) : base(child)
		{
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00022EC4 File Offset: 0x000210C4
		protected override long InternalAggregate(ref Exception singularExceptionToThrow)
		{
			checked
			{
				long result;
				using (IEnumerator<long> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
				{
					long num = 0L;
					while (enumerator.MoveNext())
					{
						long num2 = enumerator.Current;
						num += num2;
					}
					result = num;
				}
				return result;
			}
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00022F14 File Offset: 0x00021114
		protected override QueryOperatorEnumerator<long, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new LongSumAggregationOperator.LongSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x0200015E RID: 350
		private class LongSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<long>
		{
			// Token: 0x060009D7 RID: 2519 RVA: 0x00022F1F File Offset: 0x0002111F
			internal LongSumAggregationOperatorEnumerator(QueryOperatorEnumerator<long, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x060009D8 RID: 2520 RVA: 0x00022F30 File Offset: 0x00021130
			protected override bool MoveNextCore(ref long currentElement)
			{
				long num = 0L;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<long, TKey> source = this._source;
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
							num2 += num;
						}
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = num2;
					return true;
				}
				return false;
			}

			// Token: 0x060009D9 RID: 2521 RVA: 0x00022F8E File Offset: 0x0002118E
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400070C RID: 1804
			private readonly QueryOperatorEnumerator<long, TKey> _source;
		}
	}
}
