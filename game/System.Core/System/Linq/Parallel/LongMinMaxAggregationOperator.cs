using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200015B RID: 347
	internal sealed class LongMinMaxAggregationOperator : InlinedAggregationOperator<long, long, long>
	{
		// Token: 0x060009CE RID: 2510 RVA: 0x00022D45 File Offset: 0x00020F45
		internal LongMinMaxAggregationOperator(IEnumerable<long> child, int sign) : base(child)
		{
			this._sign = sign;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00022D58 File Offset: 0x00020F58
		protected override long InternalAggregate(ref Exception singularExceptionToThrow)
		{
			long result;
			using (IEnumerator<long> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
					result = 0L;
				}
				else
				{
					long num = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							long num2 = enumerator.Current;
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
							long num3 = enumerator.Current;
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

		// Token: 0x060009D0 RID: 2512 RVA: 0x00022DEC File Offset: 0x00020FEC
		protected override QueryOperatorEnumerator<long, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new LongMinMaxAggregationOperator.LongMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x04000709 RID: 1801
		private readonly int _sign;

		// Token: 0x0200015C RID: 348
		private class LongMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<long>
		{
			// Token: 0x060009D1 RID: 2513 RVA: 0x00022DFD File Offset: 0x00020FFD
			internal LongMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<long, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x060009D2 RID: 2514 RVA: 0x00022E18 File Offset: 0x00021018
			protected override bool MoveNextCore(ref long currentElement)
			{
				QueryOperatorEnumerator<long, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						long num2 = 0L;
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
						long num3 = 0L;
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

			// Token: 0x060009D3 RID: 2515 RVA: 0x00022EAE File Offset: 0x000210AE
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400070A RID: 1802
			private QueryOperatorEnumerator<long, TKey> _source;

			// Token: 0x0400070B RID: 1803
			private int _sign;
		}
	}
}
