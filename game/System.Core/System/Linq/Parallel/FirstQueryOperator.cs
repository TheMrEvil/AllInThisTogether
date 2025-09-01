using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200019B RID: 411
	internal sealed class FirstQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000AE5 RID: 2789 RVA: 0x000263D8 File Offset: 0x000245D8
		internal FirstQueryOperator(IEnumerable<TSource> child, Func<TSource, bool> predicate) : base(child)
		{
			this._predicate = predicate;
			this._prematureMergeNeeded = base.Child.OrdinalIndexState.IsWorseThan(OrdinalIndexState.Increasing);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00026180 File Offset: 0x00024380
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(base.Child.Open(settings, false), this, settings, preferStriping);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00026400 File Offset: 0x00024600
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			if (this._prematureMergeNeeded)
			{
				ListQueryResults<TSource> listQueryResults = QueryOperator<TSource>.ExecuteAndCollectResults<TKey>(inputStream, inputStream.PartitionCount, base.Child.OutputOrdered, preferStriping, settings);
				this.WrapHelper<int>(listQueryResults.GetPartitionedStream(), recipient, settings);
				return;
			}
			this.WrapHelper<TKey>(inputStream, recipient, settings);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002644C File Offset: 0x0002464C
		private void WrapHelper<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			FirstQueryOperator<TSource>.FirstQueryOperatorState<TKey> operatorState = new FirstQueryOperator<TSource>.FirstQueryOperatorState<TKey>();
			CountdownEvent sharedBarrier = new CountdownEvent(partitionCount);
			PartitionedStream<TSource, int> partitionedStream = new PartitionedStream<TSource, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new FirstQueryOperator<TSource>.FirstQueryOperatorEnumerator<TKey>(inputStream[i], this._predicate, operatorState, sharedBarrier, settings.CancellationState.MergedCancellationToken, inputStream.KeyComparer, i);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000080E3 File Offset: 0x000062E3
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000786 RID: 1926
		private readonly Func<TSource, bool> _predicate;

		// Token: 0x04000787 RID: 1927
		private readonly bool _prematureMergeNeeded;

		// Token: 0x0200019C RID: 412
		private class FirstQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TSource, int>
		{
			// Token: 0x06000AEB RID: 2795 RVA: 0x000264C2 File Offset: 0x000246C2
			internal FirstQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, Func<TSource, bool> predicate, FirstQueryOperator<TSource>.FirstQueryOperatorState<TKey> operatorState, CountdownEvent sharedBarrier, CancellationToken cancellationToken, IComparer<TKey> keyComparer, int partitionId)
			{
				this._source = source;
				this._predicate = predicate;
				this._operatorState = operatorState;
				this._sharedBarrier = sharedBarrier;
				this._cancellationToken = cancellationToken;
				this._keyComparer = keyComparer;
				this._partitionId = partitionId;
			}

			// Token: 0x06000AEC RID: 2796 RVA: 0x00026500 File Offset: 0x00024700
			internal override bool MoveNext(ref TSource currentElement, ref int currentKey)
			{
				if (this._alreadySearched)
				{
					return false;
				}
				TSource tsource = default(TSource);
				TKey tkey = default(TKey);
				try
				{
					TSource tsource2 = default(TSource);
					TKey tkey2 = default(TKey);
					int num = 0;
					while (this._source.MoveNext(ref tsource2, ref tkey2))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						if (this._predicate == null || this._predicate(tsource2))
						{
							tsource = tsource2;
							tkey = tkey2;
							FirstQueryOperator<TSource>.FirstQueryOperatorState<TKey> operatorState = this._operatorState;
							lock (operatorState)
							{
								if (this._operatorState._partitionId == -1 || this._keyComparer.Compare(tkey, this._operatorState._key) < 0)
								{
									this._operatorState._key = tkey;
									this._operatorState._partitionId = this._partitionId;
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
				this._alreadySearched = true;
				if (this._partitionId == this._operatorState._partitionId)
				{
					this._sharedBarrier.Wait(this._cancellationToken);
					if (this._partitionId == this._operatorState._partitionId)
					{
						currentElement = tsource;
						currentKey = 0;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000AED RID: 2797 RVA: 0x00026664 File Offset: 0x00024864
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000788 RID: 1928
			private QueryOperatorEnumerator<TSource, TKey> _source;

			// Token: 0x04000789 RID: 1929
			private Func<TSource, bool> _predicate;

			// Token: 0x0400078A RID: 1930
			private bool _alreadySearched;

			// Token: 0x0400078B RID: 1931
			private int _partitionId;

			// Token: 0x0400078C RID: 1932
			private FirstQueryOperator<TSource>.FirstQueryOperatorState<TKey> _operatorState;

			// Token: 0x0400078D RID: 1933
			private CountdownEvent _sharedBarrier;

			// Token: 0x0400078E RID: 1934
			private CancellationToken _cancellationToken;

			// Token: 0x0400078F RID: 1935
			private IComparer<TKey> _keyComparer;
		}

		// Token: 0x0200019D RID: 413
		private class FirstQueryOperatorState<TKey>
		{
			// Token: 0x06000AEE RID: 2798 RVA: 0x00026671 File Offset: 0x00024871
			public FirstQueryOperatorState()
			{
			}

			// Token: 0x04000790 RID: 1936
			internal TKey _key;

			// Token: 0x04000791 RID: 1937
			internal int _partitionId = -1;
		}
	}
}
