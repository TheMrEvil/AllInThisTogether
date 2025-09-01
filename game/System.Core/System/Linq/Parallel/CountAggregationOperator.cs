using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200013B RID: 315
	internal sealed class CountAggregationOperator<TSource> : InlinedAggregationOperator<TSource, int, int>
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x00021921 File Offset: 0x0001FB21
		internal CountAggregationOperator(IEnumerable<TSource> child) : base(child)
		{
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0002192C File Offset: 0x0001FB2C
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

		// Token: 0x0600096B RID: 2411 RVA: 0x0002197C File Offset: 0x0001FB7C
		protected override QueryOperatorEnumerator<int, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<TSource, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new CountAggregationOperator<TSource>.CountAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x0200013C RID: 316
		private class CountAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<int>
		{
			// Token: 0x0600096C RID: 2412 RVA: 0x00021987 File Offset: 0x0001FB87
			internal CountAggregationOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x0600096D RID: 2413 RVA: 0x00021998 File Offset: 0x0001FB98
			protected override bool MoveNextCore(ref int currentElement)
			{
				TSource tsource = default(TSource);
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<TSource, TKey> source = this._source;
				if (source.MoveNext(ref tsource, ref tkey))
				{
					int num = 0;
					int num2 = 0;
					do
					{
						if ((num2++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						checked
						{
							num++;
						}
					}
					while (source.MoveNext(ref tsource, ref tkey));
					currentElement = num;
					return true;
				}
				return false;
			}

			// Token: 0x0600096E RID: 2414 RVA: 0x000219FA File Offset: 0x0001FBFA
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040006EF RID: 1775
			private readonly QueryOperatorEnumerator<TSource, TKey> _source;
		}
	}
}
