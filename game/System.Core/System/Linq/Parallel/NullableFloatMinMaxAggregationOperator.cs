using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200016D RID: 365
	internal sealed class NullableFloatMinMaxAggregationOperator : InlinedAggregationOperator<float?, float?, float?>
	{
		// Token: 0x06000A04 RID: 2564 RVA: 0x00023AF9 File Offset: 0x00021CF9
		internal NullableFloatMinMaxAggregationOperator(IEnumerable<float?> child, int sign) : base(child)
		{
			this._sign = sign;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00023B0C File Offset: 0x00021D0C
		protected override float? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			float? num;
			using (IEnumerator<float?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					float? num2 = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							float? num3 = enumerator.Current;
							if (num3 != null)
							{
								if (num2 != null)
								{
									float? num4 = num3;
									float? num5 = num2;
									if (!(num4.GetValueOrDefault() < num5.GetValueOrDefault() & (num4 != null & num5 != null)) && !float.IsNaN(num3.GetValueOrDefault()))
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
							float? num6 = enumerator.Current;
							if (num6 != null)
							{
								if (num2 != null)
								{
									float? num5 = num6;
									float? num4 = num2;
									if (!(num5.GetValueOrDefault() > num4.GetValueOrDefault() & (num5 != null & num4 != null)) && !float.IsNaN(num2.GetValueOrDefault()))
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

		// Token: 0x06000A06 RID: 2566 RVA: 0x00023C2C File Offset: 0x00021E2C
		protected override QueryOperatorEnumerator<float?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<float?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableFloatMinMaxAggregationOperator.NullableFloatMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x04000718 RID: 1816
		private readonly int _sign;

		// Token: 0x0200016E RID: 366
		private class NullableFloatMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<float?>
		{
			// Token: 0x06000A07 RID: 2567 RVA: 0x00023C3D File Offset: 0x00021E3D
			internal NullableFloatMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<float?, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x06000A08 RID: 2568 RVA: 0x00023C58 File Offset: 0x00021E58
			protected override bool MoveNextCore(ref float? currentElement)
			{
				QueryOperatorEnumerator<float?, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						float? num2 = null;
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
									float? num3 = num2;
									float? num4 = currentElement;
									if (!(num3.GetValueOrDefault() < num4.GetValueOrDefault() & (num3 != null & num4 != null)) && !float.IsNaN(num2.GetValueOrDefault()))
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
						float? num5 = null;
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
									float? num4 = num5;
									float? num3 = currentElement;
									if (!(num4.GetValueOrDefault() > num3.GetValueOrDefault() & (num4 != null & num3 != null)) && !float.IsNaN(currentElement.GetValueOrDefault()))
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

			// Token: 0x06000A09 RID: 2569 RVA: 0x00023D92 File Offset: 0x00021F92
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000719 RID: 1817
			private QueryOperatorEnumerator<float?, TKey> _source;

			// Token: 0x0400071A RID: 1818
			private int _sign;
		}
	}
}
