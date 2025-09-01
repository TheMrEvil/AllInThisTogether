using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000169 RID: 361
	internal sealed class NullableDoubleSumAggregationOperator : InlinedAggregationOperator<double?, double?, double?>
	{
		// Token: 0x060009F8 RID: 2552 RVA: 0x00023887 File Offset: 0x00021A87
		internal NullableDoubleSumAggregationOperator(IEnumerable<double?> child) : base(child)
		{
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00023890 File Offset: 0x00021A90
		protected override double? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double? result;
			using (IEnumerator<double?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				double num = 0.0;
				while (enumerator.MoveNext())
				{
					double num2 = num;
					result = enumerator.Current;
					num = num2 + result.GetValueOrDefault();
				}
				result = new double?(num);
			}
			return result;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x000238F4 File Offset: 0x00021AF4
		protected override QueryOperatorEnumerator<double?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDoubleSumAggregationOperator.NullableDoubleSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x0200016A RID: 362
		private class NullableDoubleSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double?>
		{
			// Token: 0x060009FB RID: 2555 RVA: 0x000238FF File Offset: 0x00021AFF
			internal NullableDoubleSumAggregationOperatorEnumerator(QueryOperatorEnumerator<double?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x060009FC RID: 2556 RVA: 0x00023910 File Offset: 0x00021B10
			protected override bool MoveNextCore(ref double? currentElement)
			{
				double? num = null;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<double?, TKey> source = this._source;
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
						num2 += num.GetValueOrDefault();
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = new double?(num2);
					return true;
				}
				return false;
			}

			// Token: 0x060009FD RID: 2557 RVA: 0x00023989 File Offset: 0x00021B89
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000716 RID: 1814
			private readonly QueryOperatorEnumerator<double?, TKey> _source;
		}
	}
}
