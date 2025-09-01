using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200012A RID: 298
	internal sealed class GroupJoinQueryOperator<TLeftInput, TRightInput, TKey, TOutput> : BinaryQueryOperator<TLeftInput, TRightInput, TOutput>
	{
		// Token: 0x0600091E RID: 2334 RVA: 0x00020148 File Offset: 0x0001E348
		internal GroupJoinQueryOperator(ParallelQuery<TLeftInput> left, ParallelQuery<TRightInput> right, Func<TLeftInput, TKey> leftKeySelector, Func<TRightInput, TKey> rightKeySelector, Func<TLeftInput, IEnumerable<TRightInput>, TOutput> resultSelector, IEqualityComparer<TKey> keyComparer) : base(left, right)
		{
			this._leftKeySelector = leftKeySelector;
			this._rightKeySelector = rightKeySelector;
			this._resultSelector = resultSelector;
			this._keyComparer = keyComparer;
			this._outputOrdered = base.LeftChild.OutputOrdered;
			base.SetOrdinalIndex(OrdinalIndexState.Shuffled);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00020194 File Offset: 0x0001E394
		internal override QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TLeftInput> leftChildQueryResults = base.LeftChild.Open(settings, false);
			QueryResults<TRightInput> rightChildQueryResults = base.RightChild.Open(settings, false);
			return new BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults(leftChildQueryResults, rightChildQueryResults, this, settings, false);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x000201C8 File Offset: 0x0001E3C8
		public override void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TLeftInput, TLeftKey> leftStream, PartitionedStream<TRightInput, TRightKey> rightStream, IPartitionedStreamRecipient<TOutput> outputRecipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = leftStream.PartitionCount;
			if (base.LeftChild.OutputOrdered)
			{
				this.WrapPartitionedStreamHelper<TLeftKey, TRightKey>(ExchangeUtilities.HashRepartitionOrdered<TLeftInput, TKey, TLeftKey>(leftStream, this._leftKeySelector, this._keyComparer, null, settings.CancellationState.MergedCancellationToken), rightStream, outputRecipient, partitionCount, settings.CancellationState.MergedCancellationToken);
				return;
			}
			this.WrapPartitionedStreamHelper<int, TRightKey>(ExchangeUtilities.HashRepartition<TLeftInput, TKey, TLeftKey>(leftStream, this._leftKeySelector, this._keyComparer, null, settings.CancellationState.MergedCancellationToken), rightStream, outputRecipient, partitionCount, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00020254 File Offset: 0x0001E454
		private void WrapPartitionedStreamHelper<TLeftKey, TRightKey>(PartitionedStream<Pair<TLeftInput, TKey>, TLeftKey> leftHashStream, PartitionedStream<TRightInput, TRightKey> rightPartitionedStream, IPartitionedStreamRecipient<TOutput> outputRecipient, int partitionCount, CancellationToken cancellationToken)
		{
			PartitionedStream<Pair<TRightInput, TKey>, int> partitionedStream = ExchangeUtilities.HashRepartition<TRightInput, TKey, TRightKey>(rightPartitionedStream, this._rightKeySelector, this._keyComparer, null, cancellationToken);
			PartitionedStream<TOutput, TLeftKey> partitionedStream2 = new PartitionedStream<TOutput, TLeftKey>(partitionCount, leftHashStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream2[i] = new HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, TKey, TOutput>(leftHashStream[i], partitionedStream[i], null, this._resultSelector, this._keyComparer, cancellationToken);
			}
			outputRecipient.Receive<TLeftKey>(partitionedStream2);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000202C8 File Offset: 0x0001E4C8
		internal override IEnumerable<TOutput> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TLeftInput> outer = CancellableEnumerable.Wrap<TLeftInput>(base.LeftChild.AsSequentialQuery(token), token);
			IEnumerable<TRightInput> inner = CancellableEnumerable.Wrap<TRightInput>(base.RightChild.AsSequentialQuery(token), token);
			return outer.GroupJoin(inner, this._leftKeySelector, this._rightKeySelector, this._resultSelector, this._keyComparer);
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000699 RID: 1689
		private readonly Func<TLeftInput, TKey> _leftKeySelector;

		// Token: 0x0400069A RID: 1690
		private readonly Func<TRightInput, TKey> _rightKeySelector;

		// Token: 0x0400069B RID: 1691
		private readonly Func<TLeftInput, IEnumerable<TRightInput>, TOutput> _resultSelector;

		// Token: 0x0400069C RID: 1692
		private readonly IEqualityComparer<TKey> _keyComparer;
	}
}
