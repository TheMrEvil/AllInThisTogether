using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200012D RID: 301
	internal sealed class IntersectQueryOperator<TInputOutput> : BinaryQueryOperator<TInputOutput, TInputOutput, TInputOutput>
	{
		// Token: 0x06000928 RID: 2344 RVA: 0x00020625 File Offset: 0x0001E825
		internal IntersectQueryOperator(ParallelQuery<TInputOutput> left, ParallelQuery<TInputOutput> right, IEqualityComparer<TInputOutput> comparer) : base(left, right)
		{
			this._comparer = comparer;
			this._outputOrdered = base.LeftChild.OutputOrdered;
			base.SetOrdinalIndex(OrdinalIndexState.Shuffled);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00020650 File Offset: 0x0001E850
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInputOutput> leftChildQueryResults = base.LeftChild.Open(settings, false);
			QueryResults<TInputOutput> rightChildQueryResults = base.RightChild.Open(settings, false);
			return new BinaryQueryOperator<TInputOutput, TInputOutput, TInputOutput>.BinaryQueryOperatorResults(leftChildQueryResults, rightChildQueryResults, this, settings, false);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00020684 File Offset: 0x0001E884
		public override void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TInputOutput, TLeftKey> leftPartitionedStream, PartitionedStream<TInputOutput, TRightKey> rightPartitionedStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, bool preferStriping, QuerySettings settings)
		{
			if (base.OutputOrdered)
			{
				this.WrapPartitionedStreamHelper<TLeftKey, TRightKey>(ExchangeUtilities.HashRepartitionOrdered<TInputOutput, NoKeyMemoizationRequired, TLeftKey>(leftPartitionedStream, null, null, this._comparer, settings.CancellationState.MergedCancellationToken), rightPartitionedStream, outputRecipient, settings.CancellationState.MergedCancellationToken);
				return;
			}
			this.WrapPartitionedStreamHelper<int, TRightKey>(ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TLeftKey>(leftPartitionedStream, null, null, this._comparer, settings.CancellationState.MergedCancellationToken), rightPartitionedStream, outputRecipient, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000206F8 File Offset: 0x0001E8F8
		private void WrapPartitionedStreamHelper<TLeftKey, TRightKey>(PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftHashStream, PartitionedStream<TInputOutput, TRightKey> rightPartitionedStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, CancellationToken cancellationToken)
		{
			int partitionCount = leftHashStream.PartitionCount;
			PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, int> partitionedStream = ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TRightKey>(rightPartitionedStream, null, null, this._comparer, cancellationToken);
			PartitionedStream<TInputOutput, TLeftKey> partitionedStream2 = new PartitionedStream<TInputOutput, TLeftKey>(partitionCount, leftHashStream.KeyComparer, OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				if (base.OutputOrdered)
				{
					partitionedStream2[i] = new IntersectQueryOperator<TInputOutput>.OrderedIntersectQueryOperatorEnumerator<TLeftKey>(leftHashStream[i], partitionedStream[i], this._comparer, leftHashStream.KeyComparer, cancellationToken);
				}
				else
				{
					partitionedStream2[i] = (QueryOperatorEnumerator<TInputOutput, TLeftKey>)new IntersectQueryOperator<TInputOutput>.IntersectQueryOperatorEnumerator<TLeftKey>(leftHashStream[i], partitionedStream[i], this._comparer, cancellationToken);
				}
			}
			outputRecipient.Receive<TLeftKey>(partitionedStream2);
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00020798 File Offset: 0x0001E998
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TInputOutput> first = CancellableEnumerable.Wrap<TInputOutput>(base.LeftChild.AsSequentialQuery(token), token);
			IEnumerable<TInputOutput> second = CancellableEnumerable.Wrap<TInputOutput>(base.RightChild.AsSequentialQuery(token), token);
			return first.Intersect(second, this._comparer);
		}

		// Token: 0x040006AA RID: 1706
		private readonly IEqualityComparer<TInputOutput> _comparer;

		// Token: 0x0200012E RID: 302
		private class IntersectQueryOperatorEnumerator<TLeftKey> : QueryOperatorEnumerator<TInputOutput, int>
		{
			// Token: 0x0600092E RID: 2350 RVA: 0x000207D6 File Offset: 0x0001E9D6
			internal IntersectQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> rightSource, IEqualityComparer<TInputOutput> comparer, CancellationToken cancellationToken)
			{
				this._leftSource = leftSource;
				this._rightSource = rightSource;
				this._comparer = comparer;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x0600092F RID: 2351 RVA: 0x000207FC File Offset: 0x0001E9FC
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
					if (this._hashLookup.Remove(pair2.First))
					{
						currentElement = pair2.First;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000930 RID: 2352 RVA: 0x000208E8 File Offset: 0x0001EAE8
			protected override void Dispose(bool disposing)
			{
				this._leftSource.Dispose();
				this._rightSource.Dispose();
			}

			// Token: 0x040006AB RID: 1707
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> _leftSource;

			// Token: 0x040006AC RID: 1708
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> _rightSource;

			// Token: 0x040006AD RID: 1709
			private IEqualityComparer<TInputOutput> _comparer;

			// Token: 0x040006AE RID: 1710
			private Set<TInputOutput> _hashLookup;

			// Token: 0x040006AF RID: 1711
			private CancellationToken _cancellationToken;

			// Token: 0x040006B0 RID: 1712
			private Shared<int> _outputLoopCount;
		}

		// Token: 0x0200012F RID: 303
		private class OrderedIntersectQueryOperatorEnumerator<TLeftKey> : QueryOperatorEnumerator<TInputOutput, TLeftKey>
		{
			// Token: 0x06000931 RID: 2353 RVA: 0x00020900 File Offset: 0x0001EB00
			internal OrderedIntersectQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> rightSource, IEqualityComparer<TInputOutput> comparer, IComparer<TLeftKey> leftKeyComparer, CancellationToken cancellationToken)
			{
				this._leftSource = leftSource;
				this._rightSource = rightSource;
				this._comparer = new WrapperEqualityComparer<TInputOutput>(comparer);
				this._leftKeyComparer = leftKeyComparer;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000932 RID: 2354 RVA: 0x00020938 File Offset: 0x0001EB38
			internal override bool MoveNext(ref TInputOutput currentElement, ref TLeftKey currentKey)
			{
				int num = 0;
				if (this._hashLookup == null)
				{
					this._hashLookup = new Dictionary<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>>(this._comparer);
					Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					TLeftKey tleftKey = default(TLeftKey);
					while (this._leftSource.MoveNext(ref pair, ref tleftKey))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						Wrapper<TInputOutput> key = new Wrapper<TInputOutput>(pair.First);
						Pair<TInputOutput, TLeftKey> pair2;
						if (!this._hashLookup.TryGetValue(key, out pair2) || this._leftKeyComparer.Compare(tleftKey, pair2.Second) < 0)
						{
							this._hashLookup[key] = new Pair<TInputOutput, TLeftKey>(pair.First, tleftKey);
						}
					}
				}
				Pair<TInputOutput, NoKeyMemoizationRequired> pair3 = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
				int num2 = 0;
				while (this._rightSource.MoveNext(ref pair3, ref num2))
				{
					if ((num++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					Wrapper<TInputOutput> key2 = new Wrapper<TInputOutput>(pair3.First);
					Pair<TInputOutput, TLeftKey> pair4;
					if (this._hashLookup.TryGetValue(key2, out pair4))
					{
						currentElement = pair4.First;
						currentKey = pair4.Second;
						this._hashLookup.Remove(new Wrapper<TInputOutput>(pair4.First));
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000933 RID: 2355 RVA: 0x00020A72 File Offset: 0x0001EC72
			protected override void Dispose(bool disposing)
			{
				this._leftSource.Dispose();
				this._rightSource.Dispose();
			}

			// Token: 0x040006B1 RID: 1713
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> _leftSource;

			// Token: 0x040006B2 RID: 1714
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> _rightSource;

			// Token: 0x040006B3 RID: 1715
			private IEqualityComparer<Wrapper<TInputOutput>> _comparer;

			// Token: 0x040006B4 RID: 1716
			private IComparer<TLeftKey> _leftKeyComparer;

			// Token: 0x040006B5 RID: 1717
			private Dictionary<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>> _hashLookup;

			// Token: 0x040006B6 RID: 1718
			private CancellationToken _cancellationToken;
		}
	}
}
