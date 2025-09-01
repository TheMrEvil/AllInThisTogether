using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000179 RID: 377
	internal sealed class NullableLongMinMaxAggregationOperator : InlinedAggregationOperator<long?, long?, long?>
	{
		// Token: 0x06000A28 RID: 2600 RVA: 0x000244AD File Offset: 0x000226AD
		internal NullableLongMinMaxAggregationOperator(IEnumerable<long?> child, int sign) : base(child)
		{
			this._sign = sign;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000244C0 File Offset: 0x000226C0
		protected override long? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			long? num;
			using (IEnumerator<long?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					long? num2 = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							long? num3 = enumerator.Current;
							if (num2 != null)
							{
								long? num4 = num3;
								long? num5 = num2;
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
							long? num6 = enumerator.Current;
							if (num2 != null)
							{
								long? num5 = num6;
								long? num4 = num2;
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

		// Token: 0x06000A2A RID: 2602 RVA: 0x000245B0 File Offset: 0x000227B0
		protected override QueryOperatorEnumerator<long?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableLongMinMaxAggregationOperator.NullableLongMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x04000722 RID: 1826
		private readonly int _sign;

		// Token: 0x0200017A RID: 378
		private class NullableLongMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<long?>
		{
			// Token: 0x06000A2B RID: 2603 RVA: 0x000245C1 File Offset: 0x000227C1
			internal NullableLongMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<long?, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x06000A2C RID: 2604 RVA: 0x000245DC File Offset: 0x000227DC
			protected override bool MoveNextCore(ref long? currentElement)
			{
				QueryOperatorEnumerator<long?, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						long? num2 = null;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (currentElement != null)
							{
								long? num3 = num2;
								long? num4 = currentElement;
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
						long? num5 = null;
						while (source.MoveNext(ref num5, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (currentElement != null)
							{
								long? num4 = num5;
								long? num3 = currentElement;
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

			// Token: 0x06000A2D RID: 2605 RVA: 0x000246E6 File Offset: 0x000228E6
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000723 RID: 1827
			private QueryOperatorEnumerator<long?, TKey> _source;

			// Token: 0x04000724 RID: 1828
			private int _sign;
		}
	}
}
