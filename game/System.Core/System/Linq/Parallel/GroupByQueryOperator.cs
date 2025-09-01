using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A0 RID: 416
	internal sealed class GroupByQueryOperator<TSource, TGroupKey, TElement> : UnaryQueryOperator<TSource, IGrouping<TGroupKey, TElement>>
	{
		// Token: 0x06000AF8 RID: 2808 RVA: 0x000267CD File Offset: 0x000249CD
		internal GroupByQueryOperator(IEnumerable<TSource> child, Func<TSource, TGroupKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TGroupKey> keyComparer) : base(child)
		{
			this._keySelector = keySelector;
			this._elementSelector = elementSelector;
			this._keyComparer = keyComparer;
			base.SetOrdinalIndexState(OrdinalIndexState.Shuffled);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000267F4 File Offset: 0x000249F4
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<IGrouping<TGroupKey, TElement>> recipient, bool preferStriping, QuerySettings settings)
		{
			if (base.Child.OutputOrdered)
			{
				this.WrapPartitionedStreamHelperOrdered<TKey>(ExchangeUtilities.HashRepartitionOrdered<TSource, TGroupKey, TKey>(inputStream, this._keySelector, this._keyComparer, null, settings.CancellationState.MergedCancellationToken), recipient, settings.CancellationState.MergedCancellationToken);
				return;
			}
			this.WrapPartitionedStreamHelper<TKey, int>(ExchangeUtilities.HashRepartition<TSource, TGroupKey, TKey>(inputStream, this._keySelector, this._keyComparer, null, settings.CancellationState.MergedCancellationToken), recipient, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00026874 File Offset: 0x00024A74
		private void WrapPartitionedStreamHelper<TIgnoreKey, TKey>(PartitionedStream<Pair<TSource, TGroupKey>, TKey> hashStream, IPartitionedStreamRecipient<IGrouping<TGroupKey, TElement>> recipient, CancellationToken cancellationToken)
		{
			int partitionCount = hashStream.PartitionCount;
			PartitionedStream<IGrouping<TGroupKey, TElement>, TKey> partitionedStream = new PartitionedStream<IGrouping<TGroupKey, TElement>, TKey>(partitionCount, hashStream.KeyComparer, OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				if (this._elementSelector == null)
				{
					GroupByIdentityQueryOperatorEnumerator<TSource, TGroupKey, TKey> groupByIdentityQueryOperatorEnumerator = new GroupByIdentityQueryOperatorEnumerator<TSource, TGroupKey, TKey>(hashStream[i], this._keyComparer, cancellationToken);
					partitionedStream[i] = (QueryOperatorEnumerator<IGrouping<TGroupKey, TElement>, TKey>)groupByIdentityQueryOperatorEnumerator;
				}
				else
				{
					partitionedStream[i] = new GroupByElementSelectorQueryOperatorEnumerator<TSource, TGroupKey, TElement, TKey>(hashStream[i], this._keyComparer, this._elementSelector, cancellationToken);
				}
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x000268F4 File Offset: 0x00024AF4
		private void WrapPartitionedStreamHelperOrdered<TKey>(PartitionedStream<Pair<TSource, TGroupKey>, TKey> hashStream, IPartitionedStreamRecipient<IGrouping<TGroupKey, TElement>> recipient, CancellationToken cancellationToken)
		{
			int partitionCount = hashStream.PartitionCount;
			PartitionedStream<IGrouping<TGroupKey, TElement>, TKey> partitionedStream = new PartitionedStream<IGrouping<TGroupKey, TElement>, TKey>(partitionCount, hashStream.KeyComparer, OrdinalIndexState.Shuffled);
			IComparer<TKey> keyComparer = hashStream.KeyComparer;
			for (int i = 0; i < partitionCount; i++)
			{
				if (this._elementSelector == null)
				{
					OrderedGroupByIdentityQueryOperatorEnumerator<TSource, TGroupKey, TKey> orderedGroupByIdentityQueryOperatorEnumerator = new OrderedGroupByIdentityQueryOperatorEnumerator<TSource, TGroupKey, TKey>(hashStream[i], this._keySelector, this._keyComparer, keyComparer, cancellationToken);
					partitionedStream[i] = (QueryOperatorEnumerator<IGrouping<TGroupKey, TElement>, TKey>)orderedGroupByIdentityQueryOperatorEnumerator;
				}
				else
				{
					partitionedStream[i] = new OrderedGroupByElementSelectorQueryOperatorEnumerator<TSource, TGroupKey, TElement, TKey>(hashStream[i], this._keySelector, this._elementSelector, this._keyComparer, keyComparer, cancellationToken);
				}
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0002698B File Offset: 0x00024B8B
		internal override QueryResults<IGrouping<TGroupKey, TElement>> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TSource, IGrouping<TGroupKey, TElement>>.UnaryQueryOperatorResults(base.Child.Open(settings, false), this, settings, false);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000269A4 File Offset: 0x00024BA4
		internal override IEnumerable<IGrouping<TGroupKey, TElement>> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TSource> source = CancellableEnumerable.Wrap<TSource>(base.Child.AsSequentialQuery(token), token);
			if (this._elementSelector == null)
			{
				return (IEnumerable<IGrouping<TGroupKey, TElement>>)source.GroupBy(this._keySelector, this._keyComparer);
			}
			return source.GroupBy(this._keySelector, this._elementSelector, this._keyComparer);
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000796 RID: 1942
		private readonly Func<TSource, TGroupKey> _keySelector;

		// Token: 0x04000797 RID: 1943
		private readonly Func<TSource, TElement> _elementSelector;

		// Token: 0x04000798 RID: 1944
		private readonly IEqualityComparer<TGroupKey> _keyComparer;
	}
}
