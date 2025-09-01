using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000131 RID: 305
	internal sealed class UnionQueryOperator<TInputOutput> : BinaryQueryOperator<TInputOutput, TInputOutput, TInputOutput>
	{
		// Token: 0x0600093A RID: 2362 RVA: 0x00020C5C File Offset: 0x0001EE5C
		internal UnionQueryOperator(ParallelQuery<TInputOutput> left, ParallelQuery<TInputOutput> right, IEqualityComparer<TInputOutput> comparer) : base(left, right)
		{
			this._comparer = comparer;
			this._outputOrdered = (base.LeftChild.OutputOrdered || base.RightChild.OutputOrdered);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00020C90 File Offset: 0x0001EE90
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInputOutput> leftChildQueryResults = base.LeftChild.Open(settings, false);
			QueryResults<TInputOutput> rightChildQueryResults = base.RightChild.Open(settings, false);
			return new BinaryQueryOperator<TInputOutput, TInputOutput, TInputOutput>.BinaryQueryOperatorResults(leftChildQueryResults, rightChildQueryResults, this, settings, false);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00020CC4 File Offset: 0x0001EEC4
		public override void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TInputOutput, TLeftKey> leftStream, PartitionedStream<TInputOutput, TRightKey> rightStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = leftStream.PartitionCount;
			if (base.LeftChild.OutputOrdered)
			{
				PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftHashStream = ExchangeUtilities.HashRepartitionOrdered<TInputOutput, NoKeyMemoizationRequired, TLeftKey>(leftStream, null, null, this._comparer, settings.CancellationState.MergedCancellationToken);
				this.WrapPartitionedStreamFixedLeftType<TLeftKey, TRightKey>(leftHashStream, rightStream, outputRecipient, partitionCount, settings.CancellationState.MergedCancellationToken);
				return;
			}
			PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, int> leftHashStream2 = ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TLeftKey>(leftStream, null, null, this._comparer, settings.CancellationState.MergedCancellationToken);
			this.WrapPartitionedStreamFixedLeftType<int, TRightKey>(leftHashStream2, rightStream, outputRecipient, partitionCount, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00020D48 File Offset: 0x0001EF48
		private void WrapPartitionedStreamFixedLeftType<TLeftKey, TRightKey>(PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftHashStream, PartitionedStream<TInputOutput, TRightKey> rightStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, int partitionCount, CancellationToken cancellationToken)
		{
			if (base.RightChild.OutputOrdered)
			{
				PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TRightKey> rightHashStream = ExchangeUtilities.HashRepartitionOrdered<TInputOutput, NoKeyMemoizationRequired, TRightKey>(rightStream, null, null, this._comparer, cancellationToken);
				this.WrapPartitionedStreamFixedBothTypes<TLeftKey, TRightKey>(leftHashStream, rightHashStream, outputRecipient, partitionCount, cancellationToken);
				return;
			}
			PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, int> rightHashStream2 = ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TRightKey>(rightStream, null, null, this._comparer, cancellationToken);
			this.WrapPartitionedStreamFixedBothTypes<TLeftKey, int>(leftHashStream, rightHashStream2, outputRecipient, partitionCount, cancellationToken);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00020DA0 File Offset: 0x0001EFA0
		private void WrapPartitionedStreamFixedBothTypes<TLeftKey, TRightKey>(PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftHashStream, PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TRightKey> rightHashStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, int partitionCount, CancellationToken cancellationToken)
		{
			if (base.LeftChild.OutputOrdered || base.RightChild.OutputOrdered)
			{
				IComparer<ConcatKey<TLeftKey, TRightKey>> keyComparer = ConcatKey<TLeftKey, TRightKey>.MakeComparer(leftHashStream.KeyComparer, rightHashStream.KeyComparer);
				PartitionedStream<TInputOutput, ConcatKey<TLeftKey, TRightKey>> partitionedStream = new PartitionedStream<TInputOutput, ConcatKey<TLeftKey, TRightKey>>(partitionCount, keyComparer, OrdinalIndexState.Shuffled);
				for (int i = 0; i < partitionCount; i++)
				{
					partitionedStream[i] = new UnionQueryOperator<TInputOutput>.OrderedUnionQueryOperatorEnumerator<TLeftKey, TRightKey>(leftHashStream[i], rightHashStream[i], base.LeftChild.OutputOrdered, base.RightChild.OutputOrdered, this._comparer, keyComparer, cancellationToken);
				}
				outputRecipient.Receive<ConcatKey<TLeftKey, TRightKey>>(partitionedStream);
				return;
			}
			PartitionedStream<TInputOutput, int> partitionedStream2 = new PartitionedStream<TInputOutput, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Shuffled);
			for (int j = 0; j < partitionCount; j++)
			{
				partitionedStream2[j] = new UnionQueryOperator<TInputOutput>.UnionQueryOperatorEnumerator<TLeftKey, TRightKey>(leftHashStream[j], rightHashStream[j], this._comparer, cancellationToken);
			}
			outputRecipient.Receive<int>(partitionedStream2);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00020E7C File Offset: 0x0001F07C
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TInputOutput> first = CancellableEnumerable.Wrap<TInputOutput>(base.LeftChild.AsSequentialQuery(token), token);
			IEnumerable<TInputOutput> second = CancellableEnumerable.Wrap<TInputOutput>(base.RightChild.AsSequentialQuery(token), token);
			return first.Union(second, this._comparer);
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040006BB RID: 1723
		private readonly IEqualityComparer<TInputOutput> _comparer;

		// Token: 0x02000132 RID: 306
		private class UnionQueryOperatorEnumerator<TLeftKey, TRightKey> : QueryOperatorEnumerator<TInputOutput, int>
		{
			// Token: 0x06000941 RID: 2369 RVA: 0x00020EBA File Offset: 0x0001F0BA
			internal UnionQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TRightKey> rightSource, IEqualityComparer<TInputOutput> comparer, CancellationToken cancellationToken)
			{
				this._leftSource = leftSource;
				this._rightSource = rightSource;
				this._comparer = comparer;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000942 RID: 2370 RVA: 0x00020EE0 File Offset: 0x0001F0E0
			internal override bool MoveNext(ref TInputOutput currentElement, ref int currentKey)
			{
				if (this._hashLookup == null)
				{
					this._hashLookup = new Set<TInputOutput>(this._comparer);
					this._outputLoopCount = new Shared<int>(0);
				}
				if (this._leftSource != null)
				{
					TLeftKey tleftKey = default(TLeftKey);
					Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					int num = 0;
					while (this._leftSource.MoveNext(ref pair, ref tleftKey))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						if (this._hashLookup.Add(pair.First))
						{
							currentElement = pair.First;
							return true;
						}
					}
					this._leftSource.Dispose();
					this._leftSource = null;
				}
				if (this._rightSource != null)
				{
					TRightKey trightKey = default(TRightKey);
					Pair<TInputOutput, NoKeyMemoizationRequired> pair2 = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					while (this._rightSource.MoveNext(ref pair2, ref trightKey))
					{
						Shared<int> outputLoopCount = this._outputLoopCount;
						int value = outputLoopCount.Value;
						outputLoopCount.Value = value + 1;
						if ((value & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						if (this._hashLookup.Add(pair2.First))
						{
							currentElement = pair2.First;
							return true;
						}
					}
					this._rightSource.Dispose();
					this._rightSource = null;
				}
				return false;
			}

			// Token: 0x06000943 RID: 2371 RVA: 0x00021016 File Offset: 0x0001F216
			protected override void Dispose(bool disposing)
			{
				if (this._leftSource != null)
				{
					this._leftSource.Dispose();
				}
				if (this._rightSource != null)
				{
					this._rightSource.Dispose();
				}
			}

			// Token: 0x040006BC RID: 1724
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> _leftSource;

			// Token: 0x040006BD RID: 1725
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TRightKey> _rightSource;

			// Token: 0x040006BE RID: 1726
			private Set<TInputOutput> _hashLookup;

			// Token: 0x040006BF RID: 1727
			private CancellationToken _cancellationToken;

			// Token: 0x040006C0 RID: 1728
			private Shared<int> _outputLoopCount;

			// Token: 0x040006C1 RID: 1729
			private readonly IEqualityComparer<TInputOutput> _comparer;
		}

		// Token: 0x02000133 RID: 307
		private class OrderedUnionQueryOperatorEnumerator<TLeftKey, TRightKey> : QueryOperatorEnumerator<TInputOutput, ConcatKey<TLeftKey, TRightKey>>
		{
			// Token: 0x06000944 RID: 2372 RVA: 0x00021040 File Offset: 0x0001F240
			internal OrderedUnionQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TRightKey> rightSource, bool leftOrdered, bool rightOrdered, IEqualityComparer<TInputOutput> comparer, IComparer<ConcatKey<TLeftKey, TRightKey>> keyComparer, CancellationToken cancellationToken)
			{
				this._leftSource = leftSource;
				this._rightSource = rightSource;
				this._keyComparer = keyComparer;
				this._leftOrdered = leftOrdered;
				this._rightOrdered = rightOrdered;
				this._comparer = comparer;
				if (this._comparer == null)
				{
					this._comparer = EqualityComparer<TInputOutput>.Default;
				}
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000945 RID: 2373 RVA: 0x0002109C File Offset: 0x0001F29C
			internal override bool MoveNext(ref TInputOutput currentElement, ref ConcatKey<TLeftKey, TRightKey> currentKey)
			{
				if (this._outputEnumerator == null)
				{
					Dictionary<Wrapper<TInputOutput>, Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>>> dictionary = new Dictionary<Wrapper<TInputOutput>, Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>>>(new WrapperEqualityComparer<TInputOutput>(this._comparer));
					Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					TLeftKey tleftKey = default(TLeftKey);
					int num = 0;
					while (this._leftSource.MoveNext(ref pair, ref tleftKey))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						ConcatKey<TLeftKey, TRightKey> concatKey = ConcatKey<TLeftKey, TRightKey>.MakeLeft(this._leftOrdered ? tleftKey : default(TLeftKey));
						Wrapper<TInputOutput> key = new Wrapper<TInputOutput>(pair.First);
						Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>> pair2;
						if (!dictionary.TryGetValue(key, out pair2) || this._keyComparer.Compare(concatKey, pair2.Second) < 0)
						{
							dictionary[key] = new Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>>(pair.First, concatKey);
						}
					}
					TRightKey trightKey = default(TRightKey);
					while (this._rightSource.MoveNext(ref pair, ref trightKey))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						ConcatKey<TLeftKey, TRightKey> concatKey2 = ConcatKey<TLeftKey, TRightKey>.MakeRight(this._rightOrdered ? trightKey : default(TRightKey));
						Wrapper<TInputOutput> key2 = new Wrapper<TInputOutput>(pair.First);
						Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>> pair3;
						if (!dictionary.TryGetValue(key2, out pair3) || this._keyComparer.Compare(concatKey2, pair3.Second) < 0)
						{
							dictionary[key2] = new Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>>(pair.First, concatKey2);
						}
					}
					this._outputEnumerator = dictionary.GetEnumerator();
				}
				if (this._outputEnumerator.MoveNext())
				{
					KeyValuePair<Wrapper<TInputOutput>, Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>>> keyValuePair = this._outputEnumerator.Current;
					Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>> value = keyValuePair.Value;
					currentElement = value.First;
					currentKey = value.Second;
					return true;
				}
				return false;
			}

			// Token: 0x06000946 RID: 2374 RVA: 0x00021252 File Offset: 0x0001F452
			protected override void Dispose(bool disposing)
			{
				this._leftSource.Dispose();
				this._rightSource.Dispose();
			}

			// Token: 0x040006C2 RID: 1730
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> _leftSource;

			// Token: 0x040006C3 RID: 1731
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TRightKey> _rightSource;

			// Token: 0x040006C4 RID: 1732
			private IComparer<ConcatKey<TLeftKey, TRightKey>> _keyComparer;

			// Token: 0x040006C5 RID: 1733
			private IEnumerator<KeyValuePair<Wrapper<TInputOutput>, Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>>>> _outputEnumerator;

			// Token: 0x040006C6 RID: 1734
			private bool _leftOrdered;

			// Token: 0x040006C7 RID: 1735
			private bool _rightOrdered;

			// Token: 0x040006C8 RID: 1736
			private IEqualityComparer<TInputOutput> _comparer;

			// Token: 0x040006C9 RID: 1737
			private CancellationToken _cancellationToken;
		}
	}
}
