using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000159 RID: 345
	internal sealed class LongCountAggregationOperator<TSource> : InlinedAggregationOperator<TSource, long, long>
	{
		// Token: 0x060009C8 RID: 2504 RVA: 0x00022C5D File Offset: 0x00020E5D
		internal LongCountAggregationOperator(IEnumerable<TSource> child) : base(child)
		{
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00022C68 File Offset: 0x00020E68
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

		// Token: 0x060009CA RID: 2506 RVA: 0x00022CB8 File Offset: 0x00020EB8
		protected override QueryOperatorEnumerator<long, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<TSource, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new LongCountAggregationOperator<TSource>.LongCountAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x0200015A RID: 346
		private class LongCountAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<long>
		{
			// Token: 0x060009CB RID: 2507 RVA: 0x00022CC3 File Offset: 0x00020EC3
			internal LongCountAggregationOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x060009CC RID: 2508 RVA: 0x00022CD4 File Offset: 0x00020ED4
			protected override bool MoveNextCore(ref long currentElement)
			{
				TSource tsource = default(TSource);
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<TSource, TKey> source = this._source;
				if (source.MoveNext(ref tsource, ref tkey))
				{
					long num = 0L;
					int num2 = 0;
					do
					{
						if ((num2++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						checked
						{
							num += 1L;
						}
					}
					while (source.MoveNext(ref tsource, ref tkey));
					currentElement = num;
					return true;
				}
				return false;
			}

			// Token: 0x060009CD RID: 2509 RVA: 0x00022D38 File Offset: 0x00020F38
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000708 RID: 1800
			private readonly QueryOperatorEnumerator<TSource, TKey> _source;
		}
	}
}
