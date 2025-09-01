using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200016F RID: 367
	internal sealed class NullableFloatSumAggregationOperator : InlinedAggregationOperator<float?, double?, float?>
	{
		// Token: 0x06000A0A RID: 2570 RVA: 0x00023D9F File Offset: 0x00021F9F
		internal NullableFloatSumAggregationOperator(IEnumerable<float?> child) : base(child)
		{
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00023DA8 File Offset: 0x00021FA8
		protected override float? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			float? result;
			using (IEnumerator<double?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				double num = 0.0;
				while (enumerator.MoveNext())
				{
					double num2 = num;
					double? num3 = enumerator.Current;
					num = num2 + num3.GetValueOrDefault();
				}
				result = new float?((float)num);
			}
			return result;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00023E10 File Offset: 0x00022010
		protected override QueryOperatorEnumerator<double?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<float?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableFloatSumAggregationOperator.NullableFloatSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000170 RID: 368
		private class NullableFloatSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double?>
		{
			// Token: 0x06000A0D RID: 2573 RVA: 0x00023E1B File Offset: 0x0002201B
			internal NullableFloatSumAggregationOperatorEnumerator(QueryOperatorEnumerator<float?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000A0E RID: 2574 RVA: 0x00023E2C File Offset: 0x0002202C
			protected override bool MoveNextCore(ref double? currentElement)
			{
				float? num = null;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<float?, TKey> source = this._source;
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
						num2 += (double)num.GetValueOrDefault();
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = new double?(num2);
					return true;
				}
				return false;
			}

			// Token: 0x06000A0F RID: 2575 RVA: 0x00023EA6 File Offset: 0x000220A6
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400071B RID: 1819
			private readonly QueryOperatorEnumerator<float?, TKey> _source;
		}
	}
}
