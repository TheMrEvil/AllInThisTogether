using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x02000107 RID: 263
	internal class OrderPreservingMergeHelper<TInputOutput, TKey> : IMergeHelper<!0>
	{
		// Token: 0x060008B4 RID: 2228 RVA: 0x0001DDF7 File Offset: 0x0001BFF7
		internal OrderPreservingMergeHelper(PartitionedStream<TInputOutput, TKey> partitions, TaskScheduler taskScheduler, CancellationState cancellationState, int queryId)
		{
			this._taskGroupState = new QueryTaskGroupState(cancellationState, queryId);
			this._partitions = partitions;
			this._results = new Shared<TInputOutput[]>(null);
			this._taskScheduler = taskScheduler;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001DE27 File Offset: 0x0001C027
		void IMergeHelper<!0>.Execute()
		{
			OrderPreservingSpoolingTask<TInputOutput, TKey>.Spool(this._taskGroupState, this._partitions, this._results, this._taskScheduler);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0001DE46 File Offset: 0x0001C046
		IEnumerator<TInputOutput> IMergeHelper<!0>.GetEnumerator()
		{
			return this._results.Value.GetEnumerator();
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001DE58 File Offset: 0x0001C058
		public TInputOutput[] GetResultsAsArray()
		{
			return this._results.Value;
		}

		// Token: 0x04000609 RID: 1545
		private QueryTaskGroupState _taskGroupState;

		// Token: 0x0400060A RID: 1546
		private PartitionedStream<TInputOutput, TKey> _partitions;

		// Token: 0x0400060B RID: 1547
		private Shared<TInputOutput[]> _results;

		// Token: 0x0400060C RID: 1548
		private TaskScheduler _taskScheduler;
	}
}
