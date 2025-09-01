using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000127 RID: 295
	internal sealed class ExceptQueryOperator<TInputOutput> : BinaryQueryOperator<TInputOutput, TInputOutput, TInputOutput>
	{
		// Token: 0x06000912 RID: 2322 RVA: 0x0001FCC6 File Offset: 0x0001DEC6
		internal ExceptQueryOperator(ParallelQuery<TInputOutput> left, ParallelQuery<TInputOutput> right, IEqualityComparer<TInputOutput> comparer) : base(left, right)
		{
			this._comparer = comparer;
			this._outputOrdered = base.LeftChild.OutputOrdered;
			base.SetOrdinalIndex(OrdinalIndexState.Shuffled);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001FCF0 File Offset: 0x0001DEF0
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInputOutput> leftChildQueryResults = base.LeftChild.Open(settings, false);
			QueryResults<TInputOutput> rightChildQueryResults = base.RightChild.Open(settings, false);
			return new BinaryQueryOperator<TInputOutput, TInputOutput, TInputOutput>.BinaryQueryOperatorResults(leftChildQueryResults, rightChildQueryResults, this, settings, false);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001FD24 File Offset: 0x0001DF24
		public override void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TInputOutput, TLeftKey> leftStream, PartitionedStream<TInputOutput, TRightKey> rightStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, bool preferStriping, QuerySettings settings)
		{
			if (base.OutputOrdered)
			{
				this.WrapPartitionedStreamHelper<TLeftKey, TRightKey>(ExchangeUtilities.HashRepartitionOrdered<TInputOutput, NoKeyMemoizationRequired, TLeftKey>(leftStream, null, null, this._comparer, settings.CancellationState.MergedCancellationToken), rightStream, outputRecipient, settings.CancellationState.MergedCancellationToken);
				return;
			}
			this.WrapPartitionedStreamHelper<int, TRightKey>(ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TLeftKey>(leftStream, null, null, this._comparer, settings.CancellationState.MergedCancellationToken), rightStream, outputRecipient, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001FD98 File Offset: 0x0001DF98
		private void WrapPartitionedStreamHelper<TLeftKey, TRightKey>(PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftHashStream, PartitionedStream<TInputOutput, TRightKey> rightPartitionedStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, CancellationToken cancellationToken)
		{
			int partitionCount = leftHashStream.PartitionCount;
			PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, int> partitionedStream = ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TRightKey>(rightPartitionedStream, null, null, this._comparer, cancellationToken);
			PartitionedStream<TInputOutput, TLeftKey> partitionedStream2 = new PartitionedStream<TInputOutput, TLeftKey>(partitionCount, leftHashStream.KeyComparer, OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				if (base.OutputOrdered)
				{
					partitionedStream2[i] = new ExceptQueryOperator<TInputOutput>.OrderedExceptQueryOperatorEnumerator<TLeftKey>(leftHashStream[i], partitionedStream[i], this._comparer, leftHashStream.KeyComparer, cancellationToken);
				}
				else
				{
					partitionedStream2[i] = (QueryOperatorEnumerator<TInputOutput, TLeftKey>)new ExceptQueryOperator<TInputOutput>.ExceptQueryOperatorEnumerator<TLeftKey>(leftHashStream[i], partitionedStream[i], this._comparer, cancellationToken);
				}
			}
			outputRecipient.Receive<TLeftKey>(partitionedStream2);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0001FE38 File Offset: 0x0001E038
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TInputOutput> first = CancellableEnumerable.Wrap<TInputOutput>(base.LeftChild.AsSequentialQuery(token), token);
			IEnumerable<TInputOutput> second = CancellableEnumerable.Wrap<TInputOutput>(base.RightChild.AsSequentialQuery(token), token);
			return first.Except(second, this._comparer);
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400068C RID: 1676
		private readonly IEqualityComparer<TInputOutput> _comparer;

		// Token: 0x02000128 RID: 296
		private class ExceptQueryOperatorEnumerator<TLeftKey> : QueryOperatorEnumerator<TInputOutput, int>
		{
			// Token: 0x06000918 RID: 2328 RVA: 0x0001FE76 File Offset: 0x0001E076
			internal ExceptQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> rightSource, IEqualityComparer<TInputOutput> comparer, CancellationToken cancellationToken)
			{
				this._leftSource = leftSource;
				this._rightSource = rightSource;
				this._comparer = comparer;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000919 RID: 2329 RVA: 0x0001FE9C File Offset: 0x0001E09C
			internal override bool MoveNext(ref TInputOutput currentElement, ref int currentKey)
			{
				if (this._hashLookup == null)
				{
					this._outputLoopCount = new Shared<int>(0);
					this._hashLookup = new Set<TInputOutput>(this._comparer);
					Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					int num = 0;
					int num2 = 0;
					while (this._rightSource.MoveNext(ref pair, ref num))
					{
						if ((num2++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						this._hashLookup.Add(pair.First);
					}
				}
				Pair<TInputOutput, NoKeyMemoizationRequired> pair2 = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
				TLeftKey tleftKey = default(TLeftKey);
				while (this._leftSource.MoveNext(ref pair2, ref tleftKey))
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
				return false;
			}

			// Token: 0x0600091A RID: 2330 RVA: 0x0001FF88 File Offset: 0x0001E188
			protected override void Dispose(bool disposing)
			{
				this._leftSource.Dispose();
				this._rightSource.Dispose();
			}

			// Token: 0x0400068D RID: 1677
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> _leftSource;

			// Token: 0x0400068E RID: 1678
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> _rightSource;

			// Token: 0x0400068F RID: 1679
			private IEqualityComparer<TInputOutput> _comparer;

			// Token: 0x04000690 RID: 1680
			private Set<TInputOutput> _hashLookup;

			// Token: 0x04000691 RID: 1681
			private CancellationToken _cancellationToken;

			// Token: 0x04000692 RID: 1682
			private Shared<int> _outputLoopCount;
		}

		// Token: 0x02000129 RID: 297
		private class OrderedExceptQueryOperatorEnumerator<TLeftKey> : QueryOperatorEnumerator<TInputOutput, TLeftKey>
		{
			// Token: 0x0600091B RID: 2331 RVA: 0x0001FFA0 File Offset: 0x0001E1A0
			internal OrderedExceptQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> rightSource, IEqualityComparer<TInputOutput> comparer, IComparer<TLeftKey> leftKeyComparer, CancellationToken cancellationToken)
			{
				this._leftSource = leftSource;
				this._rightSource = rightSource;
				this._comparer = comparer;
				this._leftKeyComparer = leftKeyComparer;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x0600091C RID: 2332 RVA: 0x0001FFD0 File Offset: 0x0001E1D0
			internal override bool MoveNext(ref TInputOutput currentElement, ref TLeftKey currentKey)
			{
				if (this._outputEnumerator == null)
				{
					Set<TInputOutput> set = new Set<TInputOutput>(this._comparer);
					Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					int num = 0;
					int num2 = 0;
					while (this._rightSource.MoveNext(ref pair, ref num))
					{
						if ((num2++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						set.Add(pair.First);
					}
					Dictionary<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>> dictionary = new Dictionary<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>>(new WrapperEqualityComparer<TInputOutput>(this._comparer));
					Pair<TInputOutput, NoKeyMemoizationRequired> pair2 = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					TLeftKey tleftKey = default(TLeftKey);
					while (this._leftSource.MoveNext(ref pair2, ref tleftKey))
					{
						if ((num2++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						if (!set.Contains(pair2.First))
						{
							Wrapper<TInputOutput> key = new Wrapper<TInputOutput>(pair2.First);
							Pair<TInputOutput, TLeftKey> pair3;
							if (!dictionary.TryGetValue(key, out pair3) || this._leftKeyComparer.Compare(tleftKey, pair3.Second) < 0)
							{
								dictionary[key] = new Pair<TInputOutput, TLeftKey>(pair2.First, tleftKey);
							}
						}
					}
					this._outputEnumerator = dictionary.GetEnumerator();
				}
				if (this._outputEnumerator.MoveNext())
				{
					KeyValuePair<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>> keyValuePair = this._outputEnumerator.Current;
					Pair<TInputOutput, TLeftKey> value = keyValuePair.Value;
					currentElement = value.First;
					currentKey = value.Second;
					return true;
				}
				return false;
			}

			// Token: 0x0600091D RID: 2333 RVA: 0x0002012F File Offset: 0x0001E32F
			protected override void Dispose(bool disposing)
			{
				this._leftSource.Dispose();
				this._rightSource.Dispose();
			}

			// Token: 0x04000693 RID: 1683
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> _leftSource;

			// Token: 0x04000694 RID: 1684
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> _rightSource;

			// Token: 0x04000695 RID: 1685
			private IEqualityComparer<TInputOutput> _comparer;

			// Token: 0x04000696 RID: 1686
			private IComparer<TLeftKey> _leftKeyComparer;

			// Token: 0x04000697 RID: 1687
			private IEnumerator<KeyValuePair<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>>> _outputEnumerator;

			// Token: 0x04000698 RID: 1688
			private CancellationToken _cancellationToken;
		}
	}
}
