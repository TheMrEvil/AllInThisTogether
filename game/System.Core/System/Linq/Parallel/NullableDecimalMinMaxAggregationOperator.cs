using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000161 RID: 353
	internal sealed class NullableDecimalMinMaxAggregationOperator : InlinedAggregationOperator<decimal?, decimal?, decimal?>
	{
		// Token: 0x060009E0 RID: 2528 RVA: 0x0002310E File Offset: 0x0002130E
		internal NullableDecimalMinMaxAggregationOperator(IEnumerable<decimal?> child, int sign) : base(child)
		{
			this._sign = sign;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00023120 File Offset: 0x00021320
		protected override decimal? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal? num;
			using (IEnumerator<decimal?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					decimal? num2 = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							decimal? num3 = enumerator.Current;
							if (num2 != null)
							{
								decimal? num4 = num3;
								decimal? num5 = num2;
								if (!(num4.GetValueOrDefault() < num5.GetValueOrDefault() & (num4 != null & num5 != null)))
								{
									continue;
								}
							}
							num2 = num3;
						}
					}
					else
					{
						while (enumerator.MoveNext())
						{
							decimal? num6 = enumerator.Current;
							if (num2 != null)
							{
								decimal? num5 = num6;
								decimal? num4 = num2;
								if (!(num5.GetValueOrDefault() > num4.GetValueOrDefault() & (num5 != null & num4 != null)))
								{
									continue;
								}
							}
							num2 = num6;
						}
					}
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00023218 File Offset: 0x00021418
		protected override QueryOperatorEnumerator<decimal?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDecimalMinMaxAggregationOperator.NullableDecimalMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x0400070E RID: 1806
		private readonly int _sign;

		// Token: 0x02000162 RID: 354
		private class NullableDecimalMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<decimal?>
		{
			// Token: 0x060009E3 RID: 2531 RVA: 0x00023229 File Offset: 0x00021429
			internal NullableDecimalMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal?, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x060009E4 RID: 2532 RVA: 0x00023244 File Offset: 0x00021444
			protected override bool MoveNextCore(ref decimal? currentElement)
			{
				QueryOperatorEnumerator<decimal?, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						decimal? num2 = null;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (currentElement != null)
							{
								decimal? num3 = num2;
								decimal? num4 = currentElement;
								if (!(num3.GetValueOrDefault() < num4.GetValueOrDefault() & (num3 != null & num4 != null)))
								{
									continue;
								}
							}
							currentElement = num2;
						}
					}
					else
					{
						decimal? num5 = null;
						while (source.MoveNext(ref num5, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (currentElement != null)
							{
								decimal? num4 = num5;
								decimal? num3 = currentElement;
								if (!(num4.GetValueOrDefault() > num3.GetValueOrDefault() & (num4 != null & num3 != null)))
								{
									continue;
								}
							}
							currentElement = num5;
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x060009E5 RID: 2533 RVA: 0x00023354 File Offset: 0x00021554
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400070F RID: 1807
			private QueryOperatorEnumerator<decimal?, TKey> _source;

			// Token: 0x04000710 RID: 1808
			private int _sign;
		}
	}
}
