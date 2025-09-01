using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000145 RID: 325
	internal sealed class DoubleMinMaxAggregationOperator : InlinedAggregationOperator<double, double, double>
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x00021F8F File Offset: 0x0002018F
		internal DoubleMinMaxAggregationOperator(IEnumerable<double> child, int sign) : base(child)
		{
			this._sign = sign;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00021FA0 File Offset: 0x000201A0
		protected override double InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double result;
			using (IEnumerator<double> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
					result = 0.0;
				}
				else
				{
					double num = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							double num2 = enumerator.Current;
							if (num2 < num || double.IsNaN(num2))
							{
								num = num2;
							}
						}
					}
					else
					{
						while (enumerator.MoveNext())
						{
							double num3 = enumerator.Current;
							if (num3 > num || double.IsNaN(num))
							{
								num = num3;
							}
						}
					}
					result = num;
				}
			}
			return result;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0002204C File Offset: 0x0002024C
		protected override QueryOperatorEnumerator<double, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DoubleMinMaxAggregationOperator.DoubleMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x040006F6 RID: 1782
		private readonly int _sign;

		// Token: 0x02000146 RID: 326
		private class DoubleMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double>
		{
			// Token: 0x0600098A RID: 2442 RVA: 0x0002205D File Offset: 0x0002025D
			internal DoubleMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<double, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x0600098B RID: 2443 RVA: 0x00022078 File Offset: 0x00020278
			protected override bool MoveNextCore(ref double currentElement)
			{
				QueryOperatorEnumerator<double, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						double num2 = 0.0;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num2 < currentElement || double.IsNaN(num2))
							{
								currentElement = num2;
							}
						}
					}
					else
					{
						double num3 = 0.0;
						while (source.MoveNext(ref num3, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num3 > currentElement || double.IsNaN(currentElement))
							{
								currentElement = num3;
							}
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x0600098C RID: 2444 RVA: 0x00022130 File Offset: 0x00020330
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040006F7 RID: 1783
			private QueryOperatorEnumerator<double, TKey> _source;

			// Token: 0x040006F8 RID: 1784
			private int _sign;
		}
	}
}
