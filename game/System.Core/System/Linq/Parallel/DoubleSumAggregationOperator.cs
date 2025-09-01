using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000147 RID: 327
	internal sealed class DoubleSumAggregationOperator : InlinedAggregationOperator<double, double, double>
	{
		// Token: 0x0600098D RID: 2445 RVA: 0x0002213D File Offset: 0x0002033D
		internal DoubleSumAggregationOperator(IEnumerable<double> child) : base(child)
		{
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00022148 File Offset: 0x00020348
		protected override double InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double result;
			using (IEnumerator<double> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				double num = 0.0;
				while (enumerator.MoveNext())
				{
					double num2 = enumerator.Current;
					num += num2;
				}
				result = num;
			}
			return result;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x000221A0 File Offset: 0x000203A0
		protected override QueryOperatorEnumerator<double, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DoubleSumAggregationOperator.DoubleSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x02000148 RID: 328
		private class DoubleSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double>
		{
			// Token: 0x06000990 RID: 2448 RVA: 0x000221AB File Offset: 0x000203AB
			internal DoubleSumAggregationOperatorEnumerator(QueryOperatorEnumerator<double, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
			}

			// Token: 0x06000991 RID: 2449 RVA: 0x000221BC File Offset: 0x000203BC
			protected override bool MoveNextCore(ref double currentElement)
			{
				double num = 0.0;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<double, TKey> source = this._source;
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
						num2 += num;
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = num2;
					return true;
				}
				return false;
			}

			// Token: 0x06000992 RID: 2450 RVA: 0x00022228 File Offset: 0x00020428
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040006F9 RID: 1785
			private readonly QueryOperatorEnumerator<double, TKey> _source;
		}
	}
}
