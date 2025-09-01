using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000120 RID: 288
	internal sealed class AssociativeAggregationOperator<TInput, TIntermediate, TOutput> : UnaryQueryOperator<TInput, TIntermediate>
	{
		// Token: 0x060008F4 RID: 2292 RVA: 0x0001F600 File Offset: 0x0001D800
		internal AssociativeAggregationOperator(IEnumerable<TInput> child, TIntermediate seed, Func<TIntermediate> seedFactory, bool seedIsSpecified, Func<TIntermediate, TInput, TIntermediate> intermediateReduce, Func<TIntermediate, TIntermediate, TIntermediate> finalReduce, Func<TIntermediate, TOutput> resultSelector, bool throwIfEmpty, QueryAggregationOptions options) : base(child)
		{
			this._seed = seed;
			this._seedFactory = seedFactory;
			this._seedIsSpecified = seedIsSpecified;
			this._intermediateReduce = intermediateReduce;
			this._finalReduce = finalReduce;
			this._resultSelector = resultSelector;
			this._throwIfEmpty = throwIfEmpty;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001F640 File Offset: 0x0001D840
		internal TOutput Aggregate()
		{
			TIntermediate tintermediate = default(TIntermediate);
			bool flag = false;
			using (IEnumerator<TIntermediate> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				while (enumerator.MoveNext())
				{
					if (flag)
					{
						try
						{
							tintermediate = this._finalReduce(tintermediate, enumerator.Current);
							continue;
						}
						catch (Exception ex)
						{
							throw new AggregateException(new Exception[]
							{
								ex
							});
						}
					}
					tintermediate = enumerator.Current;
					flag = true;
				}
				if (!flag)
				{
					if (this._throwIfEmpty)
					{
						throw new InvalidOperationException("Sequence contains no elements");
					}
					tintermediate = ((this._seedFactory == null) ? this._seed : this._seedFactory());
				}
			}
			TOutput result;
			try
			{
				result = this._resultSelector(tintermediate);
			}
			catch (Exception ex2)
			{
				throw new AggregateException(new Exception[]
				{
					ex2
				});
			}
			return result;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001F730 File Offset: 0x0001D930
		internal override QueryResults<TIntermediate> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TInput, TIntermediate>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001F748 File Offset: 0x0001D948
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<TIntermediate> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TIntermediate, int> partitionedStream = new PartitionedStream<TIntermediate, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new AssociativeAggregationOperator<TInput, TIntermediate, TOutput>.AssociativeAggregationOperatorEnumerator<TKey>(inputStream[i], this, i, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000080E3 File Offset: 0x000062E3
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<TIntermediate> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000674 RID: 1652
		private readonly TIntermediate _seed;

		// Token: 0x04000675 RID: 1653
		private readonly bool _seedIsSpecified;

		// Token: 0x04000676 RID: 1654
		private readonly bool _throwIfEmpty;

		// Token: 0x04000677 RID: 1655
		private Func<TIntermediate, TInput, TIntermediate> _intermediateReduce;

		// Token: 0x04000678 RID: 1656
		private Func<TIntermediate, TIntermediate, TIntermediate> _finalReduce;

		// Token: 0x04000679 RID: 1657
		private Func<TIntermediate, TOutput> _resultSelector;

		// Token: 0x0400067A RID: 1658
		private Func<TIntermediate> _seedFactory;

		// Token: 0x02000121 RID: 289
		private class AssociativeAggregationOperatorEnumerator<TKey> : QueryOperatorEnumerator<TIntermediate, int>
		{
			// Token: 0x060008FA RID: 2298 RVA: 0x0001F79D File Offset: 0x0001D99D
			internal AssociativeAggregationOperatorEnumerator(QueryOperatorEnumerator<TInput, TKey> source, AssociativeAggregationOperator<TInput, TIntermediate, TOutput> reduceOperator, int partitionIndex, CancellationToken cancellationToken)
			{
				this._source = source;
				this._reduceOperator = reduceOperator;
				this._partitionIndex = partitionIndex;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x060008FB RID: 2299 RVA: 0x0001F7C4 File Offset: 0x0001D9C4
			internal override bool MoveNext(ref TIntermediate currentElement, ref int currentKey)
			{
				if (this._accumulated)
				{
					return false;
				}
				this._accumulated = true;
				bool flag = false;
				TIntermediate tintermediate = default(TIntermediate);
				if (this._reduceOperator._seedIsSpecified)
				{
					tintermediate = ((this._reduceOperator._seedFactory == null) ? this._reduceOperator._seed : this._reduceOperator._seedFactory());
				}
				else
				{
					TInput tinput = default(TInput);
					TKey tkey = default(TKey);
					if (!this._source.MoveNext(ref tinput, ref tkey))
					{
						return false;
					}
					flag = true;
					tintermediate = (TIntermediate)((object)tinput);
				}
				TInput arg = default(TInput);
				TKey tkey2 = default(TKey);
				int num = 0;
				while (this._source.MoveNext(ref arg, ref tkey2))
				{
					if ((num++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					flag = true;
					tintermediate = this._reduceOperator._intermediateReduce(tintermediate, arg);
				}
				if (flag)
				{
					currentElement = tintermediate;
					currentKey = this._partitionIndex;
					return true;
				}
				return false;
			}

			// Token: 0x060008FC RID: 2300 RVA: 0x0001F8BF File Offset: 0x0001DABF
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400067B RID: 1659
			private readonly QueryOperatorEnumerator<TInput, TKey> _source;

			// Token: 0x0400067C RID: 1660
			private readonly AssociativeAggregationOperator<TInput, TIntermediate, TOutput> _reduceOperator;

			// Token: 0x0400067D RID: 1661
			private readonly int _partitionIndex;

			// Token: 0x0400067E RID: 1662
			private readonly CancellationToken _cancellationToken;

			// Token: 0x0400067F RID: 1663
			private bool _accumulated;
		}
	}
}
