using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001CB RID: 459
	internal sealed class TakeOrSkipWhileQueryOperator<TResult> : UnaryQueryOperator<TResult, TResult>
	{
		// Token: 0x06000B96 RID: 2966 RVA: 0x00028A97 File Offset: 0x00026C97
		internal TakeOrSkipWhileQueryOperator(IEnumerable<TResult> child, Func<TResult, bool> predicate, Func<TResult, int, bool> indexedPredicate, bool take) : base(child)
		{
			this._predicate = predicate;
			this._indexedPredicate = indexedPredicate;
			this._take = take;
			this.InitOrderIndexState();
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00028ABC File Offset: 0x00026CBC
		private void InitOrderIndexState()
		{
			OrdinalIndexState state = OrdinalIndexState.Increasing;
			OrdinalIndexState ordinalIndexState = base.Child.OrdinalIndexState;
			if (this._indexedPredicate != null)
			{
				state = OrdinalIndexState.Correct;
				this._limitsParallelism = (ordinalIndexState == OrdinalIndexState.Increasing);
			}
			OrdinalIndexState ordinalIndexState2 = ordinalIndexState.Worse(OrdinalIndexState.Correct);
			if (ordinalIndexState2.IsWorseThan(state))
			{
				this._prematureMerge = true;
			}
			if (!this._take)
			{
				ordinalIndexState2 = ordinalIndexState2.Worse(OrdinalIndexState.Increasing);
			}
			base.SetOrdinalIndexState(ordinalIndexState2);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00028B1C File Offset: 0x00026D1C
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

		// Token: 0x06000B99 RID: 2969 RVA: 0x00028B68 File Offset: 0x00026D68
		private void WrapHelper<TKey>(PartitionedStream<TResult, TKey> inputStream, IPartitionedStreamRecipient<TResult> recipient, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> operatorState = new TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey>();
			CountdownEvent sharedBarrier = new CountdownEvent(partitionCount);
			Func<TResult, TKey, bool> indexedPredicate = (Func<TResult, TKey, bool>)this._indexedPredicate;
			PartitionedStream<TResult, TKey> partitionedStream = new PartitionedStream<TResult, TKey>(partitionCount, inputStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new TakeOrSkipWhileQueryOperator<TResult>.TakeOrSkipWhileQueryOperatorEnumerator<TKey>(inputStream[i], this._predicate, indexedPredicate, this._take, operatorState, sharedBarrier, settings.CancellationState.MergedCancellationToken, inputStream.KeyComparer);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00028BF8 File Offset: 0x00026DF8
		internal override QueryResults<TResult> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TResult, TResult>.UnaryQueryOperatorResults(base.Child.Open(settings, true), this, settings, preferStriping);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00028C10 File Offset: 0x00026E10
		internal override IEnumerable<TResult> AsSequentialQuery(CancellationToken token)
		{
			if (this._take)
			{
				if (this._indexedPredicate != null)
				{
					return base.Child.AsSequentialQuery(token).TakeWhile(this._indexedPredicate);
				}
				return base.Child.AsSequentialQuery(token).TakeWhile(this._predicate);
			}
			else
			{
				if (this._indexedPredicate != null)
				{
					return CancellableEnumerable.Wrap<TResult>(base.Child.AsSequentialQuery(token), token).SkipWhile(this._indexedPredicate);
				}
				return CancellableEnumerable.Wrap<TResult>(base.Child.AsSequentialQuery(token), token).SkipWhile(this._predicate);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x00028CA0 File Offset: 0x00026EA0
		internal override bool LimitsParallelism
		{
			get
			{
				return this._limitsParallelism;
			}
		}

		// Token: 0x04000815 RID: 2069
		private Func<TResult, bool> _predicate;

		// Token: 0x04000816 RID: 2070
		private Func<TResult, int, bool> _indexedPredicate;

		// Token: 0x04000817 RID: 2071
		private readonly bool _take;

		// Token: 0x04000818 RID: 2072
		private bool _prematureMerge;

		// Token: 0x04000819 RID: 2073
		private bool _limitsParallelism;

		// Token: 0x020001CC RID: 460
		private class TakeOrSkipWhileQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TResult, TKey>
		{
			// Token: 0x06000B9D RID: 2973 RVA: 0x00028CA8 File Offset: 0x00026EA8
			internal TakeOrSkipWhileQueryOperatorEnumerator(QueryOperatorEnumerator<TResult, TKey> source, Func<TResult, bool> predicate, Func<TResult, TKey, bool> indexedPredicate, bool take, TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> operatorState, CountdownEvent sharedBarrier, CancellationToken cancelToken, IComparer<TKey> keyComparer)
			{
				this._source = source;
				this._predicate = predicate;
				this._indexedPredicate = indexedPredicate;
				this._take = take;
				this._operatorState = operatorState;
				this._sharedBarrier = sharedBarrier;
				this._cancellationToken = cancelToken;
				this._keyComparer = keyComparer;
			}

			// Token: 0x06000B9E RID: 2974 RVA: 0x00028CF8 File Offset: 0x00026EF8
			internal override bool MoveNext(ref TResult currentElement, ref TKey currentKey)
			{
				if (this._buffer == null)
				{
					List<Pair<TResult, TKey>> list = new List<Pair<TResult, TKey>>();
					try
					{
						TResult tresult = default(TResult);
						TKey tkey = default(TKey);
						int num = 0;
						while (this._source.MoveNext(ref tresult, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							list.Add(new Pair<TResult, TKey>(tresult, tkey));
							if (this._updatesSeen != this._operatorState._updatesDone)
							{
								TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> operatorState = this._operatorState;
								lock (operatorState)
								{
									this._currentLowKey = this._operatorState._currentLowKey;
									this._updatesSeen = this._operatorState._updatesDone;
								}
							}
							if (this._updatesSeen > 0 && this._keyComparer.Compare(tkey, this._currentLowKey) > 0)
							{
								break;
							}
							bool flag2;
							if (this._predicate != null)
							{
								flag2 = this._predicate(tresult);
							}
							else
							{
								flag2 = this._indexedPredicate(tresult, tkey);
							}
							if (!flag2)
							{
								TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> operatorState = this._operatorState;
								lock (operatorState)
								{
									if (this._operatorState._updatesDone == 0 || this._keyComparer.Compare(this._operatorState._currentLowKey, tkey) > 0)
									{
										this._currentLowKey = (this._operatorState._currentLowKey = tkey);
										TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> operatorState2 = this._operatorState;
										int num2 = operatorState2._updatesDone + 1;
										operatorState2._updatesDone = num2;
										this._updatesSeen = num2;
									}
									break;
								}
							}
						}
					}
					finally
					{
						this._sharedBarrier.Signal();
					}
					this._sharedBarrier.Wait(this._cancellationToken);
					this._buffer = list;
					this._bufferIndex = new Shared<int>(-1);
				}
				if (this._take)
				{
					if (this._bufferIndex.Value >= this._buffer.Count - 1)
					{
						return false;
					}
					this._bufferIndex.Value++;
					currentElement = this._buffer[this._bufferIndex.Value].First;
					currentKey = this._buffer[this._bufferIndex.Value].Second;
					return this._operatorState._updatesDone == 0 || this._keyComparer.Compare(this._operatorState._currentLowKey, currentKey) > 0;
				}
				else
				{
					if (this._operatorState._updatesDone == 0)
					{
						return false;
					}
					if (this._bufferIndex.Value < this._buffer.Count - 1)
					{
						this._bufferIndex.Value++;
						while (this._bufferIndex.Value < this._buffer.Count)
						{
							if (this._keyComparer.Compare(this._buffer[this._bufferIndex.Value].Second, this._operatorState._currentLowKey) >= 0)
							{
								currentElement = this._buffer[this._bufferIndex.Value].First;
								currentKey = this._buffer[this._bufferIndex.Value].Second;
								return true;
							}
							this._bufferIndex.Value++;
						}
					}
					return this._source.MoveNext(ref currentElement, ref currentKey);
				}
			}

			// Token: 0x06000B9F RID: 2975 RVA: 0x000290D4 File Offset: 0x000272D4
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400081A RID: 2074
			private readonly QueryOperatorEnumerator<TResult, TKey> _source;

			// Token: 0x0400081B RID: 2075
			private readonly Func<TResult, bool> _predicate;

			// Token: 0x0400081C RID: 2076
			private readonly Func<TResult, TKey, bool> _indexedPredicate;

			// Token: 0x0400081D RID: 2077
			private readonly bool _take;

			// Token: 0x0400081E RID: 2078
			private readonly IComparer<TKey> _keyComparer;

			// Token: 0x0400081F RID: 2079
			private readonly TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> _operatorState;

			// Token: 0x04000820 RID: 2080
			private readonly CountdownEvent _sharedBarrier;

			// Token: 0x04000821 RID: 2081
			private readonly CancellationToken _cancellationToken;

			// Token: 0x04000822 RID: 2082
			private List<Pair<TResult, TKey>> _buffer;

			// Token: 0x04000823 RID: 2083
			private Shared<int> _bufferIndex;

			// Token: 0x04000824 RID: 2084
			private int _updatesSeen;

			// Token: 0x04000825 RID: 2085
			private TKey _currentLowKey;
		}

		// Token: 0x020001CD RID: 461
		private class OperatorState<TKey>
		{
			// Token: 0x06000BA0 RID: 2976 RVA: 0x00002162 File Offset: 0x00000362
			public OperatorState()
			{
			}

			// Token: 0x04000826 RID: 2086
			internal volatile int _updatesDone;

			// Token: 0x04000827 RID: 2087
			internal TKey _currentLowKey;
		}
	}
}
