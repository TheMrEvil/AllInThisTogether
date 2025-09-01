using System;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x02000181 RID: 385
	internal class PartitionedStreamMerger<TOutput> : IPartitionedStreamRecipient<TOutput>
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00024954 File Offset: 0x00022B54
		internal MergeExecutor<TOutput> MergeExecutor
		{
			get
			{
				return this._mergeExecutor;
			}
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002495C File Offset: 0x00022B5C
		internal PartitionedStreamMerger(bool forEffectMerge, ParallelMergeOptions mergeOptions, TaskScheduler taskScheduler, bool outputOrdered, CancellationState cancellationState, int queryId)
		{
			this._forEffectMerge = forEffectMerge;
			this._mergeOptions = mergeOptions;
			this._isOrdered = outputOrdered;
			this._taskScheduler = taskScheduler;
			this._cancellationState = cancellationState;
			this._queryId = queryId;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00024991 File Offset: 0x00022B91
		public void Receive<TKey>(PartitionedStream<TOutput, TKey> partitionedStream)
		{
			this._mergeExecutor = MergeExecutor<TOutput>.Execute<TKey>(partitionedStream, this._forEffectMerge, this._mergeOptions, this._taskScheduler, this._isOrdered, this._cancellationState, this._queryId);
		}

		// Token: 0x04000732 RID: 1842
		private bool _forEffectMerge;

		// Token: 0x04000733 RID: 1843
		private ParallelMergeOptions _mergeOptions;

		// Token: 0x04000734 RID: 1844
		private bool _isOrdered;

		// Token: 0x04000735 RID: 1845
		private MergeExecutor<TOutput> _mergeExecutor;

		// Token: 0x04000736 RID: 1846
		private TaskScheduler _taskScheduler;

		// Token: 0x04000737 RID: 1847
		private int _queryId;

		// Token: 0x04000738 RID: 1848
		private CancellationState _cancellationState;
	}
}
