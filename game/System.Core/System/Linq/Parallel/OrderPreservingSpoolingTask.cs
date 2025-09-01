using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001D6 RID: 470
	internal class OrderPreservingSpoolingTask<TInputOutput, TKey> : SpoolingTaskBase
	{
		// Token: 0x06000BC0 RID: 3008 RVA: 0x00029784 File Offset: 0x00027984
		private OrderPreservingSpoolingTask(int taskIndex, QueryTaskGroupState groupState, Shared<TInputOutput[]> results, SortHelper<TInputOutput> sortHelper) : base(taskIndex, groupState)
		{
			this._results = results;
			this._sortHelper = sortHelper;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000297A0 File Offset: 0x000279A0
		internal static void Spool(QueryTaskGroupState groupState, PartitionedStream<TInputOutput, TKey> partitions, Shared<TInputOutput[]> results, TaskScheduler taskScheduler)
		{
			int maxToRunInParallel = partitions.PartitionCount - 1;
			SortHelper<TInputOutput, TKey>[] sortHelpers = SortHelper<TInputOutput, TKey>.GenerateSortHelpers(partitions, groupState);
			Task task = new Task(delegate()
			{
				for (int j = 0; j < maxToRunInParallel; j++)
				{
					new OrderPreservingSpoolingTask<TInputOutput, TKey>(j, groupState, results, sortHelpers[j]).RunAsynchronously(taskScheduler);
				}
				new OrderPreservingSpoolingTask<TInputOutput, TKey>(maxToRunInParallel, groupState, results, sortHelpers[maxToRunInParallel]).RunSynchronously(taskScheduler);
			});
			groupState.QueryBegin(task);
			task.RunSynchronously(taskScheduler);
			for (int i = 0; i < sortHelpers.Length; i++)
			{
				sortHelpers[i].Dispose();
			}
			groupState.QueryEnd(false);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00029840 File Offset: 0x00027A40
		protected override void SpoolingWork()
		{
			TInputOutput[] value = this._sortHelper.Sort();
			if (!this._groupState.CancellationState.MergedCancellationToken.IsCancellationRequested && this._taskIndex == 0)
			{
				this._results.Value = value;
			}
		}

		// Token: 0x04000850 RID: 2128
		private Shared<TInputOutput[]> _results;

		// Token: 0x04000851 RID: 2129
		private SortHelper<TInputOutput> _sortHelper;

		// Token: 0x020001D7 RID: 471
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x06000BC3 RID: 3011 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x06000BC4 RID: 3012 RVA: 0x00029888 File Offset: 0x00027A88
			internal void <Spool>b__0()
			{
				for (int i = 0; i < this.maxToRunInParallel; i++)
				{
					new OrderPreservingSpoolingTask<TInputOutput, TKey>(i, this.groupState, this.results, this.sortHelpers[i]).RunAsynchronously(this.taskScheduler);
				}
				new OrderPreservingSpoolingTask<TInputOutput, TKey>(this.maxToRunInParallel, this.groupState, this.results, this.sortHelpers[this.maxToRunInParallel]).RunSynchronously(this.taskScheduler);
			}

			// Token: 0x04000852 RID: 2130
			public QueryTaskGroupState groupState;

			// Token: 0x04000853 RID: 2131
			public Shared<TInputOutput[]> results;

			// Token: 0x04000854 RID: 2132
			public SortHelper<TInputOutput, TKey>[] sortHelpers;

			// Token: 0x04000855 RID: 2133
			public TaskScheduler taskScheduler;

			// Token: 0x04000856 RID: 2134
			public int maxToRunInParallel;
		}
	}
}
