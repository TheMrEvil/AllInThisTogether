using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200014B RID: 331
	internal sealed class FloatMinMaxAggregationOperator : InlinedAggregationOperator<float, float, float>
	{
		// Token: 0x06000999 RID: 2457 RVA: 0x00022394 File Offset: 0x00020594
		internal FloatMinMaxAggregationOperator(IEnumerable<float> child, int sign) : base(child)
		{
			this._sign = sign;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x000223A4 File Offset: 0x000205A4
		protected override float InternalAggregate(ref Exception singularExceptionToThrow)
		{
			float result;
			using (IEnumerator<float> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException("Sequence contains no elements");
					result = 0f;
				}
				else
				{
					float num = enumerator.Current;
					if (this._sign == -1)
					{
						while (enumerator.MoveNext())
						{
							float num2 = enumerator.Current;
							if (num2 < num || float.IsNaN(num2))
							{
								num = num2;
							}
						}
					}
					else
					{
						while (enumerator.MoveNext())
						{
							float num3 = enumerator.Current;
							if (num3 > num || float.IsNaN(num))
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

		// Token: 0x0600099B RID: 2459 RVA: 0x0002244C File Offset: 0x0002064C
		protected override QueryOperatorEnumerator<float, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<float, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new FloatMinMaxAggregationOperator.FloatMinMaxAggregationOperatorEnumerator<TKey>(source, index, this._sign, cancellationToken);
		}

		// Token: 0x040006FB RID: 1787
		private readonly int _sign;

		// Token: 0x0200014C RID: 332
		private class FloatMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<float>
		{
			// Token: 0x0600099C RID: 2460 RVA: 0x0002245D File Offset: 0x0002065D
			internal FloatMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<float, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this._source = source;
				this._sign = sign;
			}

			// Token: 0x0600099D RID: 2461 RVA: 0x00022478 File Offset: 0x00020678
			protected override bool MoveNextCore(ref float currentElement)
			{
				QueryOperatorEnumerator<float, TKey> source = this._source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this._sign == -1)
					{
						float num2 = 0f;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num2 < currentElement || float.IsNaN(num2))
							{
								currentElement = num2;
							}
						}
					}
					else
					{
						float num3 = 0f;
						while (source.MoveNext(ref num3, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							if (num3 > currentElement || float.IsNaN(currentElement))
							{
								currentElement = num3;
							}
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x0600099E RID: 2462 RVA: 0x00022528 File Offset: 0x00020728
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040006FC RID: 1788
			private QueryOperatorEnumerator<float, TKey> _source;

			// Token: 0x040006FD RID: 1789
			private int _sign;
		}
	}
}
