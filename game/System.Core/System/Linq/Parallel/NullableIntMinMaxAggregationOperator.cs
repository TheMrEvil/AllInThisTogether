using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000173 RID: 371
	internal sealed class NullableIntMinMaxAggregationOperator : InlinedAggregationOperator<int?, int?, int?>
	{
		// Token: 0x06000A16 RID: 2582 RVA: 0x0002400E File Offset: 0x0002220E
		internal NullableIntMinMaxAggregationOperator(IEnumerable<int?> child, int sign) : base(child)
		{
			this._sign = sign;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00024020 File Offset: 0x00022220
		protected override int? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			int? num;
			using (IEnumerator<int?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					int? num2 = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							int? num3 = enumerator.Current;
							if (num2 != null)
							{
								int? num4 = num3;
								int? num5 = num2;
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
							int? num6 = enumerator.Current;
							if (num2 != null)
							{
								int? num5 = num6;
								int? num4 = num2;
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

		// Token: 0x06000A18 RID: 2584 RVA: 0x00024110 File Offset: 0x00022310
		protected override QueryOperatorEnumerator<int?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableIntMinMaxAggregationOperator.NullableIntMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x0400071D RID: 1821
		private readonly int _sign;

		// Token: 0x02000174 RID: 372
		private class NullableIntMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<int?>
		{
			// Token: 0x06000A19 RID: 2585 RVA: 0x00024121 File Offset: 0x00022321
			internal NullableIntMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<int?, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x06000A1A RID: 2586 RVA: 0x0002413C File Offset: 0x0002233C
			protected override bool MoveNextCore(ref int? currentElement)
			{
				QueryOperatorEnumerator<int?, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						int? num2 = null;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (currentElement != null)
							{
								int? num3 = num2;
								int? num4 = currentElement;
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
						int? num5 = null;
						while (source.MoveNext(ref num5, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (currentElement != null)
							{
								int? num4 = num5;
								int? num3 = currentElement;
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

			// Token: 0x06000A1B RID: 2587 RVA: 0x00024246 File Offset: 0x00022446
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400071E RID: 1822
			private QueryOperatorEnumerator<int?, TKey> _source;

			// Token: 0x0400071F RID: 1823
			private int _sign;
		}
	}
}
