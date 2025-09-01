using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000153 RID: 339
	internal sealed class IntMinMaxAggregationOperator : InlinedAggregationOperator<int, int, int>
	{
		// Token: 0x060009B6 RID: 2486 RVA: 0x000228B1 File Offset: 0x00020AB1
		internal IntMinMaxAggregationOperator(IEnumerable<int> child, int sign) : base(child)
		{
			this._sign = sign;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x000228C4 File Offset: 0x00020AC4
		protected override int InternalAggregate(ref Exception singularExceptionToThrow)
		{
			int result;
			using (IEnumerator<int> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
					result = 0;
				}
				else
				{
					int num = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							int num2 = enumerator.Current;
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
							int num3 = enumerator.Current;
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

		// Token: 0x060009B8 RID: 2488 RVA: 0x00022958 File Offset: 0x00020B58
		protected override QueryOperatorEnumerator<int, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new IntMinMaxAggregationOperator.IntMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x04000703 RID: 1795
		private readonly int _sign;

		// Token: 0x02000154 RID: 340
		private class IntMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<int>
		{
			// Token: 0x060009B9 RID: 2489 RVA: 0x00022969 File Offset: 0x00020B69
			internal IntMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<int, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x060009BA RID: 2490 RVA: 0x00022984 File Offset: 0x00020B84
			protected override bool MoveNextCore(ref int currentElement)
			{
				QueryOperatorEnumerator<int, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						int num2 = 0;
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
						int num3 = 0;
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

			// Token: 0x060009BB RID: 2491 RVA: 0x00022A18 File Offset: 0x00020C18
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000704 RID: 1796
			private readonly QueryOperatorEnumerator<int, TKey> _source;

			// Token: 0x04000705 RID: 1797
			private readonly int _sign;
		}
	}
}
