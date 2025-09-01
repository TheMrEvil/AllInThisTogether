using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000167 RID: 359
	internal sealed class NullableDoubleMinMaxAggregationOperator : InlinedAggregationOperator<double?, double?, double?>
	{
		// Token: 0x060009F2 RID: 2546 RVA: 0x000235E4 File Offset: 0x000217E4
		internal NullableDoubleMinMaxAggregationOperator(IEnumerable<double?> child, int sign) : base(child)
		{
			this._sign = sign;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x000235F4 File Offset: 0x000217F4
		protected override double? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double? num;
			using (IEnumerator<double?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					double? num2 = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							double? num3 = enumerator.Current;
							if (num3 != null)
							{
								if (num2 != null)
								{
									double? num4 = num3;
									double? num5 = num2;
									if (!(num4.GetValueOrDefault() < num5.GetValueOrDefault() & (num4 != null & num5 != null)) && !double.IsNaN(num3.GetValueOrDefault()))
									{
										continue;
									}
								}
								num2 = num3;
							}
						}
					}
					else
					{
						while (enumerator.MoveNext())
						{
							double? num6 = enumerator.Current;
							if (num6 != null)
							{
								if (num2 != null)
								{
									double? num5 = num6;
									double? num4 = num2;
									if (!(num5.GetValueOrDefault() > num4.GetValueOrDefault() & (num5 != null & num4 != null)) && !double.IsNaN(num2.GetValueOrDefault()))
									{
										continue;
									}
								}
								num2 = num6;
							}
						}
					}
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00023714 File Offset: 0x00021914
		protected override QueryOperatorEnumerator<double?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDoubleMinMaxAggregationOperator.NullableDoubleMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x04000713 RID: 1811
		private readonly int _sign;

		// Token: 0x02000168 RID: 360
		private class NullableDoubleMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double?>
		{
			// Token: 0x060009F5 RID: 2549 RVA: 0x00023725 File Offset: 0x00021925
			internal NullableDoubleMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<double?, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x060009F6 RID: 2550 RVA: 0x00023740 File Offset: 0x00021940
			protected override bool MoveNextCore(ref double? currentElement)
			{
				QueryOperatorEnumerator<double?, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						double? num2 = null;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num2 != null)
							{
								if (currentElement != null)
								{
									double? num3 = num2;
									double? num4 = currentElement;
									if (!(num3.GetValueOrDefault() < num4.GetValueOrDefault() & (num3 != null & num4 != null)) && !double.IsNaN(num2.GetValueOrDefault()))
									{
										continue;
									}
								}
								currentElement = num2;
							}
						}
					}
					else
					{
						double? num5 = null;
						while (source.MoveNext(ref num5, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num5 != null)
							{
								if (currentElement != null)
								{
									double? num4 = num5;
									double? num3 = currentElement;
									if (!(num4.GetValueOrDefault() > num3.GetValueOrDefault() & (num4 != null & num3 != null)) && !double.IsNaN(currentElement.GetValueOrDefault()))
									{
										continue;
									}
								}
								currentElement = num5;
							}
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x060009F7 RID: 2551 RVA: 0x0002387A File Offset: 0x00021A7A
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000714 RID: 1812
			private QueryOperatorEnumerator<double?, TKey> _source;

			// Token: 0x04000715 RID: 1813
			private int _sign;
		}
	}
}
