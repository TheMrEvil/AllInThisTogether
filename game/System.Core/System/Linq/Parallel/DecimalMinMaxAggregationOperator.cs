using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200013F RID: 319
	internal sealed class DecimalMinMaxAggregationOperator : InlinedAggregationOperator<decimal, decimal, decimal>
	{
		// Token: 0x06000975 RID: 2421 RVA: 0x00021B77 File Offset: 0x0001FD77
		internal DecimalMinMaxAggregationOperator(IEnumerable<decimal> child, int sign) : base(child)
		{
			this._sign = sign;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00021B88 File Offset: 0x0001FD88
		protected override decimal InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal result;
			using (IEnumerator<decimal> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
					result = 0m;
				}
				else
				{
					decimal num = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							decimal num2 = enumerator.Current;
							if (num2 < num)
							{
								num = num2;
							}
						}
					}
					else
					{
						while (enumerator.MoveNext())
						{
							decimal num3 = enumerator.Current;
							if (num3 > num)
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

		// Token: 0x06000977 RID: 2423 RVA: 0x00021C2C File Offset: 0x0001FE2C
		protected override QueryOperatorEnumerator<decimal, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DecimalMinMaxAggregationOperator.DecimalMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x040006F1 RID: 1777
		private readonly int _sign;

		// Token: 0x02000140 RID: 320
		private class DecimalMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<decimal>
		{
			// Token: 0x06000978 RID: 2424 RVA: 0x00021C3D File Offset: 0x0001FE3D
			internal DecimalMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x06000979 RID: 2425 RVA: 0x00021C58 File Offset: 0x0001FE58
			protected override bool MoveNextCore(ref decimal currentElement)
			{
				QueryOperatorEnumerator<decimal, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						decimal num2 = 0m;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num2 < currentElement)
							{
								currentElement = num2;
							}
						}
					}
					else
					{
						decimal num3 = 0m;
						while (source.MoveNext(ref num3, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num3 > currentElement)
							{
								currentElement = num3;
							}
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x0600097A RID: 2426 RVA: 0x00021D14 File Offset: 0x0001FF14
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040006F2 RID: 1778
			private QueryOperatorEnumerator<decimal, TKey> _source;

			// Token: 0x040006F3 RID: 1779
			private int _sign;
		}
	}
}
