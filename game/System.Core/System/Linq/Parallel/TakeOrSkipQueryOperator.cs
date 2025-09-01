using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001C8 RID: 456
	internal sealed class TakeOrSkipQueryOperator<TResult> : UnaryQueryOperator<TResult, TResult>
	{
		// Token: 0x06000B87 RID: 2951 RVA: 0x00028500 File Offset: 0x00026700
		internal TakeOrSkipQueryOperator(IEnumerable<TResult> child, int count, bool take) : base(child)
		{
			this._count = count;
			this._take = take;
			base.SetOrdinalIndexState(this.OutputOrdinalIndexState());
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00028524 File Offset: 0x00026724
		private OrdinalIndexState OutputOrdinalIndexState()
		{
			OrdinalIndexState ordinalIndexState = base.Child.OrdinalIndexState;
			if (ordinalIndexState == OrdinalIndexState.Indexable)
			{
				return OrdinalIndexState.Indexable;
			}
			if (ordinalIndexState.IsWorseThan(OrdinalIndexState.Increasing))
			{
				this._prematureMerge = true;
				ordinalIndexState = OrdinalIndexState.Correct;
			}
			if (!this._take && ordinalIndexState == OrdinalIndexState.Correct)
			{
				ordinalIndexState = OrdinalIndexState.Increasing;
			}
			return ordinalIndexState;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00028564 File Offset: 0x00026764
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TResult, TKey> inputStream, IPartitionedStreamRecipient<TResult> recipient, bool preferStriping, QuerySettings settings)
		{
			if (this._prematureMerge)
			{
				PartitionedStream<TResult, int> partitionedStream = QueryOperator<TResult>.ExecuteAndCollectResults<TKey>(inputStream, inputStream.PartitionCount, base.Child.OutputOrdered, preferStriping, settings).GetPartitionedStream();
				this.WrapHelper<int>(partitionedStream, recipient, settings);
				return;
			}
			this.WrapHelper<TKey>(inputStream, recipient, settings);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x000285B0 File Offset: 0x000267B0
		private void WrapHelper<TKey>(PartitionedStream<TResult, TKey> inputStream, IPartitionedStreamRecipient<TResult> recipient, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			FixedMaxHeap<TKey> sharedIndices = new FixedMaxHeap<TKey>(this._count, inputStream.KeyComparer);
			CountdownEvent sharedBarrier = new CountdownEvent(partitionCount);
			PartitionedStream<TResult, TKey> partitionedStream = new PartitionedStream<TResult, TKey>(partitionCount, inputStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new TakeOrSkipQueryOperator<TResult>.TakeOrSkipQueryOperatorEnumerator<TKey>(inputStream[i], this._take, sharedIndices, sharedBarrier, settings.CancellationState.MergedCancellationToken, inputStream.KeyComparer);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00028636 File Offset: 0x00026836
		internal override QueryResults<TResult> Open(QuerySettings settings, bool preferStriping)
		{
			return TakeOrSkipQueryOperator<TResult>.TakeOrSkipQueryOperatorResults.NewResults(base.Child.Open(settings, true), this, settings, preferStriping);
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002864D File Offset: 0x0002684D
		internal override IEnumerable<TResult> AsSequentialQuery(CancellationToken token)
		{
			if (this._take)
			{
				return base.Child.AsSequentialQuery(token).Take(this._count);
			}
			return CancellableEnumerable.Wrap<TResult>(base.Child.AsSequentialQuery(token), token).Skip(this._count);
		}

		// Token: 0x04000807 RID: 2055
		private readonly int _count;

		// Token: 0x04000808 RID: 2056
		private readonly bool _take;

		// Token: 0x04000809 RID: 2057
		private bool _prematureMerge;

		// Token: 0x020001C9 RID: 457
		private class TakeOrSkipQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TResult, TKey>
		{
			// Token: 0x06000B8E RID: 2958 RVA: 0x0002868C File Offset: 0x0002688C
			internal TakeOrSkipQueryOperatorEnumerator(QueryOperatorEnumerator<TResult, TKey> source, bool take, FixedMaxHeap<TKey> sharedIndices, CountdownEvent sharedBarrier, CancellationToken cancellationToken, IComparer<TKey> keyComparer)
			{
				this._source = source;
				this._count = sharedIndices.Size;
				this._take = take;
				this._sharedIndices = sharedIndices;
				this._sharedBarrier = sharedBarrier;
				this._cancellationToken = cancellationToken;
				this._keyComparer = keyComparer;
			}

			// Token: 0x06000B8F RID: 2959 RVA: 0x000286D8 File Offset: 0x000268D8
			internal override bool MoveNext(ref TResult currentElement, ref TKey currentKey)
			{
				if (this._buffer == null && this._count > 0)
				{
					List<Pair<TResult, TKey>> list = new List<Pair<TResult, TKey>>();
					TResult first = default(TResult);
					TKey tkey = default(TKey);
					int num = 0;
					while (list.Count < this._count && this._source.MoveNext(ref first, ref tkey))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						list.Add(new Pair<TResult, TKey>(first, tkey));
						FixedMaxHeap<TKey> sharedIndices = this._sharedIndices;
						lock (sharedIndices)
						{
							if (!this._sharedIndices.Insert(tkey))
							{
								break;
							}
						}
					}
					this._sharedBarrier.Signal();
					this._sharedBarrier.Wait(this._cancellationToken);
					this._buffer = list;
					this._bufferIndex = new Shared<int>(-1);
				}
				if (!this._take)
				{
					TKey y = default(TKey);
					if (this._count > 0)
					{
						if (this._sharedIndices.Count < this._count)
						{
							return false;
						}
						y = this._sharedIndices.MaxValue;
						if (this._bufferIndex.Value < this._buffer.Count - 1)
						{
							this._bufferIndex.Value++;
							while (this._bufferIndex.Value < this._buffer.Count)
							{
								if (this._keyComparer.Compare(this._buffer[this._bufferIndex.Value].Second, y) > 0)
								{
									currentElement = this._buffer[this._bufferIndex.Value].First;
									currentKey = this._buffer[this._bufferIndex.Value].Second;
									return true;
								}
								this._bufferIndex.Value++;
							}
						}
					}
					return this._source.MoveNext(ref currentElement, ref currentKey);
				}
				if (this._count == 0 || this._bufferIndex.Value >= this._buffer.Count - 1)
				{
					return false;
				}
				this._bufferIndex.Value++;
				currentElement = this._buffer[this._bufferIndex.Value].First;
				currentKey = this._buffer[this._bufferIndex.Value].Second;
				return this._sharedIndices.Count == 0 || this._keyComparer.Compare(this._buffer[this._bufferIndex.Value].Second, this._sharedIndices.MaxValue) <= 0;
			}

			// Token: 0x06000B90 RID: 2960 RVA: 0x000289C8 File Offset: 0x00026BC8
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400080A RID: 2058
			private readonly QueryOperatorEnumerator<TResult, TKey> _source;

			// Token: 0x0400080B RID: 2059
			private readonly int _count;

			// Token: 0x0400080C RID: 2060
			private readonly bool _take;

			// Token: 0x0400080D RID: 2061
			private readonly IComparer<TKey> _keyComparer;

			// Token: 0x0400080E RID: 2062
			private readonly FixedMaxHeap<TKey> _sharedIndices;

			// Token: 0x0400080F RID: 2063
			private readonly CountdownEvent _sharedBarrier;

			// Token: 0x04000810 RID: 2064
			private readonly CancellationToken _cancellationToken;

			// Token: 0x04000811 RID: 2065
			private List<Pair<TResult, TKey>> _buffer;

			// Token: 0x04000812 RID: 2066
			private Shared<int> _bufferIndex;
		}

		// Token: 0x020001CA RID: 458
		private class TakeOrSkipQueryOperatorResults : UnaryQueryOperator<TResult, TResult>.UnaryQueryOperatorResults
		{
			// Token: 0x06000B91 RID: 2961 RVA: 0x000289D5 File Offset: 0x00026BD5
			public static QueryResults<TResult> NewResults(QueryResults<TResult> childQueryResults, TakeOrSkipQueryOperator<TResult> op, QuerySettings settings, bool preferStriping)
			{
				if (childQueryResults.IsIndexible)
				{
					return new TakeOrSkipQueryOperator<TResult>.TakeOrSkipQueryOperatorResults(childQueryResults, op, settings, preferStriping);
				}
				return new UnaryQueryOperator<TResult, TResult>.UnaryQueryOperatorResults(childQueryResults, op, settings, preferStriping);
			}

			// Token: 0x06000B92 RID: 2962 RVA: 0x000289F2 File Offset: 0x00026BF2
			private TakeOrSkipQueryOperatorResults(QueryResults<TResult> childQueryResults, TakeOrSkipQueryOperator<TResult> takeOrSkipOp, QuerySettings settings, bool preferStriping) : base(childQueryResults, takeOrSkipOp, settings, preferStriping)
			{
				this._takeOrSkipOp = takeOrSkipOp;
				this._childCount = this._childQueryResults.ElementsCount;
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00028A17 File Offset: 0x00026C17
			internal override bool IsIndexible
			{
				get
				{
					return this._childCount >= 0;
				}
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00028A25 File Offset: 0x00026C25
			internal override int ElementsCount
			{
				get
				{
					if (this._takeOrSkipOp._take)
					{
						return Math.Min(this._childCount, this._takeOrSkipOp._count);
					}
					return Math.Max(this._childCount - this._takeOrSkipOp._count, 0);
				}
			}

			// Token: 0x06000B95 RID: 2965 RVA: 0x00028A63 File Offset: 0x00026C63
			internal override TResult GetElement(int index)
			{
				if (this._takeOrSkipOp._take)
				{
					return this._childQueryResults.GetElement(index);
				}
				return this._childQueryResults.GetElement(this._takeOrSkipOp._count + index);
			}

			// Token: 0x04000813 RID: 2067
			private TakeOrSkipQueryOperator<TResult> _takeOrSkipOp;

			// Token: 0x04000814 RID: 2068
			private int _childCount;
		}
	}
}
